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
    public partial class TableCashierForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<int> idList;
        private DateTime lastTime;
        private DateTime thisTime;
        private string macAdd;

        //构造函数
        public TableCashierForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CashierForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Location = new Point(0, 0);
                this.Size = new Size(this.Size.Width, Screen.GetWorkingArea(this).Height);
                cb_pcs.Items.AddRange(db.CashPrintTime.Select(x => x.macAdd).Distinct().ToArray());
                if (cb_pcs.Items.Count != 0)
                    cb_pcs.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
                this.Close();
            }
        }

        private void get_print_time()
        {
            if (ptTimeList.SelectedIndex == 0)
            {
                //macAdd = PCUtil.getMacAddr_Local();
                var printT = db.CashPrintTime.Where(x=>x.macAdd == macAdd).OrderByDescending(x => x.time).FirstOrDefault();
                //var printT = db.CashPrintTime.OrderByDescending(x => x.time).FirstOrDefault();
                lastTime = DateTime.Parse("2013-01-01 00:00:00");
                if (printT != null)
                    lastTime = printT.time;
                
                thisTime = DateTime.Now;
            }
            else
            {
                thisTime = Convert.ToDateTime(ptTimeList.Text);
                if (ptTimeList.SelectedIndex == ptTimeList.Items.Count - 1)
                    lastTime = DateTime.Parse("2013-01-01 00:00:00");
                else
                    lastTime = Convert.ToDateTime(ptTimeList.Items[ptTimeList.SelectedIndex + 1].ToString());
            }
        }

        //查询
        private void dgv_show()
        {
            dgv.Rows.Clear();
            List<Account> accountList = findAccount();
            if (!accountList.Any())
                return;

            //第一部分：账单部分
            double totalMoney = 0;
            foreach (var act in accountList)
            {
                var money = BathClass.get_account_money(act);
                string act_text = string.Join("\n", act.text.Split('|').ToArray());

                var money_str = "";
                if (act.cash != null)
                {
                    if (act.changes == null)
                        money_str += act.cash + "现";
                    else
                        money_str += act.cash - act.changes + "现";
                }
                if (act.bankUnion != null)
                    money_str = money_str == "" ? money_str + act.bankUnion + "银" : money_str + "\n" + act.bankUnion + "银";
                if (act.creditCard != null)
                    money_str = money_str == "" ? money_str + act.creditCard + "卡" : money_str + "\n" + act.creditCard + "卡";
                if (act.coupon != null)
                    money_str = money_str == "" ? money_str + act.coupon + "券" : money_str + "\n" + act.coupon + "券";
                if (act.groupBuy != null)
                    money_str = money_str == "" ? money_str + act.groupBuy + "团" : money_str + "\n" + act.groupBuy + "团";
                if (act.zero != null)
                    money_str = money_str == "" ? money_str + act.zero + "挂" : money_str + "\n" + act.zero + "挂";
                if (act.server != null)
                    money_str = money_str == "" ? money_str + act.server + "招" : money_str + "\n" + act.server + "招";
                if (act.wipeZero != null)
                    money_str = money_str == "" ? money_str + act.wipeZero + "抹" : money_str + "\n" + act.wipeZero + "抹";

                dgv.Rows.Add(act.id, act_text, act.payTime.ToString("MM-dd HH:mm"), money_str);
                totalMoney += money;
            }
            dgv.Rows.Add("合计", "", "", totalMoney);

            totalMoney = 0;
            dgv.Rows.Add();

            //第二部分：订单部分
            var orders = db.HisOrders.Where(x => idList.Contains(x.accountId.Value) && x.deleteEmployee == null);
            var menus = orders.Select(x => x.menu).Distinct();
            foreach (var menu in menus)
            {
                var menu_orders = orders.Where(x => x.menu == menu);
                var tmp_menu = db.Menu.FirstOrDefault(x => x.name == menu);

                double t_number = menu_orders.Sum(x => x.number);
                double money = BathClass.get_orders_money(accountList, menu_orders.ToList());
                double t_money = 0;
                if (tmp_menu != null)
                    t_money = t_number * tmp_menu.price;

                dgv.Rows.Add(menu, t_number, t_money, money);
                totalMoney += money;
            }

            //第三部分：售卡部分
            double card_cash = 0, card_bank = 0;
            var cardsale = db.CardSale.Where(x => (x.macAddress == macAdd || x.macAddress == null) && x.payTime >= lastTime && x.payTime <= thisTime);

            var cardsale_free = cardsale.Where(x => x.note == "赠送卡");
            if (cardsale_free.Any())
            {
                dgv.Rows.Add("赠送卡", cardsale_free.Count());
            }

            cardsale = cardsale.Where(x => x.note == null);
            //dgv.Rows.Add(MConvert<int>.ToTypeOrDefault(balance).ToString() + "会员卡", card_balance.Count(), "", card_balance_money);
            var balance_list = cardsale.Select(x => x.balance).Distinct();
            foreach (var balance in balance_list)
            {
                var card_balance = cardsale.Where(x => x.balance == balance);
                double card_balance_money = 0;
                var card_balance_cash = card_balance.Where(x => x.cash != null);
                if (card_balance_cash.Any())
                {
                    double cash = card_balance_cash.Sum(x => x.cash).Value;
                    card_balance_money += cash;
                    card_cash += cash;
                }

                var card_balance_bank = card_balance.Where(x => x.bankUnion != null);
                if (card_balance_bank.Any())
                {
                    double bank = card_balance_bank.Sum(x => x.bankUnion).Value;
                    card_balance_money += bank;
                    card_bank += bank;
                }

                totalMoney += card_balance_money;
                dgv.Rows.Add(MConvert<int>.ToTypeOrDefault(balance, 0) + "会员卡", card_balance.Count(), "", card_balance_money);
            }

            dgv.Rows.Add("合计", "", "", totalMoney);
            dgv.Rows.Add();


            //第四部分：汇总部分
            string[] cashRow = { "现金", "", "", card_cash.ToString() };
            string[] bankRow = { "银联", "", "", card_bank.ToString() };
            string[] cardRow = { "储值卡", "", "", "0" };
            string[] couponRow = { "优惠券", "", "", "0" };
            string[] groupBuyRow = { "团购优惠", "", "", "0" };
            string[] discountRow = { "挂账", "", "", "0" };
            string[] serverRow = { "免单", "", "", "0" };
            string[] otherRow = { "扣卡", "", "", "0" };
            string[] wipeRow = { "抹零", "", "", "0" };

            double act_money = card_bank+card_cash;

            double tmp_Money = Convert.ToDouble(accountList.Where(x => x.cash != null).Sum(x => x.cash));
            cashRow[3] = (Convert.ToDouble(cashRow[3]) + tmp_Money).ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.changes != null).Sum(x => x.changes));
            cashRow[3] = (Convert.ToDouble(cashRow[3]) - tmp_Money).ToString();
            act_money -= tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.bankUnion != null).Sum(x => x.bankUnion));
            bankRow[3] = (Convert.ToDouble(bankRow[3]) + tmp_Money).ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.creditCard != null).Sum(x => x.creditCard));
            cardRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.coupon != null).Sum(x => x.coupon));
            couponRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.groupBuy != null).Sum(x => x.groupBuy));
            groupBuyRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.zero != null).Sum(x => x.zero));
            discountRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.server != null).Sum(x => x.server));
            serverRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.deducted != null).Sum(x => x.deducted));
            otherRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.wipeZero != null).Sum(x => x.wipeZero));
            wipeRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            dgv.Rows.Add(cashRow);
            dgv.Rows.Add(bankRow);
            dgv.Rows.Add(cardRow);
            dgv.Rows.Add(couponRow);
            dgv.Rows.Add(groupBuyRow);
            dgv.Rows.Add(discountRow);
            dgv.Rows.Add(serverRow);
            dgv.Rows.Add(otherRow);
            dgv.Rows.Add(wipeRow);
            dgv.Rows.Add("合计", "", "", act_money);

            BathClass.set_dgv_fit(dgv);
        }

        //查询账务
        private List<Account> findAccount()
        {
            var accountList = db.Account.Where(x => x.payTime >= lastTime && 
                x.payTime <= thisTime && x.macAddress == macAdd && x.abandon == null).ToList();
            //var accountList = db.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null).ToList();

            //foreach (var act in accountList)
            //    idList.AddRange(act.systemId.Split('|'));
            idList = accountList.Select(x => x.id).ToList();

            return accountList;
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
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
                default:
                    break;
            }
        }

        //查询
        private void toolFind_Click(object sender, EventArgs e)
        {
            get_print_time();
            dgv_show();
        }

        //选择的收银电脑改变
        private void cb_pcs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_pcs.Items.Count == 0)
                return;

            ptTimeList.Items.Clear();
            ptTimeList.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            macAdd = cb_pcs.Text;
            var cpts = db.CashPrintTime.Where(x => x.macAdd == macAdd).OrderByDescending(x => x.time).Select(x => x.time);
            foreach (var cpt in cpts)
                ptTimeList.Items.Add(cpt.ToString("yyyy-MM-dd HH:mm:ss"));

            ptTimeList.SelectedIndex = 0;
        }
    }
}
