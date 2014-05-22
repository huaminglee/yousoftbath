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

using System.Timers;
using System.Threading;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class ReceptionSeatForm : Form
    {
        //成员变量
        private Seat m_Seat = null;
        private Thread m_thread;//刷新线程

        //构造函数
        public ReceptionSeatForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void ReceptionSeatForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            
            currentUser.Text = "当前用户:" + LogIn.m_User.id + " " + LogIn.m_User.name;

            CFormCreate.createSeat(db, seatPanel, seatTab, new EventHandler(btn_Click), seatContext);
            setStatus(db);

            m_thread = new Thread(new ThreadStart(update_seats_ui));
            m_thread.Start();
        }

        //刷新线程
        private void update_seats_ui()
        {
            while (true)
            {
                try
                {
                    var db_new = new BathDBDataContext(LogIn.connectionString);
                    var seats_id_tmp = db_new.Seat.Select(x => x.id).ToList();
                    var seats_status_tmp = db_new.Seat.Select(x => x.status).ToList();

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
                            case 1://可用
                                if (btn.BackColor != Color.White)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.White });
                                }
                                break;
                            case 2://正在使用
                                if (btn.BackColor != Color.Cyan)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Cyan });
                                }
                                break;
                            case 3://已经结账
                                if (btn.BackColor != Color.Gray)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Gray });
                                }
                                break;
                            case 4://锁定
                                if (btn.BackColor != Color.Orange)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Orange });
                                }
                                break;
                            case 5://停用
                                if (btn.BackColor != Color.Red)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Red });
                                }
                                break;
                            case 6://警告
                                if (btn.BackColor != Color.Yellow)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Yellow });
                                }
                                break;
                            case 7://押金离场
                                if (btn.BackColor != Color.Violet)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.Violet });
                                }
                                break;
                            case 8://重新结账
                                if (btn.BackColor != Color.CornflowerBlue)
                                {
                                    changed = true;
                                    this.Invoke(new set_btn_color_delegate(set_btn_color), new object[] { btn, Color.CornflowerBlue });
                                }
                                break;
                            case 9://预约客房
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
                        this.Invoke(new set_status_delegate(setStatus), new object[] { db_new });
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
        }

        private delegate void watchSeatDelegate();

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

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            var db_new = new BathDBDataContext(LogIn.connectionString);
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            var seat = db_new.Seat.FirstOrDefault(x => x.text == btn.Text);
            switch (seat.status)
            {
                case 2://正在使用
                case 6://警告
                case 7://押金离场
                case 8://重新结账
                    var dao = new DAO(LogIn.connectionString);
                    var s = dao.get_seat("text='" + seat.text + "'");
                    OrderCheckForm orderForm = new OrderCheckForm(s, LogIn.connectionString, LogIn.options);
                    orderForm.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void setStatus(BathDBDataContext dc_new)
        {
            if (!BathClass.getAuthority(dc_new, LogIn.m_User, "营业信息查看"))
            {
                statusStrip2.Visible = false;
                return;
            }
            seatTotal.Text = dc_new.Seat.Count().ToString();
            seatAvi.Text = dc_new.Seat.Where(x => x.status == 1).Count().ToString();

            DateTime st = DateTime.Parse("2013-1-1 00:00:00");
            if (dc_new.ClearTable.Count() != 0)
            {
                st = dc_new.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault().clearTime;
            }
            int count = 0;
            double pm = BathClass.get_paid_expense(dc_new, st, ref count);
            seatPaid.Text = count.ToString();
            moneyPaid.Text = pm.ToString();

            seatUnpaid.Text = dc_new.Seat.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status == 8).Count().ToString();
            double upm = BathClass.get_unpaid_expense(dc_new, LogIn.connectionString);
            moneyUnpaid.Text = upm.ToString();

            moneyTotal.Text = (pm + upm).ToString();
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tool_open_seat()
        {
            var db_new = new BathDBDataContext(LogIn.connectionString);
            string text = tSeat.Text;
            m_Seat = db_new.Seat.FirstOrDefault(x => x.text == text);
            if (m_Seat == null || (m_Seat.status != 2 && m_Seat.status != 6 && m_Seat.status != 7 && m_Seat.status != 8))
            {
                GeneralClass.printErrorMsg("手牌不存在或者不在使用中，不可查看消费!");
                return;
            }

            var dao = new DAO(LogIn.connectionString);
            var s = dao.get_seat("text", m_Seat.text);
            OrderCheckForm orderCheckForm = new OrderCheckForm(s, LogIn.connectionString, LogIn.options);
            orderCheckForm.ShowDialog();
            tSeat.Text = "";
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
                    tool_open_seat();
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

        private Seat getContextSenderSeat(BathDBDataContext dc, object sender)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip cmenu = item.GetCurrentParent() as ContextMenuStrip;
            Button bt = cmenu.SourceControl as Button;
            return dc.Seat.FirstOrDefault(x => x.text == bt.Text);
        }

        //添加备注
        private void CtxAddNote_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            Seat seat = getContextSenderSeat(db_new, sender);
            if (seat.status != 2 && seat.status != 6)
            {
                GeneralClass.printErrorMsg("手牌未使用，不能添加备注");
                return;
            }

            NoteForm noteForm = new NoteForm();
            if (noteForm.ShowDialog() != DialogResult.OK)
                return;

            seat.note = noteForm.note;
            db_new.SubmitChanges();
        }
        #endregion

        private void ReceptionSeatForm_SizeChanged(object sender, EventArgs e)
        {
        }

        private void unWarnTool_Click(object sender, EventArgs e)
        {
            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            Seat seat = getContextSenderSeat(db_new, sender);
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

        private void tSeat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void ReceptionSeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }
    }
}
