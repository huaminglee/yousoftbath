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
using YouSoftUtil;
using YouSoftBathGeneralClass;
using System.Drawing.Printing;

namespace YouSoftBathReception
{
    public partial class PayForm : Form
    {
        //成员变量
        private DAO dao;
        private List<CSeat> m_Seats = new List<CSeat>();
        //private List<CSeat> m_Seats_Real = new List<CSeat>();
        //private Dictionary<string, bool> seat_keeps = new Dictionary<string, bool>();
        Dictionary<Int64, bool> m_orders = new Dictionary<Int64, bool>();
        private string customerId = "";
        private Dictionary<string, double> m_Member_List = null;
        private CCardInfo m_promotion_Member = null;
        private double m_discount_money = 0;
        private bool watch = false;
        private double m_money;
        public CAccount account = new CAccount();
        private string signature;
        private int m_wipe_limit = 0;
        private DateTime now;

        private int _newAccountId = -1;
        public int newAccountId
        {
            get { return _newAccountId; }
            set { _newAccountId = value; }
        }

        //构造函数
        public PayForm(List<CSeat> _seats, Dictionary<Int64, bool> orders, 
            CCardInfo member, double discount_money, double money)
        {
            //seat_keeps = seats;
            m_orders = orders;
            m_wipe_limit = MConvert<int>.ToTypeOrDefault(LogIn.options.抹零限制, 0);

            m_Seats = _seats;
            //m_Seats.AddRange(db.Seat.Where(x => seats.Keys.Contains(x.text)));
            m_promotion_Member = member;
            m_discount_money = discount_money;
            m_money = money;
            InitializeComponent();
        }

        //对话框载入
        private void PayForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            BathClass.change_input_en();
            watch = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用结账监控, false);
            moneyPayable.Text = m_money.ToString();
            changes.Text = (-m_money).ToString();
            now = BathClass.Now(LogIn.connectionString);

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
            bool allNull = false;
            if (cash.Text.Trim() == "" && bankUnion.Text.Trim() == "" &&
                creditCard.Text.Trim() == "" && sign.Text.Trim() == "" &&
                zero.Text.Trim() == "" && coupon.Text.Trim() == "" &&
                groupBuy.Text.Trim() == "" && wipeZero.Text.Trim() == "")
                allNull = true;
            //foreach (Control c in panel2.Controls)
            //{
            //    if (c.GetType() == typeof(TextBox))
            //    {
            //        if (c.Text != "")
            //        {
            //            allNull = false;
            //            break;
            //        }
            //    }
            //}
            if (allNull)
            {
                cash.Text = m_money.ToString();
                changes.Text = "0";
            }

            if (m_wipe_limit > 0 && wipeZero.Text != "" && Convert.ToDouble(wipeZero.Text) >= m_wipe_limit)
            {
                wipeZero.SelectAll();
                BathClass.printErrorMsg("抹零必须小于" + m_wipe_limit.ToString() + "块!");
                wipeZero.Focus();  //调整一下顺序,并且选中抹零框中的数字
                wipeZero.SelectAll();

                btnOk.Enabled = true;
                return;
            }

            if (Convert.ToDouble(changes.Text) < 0)
            {
                BathClass.printErrorMsg("结账金额不足");
                btnOk.Enabled = true;   //警告之后确按钮要启用才行。

                return;
            }

