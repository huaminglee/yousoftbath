using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using YouSoftBathConstants;
using YouSoftBathGeneralClass;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using YouSoftBathFormClass;
using System.Transactions;
using System.Threading;
using System.Timers;

namespace YouSoftBathShoe
{
    public partial class MainWindow : Form
    {
        //成员变量
        private DAO dao;
        private CSeat m_Seat = null;
        private string companyName;
        private DateTime thisTime;//上一次夜审时间
        private Thread m_thread;//刷新线程
        private Thread m_thread_msg;//检测结账取鞋消息
        private Thread m_thread_seatCard;//手牌锁线程
        private Thread m_thread_clearMemory;

        private bool seatLock;//启用欧亿达手牌锁
        private bool auto_seat_card;//自动感应手牌
        private string lock_type;//手牌类型
        private object cancel_open_delay;//取消开牌时限
        private int seat_length = -1;
        private int seat_start = -1;

        public MainWindow()
        {
            InitializeComponent();
            if (File.Exists(@".\img\main.jpg"))
                seatPanel.BackgroundImage = Image.FromFile(@".\img\main.jpg");
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            //var db = new BathDBDataContext(LogIn.connectionString);
            //seat_length = db.Seat.FirstOrDefault().text.Length;
            seat_length = dao.get_seat_length();
            seat_start = (seat_length == 3) ? 17 : 16;
            this.Text = Constants.appName + "鞋吧系统" + Constants.version + " 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户:" + LogIn.m_User.id + "  " + LogIn.m_User.name;

            //var ops = db.Options.FirstOrDefault();
            seatLock = LogIn.options.启用手牌锁.Value;
            auto_seat_card = MConvert<bool>.ToTypeOrDefault(LogIn.options.自动感应手牌, false);
            lock_type = LogIn.options.手牌锁类型;
            companyName = LogIn.options.companyName;
            changeSeatTool.Visible = !MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);
            toolStripLabel3.Visible = !MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);
            cancel_open_delay = LogIn.options.取消开牌时限;

            get_clear_table_time();
            //CFormCreate.createSeat(db, seatPanel, seatTab, new EventHandler(btn_Click), seatContext);
            //setStatus(db);
            var td = new Thread(new ThreadStart(initial_ui_thread));
            td.IsBackground = true;
            td.Start();

            dgv_shoe_show();

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

