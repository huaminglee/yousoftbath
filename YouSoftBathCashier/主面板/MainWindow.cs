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
using System.IO;
using YouSoftBathConstants;

namespace YouSoftBathReception
{
    public partial class MainWindow : Form
    {
        //成员变量
        private CSeat m_Seat = null;

        private Thread m_thread;//刷新线程
        private Thread m_thread_seatCard;//手牌锁线程
        private Thread m_thread_detect_reserve_over_due;//手牌锁线程
        private Thread m_thread_clearMemory;

        public bool _seatLock;//启用手牌锁
        public bool _auto_seat_card;//自动感应
        public string _lock_type;
        private int _seat_length = -1;
        private int _seat_start = -1;
        private bool use_pad = false;
        private bool _has_double_department = false;

        //private COptions _m_options;
        private DAO _dao;

        public bool seatLock
        {
            get { return _seatLock; }
            set { _seatLock = value; }
        }

        public bool auto_seat_card
        {
            get { return _auto_seat_card; }
            set { _auto_seat_card = value; }
        }

        public string lock_type
        {
            get { return _lock_type; }
            set { _lock_type = value; }
        }

        public int seat_length
        {
            get { return _seat_length; }
            set { _seat_length = value; }
        }

        public int seat_start
        {
            get { return _seat_start; }
            set { _seat_start = value; }
        }

        public bool has_double_department
        {
            get { return _has_double_department; }
            set { _has_double_department = value; }
        }

        public DAO dao
        {
            get { return _dao; }
            set { _dao = value; }
        }

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

            //var db = new BathDBDataContext(LogIn.connectionString);
            seat_length = dao.get_seat_length();
            seat_start = (seat_length == 3) ? 17 : 16;

