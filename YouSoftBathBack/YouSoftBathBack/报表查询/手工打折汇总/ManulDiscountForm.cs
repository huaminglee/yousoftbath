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

namespace YouSoftBathBack
{
    public partial class ManulDiscountForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private bool use_pad;

        //构造函数
        public ManulDiscountForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void BounusDetailsTableForm_Load(object sender, EventArgs e)
        {
            List<Employee> es = new List<Employee>();
            foreach (var em in db.Employee)
            {
                if (BathClass.getAuthority(db, em, "手工打折"))
                    es.Add(em);
            }
            employee.Items.AddRange(es.Select(x => x.name).ToArray());
            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";

            startTime.Value = Convert.ToDateTime(BathClass.Now(LogIn.connectionString).AddMonths(-1).ToShortDateString() + " 00:00:00");

            use_pad = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);
            dgv_show();
        }

        //显示信息
        private void dgv_show()
        {
            dgv.Rows.Clear();
            var ops = db.Operation.Where(x => x.explain == "手工打折");
            ops = ops.Where(x => x.opTime >= startTime.Value && x.opTime <= endTime.Value);

            if (cEmployee.Checked)
                ops = ops.Where(x => x.employee == employee.Text);

            foreach (var op in ops)
            {
                var x = db.Account.FirstOrDefault(a => a.systemId == op.note2);
                if (x == null) continue;
                string[] row = {
                                   x.id.ToString(),
                                   x.text,
                                   x.systemId,
                                   x.payTime.ToString(),
                                   x.payEmployee,
                                   op.employee,
                                   op.note1,
                                   BathClass.get_account_money(x).ToString()
                               };
                dgv.Rows.Add(row);
            }
            BathClass.set_dgv_fit(dgv);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "提成明细统计", false, "");
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
                case Keys.F3:
                    dgv_show();
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "提成明细统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void cBoxCatgory_CheckedChanged(object sender, EventArgs e)
        {
            employee.Enabled = cEmployee.Checked;
        }

        //显示订单详情
        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
                return;

            int act_id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var orders = db.HisOrders.Where(x => x.accountId == act_id && x.deleteEmployee == null);
            dgvOrders.DataSource = from x in orders
                                   orderby x.id
                                   select new
                                   {
                                       系统账号 = x.systemId,
                                       手牌号 = x.text,
                                       房间 = x.roomId,
                                       录入员工 = x.inputEmployee,
                                       项目名称 = x.menu,
                                       单价 = db.Menu.FirstOrDefault(y => y.name == x.menu) == null ? 0 : db.Menu.FirstOrDefault(y => y.name == x.menu).price,
                                       数量 = x.number,
                                       金额 = x.money,
                                       技师号 = x.technician,
                                       消费时间 = x.inputTime
                                   };
            dgvOrders.Columns[2].Visible = use_pad;
            BathClass.set_dgv_fit(dgvOrders);
        }
    }
}
