using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using System.Transactions;
using YouSoftBathFormClass;
using System.Threading;
using System.Timers;

namespace YouSoftBathSelf
{
    //消费查询
    public partial class SeatExpenseForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Seat m_Seat = null;
        public bool paid = false;
        private CardInfo m_Member = null;
        private double discount_money = 0;
        private CardInfo m_promotion_Member;

        //构造函数
        public SeatExpenseForm(Seat seat)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seat = db.Seat.FirstOrDefault(x => x.text == seat.text);

            InitializeComponent();
        }

        //对话框载入
        private void SeatExpenseForm_Load(object sender, EventArgs e)
        {
            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);

            dgvExpense_show();
            setStatus();
        }

        //显示台位消费信息
        public void dgvExpense_show()
        {
            BathDBDataContext dc = new BathDBDataContext(LogIn.connectionString);
            dgvExpense.Rows.Clear();
            List<string> ids = new List<string>();
            ids.Add(m_Seat.systemId);

            var orders = dc.Orders.Where(x => ids.Contains(x.systemId) && x.deleteEmployee == null && !x.paid);
            orders = orders.OrderBy(x => x.inputTime);
            foreach (var o in orders)
            {
                string[] row = new string[10];
                row[0] = o.id.ToString();
                row[1] = o.text;
                row[2] = o.menu;
                row[3] = o.technician;
                row[4] = o.techType;

                row[6] = o.number.ToString();
                
                row[8] = o.inputTime.ToString();
                row[9] = o.inputEmployee;

                var m = db.Menu.FirstOrDefault(x=>x.name==o.menu);
                bool redRow = false;
                if (m==null)
                {
                    row[5] = "";
                    row[7] = o.money.ToString();
                    redRow = true;
                }
                else
                {
                    if (o.priceType == "每小时")
                    {
                        row[5] = o.money.ToString() + "/时";
                        row[7] = (Math.Ceiling((DateTime.Now - o.inputTime).TotalHours) * o.money).ToString();
                    }
                    else
                    {
                        row[5] = m.price.ToString();
                        row[7] = o.money.ToString();
                    }
                }

                dgvExpense.Rows.Add(row);
                if (redRow)
                {
                    dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            BathClass.set_dgv_fit(dgvExpense);
        }

        //显示信息
        private void setStatus()
        {
            double money = 0;
            seatText.Text = m_Seat.text;
            openTime.Text = m_Seat.openTime.ToString();
            TimeSpan ts = DateTime.Now - Convert.ToDateTime(m_Seat.openTime);
            timeSpan.Text = string.Format("{0}小时{1}分", ts.Hours, ts.Minutes);

            systemId.Text = m_Seat.systemId;

            money = BathClass.get_seat_expense(m_Seat, LogIn.connectionString);
            moneyPayable.Text = money.ToString();
        }

        //宾客付款
        private void payTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext dc = new BathDBDataContext(LogIn.connectionString);

            HintForm hintForm = new HintForm("请插入会员卡...");
            if (hintForm.ShowDialog() != DialogResult.OK)
                return;

            MemberCardUsingForm memberCardUsingForm = new MemberCardUsingForm(Convert.ToDouble(moneyPayable.Text));
            if (memberCardUsingForm.ShowDialog() != DialogResult.OK)
                return;

            if (m_Member == null)
                m_Member = memberCardUsingForm.m_member;

            Account account = new Account();
            account.text = m_Seat.text;
            account.systemId = m_Seat.systemId;
            account.openTime = m_Seat.openTime.ToString();
            account.openEmployee = m_Seat.openEmployee;
            account.payTime = DateTime.Now;
            account.payEmployee = LogIn.m_User.id.ToString();
            account.macAddress = PCUtil.getMacAddr_Local();
            if (m_promotion_Member != null)
            {
                account.promotionMemberId = m_promotion_Member.CI_CardNo;
                account.promotionAmount = discount_money;
            }
            if (m_Member != null)
                account.memberId = memberCardUsingForm.m_member.CI_CardNo;

            account.creditCard = Convert.ToInt32(moneyPayable.Text);
            db.Account.InsertOnSubmit(account);

            db.SubmitChanges();

            var orderList = db.Orders.Where(x => x.systemId == m_Seat.systemId && !x.paid);
            foreach (Orders order in orderList)
            {
                order.paid = true;
                order.accountId = account.id;
            }

            m_Seat.status = 3;
            var room = db.Room.FirstOrDefault(x => x.seat == m_Seat.text);
            if (room != null)
                room.status = "等待清洁";

            insert_member_infor(account);
            db.SubmitChanges();

            var act_old = dc.Account.FirstOrDefault(x => x.systemId == m_Seat.systemId && x.abandon != null);
            if (act_old == null)
            {
                printTool_Click("结账单");
                printTool_Click("存根单");
            }
            else
            {
                printTool_Click("补救单");
            }

            var act = dc.Account.FirstOrDefault(x => x.systemId == m_Seat.systemId && x.abandon == null);

        }

        //插入会员消费记录
        private void insert_member_infor(Account account)
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
                cc.CC_InputDate = DateTime.Now;
                db.CardCharge.InsertOnSubmit(cc);
            }

            if (m_Member != null)
            {
                CardCharge cc = new CardCharge();
                cc.CC_CardNo = m_Member.CI_CardNo;
                cc.CC_AccountNo = account.id.ToString();
                cc.CC_ItemExplain = "会员刷卡";
                cc.CC_LenderSum = account.creditCard;
                cc.expense = account_money;
                cc.CC_InputOperator = LogIn.m_User.id.ToString();
                cc.CC_InputDate = DateTime.Now;
                db.CardCharge.InsertOnSubmit(cc);
                db.SubmitChanges();
            }
            //db.SubmitChanges();
        }

        //插入账单数据库
        private void insert_account(ref Account account, string name)
        {
            account.text = m_Seat.text;
            account.systemId = m_Seat.systemId;
            account.openTime = m_Seat.openTime.ToString();
            account.openEmployee = m_Seat.openEmployee;
            account.payTime = DateTime.Now;
            account.payEmployee = LogIn.m_User.id;
            account.server = BathClass.get_seat_expense(m_Seat, LogIn.connectionString);
            account.serverEmployee = name;
            account.macAddress = PCUtil.getMacAddr_Local();

            db.Account.InsertOnSubmit(account);
            db.SubmitChanges();

        }

        //更新手牌，客房
        private void update_seat_room()
        {
            m_Seat.status = 3;
            var room = db.Room.FirstOrDefault(x => x.seat == m_Seat.text);
            if (room != null)
                room.status = "等待清洁";
        }

        //修改订单数据库的paid属性
        private void set_order_paid(Account account)
        {
            var orderList = db.Orders.Where(x => x.systemId == m_Seat.systemId && !x.paid);
            foreach (Orders order in orderList)
            {
                order.paid = true;
                order.accountId = account.id;
            }
        }

        //宾客转账
        private void transferTool_Click(object sender, EventArgs e)
        {
            if (m_Seat == null)
            {
                BathClass.printErrorMsg("未选择手牌!");
                return;
            }

            //TransferSelectForm transferSelectForm = new TransferSelectForm(m_Seat);
            //if (transferSelectForm.ShowDialog() != DialogResult.OK)
            //    return;

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");
            //PrintSeatBill.Print_DataGridView(m_seat, "转账确认单", dgvExpense, printCols, moneyPayable.Text,
            //    db.Options.FirstOrDefault().companyName);
            paid = true;
            this.DialogResult = DialogResult.OK;
        }

        //打印账单
        private void printTool_Click(string title)
        {
            //this.TopMost = true;

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");
            var act = db.Account.Where(x => x.systemId == m_Seat.systemId && x.abandon == null).
                OrderByDescending(x => x.payTime).FirstOrDefault();
            //PrintBill.Print_DataGridView(act, title, dgvExpense, printCols, 
                //db.Options.FirstOrDefault().companyName);
        }

        //会员打折
        private void memberTool_Click(object sender, EventArgs e)
        {
            double money_pre = Convert.ToDouble(moneyPayable.Text);
            MemberPromotionOptionForm memberPromotionOptionForm = new MemberPromotionOptionForm(m_Seat);
            if (memberPromotionOptionForm.ShowDialog() != DialogResult.OK)
                return;

            m_promotion_Member = memberPromotionOptionForm.m_Member;
            double money = BathClass.get_seat_expense(m_Seat, LogIn.connectionString);
            moneyPayable.Text = money.ToString();
            discount_money = money_pre - money;
            dgvExpense_show();
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
                case Keys.F1:
                case Keys.Add:
                    payTool_Click(null, null);
                    break;
                case Keys.F4:
                    transferTool_Click(null, null);
                    break;
                case Keys.F6:
                case Keys.Decimal:
                    memberTool_Click(null, null);
                    break;
                case Keys.F10:
                    toolReturn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //发送消息给鞋部
        private void sendMessageToShoes(Account account)
        {
            if (db.Options.Count() == 0)
                return;

            var q = db.Options.FirstOrDefault().启用鞋部;
            if (!Convert.ToBoolean(q))
                return;

            ShoeMsg msg = new ShoeMsg();
            msg.text = account.text;
            msg.payEmployee = account.payEmployee;
            msg.payTime = account.payTime;
            msg.processed = false;
            db.ShoeMsg.InsertOnSubmit(msg);
            db.SubmitChanges();
        }

        //退单 F10
        private void toolReturn_Click(object sender, EventArgs e)
        {
            BathDBDataContext dc = new BathDBDataContext(LogIn.connectionString);

            if (dgvExpense.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要输入订单!");
                return;
            }

            int orderId = Convert.ToInt32(dgvExpense.CurrentRow.Cells[0].Value);
            var order = dc.Orders.FirstOrDefault(x => x.id == orderId);
            if (order == null || order.menu.Contains("套餐"))
            {
                BathClass.printErrorMsg("不能删除套餐优惠!");
                return;
            }

            InputEmployeeByPwd inputServerForm = new InputEmployeeByPwd();
            if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;
            order.deleteEmployee = inputServerForm.employee.id.ToString();

            Employee employee = dc.Employee.FirstOrDefault(x => x.id.ToString() == order.technician);
            if (employee != null)
                employee.status = "空闲";

            var menu = dc.Menu.FirstOrDefault(x => x.name == order.menu);
            if (dc.Catgory.FirstOrDefault(x=>x.id == menu.catgoryId).name == "会员卡")
            {
                var cardno = dc.CardCharge.FirstOrDefault(x => x.systemId == order.systemId).CC_CardNo;
                dc.CardCharge.DeleteAllOnSubmit(dc.CardCharge.Where(x => x.systemId == order.systemId));

                if (!order.menu.Contains("充值") && BathClass.printAskMsg("是否删除售卡记录?") == DialogResult.Yes)
                {
                    dc.CardInfo.DeleteOnSubmit(dc.CardInfo.FirstOrDefault(x => x.CI_CardNo == cardno));
                }
            }
            dc.SubmitChanges();

            Seat seat = dc.Seat.FirstOrDefault(x => x.systemId == order.systemId);
            //BathClass.find_combo(dc, seat, null);
            dgvExpense_show();
            setStatus();
        }

        private void SeatExpenseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MConvert<bool>.ToTypeOrDefault(db.Options.FirstOrDefault().启用手牌锁, false))
                Thread.Sleep(3000);
        }
    }
}
