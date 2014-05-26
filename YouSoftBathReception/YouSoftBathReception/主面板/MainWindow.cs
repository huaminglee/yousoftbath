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
using YouSoftBathConstants;
using YouSoftBathFormClass;
using System.IO;
using YouSoftUtil;

namespace YouSoftBathReception
{
    public partial class MainWindow : Form
    {
        //成员变量
        private CSeat m_Seat = null;
        private Thread m_thread;//刷新线程
        public bool seatLock;//启用手牌锁
        private Thread m_thread_seatCard;//手牌锁线程
        private Thread m_thread_clearMemory;

        private bool auto_seat_card;
        public string lock_type;
        private bool use_pad = false;
        private DAO dao;

        //构造函数
        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(@".\img\main.jpg"))
                seatPanel.BackgroundImage = Image.FromFile(@".\img\main.jpg");
        }

        //对话框载入
        private void ReceptionSeatForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);

            seatLock = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用手牌锁, false);
            auto_seat_card = MConvert<bool>.ToTypeOrDefault(LogIn.options.自动感应手牌, false);
            use_pad = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);
            lock_type = LogIn.options.手牌锁类型;
            tSeat.Visible = !seatLock;

            this.Text = "咱家店小二-前台系统" + Constants.version + " 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户: " + LogIn.m_User.id + "   " + LogIn.m_User.name;

            var td = new Thread(new ThreadStart(initial_ui_thread));
            td.IsBackground = true;
            td.Start();

            //CFormCreate.createSeat(db, seatPanel, seatTab, new EventHandler(btn_Click), seatContext);
            //setStatus(db);

            m_thread = new Thread(new ThreadStart(update_seats_ui));
            m_thread.IsBackground = true;
            m_thread.Start();

            m_thread_clearMemory = new Thread(new ThreadStart(clear_Memory));
            m_thread_clearMemory.IsBackground = true;
            m_thread_clearMemory.Start();

            if (seatLock && auto_seat_card)
            {
                m_thread_seatCard = new Thread(new ThreadStart(seat_card_thread));
                m_thread_seatCard.IsBackground = true;
                m_thread_seatCard.Start();
            }
        }

        private void clear_Memory()
        {
            while (true)
            {
                try
                {
                    BathClass.ClearMemory();
                    Thread.Sleep(2 * 1000);
                }
                catch
                {
                }
            }
        }

        private void initial_ui_thread()
        {
            try
            {
                this.Invoke(new no_par_delegate(initial_ui), null);
            }
            catch (System.Exception e)
            {

            }
        }

        private delegate void no_par_delegate();
        private void initial_ui()
        {
            CFormCreate.createSeatByDao(dao, LogIn.options, seatPanel, seatTab, new EventHandler(btn_Click),new EventHandler(btn_MouseHover), seatContext, null);
            setStatus();
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            BathDBDataContext db = new BathDBDataContext(LogIn.connectionString);
            Button btn = (Button)sender;
            string tooltipmessage = "";
            string seatId = btn.Text;
            string roomNo = dao.get_seat_room(seatId);
            var openTime = db.Seat.FirstOrDefault(x => x.text == seatId).openTime;
            if (roomNo == "")
            {
                roomNo = "未开房!";
            }
            tooltipmessage = "   房间号:" + roomNo + "\n";
            tooltipmessage += "入场时间:" + openTime;
            toolTip1.SetToolTip(btn, tooltipmessage);     
        }

        //刷新线程
        private void update_seats_ui()
        {
            while (true)
            {
                try
                {
                    var seats = dao.get_seats("typeid in (select id from [SeatType] where department='桑拿部')");
                    if (seats == null || seats.Count == 0) continue;
                    var seats_id_tmp = seats.Select(x => x.id).ToList();
                    var seats_status_tmp = seats.Select(x => x.status).ToList();

                    bool changed = false;
                    for (int i = 0; i < seats_id_tmp.Count; i++)
                    {
                        var btn = (Button)seatPanel.Controls.Find(seats_id_tmp[i].ToString(), false).FirstOrDefault();
                        if (btn == null)
                        {
                            foreach (TabPage tp in seatTab.Controls)
                            {
                                btn = (Button)tp.Controls.Find(seats_id_tmp[i].ToString(), false).FirstOrDefault();
                                if (btn != null)
                                    break;
                            }
                        }

                        switch (seats_status_tmp[i])
                        {
                            case SeatStatus.AVILABLE://可用
                                if (btn.BackColor != Color.White)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.White });
                                    else
                                        btn.BackColor = Color.White;
                                }
                                break;
                            case SeatStatus.USING://正在使用
                                if (btn.BackColor != Color.Cyan)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Cyan });
                                    else
                                        btn.BackColor = Color.Cyan;
                                }
                                break;
                            case SeatStatus.PAIED://已经结账
                                if (btn.BackColor != Color.Gray)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Gray });
                                    else
                                        btn.BackColor = Color.Gray;
                                }
                                break;
                            case SeatStatus.LOCKING://锁定
                                if (btn.BackColor != Color.Orange)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Orange });
                                    else
                                        btn.BackColor = Color.Orange;
                                }
                                break;
                            case SeatStatus.STOPPED://停用
                                if (btn.BackColor != Color.Red)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Red });
                                    else
                                        btn.BackColor = Color.Red;
                                }
                                break;
                            case SeatStatus.WARNING://警告
                                if (btn.BackColor != Color.Yellow)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Yellow });
                                    else
                                        btn.BackColor = Color.Yellow;
                                }
                                break;
                            case SeatStatus.DEPOSITLEFT://押金离场
                                if (btn.BackColor != Color.Violet)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Violet });
                                    else
                                        btn.BackColor = Color.Violet;
                                }
                                break;
                            case SeatStatus.REPAIED://重新结账
                                if (btn.BackColor != Color.CornflowerBlue)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.CornflowerBlue });
                                    else
                                        btn.BackColor = Color.CornflowerBlue;
                                }
                                break;
                            case SeatStatus.RESERVE://预约客房
                                if (btn.BackColor != Color.SpringGreen)
                                {
                                    changed = true;
                                    if (this.InvokeRequired)
                                        this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.SpringGreen });
                                    else
                                        btn.BackColor = Color.SpringGreen;
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (changed)
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new no_par_delegate(setStatus), new object[] { });
                        else
                            setStatus();
                    }
                }
                catch
                {
                }
            }
        }
        private delegate void set_btn_color_delegate(Button btn, Color color);
        private void set_btn_color(Button btn, Color color)
        {
            btn.BackColor = color;
        }
        private delegate void delegate_show_seat_expense_form(CSeat seat);
        private delegate void delegate_print_msg(string msg);
        private void show_seat_expense_form(CSeat seat)
        {
            SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat, -1, seatLock, auto_seat_card, dao);
            seatExpenseForm.ShowDialog();
        }
        //手牌线程
        private void seat_card_thread()
        {
            while (true)
            {
                try
                {
                    if (lock_type == "欧亿达")
                    {
                        Thread.Sleep(500);
                        if (OYD.FKOPEN() != 1)
                            continue;

                        OYD.CH375SetTimeout(0, 5000, 5000);
                    }

                    byte[] buff = new byte[200];
                    string seat_text = "";

                    int rt = -1;
                    if (lock_type == "欧亿达")
                    {
                        Thread.Sleep(500);
                        rt = OYD.OYEDA_id(buff);
                    }
                    else if (lock_type == "锦衣卫")
                        rt = JYW.ReadID(buff);
                    else if (lock_type == "RF")
                        rt = RF.RF_RFID(ref seat_text);

                    if (rt != 0)
                        continue;

                    CSeat seat = null;
                    if (lock_type == "欧亿达")
                    {
                        seat_text = Encoding.Default.GetString(buff, 0, 20).Trim();
                        seat_text = seat_text.Substring(0, 16);
                        //seat = dao.get_seat("oId", seat_id);
                    }
                    else if (lock_type == "锦衣卫")
                    {
                        seat_text = BathClass.byteToHexStr(buff);
                        seat_text = seat_text.Substring(0, 16);
                        //seat = dao.get_seat("oId", seat_id);
                    }

                    seat = dao.get_seat("oId", seat_text);

                    if (seat == null)
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                    new object[] { "手牌" + seat_text + "不存在" });
                        else
                            BathClass.printErrorMsg("手牌" + seat_text + "不存在");
                        continue;
                    }
                    else if (seat.status == SeatStatus.USING || seat.status == SeatStatus.WARNING ||
                        seat.status == SeatStatus.DEPOSITLEFT || seat.status == SeatStatus.REPAIED)
                    {
                        if (lock_type == "欧亿达")
                            Thread.Sleep(500);

                        if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                            (lock_type == "锦衣卫" && JYW.MD(buff) != 0) ||
                            (lock_type == "RF" && RF.RF_MD() != 0))
                        {
                            continue;
                        }
                        if (seat.deposit != null)
                        {
                            if (this.InvokeRequired)
                                this.Invoke(new delegate_print_msg(BathClass.printInformation),
                                    new object[] { "交押金：" + seat.deposit.ToString() });
                            else
                                BathClass.printInformation("交押金：" + seat.deposit.ToString());
                        }

                        if (seat.note != null && seat.note != "")
                        {
                            if (this.InvokeRequired)
                                this.Invoke(new delegate_print_msg(BathClass.printInformation),
                                    new object[] { seat.note });
                            else
                                BathClass.printInformation(seat.note);
                        }

                        if (this.InvokeRequired)
                            this.Invoke(new delegate_show_seat_expense_form(show_seat_expense_form),
                                    new object[] { seat });
                        else
                            show_seat_expense_form(seat);
                        Thread.Sleep(500);
                    }
                    else if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.PAIED)
                    {
                        if ((lock_type == "欧亿达" && OYD.OYEDA_fk(buff) != 0) ||
                            (lock_type == "锦衣卫" && JYW.FK(buff) != 0) ||
                            (lock_type == "RF" && RF.RF_FK(seat_text) != 0))
                            continue;

                        open_one_seat(seat);

                        if (this.InvokeRequired)
                            this.Invoke(new delegate_show_seat_expense_form(open_seat),
                                    new object[] { seat });
                        else
                            open_seat(seat);
                        //open_seat(seat);
                    }
                }
                catch
                {
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            var manuInput = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号结账, false);
            var manuOpen = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号开牌, false);
            //m_Seat = db_new.Seat.FirstOrDefault(x => x.text == btn.Text);
            m_Seat = dao.get_seat("text", btn.Text);
            var mtype = dao.get_seattype("id", m_Seat.typeId);
            //var mtype = db_new.SeatType.FirstOrDefault(x => x.id == m_Seat.typeId);

            switch (m_Seat.status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                case SeatStatus.RESERVE://预定客房
                    if (!manuOpen && mtype.menuId != null)
                    {
                        BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                        return;
                    }
                    open_seat(m_Seat);
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.WARNING://警告
                case SeatStatus.DEPOSITLEFT://押金离场
                case SeatStatus.REPAIED://重新结账
                    if (mtype.department != "客房部" && !manuInput && mtype.menuId != null)
                    {
                        BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                        return;
                    }
                    if (m_Seat.deposit != null)
                        BathClass.printInformation("交押金：" + m_Seat.deposit.ToString());

                    if (m_Seat.note != null && m_Seat.note != "")
                        BathClass.printInformation(m_Seat.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(m_Seat, -1, seatLock, auto_seat_card, dao);
                    seatExpenseForm.Show();
                    break;
                case SeatStatus.LOCKING://锁定
                    break;
                case SeatStatus.STOPPED://停用
                    BathClass.printErrorMsg("台位已经停用!");
                    break;
                default:
                    break;
            }
        }
        //开单个牌，用于以刷卡方式
        private void open_one_seat(CSeat seat)
        {
            string systemId = dao.systemId();
            StringBuilder sb = new StringBuilder();
            if (seat.status == SeatStatus.PAIED)
            {
                sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
            }

            sb.Append("update [Seat] set openEmployee='" + LogIn.m_User.id + "',openTime=getdate(),systemId='");
            sb.Append(systemId + "',status=2 where text='" + seat.text + "' ");
            sb.Append("insert into [SystemIds](systemId) values('" + systemId + "') ");

            var menu = dao.get_seat_menu(seat.text);
            if (menu != null)
            {
                sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                sb.Append(@"values('" + menu.name + "','" + seat.text + "','" + systemId + "',1," + menu.price + ",getdate(),'");
                sb.Append(LogIn.m_User.id + "','False')");
            }
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("开牌失败，请重试!");
                return;
            }
        }
        //开牌
        private void open_seat(CSeat seat)
        {
            if (!dao.get_authority(LogIn.m_User, "开牌"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            OpenSeatForm openSeatForm = new OpenSeatForm(seat, true);
            openSeatForm.ShowDialog();
            //dgv_shoe_show();
        }

        //F6开牌
        private void tool_open_seat()
        {
            if (tSeat.Text == "")
                return;

            string text = tSeat.Text;
            tSeat.Text = "";
            var use_idCard = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用ID手牌锁, false);
            var manuInput = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号结账, false);
            var manuOpen = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号开牌, false);

            var seat1 = dao.get_seat("text", text);
            if (seat1 != null)
            {
                var mtype = dao.get_seattype("id", seat1.typeId);
                //var seatType = db_new.SeatType.FirstOrDefault(x => x.id == seat1.typeId);
                if (mtype.department == "客房部")
                {
                    var form = new OpenRoomForm(seat1);
                    form.ShowDialog();
                }
                else
                {
                    
                    SeatStatus status = seat1.status;
                    if (status == SeatStatus.USING || status == SeatStatus.WARNING ||
                        status == SeatStatus.DEPOSITLEFT || status == SeatStatus.REPAIED)
                    {
                        if (seat1.deposit != null)
                            BathClass.printInformation("交押金：" + seat1.deposit.ToString());

                        if (seat1.note != null && seat1.note != "")
                            BathClass.printInformation(seat1.note);

                        SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat1, -1, seatLock, auto_seat_card, dao);
                        seatExpenseForm.ShowDialog();
                    }
                    else if (status==SeatStatus.AVILABLE||status==SeatStatus.PAIED)
                    {
                        if (!manuOpen && mtype.menuId != null)
                        {
                            BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                            return;
                        }
                        open_seat(seat1);
                    }
                    else if (status == SeatStatus.LOCKING)
                        BathClass.printErrorMsg("台位已经锁定!");
                    else if (status == SeatStatus.STOPPED)
                        BathClass.printErrorMsg("台位已经停用!");
                }
            }
            else if (use_idCard)
            {
                var seat2 = dao.get_seat("oId", text);
                if (seat2 == null)
                {
                    BathClass.printErrorMsg("手牌不存在!");
                    return;
                }
                var mtype = dao.get_seattype("id", seat2.typeId);
                SeatStatus status = seat2.status;
                if (status == SeatStatus.USING || status == SeatStatus.WARNING ||
                            status == SeatStatus.DEPOSITLEFT || status == SeatStatus.REPAIED)
                {
                    if (MConvert<bool>.ToTypeOrDefault(seat2.ordering, false))
                    {
                        BathClass.printErrorMsg("正在录单!");
                        return;
                    }

                    if (seat2.deposit != null)
                        BathClass.printInformation("交押金：" + seat2.deposit.ToString());

                    if (seat2.note != null && seat2.note != "")
                        BathClass.printInformation(seat2.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat2, -1, seatLock, auto_seat_card, dao);
                    seatExpenseForm.ShowDialog();
                }
                else if (status==SeatStatus.AVILABLE || status==SeatStatus.PAIED)
                {
                    if (!manuOpen && mtype.menuId != null)
                    {
                        BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                        return;
                    }
                    open_seat(seat2);
                }
                else if (status == SeatStatus.LOCKING)
                    BathClass.printErrorMsg("台位已经锁定!");
                else if (status == SeatStatus.STOPPED)
                    BathClass.printErrorMsg("台位已经停用!");
            }
        }

        //设置状态信息
        private void setStatus()
        {
            if (!dao.get_authority(LogIn.m_User, "营业信息查看"))
            {
                statusStrip2.Visible = false;
                return;
            }

            seatTotal.Text = dao.get_seat_count(SeatStatus.INVALID).ToString();
            seatAvi.Text = dao.get_seat_count(SeatStatus.AVILABLE).ToString();
            //seatTotal.Text = db_new.Seat.Count().ToString();
            //seatAvi.Text = db_new.Seat.Where(x => x.status == 1).Count().ToString();

            DateTime? st = dao.get_last_clear_time();
            if (st == null)
                st = DateTime.Parse("2013-1-1 00:00:00");

            int count = 0;
            double pm = dao.get_paid_expense(st.Value, ref count);
            seatPaid.Text = count.ToString();
            moneyPaid.Text = pm.ToString();

            var status = new List<SeatStatus>();
            status.Add(SeatStatus.USING);
            status.Add(SeatStatus.WARNING);
            status.Add(SeatStatus.DEPOSITLEFT);
            status.Add(SeatStatus.REPAIED);
            seatUnpaid.Text = dao.get_seat_count(status).ToString();
            double upm = dao.get_unpaid_expense();
            moneyUnpaid.Text = upm.ToString();

            moneyTotal.Text = (pm + upm).ToString();
        }

        //消费录入
        private void orderTool_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "完整点单") &&
                !dao.get_authority(LogIn.m_User, "可见本人点单"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            List<int> sLst = new List<int>();
            sLst.Add(2);
            sLst.Add(6);

            InputSeatForm inputSeatForm = new InputSeatForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            OrderForm orderForm = new OrderForm(inputSeatForm.m_Seat, LogIn.m_User, LogIn.connectionString, false);
            orderForm.ShowDialog();
        }

        //客房
        private void btnRoom_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "包房管理"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            if (MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false))
            {
                RoomViewForm rvForm = new RoomViewForm();
                rvForm.ShowDialog();
            }
            else
            {
                var form = new CabViewForm();
                form.ShowDialog();
            }
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
            InputSeatForm inputSeatForm = new InputSeatForm(s);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            var seat = inputSeatForm.m_Seat;
            if (seat.status == SeatStatus.USING || seat.status == SeatStatus.DEPOSITLEFT)
            {
                DepositForm depositForm = new DepositForm(inputSeatForm.m_Seat);
                depositForm.ShowDialog();
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
                    toolChain_Click(null, null);
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
        private CSeat getContextSenderSeat(object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip cmenu = item.GetCurrentParent() as ContextMenuStrip;
            Button bt = cmenu.SourceControl as Button;

            return dao.get_seat("text", bt.Text);
        }

        //更换手牌
        private void CtxChangeSeat_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);

            Employee oper = null;
            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();

            if (dao.get_authority(LogIn.m_User, "更换手牌"))
            {
                oper = LogIn.m_User;
            }
            else if (inputEmployee.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            else
            {
                if (dao.get_authority(inputEmployee.employee, "更换手牌"))
                {
                    oper = inputEmployee.employee;
                }
                else
                {
                    BathClass.printErrorMsg(inputEmployee.employee.id + "不具有更换手牌操作权限!");
                    return;
                }
            }


            if (seat.status != SeatStatus.USING)
            {
                BathClass.printErrorMsg("该手牌目前不在使用中，不能换台!");
                return;
            }

            List<int> sLst = new List<int>();
            sLst.Add(1);
            sLst.Add(3);
            InputSeatForm inputSeatForm = new InputSeatForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"update [Orders] set text='" + inputSeatForm.m_Seat.text + "' where systemId='" + seat.systemId + "' ");
            sb.Append(@"update [Seat] set systemId='" + seat.systemId + "'");
            sb.Append(",openTime=getdate(),openEmployee='" + oper.id + "',chainId='" + seat.chainId);
            sb.Append("',status=" + (int)seat.status + ",ordering='False'");
            if (seat.name != null && seat.name != "")
                sb.Append(",name='" + seat.name + "'");

            if (seat.population != null)
                sb.Append(",population=" + seat.population);

            if (seat.note != null && seat.name != "")
                sb.Append(",note='" + seat.note + "'");

            sb.Append(" where id=" + inputSeatForm.m_Seat.id);
            sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("更换手牌失败，请重试！");
                return;
            }
        }

        //取消开牌
        private void CtxCancelOpen_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);
            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();

            if (dao.get_authority(LogIn.m_User, "取消开牌") ||
                (inputEmployee.ShowDialog() == DialogResult.OK &&
                dao.get_authority(inputEmployee.employee, "取消开牌")))
            {
                if (seat.status != SeatStatus.USING)
                {
                    BathClass.printErrorMsg("该台位不在使用中，不能取消开台!");
                    return;
                }

                var cancel_open_delay = LogIn.options.取消开牌时限;
                if (cancel_open_delay != null && (dao.Now() - seat.openTime.Value).TotalMinutes >= Convert.ToDouble(cancel_open_delay))
                {
                    BathClass.printErrorMsg("已超过取消开牌时限！");
                    return;
                }

                var orders = dao.get_orders("systemId='" + seat.systemId + "'");
                if (orders.Count > 2)
                {
                    BathClass.printErrorMsg("已经点单，不能取消开台");
                    return;
                }

                if (BathClass.printAskMsg("确认取消开台?") != DialogResult.Yes)
                    return;

                string user_id;
                if (inputEmployee.employee != null)
                    user_id = inputEmployee.employee.id;
                else
                    user_id = LogIn.m_User.id;

                StringBuilder sb = new StringBuilder();
                sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
                sb.Append(user_id + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime.ToString());
                sb.Append("','取消开牌',getdate())");
                
                sb.Append(@" delete from [Orders] where systemId='" + seat.systemId + "'");
                sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
                if (!dao.execute_command(sb.ToString()))
                {
                    BathClass.printErrorMsg("取消开牌失败!");
                    return;
                }
            }
            else
            {
                BathClass.printErrorMsg("不具有取消开牌权限!");
            }
        }

        //挂失手牌
        private void CtxLooseSeat_Click(object sender, EventArgs e)
        {

        }

        //添加备注
        private void CtxAddNote_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "添加备注"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            var seat = getContextSenderSeat(sender);
            if (seat.status != SeatStatus.USING && seat.status != SeatStatus.WARNING)
            {
                BathClass.printErrorMsg("手牌未使用，不能添加备注");
                return;
            }

            NoteForm noteForm = new NoteForm();
            if (noteForm.ShowDialog() != DialogResult.OK)
                return;

            string cmd_str = @"update [Seat] set note='" + noteForm.note + "' where text='" + seat.text + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("手牌添加失败!");
                return;
            }
        }

        //锁定解锁
        private void CtxLock_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);

            if (dao.get_authority(LogIn.m_User, "锁定解锁"))
            {
                lock_unlock(seat, LogIn.m_User);
                return;
            }

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputEmployee.employee, "锁定解锁"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            lock_unlock(seat, inputEmployee.employee);
        }

        private void lock_unlock(CSeat seat, Employee employee)
        {
            if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.USING)
            {
                string cmd_str = @"update [Seat] set status=4 where text='" + seat.text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("手牌锁定失败");
                    return;
                }
                //seat.status = seat;
            }
            else if (seat.status == SeatStatus.LOCKING)
            {
                string cmd_str = @"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('"
                    + employee.name + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime + "','解锁手牌',getdate())";

                if (seat.systemId == null || seat.systemId == "")
                    cmd_str += @" update [Seat] set status=1 where text='" + seat.text + "'";
                else
                    cmd_str += @" update [Seat] set status=2 where text='" + seat.text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("手牌解锁失败！");
                    return;
                }
            }

            //db_new.SubmitChanges();
        }

        //停用启用
        private void CtxSop_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputEmployee.employee, "停用启用"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            if (seat.status == SeatStatus.USING || seat.status == SeatStatus.WARNING ||
                seat.status == SeatStatus.DEPOSITLEFT || seat.status == SeatStatus.RESERVE)
            {
                BathClass.printErrorMsg("手牌正在使用不能停用");
                return;
            }
            else if (seat.status == SeatStatus.LOCKING)
            {
                BathClass.printErrorMsg("手牌已经锁定，不能停用");
                return;
            }
            else if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.PAIED)
            {
                string cmd_str = @"update [Seat] set status=" + (int)SeatStatus.STOPPED + " where text='" + seat.text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("手牌停用失败!");
                    return;
                }
            }
            else if (seat.status == SeatStatus.STOPPED)
            {
                string cmd_str = @"update [Seat] set status=" + (int)SeatStatus.AVILABLE + " where text='" + seat.text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("手牌启用失败!");
                    return;
                }
            }
        }

        //重新结账
        private void CtxRepay_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputEmployee.employee, "重新结账"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            //if (seat.status != SeatStatus.PAIED && seat.status != SeatStatus.AVILABLE)
            //{
            //    BathClass.printErrorMsg("手牌正在使用，不能重新结账");
            //    return;
            //}

            RepayActListForm form = new RepayActListForm(seat, inputEmployee.employee);
            form.ShowDialog();
        }

        //增加联排
        private void CtxChain_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            //Seat seat = getContextSenderSeat(db_new, sender);
            //OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            //openSeatForm.ShowDialog();

            var seat = getContextSenderSeat(sender);
            OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            openSeatForm.ShowDialog();
        }

        //重打账单
        private void CtxReprint_Click(object sender, EventArgs e)
        {
            CSeat seat = getContextSenderSeat(sender);
            ReceptionClass.reprint_bill(seat, dao, use_pad);
        }

        //右键解除警告
        private void unWarnTool_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);
            if (seat.status != SeatStatus.WARNING)
                return;

            Employee op_user;
            if (dao.get_authority(LogIn.m_User, "解除警告"))
                op_user = LogIn.m_User;
            else
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;
                if (dao.get_authority(inputEmployee.employee, "解除警告"))
                    op_user = inputEmployee.employee;
                else
                {
                    BathClass.printErrorMsg("不具有权限!");
                    return;
                }
            }

            string cmd_str = @"update [Seat] set status=2, unWarn='" + op_user.id + "' where text='" + seat.text + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("解除警告失败，请重试!");
                return;
            }
        }

        //恢复转账
        private void undoTransferTool_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);
            if (seat.status != SeatStatus.AVILABLE && seat.status != SeatStatus.PAIED)
            {
                BathClass.printErrorMsg("手牌已经重新使用，请在该手牌结账后恢复转账");
                return;
            }

            if (dao.exist_instance("Orders", "text='" + seat.text + "'"))
            {
                string cmd_str = @"update [Seat] set status=2 where text='" + seat.text + "' ";
                cmd_str += @"update [Orders] set systemId='" + seat.systemId + "' where text='" + seat.text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("恢复转账失败，请重试!");
                    return;
                }
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
            if (dao.get_authority(LogIn.m_User, "技师管理"))
            {
                BathClass.printErrorMsg("不具有权限");
                return;
            }

            TechnicianSeclectForm technicianForm = new TechnicianSeclectForm();
            technicianForm.ShowDialog();
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

            string cmd_str = @"insert into [RoomWarn](msg) values('警告')";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("预警失败,请重试!");
                return;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
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
            if (seatLock && !auto_seat_card)
                pay_seat_by_card();
            else
                tool_open_seat();
        }

        //查看联排
        private void toolChain_Click(object sender, EventArgs e)
        {
            ChainForm chainForm = new ChainForm();
            chainForm.ShowDialog();
        }

        //开牌
        private void openTool_Click(object sender, EventArgs e)
        {
            if (seatLock && !auto_seat_card)
                open_seat_by_card();
            else
                tool_open_seat();
        }

        //非自动感应手牌
        private void pay_seat_by_card()
        {
            try
            {
                if (!seatLock)
                {
                    BathClass.printErrorMsg("未启用手牌锁!");
                    return;
                }

                if (lock_type == "欧亿达")
                {
                    Thread.Sleep(500);
                    if (OYD.FKOPEN() != 1)
                        return;

                    OYD.CH375SetTimeout(0, 5000, 5000);
                }

                byte[] buff = new byte[200];
                string seat_text = "";

                int rt = -1;
                if (lock_type == "欧亿达")
                {
                    Thread.Sleep(500);
                    rt = OYD.OYEDA_id(buff);
                }
                else if (lock_type == "锦衣卫")
                    rt = JYW.ReadID(buff);
                else if (lock_type == "RF")
                    rt = RF.RF_RFID(ref seat_text);

                if (rt != 0)
                    return;

                CSeat seat = null;
                if (lock_type == "欧亿达")
                {
                    seat_text = Encoding.Default.GetString(buff, 0, 20).Trim();
                    seat_text = seat_text.Substring(0, 16);
                }
                else if (lock_type == "锦衣卫")
                {
                    seat_text = BathClass.byteToHexStr(buff);
                    seat_text = seat_text.Substring(0, 16);
                }

                seat = dao.get_seat("oId", seat_text);
                if (seat == null)
                {
                    BathClass.printErrorMsg("手牌" + seat_text + "不存在");
                    return;
                }
                else if (seat.status == SeatStatus.USING || seat.status == SeatStatus.WARNING ||
                    seat.status == SeatStatus.DEPOSITLEFT || seat.status == SeatStatus.REPAIED)
                {
                    if (lock_type == "欧亿达")
                        Thread.Sleep(500);

                    if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                        (lock_type == "锦衣卫" && JYW.MD(buff) != 0) ||
                        (lock_type == "RF" && RF.RF_MD() != 0))
                        return;

                    if (seat.deposit != null)
                        BathClass.printErrorMsg("交押金：" + seat.deposit.ToString());

                    if (seat.note != null && seat.note != "")
                        BathClass.printErrorMsg(seat.note);

                    show_seat_expense_form(seat);
                }
                else if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.PAIED)
                {
                    BathClass.printErrorMsg("手牌未开牌");
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //非自动感应手牌开牌
        private void open_seat_by_card()
        {
            try
            {
                if (!seatLock)
                {
                    BathClass.printErrorMsg("未启用手牌锁");
                    return;
                }

                if (lock_type == "欧亿达")
                {
                    Thread.Sleep(500);
                    if (OYD.FKOPEN() != 1)
                        return;

                    OYD.CH375SetTimeout(0, 5000, 5000);
                }

                byte[] buff = new byte[200];
                string seat_text = "";
                CSeat seat = null;

                int rt = -1;
                if (lock_type == "欧亿达")
                {
                    Thread.Sleep(500);
                    rt = OYD.OYEDA_id(buff);
                }
                else if (lock_type == "锦衣卫")
                    rt = JYW.ReadID(buff);
                else if (lock_type == "RF")
                    rt = RF.RF_RFID(ref seat_text);

                if (rt != 0)
                    return;

                if (lock_type == "欧亿达")
                {
                    seat_text = Encoding.Default.GetString(buff, 0, 20).Trim();
                    seat_text = seat_text.Substring(0, 16);
                }
                else if (lock_type == "锦衣卫")
                {
                    seat_text = BathClass.byteToHexStr(buff);
                    seat_text = seat_text.Substring(0, 16);
                }

                seat = dao.get_seat("text", seat_text);
                if (seat == null)
                {
                    BathClass.printErrorMsg("手牌" + seat_text + "不存在");
                    return;
                }

                if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.PAIED)
                {
                    if (lock_type == "欧亿达")
                        Thread.Sleep(500);

                    if ((lock_type == "欧亿达" && OYD.OYEDA_fk(buff) != 0) ||
                        (lock_type == "锦衣卫" && JYW.FK(buff) != 0) ||
                        (lock_type == "RF" && RF.RF_FK(seat_text) != 0))
                        return;

                    open_one_seat(seat);
                    open_seat(seat);
                }
                else if (seat.status == SeatStatus.USING)
                {
                    InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();

                    if (dao.get_authority(LogIn.m_User, "取消开牌") ||
                        (inputEmployee.ShowDialog() != DialogResult.OK &&
                        dao.get_authority(inputEmployee.employee, "取消开牌")))
                    {

                        var q = LogIn.options.取消开牌时限;
                        if (q != null && (DateTime.Now - seat.openTime.Value).TotalMinutes >= Convert.ToDouble(q))
                        {
                            BathClass.printErrorMsg("已超过取消开牌时限");
                            return;
                        }
                        var orders = dao.get_orders("systemId='" + seat.systemId + "'");
                        if (orders.Count() >= 2)
                        {
                            BathClass.printErrorMsg("已经点单，不能取消开台");
                            return;
                        }

                        if (BathClass.printAskMsg("确认取消开台?") != DialogResult.Yes)
                            return;

                        if (lock_type == "欧亿达")
                            Thread.Sleep(500);

                        if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                        (lock_type == "锦衣卫" && JYW.MD(buff) != 0) ||
                            (lock_type == "RF" && RF.RF_MD() != 0))
                            return;

                        string cmd_str = @"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('"
                            + inputEmployee.employee.id + "','" + seat.text + "','"
                            + seat.openEmployee + "','" + seat.openTime.ToString()
                            + "','取消开牌',getdate()";

                        cmd_str += @" delete from [Orders] where systemId='" + seat.systemId + "'";
                        cmd_str += dao.reset_seat_string() + "text='" + seat.text + "')";
                        if (!dao.execute_command(cmd_str))
                        {
                            BathClass.printErrorMsg("取消开牌失败!");
                            return;
                        }
                    }
                    else
                    {
                        BathClass.printErrorMsg("不具有取消开牌权限");
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
