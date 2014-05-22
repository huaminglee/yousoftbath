using System;
using System.IO;
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
using System.Media;
using YouSoftBathConstants;
using System.Timers;
using System.Threading;

using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class MainForm : Form
    {
        //成员变量
        private DAO dao;
        private CSeat m_Seat = null;
        //private static System.Timers.Timer updateTimer;//刷新线程
        private Thread m_thread_update_seat;//刷新手牌线程
        private Thread m_thread_msg;//吧台消息检测线程
        private Thread m_thread_wait_over;//派遣技师之后等待超时检测
        private Thread m_thread_clearMemory;

        private int msg_delay;//等待时限
        private SoundPlayer sp = new SoundPlayer();

        //构造函数
        public MainForm()
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

            this.Text = Constants.appName + "-吧台" + Constants.version + " 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户:" + LogIn.m_User.id + "  " + LogIn.m_User.name;
            //CFormCreate.createSeat(db, seatPanel, seateTab, new System.EventHandler(btn_Click), null);
            var td = new Thread(new ThreadStart(initial_ui_thread));
            td.IsBackground = true;
            td.Start();

            if (MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false))
            {
                msg_delay = LogIn.options.包房等待时限.HasValue ?
                    LogIn.options.包房等待时限.Value : 10;

                m_thread_msg = new Thread(new ThreadStart(msgTimer_Elapsed));
                m_thread_msg.IsBackground = true;
                m_thread_msg.Start();

                m_thread_wait_over = new Thread(new ThreadStart(room_wait_over_detect));
                m_thread_wait_over.IsBackground = true;
                m_thread_wait_over.Start();

                btnCab.Visible = false;
                cabL.Visible = false;
            }
            else
            {
                roomL.Visible = false;
                btnRoom.Visible = false;

                callL.Visible = false;
                toolCall.Visible = false;

                toolMsg.Visible = false;
                msgL.Visible = false;

                toolWarn.Visible = false;
            }

            m_thread_update_seat = new Thread(new ThreadStart(update_seats_ui));
            m_thread_update_seat.IsBackground = true;
            m_thread_update_seat.Start();

            m_thread_clearMemory = new Thread(new ThreadStart(clear_Memory));
            m_thread_clearMemory.IsBackground = true;
            m_thread_clearMemory.Start();
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
            CFormCreate.createSeatByDao(dao, LogIn.options, seatPanel, seateTab, new EventHandler(btn_Click),new EventHandler(btn_MouseHover), null, null);
            setStatus();
        }

        //刷新线程
        private void update_seats_ui()
        {
            while (true)
            {
                try
                {
                    var seats = dao.get_seats(null, null);
                    var seats_id_tmp = seats.Select(x => x.id).ToList();
                    var seats_status_tmp = seats.Select(x => x.status).ToList();
                    //var db_new = new BathDBDataContext(LogIn.connectionString);
                    //var seats_id_tmp = db_new.Seat.Select(x => x.id).ToList();
                    //var seats_status_tmp = db_new.Seat.Select(x => x.status).ToList();

                    bool changed = false;
                    for (int i = 0; i < seats_id_tmp.Count; i++)
                    {
                        var btn = (Button)seatPanel.Controls.Find(seats_id_tmp[i].ToString(), false).FirstOrDefault();
                        if (btn == null)
                        {
                            foreach (TabPage tp in seateTab.Controls)
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
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.White });
                                }
                                break;
                            case SeatStatus.USING://正在使用
                                if (btn.BackColor != Color.Cyan)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Cyan });
                                }
                                break;
                            case SeatStatus.PAIED://已经结账
                                if (btn.BackColor != Color.Gray)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Gray });
                                }
                                break;
                            case SeatStatus.LOCKING://锁定
                                if (btn.BackColor != Color.Orange)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Orange });
                                }
                                break;
                            case SeatStatus.STOPPED://停用
                                if (btn.BackColor != Color.Red)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Red });
                                }
                                break;
                            case SeatStatus.WARNING://警告
                                if (btn.BackColor != Color.Yellow)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Yellow });
                                }
                                break;
                            case SeatStatus.DEPOSITLEFT://押金离场
                                if (btn.BackColor != Color.Violet)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Violet });
                                }
                                break;
                            case SeatStatus.REPAIED://重新结账
                                if (btn.BackColor != Color.CornflowerBlue)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.CornflowerBlue });
                                }
                                break;
                            case SeatStatus.RESERVE://预约客房
                                if (btn.BackColor != Color.SpringGreen)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.SpringGreen });
                                }
                                break;
                            default:
                                break;
                        }
                    }

                    if (changed)
                    {
                        this.Invoke(new no_par_delegate(setStatus), null);
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
        
        //每隔1分钟监听一次,吧台消息
        private void msgTimer_Elapsed()
        {
            while (true)
            {
                try
                {
                    //var dc_check = new BathDBDataContext(LogIn.connectionString);

                    var barMsg = dao.get_barMsg("[read]='False'");
                    //var barMsg = dc_check.BarMsg.FirstOrDefault(x => !x.read.Value);
                    if (barMsg != null)
                    {
                        play();
                        this.Invoke(new show_msg_delegate(show_msg), new object[] { "房间号：" + barMsg.roomId.Trim() + ",手牌号:" + barMsg.seatId + barMsg.msg });
                        //barMsg.read = true;
                        //dc_check.SubmitChanges();
                        dao.execute_command("update [BarMsg] set [read]='True' where id=" + barMsg.id);
                        sp.Stop();
                    }
                }
                catch{}
            }
        }

        //包厢派遣技师之后等待超时检测
        private void room_wait_over_detect()
        {
            while (true)
            {
                try
                {
                    //var dc_check = new BathDBDataContext(LogIn.connectionString);
                    var rooms = dao.get_rooms("status like '%等待服务%'");
                    foreach (var room in rooms)
                    {
                        var tmp_s = room.status.Split('|');
                        var tmp_o = room.orderTime.Split('|');
                        for (int i = 0; i < tmp_s.Length; i++)
                        {
                            if (tmp_s[i] == "等待服务")
                            {
                                try
                                {
                                    var st = Convert.ToDateTime(tmp_o[i]);
                                    if ((DateTime.Now - st).TotalMinutes >= msg_delay)
                                    {
                                        play();
                                        this.Invoke(new watchSeatDelegate(roomwaitover_show));
                                        sp.Stop();
                                        Thread.Sleep(5 * 60 * 1000);
                                    }
                                }
                                catch{}
                            }
                        }
                    }
                }
                catch{}
            }
            
        }

        private void roomwaitover_show()
        {
            RoomWaitOverForm form = new RoomWaitOverForm();
            form.ShowDialog();
        }

        //播放声音
        private void play()
        {
            if (!File.Exists(@"msg.wav"))
                return;

            sp.SoundLocation = @"msg.wav";
            sp.PlayLooping();
        }

        private delegate void show_msg_delegate(string msg);
        private delegate void watchSeatDelegate();
        private void show_msg(string msg)
        {
            InformationDlg dlg = new InformationDlg(msg);
            dlg.ShowDialog();
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

            SeatStatus status = m_Seat.status;
            switch (status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                    BathClass.printErrorMsg("手牌未开牌");
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.DEPOSITLEFT://押金离场
                    if (!dao.get_authority(LogIn.m_User, "完整点单") &&
                        !dao.get_authority(LogIn.m_User, "可见本人点单"))
                    {
                        BathClass.printErrorMsg("权限不够!");
                        break;
                    }

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

                    //m_Seat.ordering = true;
                    //db_new.SubmitChanges();

                    OrderForm orderForm = new OrderForm(m_Seat, LogIn.m_User, LogIn.connectionString, false);
                    orderForm.ShowDialog();

                    //m_Seat.ordering = false;
                    //db_new.SubmitChanges();

                    break;
                case SeatStatus.LOCKING://锁定
                    BathClass.printErrorMsg("手牌已经锁定!");
                    break;
                case SeatStatus.STOPPED://停用
                    BathClass.printErrorMsg("台位已经停用!");
                    break;
                case SeatStatus.WARNING://警告
                    OrderCheckForm orderCheckForm = new OrderCheckForm(m_Seat, LogIn.connectionString, LogIn.options);
                    orderCheckForm.ShowDialog();
                    break;
                case SeatStatus.REPAIED:
                    BathClass.printErrorMsg("补救台位不能录单");
                    break;
                default:
                    break;
            }
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

        //F6开牌
        private void tool_open_seat()
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            string text = tSeat.Text;
            m_Seat = dao.get_seat("text='" + text + "' or oId='" + text + "'");
            //m_Seat = db_new.Seat.FirstOrDefault(x => x.text == text || x.oId == text);
            if (m_Seat == null)
            {
                BathClass.printErrorMsg("手牌不可用!");
                return;
            }

            switch (m_Seat.status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                    BathClass.printErrorMsg("手牌未开牌");
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.DEPOSITLEFT://押金离场
                    if (!dao.get_authority(LogIn.m_User, "完整点单") &&
                        !dao.get_authority(LogIn.m_User, "可见本人点单"))
                    {
                        BathClass.printErrorMsg("权限不够!");
                        break;
                    }
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

                    //m_Seat.ordering = true;
                    //db_new.SubmitChanges();

                    OrderForm orderForm = new OrderForm(m_Seat, LogIn.m_User, LogIn.connectionString, false);
                    orderForm.ShowDialog();
                    
                    //m_Seat.ordering = false;
                    //db_new.SubmitChanges();
                    break;
                case SeatStatus.LOCKING://锁定
                    BathClass.printErrorMsg("手牌已经锁定!");
                    break;
                case SeatStatus.STOPPED://停用
                    BathClass.printErrorMsg("手牌已经停用!");
                    break;
                case SeatStatus.WARNING://警告
                    OrderCheckForm orderCheckForm = new OrderCheckForm(m_Seat, LogIn.connectionString,LogIn.options);
                    orderCheckForm.ShowDialog();
                    break;
                case SeatStatus.REPAIED:
                    BathClass.printErrorMsg("补救台位不能录单");
                    break;
                default:
                    break;
            }

            tSeat.Text = "";
        }

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

            seatUnpaid.Text = db_new.Seat.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status==8).Count().ToString();
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

            //var seat = dao.get_seat("text", inputSeatForm.m_Seat.text);
            //var seat = db_new.Seat.FirstOrDefault(x => x.text == inputSeatForm.m_Seat.text);
            //seat.ordering = true;
            //db_new.SubmitChanges();
            
            OrderForm orderForm = new OrderForm(inputSeatForm.m_Seat, LogIn.m_User, LogIn.connectionString, false);
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

            RoomViewForm rmvForm = new RoomViewForm();
            rmvForm.ShowDialog();
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("是否退出?") == DialogResult.Yes)
                this.Close();
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
                case Keys.F2:
                    orderTool_Click(null, null);
                    break;
                case Keys.F4:
                    btnRoom_Click(null, null);
                    break;
                case Keys.F10:
                    toolPwd_Click(null, null);
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

        /*#region 右键

        //获取右键点击的台位
        private Seat getContextSenderSeat(BathDBDataContext db_new, object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip cmenu = item.GetCurrentParent() as ContextMenuStrip;
            Button bt = cmenu.SourceControl as Button;
            return db_new.Seat.FirstOrDefault(x => x.text == bt.Text);
        }

        //更换手牌
        private void CtxChangeSeat_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            Seat seat = getContextSenderSeat(db_new, sender);

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
            InputSeatForm inputSeatForm = new InputSeatForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            Seat newSeat = db_new.Seat.FirstOrDefault(x => x == inputSeatForm.m_Seat);
            var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            foreach (Orders order in orders)
                order.text = newSeat.text;

            newSeat.systemId = seat.systemId;
            newSeat.name = seat.name;
            newSeat.population = seat.population;
            newSeat.openTime = BathClass.Now(LogIn.connectionString);
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
            Seat seat = getContextSenderSeat(db_new, sender);

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

            if (db_new.Options.Count() != 0)
            {
                Options options = db_new.Options.FirstOrDefault();
                var q = options.取消开牌时限;
                if (q != null && BathClass.Now(LogIn.connectionString)-seat.openTime>=TimeSpan.Parse("00:"+q.ToString()+":00"))
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

            Seat seat = getContextSenderSeat(db_new, sender);
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
            Seat seat = getContextSenderSeat(db_new, sender);

            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.employee, "锁定解锁"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }


            if (seat.status == 1 || seat.status == 2)
                seat.status = 4;
            else if (seat.status == 4)
            {
                Operation op = new Operation();
                op.employee = inputEmployee.employee.name;
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
            Seat seat = getContextSenderSeat(db_new, sender);

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
            Seat seat = getContextSenderSeat(db_new, sender);

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

            var act = db_new.Account.FirstOrDefault(x => x.systemId.Contains(seat.systemId) && x.abandon == null);
            var ids = act.systemId.Split('|');
            var seats = db_new.Seat.Where(x => ids.Contains(x.systemId));
            foreach (var s in seats)
            {
                if (s.status == 2 || s.status == 6 || s.status == 7)
                {
                    BathClass.printErrorMsg("已经重新开牌，请先更换手牌");
                    return;
                }
                s.status = 8;
                var orders = db_new.Orders.Where(x => x.systemId == s.systemId);
                foreach (var order in orders)
                    order.paid = false;
            }
            act.abandon = inputEmployee.employee.id;
            var cc = db_new.CardCharge.FirstOrDefault(x => act.systemId==x.CC_AccountNo);
            if (cc != null)
                db_new.CardCharge.DeleteOnSubmit(cc);
            db_new.SubmitChanges();
        }

        #endregion*/

        //查看联排
        private void toolChain_Click(object sender, EventArgs e)
        {
            ChainForm chainForm = new ChainForm();
            chainForm.ShowDialog();
        }

        //技师管理
        private void toolTech_Click(object sender, EventArgs e)
        {
            //var db_new = new BathDBDataContext(LogIn.connectionString);
            if (!dao.get_authority(LogIn.m_User, "技师管理"))
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
            //var db_new = new BathDBDataContext(LogIn.connectionString);
            if (!dao.get_authority(LogIn.m_User, "录单汇总"))
            {
                BathClass.printErrorMsg("不具有权限");
                return;
            }

            TableOrderTableForm orderTableForm = new TableOrderTableForm(LogIn.connectionString, LogIn.m_User);
            orderTableForm.ShowDialog();
        }

        //修改密码
        private void toolPwd_Click(object sender, EventArgs e)
        {
            ModifyPwdForm pwdForm = new ModifyPwdForm();
            pwdForm.ShowDialog();
        }

        private void ReceptionSeatForm_StyleChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //    return;
            //else
            //    createSeat(seatPanel);
        }

        //包厢消息汇总
        private void toolMsg_Click(object sender, EventArgs e)
        {
            BarMsgForm form = new BarMsgForm();
            form.ShowDialog();
        }

        //催钟
        private void toolCall_Click(object sender, EventArgs e)
        {
            InformRoomForm room = new InformRoomForm(LogIn.connectionString);
            room.ShowDialog();
        }

        private void toolWarn_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("确定预警?") != DialogResult.Yes)
                return;

            if (!dao.execute_command("insert into [RoomWarn](msg) values('警告')"))
            {
                BathClass.printErrorMsg("预警失败!");
                return;
            }
            //var dc_new = new BathDBDataContext(LogIn.connectionString);
            //var warn = new RoomWarn();
            //warn.msg = "警告";
            //dc_new.RoomWarn.InsertOnSubmit(warn);
            //dc_new.SubmitChanges();
        }

        private void tSeat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //包厢管理
        private void btnCab_Click(object sender, EventArgs e)
        {
            CabViewForm form = new CabViewForm();
            form.ShowDialog();
        }

        private void ReceptionSeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
