using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using YouSoftBathGeneralClass;
using System.Timers;
using System.Threading;

using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class MainWindow : Form
    {
        //成员变量
        //private BathDBDataContext db = null;
        private HotelRoom m_Seat = null;

        private List<int> all_seats;//手牌状态
        private List<int> all_seats_id;//手牌id

        private Thread m_thread;//刷新线程

        //手牌大小控制
        private static int btn_size = 45;
        private static int btn_space = 13;

        public static bool seatLock;//启用手牌锁
        public static string lock_type;

        //构造函数
        public MainWindow()
        {
            InitializeComponent();
        }

        //对话框载入
        private void ReceptionSeatForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var ops = db.Options.FirstOrDefault();
            seatLock = BathClass.ToBool(ops.启用手牌锁);
            lock_type = ops.手牌锁类型;
            tSeat.Visible = !seatLock;

            this.Text = "咱家店小二-前台系统V2.1 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户: " + LogIn.m_User.id + "   " + LogIn.m_User.name;

            createSeat(db, seatPanel);
            setStatus();

            all_seats = db.HotelRoom.Select(x => x.status).ToList();
            all_seats_id = db.HotelRoom.Select(x => x.id).ToList();

            m_thread = new Thread(new ThreadStart(update_seats_ui));
            m_thread.Start();
            //MessageBox.Show("fcuk");
        }

        //刷新线程
        private void update_seats_ui()
        {
            while (true)
            {
                try
                {
                    var db_new = new BathDBDataContext(LogIn.connectionString);
                    var seats_tmp = db_new.HotelRoom.Select(x => x.status).ToList();

                    bool changed = false;
                    for (int i = 0; i < seats_tmp.Count; i++)
                    {
                        if (seats_tmp[i] != all_seats[i])
                        {
                            var btn = seatPanel.Controls.Find(all_seats_id[i].ToString(), false).FirstOrDefault();
                            var btns = btn as Button;
                            btn_status(btns, seats_tmp[i]);
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        setStatus();
                        all_seats = seats_tmp;
                    }
                }
                catch
                {}
            }
        }

        //手牌线程
        private void seatTimer_Elapsed()
        {
            if (!seatLock)
            {
                BathClass.printErrorMsg("未启用手牌锁!");
                return;
            }

            if (lock_type == "欧亿达")
            {
                if (OYD.FKOPEN() != 1)
                    return;

                OYD.CH375SetTimeout(0, 5000, 5000);
                Thread.Sleep(500);
            }

            byte[] buff = new byte[200];

            int rt = -1;
            if (lock_type == "欧亿达")
            {
                rt = OYD.OYEDA_id(buff);
                Thread.Sleep(500);
            }
            else if (lock_type == "锦衣卫")
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

            /*var db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = db_new.Seat.FirstOrDefault(x => x.text == seat_text);
            if (seat == null)
            {
                BathClass.printErrorMsg("手牌" + seat_text + "不存在");
                return;
            }
            else if (seat.status == 2 || seat.status == 6 || seat.status == 7 || seat.status == 8)
            {
                if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                    (lock_type == "锦衣卫" && JYW.MD(buff) != 0))
                    return;

                SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat);
                seatExpenseForm.ShowDialog();
            }*/
        }


        private delegate void seatExpenseForm_show_Delegate(SeatExpenseForm seatExpenseForm);
        private delegate void watchSeatDelegate();

        //创建单个台位按钮
        private void createButton(int x, int y, HotelRoom table, Control sp)
        {
            Button btn = new Button();

            Single bf = 13F;
            int l = table.text.Length;
            if (l == 3)
                bf = 13F;
            else if (l == 4)
                bf = 10f;

            btn.Font = new Font("SimSun", bf);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = table.id.ToString();
            btn.Text = table.text;
            btn.Size = new System.Drawing.Size(btn_size, btn_size);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn.ContextMenuStrip = seatContext;
            btn.TabStop = false;
            btn.Click += new System.EventHandler(btn_Click);
            btn_status(btn, table.status);

            sp.Controls.Add(btn);
        }

        //生成按钮状态
        private void btn_status(Button btn, int status)
        {
            switch (status)
            {
                case 1://可用
                    btn.BackColor = Color.White;
                    break;
                case 2://正在使用
                    btn.BackColor = Color.Cyan;
                    break;
                case 3://已经结账
                    btn.BackColor = Color.Gray;
                    break;
                case 4://锁定
                    btn.BackColor = Color.Orange;
                    break;
                case 5://停用
                    btn.BackColor = Color.Red;
                    break;
                case 6://警告
                    btn.BackColor = Color.Yellow;
                    break;
                case 7://押金离场
                    btn.BackColor = Color.Violet;
                    break;
                case 8://重新结账
                    btn.BackColor = Color.CornflowerBlue;
                    break;
                default:
                    break;
            }
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var manuInput = BathClass.ToBool(db_new.Options.FirstOrDefault().允许手工输入手牌号结账);
            m_Seat = db_new.HotelRoom.FirstOrDefault(x => x.text == btn.Text);
            var mtype = db_new.HotelRoomType.FirstOrDefault(x => x.id == m_Seat.typeId);
            if (!manuInput && mtype.menuId != null)
            {
                BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                return;
            }

            switch (m_Seat.status)
            {
                case 1://可用
                case 3://已经结账
                    break;
                case 2://正在使用
                case 6://警告
                case 7://押金离场
                case 8://重新结账
                    if (m_Seat.note != null && m_Seat.note != "")
                        BathClass.printInformation(m_Seat.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(m_Seat);
                    seatExpenseForm.ShowDialog();
                    break;
                case 4://锁定
                    break;
                case 5://停用
                    BathClass.printErrorMsg("台位已经停用!");
                    break;
                default:
                    break;
            }
        }

        //创建台位
        private void createSeat(BathDBDataContext dc, Control sp)
        {
            var seats = dc.HotelRoom.OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
            int tableCountSp = sp.Controls.Count;
            int tableCount = seats.Count;

            Size clientSize = seatPanel.Size;
            int nR = (clientSize.Width-btn_space) / (btn_size + btn_space);
            int theRow = tableCountSp / nR;
            int theCol = tableCountSp - theRow * nR;

            int theCount = tableCountSp;
            //增多
            while (theCount < tableCount)
            {
                while (theCol < nR && theCount < tableCount)
                {
                    if (theCount != 0 && seats[theCount].typeId != seats[theCount - 1].typeId)
                    {
                        theCol = 0;
                        theRow++;
                    }
                    int x = theCol * btn_size + btn_space * (theCol + 1);
                    int y = theRow * btn_size + btn_space * (theRow + 1);
                    createButton(x, y, seats[theCount], sp);

                    theCount++;
                    theCol++;
                }
                theCol = 0;
                theRow++;
            }

        }

        //F6开牌
        private void tool_open_seat()
        {
            if (tSeat.Text == "")
                return;

            string text = tSeat.Text;
            tSeat.Text = "";
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var manuInput = BathClass.ToBool(db_new.Options.FirstOrDefault().允许手工输入手牌号结账);

            var seat1 = db_new.HotelRoom.FirstOrDefault(x => x.text == text);
            var seat2 = db_new.HotelRoom.FirstOrDefault(x => x.oId == text);
            if (seat1 == null && seat2 == null)
            {
                BathClass.printErrorMsg("手牌不可用!");
                return;
            }

            if (seat1 != null)
            {
                var mtype = db_new.HotelRoomType.FirstOrDefault(x => x.id == seat1.typeId);
                if (!manuInput && mtype.menuId != null)
                {
                    BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                    return;
                }
                int status = seat1.status;
                if (status == 2 || status == 6 || status == 7 || status == 8)
                {
                    if (seat1.note != null && seat1.note != "")
                        BathClass.printInformation(seat1.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat1);
                    seatExpenseForm.ShowDialog();
                }
                else if (status == 4)
                    BathClass.printErrorMsg("台位已经锁定!");
                else if (status == 5)
                    BathClass.printErrorMsg("台位已经停用!");
            }
            else if (seat2 != null)
            {
                var mtype = db_new.HotelRoomType.FirstOrDefault(x => x.id == seat2.typeId);
                if (!manuInput && mtype.menuId != null)
                {
                    BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                    return;
                }

                int status = seat2.status;
                if (status == 2 || status == 6 || status == 7 || status == 8)
                {
                    if (seat2.note != null && seat2.note != "")
                        BathClass.printInformation(seat2.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat2);
                    seatExpenseForm.ShowDialog();
                }
                else if (status == 4)
                    BathClass.printErrorMsg("台位已经锁定!");
                else if (status == 5)
                    BathClass.printErrorMsg("台位已经停用!");
            }
            
        }

        //设置状态信息
        private void setStatus()
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "营业信息查看"))
            {
                statusStrip2.Visible = false;
                return;
            }

            seatTotal.Text = db_new.HotelRoom.Count().ToString();
            seatAvi.Text = db_new.HotelRoom.Where(x => x.status == 1).Count().ToString();

            DateTime st = DateTime.Parse("2013-1-1 00:00:00");
            if (db_new.ClearTable.Count() != 0)
            {
                st = db_new.ClearTable.ToList().Last().clearTime;
            }
            int count = 0;
            double pm = BathClass.get_paid_expense(db_new, st, ref count);
            seatPaid.Text = count.ToString();
            moneyPaid.Text = pm.ToString();

            seatUnpaid.Text = db_new.HotelRoom.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status == 8).Count().ToString();
            double upm = BathClass.get_unpaid_expense(db_new, LogIn.connectionString);
            moneyUnpaid.Text = upm.ToString();

            moneyTotal.Text = (pm + upm).ToString();
        }

        //消费录入
        private void orderTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "完整点单") &&
                !BathClass.getAuthority(db_new, LogIn.m_User, "可见本人点单"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            List<int> sLst = new List<int>();
            sLst.Add(2);
            sLst.Add(6);

            var inputSeatForm = new InputRoomForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            var seat = db_new.HotelRoom.FirstOrDefault(x => x.text == inputSeatForm.m_Seat.text);
            seat.ordering = true;
            db_new.SubmitChanges();
            
            var orderForm = new HotelRoomOrderForm(inputSeatForm.m_Seat);
            orderForm.ShowDialog();

            seat.ordering = false;
            db_new.SubmitChanges();
        }

        //客房
        private void btnRoom_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "包房管理"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            if (BathClass.ToBool(db_new.Options.FirstOrDefault().启用客房面板))
            {
                RoomViewForm rvForm = new RoomViewForm();
                rvForm.ShowDialog();
            }
            else
            {
                var form = new CabViewForm();
                form.ShowDialog();
            }
            RoomViewForm rmvForm = new RoomViewForm();
            rmvForm.ShowDialog();
        }

        //收银报表
        private void btnCashierTable_Click(object sender, EventArgs e)
        {
            TableCashierForm tableCashierForm = new TableCashierForm();
            tableCashierForm.ShowDialog();
        }

        //押金离场
        private void btnDeposit_Click(object sender, EventArgs e)
        {
            List<int> s = new List<int>();
            s.Add(2);
            s.Add(7);
            var inputSeatForm = new InputRoomForm(s);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            var seat = inputSeatForm.m_Seat;
            if (seat.status == 2)
            {
                DepositForm depositForm = new DepositForm(inputSeatForm.m_Seat);
                depositForm.ShowDialog();
            }
            else if (seat.status == 7 && BathClass.printAskMsg("是否取消押金离场状态")==DialogResult.Yes)
            {
                var db_new = new BathDBDataContext(LogIn.connectionString);
                var seat_new = db_new.HotelRoom.FirstOrDefault(x => x.text == seat.text);
                seat_new.status = 2;
                seat_new.note = null;
                db_new.SubmitChanges();
            }
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("是否退出?") == DialogResult.Yes)
                this.Close();
        }

        //会员管理
        private void toolMember_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm();
            memberForm.ShowDialog();
        }

        //预打账单
        private void toolPreprint_Click(object sender, EventArgs e)
        {
            PreprintBillForm preprintBillForm = new PreprintBillForm();
            preprintBillForm.ShowDialog();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (BathClass.printAskMsg("是否退出?") == DialogResult.Yes)
                        this.Close();
                    break;
                case Keys.Enter:
                    tool_open_seat();
                    break;
                case Keys.F3:
                    payTool_Click(null, null);
                    break;
                case Keys.Space:
                    if (seatLock)
                        seatTimer_Elapsed();
                    break;
                case Keys.F1:
                    toolTech_Click(null, null);
                    break;
                case Keys.F2:
                    orderTool_Click(null, null);
                    break;
                case Keys.F4:
                    btnRoom_Click(null, null);
                    break;
                case Keys.F5:
                    btnCashierTable_Click(null, null);
                    break;
                case Keys.F6:
                    btnDeposit_Click(null, null);
                    break;
                case Keys.F7:
                    toolMember_Click(null, null);
                    break;
                case Keys.F8:
                    toolPreprint_Click(null, null);
                    break;
                case Keys.F9:
                    toolAllOrder_Click(null, null);
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "0";
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "1";
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "2";
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "3";
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "4";
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "5";
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "6";
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "7";
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "8";
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    if (!tSeat.TextBox.ContainsFocus)
                        tSeat.Text += "9";
                    break;
                case Keys.Back:
                    if (tSeat.Text != "" && !tSeat.TextBox.ContainsFocus)
                    {
                        tSeat.Text = tSeat.Text.Substring(0, tSeat.Text.Length - 1);
                    }
                    break;
                default:
                    break;
            }
        }

        #region 右键

        //获取右键点击的台位
        private HotelRoom getContextSenderSeat(BathDBDataContext db_new, object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip cmenu = item.GetCurrentParent() as ContextMenuStrip;
            Button bt = cmenu.SourceControl as Button;
            return db_new.HotelRoom.FirstOrDefault(x => x.text == bt.Text);
        }

        //更换手牌
        private void CtxChangeSeat_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.employee, "更换手牌"))
            {
                BathClass.printErrorMsg(inputEmployee.employee.id + "不具有更换手牌操作权限!");
                return;
            }

            if (seat.status != 2)
            {
                BathClass.printErrorMsg("该手牌目前不在使用中，不能换台!");
                return;
            }

            List<int> sLst = new List<int>();
            sLst.Add(1);
            sLst.Add(3);
            var inputSeatForm = new InputRoomForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            HotelRoom newSeat = db_new.HotelRoom.FirstOrDefault(x => x == inputSeatForm.m_Seat);
            var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            foreach (Orders order in orders)
                order.text = newSeat.text;

            newSeat.systemId = seat.systemId;
            newSeat.name = seat.name;
            newSeat.population = seat.population;
            newSeat.openTime = GeneralClass.Now;
            newSeat.openEmployee = LogIn.m_User.name;
            newSeat.phone = seat.phone;
            newSeat.chainId = seat.chainId;
            newSeat.status = seat.status;
            newSeat.note = seat.note;
            newSeat.ordering = seat.ordering;

            BathClass.reset_seat(seat);
            db_new.SubmitChanges();
        }

        //取消开牌
        private void CtxCancelOpen_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.employee, "取消开牌"))
            {
                BathClass.printErrorMsg(inputEmployee.employee.id + "不具有取消开台权限!");
                return;
            }

            if (seat.status != 2)
            {
                BathClass.printErrorMsg("该台位不在使用中，不能取消开台!");
                return;
            }

            var options = db_new.Options.FirstOrDefault();
            if (options != null)
            {
                var q = options.取消开牌时限;
                if (q != null && GeneralClass.Now-seat.openTime>=TimeSpan.Parse("00:"+q.ToString()+":00"))
                {
                    BathClass.printErrorMsg("已超过取消开牌时限！");
                    return;
                }
            }
            var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            if (orders.Count() > 2)
            {
                BathClass.printErrorMsg("已经点单，不能取消开台");
                return;
            }

            if (BathClass.printAskMsg("确认取消开台?") != DialogResult.Yes)
                return;

            Operation op = new Operation();
            op.employee = inputEmployee.employee.id;
            op.seat = seat.text;
            op.openEmployee = seat.openEmployee;
            op.openTime = seat.openTime;
            op.explain = "取消开牌";
            op.opTime = BathClass.Now(LogIn.connectionString);
            db_new.Operation.InsertOnSubmit(op);

            db_new.Orders.DeleteAllOnSubmit(db_new.Orders.Where(x => x.systemId == seat.systemId));
            BathClass.reset_seat(seat);
            db_new.SubmitChanges();
        }

        //挂失手牌
        private void CtxLooseSeat_Click(object sender, EventArgs e)
        {

        }

        //添加备注
        private void CtxAddNote_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "添加备注"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            var seat = getContextSenderSeat(db_new, sender);
            if (seat.status != 2 && seat.status != 6)
            {
                BathClass.printErrorMsg("手牌未使用，不能添加备注");
                return;
            }

            NoteForm noteForm = new NoteForm();
            if (noteForm.ShowDialog() != DialogResult.OK)
                return;

            seat.note = noteForm.note;
            db_new.SubmitChanges();
        }

        //锁定解锁
        private void CtxLock_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);

            if (BathClass.getAuthority(db_new, LogIn.m_User, "锁定解锁"))
            {
                lock_unlock(seat, db_new, LogIn.m_User);
                return;
            }

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.employee, "锁定解锁"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            lock_unlock(seat, db_new, inputEmployee.employee);
        }

        private void lock_unlock(HotelRoom seat, BathDBDataContext db_new, Employee employee)
        {
            if (seat.status == 1 || seat.status == 2)
                seat.status = 4;
            else if (seat.status == 4)
            {
                Operation op = new Operation();
                op.employee = employee.name;
                op.seat = seat.text;
                op.openEmployee = seat.openEmployee;
                op.openTime = seat.openTime;
                op.explain = "解锁手牌";
                op.opTime = BathClass.Now(LogIn.connectionString);
                db_new.Operation.InsertOnSubmit(op);

                if (seat.systemId == null)
                    seat.status = 1;
                else
                    seat.status = 2;
            }

            db_new.SubmitChanges();
        }

        //停用启用
        private void CtxSop_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.employee, "停用启用"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            if (seat.status == 2 || seat.status == 6 || seat.status == 7 || seat.status == 8)
            {
                BathClass.printErrorMsg("手牌正在使用不能停用");
                return;
            }
            else if (seat.status == 4)
            {
                BathClass.printErrorMsg("手牌已经锁定，不能停用");
                return;
            }
            else if (seat.status == 1 || seat.status == 3)
            {
                seat.status = 5;
            }
            else if (seat.status == 5)
            {
                seat.status = 1;
            }

            db_new.SubmitChanges();
        }

        //重新结账
        private void CtxRepay_Click(object sender, EventArgs e)
        {
            //TableCashierCheckForm tableCashierSummaryForm = new TableCashierCheckForm();
            //tableCashierSummaryForm.ShowDialog();

            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.employee, "重新结账"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            if (seat.status != 3)
            {
                BathClass.printErrorMsg("手牌未结账，不能重新结账");
                return;
            }

            RepayActListForm form = new RepayActListForm(seat, inputEmployee.employee);
            form.ShowDialog();

            /*var act = db_new.Account.FirstOrDefault(x => x.systemId.Contains(seat.systemId) && x.abandon == null);
            if (act == null)
            {
                if (BathClass.printAskMsg("手牌未结账，是否直接恢复手牌?") != DialogResult.Yes)
                    return;

                seat.status = 2;
                db_new.SubmitChanges();
                return;
            }
            var ids = act.systemId.Split('|');
            var seats = db_new.Seat.Where(x => ids.Contains(x.systemId));
            foreach (var s in seats)
            {
                if (s.status == 2 || s.status == 6 || s.status == 7)
                {
                    BathClass.printErrorMsg("已经重新开牌，请先更换手牌");
                    return;
                }
                s.status = 2;
                var orders = db_new.HisOrders.Where(x => x.systemId == s.systemId);
                foreach (var order in orders)
                {
                    var ho = new Orders();
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
                    ho.paid = false;
                    ho.accountId = order.accountId;
                    ho.billId = order.billId;
                    db_new.HisOrders.DeleteOnSubmit(order);
                    db_new.Orders.InsertOnSubmit(ho);
                }
            }
            act.abandon = inputEmployee.employee.id;
            var cc = db_new.CardCharge.Where(x => act.id.ToString() == x.CC_AccountNo);
            if (cc.Any())
                db_new.CardCharge.DeleteAllOnSubmit(cc);
            db_new.SubmitChanges();*/
        }

        private void CtxChain_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);
            //OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            //openSeatForm.ShowDialog();
        }

        //重打账单
        private void CtxReprint_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);

            if (seat.systemId == null || seat.status != 3)
            {
                BathClass.printErrorMsg("已经重新开牌，不能重打账单!");
                return;
            }

            var account = db_new.Account.FirstOrDefault(x => x.systemId.Contains(seat.systemId) && x.abandon == null);

            DataGridView dgv = new DataGridView();

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "手牌号";
            dgv.Columns.Add(col);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "项目名称";
            dgv.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "技师";
            dgv.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "单价";
            dgv.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "数量";
            dgv.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "金额";
            dgv.Columns.Add(col5);

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var co_name = db_new.Options.FirstOrDefault().companyName;
            if (account != null)
            {
                var ids = account.systemId.Split('|');
                var orders = db_new.HisOrders.Where(x => ids.Contains(x.systemId) && x.paid && x.deleteEmployee == null);
                foreach (var order in orders)
                {
                    string[] row = new string[6];
                    row[0] = order.text;
                    row[1] = order.menu;
                    row[2] = order.technician;

                    var menu = db_new.Menu.FirstOrDefault(x => x.name == order.menu);
                    if (menu != null)
                        row[3] = menu.price.ToString();

                    row[4] = order.number.ToString();
                    row[5] = order.money.ToString();
                    dgv.Rows.Add(row);
                }
                PrintBill.Print_DataGridView(account, "存根单", dgv, printCols, co_name);
            }
            else
            {
                var orders = db_new.Orders.Where(x => x.text == seat.text && !x.paid && x.deleteEmployee == null);
                foreach (var order in orders)
                {
                    string[] row = new string[6];
                    row[0] = order.text;
                    row[1] = order.menu;
                    row[2] = order.technician;

                    var menu = db_new.Menu.FirstOrDefault(x => x.name == order.menu);
                    if (menu != null)
                        row[3] = menu.price.ToString();

                    row[4] = order.number.ToString();
                    row[5] = order.money.ToString();
                    dgv.Rows.Add(row);
                }

                var money = BathClass.get_cur_orders_money(orders, LogIn.connectionString, BathClass.Now(LogIn.connectionString));
                List<HotelRoom> seats = new List<HotelRoom>();
                seats.Add(seat);
                PrintSeatBill.Print_DataGridView(seats, "转账确认单", dgv, printCols, money.ToString(), co_name);

            }
        }

        //右键解除警告
        private void unWarnTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);
            if (seat.status != 6)
                return;

            Employee op_user;
            if (BathClass.getAuthority(db_new, LogIn.m_User, "解除警告"))
                op_user = LogIn.m_User;
            else
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;
                if (BathClass.getAuthority(db_new, inputEmployee.employee, "解除警告"))
                    op_user = inputEmployee.employee;
                else
                {
                    BathClass.printErrorMsg("不具有权限!");
                    return;
                }
            }

            seat.status = 2;
            seat.unwarn = op_user.id;
            db_new.SubmitChanges();
        }

        //恢复转账
        private void undoTransferTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(db_new, sender);
            if (seat.status != 1 && seat.status != 3)
            {
                BathClass.printErrorMsg("手牌已经重新使用，请在该手牌结账后恢复转账");
                return;
            }

            var orders = db_new.Orders.Where(x => x.text == seat.text && !x.paid);
            if (orders.Any())
            {
                seat.status = 2;
                foreach (var order in orders)
                {
                    order.systemId = seat.systemId;
                }

                db_new.SubmitChanges();
            }
            else
            {
                BathClass.printErrorMsg("未检测到该手牌的转账账单!");
                return;
            }
        }
        #endregion

        //技师管理
        private void toolTech_Click(object sender, EventArgs e)
        {
            var db_new = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "技师管理"))
            {
                BathClass.printErrorMsg("不具有权限");
                return;
            }

            TechnicianSeclectForm technicianForm = new TechnicianSeclectForm();
            technicianForm.ShowDialog();
        }

        //录单汇总
        private void toolAllOrder_Click(object sender, EventArgs e)
        {
            var db_new = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "录单汇总"))
            {
                BathClass.printErrorMsg("不具有权限");
                return;
            }

            TableOrderTableForm orderTableForm = new TableOrderTableForm(LogIn.connectionString, LogIn.m_User);
            orderTableForm.ShowDialog();
        }

        private void toolPwd_Click(object sender, EventArgs e)
        {
            ModifyPwdForm pwdForm = new ModifyPwdForm();
            pwdForm.ShowDialog();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //    return;
            //else
            //{
            //    createSeat(seatPanel);
            //    setStatus();
            //}
        }

        private void toolWarn_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("确定预警?") != DialogResult.Yes)
                return;

            var dc_new = new BathDBDataContext(LogIn.connectionString);
            var warn = new RoomWarn();
            warn.msg = "警告";
            dc_new.RoomWarn.InsertOnSubmit(warn);
            dc_new.SubmitChanges();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            if (seatLock && lock_type == "锦衣卫")
                JYW.CloseReader();
        }

        private void tSeat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //结账
        private void payTool_Click(object sender, EventArgs e)
        {
            if (seatLock)
                seatTimer_Elapsed();
            else
                tool_open_seat();
        }
    }
}
