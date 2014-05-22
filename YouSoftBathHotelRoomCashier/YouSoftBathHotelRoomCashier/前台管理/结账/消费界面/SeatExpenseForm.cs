using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using YouSoftBathGeneralClass;
using System.Transactions;
using YouSoftBathFormClass;
using System.Threading;
using System.Timers;

namespace YouSoftBathReception
{
    //消费查询
    public partial class SeatExpenseForm : Form
    {
        //成员变量
        //private BathDBDataContext db = null;
        public List<HotelRoom> m_Seats = new List<HotelRoom>();
        private HotelRoom m_Seat = null;
        //private bool chain = true;
        private CardInfo m_Member = null;
        private double discount_money = 0;
        private bool inputBillId = false;
        private string companyName;

        private bool printBill = false;
        private bool printStubBill = false;
        private bool printShoe = false;

        //private bool manul_pay = false;
        private bool use_oyd = false;
        private Options m_Options;

        //构造函数
        public SeatExpenseForm(HotelRoom seat)
        {
            var db = new BathDBDataContext(LogIn.connectionString);

            if (MainWindow.seatLock)
            {
                m_Seats.Add(db.HotelRoom.FirstOrDefault(x => x.text == seat.text));
            }
            else
            {
                var seats = db.HotelRoom.Where(x => (seat.chainId == null && x.text == seat.text) || (seat.chainId != null && x.chainId == seat.chainId));
                seats = seats.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status == 8);
                m_Seats.AddRange(seats);
            }
            foreach (var s in m_Seats)
            {
                s.paying = true;
            }
            db.SubmitChanges();
            //m_Seats.AddRange(db.HotelRoom.Where(x => x.chainId == seat.chainId && (x.status == 2 || x.status == 6 || x.status == 7)));

            InitializeComponent();
        }

        //对话框载入
        private void SeatExpenseForm_Load(object sender, EventArgs e)
        {
            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);

