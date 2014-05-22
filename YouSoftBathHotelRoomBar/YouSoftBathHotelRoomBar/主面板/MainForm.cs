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

using System.Timers;
using System.Threading;

using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class MainForm : Form
    {
        //成员变量
        private HotelRoom m_HotelRoom = null;
        private List<int> all_rooms;//手牌状态
        private List<int> all_rooms_id;//手牌id

        private Thread m_thread_update_room;//刷新客房线程
        private bool m_close = false;
        private Options ops;

        //手牌大小控制
        private static int btn_size = 45;
        private static int btn_space = 13;

        //private System.Timers.Timer msgTimer;//检测消息
        //private System.Timers.Timer roomTimer;//检测等待超时

        //private int msg_delay;//等待时限
        //private SoundPlayer sp = new SoundPlayer();

        //构造函数
        public MainForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void ReceptionSeatForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);

            this.Text = "咱家店小二-吧台V2.1 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户:" + LogIn.m_User.id + "  " + LogIn.m_User.name;
            ops = db.Options.FirstOrDefault();
            createRooms(db, roomPanel);

            roomL.Visible = false;
            btnRoom.Visible = false;

            callL.Visible = false;
            toolCall.Visible = false;

            toolMsg.Visible = false;
            msgL.Visible = false;

            toolWarn.Visible = false;
            all_rooms = db.HotelRoom.Select(x => x.status).ToList();
            all_rooms_id = db.HotelRoom.Select(x => x.id).ToList();

            m_thread_update_room = new Thread(new ThreadStart(update_rooms_ui));
            m_thread_update_room.Start();
        }

        //刷新线程
        private void update_rooms_ui()
        {
            while (true)
            {
                if (m_close)
                    break;
                try
                {
                    var db_new = new BathDBDataContext(LogIn.connectionString);
                    var seats_tmp = db_new.HotelRoom.Select(x => x.status).ToList();

                    bool changed = false;
                    for (int i = 0; i < seats_tmp.Count; i++)
                    {
                        if (seats_tmp[i] != all_rooms[i])
                        {
                            var btn = roomPanel.Controls.Find(all_rooms_id[i].ToString(), false).FirstOrDefault();
                            var btns = btn as Button;
                            btn_status(btns, seats_tmp[i]);
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        setStatus();
                        all_rooms = seats_tmp;
                    }
                }
                catch
                {
                }
            }
        }

        //刷新线程
        /*private void update_seats_ui()
        {
            while (true)
            {
                if (m_close)
                    break;
                try
                {
                    var db_new = new BathDBDataContext(LogIn.connectionString);
                    var seats_tmp = db_new.Seat.Select(x => x.status).ToList();

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
                {
                }
            }
        }*/
        
        //每隔1分钟监听一次,吧台消息
        /*private void msgTimer_Elapsed(object sender, EventArgs e)
        {
            msgTimer.Stop();
            var dc_check = new BathDBDataContext(LogIn.connectionString);

            var barMsg = dc_check.BarMsg.FirstOrDefault(x => !x.read.Value);
            if (barMsg != null)
            {
                play();
                this.Invoke(new show_msg_delegate(show_msg), new object[] { "房间号：" + barMsg.roomId.Trim() + "," + barMsg.msg});
                barMsg.read = true;
                dc_check.SubmitChanges();
                sp.Stop();
            }

            msgTimer.Start();
        }

        //每隔1分钟监听一次,吧台消息
        private void roomTimer_Elapsed(object sender, EventArgs e)
        {
            var dc_check = new BathDBDataContext(LogIn.connectionString);
            roomTimer.Stop();
            if (dc_check.Room.Any(x => x.status == "等待服务" && (DateTime.Now - x.orderTime.Value).TotalMinutes >= msg_delay))
            {
                play();
                this.Invoke(new watchSeatDelegate(roomwaitover_show));
                sp.Stop();
            }
            roomTimer.Start();
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
            //MessageBox.Show(msg, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }*/

        //监控台位数据库改变
        /*private void watchSeat()
        {
            if (m_connection == null)
                m_connection = new SqlConnection(LogIn.connectionString);

            if (m_connection.State != ConnectionState.Open)
                m_connection.Open();

            SqlCommand cmd = m_connection.CreateCommand();
            cmd.Notification = null;//清除 
            cmd.CommandText = "select id, oId, text, typeId, systemId, openTime, openEmployee, payTime, payEmployee, chainId, status From dbo.Seat";

            //监控台位数据库
            SqlDependency dependency = new SqlDependency(cmd);
            dependency.OnChange += new OnChangeEventHandler(seat_OnChange);

            //SqlDependency绑定的SqlCommand对象必须要执行一下，才能将SqlDependency对象的HasChange属性设为true
            SqlDataAdapter thisAdapter = new SqlDataAdapter(cmd);
            DataSet posDataSet = new DataSet();
            thisAdapter.Fill(posDataSet, "Seat");

            //刷新台位信息
            if (this.WindowState != FormWindowState.Minimized)
                update_seats();
            //刷新状态栏
            setStatus();
        }*/

        /*
        
        //监控台位数据库改变
        private void seat_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //因为是子线程，需要用invoke方法更新ui 
            if (this.InvokeRequired)
            {
                this.Invoke(new watchSeatDelegate(watchSeat), null);
            }
            else
            {
                watchSeat();
            }

            SqlDependency dependency = (SqlDependency)sender;
            //通知之后，当前dependency失效，需要重新设置通知 
            dependency.OnChange -= seat_OnChange;
        }*/

        //创建单个台位按钮
        private void createButton(int x, int y, Seat table, Control sp)
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
            //btn.ContextMenuStrip = seatContext;
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

            m_HotelRoom = db_new.HotelRoom.FirstOrDefault(x => x.text == btn.Text);
            int status = m_HotelRoom.status;
            switch (status)
            {
                case 1://可用
                case 3://已经结账
                    BathClass.printErrorMsg("手牌未开牌");
                    break;
                case 2://正在使用
                case 7://押金离场
                    if (!BathClass.getAuthority(db_new, LogIn.m_User, "完整点单") &&
                        !BathClass.getAuthority(db_new, LogIn.m_User, "可见本人点单"))
                    {
                        BathClass.printErrorMsg("权限不够!");
                        break;
                    }

                    if (m_HotelRoom.paying.HasValue && m_HotelRoom.paying.Value)
                    {
                        BathClass.printErrorMsg("正在结账!");
                        break;
                    }

                    m_HotelRoom.ordering = true;
                    db_new.SubmitChanges();

                    var orderForm = new HotelRoomOrderForm(m_HotelRoom);
                    orderForm.ShowDialog();

                    m_HotelRoom.ordering = false;
                    db_new.SubmitChanges();

                    break;
                case 4://锁定
                    BathClass.printErrorMsg("手牌已经锁定!");
                    break;
                case 5://停用
                    BathClass.printErrorMsg("台位已经停用!");
                    break;
                case 6://警告
                    var orderCheckForm = new HotelRoomOrderCheckForm(m_HotelRoom);
                    orderCheckForm.ShowDialog();
                    break;
                case 8:
                    BathClass.printErrorMsg("补救台位不能录单");
                    break;
                default:
                    break;
            }
        }

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
            //btn.ContextMenuStrip = seatContext;
            btn.TabStop = false;
            btn.Click += new System.EventHandler(btn_Click);
            btn_status(btn, table.status);

            sp.Controls.Add(btn);
        }

        //创建台位
        private void createRooms(BathDBDataContext dc, Control sp)
        {
            var seats = dc.HotelRoom.OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();

            int tableCountSp = sp.Controls.Count;
            int tableCount = seats.Count;

            Size clientSize = roomPanel.Size;
            int nR = (clientSize.Width - btn_space) / (btn_size + btn_space);
            int theRow = tableCountSp / nR;
            int theCol = tableCountSp - theRow * nR;

            //var sLst = db.Seat.OrderBy(x => x.text).ToList();
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

            ////减少
            //while (theCount > tableCount)
            //{
            //    sp.Controls.RemoveAt(sp.Controls.Count - 1);
            //    theCount--;
            //}

            ////不变
            //for (int i = 0; i < tableCount; i++)
            //{
            //    ((Button)sp.Controls[i]).Text = seats[i].text;
            //    btn_status((Button)sp.Controls[i], seats[i]);
            //}
        }

        //F6开牌
        private void tool_open_seat()
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            string text = tSeat.Text;
            tSeat.Text = "";
            m_HotelRoom = db_new.HotelRoom.FirstOrDefault(x => x.text == text || x.oId == text);
            if (m_HotelRoom == null)
            {
                BathClass.printErrorMsg("手牌不可用!");
                return;
            }

            switch (m_HotelRoom.status)
            {
                case 1://可用
                case 3://已经结账
                    BathClass.printErrorMsg("手牌未开牌");
                    break;
                case 2://正在使用
                case 7://押金离场
                    if (!BathClass.getAuthority(db_new, LogIn.m_User, "完整点单") &&
                        !BathClass.getAuthority(db_new, LogIn.m_User, "可见本人点单"))
                    {
                        BathClass.printErrorMsg("权限不够!");
                        break;
                    }
                    if (m_HotelRoom.paying.HasValue && m_HotelRoom.paying.Value)
                    {
                        BathClass.printErrorMsg("正在结账!");
                        break;
                    }

                    m_HotelRoom.ordering = true;
                    db_new.SubmitChanges();

                    HotelRoomOrderForm orderForm = new HotelRoomOrderForm(m_HotelRoom);
                    orderForm.ShowDialog();

                    m_HotelRoom.ordering = false;
                    db_new.SubmitChanges();
                    break;
                case 4://锁定
                    BathClass.printErrorMsg("手牌已经锁定!");
                    break;
                case 5://停用
                    BathClass.printErrorMsg("手牌已经停用!");
                    break;
                case 6://警告
                    HotelRoomOrderCheckForm orderCheckForm = new HotelRoomOrderCheckForm(m_HotelRoom);
                    orderCheckForm.ShowDialog();
                    break;
                case 8:
                    BathClass.printErrorMsg("补救台位不能录单");
                    break;
                default:
                    break;
            }

            tSeat.Text = "";
        }

        private void setStatus()
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
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

        #region 右键

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

        //重打账单
        private void CtxReprint_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            Seat seat = getContextSenderSeat(db_new, sender);

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

            var ids = account.systemId.Split('|');
            var orders = db_new.Orders.Where(x => ids.Contains(x.systemId) && x.paid && x.deleteEmployee == null);
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

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");
            PrintBill.Print_DataGridView(account, "结账单", dgv, printCols, true, null, ops.companyName);
            PrintBill.Print_DataGridView(account, "结账单", dgv, printCols, true, null, ops.companyName);
        }
        #endregion

        //查看联排
        private void toolChain_Click(object sender, EventArgs e)
        {
            ChainForm chainForm = new ChainForm();
            chainForm.ShowDialog();
        }

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
            InformRoomForm room = new InformRoomForm();
            room.ShowDialog();
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
            m_close = true;
        }
    }
}
