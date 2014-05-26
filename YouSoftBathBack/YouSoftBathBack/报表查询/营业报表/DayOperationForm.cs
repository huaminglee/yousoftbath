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
using System.Threading;

namespace YouSoftBathBack
{
    /// <summary>
    /// 营业报表
    /// </summary>
    public partial class DayOperationForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime lastTime;
        private DateTime thisTime;
        private List<string> seat_male;
        private List<string> seat_female;
        private Thread m_thread;

        private int FORMAT_RIGHT_SIDE = 0;
        private int FORMAT_VERTICAL_SIDE = 1;

        //构造函数
        public DayOperationForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框导入
        private void OperationForm_Load(object sender, EventArgs e)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 13);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 13);

            searchType.SelectedIndex = 0;
            startTime.Value = DateTime.Now.AddDays(-1);

            var format = db.Options.FirstOrDefault().营业报表格式;
            if (format == null)
            {
                db.Options.FirstOrDefault().营业报表格式 = FORMAT_RIGHT_SIDE;
                db.SubmitChanges();
            }

            var seattype = db.SeatType.Select(x => x.id).ToList();
            seat_male = db.Seat.Where(x => x.typeId == seattype[0]).Select(x => x.text).ToList();
            seat_female = db.Seat.Where(x => x.typeId == seattype[1]).Select(x => x.text).ToList();
        }

        //获取夜审时间
        private bool get_clear_table_time()
        {
            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == startTime.Value.AddDays(1).Date);
            if (ct == null)
            {
                GeneralClass.printErrorMsg("没有夜审，不能查询");
                return false;
            }

            thisTime = ct.clearTime;
            var lct = db.ClearTable.Where(x => x.id < ct.id).OrderByDescending(x => x.id).FirstOrDefault();
            if (lct == null)
                lastTime = DateTime.Parse("2013-01-01");
            else
                lastTime = lct.clearTime;

            return true;
        }

        //往dgv中添加列
        private void add_cols_to_dgv(DataGridView pdgv, string header)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = header;
            pdgv.Columns.Add(col);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            if (!get_clear_table_time())
                return;

            dgv.Rows.Clear();
            if (searchType.SelectedIndex == 0)
            {
                if (db.Options.FirstOrDefault().营业报表格式 == FORMAT_RIGHT_SIDE)
                {
                    dgv.Columns.Clear();
                    add_cols_to_dgv(dgv, "类别");
                    add_cols_to_dgv(dgv, "部门项目");
                    add_cols_to_dgv(dgv, "单价");
                    add_cols_to_dgv(dgv, "数量");
                    add_cols_to_dgv(dgv, "发生金额");
                    add_cols_to_dgv(dgv, "");
                    add_cols_to_dgv(dgv, "付款方式");
                    add_cols_to_dgv(dgv, "金额");

                    m_thread = new Thread(new ThreadStart(find_Day_Table_right));
                    m_thread.Start();
                }
                else
                {
                    dgv.Columns.Clear();
                    add_cols_to_dgv(dgv, "类别");
                    add_cols_to_dgv(dgv, "部门项目");
                    add_cols_to_dgv(dgv, "数量");
                    add_cols_to_dgv(dgv, "总金额");
                    add_cols_to_dgv(dgv, "实收金额");

                    m_thread = new Thread(new ThreadStart(find_Day_Table));
                    m_thread.Start();
                }
            }
            else
                find_details();
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);
        private delegate void delegate_add_row(object[] vals);
        private void add_row(object[] vals)
        {
            dgv.Rows.Add(vals);
        }

        private delegate void delegate_change_dgv_color(Color color);
        private void change_dgv_color(Color color)
        {
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = color;
        }

        private void find_Day_Table_right()
        {
            try
            {
                var accountList = db.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;
                var idLst = accountList.Select(x => x.id).ToList();

                #region 按项目类别查找结账项目
                var catgoryList = db.Catgory.Select(x => x.name);
                foreach (string catgory in catgoryList)
                {
                    bool first = true;
                    double totalMoney = 0;
                    double totalNumber = 0;
                    var menuList = db.Menu.Where(x => x.catgoryId == db.Catgory.FirstOrDefault(s => s.name == catgory).id).Select(x => x.name);
                    foreach (string menu in menuList)
                    {
                        var orderList = db.HisOrders.Where(x => x.menu == menu && idLst.Contains(x.accountId.Value) && x.deleteEmployee == null);
                        if (orderList.Any())
                        {
                            var price_list = orderList.Where(x => x.number != 0).Select(x => x.money / x.number).Distinct();
                            foreach (var price in price_list)
                            {
                                var order_price_list = orderList.Where(x => x.number != 0 && x.money / x.number == price);
                                double money = BathClass.get_orders_money(accountList.ToList(), order_price_list.ToList());
                                double number = order_price_list.Sum(x => x.number);
                                if (first)
                                {
                                    first = false;
                                    this.Invoke(new delegate_add_row(add_row),
                                        (Object)(new object[] { catgory, menu, price, number, money }));
                                }
                                else
                                {
                                    this.Invoke(new delegate_add_row(add_row),
                                        (Object)(new object[] { "", menu, price, number, money }));
                                }
                                totalMoney += money;
                                totalNumber += number;
                            }
                        }
                    }
                    if (!first)
                    {
                        this.Invoke(new delegate_add_row(add_row),
                            (Object)(new object[] { "", "", "小计", totalNumber, totalMoney }));
                        this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                    }
                }
                #endregion

                #region 查找套餐
                double cTotal = 0;
                double cNumber = 0;
                var comboList = db.Combo.Select(x => x.id.ToString());
                bool firstRow = true;
                var combo_orders = db.HisOrders.Where(x => x.menu.Contains("套餐") && idLst.Contains(x.accountId.Value));
                var combo_menus = combo_orders.Select(x => x.menu).Distinct();
                foreach (string combo_menu in combo_menus)
                {
                    var orderList = combo_orders.Where(x => x.menu == combo_menu);
                    if (orderList.Any())
                    {
                        double money = Convert.ToDouble(orderList.Sum(x => x.money));
                        double number = orderList.Count();
                        cTotal += money;
                        cNumber += number;
                        if (firstRow)
                        {
                            firstRow = false;
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "套餐", combo_menu, "", number, money }));
                        }
                        else
                        {
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "", combo_menu, "", number, money }));
                        }
                    }
                }
                if (!firstRow)
                {
                    this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cNumber, cTotal }));
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                }
                #endregion

                #region 查找售卡
                firstRow = true;
                var cardsale = db.CardSale.Where(x => x.explain == null && x.payTime >= lastTime && x.payTime <= thisTime && x.abandon==null);
                double cardsale_cash = 0;
                double cardsale_bank = 0;
                var card_cash = cardsale.Where(x => x.cash != null);
                var card_bank = cardsale.Where(x => x.bankUnion != null);
                if (card_cash.Any())
                    cardsale_cash = card_cash.Sum(x => x.cash).Value;

                if (card_bank.Any())
                    cardsale_bank = card_bank.Sum(x => x.bankUnion).Value;

                var balance_list = cardsale.Select(x => x.balance).Distinct();
                foreach (var balance in balance_list)
                {
                    var card_balance = cardsale.Where(x => x.balance == balance);
                    double card_balance_money = 0;
                    var card_balance_cash = card_balance.Where(x => x.cash != null);
                    if (card_balance_cash.Any())
                        card_balance_money += card_balance_cash.Sum(x => x.cash).Value;

                    var card_balance_bank = card_balance.Where(x => x.bankUnion != null);
                    if (card_balance_bank.Any())
                        card_balance_money += card_balance_bank.Sum(x => x.bankUnion).Value;
                    if (firstRow)
                    {
                        firstRow = false;
                        this.Invoke(new delegate_add_row(add_row),
                            (Object)(new object[] { "会员卡", MConvert<int>.ToTypeOrDefault(balance, 0).ToString() + "会员卡", "", 
                            card_balance.Count(), card_balance_money.ToString() }));
                    }
                    else
                    {
                        this.Invoke(new delegate_add_row(add_row),
                            (Object)(new object[] { "", MConvert<int>.ToTypeOrDefault(balance, 0).ToString() + "会员卡", "", 
                            card_balance.Count(), card_balance_money.ToString() }));
                    }
                }
                if (!firstRow)
                {
                    this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cardsale.Count().ToString(), 
                        (cardsale_bank+cardsale_cash).ToString() }));
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                }

                #endregion

                #region 查找充值
                firstRow = true;
                var cardpop = db.CardSale.Where(x => x.explain != null && x.payTime >= lastTime && x.payTime <= thisTime);
                double cardpop_cash = 0;
                double cardpop_bank = 0;
                var card_pop_cash = cardpop.Where(x => x.cash != null);
                var card_pop_bank = cardpop.Where(x => x.bankUnion != null);
                if (card_pop_cash.Any())
                    cardpop_cash = card_pop_cash.Sum(x => x.cash).Value;

                if (card_pop_bank.Any())
                    cardpop_bank = card_pop_bank.Sum(x => x.bankUnion).Value;

                this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "会员充值", "", "现金", card_pop_cash.Count().ToString(), 
                        cardpop_cash.ToString() }));
                this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "银联", card_pop_bank.Count().ToString(), 
                        cardpop_bank.ToString() }));
                this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cardpop.Count().ToString(), 
                        (cardpop_cash+cardpop_bank).ToString() }));
                this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                #endregion

                #region 查找其他类别
                cTotal = 0;
                cNumber = 0;
                firstRow = true;
                var other_orders = db.HisOrders.Where(x => idLst.Contains(x.accountId.Value) && x.deleteEmployee == null &&
                    !x.menu.Contains("套餐") && db.Menu.FirstOrDefault(y => y.name == x.menu) == null);
                var other_menus = other_orders.Select(x => x.menu).Distinct();
                foreach (var om in other_menus)
                {
                    var tmp_orders = other_orders.Where(x => x.menu == om);
                    if (tmp_orders.Any())
                    {
                        double money = tmp_orders.Sum(x => x.money);
                        double number = tmp_orders.Sum(x => x.number);
                        cTotal += money;
                        cNumber += number;
                        if (firstRow)
                        {
                            firstRow = false;
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "其他", om, "", number, money }));
                        }
                        else
                        {
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "", om, "", number, money }));
                        }
                    }
                }
                if (!firstRow)
                {
                    this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cNumber, cTotal }));
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                }
                #endregion

                double totalAmount = cardsale_bank + cardsale_cash + cardpop_bank + cardpop_cash;
                double actualMoney = totalAmount;
                double cash = 0;
                double bankUnion = 0;

                if (dgv.Rows.Count == 0)
                    return;

                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));

                #region 收款信息在营业报表右边
                double tmp_Money = Convert.ToDouble(accountList.Where(x => x.cash != null).Sum(x => x.cash));
                totalAmount += tmp_Money;
                actualMoney += tmp_Money;
                cash += tmp_Money;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.changes != null).Sum(x => x.changes));
                totalAmount -= tmp_Money;
                actualMoney -= tmp_Money;
                cash -= tmp_Money;

                dgv.Rows[0].Cells[6].Value = "现金";
                dgv.Rows[0].Cells[7].Value = cash;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.bankUnion != null).Sum(x => x.bankUnion));
                totalAmount += tmp_Money;
                actualMoney += tmp_Money;
                dgv.Rows[1].Cells[6].Value = "银联";
                dgv.Rows[1].Cells[7].Value = bankUnion + tmp_Money;

                int row_index = 3;
                tmp_Money = Convert.ToDouble(accountList.Where(x => x.creditCard != null).Sum(x => x.creditCard));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "储值卡";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.coupon != null).Sum(x => x.coupon));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "优惠券";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.groupBuy != null).Sum(x => x.groupBuy));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "团购优惠";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.zero != null).Sum(x => x.zero));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "挂账";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.server != null).Sum(x => x.server));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "招待";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.deducted != null).Sum(x => x.deducted));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "扣卡";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.wipeZero != null).Sum(x => x.wipeZero));
                totalAmount += tmp_Money;
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();
                dgv.Rows[row_index - 1].Cells[6].Value = "抹零";
                dgv.Rows[row_index - 1].Cells[7].Value = tmp_Money;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                row_index++;
                #endregion

                #region 售卡信息
                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "现金售卡";
                dgv.Rows[row_index - 1].Cells[7].Value = cardsale_cash;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "银联售卡";
                dgv.Rows[row_index - 1].Cells[7].Value = cardsale_bank;
                row_index++;
                #endregion

                #region 充值信息

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "现金充值";
                dgv.Rows[row_index - 1].Cells[7].Value = cardpop_cash;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "银联充值";
                dgv.Rows[row_index - 1].Cells[7].Value = cardpop_bank;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                row_index++;

                #endregion

                #region 人数 总营业额 统计信息

                var anmoMenuLst = db.Menu.Where(x => db.Catgory.FirstOrDefault(y => y.id == x.catgoryId).name == "按摩").Select(x => x.name).ToList();
                var anmoOrderList = db.HisOrders.Where(x => idLst.Contains(x.accountId.Value) && anmoMenuLst.Contains(x.menu) && x.menu != "小费" && x.deleteEmployee == null).Select(x => x.systemId);
                int totalPop = db.HisOrders.Where(x => idLst.Contains(x.accountId.Value) && x.deleteEmployee == null).Select(x => x.systemId).Distinct().Count();
                int anmoPop = anmoOrderList.Distinct().Count();

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "总人数";
                dgv.Rows[row_index - 1].Cells[7].Value = totalPop;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "按摩人数";
                dgv.Rows[row_index - 1].Cells[7].Value = anmoPop;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "比例";
                dgv.Rows[row_index - 1].Cells[7].Value = ((double)anmoPop / (double)totalPop).ToString("p");
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "总营业额";
                dgv.Rows[row_index - 1].Cells[7].Value = totalAmount;
                row_index++;

                if (dgv.Rows.Count < row_index)
                    this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                dgv.Rows[row_index - 1].Cells[6].Value = "实际收入";
                dgv.Rows[row_index - 1].Cells[7].Value = actualMoney;
                row_index++;

                #endregion

                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }
        
        //查询营业报表
        private void find_Day_Table()
        {
            try
            {
                var accountList = db.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (!accountList.Any())
                    return;
                var idLst = accountList.Select(x => x.id).ToList();

                #region 按照项目类别统计结账项目信息

                var catgoryList = db.Catgory.Select(x => x.name);
                foreach (string catgory in catgoryList)
                {
                    bool first = true;
                    double totalMoney = 0;
                    double totalNumber = 0;
                    var menuList = db.Menu.Where(x => x.catgoryId == db.Catgory.FirstOrDefault(s => s.name == catgory).id).Select(x => x.name);
                    foreach (string menu in menuList)
                    {
                        var m = db.Menu.FirstOrDefault(x => x.name == menu);
                        var orderList = db.HisOrders.Where(x => x.menu == menu && idLst.Contains(x.accountId.Value) && x.deleteEmployee == null);
                        if (orderList.Any())
                        {
                            double money = BathClass.get_orders_money(accountList.ToList(), orderList.ToList());
                            double number = orderList.Sum(x => x.number);

                            string money_str = money.ToString();
                            double s_number = 0;
                            if (menu.Contains("浴资"))
                            {
                                s_number = orderList.Where(x => x.money != 0).Sum(x => x.number);
                                money_str += "(" + s_number.ToString() + ")";
                            }
                            if (first)
                            {
                                first = false;
                                this.Invoke(new delegate_add_row(add_row),
                                    (Object)(new object[] { catgory, menu, number, m.price * number, money_str }));
                            }
                            else
                            {
                                this.Invoke(new delegate_add_row(add_row),
                                    (Object)(new object[] { "", menu, number, m.price * number, money_str }));
                            }
                            totalMoney += money;
                            totalNumber += number;

                        }
                    }
                    if (!first)
                    {
                        this.Invoke(new delegate_add_row(add_row),
                            (Object)(new object[] { "", "小计", totalNumber, "", totalMoney }));
                        this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                    }
                }

                #endregion

                #region 查找套餐

                double cTotal = 0;
                double cNumber = 0;
                var comboList = db.Combo.Select(x => x.id.ToString());
                bool firstRow = true;
                foreach (string comboId in comboList)
                {
                    string menu = "套餐" + comboId + "优惠";
                    var orderList = db.HisOrders.Where(x => x.menu == menu && idLst.Contains(x.accountId.Value));
                    if (orderList.Any())
                    {
                        double money = Convert.ToDouble(orderList.Sum(x => x.money));
                        double number = orderList.Count();
                        cTotal += money;
                        cNumber += number;
                        if (firstRow)
                        {
                            firstRow = false;
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "套餐", menu, "", number, money }));
                        }
                        else
                        {
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "", menu, "", number, money }));
                        }
                    }
                }
                if (!firstRow)
                {
                    this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "小计", cNumber, "", cTotal }));
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                }

                #endregion

                #region 查找售卡

                firstRow = true;
                var cardsale = db.CardSale.Where(x => x.payTime >= lastTime && x.payTime <= thisTime);

                double cardsale_cash = 0;
                double cardsale_bank = 0;
                var card_cash = cardsale.Where(x => x.cash != null);
                var card_bank = cardsale.Where(x => x.bankUnion != null);
                if (card_cash.Any())
                    cardsale_cash = card_cash.Sum(x => x.cash).Value;

                if (card_bank.Any())
                    cardsale_bank = card_bank.Sum(x => x.bankUnion).Value;

                var balance_list = cardsale.Select(x => x.balance);
                foreach (var balance in balance_list)
                {
                    var card_balance = cardsale.Where(x => x.balance == balance);

                    double card_balance_money = 0;
                    var card_balance_cash = card_balance.Where(x => x.cash != null);
                    if (card_balance_cash.Any())
                        card_balance_money += card_balance_cash.Sum(x => x.cash).Value;

                    var card_balance_bank = card_balance.Where(x => x.bankUnion != null);
                    if (card_balance_bank.Any())
                        card_balance_money += card_balance_bank.Sum(x => x.bankUnion).Value;

                    if (firstRow)
                    {
                        firstRow = false;
                        this.Invoke(new delegate_add_row(add_row),
                            (Object)(new object[] { "会员卡", MConvert<int>.ToTypeOrDefault(balance, 0).ToString() + "会员卡", "", 
                            card_balance.Count(), card_balance_money }));
                    }
                    else
                    {
                        this.Invoke(new delegate_add_row(add_row),
                            (Object)(new object[] { "", MConvert<int>.ToTypeOrDefault(balance, 0).ToString() + "会员卡", "", 
                            card_balance.Count(),card_balance_money }));
                    }
                }
                if (!firstRow)
                {
                    this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cardsale.Count(), cardsale_cash + cardsale_bank }));
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                }

                #endregion

                #region 查找充值
                firstRow = true;
                var cardpop = db.CardSale.Where(x => x.explain != null && x.payTime >= lastTime && x.payTime <= thisTime);
                double cardpop_cash = 0;
                double cardpop_bank = 0;
                var card_pop_cash = cardpop.Where(x => x.cash != null);
                var card_pop_bank = cardpop.Where(x => x.bankUnion != null);
                if (card_pop_cash.Any())
                    cardpop_cash = card_pop_cash.Sum(x => x.cash).Value;

                if (card_pop_bank.Any())
                    cardpop_bank = card_pop_bank.Sum(x => x.bankUnion).Value;

                this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "会员充值", "", "现金", card_pop_cash.Count().ToString(), 
                        cardpop_cash.ToString() }));
                this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "银联", card_pop_bank.Count().ToString(), 
                        cardpop_bank.ToString() }));
                this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cardpop.Count().ToString(), 
                        (cardpop_cash+cardpop_bank).ToString() }));
                this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                #endregion

                #region 查找其他类别
                cTotal = 0;
                cNumber = 0;
                firstRow = true;
                var other_orders = db.HisOrders.Where(x => idLst.Contains(x.accountId.Value) && x.deleteEmployee == null &&
                    !x.menu.Contains("套餐") && db.Menu.FirstOrDefault(y => y.name == x.menu) == null);
                var other_menus = other_orders.Select(x => x.menu).Distinct();
                foreach (var om in other_menus)
                {
                    var tmp_orders = other_orders.Where(x => x.menu == om);
                    if (tmp_orders.Any())
                    {
                        double money = tmp_orders.Sum(x => x.money);
                        double number = tmp_orders.Sum(x => x.number);
                        cTotal += money;
                        cNumber += number;
                        if (firstRow)
                        {
                            firstRow = false;
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "其他", om, "", number, money }));
                        }
                        else
                        {
                            object[] row = { "", om, "", number, money };
                            this.Invoke(new delegate_add_row(add_row),
                                (Object)(new object[] { "", om, "", number, money }));
                        }
                    }
                }
                if (!firstRow)
                {
                    this.Invoke(new delegate_add_row(add_row),
                        (Object)(new object[] { "", "", "小计", cNumber, cTotal }));
                    this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
                }

                #endregion

                double totalAmount = cardsale_bank + cardsale_cash;
                double actualMoney = totalAmount;
                double cash = 0;
                double bankUnion = 0;

                if (dgv.Rows.Count == 0)
                    return;

                #region 收银信息汇总

                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));
                //dgv.Rows.Add();

                double tmp_Money = Convert.ToDouble(accountList.Where(x => x.cash != null).Sum(x => x.cash));
                totalAmount += tmp_Money;
                actualMoney += tmp_Money;
                cash += tmp_Money;

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.changes != null).Sum(x => x.changes));
                totalAmount -= tmp_Money;
                actualMoney -= tmp_Money;
                cash -= tmp_Money;
                //dgv.Rows.Add("现金", cash);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "现金", cash }));

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.bankUnion != null).Sum(x => x.bankUnion));
                totalAmount += tmp_Money;
                actualMoney += tmp_Money;
                //dgv.Rows.Add("银联", bankUnion + tmp_Money);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "银联", bankUnion + tmp_Money }));

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.creditCard != null).Sum(x => x.creditCard));
                totalAmount += tmp_Money;
                //dgv.Rows.Add("储值卡", tmp_Money);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "储值卡", tmp_Money }));

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.coupon != null).Sum(x => x.coupon));
                totalAmount += tmp_Money;
                //dgv.Rows.Add("优惠券", tmp_Money);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "优惠券", tmp_Money }));

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.groupBuy != null).Sum(x => x.groupBuy));
                totalAmount += tmp_Money;
                //dgv.Rows.Add("团购优惠", tmp_Money);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "团购优惠", tmp_Money }));


                tmp_Money = Convert.ToDouble(accountList.Where(x => x.zero != null).Sum(x => x.zero));
                totalAmount += tmp_Money;
                //dgv.Rows.Add("挂账", tmp_Money);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "挂账", tmp_Money }));

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.server != null).Sum(x => x.server));
                totalAmount += tmp_Money;
                //dgv.Rows.Add("招待", tmp_Money);
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "招待", tmp_Money }));

                tmp_Money = Convert.ToDouble(accountList.Where(x => x.wipeZero != null).Sum(x => x.wipeZero));
                totalAmount += tmp_Money;

                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "抹零", tmp_Money }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));

                #endregion

                #region 售卡信息汇总

                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "现金售卡", cardsale_cash }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "银联售卡", cardsale_bank }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));

                #endregion

                #region 充值信息

                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "现金充值", cardpop_cash }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "银联售卡", cardpop_bank }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));

                #endregion

                #region 人数 总营业额 统计信息

                var act_orders = db.HisOrders.Where(x => idLst.Contains(x.accountId.Value) && x.deleteEmployee == null);
                var anmoMenuLst = db.Menu.Where(x => db.Catgory.FirstOrDefault(y => y.id == x.catgoryId).name == "按摩").Select(x => x.name).ToList();
                var anmoOrderList = db.HisOrders.Where(x => idLst.Contains(x.accountId.Value) && anmoMenuLst.Contains(x.menu) && x.menu != "小费" && x.deleteEmployee == null).Select(x => x.systemId);
                int male_pop = act_orders.Where(x => seat_male.Contains(x.text)).Select(x => x.systemId).Distinct().Count();
                int female_pop = act_orders.Where(x => seat_female.Contains(x.text)).Select(x => x.systemId).Distinct().Count();
                int totalPop = male_pop + female_pop;
                int anmoPop = anmoOrderList.Distinct().Count();

                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "总人数", totalPop }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "男宾人数", male_pop }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { "女宾人数", female_pop }));
                this.Invoke(new delegate_add_row(add_row), (Object)(new object[] { }));

                string[] t_row = { "总营业额", totalAmount.ToString() };
                dgv.Rows.Insert(0, t_row);
                dgv.Rows.Insert(1, 1);

                #endregion

                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        //查询销售汇总报表
        private void find_details()
        {
            double paidAmount = 0;
            double unpaidAmount = 0;

            var orders = db.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
            if (orders.Count() == 0)
                return;

            var catgoryList = db.Catgory.Select(x => x.name);
            foreach (string catgory in catgoryList)
            {
                double paidTotalMoney = 0;
                double unpaidTotalMoney = 0;
                dgv.Rows.Add(catgory + "类小计");
                dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                var menuList = db.Menu.Where(x => x.catgoryId == db.Catgory.FirstOrDefault(s => s.name == catgory).id).Select(x => x.name);
                foreach (string menu in menuList)
                {
                    var orderList = orders.Where(x => x.menu == menu);
                    if (orderList.Count() != 0)
                    {
                        var paidList = orderList.Where(x => x.paid);
                        var unpaidList = orderList.Where(x => !x.paid);
                        double paidMoney = paidList.Count() == 0 ? 0 : Convert.ToDouble(paidList.Sum(x => x.money));
                        double unpaidMoney = unpaidList.Count() == 0 ? 0 : Convert.ToDouble(unpaidList.Sum(x => x.money));
                        string[] row = {menu,
                                        db.Menu.FirstOrDefault(x=>x.name==menu).price.ToString("0.00"), 
                                        orderList.Count().ToString(), 
                                        paidMoney.ToString(),
                                        paidList.Count().ToString(),
                                        unpaidMoney.ToString(),
                                        unpaidList.Count().ToString()};
                        dgv.Rows.Add(row);
                        paidAmount += paidMoney;
                        unpaidAmount += unpaidMoney;
                        paidTotalMoney += paidMoney;
                        unpaidTotalMoney += unpaidMoney;
                    }
                }
                string[] tRow = { "", "", "小计", paidTotalMoney.ToString(), unpaidTotalMoney.ToString() };
                dgv.Rows.Add(tRow);
            }

            //查找套餐
            double cPaidTotal = 0;
            double cUnpaidTotal = 0;
            dgv.Rows.Add("套餐类类小计");
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            var comboList = db.Combo.Select(x => x.id.ToString());
            foreach (string comboId in comboList)
            {
                string menu = "套餐" + comboId + "优惠";
                var orderList = orders.Where(x => x.menu == menu);
                if (orderList.Count() != 0)
                {
                    var paidList = orderList.Where(x => x.paid);
                    var unpaidList = orderList.Where(x => !x.paid);
                    double paidMoney = paidList.Count() == 0 ? 0 : Convert.ToDouble(paidList.Sum(x => x.money));
                    double unpaidMoney = unpaidList.Count() == 0 ? 0 : Convert.ToDouble(unpaidList.Sum(x => x.money));
                    string[] row = {menu, 
                                    "", 
                                    orderList.Count().ToString(),
                                    paidMoney.ToString(),
                                    paidList.Count().ToString(),
                                    unpaidMoney.ToString(),
                                   unpaidList.Count().ToString()};
                    dgv.Rows.Add(row);
                    paidAmount += paidMoney;
                    unpaidAmount += unpaidMoney;
                    cPaidTotal += paidMoney;
                    cUnpaidTotal += unpaidMoney;
                }
            }
            string[] tcRow = { "", "", "小计", cPaidTotal.ToString(), cUnpaidTotal.ToString() };
            dgv.Rows.Add(tcRow);

            dgv.Rows.Add("营业数据汇总");
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            string[] row1 = { "已结账金额", paidAmount.ToString("0.00"), "", "未结账金额", unpaidAmount.ToString("0.00") };
            dgv.Rows.Add(row1);
            BathClass.set_dgv_fit(dgv);
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                BathClass.printErrorMsg("正在查询，请稍后！");
                return;
            }
            ExportToExcel.ExportExcel("营业报表 "+startTime.Value.ToString("yyyy-MM-dd"), dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                BathClass.printErrorMsg("正在查询，请稍后！");
                return;
            }
            PrintDGV.Print_DataGridView(dgv, "营业报表", false, "作业时间:" + startTime.Text);
        }

        //查询方式改变，清空报表
        private void searchType_TextChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (searchType.SelectedIndex == 0)
            {
                dgv.Columns[4].HeaderText = "发生金额";
                dgv.Columns[5].HeaderText = "";
                dgv.Columns[6].HeaderText = "付款方式";
                dgv.Columns[7].HeaderText = "金额";
            }
            else
            {
                dgv.Columns[4].HeaderText = "已结账金额";
                dgv.Columns[5].HeaderText = "已结账数量";
                dgv.Columns[6].HeaderText = "未结账金额";
                dgv.Columns[7].HeaderText = "未结账数量";
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
                    findTool_Click(null, null);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "营业报表", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F6:
                    formatTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //切换格式
        private void formatTool_Click(object sender, EventArgs e)
        {
            var ops = db.Options.FirstOrDefault();
            if (ops.营业报表格式 == FORMAT_RIGHT_SIDE)
                ops.营业报表格式 = FORMAT_VERTICAL_SIDE;
            else
                ops.营业报表格式 = FORMAT_RIGHT_SIDE;
            db.SubmitChanges();
            findTool_Click(null, null);
        }

        private void DayOperationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }
    }
}
