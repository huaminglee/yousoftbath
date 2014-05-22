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

using YouSoftBathGeneralClass;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using YouSoftBathFormClass;
using System.Transactions;
using System.Threading;
using System.Timers;

namespace YouSoftBathHotelRoom
{
    public partial class MainWindow : Form
    {
        //成员变量
        private HotelRoom m_Seat = null;
        private string companyName;

        private Control seatPanel;
        private List<int> all_seats;//手牌状态
        private List<int> all_seats_id;//手牌id

        private DateTime thisTime;//上一次夜审时间

        private Thread m_thread;//刷新线程
        private bool m_close = false;

        //手牌大小控制
        private static int btn_size = 45;
        private static int btn_space = 13;

        private bool seatLock;//启用欧亿达手牌锁
        private string lock_type;//手牌类型

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            all_seats = db.Seat.Select(x => x.status).ToList();
            all_seats_id = db.Seat.Select(x => x.id).ToList();

            this.Text = "咱家店小二-酒店客房开房系统V2.1 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户:" + LogIn.m_User.id + "  " + LogIn.m_User.name;

            var ops = db.Options.FirstOrDefault();
            seatLock = ops.启用手牌锁.Value;
            lock_type = ops.手牌锁类型;
            companyName = ops.companyName;

            get_clear_table_time(db);
            seatPanel = sp.Panel1;
            createSeat(seatPanel);
            setStatus();

            dgv_shoe_show();

            m_thread = new Thread(new ThreadStart(update_seats_ui));
            m_thread.Start();
        }

        //刷新线程
        private void update_seats_ui()
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
                        if (seats_tmp[i] != all_seats[i])
                        {
                            var btn = seatPanel.Controls.Find(all_seats_id[i].ToString(), false).FirstOrDefault();
                            var btns = btn as Button;
                            btn_status(btns, db_new.HotelRoom.FirstOrDefault(x => x.id == all_seats_id[i]));
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
        }

        //手牌线程
        private void seatTimer_Elapsed()
        {
            /*if (!seatLock)
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
            if (lock_type == "欧亿达")
            {
                str = Encoding.Default.GetString(buff, 0, 20).Trim();
                seat_text = str.Substring(str.Length - BathClass.lock_id_length);
            }
            else if (lock_type == "锦衣卫")
            {
                str = BathClass.byteToHexStr(buff);
                seat_text = str.Substring(17, BathClass.lock_id_length);
            }

            var db_new = new BathDBDataContext(LogIn.connectionString);
            var seat = db_new.Seat.FirstOrDefault(x => x.text == seat_text);
            if (seat == null)
            {
                BathClass.printErrorMsg("手牌" + seat_text + "不存在");
                return;
            }

            if (seat.status == 1 || seat.status == 3)
            {
                if ((lock_type == "欧亿达" && OYD.OYEDA_fk(buff) != 0) ||
                    (lock_type == "锦衣卫" && JYW.FK(buff) != 0))
                    return;

                open_one_seat(seat, db_new);
                open_seat(seat, db_new);
            }
            else if (seat.status == 2)
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();

                if (BathClass.getAuthority(db_new, LogIn.m_User, "取消开牌") ||
                    (inputEmployee.ShowDialog() != DialogResult.OK &&
                    BathClass.getAuthority(db_new, inputEmployee.employee, "取消开牌")))
                {
                    if (seat.status != 2)
                    {
                        BathClass.printErrorMsg("该台位不在使用中，不能取消开台!");
                        return;
                    }

                    if (db_new.Options.Count() != 0)
                    {
                        Options options = db_new.Options.ToList()[0];
                        var q = options.取消开牌时限;
                        if (q != null && BathClass.Now(LogIn.connectionString) - seat.openTime >= TimeSpan.Parse("00:" + q.ToString() + ":00"))
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

                    if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                    (lock_type == "锦衣卫" && JYW.MD(buff) != 0))
                        return;

                    Operation op = new Operation();

                    if (inputEmployee.employee != null)
                        op.employee = inputEmployee.employee.id;
                    else
                        op.employee = LogIn.m_User.id;
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
                else
                {
                    BathClass.printErrorMsg("不具有取消开牌权限!");
                }
            }*/
        }

        private void orderform_show(Seat seat)
        {
            OrderForm orderForm = new OrderForm(seat, LogIn.m_User, LogIn.connectionString);
            orderForm.ShowDialog();
        }

