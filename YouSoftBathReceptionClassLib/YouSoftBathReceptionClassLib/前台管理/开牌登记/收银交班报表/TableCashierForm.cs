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
using System.Data.SqlClient;
using System.IO;
using System.Threading;
using YouSoftUtil;
using YouSoftBathConstants;

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
                this.Size = new Size(this.Size.Width, Screen.GetWorkingArea(this).Height);
                this.Location = new Point(0, 0);
                ptTimeList.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                macAdd = PCUtil.getMacAddr_Local();
                var cpts = db.CashPrintTime.Where(x => x.macAdd == macAdd).OrderByDescending(x => x.time).Select(x => x.time);
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

            #region 账单部分

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

            #endregion

            totalMoney = 0;
            dgv.Rows.Add();

            #region 订单部分

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

            #endregion

            #region 第三部分：售卡部分

            double card_cash = 0, card_bank = 0;
            var cardsale = db.CardSale.Where(x => (x.macAddress == macAdd || x.macAddress == null) &&
                x.explain == null && x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);

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

            #endregion

            #region 第三部分：充值部分

            var cardsale_pop = db.CardSale.Where(x => (x.macAddress == macAdd || x.macAddress == null) &&
                x.explain == "会员充值" && x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);

            double card_cash_pop = 0;
            double card_bank_pop = 0;
            double card_server_pop = 0;
            if (cardsale_pop.Any())
            {
                var cardsale_pop_cash = cardsale_pop.Where(x => x.cash != null);
                if (cardsale_pop_cash.Any())
                    card_cash_pop = cardsale_pop_cash.Sum(x => x.cash).Value;

                var cardsale_pop_bank = cardsale_pop.Where(x => x.bankUnion != null);
                if (cardsale_pop_bank.Any())
                    card_bank_pop = cardsale_pop_bank.Sum(x => x.bankUnion).Value;

                var cardsale_pop_server = cardsale_pop.Where(x => x.server != null);
                if (cardsale_pop_server.Any())
                    card_server_pop = cardsale_pop_server.Sum(x => x.server).Value;
            }

            totalMoney += card_cash_pop + card_server_pop + card_bank_pop;
            card_cash += card_cash_pop;
            card_bank += card_bank_pop;
            dgv.Rows.Add("合计", "", "", totalMoney);
            dgv.Rows.Add();

            #endregion

            #region 汇总部分

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
            
            #endregion

            BathClass.set_dgv_fit(dgv);
        }

        //查询订单
        //private List<HisOrders> findOrder(string menu)
        //{
        //    List<HisOrders> orderList = 
        //    return orderList;
        //}

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

                if (ptTimeList.SelectedIndex == 0)
                {
                    CashPrintTime cpt = new CashPrintTime();
                    cpt.macAdd = macAdd;
                    cpt.time = GeneralClass.Now;
                    dc.CashPrintTime.InsertOnSubmit(cpt);
                    dc.SubmitChanges();
                }

                PrintReceipt.Print_DataGridView("收银交班", dgv, lastTime.ToString("yyyy-MM-dd HH:mm:ss"), thisTime.ToString("yyyy-MM-dd HH:mm:ss"), companyName);
                //BathClass.set_dgv_fit(dgv);
                dgv.Rows.Clear();
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
                case Keys.F6:   //绑定夜审快捷键
                    clearTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }


        private void send_sms_thread()
        {
            try
            {
                var phones = IOUtil.read_file(ConfigKeys.KEY_PHONES_FILE);
                if (phones.Count == 0)
                    return;

                smsPort = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
                smsBaud = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
                if (smsPort == "" || smsBaud == "")
                {
                    SMmsForm smsForm = new SMmsForm();
                    if (smsForm.ShowDialog() != DialogResult.OK)
                        return;

                    smsPort = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
                    smsBaud = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
                }

                if (smsPort == "" || smsBaud == "")
                    return;

                var dao = new DAO(LogIn.connectionString);
                var dts = dao.get_last_index_clear_time(2);
                DateTime st = DateTime.Parse("2013-01-01 00:00:00");
                DateTime et = dts[0].Value;
                if (dts.Count == 2)
                    st = dts[1].Value;

                string sms_msg = dao.get_sms_msg(st, et);
                send_SMS(phones,sms_msg);
            }
            catch
            {
            	
            }
        }

        //夜审并且备份数据库,并且发送夜审短信
        private void clearTool_Click(object sender, EventArgs e)
        {
            bool hasCleared = false;
            DateTime now = GeneralClass.Now;
            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == now.Date);
            if (ct != null)
            {
                hasCleared = true;
                ClearOptionsForm clearOptionsForm = new ClearOptionsForm(ct.clearTime);
                if (clearOptionsForm.ShowDialog() != DialogResult.OK)
                    return;
            }

            var seats = db.Seat.Where(x => x.status == 3);
            foreach (Seat seat in seats)
            {
                BathClass.reset_seat(seat);
            }

            if (!hasCleared)
                ct = new ClearTable();
            ct.clearTime = GeneralClass.Now;

            if (!hasCleared)
                db.ClearTable.InsertOnSubmit(ct);
            db.SubmitChanges();

            Thread td = new Thread(new ThreadStart(send_sms_thread));
            td.Start();

            backUp();
        }

        private string smsPort;
        private string smsBaud;

        private void send_SMS(List<string> phones, string msg)
        {
            String TypeStr = "";
            String CopyRightToCOM = "";
            String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";

            if (SmsClass.Sms_Connection(CopyRightStr, uint.Parse(smsPort[3].ToString()), uint.Parse(smsBaud), out TypeStr, out CopyRightToCOM) != 1)
            {
                BathClass.printErrorMsg("短信猫连接失败，请重试!");
                return;
            }

            string fileName = "短信发送-" + DateTime.Now.ToString("yyMMddHHmm") + ".txt";
            foreach (var phone in phones)
            {
                if (phone.Length != 11) continue;
                if (SmsClass.Sms_Send(phone, msg) == 0)
                {
                    BathClass.printErrorMsg("电话：" + phone + "短信发送失败!");
                }
            }

            IOUtil.insert_file("一共发送：" + phones.Count.ToString() + "条短信", fileName);
            SmsClass.Sms_Disconnection();
        }

        //打印时间改变
        private void ptTimeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_print_time();

            if (BathClass.getAuthority(db, LogIn.m_User, "收银报表"))
                dgv_show();
        }

        private void backUp()
        {
            SqlConnection con = new SqlConnection(LogIn.connectionString);

            try
            {
                string filename = @"D:\BathDB_" + DateTime.Now.ToString("yyy-MM-dd") + ".bak";
                string m_cmd_backup = @"backup database BathDB to disk= '" + filename + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(m_cmd_backup, con);
                cmd.ExecuteNonQuery();
                delete_history_files();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void delete_history_files()
        {
            string fileName =  @"D:\BathDB_";
            string file_yesterday = @"D:\BathDB_" + DateTime.Now.AddDays(-1).ToLongDateString() + ".bak";
            string file_today = @"D:\BathDB_" + DateTime.Now.ToLongDateString() + ".bak";

            var dirInfo = new DirectoryInfo(@"D:\");
            if (dirInfo == null)
                return;

            var files = dirInfo.GetFileSystemInfos();
            for (int i = 0; i < files.Length; i++ )
            {
                var file = files[i] as FileInfo;
                if (file == null || !file.FullName.Contains(fileName) || file.FullName.Contains(file_today) || file.FullName.Contains(file_yesterday))
                    continue;

                File.Delete(file.FullName);
            }
        }

        private void ToolSmsSet_Click(object sender, EventArgs e)
        {
            SMmsForm smsForm = new SMmsForm();
            smsForm.ShowDialog();
        }

        //发送号码薄
        private void ToolPhoneBook_Click(object sender, EventArgs e)
        {
            var form = new PhoneBookForm();
            form.ShowDialog();
        }
    }
}
