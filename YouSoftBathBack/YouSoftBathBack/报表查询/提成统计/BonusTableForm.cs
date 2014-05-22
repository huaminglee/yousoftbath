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
    public partial class BonusTableForm : Form
    {
        //成员变量
        private Thread m_thread;
        private Thread m_thread_details;
        private DateTime lastTime;//起始时间
        private DateTime thisTime;//终止时间
        private bool use_pad;//启用客房平板
        private bool input_id;//输入单据号

        private int FORMAT_ALL_NODIANLUN = 0;//已结未结 不区分点钟，轮钟
        private int FORMAT_ALL_DIANLUN = 1;//已结未结  区分点钟轮钟
        private int FORMAT_INPUTTIME_DIANLUN = 2;//纯粹按照输入时间 区分点钟轮钟
        private int FORMAT_INPUTTIME_NODIANLUN = 3;//纯粹按照输入时间 不区分点钟轮钟

        private int TABLE_FORMAT = -1;

        //构造函数
        public BonusTableForm()
        {
            InitializeComponent();
            p2.Visible = false;
            p3.Dock = DockStyle.Fill;
        }

        //对话框载入
        private void BonusTableForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            startDate.Value = DateTime.Now.AddDays(-1);
            endDate.Value = DateTime.Now.AddDays(-1);
            catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            menu.Items.AddRange(db.Menu.Where(x=>x.technician).Select(x => x.name).ToArray());

            var ops = db.Options.FirstOrDefault();
            use_pad = MConvert<bool>.ToTypeOrDefault(ops.启用客房面板, false);
            input_id = MConvert<bool>.ToTypeOrDefault(ops.录单输入单据编号, false);
            dgvDetails.Columns[1].Visible = input_id;
            dgv2.Columns[1].Visible = input_id;
            toolChoice.SelectedIndex = 0;

        }

        //提成汇总统计
        private void dgv1_show_duplicated()
        {
            try
            {
                var db = new BathDBDataContext(LogIn.connectionString);
                dgv4.Rows.Clear();
                var accountList = db.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;

                var idLst = accountList.Select(x => x.id).ToList();
                var orderLst = db.Orders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
                //orderLst = orderLst.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                orderLst = orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = db.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = db.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                    orderLst = orderLst.Where(x => x.menu == menu.Text);

                double all_money = 0; //总金额
                double all_ratio = 0; //总提成
                var techList = orderLst.Select(x => x.technician).Distinct().ToList();
                foreach (string techId in techList)
                {
                    double tech_money = 0; //技师总金额
                    double tech_ratio = 0; //技师总提成
                    var tech_orders = orderLst.Where(x => x.technician == techId);
                    var menus = tech_orders.Select(x => x.menu).Distinct();
                    foreach (var m in menus)
                    {
                        string[] row = new string[12];
                        row[0] = techId;//技师号
                        row[1] = m;//项目名称

                        var tech_menu = db.Menu.FirstOrDefault(x => x.name == m);//项目
                        string tech_ratio_type = tech_menu.techRatioType;//提成方式
                        //double on_ratio = tech_menu.onRatio;//上钟提成
                        //double order_ratio = tech_menu.orderRatio;//点钟提成

                        var ordersOn = tech_orders.Where(x => x.menu == m && (x.techType == "上钟" || x.techType == null));

                        row[4] = row[5] = "";
                        double b_o_on = 0;//上钟金额
                        double bOn = 0;//上钟提成

                        if (ordersOn.Any())
                        {
                            row[2] = ordersOn.Sum(x => x.number).ToString();//上钟数量
                            if (tech_menu.techRatioType == "按实收提成" || tech_menu.techRatioType == null)
                                b_o_on = ordersOn.Sum(x => x.money);
                            else
                                b_o_on = ordersOn.Sum(x => tech_menu.price);

                            if (m != "小费")
                                tech_money += b_o_on;
                            all_money += b_o_on;
                            row[3] = (tech_menu.price * Convert.ToDouble(row[2])).ToString();
                            row[4] = b_o_on.ToString();//上钟金额
                            if (tech_menu.onRatio != null)
                            {
                                bOn = ordersOn.Sum(x => x.money) * Convert.ToDouble(tech_menu.onRatio) / 100.0;
                                tech_ratio += bOn;
                                all_ratio += bOn;
                                row[5] = bOn.ToString();//上钟提成
                            }
                        }
                        else
                            row[2] = "0";//上钟数量

                        var orderOrder = tech_orders.Where(x => x.menu == m && x.techType == "点钟");
                        row[7] = row[8] = "";
                        double b_o_order = 0;//点钟金额
                        double bOrder = 0;//点钟提成

                        if (orderOrder.Any())
                        {
                            row[6] = orderOrder.Sum(x => x.number).ToString();//点钟数量

                            if (tech_menu.techRatioType == "按实收提成" || tech_menu.techRatioType == null)
                                b_o_order = orderOrder.Sum(x => x.money);
                            else
                                b_o_order = orderOrder.Sum(x => tech_menu.price);

                            tech_money += b_o_order;
                            all_money += b_o_order;
                            row[7] = b_o_order.ToString();//点钟金额
                            if (tech_menu.orderRatio != null)
                            {
                                bOrder = orderOrder.Sum(x => x.money) * Convert.ToDouble(tech_menu.orderRatio) / 100.0;
                                tech_ratio += bOrder;
                                all_ratio += bOrder;
                                row[8] = bOrder.ToString();//点钟提成
                            }
                        }
                        else
                            row[6] = "0";

                        row[9] = (Convert.ToDouble(row[2]) + Convert.ToDouble(row[6])).ToString();//总数量
                        row[10] = (b_o_on + b_o_order).ToString();//总金额
                        row[11] = (bOn + bOrder).ToString();//总提成
                        dgv4.Rows.Add(row);
                    }
                    string[] tech_row = new string[12];
                    tech_row[0] = "";
                    tech_row[2] = tech_orders.Where(x => x.menu != "小费").Sum(x => x.number).ToString();
                    tech_row[4] = tech_money.ToString();
                    tech_row[11] = tech_ratio.ToString();
                    dgv4.Rows.Add(tech_row);
                    dgv4.Rows[dgv4.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
                string[] all_row = new string[12];
                all_row[10] = all_money.ToString();
                all_row[11] = all_ratio.ToString();
                dgv4.Rows.Add(all_row);
                set_columns_invisible();
            }
            catch (System.Exception e)
            {
            	
            }
        }

        //服务员提成汇总统计
        private void dgvWaiter_show()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var accountList = dc.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;

                var waiter_menus = dc.Menu.Where(x => x.waiter != null && x.waiter.Value);
                var idLst = accountList.Select(x => x.id);
                var paid_orderLst = dc.HisOrders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
                var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                all_his_orders = paid_orderLst.Union(all_his_orders).Distinct();
                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

                if (cboxSeat.Checked)
                {
                    all_his_orders = all_his_orders.Where(x => x.technician == seat.Text);
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                }
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    waiter_menus = waiter_menus.Where(x => x.catgoryId == catgoryId);
                }

                var waiter_menu_names = waiter_menus.Select(x => x.name);
                all_his_orders = all_his_orders.Where(x => waiter_menu_names.Contains(x.menu));
                orderLst = orderLst.Where(x => waiter_menu_names.Contains(x.menu));

                if (cboxMenu.Checked)
                {
                    orderLst = orderLst.Where(x => x.menu == menu.Text);
                    all_his_orders = all_his_orders.Where(x => x.menu == menu.Text);
                }

                double all_paid_money = 0; //总金额
                double all_ratio = 0; //总提成
                var unpaid_waiters = orderLst.Select(x => x.inputEmployee).Distinct();
                var all_his_waiters = all_his_orders.Select(x => x.inputEmployee).Distinct();
                var waiters = unpaid_waiters.Union(all_his_waiters).Distinct();
                foreach (string waiter in waiters)
                {
                    double waiter_paid_money = 0;
                    double waiter_unpaid_money = 0;
                    double waiter_ratio = 0;
                    double waiter_paid_count = 0;
                    double waiter_unpaid_count = 0;

                    var unpaid_waiter_orders = orderLst.Where(x => x.inputEmployee == waiter);
                    var unpaid_menus = unpaid_waiter_orders.Select(x => x.menu).Distinct();

                    var all_his_waiter_orders = all_his_orders.Where(x => x.inputEmployee == waiter);
                    var all_his_menus = all_his_waiter_orders.Select(x => x.menu).Distinct();
                    var menus = unpaid_menus.Union(all_his_menus).Distinct().ToList();
                    foreach (var m in menus)
                    {
                        var unpaid_waiter_menu_orders = unpaid_waiter_orders.Where(x => x.menu == m).ToList();
                        var all_his_waiter_menu_orders = all_his_waiter_orders.Where(x => x.menu == m).ToList();
                        if (!unpaid_waiter_menu_orders.Any() && !all_his_waiter_menu_orders.Any())
                            continue;

                        double paidMoney = 0;
                        double paid_count = 0;
                        double unpaidMoney = 0;
                        double unpaid_count = 0;

                        if (all_his_waiter_menu_orders.Any())
                        {
                            double tmp_total_money = all_his_waiter_menu_orders.Sum(x => x.money);
                            double tmp_total_count = all_his_waiter_menu_orders.Sum(x => x.number);
                            var all_his_paid_waiter_menu_orders =
                                all_his_waiter_menu_orders.Where(x => x.paid && idLst.Contains(x.accountId.Value) && x.deleteEmployee == null);

                            if (all_his_paid_waiter_menu_orders.Any())
                            {
                                paid_count = all_his_paid_waiter_menu_orders.Sum(x => x.number);
                                paidMoney = all_his_paid_waiter_menu_orders.Sum(x => x.money);
                            }
                            unpaid_count = tmp_total_count - paid_count;
                            unpaidMoney = tmp_total_money - paidMoney;
                        }


                        if (unpaid_waiter_menu_orders.Any())
                        {
                            unpaid_count += unpaid_waiter_menu_orders.Sum(x => x.number);
                            unpaidMoney += unpaid_waiter_menu_orders.Sum(x => x.money);
                        }

                        var menu_obj = dc.Menu.FirstOrDefault(x => x.name == m);
                        var raito_type = MConvert<int>.ToTypeOrDefault(menu_obj.waiterRatioType, 0);
                        double waiter_menu_ratio = 0;
                        if (raito_type == 0)
                            waiter_menu_ratio = menu_obj.waiterRatio.Value * paidMoney / 100.0;
                        else if (raito_type == 1)
                            waiter_menu_ratio = menu_obj.waiterRatio.Value * paid_count;
                        double totalMoney = paidMoney + unpaidMoney;
                        string[] waiter_objs = { waiter, m, paid_count.ToString(), paidMoney.ToString(), 
                                               waiter_menu_ratio.ToString(),
                                             unpaid_count.ToString(), unpaidMoney.ToString() };
                        this.Invoke(new delegate_add_row(dgv_add_row), (Object)waiter_objs);

                        waiter_paid_count += paid_count;
                        waiter_paid_money += paidMoney;
                        waiter_ratio += waiter_menu_ratio;
                        waiter_unpaid_count += unpaid_count;
                        waiter_unpaid_money += unpaidMoney;
                    }

                    all_paid_money += waiter_paid_money;
                    all_ratio += waiter_ratio;
                    string[] objs = { "", "", waiter_paid_count.ToString(), waiter_paid_money.ToString(),
                                    waiter_ratio.ToString(),
                                    waiter_unpaid_count.ToString(), waiter_unpaid_money.ToString() };
                    this.Invoke(new delegate_add_row(dgv_add_row), (Object)objs);
                    this.Invoke(new delegate_change_dgv_color(change_dgv0_color), Color.LightSkyBlue);
                }
                string[] all_row = new string[12];
                all_row[10] = all_paid_money.ToString();
                all_row[11] = all_ratio.ToString();
                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
            }
            catch
            {
            	
            }
        }

        private void change_dgv0_color(Color color)
        {
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = color;
        }
        private void dgv_add_row(string[] vals)
        {
            dgv.Rows.Add(vals);
        }

        //提成汇总统计  不区分点钟 轮钟
        private void dgv_show_nodianlun()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var accountList = dc.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;

                var idLst = accountList.Select(x => x.id);
                var paid_orderLst = dc.HisOrders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
                var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                all_his_orders = paid_orderLst.Union(all_his_orders).Distinct();

                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                //orderLst = paid_orderLst.Union(orderLst).Distinct();

                all_his_orders = all_his_orders.Where(x => x.technician != null);
                orderLst = orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                {
                    all_his_orders = all_his_orders.Where(x => x.technician == seat.Text);
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                }
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                    all_his_orders = all_his_orders.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                {
                    orderLst = orderLst.Where(x => x.menu == menu.Text);
                    all_his_orders = all_his_orders.Where(x => x.menu == menu.Text);
                }

                double all_money = 0; //总金额
                double all_ratio = 0; //总提成
                //var orders = orderLst.AsEnumerable();
                var unpaid_techList = orderLst.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var all_his_techList = all_his_orders.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var techList = unpaid_techList.Union(all_his_techList).Distinct();
                foreach (string techId in techList)
                {
                    double tech_paid_money = 0;
                    double tech_unpaid_money = 0;
                    double tech_paid_count = 0;
                    double tech_unpaid_count = 0;

                    var unpaid_tech_orders = orderLst.Where(x => x.technician == techId);
                    var unpaid_menus = unpaid_tech_orders.Select(x => x.menu).Distinct();

                    var all_his_tech_orders = all_his_orders.Where(x => x.technician == techId);
                    var all_his_menus = all_his_tech_orders.Select(x => x.menu).Distinct();
                    var menus = unpaid_menus.Union(all_his_menus).Distinct().ToList();
                    foreach (var m in menus)
                    {
                        var unpaid_tech_menu_orders = unpaid_tech_orders.Where(x => x.menu == m).ToList();
                        var all_his_tech_menu_orders = all_his_tech_orders.Where(x => x.menu == m).ToList();
                        if (!unpaid_tech_menu_orders.Any() && !all_his_tech_menu_orders.Any())
                            continue;

                        double paidMoney = 0;
                        double paid_count = 0;
                        double unpaidMoney = 0;
                        double unpaid_count = 0;

                        if (all_his_tech_menu_orders.Any())
                        {
                            double tmp_total_money = all_his_tech_menu_orders.Sum(x => x.money);
                            double tmp_total_count = all_his_tech_menu_orders.Sum(x => x.number);
                            var all_his_paid_tech_menu_orders =
                                all_his_tech_menu_orders.Where(x => x.paid && idLst.Contains(x.accountId.Value) && x.deleteEmployee == null);

                            if (all_his_paid_tech_menu_orders.Any())
                            {
                                paid_count = all_his_paid_tech_menu_orders.Sum(x => x.number);
                                paidMoney = all_his_paid_tech_menu_orders.Sum(x => x.money);
                            }
                            unpaid_count = tmp_total_count - paid_count;
                            unpaidMoney = tmp_total_money - paidMoney;
                        }


                        if (unpaid_tech_menu_orders.Any())
                        {
                            unpaid_count += unpaid_tech_menu_orders.Sum(x => x.number);
                            unpaidMoney += unpaid_tech_menu_orders.Sum(x => x.money);
                        }

                        double totalMoney = paidMoney + unpaidMoney;
                        string[] tech_objs = { techId, m, paid_count.ToString(), paidMoney.ToString(), 
                                             unpaid_count.ToString(), unpaidMoney.ToString() };
                        this.Invoke(new delegate_add_row(add_row), (Object)tech_objs);

                        tech_paid_count += paid_count;
                        tech_paid_money += paidMoney;
                        tech_unpaid_count += unpaid_count;
                        tech_unpaid_money += unpaidMoney;
                    }

                    string[] objs = { "", "", tech_paid_count.ToString(), tech_paid_money.ToString(), 
                                    tech_unpaid_count.ToString(), tech_unpaid_money.ToString() };
                    this.Invoke(new delegate_add_row(add_row), (Object)objs);
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightSkyBlue);
                }
                string[] all_row = new string[12];
                all_row[10] = all_money.ToString();
                all_row[11] = all_ratio.ToString();
                //BathClass.set_dgv_fit(dgv4);
                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv4);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        //提成汇总统计  纯粹按照录单时间 不区分点钟 轮钟
        private void dgv_show_input_nodianlun()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

                all_his_orders = all_his_orders.Where(x => x.technician != null);
                orderLst = orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                {
                    all_his_orders = all_his_orders.Where(x => x.technician == seat.Text);
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                }
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                    all_his_orders = all_his_orders.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                {
                    orderLst = orderLst.Where(x => x.menu == menu.Text);
                    all_his_orders = all_his_orders.Where(x => x.menu == menu.Text);
                }

                double all_money = 0; //总金额
                double all_ratio = 0; //总提成
                var unpaid_techList = orderLst.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var all_his_techList = all_his_orders.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var techList = unpaid_techList.Union(all_his_techList).Distinct();
                foreach (string techId in techList)
                {
                    double tech_money = 0;
                    double tech_count = 0;

                    var unpaid_tech_orders = orderLst.Where(x => x.technician == techId);
                    var unpaid_menus = unpaid_tech_orders.Select(x => x.menu).Distinct();

                    var all_his_tech_orders = all_his_orders.Where(x => x.technician == techId);
                    var all_his_menus = all_his_tech_orders.Select(x => x.menu).Distinct();
                    var menus = unpaid_menus.Union(all_his_menus).Distinct().ToList();
                    foreach (var m in menus)
                    {
                        var unpaid_tech_menu_orders = unpaid_tech_orders.Where(x => x.menu == m).ToList();
                        var all_his_tech_menu_orders = all_his_tech_orders.Where(x => x.menu == m).ToList();
                        if (!unpaid_tech_menu_orders.Any() && !all_his_tech_menu_orders.Any())
                            continue;

                        double tech_menu_money = 0;
                        double tech_menu_count = 0;

                        if (all_his_tech_menu_orders.Any())
                        {
                            tech_menu_money = all_his_tech_menu_orders.Sum(x => x.money);
                            tech_menu_count = all_his_tech_menu_orders.Sum(x => x.number);
                        }


                        if (unpaid_tech_menu_orders.Any())
                        {
                            tech_menu_count += unpaid_tech_menu_orders.Sum(x => x.number);
                            tech_menu_money += unpaid_tech_menu_orders.Sum(x => x.money);
                        }

                        string[] tech_objs = { techId, m, tech_menu_count.ToString(), tech_menu_money.ToString()};
                        this.Invoke(new delegate_add_row(add_row), (Object)tech_objs);

                        tech_count += tech_menu_count;
                        tech_money += tech_menu_money;
                        all_money += tech_menu_money;
                    }

                    string[] objs = { "", "", tech_count.ToString(), tech_money.ToString()};
                    this.Invoke(new delegate_add_row(add_row), (Object)objs);
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightSkyBlue);
                }
                string[] all_row = new string[4];
                all_row[3] = all_money.ToString();
                //BathClass.set_dgv_fit(dgv4);
                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv4);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        //提成汇总统计，启用客房平板
        private void dgv_show_dianlun()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var accountList = dc.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;

                var idLst = accountList.Select(x => x.id);
                var paid_orderLst = dc.HisOrders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
                var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                all_his_orders = paid_orderLst.Union(all_his_orders).Distinct();

                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                //orderLst = paid_orderLst.Union(orderLst).Distinct();

                all_his_orders = all_his_orders.Where(x => x.technician != null);
                orderLst = orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                {
                    all_his_orders = all_his_orders.Where(x => x.technician == seat.Text);
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                }
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                    all_his_orders = all_his_orders.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                {
                    orderLst = orderLst.Where(x => x.menu == menu.Text);
                    all_his_orders = all_his_orders.Where(x => x.menu == menu.Text);
                }

                double all_count_paid_lun = 0;
                double all_money_paid_lun = 0; //总金额
                double all_ratio_lun = 0; //总提成

                double all_count_paid_order = 0;
                double all_money_paid_order = 0; //总金额
                double all_ratio_order = 0; //总提成

                double all_count_unpaid_lun = 0;
                double all_money_unpaid_lun = 0; //总金额

                double all_count_unpaid_order = 0;
                double all_money_unpaid_order = 0; //总金额

                //double all_ratio = 0; //总提成

                //var orders = orderLst.AsEnumerable();

                var unpaid_techList = orderLst.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var all_his_techList = all_his_orders.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var techList = unpaid_techList.Union(all_his_techList).Distinct();
                foreach (string techId in techList)
                {
                    double tech_paid_money_order = 0;
                    double tech_paid_money_lun = 0;

                    double tech_unpaid_money_order = 0;
                    double tech_unpaid_money_lun = 0;

                    double tech_paid_count_order = 0;
                    double tech_paid_count_lun = 0;

                    double tech_unpaid_count_order = 0;
                    double tech_unpaid_count_lun = 0;

                    double tech_ratio_order = 0;
                    double tech_ratio_lun = 0;

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

                        double paidMoney_order = 0;//点钟金额
                        double paidMoney_lun = 0;//轮钟金额
                        double paid_count_order = 0;//点钟数量
                        double paid_count_lun = 0;//轮钟数量

                        double unpaidMoney_order = 0;//点钟金额
                        double unpaidMoney_lun = 0;//轮钟金额
                        double unpaid_count_order = 0;//点钟数量
                        double unpaid_count_lun = 0;//轮钟数量

                        if (all_his_tech_menu_orders.Any())
                        {
                            double tmp_total_money_order = 0;
                            double tmp_total_count_order = 0;
                            double tmp_total_money = all_his_tech_menu_orders.Sum(x => x.money);
                            double tmp_total_count = all_his_tech_menu_orders.Sum(x => x.number);
                            var all_his_tech_menu_orders_order = all_his_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                            if (all_his_tech_menu_orders_order.Any())
                            {
                                tmp_total_count_order = all_his_tech_menu_orders_order.Sum(x => x.number);
                                tmp_total_money_order = all_his_tech_menu_orders_order.Sum(x => x.money);
                            }

                            var all_his_paid_tech_menu_orders =
                                all_his_tech_menu_orders.Where(x => x.paid && idLst.Contains(x.accountId.Value) && x.deleteEmployee == null);

                            if (all_his_paid_tech_menu_orders.Any())
                            {
                                var paid_order_orders = all_his_paid_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                                if (paid_order_orders.Any())
                                {
                                    paid_count_order = paid_order_orders.Sum(x => x.number);
                                    paidMoney_order = paid_order_orders.Sum(x => x.money);
                                }
                                paid_count_lun = all_his_paid_tech_menu_orders.Sum(x => x.number) - paid_count_order;
                                paidMoney_lun = all_his_paid_tech_menu_orders.Sum(x => x.money) - paidMoney_order;
                            }
                            unpaid_count_order = tmp_total_count_order - paid_count_order;
                            unpaidMoney_order = tmp_total_money_order - paidMoney_order;

                            unpaid_count_lun = tmp_total_count - tmp_total_count_order - paid_count_lun;
                            unpaidMoney_lun = tmp_total_money - tmp_total_money_order - paidMoney_lun;
                        }


                        if (unpaid_tech_menu_orders.Any())
                        {
                            double tmp_order_count = 0;
                            double tmp_order_money = 0;
                            var unpaid_order_orders = unpaid_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                            if (unpaid_order_orders.Any())
                            {
                                tmp_order_count = unpaid_order_orders.Sum(x => x.number);
                                tmp_order_money = unpaid_order_orders.Sum(x => x.money);

                                unpaid_count_order += tmp_order_count;
                                unpaidMoney_order += tmp_order_money;
                            }

                            unpaid_count_lun += unpaid_tech_menu_orders.Sum(x => x.number) - tmp_order_count;
                            unpaidMoney_lun += unpaid_tech_menu_orders.Sum(x => x.money) - tmp_order_money;
                        }

                        //double totalMoney = paidMoney + unpaidMoney;

                        double tech_menu_lun_bonus = 0;
                        double tech_menu_order_bonus = 0;
                        if (tech_menu != null)
                        {
                            if (menu_ratio_cat == "按金额")
                            {
                                tech_menu_lun_bonus = paid_count_lun * tech_menu.onRatio.Value;
                                tech_menu_order_bonus = paid_count_order * tech_menu.orderRatio.Value;
                            }
                            else if (menu_ratio_cat == "按比例")
                            {
                                if (menu_ratio_type == "按实收")
                                {
                                    tech_menu_lun_bonus = Math.Round(paidMoney_lun * tech_menu.onRatio.Value / 100.0, 1);
                                    tech_menu_order_bonus = Math.Round(paidMoney_order * tech_menu.orderRatio.Value / 100.0, 1);
                                }
                                else if (menu_ratio_type == "按原价")
                                {
                                    tech_menu_lun_bonus = Math.Round(paid_count_lun * tech_menu.price * tech_menu.onRatio.Value / 100.0, 1);
                                    tech_menu_order_bonus = Math.Round(paid_count_order * tech_menu.price * tech_menu.orderRatio.Value / 100.0, 1);
                                }
                            }
                        }

                        string[] tech_objs = { techId, m, paid_count_lun.ToString(), paidMoney_lun.ToString(), 
                                             tech_menu_lun_bonus.ToString(),
                                             paid_count_order.ToString(), paidMoney_order.ToString(), 
                                             tech_menu_order_bonus.ToString(),
                                             (tech_menu_lun_bonus+tech_menu_order_bonus).ToString(),
                                             unpaid_count_lun.ToString(), unpaidMoney_lun.ToString(),
                                         unpaid_count_order.ToString(), unpaidMoney_order.ToString() };
                        this.Invoke(new delegate_add_row(add_row), (Object)tech_objs);

                        tech_paid_count_order += paid_count_order;
                        tech_paid_count_lun += paid_count_lun;

                        tech_paid_money_order += paidMoney_order;
                        tech_paid_money_lun += paidMoney_lun;

                        tech_unpaid_count_order += unpaid_count_order;
                        tech_unpaid_count_lun += unpaid_count_lun;

                        tech_unpaid_money_order += unpaidMoney_order;
                        tech_unpaid_money_lun += unpaidMoney_lun;

                        tech_ratio_lun += tech_menu_lun_bonus;
                        tech_ratio_order += tech_menu_order_bonus;
                    }

                    string[] objs = { "", "", tech_paid_count_lun.ToString(), tech_paid_money_lun.ToString(),
                                    tech_ratio_lun.ToString(), 
                                    tech_paid_count_order.ToString(), tech_paid_money_order.ToString(),
                                    tech_ratio_order.ToString(), (tech_ratio_lun+tech_ratio_order).ToString(),
                                    tech_unpaid_count_lun.ToString(), tech_unpaid_money_lun.ToString(),
                                tech_unpaid_count_order.ToString(), tech_unpaid_money_order.ToString()};
                    this.Invoke(new delegate_add_row(add_row), (Object)objs);
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightSkyBlue);

                    all_count_paid_lun += tech_paid_count_lun;
                    all_money_paid_lun += tech_paid_money_lun;
                    all_ratio_lun += tech_ratio_lun;

                    all_count_paid_order += tech_paid_count_order;
                    all_money_paid_order += tech_paid_money_order;
                    all_ratio_order += tech_ratio_order;

                    all_count_unpaid_lun += tech_unpaid_count_lun;
                    all_money_unpaid_lun += tech_unpaid_money_lun;

                    all_count_unpaid_order += tech_unpaid_count_order;
                    all_money_unpaid_order += tech_unpaid_money_order;
                }
                string[] objs_all = { "", "", all_count_paid_lun.ToString(), all_money_paid_lun.ToString(),
                                    all_ratio_lun.ToString(), 
                                    all_count_paid_order.ToString(), all_money_paid_order.ToString(),
                                    all_ratio_order.ToString(), (all_ratio_lun+all_ratio_order).ToString(),
                                    all_count_unpaid_lun.ToString(), all_money_unpaid_lun.ToString(),
                                all_count_unpaid_order.ToString(), all_money_unpaid_order.ToString()};
                this.Invoke(new delegate_add_row(add_row), (Object)objs_all);
                this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv4);
                //BathClass.set_dgv_fit(dgv4);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        //提成汇总统计，启用客房平板
        private void dgv_show_input_dianlun()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

                all_his_orders = all_his_orders.Where(x => x.technician != null);
                orderLst = orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                {
                    all_his_orders = all_his_orders.Where(x => x.technician == seat.Text);
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                }
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                    all_his_orders = all_his_orders.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                {
                    orderLst = orderLst.Where(x => x.menu == menu.Text);
                    all_his_orders = all_his_orders.Where(x => x.menu == menu.Text);
                }

                double all_count_lun = 0;
                double all_money_lun = 0; //总金额
                double all_ratio_lun = 0; //总提成

                double all_count_dian = 0;
                double all_money_dian = 0; //总金额
                double all_ratio_dian = 0; //总提成


                var unpaid_techList = orderLst.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var all_his_techList = all_his_orders.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
                var techList = unpaid_techList.Union(all_his_techList).Distinct();
                foreach (string techId in techList)
                {
                    double tech_money_dian = 0;
                    double tech_money_lun = 0;

                    double tech_count_dian = 0;
                    double tech_count_lun = 0;

                    double tech_ratio_lun = 0;
                    double tech_ratio_dian = 0;

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

                        double tech_menu_money_dian = 0;//点钟金额
                        double tech_menu_money_lun = 0;//轮钟金额
                        double tech_menu_count_dian = 0;//点钟数量
                        double tech_menu_count_lun = 0;//轮钟数量

                        if (all_his_tech_menu_orders.Any())
                        {
                            double tmp_money_dian = 0;
                            double tmp_count_dian = 0;
                            double tmp_total_money = all_his_tech_menu_orders.Sum(x => x.money);
                            double tmp_total_count = all_his_tech_menu_orders.Sum(x => x.number);
                            var all_his_tech_menu_orders_dian = all_his_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                            if (all_his_tech_menu_orders_dian.Any())
                            {
                                tmp_count_dian = all_his_tech_menu_orders_dian.Sum(x => x.number);
                                tmp_money_dian = all_his_tech_menu_orders_dian.Sum(x => x.money);
                            }

                            tech_menu_count_dian += tmp_count_dian;
                            tech_menu_money_dian += tmp_money_dian;

                            tech_menu_count_lun += tmp_total_count - tmp_count_dian;
                            tech_menu_money_lun += tmp_total_money - tmp_money_dian;
                        }


                        if (unpaid_tech_menu_orders.Any())
                        {
                            double tmp_money_dian = 0;
                            double tmp_count_dian = 0;
                            var unpaid_order_orders = unpaid_tech_menu_orders.Where(x => x.techType != null && x.techType == "点钟");
                            if (unpaid_order_orders.Any())
                            {
                                tmp_count_dian = unpaid_order_orders.Sum(x => x.number);
                                tmp_money_dian = unpaid_order_orders.Sum(x => x.money);
                            }

                            tech_menu_count_dian += tmp_count_dian;
                            tech_menu_money_dian += tmp_money_dian;

                            tech_menu_count_lun += unpaid_tech_menu_orders.Sum(x => x.number) - tmp_count_dian;
                            tech_menu_money_lun += unpaid_tech_menu_orders.Sum(x => x.money) - tmp_money_dian;
                        }

                        double tech_menu_lun_bonus = 0;
                        double tech_menu_dian_bonus = 0;
                        if (tech_menu != null)
                        {
                            if (menu_ratio_cat == "按金额")
                            {
                                tech_menu_lun_bonus = tech_menu_count_lun * tech_menu.onRatio.Value;
                                tech_menu_dian_bonus = tech_menu_count_dian * tech_menu.orderRatio.Value;
                            }
                            else if (menu_ratio_cat == "按比例")
                            {
                                if (menu_ratio_type == "按实收")
                                {
                                    tech_menu_lun_bonus = Math.Round(tech_menu_money_lun * tech_menu.onRatio.Value / 100.0, 1);
                                    tech_menu_dian_bonus = Math.Round(tech_menu_money_dian * tech_menu.orderRatio.Value / 100.0, 1);
                                }
                                else if (menu_ratio_type == "按原价")
                                {
                                    tech_menu_lun_bonus = Math.Round(tech_menu_count_lun * tech_menu.price * tech_menu.onRatio.Value / 100.0, 1);
                                    tech_menu_dian_bonus = Math.Round(tech_menu_count_dian * tech_menu.price * tech_menu.orderRatio.Value / 100.0, 1);
                                }
                            }
                        }

                        string[] tech_objs = { techId, m, tech_menu_count_lun.ToString(), tech_menu_money_lun.ToString(), 
                                             tech_menu_lun_bonus.ToString(),
                                             tech_menu_count_dian.ToString(), tech_menu_money_dian.ToString(), 
                                             tech_menu_dian_bonus.ToString(),
                                             (tech_menu_lun_bonus+tech_menu_dian_bonus).ToString()};
                        this.Invoke(new delegate_add_row(add_row), (Object)tech_objs);

                        tech_count_dian += tech_menu_count_dian;
                        tech_count_lun += tech_menu_count_lun;
                        tech_money_dian += tech_menu_money_dian;
                        tech_money_lun += tech_menu_money_lun;
                        tech_ratio_dian += tech_menu_dian_bonus;
                        tech_ratio_lun += tech_menu_lun_bonus;
                    }

                    string[] objs = { "", "", tech_count_lun.ToString(), tech_money_lun.ToString(),
                                    tech_ratio_lun.ToString(), 
                                    tech_count_dian.ToString(), tech_money_dian.ToString(),
                                    tech_ratio_dian.ToString(), (tech_ratio_lun+tech_ratio_dian).ToString()};
                    this.Invoke(new delegate_add_row(add_row), (Object)objs);
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightSkyBlue);

                    all_count_lun += tech_count_lun;
                    all_money_lun += tech_money_lun;
                    all_ratio_lun += tech_ratio_lun;

                    all_count_dian += tech_count_dian;
                    all_money_dian += tech_money_dian;
                    all_ratio_dian += tech_ratio_dian;
                }
                string[] objs_all = { "", "", all_count_lun.ToString(), all_money_lun.ToString(),
                                    all_ratio_lun.ToString(), 
                                    all_count_dian.ToString(), all_money_dian.ToString(),
                                    all_ratio_dian.ToString(), (all_ratio_lun+all_ratio_dian).ToString()};
                this.Invoke(new delegate_add_row(add_row), (Object)objs_all);
                this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv4);
                //BathClass.set_dgv_fit(dgv4);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);
        private delegate void delegate_no_para();
        private delegate void delegate_change_dgv_color(Color color);
        private void change_dgv_color(Color color)
        {
            dgv4.Rows[dgv4.Rows.Count - 1].DefaultCellStyle.BackColor = color;
        }

        private delegate void delegate_add_row(string[] vals);
        private void add_row(string[] vals)
        {
            dgv4.Rows.Add(vals);
        }

        private delegate void delegate_add_row_details(object[] vals);
        private void add_row_details(object[] vals)
        {
            dgvDetails.Rows.Add(vals);
        }

        private void add_row_dgv2(object[] vals)
        {
            dgv2.Rows.Add(vals);
        }

        private void set_invisible()
        {
            for (int i = 6; i < 12; i++)
                dgv4.Columns[i].Visible = false;
        }

        //提成明细查询
        private void dgv2_show()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var accountList = dc.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;

                var idLst = accountList.Select(x => x.id);
                var paid_orderLst = dc.HisOrders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                //orderLst = paid_orderLst.Union(orderLst).Distinct();
                orderLst = orderLst.Where(x => x.technician != null);
                paid_orderLst = paid_orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                {
                    paid_orderLst = paid_orderLst.Where(x => x.technician == seat.Text);
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                }
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);

                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                    paid_orderLst = paid_orderLst.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                {
                    paid_orderLst = paid_orderLst.Where(x => x.menu == menu.Text);
                    orderLst = orderLst.Where(x => x.menu == menu.Text);
                }

                foreach (var x in orderLst)
                {
                    double price = 0;
                    var order_menu = dc.Menu.FirstOrDefault(y => y.name == x.menu);
                    if (order_menu != null)
                        price = order_menu.price;
                    var act = dc.Account.FirstOrDefault(y => y.id == x.accountId && y.abandon == null);
                    string payId = "";
                    if (act != null)
                        payId = act.payEmployee;
                    object[] row = {x.paid, x.billId,x.systemId, x.menu, x.text, price, x.number, x.money, x.technician, x.techType,
                               x.inputTime, x.inputEmployee, payId};
                    this.Invoke(new delegate_add_row_details(add_row_dgv2), (Object)row);
                }

                foreach (var x in paid_orderLst)
                {
                    double price = 0;
                    var order_menu = dc.Menu.FirstOrDefault(y => y.name == x.menu);
                    if (order_menu != null)
                        price = order_menu.price;
                    var act = dc.Account.FirstOrDefault(y => y.id == x.accountId && y.abandon == null);
                    string payId = "";
                    if (act != null)
                        payId = act.payEmployee;
                    object[] row = {x.paid, x.billId,x.systemId, x.menu, x.text, price, x.number, x.money, x.technician, x.techType,
                               x.inputTime, x.inputEmployee, payId};
                    this.Invoke(new delegate_add_row_details(add_row_dgv2), (Object)row);
                }
                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv2);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        private delegate void delegate_bind_dgv(IQueryable<Orders> val, BathDBDataContext dc);

        //提成明细查询
        private void dgv2_show_duplicated()
        {
            try
            {
                var db = new BathDBDataContext(LogIn.connectionString);
                dgv4.Rows.Clear();
                var orderLst = db.Orders.Where(x => x.paid && x.deleteEmployee == null);
                orderLst = orderLst.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime);
                orderLst = orderLst.Where(x => x.technician != null);

                if (cboxSeat.Checked)
                    orderLst = orderLst.Where(x => x.technician == seat.Text);
                if (cBoxCatgory.Checked && catgory.Text != "")
                {
                    var catgoryId = db.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                    var menus = db.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                    orderLst = orderLst.Where(x => menus.Contains(x.menu));
                }
                if (cboxMenu.Checked)
                    orderLst = orderLst.Where(x => x.menu == menu.Text);

                dgv2.DataSource = from x in orderLst
                                  orderby x.inputTime
                                  select new
                                  {
                                      系统账号 = x.systemId,
                                      项目名称 = x.menu,
                                      手牌号 = x.text,
                                      单价 = db.Menu.FirstOrDefault(y => y.name == x.menu).price,
                                      数量 = x.number,
                                      金额 = x.money,
                                      技师工号 = x.technician,
                                      服务类型 = x.techType,
                                      录入时间 = x.inputTime,
                                      录入工号 = x.inputEmployee,
                                      收银工号 = db.Account.FirstOrDefault(y => y.systemId == x.systemId && y.abandon == null).payEmployee
                                  };
                BathClass.set_dgv_fit(dgv2);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        //显示信息
        private void dgv_show()
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            if (!get_clear_table_time())
                return;

            var db = new BathDBDataContext(LogIn.connectionString);
            var format = db.Options.FirstOrDefault().提成报表格式;
            if (format == null)
            {
                format = FORMAT_ALL_DIANLUN;
                db.SubmitChanges();
            }

            TABLE_FORMAT = format.Value;
            if (toolChoice.SelectedIndex == 0)
            {
                if (TABLE_FORMAT == FORMAT_ALL_DIANLUN)
                {
                    dgv4.Rows.Clear();
                    dgv4.Columns.Clear();
                    BathClass.add_cols_to_dgv(dgv4, "技师号");
                    BathClass.add_cols_to_dgv(dgv4, "项目名称");

                    BathClass.add_cols_to_dgv(dgv4, "已结轮钟数量");
                    BathClass.add_cols_to_dgv(dgv4, "已结轮钟金额");
                    BathClass.add_cols_to_dgv(dgv4, "轮钟提成");

                    BathClass.add_cols_to_dgv(dgv4, "已结点钟数量");
                    BathClass.add_cols_to_dgv(dgv4, "已结点钟金额");
                    BathClass.add_cols_to_dgv(dgv4, "点钟提成");
                    BathClass.add_cols_to_dgv(dgv4, "总提成");

                    BathClass.add_cols_to_dgv(dgv4, "未结轮钟数量");
                    BathClass.add_cols_to_dgv(dgv4, "未结轮钟金额");

                    BathClass.add_cols_to_dgv(dgv4, "未结点钟数量");
                    BathClass.add_cols_to_dgv(dgv4, "未结点钟金额");

                    m_thread = new Thread(new ThreadStart(dgv_show_dianlun));
                    m_thread.Start();
                }
                else if (TABLE_FORMAT == FORMAT_ALL_NODIANLUN)
                {
                    dgv4.Rows.Clear();
                    dgv4.Columns.Clear();
                    BathClass.add_cols_to_dgv(dgv4, "技师号");
                    BathClass.add_cols_to_dgv(dgv4, "项目名称");

                    BathClass.add_cols_to_dgv(dgv4, "已结账数量");
                    BathClass.add_cols_to_dgv(dgv4, "已结账金额");
                    BathClass.add_cols_to_dgv(dgv4, "未结账数量");
                    BathClass.add_cols_to_dgv(dgv4, "未结账金额");

                    m_thread = new Thread(new ThreadStart(dgv_show_nodianlun));
                    m_thread.Start();
                }
                else if (TABLE_FORMAT == FORMAT_INPUTTIME_NODIANLUN)
                {
                    dgv4.Rows.Clear();
                    dgv4.Columns.Clear();
                    BathClass.add_cols_to_dgv(dgv4, "技师号");
                    BathClass.add_cols_to_dgv(dgv4, "项目名称");

                    BathClass.add_cols_to_dgv(dgv4, "数量");
                    BathClass.add_cols_to_dgv(dgv4, "金额");

                    m_thread = new Thread(new ThreadStart(dgv_show_input_nodianlun));
                    m_thread.Start();
                }

                else if (TABLE_FORMAT == FORMAT_INPUTTIME_DIANLUN)
                {
                    dgv4.Rows.Clear();
                    dgv4.Columns.Clear();
                    BathClass.add_cols_to_dgv(dgv4, "技师号");
                    BathClass.add_cols_to_dgv(dgv4, "项目名称");

                    BathClass.add_cols_to_dgv(dgv4, "轮钟数量");
                    BathClass.add_cols_to_dgv(dgv4, "轮钟金额");
                    BathClass.add_cols_to_dgv(dgv4, "轮钟提成");

                    BathClass.add_cols_to_dgv(dgv4, "点钟数量");
                    BathClass.add_cols_to_dgv(dgv4, "点钟金额");
                    BathClass.add_cols_to_dgv(dgv4, "点钟提成");

                    BathClass.add_cols_to_dgv(dgv4, "总提成");

                    m_thread = new Thread(new ThreadStart(dgv_show_input_dianlun));
                    m_thread.Start();
                }
                
                //dgv1_show();
            }
            else if (toolChoice.SelectedIndex == 1)
            {
                dgv2.Rows.Clear();
                m_thread = new Thread(new ThreadStart(dgv2_show));
                m_thread.Start();
            }
            else if (toolChoice.SelectedIndex == 2)
            {
                dgv.Rows.Clear();
                m_thread = new Thread(new ThreadStart(dgvWaiter_show));
                m_thread.Start();
            }
        }

        //获取夜审时间
        private bool get_clear_table_time()
        {
            try
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

                var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == endDate.Value.AddDays(1).Date);
                if (ct == null)
                {
                    GeneralClass.printErrorMsg("没有夜审，不能查询");
                    return false;
                }

                thisTime = ct.clearTime;

                return true;
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                return false;
            }
        }

        //设置列可见
        private void set_columns_invisible()
        {
            dgv4.Columns[5].Visible = false;
            dgv4.Columns[6].Visible = false;
            dgv4.Columns[7].Visible = false;
            dgv4.Columns[8].Visible = false;
            dgv4.Columns[9].Visible = false;
            dgv4.Columns[10].Visible = false;
            dgv4.Columns[11].Visible = false;
        }

        //显示详细订单信息
        private void dgvDetails_show()
        {
            try
            {
                if (dgv.CurrentCell == null)
                    return;

                string waiter = dgv.CurrentRow.Cells[0].Value.ToString();
                string menu_name = dgv.CurrentRow.Cells[1].Value.ToString();
                if (waiter == "" || menu_name == "")
                    return;

                var dc = new BathDBDataContext(LogIn.connectionString);
                var accountList = dc.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;

                var idLst = accountList.Select(x => x.id);
                var paid_orderLst = dc.HisOrders.Where(x => x.paid && x.deleteEmployee == null && idLst.Contains(x.accountId.Value));
                var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                all_his_orders = paid_orderLst.Union(all_his_orders).Distinct();

                var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
                orderLst = orderLst.Where(x => x.inputEmployee == waiter && x.menu == menu_name);
                all_his_orders = all_his_orders.Where(x => x.inputEmployee == waiter && x.menu == menu_name);

                foreach (var o in orderLst)
                {
                    var paid = o.accountId != null && idLst.Contains(o.accountId.Value);
                    var paidTime = o.accountId == null ? "" : dc.Account.FirstOrDefault(y => y.id == o.accountId.Value).payTime.ToString();
                    object[] row = { o.id, o.billId, paid, o.menu, o.text, o.systemId, o.number, o.money, o.inputTime, 
                                   o.inputEmployee, paidTime, o.accountId };

                    this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
                }

                foreach (var o in all_his_orders)
                {
                    var paid = o.accountId != null && idLst.Contains(o.accountId.Value);
                    var paidTime = o.accountId == null ? "" : dc.Account.FirstOrDefault(y => y.id == o.accountId.Value).payTime.ToString();
                    object[] row = { o.id, o.billId, paid, o.menu, o.text, o.systemId, o.number, o.money, o.inputTime, 
                                   o.inputEmployee, paidTime, o.accountId };
                    this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
                }
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
        }

        //服务员
        private void cboxSeat_CheckedChanged(object sender, EventArgs e)
        {
            seat.ReadOnly = !cboxSeat.Checked;
        }

        //项目类别
        private void cBoxCatgory_CheckedChanged(object sender, EventArgs e)
        {
            catgory.Enabled = cBoxCatgory.Checked;
        }

        //项目编码
        private void cboxMenu_CheckedChanged(object sender, EventArgs e)
        {
            menu.Enabled = cboxMenu.Checked;
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                BathClass.printErrorMsg("请稍等，正在查询!");
                return;
            }

            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            if (toolChoice.SelectedIndex == 0)
                ExportToExcel.ExportExcel("技师对账单 " + startDate.Value.ToString("yyyy-MM-dd"), dgv4);

            if (toolChoice.SelectedIndex == 2)
                ExportToExcel.ExportExcel("员工对账单 " + startDate.Value.ToString("yyyy-MM-dd"), dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                BathClass.printErrorMsg("请稍等，正在查询!");
                return;
            }

            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            if (toolChoice.SelectedIndex == 0)
                PrintDGV.Print_DataGridView(dgv4, "技师提成", false, "作业时间:" + startDate.Text);

            if (toolChoice.SelectedIndex == 2)
                PrintDGV.Print_DataGridView(dgv, "员工提成", false, "作业时间:" + startDate.Text);
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
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
                    PrintDGV.Print_DataGridView(dgv4, "提成统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv4);
                    break;
                default:
                    break;
            }
        }

        private void toolChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolChoice.SelectedIndex == 0)
            {
                p1.Visible = false;
                p2.Visible = false;
                p3.Visible = true;
                p3.Dock = DockStyle.Fill;
            }
            else if (toolChoice.SelectedIndex == 1)
            {
                p1.Visible = false;
                p3.Visible = false;
                p2.Visible = true;
                p2.Dock = DockStyle.Fill;
            }
            else if (toolChoice.SelectedIndex == 2)
            {
                p1.Visible = true;
                p1.Dock = DockStyle.Fill;
                p3.Visible = false;
                p2.Visible = false;
            }
            if (!get_clear_table_time())
                return;
            dgv_show();
        }

        private void seat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetails.Rows.Clear();
            if (dgv.CurrentCell == null || dgv.CurrentRow.Cells[0].Value.ToString() == "")
                return;

            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            m_thread_details = new Thread(new ThreadStart(dgvDetails_show));
            m_thread_details.Start();
        }

        private void BonusTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }

        //双击显示详细信息
        private void dgv4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv4.CurrentCell == null)
                return;

            string tech_id = dgv4.CurrentRow.Cells[0].Value.ToString();
            string menu = dgv4.CurrentRow.Cells[1].Value.ToString();
            TechMenuDetailsForm form = new TechMenuDetailsForm(tech_id, menu, lastTime, thisTime, input_id, TABLE_FORMAT);
            form.ShowDialog();
        }

        //切换格式
        private void formatTool_Click(object sender, EventArgs e)
        {
            var form = new BounsTableSetForm();
            form.ShowDialog();

            dgv_show();
        }

    }
}
