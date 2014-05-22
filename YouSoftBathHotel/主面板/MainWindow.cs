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
using YouSoftBathReception;
using YouSoftBathFormClass;
using System.IO;
using YouSoftUtil;
using YouSoftBathConstants;

namespace YouSoftBathHotel
{
    public partial class MainWindow : Form
    {
        //成员变量
        private CSeat m_Seat = null;
        private Thread m_thread;//刷新线程
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

            this.Text = "咱家店小二-客房系统" + Constants.version + " 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户: " + LogIn.m_User.id + "   " + LogIn.m_User.name;


            int rt = RoomProRFL.initializeUSB(1);
            if (rt != 0)
            {
                BathClass.printErrorMsg("打开USB失败!");
                //this.Close();
                //return;
            }
            var td = new Thread(new ThreadStart(initial_ui_thread));
            td.IsBackground = true;
            td.Start();

            m_thread = new Thread(new ThreadStart(update_seats_ui));
            m_thread.IsBackground = true;
            m_thread.Start();
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
            CFormCreate.createSeatByDao(dao, LogIn.options, seatPanel, seatTab, new EventHandler(btn_Click), seatContext, "客房部");
            setStatus();
        }

        //刷新线程
        private void update_seats_ui()
        {
            while (true)
            {
                try
                {
                    var seats = dao.get_seats("typeid in (select id from [SeatType] where department='客房部')");
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

        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            m_Seat = dao.get_seat("text", btn.Text);
            var mtype = dao.get_seattype("id", m_Seat.typeId);

            switch (m_Seat.status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                case SeatStatus.RESERVE://预定客房
                    var form = new OpenRoomForm(m_Seat);
                    form.ShowDialog();
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.WARNING://警告
                case SeatStatus.DEPOSITLEFT://押金离场
                case SeatStatus.REPAIED://重新结账
                    if (m_Seat.deposit != null || m_Seat.depositBank != null)
                    {
                        BathClass.printInformation("现金押金：" + m_Seat.deposit.ToString() + "\n银联预授："
                            + m_Seat.depositBank.ToString());
                    }

                    if (m_Seat.note != null && m_Seat.note != "")
                        BathClass.printInformation(m_Seat.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(m_Seat, -1, false, false, dao);
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

        //F6开牌
        /*private void tool_open_seat()
        {
            if (tSeat.Text == "")
                return;

            string text = tSeat.Text;
            tSeat.Text = "";

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

                        SeatExpenseForm seatExpenseForm = new SeatExpenseForm(seat1, seat_length, seatLock, auto_seat_card, dao);
                        seatExpenseForm.ShowDialog();
                    }
                    else if (status==SeatStatus.AVILABLE||status==SeatStatus.PAIED)
                    {
                        var form = new OpenRoomForm(m_Seat);
                        form.ShowDialog();
                    }
                    else if (status == SeatStatus.LOCKING)
                        BathClass.printErrorMsg("台位已经锁定!");
                    else if (status == SeatStatus.STOPPED)
                        BathClass.printErrorMsg("台位已经停用!");
                }
            }
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
                //case Keys.Enter:
                    //tool_open_seat();
                    //break;
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
                //case Keys.D0:
                //case Keys.NumPad0:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "0";
                //    break;
                //case Keys.D1:
                //case Keys.NumPad1:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "1";
                //    break;
                //case Keys.D2:
                //case Keys.NumPad2:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "2";
                //    break;
                //case Keys.D3:
                //case Keys.NumPad3:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "3";
                //    break;
                //case Keys.D4:
                //case Keys.NumPad4:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "4";
                //    break;
                //case Keys.D5:
                //case Keys.NumPad5:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "5";
                //    break;
                //case Keys.D6:
                //case Keys.NumPad6:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "6";
                //    break;
                //case Keys.D7:
                //case Keys.NumPad7:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "7";
                //    break;
                //case Keys.D8:
                //case Keys.NumPad8:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "8";
                //    break;
                //case Keys.D9:
                //case Keys.NumPad9:
                //    if (!tSeat.TextBox.ContainsFocus)
                //        tSeat.Text += "9";
                //    break;
                //case Keys.Back:
                //    if (tSeat.Text != "" && !tSeat.TextBox.ContainsFocus)
                //    {
                //        tSeat.Text = tSeat.Text.Substring(0, tSeat.Text.Length - 1);
                //    }
                //    break;
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
        }

        //重打押金单
        private void CtxRprintDeposit_Click(object sender, EventArgs e)
        {
            var seat = getContextSenderSeat(sender);

            PrintRoomDepositReceipt.Print_DataGridView("押金单客人联", seat, LogIn.m_User.name,
                seat.name, seat.phone, 
                seat.openTime.Value.ToString("yyyy-MM-dd HH:mm"),
                seat.dueTime.Value.ToString("yyyy-MM-dd HH:mm"), 
                seat.deposit.ToString(),
                LogIn.options.companyName);

            PrintRoomDepositReceipt.Print_DataGridView("押金单存根联", seat, LogIn.m_User.name,
                seat.name, seat.phone,
                seat.openTime.Value.ToString("yyyy-MM-dd HH:mm"),
                seat.dueTime.Value.ToString("yyyy-MM-dd HH:mm"),
                seat.deposit.ToString(),
                LogIn.options.companyName);
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

                string cmd_str = @"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('"
                    + user_id + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime.ToString()
                    + "','取消开牌',getdate())";

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

        private void CtxChain_Click(object sender, EventArgs e)
        {
            //BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            //Seat seat = getContextSenderSeat(db_new, sender);
            //OpenSeatForm openSeatForm = new OpenSeatForm(seat, false);
            //openSeatForm.ShowDialog();
        }

        //重打账单
        private void CtxReprint_Click(object sender, EventArgs e)
        {
            CSeat seat = getContextSenderSeat(sender);

            if (seat.status != SeatStatus.PAIED)
            {
                BathClass.printErrorMsg("已经重新开牌，不能重打账单!");
                return;
            }

            var account = dao.get_account("abandon is null and systemId like '%" + seat.systemId + "%'");
            if (account == null)
                return;

            var seats_txt = account.text.Split('|');
            string state_str = "";
            int count = seats_txt.Count();
            for (int i = 0; i < count; i++)
            {
                state_str += "text='" + seats_txt[i] + "'";
                if (i != count - 1)
                    state_str += " or ";
            }
            var seats_reprint = dao.get_seats(state_str);

            DataGridView dgv = new DataGridView();

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "手牌";
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
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var co_name = LogIn.options.companyName;
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
                    }
                    PrintBill.Print_DataGridView(seats_reprint, account, "存根单", dgv, printCols, true, null, co_name);
                }
                catch (System.Exception ex)
                {
                    BathClass.printErrorMsg(ex.ToString());
                }
                finally
                {
                    dao.close_connection(sqlCn);
                }
            }
            else
            {
                SqlConnection sqlCn = null;
                double money = 0;

                try
                {
                    sqlCn = new SqlConnection(LogIn.connectionString);
                    sqlCn.Open();

                    string cmd_str = "Select * from [Orders] where text='" + seat.text + "' and sytemId!='" + seat.systemId +
                        "' deleteEmployee is null and paid='False' order by text";

                    var cmd = new SqlCommand(cmd_str, sqlCn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
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
                            money += Convert.ToDouble(dr["money"]);
                            dgv.Rows.Add(row);
                        }
                    }

                    if (dgv.Rows.Count != 0)
                    {
                        BathClass.printErrorMsg("未检测到转账单或者结账单");
                        return;
                    }

                    List<CSeat> seats = new List<CSeat>();
                    seats.Add(seat);

                    PrintSeatBill.Print_DataGridView(seats, "", "转账确认单", dgv, printCols, money.ToString(), co_name);
                }
                catch (System.Exception ex)
                {
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
        }

        private void tSeat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //结账
        private void payTool_Click(object sender, EventArgs e)
        {
            m_Seat = read_seat_by_card();
            if (m_Seat == null)
            {
                BathClass.printErrorMsg("读取房卡数据失败或者房间未定义!");
                return;
            }

            var mtype = dao.get_seattype("id", m_Seat.typeId);

            switch (m_Seat.status)
            {
                case SeatStatus.AVILABLE://可用
                case SeatStatus.PAIED://已经结账
                case SeatStatus.RESERVE://预定客房
                    var form = new OpenRoomForm(m_Seat);
                    form.ShowDialog();
                    break;
                case SeatStatus.USING://正在使用
                case SeatStatus.WARNING://警告
                case SeatStatus.DEPOSITLEFT://押金离场
                case SeatStatus.REPAIED://重新结账
                    if (m_Seat.deposit != null || m_Seat.depositBank != null)
                    {
                        BathClass.printInformation("现金押金：" + m_Seat.deposit.ToString() + "\n银联预授：" 
                            + m_Seat.depositBank.ToString());
                    }

                    if (m_Seat.note != null && m_Seat.note != "")
                        BathClass.printInformation(m_Seat.note);

                    SeatExpenseForm seatExpenseForm = new SeatExpenseForm(m_Seat, -1, false, false, dao);
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

        //查看联排
        private void toolChain_Click(object sender, EventArgs e)
        {
            ChainForm chainForm = new ChainForm();
            chainForm.ShowDialog();
        }

        //开牌
        private void openTool_Click(object sender, EventArgs e)
        {
            //tool_open_seat();
        }

        //续房
        private void ToolContinue_Click(object sender, EventArgs e)
        {
            var seat = read_seat_by_card();
            if (seat == null)
            {
                BathClass.printErrorMsg("房间未定义或者读取卡片数据失败!");
                return;
            }

            if (seat.status != SeatStatus.USING)
            {
                BathClass.printErrorMsg("房间:" + seat.text + "不可用!");
                return;
            }
            var form = new ContinueRoomForm(seat);
            form.ShowDialog();
        }

        private CSeat read_seat_by_card()
        {
            byte[] buff = new byte[200];
            //int rt = RoomProRFL.initializeUSB(1);

            int rt = RoomProRFL.ReadCard(1, buff);
            if (rt != 0)
            {
                BathClass.printErrorMsg("读取房卡失败");
                return null;
            }

            int hotelId = MConvert<int>.ToTypeOrDefault(IOUtil.get_config_by_key(ConfigKeys.KEY_HOTELID), -1);
            if (hotelId == -1)
            {
                BathClass.printErrorMsg("未定义酒店标志!");
                return null;
            }

            byte[] lock_buff = new byte[200];
            rt = RoomProRFL.GetGuestLockNoByCardDataStr(hotelId, buff, lock_buff);
            RoomProRFL.Buzzer(1, 40);
            if (rt != 0)
            {
                BathClass.printErrorMsg("读取房卡数据失败");
                return null;
            }

            string elec_id = Encoding.Default.GetString(lock_buff).Trim();
            var seat = dao.get_seat("oId", elec_id);

            return seat;
        }

        //换房
        private void ToolChangeRoom_Click(object sender, EventArgs e)
        {
            var seat = read_seat_by_card();
            if (seat == null)
            {
                BathClass.printErrorMsg("房间未定义或者读取卡片数据失败!");
                return;
            }

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
                BathClass.printErrorMsg("该房间目前不在使用中，不能换房!");
                return;
            }

            var form = new ChangeRoomForm(seat, oper.id);
            form.ShowDialog();
        }

        //取消开房
        private void ToolCancelOpen_Click(object sender, EventArgs e)
        {
            var seat = read_seat_by_card();
            if (seat == null)
            {
                BathClass.printErrorMsg("房间未定义或者读取卡片数据失败!");
                return;
            }

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

                string cmd_str = @"insert into [Operation](employee,seat,openEmployee,openTime,explain,opTime) values('"
                    + user_id + "','" + seat.text + "','" + seat.openEmployee + "','" + seat.openTime.ToString()
                    + "','取消开牌',getdate())";

                cmd_str += @" delete from [Orders] where systemId='" + seat.systemId + "'";
                cmd_str += dao.reset_seat_string() + "text='" + seat.text + "')";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("取消开房失败!");
                    return;
                }

                byte[] buff = new byte[200];
                //int rt = RoomProRFL.initializeUSB(1);

                int hotelId = MConvert<int>.ToTypeOrDefault(IOUtil.get_config_by_key(ConfigKeys.KEY_HOTELID), -1);
                if (hotelId == -1)
                {
                    BathClass.printErrorMsg("未定义酒店标志!");
                    return;
                }

                int rt = RoomProRFL.CardErase(1, hotelId, buff);
                if (rt != 0)
                {
                    BathClass.printErrorMsg("退房失败!");
                    return;
                }
                RoomProRFL.Buzzer(1, 40);
            }
            else
            {
                BathClass.printErrorMsg("不具有取消开牌权限!");
            }
        }

        //客房押金
        private void ToolDeposit_Click(object sender, EventArgs e)
        {
            var form = new TableDepositForm();
            form.ShowDialog();
        }

    }
}