            if (MConvert<bool>.ToTypeOrDefault(LogIn.options.启用鞋部, false))
            {
                m_thread_msg = new Thread(new ThreadStart(detect_shoe_msg));
                m_thread_msg.IsBackground = true;
                m_thread_msg.Start();
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
                if (this.InvokeRequired)
                    this.Invoke(new no_par_delegate(initial_ui), null);
                else
                    initial_ui();
            }
            catch (System.Exception e)
            {

            }
        }
        private delegate void no_par_delegate();
        private void initial_ui()
        {
            CFormCreate.createSeatByDao(dao, LogIn.options, seatPanel, seatTab, new EventHandler(btn_Click), new EventHandler(btn_MouseHover), seatContext, "桑拿部");
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
                    //var db_new = new BathDBDataContext(LogIn.connectionString);
                    //var seats_id_tmp = db_new.Seat.Where(x=>db_new.SeatType.Where(y=>y.department=="桑拿部").Select(y=>y.id).Contains(x.typeId)).Select(x => x.id).ToList();
                    //var seats_status_tmp = db_new.Seat.Select(x => x.status).ToList();

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
                            this.Invoke(new no_par_delegate(setStatus));
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

        //非自动感应手牌
        private void open_seat_noauto()
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

                //var db_new = new BathDBDataContext(LogIn.connectionString);
                //var seat = db_new.Seat.FirstOrDefault(x => x.text == seat_text);
                //seat = dao.get_seat("text", seat_text);
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
                        //var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
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

                        StringBuilder sb = new StringBuilder(); 
                        sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
                        sb.Append(inputEmployee.employee.id + "','" + seat.text + "','");
                        sb.Append(seat.openEmployee + "','" + seat.openTime.ToString());
                        sb.Append("','取消开牌',getdate()");

                        sb.Append(" delete from [Orders] where systemId='" + seat.systemId + "'");
                        sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
                        if (!dao.execute_command(sb.ToString()))
                        {
                            BathClass.printErrorMsg("取消开牌失败!");
                            return;
                        }
                        //Operation op = new Operation();

                        //if (inputEmployee.employee != null)
                        //    op.employee = inputEmployee.employee.id;
                        //else
                        //    op.employee = LogIn.m_User.id;
                        //op.seat = seat.text;
                        //op.openEmployee = seat.openEmployee;
                        //op.openTime = seat.openTime;
                        //op.explain = "取消开牌";
                        //op.opTime = BathClass.Now(LogIn.connectionString);
                        //db_new.Operation.InsertOnSubmit(op);

                        //db_new.SystemIds.DeleteAllOnSubmit(db_new.SystemIds.Where(x => x.systemId == seat.systemId));
                        //db_new.Orders.DeleteAllOnSubmit(db_new.Orders.Where(x => x.systemId == seat.systemId));
                        //BathClass.reset_seat(seat);
                        //db_new.SubmitChanges();
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
                        continue;

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

                    if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.PAIED)
                    {
                        if (lock_type == "欧亿达")
                            Thread.Sleep(500);

                        if ((lock_type == "欧亿达" && OYD.OYEDA_fk(buff) != 0) ||
                            (lock_type == "锦衣卫" && JYW.FK(buff) != 0) ||
                            (lock_type == "RF" && RF.RF_FK(seat_text) != 0))
                            continue;

                        open_one_seat(seat);

                        if (this.InvokeRequired)
                            this.Invoke(new delegate_open_seat(open_seat), new object[] { seat });
                        else
                            open_seat(seat);
                        //Thread.Sleep(500);
                    }
                    else if (seat.status == SeatStatus.USING)
                    {
                        InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();

                        if (dao.get_authority(LogIn.m_User, "取消开牌") ||
                            (inputEmployee.ShowDialog() != DialogResult.OK &&
                            dao.get_authority(inputEmployee.employee, "取消开牌")))
                        {

                            if (cancel_open_delay != null &&
                                (DateTime.Now - seat.openTime.Value).TotalMinutes >= MConvert<double>.ToTypeOrDefault(cancel_open_delay, 0))
                            {
                                if (this.InvokeRequired)
                                    this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                        new object[] { "已超过取消开牌时限" });
                                else
                                    BathClass.printErrorMsg("已超过取消开牌时限");
                                continue;
                            }

                            var order_count = dao.get_orders_count("systemId='" + seat.systemId + "'");
                            //var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
                            if (order_count >= 2)
                            {
                                if (this.InvokeRequired)
                                    this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                        new object[] { "已经点单，不能取消开台" });
                                else
                                    BathClass.printErrorMsg("已经点单，不能取消开台");
                                continue;
                            }

                            if (BathClass.printAskMsg("确认取消开台?") != DialogResult.Yes)
                                continue;

                            if (lock_type == "欧亿达")
                                Thread.Sleep(500);

                            if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                            (lock_type == "锦衣卫" && JYW.MD(buff) != 0) ||
                                (lock_type == "RF" && RF.RF_MD() != 0))
                                continue;

                            string cancel_open_id;
                            if (inputEmployee.employee != null)
                                cancel_open_id = inputEmployee.employee.id;
                            else
                                cancel_open_id = LogIn.m_User.id;

                            StringBuilder sb = new StringBuilder();
                            sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
                            sb.Append(cancel_open_id + "','" + seat.text + "','");
                            sb.Append(seat.openEmployee + "','" + seat.openTime.ToString());
                            sb.Append("','取消开牌',getdate())");

                            sb.Append(@" delete from [Orders] where text='" + seat.text + "'");
                            sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
                            if (!dao.execute_command(sb.ToString()))
                            {
                                if (this.InvokeRequired)
                                    this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                        new object[] { "取消开牌失败" });
                                else
                                    BathClass.printErrorMsg("取消开牌失败");
                                continue;
                            }
                        }
                        else
                        {
                            if (this.InvokeRequired)
                                this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                    new object[] { "不具有取消开牌权限" });
                            else
                                BathClass.printErrorMsg("不具有取消开牌权限");
                        }
                    }
                    else
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                    new object[] { "手牌不可用" });
                        else
                            BathClass.printErrorMsg("手牌不可用");
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        private delegate void delegate_print_msg(string msg);
        private delegate void delegate_open_seat(CSeat seat);

        private void orderform_show(CSeat seat)
        {
            OrderForm orderForm = new OrderForm(seat, LogIn.m_User, LogIn.connectionString, false);
            orderForm.ShowDialog();
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

        //获取夜审时间
        private void get_clear_table_time()
        {
            var ct = dao.get_last_clear_time();
            //var ct = dc.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault();
            if (ct == null)
                thisTime = DateTime.Parse("2013-01-01 00:00:00");
            else
                thisTime = ct.Value;
        }

        private void dgv_shoe_show()
        {
            var dc = new BathDBDataContext(LogIn.connectionString);
            dgv.Rows.Clear();
            var orders = dc.Orders.Where(x => x.menu.Contains("浴资") && x.inputTime >= thisTime).OrderByDescending(x => x.inputTime);

            List<string> ts = new List<string>();
            foreach (var s in orders)
            {
                var t = s.text;
                if (!ts.Contains(t))
                {
                    dgv.Rows.Add(t);
                    ts.Add(t);
                }
            }
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            //m_Seat = db_new.Seat.FirstOrDefault(x => x.text == btn.Text);
            m_Seat = dao.get_seat("text", btn.Text);
            //var seat_menu = dao.get_seat_menu(m_Seat.text);
            var mtype = dao.get_seattype("id", m_Seat.typeId);
            //var mtype = db_new.SeatType.FirstOrDefault(x => x.id == m_Seat.typeId);

            switch (m_Seat.status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                    if (!MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号开牌, false) && mtype.menuId != null)
                    {
                        BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                        break;
                    }
                    if (MConvert<bool>.ToTypeOrDefault(mtype.depositeRequired, false))
                    {
                        OpenDepositForm form = new OpenDepositForm(m_Seat);
                        if (form.ShowDialog() != DialogResult.OK)
                            return;
                    }
                    open_seat(m_Seat);
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.DEPOSITLEFT://押金离场
                    if (MConvert<bool>.ToTypeOrDefault(m_Seat.paying, false))
                    {
                        BathClass.printErrorMsg("正在结账!");
                        break;
                    }
                    //if (BathClass.ToBool(m_Seat.ordering))
                    //{
                    //    BathClass.printErrorMsg("正在录单!");
                    //    break;
                    //}
                    if (!dao.get_authority(LogIn.m_User, "完整点单") &&
                        !dao.get_authority(LogIn.m_User, "可见本人点单"))
                    {
                        BathClass.printErrorMsg("权限不够");
                        break;
                    }
                    //m_Seat.ordering = true;
                    //db_new.SubmitChanges();

                    OrderForm orderForm = new OrderForm(m_Seat, LogIn.m_User, LogIn.connectionString, false);
                    orderForm.ShowDialog();

                    //m_Seat.ordering = false;
                    //db_new.SubmitChanges();
                    break;
                case SeatStatus.LOCKING://锁定
                    break;
                case SeatStatus.STOPPED://停用
                    BathClass.printErrorMsg("台位已经停用!");
                    break;
                case SeatStatus.WARNING://警告
                    break;
                case SeatStatus.REPAIED://重新结账
                    BathClass.printErrorMsg("补救台位不能录单");
                    break;
                case SeatStatus.RESERVE://客房预定
                    BathClass.printErrorMsg("预定台位不能录单");
                    break;
                default:
                    break;
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
            dgv_shoe_show();
        }

        //private delegate void set_status_delegate(BathDBDataContext dc);
        
        //设置营业信息状态栏
        /*private void setStatus(BathDBDataContext db_new)
        {
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "营业信息查看"))
            {
                statusStrip2.Visible = false;
                return;
            }

            seatTotal.Text = db_new.Seat.Count().ToString();
            seatAvi.Text = db_new.Seat.Where(x => x.status == 1).Count().ToString();

            int count = 0;
            double pm = BathClass.get_paid_expense(db_new, thisTime, ref count);
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

        //打印小票
        private void detect_shoe_msg()
        {
            while (true)
            {
                try
                {
                    var dc = new BathDBDataContext(LogIn.connectionString);
                    var msg = dc.ShoeMsg.FirstOrDefault(x => !x.processed);
                    if (msg == null)continue;

                    List<string> seats = msg.text.Split('|').ToList();
                    PrintBill.Print_DataGridView(seats, msg.payEmployee, msg.payTime.ToString(), companyName);

                    msg.processed = true;
                    dc.SubmitChanges();
                }
                catch
                {
                }
            }
        }

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("确定退出?") != DialogResult.Yes)
                return;

            this.Close();
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

            //m_Seat = db_new.Seat.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            //m_Seat.ordering = true;
            //db_new.SubmitChanges();

            OrderForm orderForm = new OrderForm(inputSeatForm.m_Seat, LogIn.m_User, LogIn.connectionString, false);
            orderForm.ShowDialog();

            //m_Seat.ordering = false;
            //db_new.SubmitChanges();
        }

        //F6开牌
        private void tool_open_seat()
        {
            if (tSeat.Text == "")
                return;

            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            string text = tSeat.Text;
            tSeat.Text = "";

            var seat1 = dao.get_seat("text", text);
            var seat2 = dao.get_seat("oId", text);
            //var seat1 = db_new.Seat.FirstOrDefault(x => x.text == text);
            //var seat2 = db_new.Seat.FirstOrDefault(x => x.oId == text);
            if (seat1 == null && seat2 == null)
            {
                BathClass.printErrorMsg("手牌不可用!");
                return;
            }

            if (seat1 != null)
            {
                //var mtype = db_new.SeatType.FirstOrDefault(x => x.id == seat1.typeId);
                var mtype = dao.get_seattype("id", seat1.typeId);
                switch (seat1.status)
                {
                    case SeatStatus.AVILABLE://可用
                    case SeatStatus.PAIED://已经结账
                        if (!MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号开牌, false) && mtype.menuId != null)
                        {
                            BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                            break;
                        }
                        open_seat(seat1);
                        //update_seats_ui();
                        break;
                    case SeatStatus.USING://正在使用
                    case SeatStatus.DEPOSITLEFT://押金离场
                        if (MConvert<bool>.ToTypeOrDefault(seat1.paying, false))
                        {
                            BathClass.printErrorMsg("正在结账!");
                            break;
                        }
                        //if (BathClass.ToBool(seat1.ordering))
                        //{
                        //    BathClass.printErrorMsg("正在录单!");
                        //    break;
                        //}
                        if (!dao.get_authority(LogIn.m_User, "完整点单") &&
                            !dao.get_authority(LogIn.m_User, "可见本人点单"))
                        {
                            BathClass.printErrorMsg("权限不够");
                            break;
                        }

                        //seat1.ordering = true;
                        //db_new.SubmitChanges();

                        OrderForm orderForm = new OrderForm(seat1, LogIn.m_User, LogIn.connectionString, false);
                        orderForm.ShowDialog();

                        //seat1.ordering = false;
                        //db_new.SubmitChanges();
                        break;
                    case SeatStatus.LOCKING://锁定
                        BathClass.printErrorMsg("台位已经锁定!");
                        break;
                    case SeatStatus.STOPPED://停用
                        BathClass.printErrorMsg("台位已经停用!");
                        break;
                    case SeatStatus.WARNING://警告
                        BathClass.printErrorMsg("台位已经警告!");
                        break;
                    case SeatStatus.REPAIED:
                        BathClass.printErrorMsg("补救台位不能录单");
                        break;
                    default:
                        break;
                }
            }
            else if (seat2 != null)
            {
                //var mtype = db_new.SeatType.FirstOrDefault(x => x.id == seat2.typeId);
                var mtype = dao.get_seattype("id", seat2.typeId);
                switch (seat2.status)
                {
                    case SeatStatus.AVILABLE://可用
                    case SeatStatus.PAIED://已经结账
                        open_seat(seat2);
                        break;
                    case SeatStatus.USING://正在使用
                    case SeatStatus.DEPOSITLEFT://押金离场
                        if (MConvert<bool>.ToTypeOrDefault(seat2.paying, false))
                        {
                            BathClass.printErrorMsg("正在结账!");
                            break;
                        }
                        //if (BathClass.ToBool(seat2.ordering))
                        //{
                        //    BathClass.printErrorMsg("正在录单!");
                        //    break;
                        //}
                        if (!dao.get_authority(LogIn.m_User, "完整点单") &&
                            !dao.get_authority(LogIn.m_User, "可见本人点单"))
                        {
                            BathClass.printErrorMsg("权限不够");
                            break;
                        }

                        //seat2.ordering = true;
                        //db_new.SubmitChanges();

                        OrderForm orderForm = new OrderForm(seat2, LogIn.m_User, LogIn.connectionString, false);
                        orderForm.ShowDialog();

                        //seat2.ordering = false;
                        //db_new.SubmitChanges();
                        break;
                    case SeatStatus.LOCKING://锁定
                        BathClass.printErrorMsg("台位已经锁定!");
                        break;
                    case SeatStatus.STOPPED://停用
                        BathClass.printErrorMsg("台位已经停用!");
                        break;
                    case SeatStatus.WARNING://警告
                        BathClass.printErrorMsg("台位已经警告!");
                        break;
                    case SeatStatus.REPAIED:
                        BathClass.printErrorMsg("补救台位不能录单");
                        break;
                    default:
                        break;
                }
            }
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
                case Keys.Space:
                    if (!auto_seat_card)
                        open_seat_noauto();
                    break;
                case Keys.F2:
                    orderTool_Click(null, null);
                    break;
                case Keys.F3:
                    cancelSeatTool_Click(null, null);
                    break;
                case Keys.F4:
                    changeSeatTool_Click(null, null);
                    break;
                case Keys.F6:
                    changePwdTool_Click(null, null);
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

        //取消开牌
        private void cancelSeatTool_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);

            InputSeatForm inputSeatForm = new InputSeatForm(2);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;
            //var seat = db_new.Seat.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            cancel_open(inputSeatForm.m_Seat);
        }

        //更换手牌
        private void changeSeatTool_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            InputSeatForm inputSeatForm = new InputSeatForm(2);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;
            //var seat = db_new.Seat.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            change_seat(inputSeatForm.m_Seat);
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

        private void change_seat(CSeat seat)
        {
            if (!dao.get_authority(LogIn.m_User, "更换手牌"))
            {
                BathClass.printErrorMsg(LogIn.m_User.name + "不具有更换手牌操作权限!");
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

            //Seat newSeat = db_new.Seat.FirstOrDefault(x => x == inputSeatForm.m_Seat);
            StringBuilder sb = new StringBuilder();
            sb.Append(@"update [Orders] set text='" + inputSeatForm.m_Seat.text + "' where systemId='" + seat.systemId + "' ");
            //var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            //foreach (Orders order in orders)
            //    order.text = newSeat.text;

            sb.Append(@"update [Seat] set systemId='" + seat.systemId + "'");
            sb.Append(",openTime=getdate(),openEmployee='" + LogIn.m_User.id + "',chainId='" + seat.chainId
                + "',status=" + (int)seat.status + ",ordering='False'");
            if (seat.name != null && seat.name != "")
                sb.Append(",name='" + seat.name + "'");

            if (seat.population != null)
                sb.Append(",population=" + seat.population);

            if (seat.note != null && seat.name != "")
                sb.Append(",note='" + seat.note + "'");

            sb.Append(" where id=" + inputSeatForm.m_Seat.id);
            //newSeat.systemId = seat.systemId;
            //newSeat.name = seat.name;
            //newSeat.population = seat.population;
            //newSeat.openTime = BathClass.Now(LogIn.connectionString);
            //newSeat.openEmployee = LogIn.m_User.name;
            ////newSeat.money = seat.money;
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
            dgv_shoe_show();
        }

        //更换手牌
        private void CtxChangeSeat_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);
            change_seat(seat);
            
        }
        private void cancel_open(CSeat seat)
        {
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

                if (cancel_open_delay != null && (dao.Now() - seat.openTime.Value).TotalMinutes >= Convert.ToDouble(cancel_open_delay))
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

                string user_id;
                if (inputEmployee.employee != null)
                    user_id = inputEmployee.employee.id;
                else
                    user_id = LogIn.m_User.id;

                StringBuilder sb = new StringBuilder();
                sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
                sb.Append(user_id + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime.ToString());
                sb.Append("','取消开牌',getdate())");
                //Operation op = new Operation();

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

                //db_new.SystemIds.DeleteAllOnSubmit(db_new.SystemIds.Where(x => x.systemId == seat.systemId));
                //db_new.SubmitChanges();
                if (!dao.execute_command(sb.ToString()))
                {
                    BathClass.printErrorMsg("取消开牌失败!");
                    return;
                }
                dgv_shoe_show();
            }
            else
            {
                BathClass.printErrorMsg("不具有取消开牌权限!");
            }
        }

        //取消开牌
        private void CtxCancelOpen_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);
            cancel_open(seat);
        }

        //新增联排
        private void CtxChain_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = getContextSenderSeat(sender);
            OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            openSeatForm.ShowDialog();
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

            if (!dao.execute_command("update [Seat] set note='"+noteForm.note+"' where text='"+seat.text+"'"))
            {
                BathClass.printErrorMsg("手牌添加备注失败!");
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

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputEmployee.employee, "锁定解锁"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.USING)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"update [Seat] set status=4 where text='" + seat.text + "'");
                if (!dao.execute_command(sb.ToString()))
                {
                    BathClass.printErrorMsg("手牌锁定失败");
                    return;
                }
            }
            else if (seat.status == SeatStatus.LOCKING)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('");
                sb.Append(inputEmployee.employee.name + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime + "','解锁手牌',getdate())");
                if (seat.systemId == null || seat.systemId == "")
                    sb.Append(@" update [Seat] set status=1 where text='" + seat.text + "'");
                else
                    sb.Append(@" update [Seat] set status=2 where text='" + seat.text + "'");

                if (!dao.execute_command(sb.ToString()))
                {
                    BathClass.printErrorMsg("手牌解锁失败！");
                    return;
                }
                //Operation op = new Operation();
                //op.employee = inputEmployee.employee.name;
                //op.seat = seat.text;
                //op.openEmployee = seat.openEmployee;
                //op.openTime = seat.openTime;
                //op.explain = "解锁手牌";
                //op.opTime = BathClass.Now(LogIn.connectionString);
                //db_new.Operation.InsertOnSubmit(op);

                //if (seat.systemId == null)
                //    seat.status = 1;
                //else
                //    seat.status = 2;
            }

            //db_new.SubmitChanges();
        }

        //停用启用
        private void CtxStop_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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
                seat.status == SeatStatus.DEPOSITLEFT || seat.status == SeatStatus.REPAIED)
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
                //seat.status = 5;
            }
            else if (seat.status == SeatStatus.STOPPED)
            {
                string cmd_str = @"update [Seat] set status=" + (int)SeatStatus.AVILABLE + " where text='" + seat.text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("手牌启用失败!");
                    return;
                }
                //seat.status = 1;
            }

            //db_new.SubmitChanges();
        }
        #endregion

        private void toolOpen_Click(object sender, EventArgs e)
        {
            if (seatLock && !auto_seat_card)
                open_seat_noauto();
            else
                tool_open_seat();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //    return;
            //else
            //    createSeat(seatPanel);
        }

        //预警
        private void toolWarn_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("确定预警?") != DialogResult.Yes)
                return;

            if (!dao.execute_command("insert into [RoomWarn](msg) values('警告')"))
            {
                BathClass.printErrorMsg("预警失败");
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
            if (seatLock && lock_type == "锦衣卫")
                JYW.CloseReader();
        }

        private void tSeat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //修改密码
        private void changePwdTool_Click(object sender, EventArgs e)
        {
            ModifyPwdForm form = new ModifyPwdForm();
            form.ShowDialog();
        }
    }
}
