using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class ReturnedBillTableForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private bool input_id;

        //构造函数
        public ReturnedBillTableForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void ReturnedBillTableForm_Load(object sender, EventArgs e)
        {
            input_id = MConvert<bool>.ToTypeOrDefault(db.Options.FirstOrDefault().录单输入单据编号, false);
            searchType.SelectedIndex = 0;

            menu.Items.AddRange(db.Menu.Select(x => x.name).ToArray());
            employee.Items.AddRange(db.Employee.Select(x => x.id).ToArray());

            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            startTime.Value = Convert.ToDateTime(BathClass.Now(LogIn.connectionString).AddMonths(-1).ToShortDateString() + " 00:00:00");

            dgv_show();
            dgv.Columns[0].Visible = input_id;
        }

        //显示信息
        private void dgv_show()
        {
            if (searchType.Text == "退单")
                findDeleteOrders();
            else if (searchType.Text == "免单")
                findFreeOrders();
        }

        //查询退单
        private void findDeleteOrders()
        {
            dgv.Rows.Clear();
            var orders = db.Orders.Where(x => x.inputTime >= startTime.Value && x.inputTime <= endTime.Value);
            var paid_orders = db.HisOrders.Where(x => x.inputTime >= startTime.Value && x.inputTime <= endTime.Value);
            orders = orders.Where(x => x.deleteEmployee != null);
            paid_orders = paid_orders.Where(x => x.deleteEmployee != null);

            if (cboxSeat.Checked)
            {
                orders = orders.Where(x => x.text == seat.Text);
                paid_orders = paid_orders.Where(x => x.text == seat.Text);
            }
            if (cboxMenu.Checked)
            {
                orders = orders.Where(x => x.menu == menu.Text);
                paid_orders = paid_orders.Where(x => x.menu == menu.Text);
            }
            if (cboxEmployee.Checked)
            {
                orders = orders.Where(x => x.deleteEmployee == employee.Text);
                paid_orders = paid_orders.Where(x => x.deleteEmployee == employee.Text);
            }

            foreach (var x in orders)
            {
                double price = 0;
                var menu = db.Menu.FirstOrDefault(y=>y.name==x.menu);
                if (menu != null)
                    price = menu.price;
                dgv.Rows.Add(x.billId, x.text, x.systemId, x.menu, price, x.number, x.money, 
                    x.technician, x.inputTime, x.inputEmployee, x.deleteEmployee,x.deleteExplain,x.deleteTime);
            }

            foreach (var x in paid_orders)
            {
                double price = 0;
                var menu = db.Menu.FirstOrDefault(y => y.name == x.menu);
                if (menu != null)
                    price = menu.price;
                dgv.Rows.Add(x.billId, x.text, x.systemId, x.menu, price, x.number, x.money,
                    x.technician, x.inputTime, x.inputEmployee, x.deleteEmployee,x.deleteExplain,x.deleteTime);
            }
            BathClass.set_dgv_fit(dgv);
        }

        //查询免单
        private void findFreeOrders()
        {
            var accounts = db.Account.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value && x.abandon == null);
            accounts = accounts.Where(x => x.serverEmployee != null);

            if (cboxSeat.Checked)
                accounts = accounts.Where(x => x.text == seat.Text);
            if (cboxEmployee.Checked)
                accounts = accounts.Where(x => x.serverEmployee == employee.Text);

            dgv.DataSource = from x in accounts
                             orderby x.payTime
                             select new
                             {
                                 手牌号 = x.text,
                                 系统账号 = x.systemId,
                                 金额 = x.server,
                                 结账时间 = x.payTime,
                                 操作员工 = x.serverEmployee
                             };
        }

        //手牌号
        private void cboxSeat_CheckedChanged(object sender, EventArgs e)
        {
            seat.Enabled = cboxSeat.Checked;
        }

        //项目
        private void cboxMenu_CheckedChanged(object sender, EventArgs e)
        {
            menu.Enabled = cboxMenu.Checked;
        }

        //操作员
        private void cboxEmployee_CheckedChanged(object sender, EventArgs e)
        {
            employee.Enabled = cboxEmployee.Checked;
            lEmployee.Visible = cboxEmployee.Checked;

            if (employee.Text == "")
                lEmployee.Text = "";
        }

        //选择操作员
        private void employee_TextChanged(object sender, EventArgs e)
        {
            lEmployee.Text = db.Employee.FirstOrDefault(x => x.id.ToString() == employee.Text).name;
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
            PrintDGV.Print_DataGridView(dgv, "退免单汇总", false, "");
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
                    PrintDGV.Print_DataGridView(dgv, "退免单汇总", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void seat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void searchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.Columns.Clear();
            if (searchType.SelectedIndex == 0)//退单
            {
                BathClass.add_cols_to_dgv(dgv, "单据号");
                BathClass.add_cols_to_dgv(dgv, "手牌号");
                BathClass.add_cols_to_dgv(dgv, "系统账号");
                BathClass.add_cols_to_dgv(dgv, "项目名称");
                BathClass.add_cols_to_dgv(dgv, "单价");
                BathClass.add_cols_to_dgv(dgv, "数量");
                BathClass.add_cols_to_dgv(dgv, "金额");
                BathClass.add_cols_to_dgv(dgv, "技师号");
                BathClass.add_cols_to_dgv(dgv, "录入时间");
                BathClass.add_cols_to_dgv(dgv, "录入员工");
                BathClass.add_cols_to_dgv(dgv, "退单员工");
                BathClass.add_cols_to_dgv(dgv, "退单原因");
                BathClass.add_cols_to_dgv(dgv, "退单时间");
            }
            else if (searchType.SelectedIndex == 1)
            {
                //BathClass.add_cols_to_dgv(dgv, "手牌号");
                //BathClass.add_cols_to_dgv(dgv, "系统账号");
                //BathClass.add_cols_to_dgv(dgv, "金额");
                //BathClass.add_cols_to_dgv(dgv, "结账时间");
                //BathClass.add_cols_to_dgv(dgv, "操作员工");
            }
        }
    }
}