            dgvChain.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 17);
            dgvChain.RowsDefaultCellStyle.Font = new Font("宋体", 17);

            var db = new BathDBDataContext(LogIn.connectionString);
            m_Options = db.Options.FirstOrDefault();
            printBill = BathClass.ToBool(m_Options.结账打印结账单);
            printShoe = BathClass.ToBool(m_Options.结账打印取鞋小票);
            printStubBill = BathClass.ToBool(m_Options.结账打印存根单);
            inputBillId = BathClass.ToBool(m_Options.录单输入单据编号);
            companyName = m_Options.companyName;
            //manul_pay = BathClass.ToBool(ops.允许手工输入手牌号结账);
            use_oyd = BathClass.ToBool(m_Options.启用手牌锁);

            if (use_oyd)
            {
                dgvChain.ReadOnly = true;
                btnReload.Text = "留牌";
                //btnChain.Visible = false;
                //seatText.ReadOnly = true;
                //btnReload.Visible = false;
            }
            dgvExpense.Columns[1].Visible = inputBillId;
            m_Seat = m_Seats[0];
            dgvChain_show();
            dgvChain.CurrentCell = dgvChain[0, 0];
            //chain = true;

            order_guoye(m_Seat, db);
            dgvExpense_show(db);
            setStatus(db);
        }

        //手牌线程
        private void seatTimer_Elapsed()
        {
            if (!MainWindow.seatLock)
            {
                BathClass.printErrorMsg("未启用手牌锁!");
                return;
            }

            if (MainWindow.lock_type == "欧亿达")
            {
                if (OYD.FKOPEN() != 1)
                    return;

                OYD.CH375SetTimeout(0, 5000, 5000);
                Thread.Sleep(500);
            }
            byte[] buff = new byte[200];

            int rt = -1;
            if (MainWindow.lock_type == "欧亿达")
            {
                rt = OYD.OYEDA_id(buff);
                Thread.Sleep(500);
            }
            else if (MainWindow.lock_type == "锦衣卫")
                rt = JYW.ReadID(buff);

            if (rt != 0)
                return;

            string str = "";
            string seat_text = "";
            if (MainWindow.lock_type == "欧亿达")
            {
                str = Encoding.Default.GetString(buff, 0, 20).Trim();
                seat_text = str.Substring(str.Length - BathClass.lock_id_length);
            }
            else if (MainWindow.lock_type == "锦衣卫")
            {
                str = BathClass.byteToHexStr(buff);
                seat_text = str.Substring(17, BathClass.lock_id_length);
            }

            var db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = db_new.HotelRoom.FirstOrDefault(x => x.text == seat_text);

            if (seat == null || (seat.status != 2 && seat.status != 6 && seat.status != 7 && seat.status != 8))
            {
                BathClass.printErrorMsg("该手牌不在使用中，不能结账!");
                return;
            }

            if ((MainWindow.lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                    (MainWindow.lock_type == "锦衣卫" && JYW.MD(buff) != 0))
                return;

            if (!m_Seats.Select(x => x.text).Contains(seat.text))
            {
                m_Seats.Add(seat);
                update_ui(seat, db_new);
            }
        }

        private void update_ui(HotelRoom seat, BathDBDataContext dc)
        {
            //dgvChain_show();
            string t = Convert.ToDateTime(seat.openTime).ToShortTimeString();
            dgvChain.Rows.Add();
            dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[0].Value = seat.text;
            dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[1].Value = false;
            dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[2].Value = t;

            dgvChain.CurrentCell = null;
            //chain = true;
            dgvExpense_show(dc);
            setStatus(dc);
        }

        private delegate void method_delegate();

        //显示台位消费信息
        public void dgvExpense_show(BathDBDataContext dc)
        {
            //BathDBDataContext dc = new BathDBDataContext(LogIn.connectionString);
            dgvExpense.Rows.Clear();
            //List<string> ids = new List<string>();
            var ids = m_Seats.Select(x => x.systemId);

            var orders = dc.Orders.Where(x => ids.Contains(x.systemId) && x.deleteEmployee == null && !x.paid);
            orders = orders.OrderBy(x => x.inputTime);
            foreach (var o in orders)
            {
                string[] row = new string[11];
                row[0] = o.id.ToString();
                row[1] = o.billId;
                row[2] = o.text;
                row[3] = o.menu;
                row[4] = o.technician;
                row[5] = o.techType;

                row[7] = o.number.ToString();
                
                row[9] = o.inputTime.ToString();
                row[10] = o.inputEmployee;

                var m = dc.Menu.FirstOrDefault(x=>x.name==o.menu);
                bool redRow = false;
                if (m==null)
                {
                    row[6] = "";
                    row[8] = o.money.ToString();
                    redRow = true;
                }
                else
                {
                    if (o.priceType == "每小时")
                    {
                        row[6] = o.money.ToString() + "/时";
                        row[8] = (Math.Ceiling((GeneralClass.Now - o.inputTime).TotalHours) * o.money).ToString();
                    }
                    else
                    {
                        row[6] = m.price.ToString();
                        row[8] = o.money.ToString();
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

        //联台手牌信息显示
        private void dgvChain_show()
        {
            dgvChain.Rows.Clear();

            foreach (HotelRoom seat in m_Seats)
            {
                string t = Convert.ToDateTime(seat.openTime).ToShortTimeString();
                dgvChain.Rows.Add();
                dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[0].Value = seat.text;
                dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[2].Value = t;
            }
            BathClass.set_dgv_fit(dgvChain);
            dgvChain.CurrentCell = null;
        }

        //显示信息
        private void setStatus(BathDBDataContext dc)
        {
            double money = 0;
            population.Text = m_Seats.Sum(x => x.population).ToString();
            money = BathClass.get_rooms_expenses(dc, m_Seats, LogIn.connectionString);
            seatText.Text = "";
            openTime.Text = "";
            timeSpan.Text = "";
            expense.Text = money.ToString();
            moneyPayable.Text = money.ToString();
        }

        //宾客付款
        private void payTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext dc = new BathDBDataContext(LogIn.connectionString);

            var seats = new List<HotelRoom>();
            seats = m_Seats;
            List<bool> keeps = new List<bool>();
            foreach (DataGridViewRow r in dgvChain.Rows)
            {
                keeps.Add(r.Cells[1].EditedFormattedValue.ToString() == "True");
            }

            PayForm payForm = new PayForm(seats, keeps, m_Member, discount_money);
            if (payForm.ShowDialog() != DialogResult.OK)
                return;

            var ids = string.Join("|", m_Seats.OrderBy(x=>x.text).Select(x => x.systemId).ToArray());
            var act_old = dc.Account.FirstOrDefault(x => x.systemId == ids && x.abandon != null);
            var act = dc.Account.FirstOrDefault(x => x.systemId == ids && x.abandon == null);
            if (act_old == null)
            {
                if (printBill)
                    printTool_Click(dc, "结账单");
                if (printStubBill)
                    printTool_Click(dc, "存根单");
                if (printShoe)
                {
                    List<string> seat_texts = new List<string>();
                    var ts = act.text.Split('|').ToList();
                    foreach (var tx in ts )
                    {
                        var s = m_Seats.FirstOrDefault(x => x.text == tx);
                        if (s == null) continue;
                        int i = m_Seats.IndexOf(s);
                        if (dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
                            seat_texts.Add(tx);
                    }
                    PrintShoeMsg.Print_DataGridView(seat_texts, act.payEmployee, act.payTime.ToString(), companyName);
                }
            }
            else
            {
                printTool_Click(dc, "补救单");
            }

            sendMessageToShoes(act);

            //处理留牌
            bool kept = false;
            for (int i = 0; i < m_Seats.Count; i++ )
            {
                if (dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
                    continue;

                kept = true;
                var s = m_Seats[i];
                var seat = dc.HotelRoom.FirstOrDefault(x => x.systemId == s.systemId);
                var orders = dc.Orders.Where(x => x.systemId == seat.systemId && x.priceType == "每小时");
                foreach (var order in orders)
                {
                    order.inputTime = GeneralClass.Now;
                    order.paid = false;
                    order.accountId = null;
                }
                seat.status = 2;
            }
            if (kept)
                dc.SubmitChanges();

            //if (seats == m_Seats)
            this.DialogResult = DialogResult.OK;
            //else
            //{
            //    m_Seats.Remove(m_Seat);
            //    dgvChain_show();
            //    dgvChain.CurrentCell = null;
            //    chain = true;
            //    dgvExpense_show();
            //    setStatus();
            //}
        }

        //手工打折
        private void discountTool_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(LogIn.connectionString);
            Employee oper = null;
            if (BathClass.getAuthority(dc, LogIn.m_User, "手工打折"))
                oper = LogIn.m_User;

            if (oper == null)
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;

                if (BathClass.getAuthority(dc, inputEmployee.employee, "手工打折"))
                    oper = inputEmployee.employee;
                else
                {
                    BathClass.printErrorMsg("没有手工打折权限");
                    return;
                }
            }
            if (oper == null)
                return;
            

            InputNumber inputNumberForm = new InputNumber("输入折扣率(<10)");
            if (inputNumberForm.ShowDialog() != DialogResult.OK)
                return;

            double number = inputNumberForm.number;
            double discountRate = 0;
            if (number < 1)
                discountRate = number;
            else
                discountRate = number / 10.0;
            var os = dc.Orders.Where(x => m_Seats.Select(y => y.systemId).Contains(x.systemId) && x.deleteEmployee == null);
            foreach (Orders o in os)
                o.money = Math.Round(o.money * discountRate);

            foreach (HotelRoom seat in m_Seats)
            {
                seat.discount = inputNumberForm.number;
                seat.discountEmployee = oper.id;
            }

            Operation op = new Operation();
            op.employee = oper.name;
            op.seat = string.Join("|", m_Seats.Select(y => y.text).ToArray());
            op.openEmployee = string.Join("|", m_Seats.Select(y => y.openEmployee).ToArray());
            //op.openTime = seat.openTime;
            op.explain = "手工打折";
            op.note1 = number.ToString();
            op.note2 = string.Join("|", m_Seats.Select(y => y.systemId).ToArray());
            op.opTime = BathClass.Now(LogIn.connectionString);
            dc.Operation.InsertOnSubmit(op);

            dc.SubmitChanges();
            dgvExpense_show(dc);
            setStatus(dc);
        }

        //签字免单
        private void freeTool_Click(object sender, EventArgs e)
        {
            SignForFreeForm inputSerForm = new SignForFreeForm();
            if (inputSerForm.ShowDialog() != DialogResult.OK)
                return;

            var dc_new = new BathDBDataContext(LogIn.connectionString);
            Account account = new Account();
            insert_account(dc_new, ref account, inputSerForm.signature);

            set_order_paid(dc_new, account);
            update_seat_room(dc_new);
            dc_new.SubmitChanges();
            setStatus(dc_new);

            if (printBill)
                printTool_Click(dc_new, "结账单");
            if (printStubBill)
                printTool_Click(dc_new, "存根单");
            if (printShoe)
            {
                List<string> seat_texts = new List<string>();
                var ts = account.text.Split('|').ToList();
                foreach (var tx in ts)
                {
                    var s = m_Seats.FirstOrDefault(x => x.text == tx);
                    if (s == null) continue;
                    int i = m_Seats.IndexOf(s);
                    if (dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
                        seat_texts.Add(tx);
                }
                PrintShoeMsg.Print_DataGridView(seat_texts, account.payEmployee, account.payTime.ToString(), companyName);
            }

            this.DialogResult = DialogResult.OK;
        }

        //插入账单数据库
        private void insert_account(BathDBDataContext dc, ref Account account, string name)
        {
            account.text = string.Join(";", m_Seats.OrderBy(x => x.text).Select(x => x.text).ToArray());
            account.systemId = string.Join(";", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            account.openTime = string.Join(";", m_Seats.OrderBy(x => x.text).Select(x => x.openTime.ToString()).ToArray());
            account.openEmployee = string.Join(";", m_Seats.OrderBy(x => x.text).Select(x => x.openEmployee).ToArray());
            account.payTime = GeneralClass.Now;
            account.payEmployee = LogIn.m_User.id;
            account.server = BathClass.get_rooms_expenses(dc, m_Seats, LogIn.connectionString);
            account.serverEmployee = name;
            account.macAddress = BathClass.getMacAddr_Local();

            dc.Account.InsertOnSubmit(account);
            dc.SubmitChanges();

        }

        //更新手牌，客房
        private void update_seat_room(BathDBDataContext dc)
        {
            foreach (HotelRoom seat in m_Seats)
            {
                dc.HotelRoom.FirstOrDefault(x => x.text == seat.text).status = 3;
                var room = dc.Room.FirstOrDefault(x => x.seat == seat.text);
                if (room != null)
                    room.status = "等待清洁";
            }
        }

        //修改订单数据库的paid属性
        private void set_order_paid(BathDBDataContext dc, Account account)
        {
            foreach (HotelRoom seat in m_Seats)
            {
                var orderList = dc.Orders.Where(x => x.systemId == seat.systemId && !x.paid);
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
                    dc.HisOrders.InsertOnSubmit(ho);
                    dc.Orders.DeleteOnSubmit(order);
                }
            }
        }

        //宾客转账
        private void transferTool_Click(object sender, EventArgs e)
        {
            InputSeatForm seatForm = new InputSeatForm(2);
            if (seatForm.ShowDialog() != DialogResult.OK)
                return;

            if (m_Seats.FirstOrDefault(x => x.text == seatForm.m_Seat.text) != null)
            {
                BathClass.printErrorMsg("转账手牌中已经包含该手牌，请重新输入");
                return;
            }

            var dc = new BathDBDataContext(LogIn.connectionString);
            string systemId = seatForm.m_Seat.systemId;
            foreach (HotelRoom s in m_Seats)
            {

                var orderList = dc.Orders.Where(x => x.systemId == s.systemId && x.deleteEmployee == null && !x.paid);
                foreach (Orders order in orderList)
                {
                    order.systemId = systemId;
                    if (order.priceType == "每小时")
                    {
                        order.priceType = "停止消费";
                        order.money = order.money * Math.Ceiling((GeneralClass.Now - order.inputTime).TotalHours);
                    }
                    order.stopTiming = true;
                }
                dc.HotelRoom.FirstOrDefault(x=>x.text==s.text).status = 3;
            }
            dc.SubmitChanges();

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");
            PrintSeatBill.Print_DataGridView(m_Seats, "转账确认单", dgvExpense, printCols, moneyPayable.Text, companyName);
            this.DialogResult = DialogResult.OK;
        }

        //打印账单
        private void printTool_Click(BathDBDataContext dc, string title)
        {
            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var ids = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            var act = dc.Account.Where(x => x.systemId == ids && x.abandon == null).
                OrderByDescending(x => x.payTime).FirstOrDefault();
            PrintBill.Print_DataGridView(act, title, dgvExpense, printCols, companyName);
        }

        //会员打折
        private void memberTool_Click(object sender, EventArgs e)
        {
            double money_pre = Convert.ToDouble(moneyPayable.Text);
            MemberPromotionOptionForm memberPromotionOptionForm = new MemberPromotionOptionForm(m_Seats);
            if (memberPromotionOptionForm.ShowDialog() != DialogResult.OK)
                return;

            var dc = new BathDBDataContext(LogIn.connectionString);
            m_Member = memberPromotionOptionForm.m_Member;
            double money = BathClass.get_rooms_expenses(dc, m_Seats, LogIn.connectionString);
            moneyPayable.Text = money.ToString();
            discount_money = money_pre - money;

            dgvExpense_show(dc);
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
                case Keys.Enter:
                    btnReload_Click(null, null);
                    break;
                case Keys.Space:
                    seatTimer_Elapsed();
                    break;
                case Keys.Delete:
                    btnDel_Click(null, null);
                    break;
                case Keys.F1:
                case Keys.Add:
                    payTool_Click(null, null);
                    break;
                case Keys.F2:
                case Keys.Subtract:
                    discountTool_Click(null, null);
                    break;
                case Keys.F3:
                    freeTool_Click(null, null);
                    break;
                case Keys.F4:
                    transferTool_Click(null, null);
                    break;
                case Keys.F6:
                case Keys.Decimal:
                    memberTool_Click(null, null);
                    break;
                case Keys.F8:
                    toolOrder_Click(null, null);
                    break;
                case Keys.F9:
                    //toolKeep_Click(null, null);
                    break;
                case Keys.F10:
                    toolReturn_Click(null, null);
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "0";
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "1";
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "2";
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "3";
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "4";
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "5";
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "6";
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "7";
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "8";
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "9";
                    break;
                case Keys.Back:
                    if (seatText.Text != "" && !seatText.ContainsFocus)
                        seatText.Text = seatText.Text.Substring(0, seatText.Text.Length - 1);
                    break;
                default:
                    break;
            }
        }

        //载入消费
        private void btnReload_Click(object sender, EventArgs e)
        {
            var dc_new = new BathDBDataContext(LogIn.connectionString);
            if (seatText.Text != "")
            {
                m_Seat = dc_new.HotelRoom.FirstOrDefault(x => x.text == seatText.Text || x.oId == seatText.Text);
                seatText.Text = "";

                if (m_Seat == null || m_Seat.status != 2 && m_Seat.status != 6 && m_Seat.status != 7 && m_Seat.status != 8)
                {
                    BathClass.printErrorMsg("该手牌不在使用中，不能结账!");
                    return;
                }
                if (m_Seats.FirstOrDefault(x=>x.text==m_Seat.text) == null)
                {
                    if (m_Seat.note != null)
                        BathClass.printInformation(m_Seat.note);
                    string t = Convert.ToDateTime(m_Seat.openTime).ToShortTimeString();
                    dgvChain.Rows.Add();
                    dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[0].Value = m_Seat.text;
                    dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[1].Value = use_oyd;
                    dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[2].Value = t;

                    order_guoye(m_Seat, dc_new);
                    m_Seats.Add(m_Seat);
                    m_Seat.paying = true;
                    dc_new.SubmitChanges();

                    //dgvChain_show();
                    dgvChain.CurrentCell = null;
                    //chain = true;
                    dgvExpense_show(dc_new);
                    setStatus(dc_new);
                }
            }
        }

        //发送消息给鞋部
        private void sendMessageToShoes(Account account)
        {
            var dc_new = new BathDBDataContext(LogIn.connectionString);
            var op = dc_new.Options.FirstOrDefault();
            if (op == null)
                return;

            var q = op.启用鞋部;
            if (!Convert.ToBoolean(q))
                return;

            ShoeMsg msg = new ShoeMsg();
            msg.text = account.text;
            msg.payEmployee = account.payEmployee;
            msg.payTime = account.payTime;
            msg.processed = false;
            dc_new.ShoeMsg.InsertOnSubmit(msg);
            dc_new.SubmitChanges();
        }

        //删除手牌
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvChain.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选择手牌!");
                return;
            }

            string text = dgvChain.CurrentRow.Cells[0].Value.ToString();

            int i = m_Seats.IndexOf(m_Seats.FirstOrDefault(x => x.text == text));
            if (use_oyd && dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
            {
                BathClass.printErrorMsg("已经刷手牌，不能删除!");
                return;
            }
            dgvChain.Rows.RemoveAt(i);
            m_Seats.Remove(m_Seats.FirstOrDefault(x => x.text == text));

            var dc_new = new BathDBDataContext(LogIn.connectionString);
            dc_new.HotelRoom.FirstOrDefault(x => x.text == text).paying = false;
            dc_new.SubmitChanges();

            //dgvChain_show();
            dgvChain.CurrentCell = null;
            //chain = true;
            dgvExpense_show(dc_new);
            setStatus(dc_new);
        }

        //消费录入
        private void toolOrder_Click(object sender, EventArgs e)
        {
            if (m_Seat == null)
            {
                BathClass.printErrorMsg("先选择手牌!");
                return;
            }

            var dc = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(dc, LogIn.m_User, "完整点单") &&
                !BathClass.getAuthority(dc, LogIn.m_User, "可见本人点单"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            var orderForm = new HotelRoomOrderForm(m_Seat);
            orderForm.ShowDialog();
            dgvExpense_show(dc);
            setStatus(dc);
        }

        //留牌操作 F9
        //private void toolKeep_Click(object sender, EventArgs e)
        //{
        //    List<int> sLst = new List<int>();
        //    sLst.Add(2);
        //    sLst.Add(6);
        //    sLst.Add(7);
        //    InputSeatForm inputseatForm = new InputSeatForm(sLst);
        //    if (inputseatForm.ShowDialog() != DialogResult.OK)
        //        return;
            

        //    var seat = inputseatForm.m_Seat;
        //    if (m_Seats.FirstOrDefault(x=>x.text==seat.text) == null)
        //    {
        //        m_Seats.Add(seat);
        //        var dc_new = new BathDBDataContext(LogIn.connectionString);
        //        dc_new.HotelRoom.FirstOrDefault(x => x.text == inputseatForm.m_Seat.text).paying = false;
        //        dc_new.SubmitChanges();
        //    }
            
        //    dgvChain_show();

        //    dgvChain.CurrentCell = null;
        //    //chain = true;

        //    //dgvExpense_show();
        //    //setStatus();
        //}

        //联牌账务
        //private void btnChain_Click(object sender, EventArgs e)
        //{
        //    //chain = true;
        //    dgvChain.CurrentCell = null;
        //    dgvExpense_show();
        //    setStatus();
        //}

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

            Employee del_employee;
            if (BathClass.getAuthority(dc, LogIn.m_User, "退单"))
                del_employee = LogIn.m_User;
            else if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;
            else if (!BathClass.getAuthority(dc, inputServerForm.employee, "退单"))
            {
                BathClass.printErrorMsg("没有退单权限");
                return;
            }
            else
                del_employee = inputServerForm.employee;

            order.deleteEmployee = del_employee.id.ToString();

            Employee employee = dc.Employee.FirstOrDefault(x => x.id.ToString() == order.technician);
            if (employee != null)
                employee.status = "空闲";

            dc.SubmitChanges();

            find_combo(dc, order);
            dgvExpense_show(dc);
            setStatus(dc);
        }

        private void find_combo(BathDBDataContext dc, Orders theOrder)
        {
            dc.Orders.DeleteAllOnSubmit(dc.Orders.Where(x => x.systemId == theOrder.systemId && x.menu.Contains("套餐")));
            var orders = dc.Orders.Where(x => x.systemId == theOrder.systemId && x.deleteEmployee == null);
            orders = orders.Where(x => !x.inputEmployee.Contains("电脑"));
            foreach (Orders tmp_order in orders)
            {
                tmp_order.comboId = null;
            }
            dc.SubmitChanges();

            var order_menus = orders.Where(x => x.comboId == null).Select(x => x.menu);
            var menus = dc.Menu;
            var comboList = dc.Combo.OrderByDescending(x => x.originPrice - x.price);
            foreach (Combo combo in comboList)
            {
                List<int> menuIds = BathClass.disAssemble(combo.menuIds);
                var combo_menus = menus.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
                if (combo_menus.All(x => order_menus.Any(y => y == x)))
                {
                    foreach (var combo_menu in combo_menus)
                    {
                        var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                        tmp_order.comboId = combo.id;
                        if (combo.priceType == "免项目")
                        {
                            var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                            var freeMenus = dc.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                            if (freeMenus.Contains(tmp_order.menu))
                                tmp_order.money = 0;
                        }
                    }
                    if (combo.priceType == "减金额")
                    {
                        Orders comboOrder = new Orders();
                        comboOrder.menu = "套餐" + combo.id.ToString() + "优惠";
                        comboOrder.text = theOrder.text;
                        comboOrder.systemId = theOrder.systemId;
                        comboOrder.number = 1;
                        comboOrder.inputTime = GeneralClass.Now;
                        comboOrder.inputEmployee = "套餐";
                        comboOrder.paid = false;
                        comboOrder.comboId = combo.id;
                        comboOrder.departmentId = 1;
                        comboOrder.money = Convert.ToDouble(combo.price) - combo.originPrice;

                        dc.Orders.InsertOnSubmit(comboOrder);
                    }
                    dc.SubmitChanges();
                }
            }
        }

        private void seatText_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void toolMember_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm(m_Seat);
            memberForm.ShowDialog();
            //dgvExpense_show();
            //setStatus();
        }

        private void SeatExpenseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var dc = new BathDBDataContext(LogIn.connectionString);
            var lockSeat = BathClass.ToBool(dc.Options.FirstOrDefault().结账未打单锁定手牌);

            for (int i = 0; i < m_Seats.Count; i++ )
            {
                var s_new = dc.HotelRoom.FirstOrDefault(x => x.text == m_Seats[i].text);
                s_new.paying = false;
                if (lockSeat && s_new.status != 3 && s_new.status != 8 && dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
                {
                    s_new.status = 4;
                }
            }
            dc.SubmitChanges();
        }

        private void seatText_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void dgvChain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChain.CurrentRow == null)
                return;

            string id = dgvChain.CurrentRow.Cells[0].Value.ToString();
            m_Seat = m_Seats.FirstOrDefault(x => x.text == id);
            //if (!use_oyd)
            //{
            //    if (dgvChain.CurrentCell.ColumnIndex != 1)
            //        chain = false;
            //    else
            //        chain = true;

            //    setStatus();
            //    dgvExpense_show();
            //}
        }

        //预打账单
        private void toolPreprint_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(LogIn.connectionString);

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var ids = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            var act = dc.Account.Where(x => x.systemId == ids && x.abandon == null).
                OrderByDescending(x => x.payTime).FirstOrDefault();
            PrePrintBill.Print_DataGridView(moneyPayable.Text,m_Seats, "预打账单", dgvExpense, printCols, companyName);
        }

        //加收过夜费
        private void order_guoye(HotelRoom seat, BathDBDataContext dc)
        {
            if (!BathClass.ToBool(m_Options.自动加收过夜费))
                return;

            if (dc.Orders.FirstOrDefault(x=>x.systemId==seat.systemId && x.menu=="过夜费") != null ||
                dc.HisOrders.FirstOrDefault(x => x.systemId == seat.systemId && x.menu == "过夜费") != null)
                return;

            var m_OverMenu = dc.Menu.FirstOrDefault(x => x.name == "过夜费");
            if (m_OverMenu == null)
                return;

            DateTime now = DateTime.Now;
            string year = now.Year.ToString();
            string month = now.Month.ToString();
            string day = now.Day.ToString();
            string date = year+"-"+month+"-"+day+" ";
            string time = ":00:00";

            DateTime st = DateTime.Parse(date + m_Options.过夜费起点 + time);
            DateTime et = DateTime.Parse(date + m_Options.过夜费终点 + time);

            DateTime open_time = seat.openTime.Value;
            if ((now >= et && open_time >= et) || (open_time <= st && now <= st))
                return;

            Orders order = new Orders();
            order.menu = m_OverMenu.name;
            order.text = seat.text;
            order.systemId = seat.systemId;
            order.number = 1;
            order.money = m_OverMenu.price;
            order.inputTime = now;
            order.inputEmployee = "过夜费";
            order.departmentId = 1;
            order.paid = false;
            dc.Orders.InsertOnSubmit(order);
            dc.SubmitChanges();
            find_combo(dc, order);
        }
    }
}
