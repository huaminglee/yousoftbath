using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class MemberExpenseTableForm : Form
    {
        //成员变量
        //private BathDBDataContext db = null;
        private Thread m_thread;
        private Thread m_thread_details;

        //构造函数
        public MemberExpenseTableForm()
        {
            //db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MemberExpenseTableForm_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            startTime.Value = Convert.ToDateTime(BathClass.Now(LogIn.connectionString).AddMonths(-1).ToShortDateString() + " 00:00:00");

            //dgv_show();
        }

        private string ToString(object obj)
        {
            if (obj == null) return "";
            else return obj.ToString();
        }
        //显示信息
        private void dgv_show()
        {
            var dc = new BathDBDataContext(LogIn.connectionString);
            var cc = dc.CardCharge.Where(x => x.CC_InputDate >= startTime.Value && x.CC_InputDate <= endTime.Value);
            if (card.Text != "")
                cc = cc.Where(x => x.CC_CardNo == card.Text);

            cc = cc.OrderBy(x => x.CC_InputDate);
            foreach(var x in cc)
            {
                string t = "";
                var ci = dc.CardInfo.FirstOrDefault(y => y.CI_CardNo == x.CC_CardNo);
                if (ci != null)
                {
                    var mt = dc.MemberType.FirstOrDefault(z => z.id == ci.CI_CardTypeNo);
                    if (mt != null)
                        t = mt.name;
                }
                
                string[] row = {
                                   x.CC_CardNo,
                                   t,
                                   x.CC_ItemExplain,
                                   BathClass.get_member_balance(dc, x.CC_CardNo).ToString(),
                                   ToString(x.expense),
                                   x.CC_AccountNo,
                                   x.CC_InputOperator,
                                   x.CC_InputDate.ToString()
                               };
                this.Invoke(new delegate_add_row(add_row), (Object)row);
            }
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), (Object)dgv);
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);

        //显示详细订单信息
        private void dgvDetails_show()
        {
            var idobj = dgv.CurrentRow.Cells[5].Value;
            if (idobj == null)
                return;

            Int64 act_id;
            if (!Int64.TryParse(idobj.ToString(), out act_id))
                return;

            var dc = new BathDBDataContext(LogIn.connectionString);
            var act = dc.Account.FirstOrDefault(x => x.id == act_id);
            if (act == null)
                return;

            var orders = dc.HisOrders.Where(x => x.accountId == act_id);
            orders = orders.OrderBy(x => x.inputTime);
            foreach (var o in orders)
            {
                string[] row = new string[7];
                row[0] = o.text;
                row[1] = o.menu;
                row[2] = o.technician;

                row[4] = o.number.ToString();

                row[6] = o.inputEmployee;

                var m = dc.Menu.FirstOrDefault(x => x.name == o.menu);
                bool redRow = false;
                if (m == null)
                {
                    row[3] = "";
                    row[5] = o.money.ToString();
                    redRow = true;
                }
                else
                {
                    if (o.priceType == "每小时")
                    {
                        row[3] = o.money.ToString() + "/时";
                        row[5] = (Math.Ceiling((act.payTime - o.inputTime).TotalHours) * o.money).ToString();
                    }
                    else
                    {
                        row[3] = m.price.ToString();
                        row[5] = o.money.ToString();
                    }
                }
                this.Invoke(new delegate_add_row(add_row_details), (Object)row);
                //dgvExpense.Rows.Add(row);
                if (redRow)
                {
                    this.Invoke(new delegate_no_para(change_dgv_color), null);
                    //dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            //BathClass.set_dgv_fit(dgvExpense);
        }

        private delegate void delegate_no_para();
        private void change_dgv_color()
        {
            dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
        }

        private delegate void delegate_add_row(string[] vals);
        private void add_row(string[] vals)
        {
            dgv.Rows.Add(vals);
        }

        private void add_row_details(string[] vals)
        {
            dgvExpense.Rows.Add(vals);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            m_thread = new Thread(new ThreadStart(dgv_show));
            m_thread.Start();
            //dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            PrintDGV.Print_DataGridView(dgv, "会员卡消费统计", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    dgv_show();
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "会员卡消费统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void card_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void MemberExpenseTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            dgvExpense.Rows.Clear();
            if (dgv.CurrentCell == null)
                return;

            m_thread_details = new Thread(new ThreadStart(dgvDetails_show));
            m_thread_details.Start();
            //dgvDetails_show();
        }
    }
}