            try
            {
                if (!memberCardNeed())
                {
                    btnOk.Enabled = true;
                    return;
                }

                string cmd_str = "";
                //Account account = new Account();
                set_account();

                //if (!dao.exist_instance("Account", "systemId='" + account.systemId + "' and abandon is null"))
                //{
                    string pars = @"text,systemId,openTime,openEmployee,payTime,payEmployee,macAddress";
                    string vals = "'" + account.text + "','" + account.systemId + "','" + account.openTime + "','" + account.openEmployee + 
                        "',getdate(),'" + account.payEmployee + "','" + account.macAddress + "'";
                    
                    if (account.promotionMemberId != null)
                    {
                        pars += ",promotionMemberId";
                        vals += ",'" + account.promotionMemberId + "'";
                    }

                    if (account.promotionAmount != null)
                    {
                        pars += ",promotionAmount";
                        vals += ",'" + account.promotionAmount + "'";
                    }

                    if (account.memberId != null)
                    {
                        pars += ",memberId";
                        vals += ",'" + account.memberId + "'";
                    }

                    //if (account.serverEmployee != null)
                    //{
                    //    pars += ",serverEmployee";
                    //    vals += ",'" + account.serverEmployee + "'";
                    //}

                    if (cash.Text.Trim() != "")
                    {
                        pars += ",cash";
                        vals += "," + cash.Text.Trim();

                        pars += ",changes";
                        vals += "," + changes.Text;
                    }

                    if (bankUnion.Text.Trim() != "")
                    {
                        pars += ",bankUnion";
                        vals += "," + bankUnion.Text.Trim();
                    }

                    if (creditCard.Text.Trim() != "")
                    {
                        pars += ",creditCard";
                        vals += "," + creditCard.Text.Trim();
                    }

                    if (coupon.Text.Trim() != "")
                    {
                        pars += ",coupon";
                        vals += "," + coupon.Text.Trim();
                    }

                    if (groupBuy.Text.Trim() != "")
                    {
                        pars += ",groupBuy";
                        vals += "," + groupBuy.Text.Trim();
                    }

                    if (zero.Text.Trim() != "")
                    {
                        pars += ",zero";
                        vals += "," + zero.Text.Trim();

                        pars += ",name";
                        vals += ",'" + customerId.ToString() + "'";
                    }

                    if (sign.Text.Trim() != "")
                    {
                        pars += ",server";
                        vals += "," + sign.Text.Trim();

                        pars += ",serverEmployee";
                        vals += ",'" + signature + "'";
                    }

                    //if (account.deducted == null)
                    //    cmd_str += "null,";
                    //else
                    //    cmd_str += "'" + account.deducted + "',";

                    //if (account.changes == null)
                    //    cmd_str += "null,";
                    //else
                    //    cmd_str += "'" + account.changes + "',";

                    if (wipeZero.Text.Trim() != "")
                    {
                        pars += ",wipeZero";
                        vals += "," + wipeZero.Text.Trim();
                    }
                    cmd_str = @"declare @NewAct TABLE (id INT NOT NULL PRIMARY KEY)";
                    cmd_str += @" insert into [Account](" + pars + ") output inserted.id into @NewAct values(" + vals + ") ";
                //}

                //var act = db.Account.FirstOrDefault(x=>x.systemId==account.systemId && x.abandon == null);
                //if (act == null)
                //{
                //    insert_account();
                //    db.Account.InsertOnSubmit(account);
                //    db.SubmitChanges();
                //}
                //else
                //{
                //    account = act;
                //}


                cmd_str += " " + set_order_paid();
                //db.SubmitChanges();

                cmd_str += " " + insert_member_infor();
                cmd_str += " " + update_seat_room();
                cmd_str += @" select id from @NewAct";
                //db.SubmitChanges();

                if (!dao.insert_account(cmd_str, ref _newAccountId))
                {
                    btnOk.Enabled = true;
                    BathClass.printErrorMsg("结账失败，请重试!");
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg("系统出错：" + ex.Message + "，请重试!");
            }
        }
        
