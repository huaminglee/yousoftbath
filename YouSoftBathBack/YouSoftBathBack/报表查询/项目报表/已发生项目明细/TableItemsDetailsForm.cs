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
    public partial class TechItemsDetailsDetailsForm : Form
    {
        //成员变量
        private Thread m_thread_details;
        private DateTime m_lastTime;//起始时间
        private DateTime m_thisTime;//终止时间

        private string m_menu_name;

        //构造函数
        public TechItemsDetailsDetailsForm(string menu_name, DateTime lastTime, DateTime thisTime)
        {
            m_menu_name = menu_name;
            m_lastTime = lastTime;
            m_thisTime = thisTime;
            InitializeComponent();
        }

        //对话框载入
        private void BonusTableForm_Load(object sender, EventArgs e)
        {
            dgvDetails.Rows.Clear();
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
            dgvDetails.RowsDefaultCellStyle.Font = new Font("宋体", 15);

            m_thread_details = new Thread(new ThreadStart(dgvDetails_show));
            m_thread_details.Start();
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);
        private delegate void delegate_add_row_details(object[] vals);
        private void add_row_details(object[] vals)
        {
            dgvDetails.Rows.Add(vals);
        }

        //显示详细订单信息
        private void dgvDetails_show()
        {
            var db = new BathDBDataContext(LogIn.connectionString);

            var orderLst = db.Orders.Where(x => x.menu == m_menu_name && x.inputTime >= m_lastTime &&
                x.inputTime <= m_thisTime && x.deleteEmployee == null);

            var paid_orderLst = db.HisOrders.Where(x => x.menu == m_menu_name && x.inputTime >= m_lastTime &&
                x.inputTime <= m_thisTime && x.deleteEmployee == null);

            double number = 0;
            double money = 0;
            foreach (Orders order in orderLst)
            {
                number += order.number;
                money += Convert.ToDouble(order.money);
                this.Invoke(new delegate_add_row_details(add_row_details),
                    (Object)new object[] { false, order.id, order.inputTime, order.menu, order.text, order.number, order.money,
                        order.inputEmployee, order.technician, order.accountId, order.billId });
            }
            foreach (var order in paid_orderLst)
            {
                number += order.number;
                money += Convert.ToDouble(order.money);
                this.Invoke(new delegate_add_row_details(add_row_details),
                    (Object)new object[] { false, order.id, order.inputTime, order.menu, order.text, order.number, order.money,
                        order.inputEmployee, order.technician, order.accountId, order.billId });
            }

            this.Invoke(new delegate_add_row_details(add_row_details),
                (Object)new object[] { false, "合计", "", m_menu_name, "", number, money });

            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit),
                dgvDetails);
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

        //核对
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
