using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using YouSoftBathFormClass;
using System.Net;
using System.Net.Mail;

using YouSoftBathGeneralClass;

namespace YouSoftBathReception
{
    public partial class PayForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<HotelRoom> m_Seats = new List<HotelRoom>();
        private List<HotelRoom> m_Seats_Real = new List<HotelRoom>();
        private List<bool> m_keeps;
        private string customerId = "";
        private Dictionary<string, double> m_Member_List = null;
        private CardInfo m_promotion_Member = null;
        private double m_discount_money = 0;
        private bool watch = false;
        private double money_real = -1;
        private double m_money = -1;
        private Account account = new Account();
        private string signature;

        private int m_wipe_limit = 0;

        //构造函数
        public PayForm(List<HotelRoom> seats, List<bool> keeps, CardInfo member, double discount_money)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_wipe_limit = BathClass.ToInt(db.Options.FirstOrDefault().抹零限制);
            m_Seats.AddRange(db.HotelRoom.Where(x => seats.Contains(x)));
            m_keeps = new List<bool>(keeps);
            m_promotion_Member = member;
            m_discount_money = discount_money;
            InitializeComponent();
        }

        //对话框载入
        private void PayForm_Load(object sender, EventArgs e)
        {
            BathClass.change_input_en();
            watch = BathClass.ToBool(db.Options.FirstOrDefault().启用结账监控);
            m_money = BathClass.get_rooms_expenses(db, m_Seats, LogIn.connectionString);
            moneyPayable.Text = m_money.ToString();
            changes.Text = (-m_money).ToString();

            for (int i = 0; i < m_Seats.Count; i++ )
            {
                if (m_keeps[i]) continue;
                m_Seats_Real.Add(m_Seats[i]);
            }
            //money_real = BathClass.get_seats_expenses(m_Seats_Real, LogIn.connectionString);
        }

        //混合结账
        private void btnOk_Click(object sender, EventArgs e)
        {
            //1，判断是否需要刷会员卡，输入挂账单位，扣卡
            //2，更新订单信息,修改订单paid属性
            //3，插入账单数据库，更新
            //4，更新台位信息
            //5，更新客房信息
            if (!btnOk.Enabled)
                return;

            btnOk.Enabled = false;
            bool allNull = true;
            foreach (Control c in panel2.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    if (c.Text != "")
                    {
                        allNull = false;
                        break;
                    }
                }
            }
            if (allNull)
            {
                cash.Text = m_money.ToString();
                changes.Text = "0";
            }

            if (m_wipe_limit > 0 && wipeZero.Text != "" && Convert.ToDouble(wipeZero.Text) >= m_wipe_limit)
            {
                wipeZero.SelectAll();
                wipeZero.Focus();
                BathClass.printErrorMsg("抹零必须小于" + m_wipe_limit.ToString() + "块!");
                return;
            }

            if (Convert.ToDouble(changes.Text) < 0)
            {
                BathClass.printErrorMsg("结账金额不足");
                return;
            }

            try
            {
                if (!memberCardNeed())
                    return;

                //Account account = new Account();
                insert_account();
                db.Account.InsertOnSubmit(account);
                db.SubmitChanges();

                set_order_paid();
                update_seat_room();
                insert_member_infor();
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                handle_exception(ex);
            }
        }

        //更新手牌，客房
        private void update_seat_room()
        {
            int count = m_Seats.Count;
            for (int i = 0; i < m_Seats.Count; i++)
            {
                if (m_keeps[i]) continue;
                m_Seats[i].status = 3;
            }
            //foreach (HotelRoom seat in m_Seats)
            //{
            //    seat.status = 3;
            //}
        }

        //修改订单数据库的paid属性
        private void set_order_paid()
        {
            var ids = m_Seats.Select(x => x.systemId);
            var orderList = db.Orders.Where(x => ids.Contains(x.systemId) && !x.paid);
            foreach (Orders order in orderList)
            {
                var ho = new HisOrders();
                ho.menu = order.menu;
                ho.text = order.text;
                ho.systemId = order.systemId;
                ho.number = order.number;
                ho.priceType = order.priceType;
                ho.money = order.money;
                ho.technician = order.technician;
                ho.techType = order.techType;
                ho.inputTime = order.inputTime;
                ho.inputEmployee = order.inputEmployee;
                ho.deleteEmployee = order.deleteEmployee;
                ho.donorEmployee = order.donorEmployee;
                ho.comboId = order.comboId;
                ho.paid = true;
                ho.accountId = account.id;
                ho.billId = order.billId;
                ho.departmentId = 1;
                db.HisOrders.InsertOnSubmit(ho);
                db.Orders.DeleteOnSubmit(order);
            }
        }

        //判断是否需要会员卡
        private bool memberCardNeed()
        {
            if (creditCard.Text != "")
            {
                CardUsingForm memberCardUsingForm = new CardUsingForm(Convert.ToDouble(creditCard.Text));
                if (memberCardUsingForm.ShowDialog() != DialogResult.OK)
                    return false;

                m_Member_List = memberCardUsingForm.m_memberList;
                if (watch)
                    BathClass.sendMessageToCamera(db, m_Seats[0].systemId);
            }

            if (coupon.Text != "" && watch)
                BathClass.sendMessageToCamera(db, m_Seats[0].systemId);

            if (zero.Text != "")
            {
                CustomerChooseForm customerChooseForm = new CustomerChooseForm();
                if (customerChooseForm.ShowDialog() != DialogResult.OK)
                    return false;

                customerId = customerChooseForm.customerId;
                Customer customer = db.Customer.FirstOrDefault(x => x.id.ToString() == customerId);
                customer.money += Convert.ToDouble(zero.Text);
                db.SubmitChanges();
            }

            if (sign.Text != "")
            {
                SignForFreeForm inputSerForm = new SignForFreeForm();
                if (inputSerForm.ShowDialog() != DialogResult.OK)
                    return false;

                signature = inputSerForm.signature;
            }
            return true;
        }

        //插入会员消费记录
        private void insert_member_infor()
        {
            double account_money = BathClass.get_account_money(account);
            if (m_promotion_Member != null)
            {
                CardCharge cc = new CardCharge();
                cc.CC_CardNo = m_promotion_Member.CI_CardNo;
                cc.CC_AccountNo = account.id.ToString();
                cc.CC_ItemExplain = "会员打折";
                cc.expense = account_money;
                cc.CC_InputOperator = LogIn.m_User.id.ToString();
                cc.CC_InputDate = GeneralClass.Now;
                db.CardCharge.InsertOnSubmit(cc);
                send_sms(m_promotion_Member.CI_CardNo);
            }

            if (m_Member_List != null)
            {
                foreach (string cardNo in m_Member_List.Keys)
                {
                    CardCharge cc = new CardCharge();
                    cc.CC_CardNo = cardNo;
                    cc.CC_AccountNo = account.id.ToString();
                    cc.CC_ItemExplain = "会员刷卡";
                    cc.CC_LenderSum = m_Member_List[cardNo];
                    cc.expense = account_money;
                    cc.CC_InputOperator = LogIn.m_User.id.ToString();
                    cc.CC_InputDate = GeneralClass.Now;
                    db.CardCharge.InsertOnSubmit(cc);
                    send_sms(cardNo);
                }
                //db.SubmitChanges();
            }
            //db.SubmitChanges();
        }

        private void send_sms(string cardno)
        {
            try
            {
                //var dc = new BathDBDataContext(LogIn.connectionString);
                var ci = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == cardno);
                if (ci.CI_Telephone == null || ci.CI_Telephone.Length != 11)
                    return;

                var mt = db.MemberType.FirstOrDefault(x => x.id == ci.CI_CardTypeNo);
                if (!BathClass.ToBool(mt.smsAfterUsing))
                    return;

                string msg = "尊敬的贵宾，您好！";
                msg += "您的" + mt.name + "，卡号：" + ci.CI_CardNo + "，";
                var cc = db.CardCharge.Where(y => y.CC_CardNo == ci.CI_CardNo);
                var debit = cc.Sum(y => y.CC_DebitSum);
                var lend = cc.Sum(y => y.CC_LenderSum);
                var cu = db.MemberSetting.FirstOrDefault().money;
                var balance_money = Convert.ToDouble(debit - lend);
                msg += "余额为：" + balance_money.ToString();

                if (mt.credits)
                {
                    var cexpense = db.CardCharge.Where(y => y.CC_CardNo == ci.CI_CardNo);
                    var cvs = cexpense.Sum(y => y.expense);
                    if (cvs.HasValue)
                    {
                        double cds = cvs.Value;
                        if (ci.CI_CreditsUsed == null)
                            msg += "积分为：" + (cds / cu).ToString();
                        else
                            msg += "积分为：" + (cds / cu - ci.CI_CreditsUsed).ToString();
                    }
                }

                String TypeStr = "";
                String CopyRightToCOM = "";
                String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
                string smsPort = BathClass.get_config_by_key("smsPort");
                string smsBaud = BathClass.get_config_by_key("smsBaud");
                if (smsPort == "" || smsBaud == "")
                {
                    SMmsForm smsForm = new SMmsForm();
                    if (smsForm.ShowDialog() != DialogResult.OK)
                        return;

                    smsPort = BathClass.get_config_by_key("smsPort");
                    smsBaud = BathClass.get_config_by_key("smsBaud");
                }

                if (smsPort == "" || smsBaud == "")
                    return;

                //SmsClass.Sms_Disconnection();
                if (SmsClass.Sms_Connection(CopyRightStr, uint.Parse(smsPort[3].ToString()), uint.Parse(smsBaud), out TypeStr, out CopyRightToCOM) != 1)
                    return;

                SmsClass.Sms_Send(ci.CI_Telephone, msg);
                SmsClass.Sms_Disconnection();
            }
            catch
            {}
        }
        
        //设置账单
        private void set_account()
        {
            //account.text = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.text).ToArray());
            //account.systemId = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            //account.openTime = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.openTime.ToString()).ToArray());
            //account.openEmployee = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.openEmployee).ToArray());

            account.departmentId = 1;
            account.text = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.text).ToArray());
            account.systemId = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            account.openTime = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.openTime.ToString()).ToArray());
            account.openEmployee = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.openEmployee).ToArray());
            account.payTime = GeneralClass.Now;
            account.payEmployee = LogIn.m_User.id.ToString();
            account.macAddress = BathClass.getMacAddr_Local();
            if (m_promotion_Member != null)
            {
                account.promotionMemberId = m_promotion_Member.CI_CardNo;
                account.promotionAmount = m_discount_money;
            }
            if (m_Member_List != null)
                account.memberId = string.Join("|", m_Member_List.Keys.ToArray());
        }

        //插入账单数据库
        private void insert_account()
        {
            set_account();
            if (bankUnion.Text != "")
                account.bankUnion = Convert.ToDouble(bankUnion.Text);

            if (creditCard.Text != "")
            {
                account.memberId = string.Join("|", m_Member_List.Keys.ToArray());
                account.creditCard = Convert.ToDouble(creditCard.Text);
            }

            if (coupon.Text != "")
            {
                account.coupon = Convert.ToDouble(coupon.Text);
            }

            if (groupBuy.Text != "")
            {
                account.groupBuy = Convert.ToDouble(groupBuy.Text);
            }

            if (zero.Text != "")
            {
                account.name = customerId;
                account.zero = Convert.ToDouble(zero.Text);
            }

            if (cash.Text != "")
            {
                account.changes = Convert.ToDouble(changes.Text);
                account.cash = Convert.ToDouble(cash.Text);
            }

            if (wipeZero.Text != "")
            {
                account.wipeZero = Convert.ToDouble(wipeZero.Text);
            }

            if (sign.Text != "")
            {
                account.server = Convert.ToDouble(sign.Text);
                account.serverEmployee = signature;
            }
        }

        //实时更新找零
        private void money_TextChanged(object sender, EventArgs e)
        {
            string msg = validateTextFilds();
            if (msg != "OK")
            {
                BathClass.printErrorMsg(msg);
                return;
            }

            double moneyPaid = 0;
            if (bankUnion.Text != "")
                moneyPaid += Convert.ToDouble(bankUnion.Text);

            if (creditCard.Text != "")
                moneyPaid += Convert.ToDouble(creditCard.Text);

            if (coupon.Text != "")
                moneyPaid += Convert.ToDouble(coupon.Text);

            if (groupBuy.Text != "")
                moneyPaid += Convert.ToDouble(groupBuy.Text);

            if (zero.Text != "")
                moneyPaid += Convert.ToDouble(zero.Text);

            if (cash.Text != "")
                moneyPaid += Convert.ToDouble(cash.Text);

            if (wipeZero.Text != "")
                moneyPaid += Convert.ToDouble(wipeZero.Text);

            if (sign.Text != "")
                moneyPaid += Convert.ToDouble(sign.Text);

            changes.Text = Math.Round((moneyPaid - Convert.ToDouble(moneyPayable.Text)), 2).ToString();
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
                    btnOk_Click(null, null);
                    break;
                case Keys.Add:
                    bankUnionTool_Click(null, null);
                    break;
                case Keys.Subtract:
                    creditCardTool_Click(null, null);
                    break;
                case Keys.Decimal:
                    signTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //银联结账
        private void bankUnionTool_Click(object sender, EventArgs e)
        {
            //2，更新订单信息,修改订单paid属性
            //3，插入账单数据库，更新
            //4，更新台位信息
            //5，更新客房信息
            if (!bankUnionTool.Enabled)
                return;

            bankUnionTool.Enabled = false;
            if (check_if_combined())
                return;

            //Account account = new Account();
            try
            {
                set_account();
                account.bankUnion = m_money;
                //account.bankUnion = money_real;
                db.Account.InsertOnSubmit(account);
                db.SubmitChanges();

                set_order_paid();
                update_seat_room();

                insert_member_infor();
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                handle_exception(ex);
            }
        }

        //储值卡结账
        private void creditCardTool_Click(object sender, EventArgs e)
        {
            //1，刷会员卡
            //2，更新订单信息,修改订单paid属性
            //3，插入账单数据库，更新
            //4，更新台位信息
            //5，更新客房信息
            if (!creditCardTool.Enabled)
                return;

            creditCardTool.Enabled = false;
            if (check_if_combined())
                return;

            try
            {
                CardUsingForm memberCardUsingForm = new CardUsingForm(Convert.ToDouble(moneyPayable.Text));
                if (memberCardUsingForm.ShowDialog() != DialogResult.OK)
                    return;

                if (m_Member_List == null)
                    m_Member_List = memberCardUsingForm.m_memberList;

                //Account account = new Account();
                set_account();
                account.creditCard = m_money;
                db.Account.InsertOnSubmit(account);
                db.SubmitChanges();

                set_order_paid();
                update_seat_room();
                insert_member_infor();
                db.SubmitChanges();

                BathClass.sendMessageToCamera(db, m_Seats[0].systemId);
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                handle_exception(ex);
            }
        }

        //挂账
        private void zeroTool_Click(object sender, EventArgs e)
        {
            //1，选择挂账单位
            //2，更新订单信息,修改订单paid属性
            //3，插入账单数据库，更新
            //4，更新客户信息
            //5，更新台位信息
            //6，更新客房信息
            if (!zeroTool.Enabled)
                return;

            zeroTool.Enabled = false;
            if (check_if_combined())
                return;

            try
            {
                CustomerChooseForm customerChooseForm = new CustomerChooseForm();
                if (customerChooseForm.ShowDialog() != DialogResult.OK)
                    return;

                Customer customer = db.Customer.FirstOrDefault(x => x.id.ToString() == customerChooseForm.customerId);
                customer.money += m_money;

                //Account account = new Account();
                set_account();
                account.zero = m_money;
                db.Account.InsertOnSubmit(account);
                db.SubmitChanges();

                set_order_paid();
                update_seat_room();
                insert_member_infor();
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                handle_exception(ex);
            }
        }

        //检查数据
        private string validateTextFilds()
        {
            double d = 0.0;
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox) && c.Text != "")
                {
                    TextBox tb = c as TextBox;
                    if (!double.TryParse(tb.Text, out d))
                    {
                        tb.SelectAll();
                        tb.Focus();
                        return "数据不合规范!";
                    }
                }
            }
            return "OK";
        }

        //只接受整数输入
        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private bool check_if_combined()
        {
            foreach (Control c in panel2.Controls)
            {
                if (c.GetType() == typeof(TextBox) && c.Text != "")
                {
                    BathClass.printErrorMsg("请输入金额后，点击确定以混合方式结账!");
                    return true;
                }
            }
            return false;
        }

        private void handle_exception(System.Exception ex)
        {
            //send_email(ex.Message);
            BathClass.printErrorMsg("系统出错：" + ex.Message + "，请重试!");

            if (account.id != 0)
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var act = dc.Account.FirstOrDefault(x => x.id == account.id);
                dc.Account.DeleteOnSubmit(act);
                dc.SubmitChanges();
            }
            this.DialogResult = DialogResult.Cancel;
        }

        private void bankUnion_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void send_email(string msg)
        {
            MailMessage myMail = new MailMessage();

            myMail.From = new MailAddress("466928773@qq.com");
            myMail.To.Add(new MailAddress("466928773@qq.com"));

            myMail.Subject = "软件错误信息";
            myMail.SubjectEncoding = Encoding.UTF8;

            myMail.Body = msg;
            myMail.BodyEncoding = Encoding.UTF8;
            myMail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.qq.com";
            smtp.Credentials = new NetworkCredential("466928773@qq.com", "flyfk000u");

            smtp.Send(myMail);
        }

        //签字免单
        private void signTool_Click(object sender, EventArgs e)
        {
            //2，更新订单信息,修改订单paid属性
            //3，插入账单数据库，更新
            //4，更新台位信息
            //5，更新客房信息
            if (check_if_combined())
                return;

            SignForFreeForm inputSerForm = new SignForFreeForm();
            if (inputSerForm.ShowDialog() != DialogResult.OK)
                return;

            if (!signTool.Enabled)
                return;

            signTool.Enabled = false;

            try
            {
                set_account();
                account.server = m_money;
                account.serverEmployee = inputSerForm.signature;
                db.Account.InsertOnSubmit(account);
                db.SubmitChanges();

                set_order_paid();
                update_seat_room();

                insert_member_infor();
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                handle_exception(ex);
            }
        }
    }
}