        //开单个牌，用于以刷卡方式
        private void open_one_seat(Seat seat, BathDBDataContext dc)
        {
            if (seat.status == 3)
            {
                BathClass.reset_seat(seat);
                dc.SubmitChanges();
            }

            seat.openEmployee = LogIn.m_User.id.ToString();
            seat.openTime = BathClass.Now(LogIn.connectionString);
            seat.systemId = BathClass.systemId(dc, LogIn.connectionString);
            //seat.chainId = chainId;
            seat.status = 2;

            //SeatType seatType = db.SeatType.FirstOrDefault(x => x.id == seat.typeId);
            //var menu = db.Menu.FirstOrDefault(x => x.id == seatType.menuId);

            //if (menu != null)
            //{
            //    Orders order = new Orders();
            //    order.menu = menu.name;
            //    order.text = seat.text;
            //    order.systemId = seat.systemId;
            //    order.number = 1;
            //    order.money = menu.price;
            //    order.inputTime = BathClass.Now(LogIn.connectionString);
            //    order.inputEmployee = LogIn.m_User.id.ToString();
            //    order.paid = false;
            //    dc.Orders.InsertOnSubmit(order);
            //}

            dc.SubmitChanges();
        }

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

        private delegate void open_seat_Delegate(Seat seat, BathDBDataContext dc);
        private delegate void orderform_show_Delegate(Seat seat);
        private delegate void watchSeatDelegate();