        //更新手牌，客房
        private string update_seat_room()
        {
            string cmd_str = "";
            int count = m_Seats.Count;
            List<CRoom> rooms = new List<CRoom>();
            for (int i = 0; i < count; i++)
            {
                cmd_str += " if not exists (select * from [Orders] where systemId='" + m_Seats[i].systemId + "')";
                cmd_str += " update [Seat] set status=3 where systemId='" + m_Seats[i].systemId + "'";

                var room = dao.get_Room("select * from [Room] where seatIds like '%" + m_Seats[i].text + "%'");
                if (room == null)
                    continue;
                var _room = rooms.FirstOrDefault(x=>x.id==room.id);
                if (_room == null)
                    rooms.Add(room);
                else
                    room = _room;
                if (room != null && room.seatIds != null)
                {
                    var ids = room.seatIds.Split('|').ToList();
                    ids.Remove(m_Seats[i].text);
                    if (ids.Count != 0)
                        room.seatIds = string.Join("|", ids.ToArray());
                    else
                        room.seatIds = null;
                }
            }
            foreach (var room in rooms)
            {
                if (room.seatIds != null)
                    cmd_str += " update [Room] set seatIds='" + room.seatIds + "' where id=" + room.id;
                else
                    cmd_str += " update [Room] set seatIds=null where id=" + room.id;
            }

            return cmd_str;
        }

        //修改订单数据库的paid属性
        private string set_order_paid()
        {
            string id_str = "";
            foreach (KeyValuePair<long, bool> order in m_orders)
            {
                if (order.Value)
                {
                    if (id_str != "")
                        id_str += " or ";
                    id_str += "id=" + order.Key;
                }
            }

            StringBuilder sb_state = new StringBuilder();
            sb_state.Append(" (");
            int count = m_Seats.Count;
            for (int i = 0; i < count; i++)
            {
                sb_state.Append(@"systemId='").Append(m_Seats[i].systemId).Append("'");
                //state_str += @"systemId='" + m_Seats[i].systemId + "'";
                if (i != count - 1)
                    sb_state.Append(" or ");
                    //state_str += " or ";
            }
            sb_state.Append(") ");
            //state_str += ") ";

            string cmd_str = @"insert into [HisOrders](menu,text,systemId,number,priceType,"
                            + @" money,technician,techtype,startTime,inputTime,inputEmployee,deleteEmployee,"
                            + @" donorEmployee,comboId,paid,accountId,billId,departmentId,deleteExplain,deleteTime,roomId,donorExplain,donorTime) "
                            + @" select menu,text,systemId,number,priceType,"
                            + @" money,technician,techtype,startTime,inputTime,inputEmployee,deleteEmployee,"
                            + @" donorEmployee,comboId,'False',(select id from @NewAct),billId,departmentId,deleteExplain,deleteTime,roomId,donorExplain,donorTime "
                            + @" from [Orders] where("
                            + @" deleteEmployee is not null and " + sb_state.ToString() + ")";

            cmd_str += @" delete from [Orders] where (deleteEmployee is not null and " + sb_state.ToString() + ")";
            if (id_str == "")
                return cmd_str;

            cmd_str += @"insert into [HisOrders](menu,text,systemId,number,priceType,"
                            + @" money,technician,techtype,startTime,inputTime,inputEmployee,deleteEmployee,"
                            + @" donorEmployee,comboId,paid,accountId,billId,departmentId,deleteExplain,deleteTime,roomId,donorExplain,donorTime) "
                            + @" select menu,text,systemId,number,priceType,"
                            + @" money,technician,techtype,startTime,inputTime,inputEmployee,deleteEmployee,"
                            + @" donorEmployee,comboId,'True',(select id from @NewAct),billId,departmentId,deleteExplain,deleteTime,roomId,donorExplain,donorTime "
                            + @" from [Orders] where("
                            + @" paid='False' and";
            
            cmd_str += "(" + id_str + "))"
                + @" delete from [Orders] where(" + id_str + ")";

            //cmd_str += @" update [HisOrders] set paid='True', accountId=(select id from [Account] where (abandon is null and systemId='"
            //    + account.systemId + "')) where (" + state_str + ")";

            return cmd_str;
            //foreach (var seat in m_Seats)
            //{
                /*if (seat_keeps[seat.text])
                {
                    double seat_money = BathClass.get_seat_expense(seat, LogIn.connectionString);
                    
                    Orders order = new Orders();
                    order.menu = "留牌预付";
                    order.text = seat.text;
                    order.systemId = seat.systemId;
                    order.number = 1;
                    order.money = -seat_money;
                    order.inputTime = now;
                    order.inputEmployee = LogIn.m_User.id;
                    order.paid = false;
                    db.Orders.InsertOnSubmit(order);

                    var ho = new HisOrders();
                    ho.menu = "留牌";
                    ho.text = seat.text;
                    ho.systemId = seat.systemId;
                    ho.number = 1;
                    ho.money = seat_money;
                    ho.inputTime = now;
                    ho.inputEmployee = LogIn.m_User.id;
                    ho.paid = true;
                    ho.accountId = account.id;
                    db.HisOrders.InsertOnSubmit(ho);

                    continue;
                }*/
                //var orderList = db.Orders.Where(x => x.systemId == seat.systemId && !x.paid);
                //foreach (Orders order in orderList)
                //{
                    //if (m_orders.Keys.Contains(order.id) && !m_orders[order.id])
                    //    continue;

                //    var ho = new HisOrders();
                //    ho.menu = order.menu;
                //    ho.text = order.text;
                //    ho.systemId = order.systemId;
                //    ho.number = order.number;
                //    ho.priceType = order.priceType;
                //    ho.money = order.money;
                //    ho.technician = order.technician;
                //    ho.techType = order.techType;
                //    ho.startTime = order.startTime;
                //    ho.inputTime = order.inputTime;
                //    ho.inputEmployee = order.inputEmployee;
                //    ho.deleteEmployee = order.deleteEmployee;
                //    ho.donorEmployee = order.donorEmployee;
                //    ho.comboId = order.comboId;
                //    ho.paid = true;
                //    ho.accountId = account.id;
                //    ho.billId = order.billId;
                //    db.HisOrders.InsertOnSubmit(ho);
                //    db.Orders.DeleteOnSubmit(order);
                //}
            //}
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
                {
                    if (!BathClass.sendMessageToCamera(dao, m_Seats[0].systemId))
                    {
                        BathClass.printErrorMsg("发送消息给摄像头失败，请重试!");
                        return false;
                    }
                }
            }

