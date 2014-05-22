using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Threading;

namespace YouSoftBathBack
{
    public partial class TechMenuDetailsForm : Form
    {
        //成员变量
        private Thread m_thread_details;
        private DateTime m_lastTime;//起始时间
        private DateTime m_thisTime;//终止时间

        private string m_tech_id;
        private string m_menu_name;
        private bool m_input;
        private int m_format;

        private int FORMAT_ALL_NODIANLUN = 0;//已结未结 不区分点钟，轮钟
        private int FORMAT_ALL_DIANLUN = 1;//已结未结  区分点钟轮钟
        private int FORMAT_INPUTTIME_DIANLUN = 2;//纯粹按照输入时间 区分点钟轮钟
        private int FORMAT_INPUTTIME_NODIANLUN = 3;//纯粹按照输入时间 不区分点钟轮钟

        //构造函数
        public TechMenuDetailsForm(string tech_id, string menu_name, 
            DateTime lastTime, DateTime thisTime, bool input, int format)
        {
            m_input = input;
            m_tech_id = tech_id;
            m_menu_name = menu_name;
            m_lastTime = lastTime;
            m_thisTime = thisTime;
            m_format = format;
            InitializeComponent();
        }

        //对话框载入
        private void BonusTableForm_Load(object sender, EventArgs e)
        {
            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
            dgvDetails.RowsDefaultCellStyle.Font = new Font("宋体", 15);
            bool use_pad = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);

            dgvDetails.Rows.Clear();
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            if (m_format == FORMAT_ALL_DIANLUN || m_format == FORMAT_ALL_NODIANLUN)
            {
                dgvDetails.Columns[3].Visible = true;
                dgvDetails.Columns[2].Visible = m_input;
                dgvDetails.Columns[12].Visible = use_pad;
                m_thread_details = new Thread(new ThreadStart(dgvDetails_show));
                m_thread_details.Start();
            }
            else
            {
                dgvDetails.Columns[3].Visible = false;
                dgvDetails.Columns[2].Visible = m_input;
                dgvDetails.Columns[12].Visible = use_pad;
                m_thread_details = new Thread(new ThreadStart(dgvDetails_input_show));
                m_thread_details.Start();
            }
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);
        private delegate void delegate_add_row_details(object[] vals);
        private void add_row_details(object[] vals)
        {
            dgvDetails.Rows.Add(vals);
        }

        //显示详细订单信息
        private void dgvDetails_input_show()
        {
            if (m_tech_id == "" || m_menu_name == "")
                return;

            var dc = new BathDBDataContext(LogIn.connectionString);

            var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= m_lastTime && x.inputTime <= m_thisTime && x.deleteEmployee == null);

            var orderLst = dc.Orders.Where(x => x.inputTime >= m_lastTime && x.inputTime <= m_thisTime && x.deleteEmployee == null);
            orderLst = orderLst.Where(x => x.technician != null);
            all_his_orders = all_his_orders.Where(x => x.technician != null);

            orderLst = orderLst.Where(x => x.technician == m_tech_id && x.menu == m_menu_name);
            all_his_orders = all_his_orders.Where(x => x.technician == m_tech_id && x.menu == m_menu_name);

            foreach (var o in orderLst)
            {
                var paid = true;
                var paidTime = o.accountId == null ? "" : dc.Account.FirstOrDefault(y => y.id == o.accountId.Value).payTime.ToString();

                object[] row = { false, o.id, o.billId, paid, o.menu, o.text, o.systemId, o.number, o.money, o.techType, o.inputTime, paidTime };
                this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
            }

            foreach (var o in all_his_orders)
            {
                var act = dc.Account.FirstOrDefault(y => y.id == o.accountId.Value);
                var paid = true;

                var paidTime = "";
                if (o.accountId != null && act != null)
                    paidTime = act.payTime.ToString();

                object[] row = { false, o.id, o.billId, paid, o.menu, o.text, o.systemId, o.number, o.money, o.techType, o.inputTime, paidTime };
                this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
            }
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgvDetails);
        }

        //显示详细订单信息
        private void dgvDetails_show()
        {
            if (m_tech_id == "" || m_menu_name == "")
                return;

            var dc = new BathDBDataContext(LogIn.connectionString);
            
            var accountList = dc.Account.Where(x => x.payTime >= m_lastTime && x.payTime <= m_thisTime && x.abandon == null);
            if (!accountList.Any())
                return;

            var idLst = accountList.Select(x => x.id);
            var paid_orderLst = dc.HisOrders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
            var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= m_lastTime && x.inputTime <= m_thisTime && x.deleteEmployee == null);
            all_his_orders = paid_orderLst.Union(all_his_orders).Distinct();

            var orderLst = dc.Orders.Where(x => x.inputTime >= m_lastTime && x.inputTime <= m_thisTime && x.deleteEmployee == null);
            orderLst = orderLst.Where(x => x.technician != null);
            all_his_orders = all_his_orders.Where(x => x.technician != null);

            orderLst = orderLst.Where(x => x.technician == m_tech_id && x.menu == m_menu_name);
            all_his_orders = all_his_orders.Where(x => x.technician == m_tech_id && x.menu == m_menu_name);

            foreach (var o in orderLst)
            {
                var paid = o.accountId != null && idLst.Contains(o.accountId.Value);
                var paidTime = o.accountId == null ? "" : dc.Account.FirstOrDefault(y => y.id == o.accountId.Value).payTime.ToString();

                object[] row = { false, o.id, o.billId, paid, o.menu, o.text, o.systemId, o.number, o.money, 
                                   o.techType, o.inputTime, paidTime, o.roomId };
                this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
            }

            foreach (var o in all_his_orders)
            {
                var act = dc.Account.FirstOrDefault(y => y.id == o.accountId.Value);
                var paid = o.accountId != null && idLst.Contains(o.accountId.Value);

                var paidTime = "";
                if (o.accountId != null && act != null)
                    paidTime =act.payTime.ToString();
               
                object[] row = { false, o.id, o.billId, paid, o.menu, o.text, o.systemId, o.number, o.money, 
                                   o.techType, o.inputTime, paidTime, o.roomId  };
                this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
            }
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgvDetails);
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void BonusTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
        }

        private void dgvDetails_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvDetails.CurrentCell == null || dgvDetails.CurrentCell.ColumnIndex != 0)
                return;

            var has_checked = (dgvDetails.CurrentCell.EditedFormattedValue.ToString() == "False");
            if (has_checked)
            {
                //dgvDetails.CurrentRow.Cells[0].Value = false;
                dgvDetails.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                //dgvDetails.CurrentRow.Cells[0].Value = true;
                dgvDetails.CurrentRow.DefaultCellStyle.BackColor = Color.Gray;
            }
        }
    }
}