            //var ops = db.Options.FirstOrDefault();
            seatLock = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用手牌锁, false);
            auto_seat_card = MConvert<bool>.ToTypeOrDefault(LogIn.options.自动感应手牌, false);
            use_pad = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);
            lock_type = LogIn.options.手牌锁类型;
            has_double_department = dao.has_hotel_department();
            tSeat.Visible = (!seatLock || has_double_department);

            this.Text = Constants.appName + "-前台系统" + Constants.version + " 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户: " + LogIn.m_User.id + "   " + LogIn.m_User.name;

            var td = new Thread(new ThreadStart(initial_ui_thread));
            td.IsBackground = true;
            td.Start();

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

            if (has_double_department)
            {
                m_thread_detect_reserve_over_due = new Thread(new ThreadStart(detect_reserve_over_due));
                m_thread_detect_reserve_over_due.IsBackground = true;
                m_thread_detect_reserve_over_due.Start();
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
            CFormCreate.createSeatByDao(dao, LogIn.options, seatPanel, seatTab, new EventHandler(btn_Click), seatContext, "桑拿部");
            setStatus();
        }

        //检测预定超时
        private void detect_reserve_over_due()
        {
            while (true)
            {
                try
                {
                    var dc_new = new BathDBDataContext(LogIn.connectionString);
                    var seats = dc_new.Seat.Where(x => x.status == 9 && DateTime.Now >= x.dueTime);
                    foreach (var seat in seats)
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new delegate_show_reserve_over_dur_form(show_reserve_over_dur_form),
                                    new object[] { seat });
                        else
                            show_reserve_over_dur_form(seat);
                    }
                }
                catch
                {
                }
            }
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
                    var seats_status_tmp = seats.Select(x=>x.status).ToList();

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

        private delegate void set_status_delegate(BathDBDataContext dc);
        private delegate void set_btn_color_delegate(Button btn, Color color);
        private void set_btn_color(Button btn, Color color)
        {
            btn.BackColor = color;
        }

        //非自动感应手牌
        private void open_seat_expense_by_card()
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

                    if (MConvert<bool>.ToTypeOrDefault(seat.ordering, false))
                    {
                        BathClass.printErrorMsg("正在录单!");
                        return;
                    }
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
                    }
                    else if (lock_type == "锦衣卫")
                    {
                        seat_text = BathClass.byteToHexStr(buff);
                        seat_text = seat_text.Substring(0, 16);
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

                        if (MConvert<bool>.ToTypeOrDefault(seat.ordering, false))
                        {
                            BathClass.printErrorMsg("正在录单!");
                            continue;
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
                        if (this.InvokeRequired)
                            this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                    new object[] { "手牌未开牌" });
                        else
                            BathClass.printErrorMsg("手牌未开牌");
                    }
                }
                catch
                {
                }
            }
        }

        private delegate void delegate_show_reserve_over_dur_form(Seat seat);
        private void show_reserve_over_dur_form(Seat seat)
        {
            var form = new ReserveOverDueForm(seat);
            form.ShowDialog();
        }

        private void show_seat_expense_form(CSeat seat)
        {
            SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat, seat_length, seatLock, auto_seat_card, dao);
            seatExpenseForm.ShowDialog();
        }

        private delegate void delegate_show_seat_expense_form(CSeat seat);
        private delegate void delegate_print_msg(string msg);

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

            //var menu = dao.get_seat_menu(seat.text);
            //if (menu != null)
            //{
            //    sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
            //    sb.Append(@"values('" + menu.name + "','" + seat.text + "','" + systemId + "',1," + menu.price + ",getdate(),'");
            //    sb.Append(LogIn.m_User.id + "','False')");
            //}
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("开牌失败，请重试!");
                return;
            }
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var manuInput = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号结账, false);
            //m_Seat = db_new.Seat.FirstOrDefault(x => x.text == btn.Text);
            m_Seat = dao.get_seat("text", btn.Text);
            var mtype = dao.get_seattype("id", m_Seat.typeId);
            //var mtype = db_new.SeatType.FirstOrDefault(x => x.id == m_Seat.typeId);

            switch (m_Seat.status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                case SeatStatus.RESERVE://预定客房
                    if (mtype.department == "客房部")
                    {
                        var form = new OpenRoomForm(m_Seat);
                        form.ShowDialog();
                    }
                    else if (mtype.menuId == null)
                    {
                        BathClass.printErrorMsg("手牌未开牌");
                        return;
                    }
                    else if (!manuInput && mtype.menuId != null)
                    {

                        BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                        return;
                    }
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.WARNING://警告
                case SeatStatus.DEPOSITLEFT://押金离场
                case SeatStatus.REPAIED://重新结账
                    if (MConvert<bool>.ToTypeOrDefault(m_Seat.ordering, false))
                    {
                        BathClass.printErrorMsg("正在录单!");
                        return;
                    }

                    if (mtype.department != "客房部" && !manuInput && mtype.menuId != null)
                    {
                        BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                        return;
                    }
                    if (m_Seat.deposit != null)
                        BathClass.printInformation("交押金：" + m_Seat.deposit.ToString());

                    if (m_Seat.note != null && m_Seat.note != "")
                        BathClass.printInformation(m_Seat.note);

                    if (mtype.menuId == null)
                    {
                        SeatExpenseForm seatExpenseForm = new SeatExpenseForm(m_Seat, seat_length, false, false, dao);
                        seatExpenseForm.Show();
                    }
                    else
                    {
                        SeatExpenseForm seatExpenseForm = new SeatExpenseForm(m_Seat, seat_length, seatLock, auto_seat_card, dao);
                        seatExpenseForm.Show();
                    }
                    
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

        //F6开牌
        private void tool_open_seat()
        {
            if (tSeat.Text == "")
                return;

            string text = tSeat.Text;
            tSeat.Text = "";
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            //var ops = db_new.Options.FirstOrDefault();
            var manuInput = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号结账, false);
            var use_idCard = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用ID手牌锁, false);

            var seat1 = dao.get_seat("text", text);
            //var seat1 = db_new.Seat.FirstOrDefault(x => x.text == text);
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
                    if (manuInput)
                    {
                        //var mtype = dao.get_seattype("id", seat1.typeId);
                        //var mtype = db_new.SeatType.FirstOrDefault(x => x.id == seat1.typeId);
                        if (!manuInput && mtype.menuId != null)
                        {
                            BathClass.printErrorMsg("不允许手工输入手牌号结账!");
                            return;
                        }
                        SeatStatus status = seat1.status;
                        if (status == SeatStatus.USING || status == SeatStatus.WARNING || 
                            status == SeatStatus.DEPOSITLEFT || status == SeatStatus.REPAIED)
                        {
                            if (MConvert<bool>.ToTypeOrDefault(seat1.ordering, false))
                            {
                                BathClass.printErrorMsg("正在录单!");
                                return;
                            }

                            if (seat1.deposit != null)
                                BathClass.printInformation("交押金：" + seat1.deposit.ToString());

                            if (seat1.note != null && seat1.note != "")
                                BathClass.printInformation(seat1.note);

                            if (mtype.menuId == null)
                            {
                                SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat1, seat_length, false, false, dao);
                                seatExpenseForm.Show();
                            }
                            else
                            {
                                SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat1, seat_length, seatLock, auto_seat_card, dao);
                                seatExpenseForm.Show();
                            }
                        }
                        else if (status == SeatStatus.LOCKING)
                            BathClass.printErrorMsg("台位已经锁定!");
                        else if (status == SeatStatus.STOPPED)
                            BathClass.printErrorMsg("台位已经停用!");
                    }
                }
            }
            else if (use_idCard)
            {
                var seat2 = dao.get_seat("oId", text);
                var mtype = dao.get_seattype("id", seat2.typeId);
                //var seat2 = db_new.Seat.FirstOrDefault(x => x.oId == text);
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

                    if (mtype.menuId == null)
                    {
                        SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat2, seat_length, false, false, dao);
                        seatExpenseForm.Show();
                    }
                    else
                    {
                        SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat2, seat_length, seatLock, auto_seat_card, dao);
                        seatExpenseForm.Show();
                    }
                }
                else if (status == SeatStatus.LOCKING)
                    BathClass.printErrorMsg("台位已经锁定!");
                else if (status == SeatStatus.STOPPED)
                    BathClass.printErrorMsg("台位已经停用!");
            }
            
            
        }

        //设置状态信息
        /*private void setStatus(BathDBDataContext db_new)
        {
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "营业信息查看"))
            {
                statusStrip2.Visible = false;
                return;
            }

            seatTotal.Text = db_new.Seat.Count().ToString();
            seatAvi.Text = db_new.Seat.Where(x => x.status == 1).Count().ToString();

            DateTime st = DateTime.Parse("2013-1-1 00:00:00");
            if (db_new.ClearTable.Count() != 0)
            {
                st = db_new.ClearTable.ToList().Last().clearTime;
            }
            int count = 0;
            double pm = BathClass.get_paid_expense(db_new, st, ref count);
            seatPaid.Text = count.ToString();
            moneyPaid.Text = pm.ToString();

            seatUnpaid.Text = db_new.Seat.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status == 8).Count().ToString();
            double upm = BathClass.get_unpaid_expense(db_new, LogIn.connectionString);
            moneyUnpaid.Text = upm.ToString();

            moneyTotal.Text = (pm + upm).ToString();
        }*/

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
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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

            var seat = dao.get_seat("text", inputSeatForm.m_Seat.text);
            //var seat = db_new.Seat.FirstOrDefault(x => x.text == inputSeatForm.m_Seat.text);
            //seat.ordering = true;
            //db_new.SubmitChanges();
            
            OrderForm orderForm = new OrderForm(seat, LogIn.m_User, LogIn.connectionString, false);
            orderForm.ShowDialog();

            //seat.ordering = false;
            //db_new.SubmitChanges();
        }

        //客房
        private void btnRoom_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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
            //RoomViewForm rmvForm = new RoomViewForm();
            //rmvForm.ShowDialog();
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
                case Keys.Space:
                    if (seatLock && !auto_seat_card)
                        open_seat_expense_by_card();
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
            //return db_new.Seat.FirstOrDefault(x => x.text == bt.Text);
        }

        //更换手牌
        private void CtxChangeSeat_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputEmployee.employee, "更换手牌"))
            {
                BathClass.printErrorMsg(inputEmployee.employee.id + "不具有更换手牌操作权限!");
                return;
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
            //Seat newSeat = db_new.Seat.FirstOrDefault(x => x == inputSeatForm.m_Seat);
            //var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            //foreach (Orders order in orders)
                //order.text = newSeat.text;

            sb.Append(@"update [Seat] set systemId='" + seat.systemId + "',name='" + seat.name + "',population=" + seat.population);
            sb.Append(",openTime=getdate(),openEmployee='" + LogIn.m_User.id + "',chainId='" + seat.chainId + "',status=" + seat.status);
            sb.Append(",note='" + seat.note + "',ordering='False'");
            //newSeat.systemId = seat.systemId;
            //newSeat.name = seat.name;
            //newSeat.population = seat.population;
            //newSeat.openTime = BathClass.Now(LogIn.connectionString);
            //newSeat.openEmployee = LogIn.m_User.name;
            //newSeat.phone = seat.phone;
            //newSeat.chainId = seat.chainId;
            //newSeat.status = seat.status;
            //newSeat.note = seat.note;
            //newSeat.ordering = seat.ordering;

            sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("更换手牌失败，请重试！");
                return;
            }

            //BathClass.reset_seat(seat);
            //db_new.SubmitChanges();
        }

        //取消开牌
        private void CtxCancelOpen_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputEmployee.employee, "取消开牌"))
            {
                BathClass.printErrorMsg(inputEmployee.employee.id + "不具有取消开台权限!");
                return;
            }

            if (seat.status != SeatStatus.USING)
            {
                BathClass.printErrorMsg("该台位不在使用中，不能取消开台!");
                return;
            }

            //var options = db_new.Options.FirstOrDefault();
            var q = LogIn.options.取消开牌时限;
            if (q != null && (dao.Now() - seat.openTime.Value).TotalMinutes >= Convert.ToInt32(q))
            {
                BathClass.printErrorMsg("已超过取消开牌时限！");
                return;
            }

            var orders = dao.get_orders("systemId='" + seat.systemId + "'");
            //var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            if (orders.Count > 2)
            {
                BathClass.printErrorMsg("已经点单，不能取消开台");
                return;
            }

            if (BathClass.printAskMsg("确认取消开台?") != DialogResult.Yes)
                return;

            StringBuilder sb = new StringBuilder();
            sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
            sb.Append(inputEmployee.employee.id + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime.ToString());
            sb.Append("','取消开牌',getdate()");
            //Operation op = new Operation();
            //op.employee = inputEmployee.employee.id;
            //op.seat = seat.text;
            //op.openEmployee = seat.openEmployee;
            //op.openTime = seat.openTime;
            //op.explain = "取消开牌";
            //op.opTime = BathClass.Now(LogIn.connectionString);
            //db_new.Operation.InsertOnSubmit(op);

            sb.Append(@" delete from [Orders] where systemId='" + seat.systemId + "'");
            sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
            //db_new.Orders.DeleteAllOnSubmit(db_new.Orders.Where(x => x.systemId == seat.systemId));
            //BathClass.reset_seat(seat);
            //db_new.SubmitChanges();
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("取消开牌失败!");
                return;
            }
        }

        //挂失手牌
        private void CtxLooseSeat_Click(object sender, EventArgs e)
        {

        }

        //添加备注
        private void CtxAddNote_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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
            //seat.note = noteForm.note;
            //db_new.SubmitChanges();
        }

        //锁定解锁
        private void CtxLock_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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
                StringBuilder sb = new StringBuilder();
                sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
                sb.Append(employee.name + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime + "','解锁手牌',getdate())");

                //Operation op = new Operation();
                //op.employee = employee.name;
                //op.seat = seat.text;
                //op.openEmployee = seat.openEmployee;
                //op.openTime = seat.openTime;
                //op.explain = "解锁手牌";
                //op.opTime = BathClass.Now(LogIn.connectionString);
                //db_new.Operation.InsertOnSubmit(op);

                if (seat.systemId == null || seat.systemId == "")
                    sb.Append(@" update [Seat] set status=1 where text='" + seat.text + "'");
                //s#eat.status = 1;
                else
                    sb.Append(@" update [Seat] set status=2 where text='" + seat.text + "'");
                    //seat.status = 2;
                if (!dao.execute_command(sb.ToString()))
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

            //db_new.SubmitChanges();
        }

        //重新结账
        private void CtxRepay_Click(object sender, EventArgs e)
        {
            //TableCashierCheckForm tableCashierSummaryForm = new TableCashierCheckForm();
            //tableCashierSummaryForm.ShowDialog();

            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);
            OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            openSeatForm.ShowDialog();
        }

        //重打账单
        private void CtxReprint_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            CSeat seat = getContextSenderSeat(sender);

            if (seat.status != SeatStatus.PAIED)
            {
                BathClass.printErrorMsg("已经重新开牌，不能重打账单!");
                return;
            }

            var account = dao.get_account("abandon is null and systemId like '%" + seat.systemId + "%'");
            //var account = db_new.Account.FirstOrDefault(x => x.systemId.Contains(seat.systemId) && x.abandon == null);
            if (account == null)
                return;

            var seats_txt = account.text.Split('|');
            string state_str = "";
            int count = seats_txt.Count();
            for (int i = 0; i < count; i++ )
            {
                state_str += "text='" + seats_txt[i] + "'";
                if (i != count - 1)
                    state_str += " or ";
            }
            var seats_reprint = dao.get_seats(state_str);
            //var seats_reprint = db_new.Seat.Where(x => seats_txt.Contains(x.text)).ToList();

            DataGridView dgv = new DataGridView();

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "手牌";
            dgv.Columns.Add(col);

            if (use_pad)
            {
                DataGridViewTextBoxColumn coll = new DataGridViewTextBoxColumn();
                coll.HeaderText = "房间";
                dgv.Columns.Add(coll);
            }

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
            if (use_pad)
                printCols.Add("房间");
            printCols.Add("项目名称");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var co_name = LogIn.options.companyName;
            //var co_name = db_new.Options.FirstOrDefault().companyName;
            if (account != null)
            {
                var ids = account.systemId.Split('|');
                SqlConnection sqlCn = null;

                try
                {
                    sqlCn = new SqlConnection(LogIn.connectionString);
                    sqlCn.Open();

                    string cmd_str = "Select * from [HisOrders] where deleteEmployee is null and accountId=" + account.id
                            + " order by text";
                    var cmd = new SqlCommand(cmd_str, sqlCn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (!use_pad)
                            {
                                string[] row = new string[6];
                                row[0] = dr["text"].ToString();
                                row[1] = dr["menu"].ToString();
                                row[2] = dr["technician"].ToString();

                                var cmenu = dao.get_Menu("name", dr["menu"].ToString());
                                if (cmenu != null)
                                    row[3] = cmenu.price.ToString();

                                row[4] = dr["number"].ToString();
                                row[5] = dr["money"].ToString();
                                dgv.Rows.Add(row);
                            }
                            else
                            {
                                string[] row = new string[7];
                                row[0] = dr["text"].ToString();
                                row[1] = dr["roomId"].ToString();
                                row[2] = dr["menu"].ToString();
                                row[3] = dr["technician"].ToString();

                                var cmenu = dao.get_Menu("name", dr["menu"].ToString());
                                if (cmenu != null)
                                    row[4] = cmenu.price.ToString();

                                row[5] = dr["number"].ToString();
                                row[6] = dr["money"].ToString();
                                dgv.Rows.Add(row);
                            }
                        }
                    }
                    PrintBill.Print_DataGridView(seats_reprint, account, "存根单", dgv, printCols, true, null, co_name);
                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show(cmd_str);
                    BathClass.printErrorMsg(ex.ToString());
                }
                finally
                {
                    dao.close_connection(sqlCn);
                }
            }
            else
            {

                //var orders = db_new.Orders.Where(x => x.systemId != seat.systemId && x.text == seat.text && !x.paid && x.deleteEmployee == null);
                //if (!orders.Any())
                //{
                //    BathClass.printErrorMsg("未检测到转账单或者结账单");
                //    return;
                //}
                //foreach (var order in orders)
                //{
                //    string[] row = new string[6];
                //    row[0] = order.text;
                //    row[1] = order.menu;
                //    row[2] = order.technician;

                //    var menu = db_new.Menu.FirstOrDefault(x => x.name == order.menu);
                //    if (menu != null)
                //        row[3] = menu.price.ToString();

                //    row[4] = order.number.ToString();
                //    row[5] = order.money.ToString();
                //    dgv.Rows.Add(row);
                //}

                SqlConnection sqlCn = null;
                double money = 0;

                try
                {
                    sqlCn = new SqlConnection(LogIn.connectionString);
                    sqlCn.Open();

                    string cmd_str = "Select * from [Orders] where text='"+seat.text+"' and sytemId!='"+seat.systemId+
                        "' deleteEmployee is null and paid='False' order by text";

                    var cmd = new SqlCommand(cmd_str, sqlCn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (!use_pad)
                            {
                                string[] row = new string[6];
                                row[0] = dr["text"].ToString();
                                row[1] = dr["menu"].ToString();
                                row[2] = dr["technician"].ToString();

                                var cmenu = dao.get_Menu("name", dr["menu"].ToString());
                                if (cmenu != null)
                                    row[3] = cmenu.price.ToString();

                                row[4] = dr["number"].ToString();
                                row[5] = dr["money"].ToString();
                                dgv.Rows.Add(row);
                            }
                            else
                            {
                                string[] row = new string[7];
                                row[0] = dr["text"].ToString();
                                row[1] = dr["roomId"].ToString();
                                row[2] = dr["menu"].ToString();
                                row[3] = dr["technician"].ToString();

                                var cmenu = dao.get_Menu("name", dr["menu"].ToString());
                                if (cmenu != null)
                                    row[4] = cmenu.price.ToString();

                                row[5] = dr["number"].ToString();
                                row[6] = dr["money"].ToString();
                                dgv.Rows.Add(row);
                            }
                        }
                    }

                    if (dgv.Rows.Count != 0)
                    {
                        BathClass.printErrorMsg("未检测到转账单或者结账单");
                        return;
                    }

                    //var money = BathClass.get_cur_orders_money(orders, LogIn.connectionString, BathClass.Now(LogIn.connectionString));
                    List<CSeat> seats = new List<CSeat>();
                    seats.Add(seat);

                    PrintSeatBill.Print_DataGridView(seats, "", "转账确认单", dgv, printCols, money.ToString(), co_name);
                }
                catch (System.Exception ex)
                {
                    //MessageBox.Show(cmd_str);
                    BathClass.printErrorMsg(ex.ToString());
                }
                finally
                {
                    dao.close_connection(sqlCn);
                }
            }
        }

        //右键解除警告
        private void unWarnTool_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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
            //seat.status = 2;
            //seat.unwarn = op_user.id;
            //db_new.SubmitChanges();
        }

        //恢复转账
        private void undoTransferTool_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);
            if (seat.status != SeatStatus.AVILABLE && seat.status != SeatStatus.PAIED)
            {
                BathClass.printErrorMsg("手牌已经重新使用，请在该手牌结账后恢复转账");
                return;
            }

            //var orders = db_new.Orders.Where(x => x.text == seat.text && !x.paid);
            //if (orders.Any())
            //{
            //    seat.status = 2;
            //    foreach (var order in orders)
            //    {
            //        order.systemId = seat.systemId;
            //    }

            //    db_new.SubmitChanges();
            //}
            if (dao.exist_instance("Orders", "text='"+seat.text+"'"))
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"update [Seat] set status=2 where text='" + seat.text + "' ");
                sb.Append(@"update [Orders] set systemId='" + seat.systemId + "' where text='" + seat.text + "'");
                if (!dao.execute_command(sb.ToString()))
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
            //if (!BathClass.getAuthority(db_new, LogIn.m_User, "技师管理"))
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
            //var dc_new = new BathDBDataContext(LogIn.connectionString);
            //var warn = new RoomWarn();
            //warn.msg = "警告";
            //dc_new.RoomWarn.InsertOnSubmit(warn);
            //dc_new.SubmitChanges();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            //if (m_thread_seatCard != null && m_thread_seatCard.IsAlive)
            //    m_thread_seatCard.Abort();

            //if (m_thread_detect_reserve_over_due != null && m_thread_detect_reserve_over_due.IsAlive)
            //    m_thread_detect_reserve_over_due.Abort();

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
                open_seat_expense_by_card();
            else
                tool_open_seat();
        }

        //查看联排
        private void toolChain_Click(object sender, EventArgs e)
        {
            ChainForm chainForm = new ChainForm();
            chainForm.ShowDialog();
        }
    }
}