            if (coupon.Text != "" && watch)
            {
                if (!BathClass.sendMessageToCamera(dao, m_Seats[0].systemId))
                {
                    BathClass.printErrorMsg("发送消息给摄像头失败，请重试!");
                    return false;
                }
            }

            if (zero.Text != "")
            {
                CustomerChooseForm customerChooseForm = new CustomerChooseForm();
                if (customerChooseForm.ShowDialog() != DialogResult.OK)
                    return false;

                customerId = customerChooseForm.customerId;

                string cmd_str = "update [Customer] set money=isnull(money,0)+" + zero.Text + " where id=" + customerId.ToString();
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("客户累计金额失败，请重试!");
                    return false;
                }
                ////Customer customer = db.Customer.FirstOrDefault(x => x.id.ToString() == customerId);
                ////customer.money += Convert.ToDouble(zero.Text);
                ////db.SubmitChanges();
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
        private string insert_member_infor()
        {
            string cmd_str = "";
            //double account_money = BathClass.get_account_money(account);
            if (m_promotion_Member != null)
            {
                cmd_str = @"insert into [CardCharge](CC_CardNo,CC_AccountNo,CC_ItemExplain,expense,CC_InputOperator,"
                    + @"CC_InputDate) select "
                    + @"'" + m_promotion_Member.CI_CardNo + "',"
                    + @"id,"
                    + @"'会员打折',"
                    + @"'" + m_money.ToString() + "',"
                    + @"'" + LogIn.m_User.id + "',"
                    + @"getdate() from account where (abandon is null and systemId='" + account.systemId + "')";
                //CardCharge cc = new CardCharge();
                //cc.CC_CardNo = m_promotion_Member.CI_CardNo;
                //cc.CC_AccountNo = account.id.ToString();
                //cc.CC_ItemExplain = "会员打折";
                //cc.expense = account_money;
                //cc.CC_InputOperator = LogIn.m_User.id.ToString();
                //cc.CC_InputDate = now;
                //db.CardCharge.InsertOnSubmit(cc);
                send_sms(m_promotion_Member.CI_CardNo);
            }

            if (m_Member_List != null)
            {
                foreach (string cardNo in m_Member_List.Keys)
                {
                    cmd_str += @" insert into [CardCharge](CC_CardNo,CC_AccountNo,CC_ItemExplain,CC_LenderSum, expense,"
                    + @"CC_InputOperator,CC_InputDate) select "
                    + @"'" + cardNo + "',"
                    + @" id ,"
                    + @"'会员刷卡',"
                    + @"'" + m_Member_List[cardNo].ToString() + "',"
                    + @"'" + m_money.ToString() + "',"
                    + @"'" + LogIn.m_User.id + "',"
                    + @"getdate() from account where (abandon is null and systemId='" + account.systemId + "')";
                    //CardCharge cc = new CardCharge();
                    //cc.CC_CardNo = cardNo;
                    //cc.CC_AccountNo = account.id.ToString();
                    //cc.CC_ItemExplain = "会员刷卡";
                    //cc.CC_LenderSum = m_Member_List[cardNo];
                    //cc.expense = account_money;
                    //cc.CC_InputOperator = LogIn.m_User.id.ToString();
                    //cc.CC_InputDate = now;
                    //db.CardCharge.InsertOnSubmit(cc);
                    send_sms(cardNo);
                }
                //db.SubmitChanges();
            }
            //db.SubmitChanges();

            return cmd_str;
        }

