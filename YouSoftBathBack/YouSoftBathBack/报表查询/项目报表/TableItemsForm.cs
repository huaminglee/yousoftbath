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
using YouSoftBathConstants;

namespace YouSoftBathBack
{
    public partial class TableItemsForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime lastTime;
        private DateTime thisTime;
        private string format;

        private static string SHOW_TIME = "0";
        private static string SHOW_NO_TIME = "1";

        //构造函数
        public TableItemsForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void TableItemsForm_Load(object sender, EventArgs e)
        {
            format = IOUtil.get_config_by_key(ConfigKeys.KEY_ITEM_TABLE_FORMAT);
            if (format == "")
            {
                format = SHOW_NO_TIME;
                IOUtil.set_config_by_key(ConfigKeys.KEY_ITEM_TABLE_FORMAT, format);
            }
            searchType.SelectedIndex = 0;

            set_datetime_format();
        }

        //获取夜审时间
        private bool get_clear_table_time()
        {
            var lct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == startDate.Value.Date);
            if (lct == null)
            {
                lct = db.ClearTable.Where(x => x.clearTime < startDate.Value).OrderByDescending(x => x.clearTime).FirstOrDefault();
                if (lct == null)
                    lastTime = DateTime.Parse("2013-01-01");
                else
                    lastTime = lct.clearTime;
            }
            else
                lastTime = lct.clearTime;

            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == endDate.Value.AddDays(1).Date);
            if (ct == null)
            {
                GeneralClass.printErrorMsg("没有夜审，不能查询");
                return false;
            }

            thisTime = ct.clearTime;

            return true;
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            if (format == SHOW_NO_TIME)
            {
                if (!get_clear_table_time())
                    return;
            }
            else
            {
                lastTime = startDate.Value;
                thisTime = endDate.Value;
            }

            dgv.Rows.Clear();
            if (searchType.Text == "单项项目统计")
                find_0();
            else if (searchType.Text == "已发生项目汇总")
                find_1();
            else if (searchType.Text == "已结账项目明细")
                find_2();
            else if (searchType.Text == "已结账项目汇总")
                find_3();
            else if (searchType.Text == "微信赠送项目明细")
                find_wx_donor();

            BathClass.set_dgv_fit(dgv);
        }

        //单项项目统计
        private void find_0()
        {
            dgv.Columns.Clear();
            add_cols_to_dgv(dgv, "编号");
            add_cols_to_dgv(dgv, "发生时间");
            add_cols_to_dgv(dgv, "项目名称");
            add_cols_to_dgv(dgv, "手牌号");
            add_cols_to_dgv(dgv, "数量");
            add_cols_to_dgv(dgv, "金额");
            add_cols_to_dgv(dgv, "输入员工");
            add_cols_to_dgv(dgv, "技师号");
            add_cols_to_dgv(dgv, "账单号");
            add_cols_to_dgv(dgv, "单据号");

            var orderLst = db.Orders.Where(x => x.menu == menu.Text && x.inputTime >= lastTime && 
                x.inputTime <= thisTime && x.deleteEmployee == null);

            var paid_orderLst = db.HisOrders.Where(x => x.menu == menu.Text && x.inputTime >= lastTime &&
                x.inputTime <= thisTime && x.deleteEmployee == null);

            if (cboxBill.Checked && tbBill.Text != "")
            {
                orderLst = db.Orders.Where(x => x.billId == tbBill.Text);
                paid_orderLst = db.HisOrders.Where(x => x.billId == tbBill.Text);

                if (!orderLst.Any() && !paid_orderLst.Any())
                {
                    BathClass.printErrorMsg("输入单据号不存在！");
                    return;
                }
            }

            double number = 0;
            double money = 0;
            foreach(Orders order in orderLst)
            {
                number += order.number;
                money += Convert.ToDouble(order.money);
                dgv.Rows.Add(order.id, order.inputTime, order.menu, order.text, order.number, order.money, 
                    order.inputEmployee, order.technician, order.accountId, order.billId);
            }
            foreach (var order in paid_orderLst)
            {
                number += order.number;
                money += Convert.ToDouble(order.money);
                dgv.Rows.Add(order.id, order.inputTime, order.menu, order.text, order.number, order.money,
                    order.inputEmployee, order.technician, order.accountId, order.billId);
            }
            dgv.Rows.Add("合计", "", menu.Text, "", number, money);
            BathClass.set_dgv_fit(dgv);
        }

        //已发生项目汇总
        private void find_1()
        {
            dgv.Columns.Clear();
            add_cols_to_dgv(dgv, "项目名称");
            add_cols_to_dgv(dgv, "已结账数量");
            add_cols_to_dgv(dgv, "已结账金额");
            add_cols_to_dgv(dgv, "未结账数量");
            add_cols_to_dgv(dgv, "未结账金额");


            IQueryable<YouSoftBathGeneralClass.Menu> menuList = db.Menu.OrderBy(x => x.catgoryId);
            if (menu.SelectedIndex != 0)
            {
                var catId = db.Catgory.FirstOrDefault(x => x.name == menu.Text).id;
                menuList = db.Menu.Where(x => x.catgoryId == catId);
            }
            foreach (var m in menuList)
            {
                var unpaid_orders = db.Orders.Where(x => x.menu == m.name && 
                    x.inputTime >= lastTime && 
                    x.inputTime <= thisTime && x.deleteEmployee == null);
                var paid_orders = db.HisOrders.Where(x => x.menu == m.name &&
                                    x.inputTime >= lastTime &&
                                    x.inputTime <= thisTime && x.deleteEmployee == null);
                if (!paid_orders.Any() && !unpaid_orders.Any())
                    continue;

                double paidMoney = 0;
                double paid_count = 0;
                if (paid_orders.Any())
                {
                    paid_count = paid_orders.Sum(x => x.number);
                    paidMoney = paid_orders.Sum(x => x.money);
                }

                double unpaidMoney = 0;
                double unpaid_count = 0;
                if (unpaid_orders.Any())
                {
                    unpaid_count = unpaid_orders.Sum(x => x.number);
                    unpaidMoney = unpaid_orders.Sum(x => x.money);
                }

                dgv.Rows.Add(m.name, paid_count, paidMoney, unpaid_count, unpaidMoney);
            }
        }

        //已结账项目明细
        private void find_2()
        {
            dgv.Columns.Clear();
            add_cols_to_dgv(dgv, "编号");
            add_cols_to_dgv(dgv, "发生时间");
            add_cols_to_dgv(dgv, "项目名称");
            add_cols_to_dgv(dgv, "技师号");
            add_cols_to_dgv(dgv, "手牌号");
            add_cols_to_dgv(dgv, "数量");
            add_cols_to_dgv(dgv, "金额");
            add_cols_to_dgv(dgv, "输入员工");
            add_cols_to_dgv(dgv, "结账单号");
            add_cols_to_dgv(dgv, "单据号");

            if (cboxBill.Checked && tbBill.Text != "")
            {
                var o = db.HisOrders.FirstOrDefault(x => x.billId == tbBill.Text);
                if (o == null)
                {
                    BathClass.printErrorMsg("输入单据号不存在！");
                    return;
                }
                dgv.Rows.Add(o.id,
                        o.inputTime,
                        o.menu,
                        o.technician,
                        o.text,
                        o.number,
                        o.money,
                        o.inputEmployee,
                        o.accountId,
                        o.billId);
                return;
            }

            IQueryable<YouSoftBathGeneralClass.Menu> menuList = db.Menu.OrderBy(x => x.catgoryId);
            if (menu.SelectedIndex != 0)
            {
                var catId = db.Catgory.FirstOrDefault(x => x.name == menu.Text).id;
                menuList = db.Menu.Where(x => x.catgoryId == catId);
            }

            var ids = db.Account.Where(x => x.payTime >= lastTime && 
                x.payTime <= thisTime && x.abandon == null).Select(x => x.id).ToList();
            
            foreach (var m in menuList)
            {
                var ol = db.HisOrders.Where(x => x.menu == m.name && x.accountId != null && ids.Contains(x.accountId.Value) && x.deleteEmployee == null);
                if (ol.Count() == 0)
                    continue;
                foreach (HisOrders o in ol)
                {
                    dgv.Rows.Add(o.id, 
                        o.inputTime, 
                        m.name, 
                        o.technician, 
                        o.text, 
                        o.number,
                        o.money,
                        o.inputEmployee,
                        o.accountId,
                        o.billId);
                }
            }
        }

        //已结账项目汇总
        private void find_3()
        {
            dgv.Columns.Clear();
            add_cols_to_dgv(dgv, "项目名称");
            add_cols_to_dgv(dgv, "技师号");
            add_cols_to_dgv(dgv, "数量");
            add_cols_to_dgv(dgv, "单价");
            add_cols_to_dgv(dgv, "金额");
            

            IQueryable<YouSoftBathGeneralClass.Menu> menuList = db.Menu.OrderBy(x => x.catgoryId);
            if (menu.SelectedIndex != 0)
            {
                var catId = db.Catgory.FirstOrDefault(x => x.name == menu.Text).id;
                menuList = db.Menu.Where(x => x.catgoryId == catId);
            }
            
            var ids = db.Account.Where(x => x.payTime >= lastTime && 
                x.payTime <= thisTime && x.abandon == null).Select(x => x.id).ToList();

            foreach (var m in menuList)
            {
                var ol = db.HisOrders.Where(x => x.menu == m.name && x.accountId != null && ids.Contains(x.accountId.Value) && x.deleteEmployee == null);
                if (ol.Count() == 0)
                    continue;

                if (m.technician)
                {
                    var techList = ol.Select(x => x.technician).Distinct();
                    foreach (string tech in techList)
                    {
                        var olt = ol.Where(x => x.technician == tech);
                        if(olt.Any())
                            dgv.Rows.Add(m.name, tech, olt.Sum(x => x.number), m.price, olt.Sum(x => x.money));
                    }
                }
                else
                {
                    dgv.Rows.Add(m.name, "", ol.Sum(x => x.number), m.price, ol.Sum(x => x.money));
                }
            }
        }

        //微信赠送项目明细
        private void find_wx_donor()
        {
            dgv.Columns.Clear();
            add_cols_to_dgv(dgv, "编号");
            add_cols_to_dgv(dgv, "发生时间");
            add_cols_to_dgv(dgv, "项目名称");
            add_cols_to_dgv(dgv, "赠送员工");
            add_cols_to_dgv(dgv, "赠送时间");
            add_cols_to_dgv(dgv, "技师号");
            add_cols_to_dgv(dgv, "手牌号");
            add_cols_to_dgv(dgv, "数量");
            add_cols_to_dgv(dgv, "金额");
            add_cols_to_dgv(dgv, "输入员工");
            add_cols_to_dgv(dgv, "结账单号");
            add_cols_to_dgv(dgv, "单据号");

            if (cboxBill.Checked && tbBill.Text != "")
            {
                var o = db.HisOrders.FirstOrDefault(x => x.billId == tbBill.Text);
                if (o == null)
                {
                    BathClass.printErrorMsg("输入单据号不存在！");
                    return;
                }
                dgv.Rows.Add(o.id,
                        o.inputTime,
                        o.menu,
                        o.donorEmployee,
                        o.donorTime,
                        o.technician,
                        o.text,
                        o.number,
                        o.money,
                        o.inputEmployee,
                        o.accountId,
                        o.billId);
                return;
            }

            IQueryable<YouSoftBathGeneralClass.Menu> menuList = db.Menu.OrderBy(x => x.catgoryId);
            if (menu.SelectedIndex != 0)
            {
                var catId = db.Catgory.FirstOrDefault(x => x.name == menu.Text).id;
                menuList = db.Menu.Where(x => x.catgoryId == catId);
            }

            var orders = db.HisOrders.Where(x => x.donorExplain != null && x.donorExplain == Constants.WX_DONOR);
            orders = orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

            foreach (var m in menuList)
            {
                var ol = orders.Where(x => x.menu == m.name);
                //var ol = db.HisOrders.Where(x => x.menu == m.name && x.accountId != null && ids.Contains(x.accountId.Value) && x.deleteEmployee == null);
                //if (!ol.Any())
                //    continue;
                foreach (HisOrders o in ol)
                {
                    dgv.Rows.Add(o.id,
                        o.inputTime,
                        m.name,
                        o.donorEmployee,
                        o.donorTime,
                        o.technician,
                        o.text,
                        o.number,
                        o.money,
                        o.inputEmployee,
                        o.accountId,
                        o.billId);
                }
            }
        }

        //往dgv中添加列
        private void add_cols_to_dgv(DataGridView pdgv, string header)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = header;
            pdgv.Columns.Add(col);
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            //BathClass.exportDgvToExcel(dgv);
            ExportToExcel.ExportExcel("项目报表 " + startDate.Value.ToString("yyyy-MM-dd"), dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "项目报表", false, "作业时间:" + startDate.Text);
        }

        //查询方式改变
        private void searchType_TextChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (searchType.Text == "单项项目统计")
            {
                cboxBill.Visible = true;
                tbBill.Visible = true;

                menu.Items.Clear();
                menu.Items.AddRange(db.Menu.Select(x => x.name).ToArray());
                if (menu.Items.Count != 0)
                    menu.SelectedIndex = 0;

            }
            else if (searchType.Text == "已发生项目汇总")
            {
                cboxBill.Visible = false;
                tbBill.Visible = false;

                menu.Items.Clear();
                menu.Items.Add("所有类别");
                menu.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
                if (menu.Items.Count != 0)
                    menu.SelectedIndex = 0;
            }
            else if (searchType.Text == "已结账项目明细")
            {
                cboxBill.Visible = true;
                tbBill.Visible = true;

                menu.Items.Clear();
                menu.Items.Add("所有类别");
                menu.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
                if (menu.Items.Count != 0)
                    menu.SelectedIndex = 0;
            }
            else if (searchType.Text == "已结账项目汇总")
            {
                cboxBill.Visible = false;
                tbBill.Visible = false;

                menu.Items.Clear();
                menu.Items.Add("所有类别");
                menu.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
                if (menu.Items.Count != 0)
                    menu.SelectedIndex = 0;

            }
            else if (searchType.Text=="微信赠送项目明细")
            {
                menu.Items.Clear();
                menu.Items.Add("所有类别");
                menu.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
                if (menu.Items.Count != 0)
                    menu.SelectedIndex = 0;
            }
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
                    findTool_Click(null, null);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "项目报表", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboxBill_CheckedChanged(object sender, EventArgs e)
        {
            tbBill.Enabled = cboxBill.Checked;
        }

        //查询明细
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (searchType.Text != "已发生项目汇总" || dgv.CurrentCell == null)
                return;

            var menu_name = dgv.CurrentRow.Cells[0].Value.ToString();
            var form = new TechItemsDetailsDetailsForm(menu_name, lastTime, thisTime);
            form.ShowDialog();
        }

        private void set_datetime_format()
        {
            if (format == SHOW_TIME)
            {
                startDate.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
                endDate.CustomFormat = "yyyy-MM-dd-HH:mm:ss";

                startDate.Value = DateTime.Now.AddDays(-1);
            }
            else
            {
                startDate.CustomFormat = "yyyy-MM-dd";
                endDate.CustomFormat = "yyyy-MM-dd";

                startDate.Value = DateTime.Now.AddDays(-1);
                endDate.Value = DateTime.Now.AddDays(-1);
            }
        }

        //切换格式
        private void formatTool_Click(object sender, EventArgs e)
        {
            if (format == SHOW_NO_TIME)
                format = SHOW_TIME;
            else
                format = SHOW_NO_TIME;

            set_datetime_format();
            IOUtil.set_config_by_key(ConfigKeys.KEY_ITEM_TABLE_FORMAT, format);
        }

    }
}