        //监控台位数据库改变
        /*private void seat_OnChange(object sender, SqlNotificationEventArgs e)
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
            btn_status(btn, table);
            btn.Click += new System.EventHandler(btn_Click);

            sp.Controls.Add(btn);
        }

        //生成按钮状态
        private void btn_status(Button btn, HotelRoom seat)
        {
            //var db_new = new BathDBDataContext(LogIn.connectionString);
            //var status = db_new.Seat.FirstOrDefault(x => x.text == seat.text).status;
            switch (seat.status)
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

        //获取夜审时间
        private void get_clear_table_time(BathDBDataContext dc)
        {
            var ct = dc.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault();
            if (ct == null)
                thisTime = DateTime.Parse("2013-01-01 00:00:00");
            else
                thisTime = ct.clearTime;
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

            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            m_Seat = db_new.HotelRoom.FirstOrDefault(x => x.text == btn.Text);
            var mtype = db_new.HotelRoomType.FirstOrDefault(x => x.id == m_Seat.typeId);

            switch (m_Seat.status)
            {
                case 1://可用
                case 3://已经结账
                    if (!BathClass.ToBool(db_new.Options.FirstOrDefault().允许手工输入手牌号开牌) && mtype.menuId != null)
                    {
                        BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                        break;
                    }
                    open_seat(m_Seat, db_new);
                    break;
                case 2://正在使用
                case 7://押金离场
                    if (!BathClass.getAuthority(db_new, LogIn.m_User, "完整点单") &&
                        !BathClass.getAuthority(db_new, LogIn.m_User, "可见本人点单"))
                    {
                        BathClass.printErrorMsg("权限不够");
                        break;
                    }
                    m_Seat.ordering = true;
                    db_new.SubmitChanges();

                    var orderForm = new HotelRoomOrderForm(m_Seat);
                    orderForm.ShowDialog();


                    m_Seat.ordering = false;
                    db_new.SubmitChanges();
                    break;
                case 4://锁定
                    break;
                case 5://停用
                    BathClass.printErrorMsg("台位已经停用!");
                    break;
                case 6://警告
                    var orderCheckForm = new HotelRoomOrderCheckForm(m_Seat);
                    orderCheckForm.ShowDialog();
                    break;
                case 8://重新结账
                    BathClass.printErrorMsg("补救台位不能录单");
                    break;
                default:
                    break;
            }
        }

        //开牌
        private void open_seat(HotelRoom seat, BathDBDataContext db_new)
        {
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "开牌"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            OpenSeatForm openSeatForm = new OpenSeatForm(seat, true);
            openSeatForm.ShowDialog();
            //updateTimer_Elapsed(null, null);
            dgv_shoe_show();
        }

        //创建台位
        private void createSeat(Control sp)
        {
            BathDBDataContext dc = new BathDBDataContext(LogIn.connectionString);
            var sLst = dc.HotelRoom.OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();

            int tableCountSp = sp.Controls.Count;
            int tableCount = sLst.Count;

            Size clientSize = seatPanel.Size;
            int nR = clientSize.Width / (btn_size + btn_space);
            int theRow = tableCountSp / nR;
            int theCol = tableCountSp - theRow * nR;

            //var sLst = db.Seat.OrderBy(x => x.text).ToList();
            int theCount = tableCountSp;
            //增多
            while (theCount < tableCount)
            {
                while (theCol < nR && theCount < tableCount)
                {
                    if (theCount != 0 && sLst[theCount].typeId != sLst[theCount - 1].typeId)
                    {
                        theCol = 0;
                        theRow++;
                    }
                    int x = theCol * btn_size + btn_space * (theCol + 1);
                    int y = theRow * btn_size + btn_space * (theRow + 1);
                    createButton(x, y, sLst[theCount], sp);

                    theCount++;
                    theCol++;
                }
                theCol = 0;
                theRow++;
            }

            //减少
            while (theCount > tableCount)
            {
                sp.Controls.RemoveAt(sp.Controls.Count - 1);
                theCount--;
            }

            //不变
            for (int i = 0; i < tableCount; i++)
            {
                ((Button)sp.Controls[i]).Text = sLst[i].text;
                btn_status((Button)sp.Controls[i], sLst[i]);
            }
        }

        //设置营业信息状态栏
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

            int count = 0;
            double pm = BathClass.get_paid_expense(db_new, thisTime, ref count);
            seatPaid.Text = count.ToString();
            moneyPaid.Text = pm.ToString();

            seatUnpaid.Text = db_new.HotelRoom.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status == 8).Count().ToString();
            double upm = BathClass.get_unpaid_expense(db_new, LogIn.connectionString);
            moneyUnpaid.Text = upm.ToString();

            moneyTotal.Text = (pm + upm).ToString();
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

            var inputSeatForm = new InputHotelRoomForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            m_Seat = db_new.HotelRoom.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            m_Seat.ordering = true;
            db_new.SubmitChanges();

            var orderForm = new HotelRoomOrderForm(m_Seat);
            orderForm.ShowDialog();

            m_Seat.ordering = false;
            db_new.SubmitChanges();
        }

        //F6开牌
        private void tool_open_seat()
        {
            if (tSeat.Text == "")
                return;

            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            string text = tSeat.Text;
            tSeat.Text = "";

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

                switch (seat1.status)
                {
                    case 1://可用
                    case 3://已经结账
                        if (!BathClass.ToBool(db_new.Options.FirstOrDefault().允许手工输入手牌号开牌) && mtype.menuId != null)
                        {
                            BathClass.printErrorMsg("不允许手工输入手牌号开牌!");
                            break;
                        }
                        open_seat(seat1, db_new);
                        break;
                    case 2://正在使用
                    case 7://押金离场
                        if (!BathClass.getAuthority(db_new, LogIn.m_User, "完整点单") &&
                            !BathClass.getAuthority(db_new, LogIn.m_User, "可见本人点单"))
                        {
                            BathClass.printErrorMsg("权限不够");
                            break;
                        }

                        seat1.ordering = true;
                        db_new.SubmitChanges();

                        var orderForm = new HotelRoomOrderForm(m_Seat);
                        orderForm.ShowDialog();

                        seat1.ordering = false;
                        db_new.SubmitChanges();
                        break;
                    case 4://锁定
                        BathClass.printErrorMsg("台位已经锁定!");
                        break;
                    case 5://停用
                        BathClass.printErrorMsg("台位已经停用!");
                        break;
                    case 6://警告
                        var orderCheckForm = new HotelRoomOrderCheckForm(m_Seat);
                        orderCheckForm.ShowDialog();
                        break;
                    case 8:
                        BathClass.printErrorMsg("补救台位不能录单");
                        break;
                    default:
                        break;
                }
            }
            else if (seat2 != null)
            {
                var mtype = db_new.HotelRoomType.FirstOrDefault(x => x.id == seat2.typeId);

                switch (seat2.status)
                {
                    case 1://可用
                    case 3://已经结账
                        open_seat(seat2, db_new);
                        break;
                    case 2://正在使用
                    case 7://押金离场
                        if (!BathClass.getAuthority(db_new, LogIn.m_User, "完整点单") &&
                            !BathClass.getAuthority(db_new, LogIn.m_User, "可见本人点单"))
                        {
                            BathClass.printErrorMsg("权限不够");
                            break;
                        }

                        seat2.ordering = true;
                        db_new.SubmitChanges();

                        var orderForm = new HotelRoomOrderForm(m_Seat);
                        orderForm.ShowDialog();

                        seat2.ordering = false;
                        db_new.SubmitChanges();
                        break;
                    case 4://锁定
                        BathClass.printErrorMsg("台位已经锁定!");
                        break;
                    case 5://停用
                        BathClass.printErrorMsg("台位已经停用!");
                        break;
                    case 6://警告
                        var orderCheckForm = new HotelRoomOrderCheckForm(m_Seat);
                        orderCheckForm.ShowDialog();
                        break;
                    case 8:
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
                    seatTimer_Elapsed();
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
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);

            InputHotelRoomForm inputSeatForm = new InputHotelRoomForm(2);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;
            var seat = db_new.HotelRoom.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            cancel_open(db_new, seat);
        }

        //更换手牌
        private void changeSeatTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            InputHotelRoomForm inputSeatForm = new InputHotelRoomForm(2);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;
            var seat = db_new.HotelRoom.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            change_seat(db_new, seat);
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

        private void change_seat(BathDBDataContext db_new, HotelRoom seat)
        {
            if (!BathClass.getAuthority(db_new, LogIn.m_User, "更换手牌"))
            {
                BathClass.printErrorMsg(LogIn.m_User.name + "不具有更换房间操作权限!");
                return;
            }

            if (seat.status != 2)
            {
                BathClass.printErrorMsg("该房间目前不在使用中，不能换房!");
                return;
            }

            List<int> sLst = new List<int>();
            sLst.Add(1);
            sLst.Add(3);
            InputHotelRoomForm inputSeatForm = new InputHotelRoomForm(sLst);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            HotelRoom newSeat = db_new.HotelRoom.FirstOrDefault(x => x.text == inputSeatForm.m_Seat.text);
            var orders = db_new.Orders.Where(x => x.systemId == seat.systemId);
            foreach (Orders order in orders)
                order.text = newSeat.text;

            newSeat.systemId = seat.systemId;
            newSeat.name = seat.name;
            newSeat.population = seat.population;
            newSeat.openTime = BathClass.Now(LogIn.connectionString);
            newSeat.openEmployee = LogIn.m_User.name;
            //newSeat.money = seat.money;
            newSeat.phone = seat.phone;
            newSeat.chainId = seat.chainId;
            newSeat.status = seat.status;
            newSeat.note = seat.note;
            newSeat.ordering = seat.ordering;

            BathClass.reset_seat(seat);
            db_new.SubmitChanges();
            dgv_shoe_show();
        }

        //更换手牌
        private void CtxChangeSeat_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            HotelRoom seat = getContextSenderSeat(db_new, sender);
            change_seat(db_new, seat);
            
        }
        private void cancel_open(BathDBDataContext db_new, HotelRoom seat)
        {
            InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();

            if (BathClass.getAuthority(db_new, LogIn.m_User, "取消开牌") ||
                (inputEmployee.ShowDialog() != DialogResult.OK &&
                BathClass.getAuthority(db_new, inputEmployee.employee, "取消开牌")))
            {
                if (seat.status != 2)
                {
                    BathClass.printErrorMsg("该台位不在使用中，不能取消开台!");
                    return;
                }

                if (db_new.Options.Count() != 0)
                {
                    Options options = db_new.Options.ToList()[0];
                    var q = options.取消开牌时限;
                    if (q != null && BathClass.Now(LogIn.connectionString) - seat.openTime >= TimeSpan.Parse("00:" + q.ToString() + ":00"))
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

                if (inputEmployee.employee != null)
                    op.employee = inputEmployee.employee.id;
                else
                    op.employee = LogIn.m_User.id;
                op.seat = seat.text;
                op.openEmployee = seat.openEmployee;
                op.openTime = seat.openTime;
                op.explain = "取消开牌";
                op.opTime = BathClass.Now(LogIn.connectionString);
                db_new.Operation.InsertOnSubmit(op);

                db_new.Orders.DeleteAllOnSubmit(db_new.Orders.Where(x => x.systemId == seat.systemId));
                BathClass.reset_seat(seat);
                db_new.SubmitChanges();
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
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            HotelRoom seat = getContextSenderSeat(db_new, sender);
            cancel_open(db_new, seat);
        }

        //新增联排
        private void CtxChain_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            HotelRoom seat = getContextSenderSeat(db_new, sender);
            OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            openSeatForm.ShowDialog();
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

            HotelRoom seat = getContextSenderSeat(db_new, sender);
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
            HotelRoom seat = getContextSenderSeat(db_new, sender);

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
        private void CtxStop_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            HotelRoom seat = getContextSenderSeat(db_new, sender);

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
            else if (seat.status == 1 || seat.status==3)
            {
                seat.status = 5;
            }
            else if (seat.status == 5)
            {
                seat.status = 1;
            }

            db_new.SubmitChanges();
        }
        #endregion

        private void toolOpen_Click(object sender, EventArgs e)
        {
            if (seatLock)
                seatTimer_Elapsed();
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

            var dc_new = new BathDBDataContext(LogIn.connectionString);
            var warn = new RoomWarn();
            warn.msg = "警告";
            dc_new.RoomWarn.InsertOnSubmit(warn);
            dc_new.SubmitChanges();
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_close = true;
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            //if (m_thread_msg != null && m_thread_msg.IsAlive)
            //    m_thread_msg.Abort();

            if (seatLock && lock_type == "锦衣卫")
                JYW.CloseReader();
        }

        private void tSeat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

    }
}