        private void send_sms(string cardno)
        {
            //try
            //{
                //修改
                //var dc = new BathDBDataContext(LogIn.connectionString);
            //    var ci = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == cardno);
            //    if (ci.CI_Telephone == null || ci.CI_Telephone.Length != 11)
            //        return;

            //    var mt = db.MemberType.FirstOrDefault(x => x.id == ci.CI_CardTypeNo);
            //    if (!BathClass.ToBool(mt.smsAfterUsing))
            //        return;

            //    string msg = "尊敬的贵宾，您好！";
            //    msg += "您的" + mt.name + "，卡号：" + ci.CI_CardNo + "，";
            //    var cc = db.CardCharge.Where(y => y.CC_CardNo == ci.CI_CardNo);
            //    var debit = cc.Sum(y => y.CC_DebitSum);
            //    var lend = cc.Sum(y => y.CC_LenderSum);
            //    var cu = db.MemberSetting.FirstOrDefault().money;
            //    var balance_money = Convert.ToDouble(debit - lend);
            //    msg += "余额为：" + balance_money.ToString();

            //    if (mt.credits)
            //    {
            //        var cexpense = db.CardCharge.Where(y => y.CC_CardNo == ci.CI_CardNo);
            //        var cvs = cexpense.Sum(y => y.expense);
            //        if (cvs.HasValue)
            //        {
            //            double cds = cvs.Value;
            //            if (ci.CI_CreditsUsed == null)
            //                msg += "积分为：" + (cds / cu).ToString();
            //            else
            //                msg += "积分为：" + (cds / cu - ci.CI_CreditsUsed).ToString();
            //        }
            //    }

            //    String TypeStr = "";
            //    String CopyRightToCOM = "";
            //    String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
            //    string smsPort = BathClass.get_config_by_key("smsPort");
            //    string smsBaud = BathClass.get_config_by_key("smsBaud");
            //    if (smsPort == "" || smsBaud == "")
            //    {
            //        SMmsForm smsForm = new SMmsForm();
            //        if (smsForm.ShowDialog() != DialogResult.OK)
            //            return;

            //        smsPort = BathClass.get_config_by_key("smsPort");
            //        smsBaud = BathClass.get_config_by_key("smsBaud");
            //    }

            //    if (smsPort == "" || smsBaud == "")
            //        return;

            //    //SmsClass.Sms_Disconnection();
            //    if (SmsClass.Sms_Connection(CopyRightStr, uint.Parse(smsPort[3].ToString()), uint.Parse(smsBaud), out TypeStr, out CopyRightToCOM) != 1)
            //        return;

            //    SmsClass.Sms_Send(ci.CI_Telephone, msg);
            //    SmsClass.Sms_Disconnection();
            //}
            //catch
            //{}
        }
        
