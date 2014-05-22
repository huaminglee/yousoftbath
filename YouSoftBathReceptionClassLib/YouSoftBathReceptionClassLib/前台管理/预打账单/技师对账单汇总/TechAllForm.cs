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

namespace YouSoftBathReception
{
    public partial class TechAllForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime lastTime;
        private DateTime thisTime;

        //构造函数
        public TechAllForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CashierForm_Load(object sender, EventArgs e)
        {
            this.Height = Screen.GetBounds(this).Height;
            try
            {
                catgory.Items.Add("所有类别");
                catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
                catgory.SelectedIndex = 0;
                this.Location = new Point(0, 0);
                toolFind_Click(null, null);
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
                this.Close();
            }
        }

        //获取夜审时间
        private bool get_clear_table_time()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
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

            var ct = db.ClearTable.Where(x => x.clearTime.Date >= endDate.Value.AddDays(1).Date).FirstOrDefault();
            if (ct == null)
                thisTime = DateTime.Now;
            else
                thisTime = ct.clearTime;

            return true;
        }

        //查询
        private void dgv_show()
        {
            dgv.Rows.Clear();

            var dc = new BathDBDataContext(LogIn.connectionString);
            var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
            var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

            all_his_orders = all_his_orders.Where(x => x.technician != null);
            orderLst = orderLst.Where(x => x.technician != null);

            if (catgory.Text != "所有类别")
            {
                var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                orderLst = orderLst.Where(x => menus.Contains(x.menu));
                all_his_orders = all_his_orders.Where(x => menus.Contains(x.menu));
            }
            var unpaid_techList = orderLst.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
            var all_his_techList = all_his_orders.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
            var techList = unpaid_techList.Union(all_his_techList).Distinct();
            foreach (string techId in techList)
            {
                double tech_bouns = 0;
                double tech_count = 0;

                var unpaid_tech_orders = orderLst.Where(x => x.technician == techId);
                var unpaid_menus = unpaid_tech_orders.Select(x => x.menu).Distinct();

                var all_his_tech_orders = all_his_orders.Where(x => x.technician == techId);
                var all_his_menus = all_his_tech_orders.Select(x => x.menu).Distinct();
                var menus = unpaid_menus.Union(all_his_menus).Distinct().ToList();
                foreach (var m in menus)
                {
                    var tech_menu = dc.Menu.FirstOrDefault(x => x.name == m);
                    if (tech_menu == null) continue;

                    var menu_ratio_type = tech_menu.techRatioType;
                    var menu_ratio_cat = tech_menu.techRatioCat;
                    var unpaid_tech_menu_orders = unpaid_tech_orders.Where(x => x.menu == m);
                    var all_his_tech_menu_orders = all_his_tech_orders.Where(x => x.menu == m);
                    if (!unpaid_tech_menu_orders.Any() && !all_his_tech_menu_orders.Any())
                        continue;

                    var unpaid_tech_menu_orders_lun = unpaid_tech_menu_orders.Where(x=>x.techType == null || x.techType == "轮钟");
                    var all_his_tech_menu_orders_lun = all_his_tech_menu_orders.Where(x => x.techType == null || x.techType == "轮钟");
                    if (unpaid_tech_menu_orders_lun.Any() || all_his_tech_menu_orders_lun.Any())
                    {
                        double paidMoney = 0;
                        double paid_count = 0;
                        double unpaidMoney = 0;
                        double unpaid_count = 0;

                        if (all_his_tech_menu_orders_lun.Any())
                        {
                            paidMoney = all_his_tech_menu_orders_lun.Sum(x => x.money);
                            paid_count = all_his_tech_menu_orders_lun.Sum(x => x.number);
                        }


                        if (unpaid_tech_menu_orders_lun.Any())
                        {
                            unpaid_count = unpaid_tech_menu_orders_lun.Sum(x => x.number);
                            unpaidMoney = unpaid_tech_menu_orders_lun.Sum(x => x.money);
                        }

                        double tech_menu_lun_bonus = 0;
                        if (menu_ratio_cat == "按金额")
                            tech_menu_lun_bonus = (paid_count + unpaid_count) * tech_menu.onRatio.Value;
                        else if (menu_ratio_cat == "按比例")
                        {
                            if (menu_ratio_type == "按实收")
                            {
                                tech_menu_lun_bonus = Math.Round((paidMoney + unpaidMoney) * tech_menu.onRatio.Value / 100.0, 1);
                            }
                            else if (menu_ratio_type == "按原价")
                            {
                                tech_menu_lun_bonus = Math.Round((paid_count + unpaid_count) * tech_menu.price * tech_menu.onRatio.Value / 100.0, 1);
                            }
                        }

                        string[] tech_objs = { techId, m, "轮钟",(paid_count+unpaid_count).ToString(), 
                                             tech_menu_lun_bonus.ToString()};
                        dgv.Rows.Add(tech_objs);

                        tech_count += paid_count + unpaid_count;
                        tech_bouns += tech_menu_lun_bonus;
                    }

                    var unpaid_tech_menu_orders_order = unpaid_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                    var all_his_tech_menu_orders_order = all_his_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                    if (unpaid_tech_menu_orders_order.Any() || all_his_tech_menu_orders_order.Any())
                    {
                        double paidMoney = 0;
                        double paid_count = 0;
                        double unpaidMoney = 0;
                        double unpaid_count = 0;

                        if (all_his_tech_menu_orders_order.Any())
                        {
                            paidMoney = all_his_tech_menu_orders_order.Sum(x => x.money);
                            paid_count = all_his_tech_menu_orders_order.Sum(x => x.number);
                        }


                        if (unpaid_tech_menu_orders_order.Any())
                        {
                            unpaid_count = unpaid_tech_menu_orders_order.Sum(x => x.number);
                            unpaidMoney = unpaid_tech_menu_orders_order.Sum(x => x.money);
                        }

                        double tech_menu_order_bonus = 0;
                        if (menu_ratio_cat == "按金额")
                            tech_menu_order_bonus = (paid_count + unpaid_count) * tech_menu.orderRatio.Value;
                        else if (menu_ratio_cat == "按比例")
                        {
                            if (menu_ratio_type == "按实收")
                            {
                                tech_menu_order_bonus = Math.Round((paidMoney + unpaidMoney) * tech_menu.orderRatio.Value / 100.0, 1);
                            }
                            else if (menu_ratio_type == "按原价")
                            {
                                tech_menu_order_bonus = Math.Round((paid_count + unpaid_count) * tech_menu.price * tech_menu.orderRatio.Value / 100.0, 1);
                            }
                        }

                        string[] tech_objs = { techId, m, "点钟", (paid_count+unpaid_count).ToString(), 
                                             tech_menu_order_bonus.ToString()};
                        dgv.Rows.Add(tech_objs);

                        tech_count += paid_count + unpaid_count;
                        tech_bouns += tech_menu_order_bonus;
                    }
                }
                dgv.Rows.Add("", "", "小计", tech_count, tech_bouns);
                dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            }
            BathClass.set_dgv_fit(dgv);
        }


        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                    dgv_show();

                var dc = new BathDBDataContext(LogIn.connectionString);
                string companyName = dc.Options.FirstOrDefault().companyName;
                List<string> printColumns = new List<string>();
                foreach (DataGridViewColumn dgvCol in dgv.Columns)
                {
                    printColumns.Add(dgvCol.HeaderText);
                }

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                PrintReceipt.Print_DataGridView("技师对账单汇总", dgv, lastTime.ToString("yyyy-MM-dd HH:mm:ss"), thisTime.ToString("yyyy-MM-dd HH:mm:ss"), companyName);
            }
            catch
            {
                BathClass.printErrorMsg("打印出错，请重新打印!");
            }
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
                    toolFind_Click(null, null);
                    break;
                case Keys.F4:
                    exportTool_Click(null, null);
                    break;
                case Keys.F5:
                    printTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //查询
        private void toolFind_Click(object sender, EventArgs e)
        {
            if (!get_clear_table_time())
                return;

            dgv_show();
        }
    }
}
