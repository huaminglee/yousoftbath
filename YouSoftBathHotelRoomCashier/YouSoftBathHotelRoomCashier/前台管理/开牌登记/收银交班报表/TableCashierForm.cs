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
    public partial class TableCashierForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private string macAdd;
        private List<int> idList;
        private DateTime lastTime;
        private DateTime thisTime;

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
                ptTimeList.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var cpts = db.CashPrintTime.OrderByDescending(x=>x.time).Select(x=>x.time);
                macAdd = BathClass.getMacAddr_Local();
                foreach (var cpt in cpts)
                    ptTimeList.Items.Add(cpt.ToString("yyyy-MM-dd HH:mm:ss"));

                ptTimeList.SelectedIndex = 0;
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
                //macAdd = BathClass.getMacAddr_Local();
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
            string[] trow = { "合计", "", "", totalMoney.ToString("0") };
            dgv.Rows.Add(trow);

            totalMoney = 0;
            dgv.Rows.Add();
            var menuList = db.Menu.Select(x => x.name);
            foreach (string menu in menuList)
            {
                List<HisOrders> orderList = findOrder(menu);
                if (orderList.Count() != 0)
                {
                    var tmp_menu = db.Menu.FirstOrDefault(x => x.name == menu);
                    double money = 0;
                    double t_money = orderList.Sum(x => x.number) * tmp_menu.price;
                    string[] row = { menu, orderList.Sum(x => x.number).ToString(), t_money.ToString("0"), money.ToString("0") };
                    dgv.Rows.Add(row);
                    totalMoney += money;
                }
            }

            var comboList = db.Combo.Select(x => x.id.ToString());
            foreach (string comboId in comboList)
            {
                string menu = "套餐" + comboId + "优惠";
                List<HisOrders> orderList = findOrder(menu);
                if (orderList.Count() != 0)
                {
                    double money = Convert.ToDouble(orderList.Sum(x => x.money));
                    string[] row = { menu, orderList.Count().ToString(), money.ToString("0"), money.ToString("0") };
                    dgv.Rows.Add(row);
                    totalMoney += money;
                }
            }
            string[] trow2 = { "合计", "", "", totalMoney.ToString("0") };
            dgv.Rows.Add(trow2);
            dgv.Rows.Add();

            string[] cashRow = { "现金", "", "", "0" };
            string[] bankRow = { "银联", "", "", "0" };
            string[] cardRow = { "储值卡", "", "", "0" };
            string[] couponRow = { "优惠券", "", "", "0" };
            string[] groupBuyRow = { "团购优惠", "", "", "0" };
            string[] discountRow = { "挂账", "", "", "0" };
            string[] serverRow = { "免单", "", "", "0" };
            string[] otherRow = { "扣卡", "", "", "0" };
            string[] wipeRow = { "抹零", "", "", "0" };

            double act_money = 0;

            double tmp_Money = Convert.ToDouble(accountList.Where(x => x.cash != null).Sum(x => x.cash));
            cashRow[3] = tmp_Money.ToString();
            act_money += tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.changes != null).Sum(x => x.changes));
            cashRow[3] = (Convert.ToDouble(cashRow[3]) - tmp_Money).ToString();
            act_money -= tmp_Money;

            tmp_Money = Convert.ToDouble(accountList.Where(x => x.bankUnion != null).Sum(x => x.bankUnion));
            bankRow[3] = tmp_Money.ToString();
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
            trow[3] = act_money.ToString("0");
            dgv.Rows.Add(trow);

            BathClass.set_dgv_fit(dgv);
        }

        //查询订单
        private List<HisOrders> findOrder(string menu)
        {
            List<HisOrders> orderList = db.HisOrders.Where(x => x.menu == menu && idList.Contains(x.accountId.Value) && x.deleteEmployee == null).ToList();
            return orderList;
        }

        //查询账务
        private List<Account> findAccount()
        {
            var accountList = db.Account.Where(x => x.payTime >= lastTime && 
                x.payTime <= thisTime && x.macAddress == macAdd && x.abandon == null).ToList();
            idList = accountList.Select(x => x.id).ToList();

            return accountList;
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
                dgv_show();

                var dc = new BathDBDataContext(LogIn.connectionString);
                string companyName = dc.Options.FirstOrDefault().companyName;
                List<string> printColumns = new List<string>();
                foreach (DataGridViewColumn dgvCol in dgv.Columns)
                {
                    printColumns.Add(dgvCol.HeaderText);
                }

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (ptTimeList.SelectedIndex == 0)
                {
                    CashPrintTime cpt = new CashPrintTime();
                    cpt.macAdd = macAdd;
                    cpt.time = GeneralClass.Now;
                    dc.CashPrintTime.InsertOnSubmit(cpt);
                    dc.SubmitChanges();
                }

                PrintReceipt.Print_DataGridView(dgv, lastTime.ToString("yyyy-MM-dd HH:mm:ss"), thisTime.ToString("yyyy-MM-dd HH:mm:ss"), companyName);
                BathClass.set_dgv_fit(dgv);
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

        //夜审
        private void clearTool_Click(object sender, EventArgs e)
        {
            bool hasto = true;
            DateTime now = GeneralClass.Now;
            var cnt = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == now.Date);
            if (cnt != null)
            {
                ClearOptionsForm clearOptionsForm = new ClearOptionsForm(cnt.clearTime);
                if (clearOptionsForm.ShowDialog() == DialogResult.OK)
                {
                    db.ClearTable.DeleteOnSubmit(cnt);
                    db.SubmitChanges();
                }
                else
                    hasto = false;
            }

            if (!hasto)
                return;

            var seats = db.Seat.Where(x => x.status == 3);
            foreach (Seat seat in seats)
            {
                BathClass.reset_seat(seat);
            }

            ClearTable ct = new ClearTable();
            ct.clearTime = GeneralClass.Now;
            db.ClearTable.InsertOnSubmit(ct);
            db.SubmitChanges();
        }

        //打印时间改变
        private void ptTimeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_print_time();

            if (BathClass.getAuthority(db, LogIn.m_User, "收银报表"))
                dgv_show();
        }
    }
}