        //设置账单
        private void set_account()
        {
            //account.text = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.text).ToArray());
            //account.systemId = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            //account.openTime = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.openTime.ToString()).ToArray());
            //account.openEmployee = string.Join("|", m_Seats_Real.OrderBy(x => x.text).Select(x => x.openEmployee).ToArray());

            account.text = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.text).ToArray());
            account.systemId = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            account.openTime = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.openTime.ToString()).ToArray());
            account.openEmployee = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.openEmployee).ToArray());
            //account.payTime = now;
            account.payEmployee = LogIn.m_User.id.ToString();
            account.macAddress = PCUtil.getMacAddr_Local();
            if (m_promotion_Member != null)
            {
                account.promotionMemberId = m_promotion_Member.CI_CardNo;
                account.promotionAmount = m_discount_money;
            }
            if (m_Member_List != null)
                account.memberId = string.Join("|", m_Member_List.Keys.ToArray());
        }

        //插入账单数据库
        /*private void insert_account()
        {
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
        }*/

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
            if (bankUnion.Text != "" && bankUnion.Text != "-")
                moneyPaid += Convert.ToDouble(bankUnion.Text);

            if (creditCard.Text != "" && creditCard.Text != "-")
                moneyPaid += Convert.ToDouble(creditCard.Text);

            if (coupon.Text != "" && coupon.Text != "-")
                moneyPaid += Convert.ToDouble(coupon.Text);

            if (groupBuy.Text != "" && groupBuy.Text != "-")
                moneyPaid += Convert.ToDouble(groupBuy.Text);

            if (zero.Text != "" && zero.Text != "-")
                moneyPaid += Convert.ToDouble(zero.Text);

            if (cash.Text != "" && cash.Text != "-")
                moneyPaid += Convert.ToDouble(cash.Text);

            if (wipeZero.Text != "" && wipeZero.Text != "-")
                moneyPaid += Convert.ToDouble(wipeZero.Text);

            if (sign.Text != "" && sign.Text != "-")
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
            {
                bankUnionTool.Enabled = true;
                btnOk.Enabled = true;

                return;
            }

            //Account account = new Account();
            try
            {
                string cmd_str = "";
                set_account();
                //if (!dao.exist_instance("Account", "systemId='" + account.systemId + "' and abandon is null"))
                //{
                    string pars = @"text,systemId,openTime,openEmployee,payTime,payEmployee,macAddress";
                    string vals = "'" + account.text + "','" + account.systemId + "','" + account.openTime + "','" + account.openEmployee +
                        "',getdate(),'" + account.payEmployee + "','" + account.macAddress + "'";

                    if (account.promotionMemberId != null)
                    {
                        pars += ",promotionMemberId";
                        vals += ",'" + account.promotionMemberId + "'";
                    }

                    if (account.promotionAmount != null)
                    {
                        pars += ",promotionAmount";
                        vals += ",'" + account.promotionAmount + "'";
                    }

                    pars += ",bankUnion";
                    vals += "," + m_money.ToString();

                    cmd_str = @"declare @NewAct TABLE (id INT NOT NULL PRIMARY KEY)";
                    cmd_str += @" insert into [Account](" + pars + ") output inserted.id into @NewAct values(" + vals + ") ";
                //}


                cmd_str += " " + set_order_paid();
                //db.SubmitChanges();

                cmd_str += " " + insert_member_infor();
                cmd_str += " " + update_seat_room();
                cmd_str += @" select id from @NewAct";
                //db.SubmitChanges();

                if (!dao.insert_account(cmd_str, ref _newAccountId))
                {
                    btnOk.Enabled = true;
                    BathClass.printErrorMsg("结账失败，请重试!");
                    return;
                }
                //var act = db.Account.FirstOrDefault(x => x.systemId == account.systemId && x.abandon == null);
                //if (act == null)
                //{
                //    db.Account.InsertOnSubmit(account);
                //    db.SubmitChanges();
                //}
                //else
                //{
                //    account = act;
                //}

                //set_order_paid();
                //insert_member_infor();
                //db.SubmitChanges();

                //update_seat_room();
                //db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg("系统出错：" + ex.Message + "，请重试!");
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
            {
                creditCardTool.Enabled = true;
                btnOk.Enabled = true;
                return;
            }

            try
            {
                CardUsingForm memberCardUsingForm = new CardUsingForm(Convert.ToDouble(moneyPayable.Text));
                if (memberCardUsingForm.ShowDialog() != DialogResult.OK)
                {
                    creditCardTool.Enabled = true;
                    btnOk.Enabled = true;
                    return;
                }

                if (m_Member_List == null)
                    m_Member_List = memberCardUsingForm.m_memberList;

                //Account account = new Account();
                string cmd_str = "";
                set_account();
                //if (!dao.exist_instance("Account", "systemId='" + account.systemId + "' and abandon is null"))
                //{
                    string pars = @"text,systemId,openTime,openEmployee,payTime,payEmployee,macAddress";
                    string vals = "'" + account.text + "','" + account.systemId + "','" + account.openTime + "','" + account.openEmployee +
                        "',getdate(),'" + account.payEmployee + "','" + account.macAddress + "'";

                    if (account.promotionMemberId != null)
                    {
                        pars += ",promotionMemberId";
                        vals += ",'" + account.promotionMemberId + "'";
                    }

                    if (account.promotionAmount != null)
                    {
                        pars += ",promotionAmount";
                        vals += ",'" + account.promotionAmount + "'";
                    }

                    if (account.memberId != null)
                    {
                        pars += ",memberId";
                        vals += ",'" + account.memberId + "'";
                    }

                    pars += ",creditCard";
                    vals += "," + m_money.ToString();

                    cmd_str = @"declare @NewAct TABLE (id INT NOT NULL PRIMARY KEY)";
                    cmd_str += @" insert into [Account](" + pars + ") output inserted.id into @NewAct values(" + vals + ") ";
                //}


                cmd_str += " " + set_order_paid();
                cmd_str += " " + insert_member_infor();
                cmd_str += " " + update_seat_room();
                cmd_str += " select id from @NewAct";

                if (!dao.insert_account(cmd_str, ref _newAccountId))
                {
                    btnOk.Enabled = true;
                    BathClass.printErrorMsg("结账失败，请重试!");
                    return;
                }

                //var act = db.Account.FirstOrDefault(x => x.systemId == account.systemId && x.abandon == null);
                //if (act == null)
                //{
                //    db.Account.InsertOnSubmit(account);
                //    db.SubmitChanges();
                //}
                //else
                //{
                //    account = act;
                //}

                //set_order_paid();
                //insert_member_infor();
                //db.SubmitChanges();

                //update_seat_room();
                //db.SubmitChanges();

                //BathClass.sendMessageToCamera(db, m_Seats[0].systemId);
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg("系统出错：" + ex.Message + "，请重试!");
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
            {
                zeroTool.Enabled = true;
                btnOk.Enabled = true;
                return;
            }

            try
            {
                CustomerChooseForm customerChooseForm = new CustomerChooseForm();
                if (customerChooseForm.ShowDialog() != DialogResult.OK)
                {
                    zeroTool.Enabled = true;
                    btnOk.Enabled = true;
                    return;
                }

                set_account();
                string cId = customerChooseForm.customerId.ToString();
                //Customer customer = db.Customer.FirstOrDefault(x => x.id.ToString() == customerChooseForm.customerId);
                //customer.money += m_money;
                string cmd_str = @"update customer set money=isnull(money,0)+" + m_money.ToString() + " where id=" + cId;

                //if (!dao.exist_instance("Account", "systemId='" + account.systemId + "' and abandon is null"))
                //{
                    string pars = @"text,systemId,openTime,openEmployee,payTime,payEmployee,macAddress";
                    string vals = "'" + account.text + "','" + account.systemId + "','" + account.openTime + "','" + account.openEmployee +
                        "',getdate(),'" + account.payEmployee + "','" + account.macAddress + "'";

                    if (account.promotionMemberId != null)
                    {
                        pars += ",promotionMemberId";
                        vals += ",'" + account.promotionMemberId + "'";
                    }

                    if (account.promotionAmount != null)
                    {
                        pars += ",promotionAmount";
                        vals += ",'" + account.promotionAmount + "'";
                    }

                    pars += ",zero";
                    vals += "," + m_money.ToString();

                    pars += ",name";
                    vals += ",'" + cId + "'";

                    cmd_str += @" declare @NewAct TABLE (id INT NOT NULL PRIMARY KEY)";
                    cmd_str += @" insert into [Account](" + pars + ") output inserted.id into @NewAct values(" + vals + ") ";
                //}

                cmd_str += " " + set_order_paid();
                cmd_str += " " + insert_member_infor();
                cmd_str += " " + update_seat_room();
                cmd_str += " select id from @NewAct";
                if (!dao.insert_account(cmd_str, ref _newAccountId))
                {
                    btnOk.Enabled = true;
                    BathClass.printErrorMsg("结账失败，请重试!");
                    return;
                }

                ////Account account = new Account();
                //set_account();
                //account.zero = m_money;
                //var act = db.Account.FirstOrDefault(x => x.systemId == account.systemId && x.abandon == null);
                //if (act == null)
                //{
                //    db.Account.InsertOnSubmit(account);
                //    db.SubmitChanges();
                //}
                //else
                //{
                //    account = act;
                //}

                //set_order_paid();
                //insert_member_infor();
                //db.SubmitChanges();

                //update_seat_room();
                //db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg("系统出错：" + ex.Message + "，请重试!");
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
                string cmd_str = "";
                set_account();
                //account.server = m_money;
                //account.serverEmployee = inputSerForm.signature;

                //if (!dao.exist_instance("Account", "systemId='" + account.systemId + "' and abandon is null"))
                //{
                    string pars = @"text,systemId,openTime,openEmployee,payTime,payEmployee,macAddress";
                    string vals = "'" + account.text + "','" + account.systemId + "','" + account.openTime + "','" + account.openEmployee +
                        "',getdate(),'" + account.payEmployee + "','" + account.macAddress + "'";

                    if (account.promotionMemberId != null)
                    {
                        pars += ",promotionMemberId";
                        vals += ",'" + account.promotionMemberId + "'";
                    }

                    if (account.promotionAmount != null)
                    {
                        pars += ",promotionAmount";
                        vals += ",'" + account.promotionAmount + "'";
                    }

                    pars += ",server";
                    vals += "," + m_money.ToString();

                    pars += ",serverEmployee";
                    vals += ",'" + inputSerForm.signature + "'";

                    cmd_str = @"declare @NewAct TABLE (id INT NOT NULL PRIMARY KEY)";
                    cmd_str += @" insert into [Account](" + pars + ") output inserted.id into @NewAct values(" + vals + ") ";
                //}

                cmd_str += " " + set_order_paid();
                cmd_str += " " + insert_member_infor();
                cmd_str += " " + update_seat_room();
                cmd_str += " select id from @NewAct";

                if (!dao.insert_account(cmd_str, ref _newAccountId))
                {
                    btnOk.Enabled = true;
                    BathClass.printErrorMsg("结账失败，请重试!");
                    return;
                }
                //db.Account.InsertOnSubmit(account);
                //db.SubmitChanges();

                //set_order_paid();
                //insert_member_infor();
                //db.SubmitChanges();

                //update_seat_room();
                //db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg("系统出错：" + ex.Message + "，请重试!");
            }
        }

        private void PayForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        //优惠券验证
        private void couponTool_Click(object sender, EventArgs e)
        {
            var form = new WXCouponVerifyForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                coupon.Text = form.couponValue.ToString();
            }
        }
    }
}
