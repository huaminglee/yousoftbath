using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using YouSoftBathConstants;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Runtime.InteropServices;
using System.Transactions;

using System.Threading;
using System.Timers;


namespace YouSoftBathFormClass
{
    public partial class OpenSeatForm : Form
    {
        private DAO dao;
        private List<CSeat> m_Seats = new List<CSeat>();
        private bool m_open = true;
        private string chainId;
        private bool seatlock;
        private bool auto_seat_card;//自动感应手牌
        private string lock_type;
        private int seat_length = -1;
        private int seat_start = -1;

        private bool _close = false;
        private Thread m_thread_seatCard;//手牌线程

        public OpenSeatForm(CSeat seat, bool open)
        {
            m_open = open;
            //var db = new BathDBDataContext(LogIn.connectionString);
            m_Seats.Add(seat);
            seat_length = seat.text.Length;
            dao = new DAO(LogIn.connectionString);
            chainId = dao.chainId();

            seatlock = LogIn.options.启用手牌锁.Value;
            lock_type = LogIn.options.手牌锁类型;
            auto_seat_card = MConvert<bool>.ToTypeOrDefault(LogIn.options.自动感应手牌, false);

            InitializeComponent();

            seatBox.Enabled = MConvert<bool>.ToTypeOrDefault(LogIn.options.允许手工输入手牌号开牌, false);
            dgv_show();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            if (seatlock && auto_seat_card)
            {
                m_thread_seatCard = new Thread(new ThreadStart(seat_card_thread));
                m_thread_seatCard.Start();
            }

        }

        //非自动感应手牌
        private void open_seat_noauto()
        {
            try
            {
                if (!seatlock)
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
                    {
                        return;
                    }
                    if (!set_one_seat_status(seat))
                        return;
                    if (!m_Seats.Select(x => x.text).Contains(seat.text))
                        m_Seats.Add(seat);
                }
                else if (seat.status == SeatStatus.USING || seat.status == SeatStatus.DEPOSITLEFT)
                {
                    if (!m_Seats.Select(x => x.text).Contains(seat.text))
                        m_Seats.Add(seat);
                }
                dgv_show();
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
                if (_close)
                    break;
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

                    if (seat.status == SeatStatus.AVILABLE || seat.status == SeatStatus.PAIED)
                    {
                        if (lock_type == "欧亿达")
                            Thread.Sleep(500);

                        if ((lock_type == "欧亿达" && OYD.OYEDA_fk(buff) != 0) ||
                            (lock_type == "锦衣卫" && JYW.FK(buff) != 0) ||
                            (lock_type == "RF" && RF.RF_FK(seat_text) != 0))
                            continue;

                        if (!set_one_seat_status(seat))
                            continue;
                        if (!m_Seats.Select(x => x.text).Contains(seat.text))
                            m_Seats.Add(seat);
                    }
                    else if (seat.status == SeatStatus.USING || seat.status == SeatStatus.DEPOSITLEFT)
                    {
                        if (!m_Seats.Select(x => x.text).Contains(seat.text))
                            m_Seats.Add(seat);
                    }
                    if (this.InvokeRequired)
                        this.Invoke(new delegate_dgv_show(dgv_show));
                    else
                        dgv_show();
                }
                catch (Exception ex)
                {
                }
            }
        }

        private delegate void delegate_dgv_show();
        private delegate void delegate_print_msg(string msg);

        //显示清单
        private void dgv_show()
        {
            dgv.Rows.Clear();
            foreach (var seat in m_Seats)
            {
                dgv.Rows.Add(seat.text, dao.get_seattype("id", seat.typeId).name);
            }
        }

        //增加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (seatBox.Text == "")
            {
                BathClass.printErrorMsg("需要输入手牌号!");
                return;
            }
            //var dc = new BathDBDataContext(LogIn.connectionString);
            var seat = dao.get_seat("text='" + seatBox.Text + "' or oId='" + seatBox.Text + "'");
            //var seat = dc.Seat.FirstOrDefault(x => x.text == seatBox.Text || x.oId == seatBox.Text);
            seatBox.Text = "";
            if (seat == null)
            {
                seatBox.SelectAll();
                seatBox.Focus();
                BathClass.printErrorMsg("手牌不存在!");
                return;
            }
            if (m_open && seat.status != SeatStatus.AVILABLE && seat.status != SeatStatus.PAIED)
            {
                seatBox.SelectAll();
                seatBox.Focus();
                BathClass.printErrorMsg("手牌不可用!");
                return;
            }
            if (!m_open && seat.status != SeatStatus.USING && seat.status != SeatStatus.WARNING && seat.status != SeatStatus.DEPOSITLEFT)
            {
                seatBox.SelectAll();
                seatBox.Focus();
                BathClass.printErrorMsg("手牌不在使用中，不能联排!");
                return;
            }
            if (!m_Seats.Contains(seat))
                m_Seats.Add(seat);
            dgv_show();
            seatBox.Text = "";
            seatBox.Focus();
        }

        //删除
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选择行!");
                return;
            }
            m_Seats.Remove(m_Seats.FirstOrDefault(x => x.text == dgv.CurrentRow.Cells[0].Value.ToString()));
            dgv_show();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            _close = true;
            //var dc_new = new BathDBDataContext(LogIn.connectionString);
            #region 开牌
            if (m_open)
            {
                int i = 0;
                foreach (var seat in m_Seats)
                {
                    //var seat_tmp = dc_new.Seat.FirstOrDefault(x => x.text == seat.text);
                    var child = Convert.ToBoolean(dgv.Rows[i].Cells[2].Value);

                    #region 不使用手牌锁
                    if (!seatlock)
                        open_one_seat(seat, child);
                    #endregion

                    #region 使用手牌锁
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        //seat_tmp.chainId = chainId;
                        //dc_new.SubmitChanges();
                        //var seatType = dc_new.SeatType.FirstOrDefault(x => x.id == seat_tmp.typeId);
                        var seatMenu = dao.get_seat_menu(seat.text);
                        #region 外卖台
                        if (seatMenu == null)
                        {
                            if (seat.status == SeatStatus.PAIED)
                            {
                                sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
                                //BathClass.reset_seat(seat_tmp);
                                //dc_new.SubmitChanges();
                            }
                            string systemId = dao.systemId();
                            sb.Append("update [Seat] set openEmployee='" + LogIn.m_User.id);
                            sb.Append("',openTime=getdate(),systemId='");
                            sb.Append(systemId + "',status=2 where text='" + seat.text + "' ");
                            //seat_tmp.openEmployee = LogIn.m_User.id.ToString();
                            //seat_tmp.openTime = BathClass.Now(LogIn.connectionString);
                            //seat_tmp.systemId = BathClass.systemId(dc_new, LogIn.connectionString);
                            //seat_tmp.status = 2;

                            sb.Append(" insert into [SystemIds](systemId) values('" + systemId + "') ");
                            sb.Append(" update [Seat] set chainId='" + chainId + "' where text='" + seat.text + "'");
                            if (!dao.execute_command(sb.ToString()))
                            {
                                BathClass.printErrorMsg("手牌号：" + seat.text + "开牌失败");
                                return;
                            }
                            //var id = new SystemIds();
                            //id.systemId = seat_tmp.systemId;
                            //dc_new.SystemIds.InsertOnSubmit(id);
                            //dc_new.SubmitChanges();
                        }
                        #endregion

                        #region 非外卖台
                        else
                        {
                            sb.Append(" update [Seat] set chainId='" + chainId + "' where text='" + seat.text + "'");
                            var child_menu = dao.get_Menu("name", "儿童浴资");
                            //var child_menu = dc_new.Menu.FirstOrDefault(x => x.name == "儿童浴资");
                            if (child && child_menu != null)
                            {
                                sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                                sb.Append(@" select '儿童浴资','" + seat.text + "',systemId,1," + child_menu.price + ",getdate(),'");
                                sb.Append(LogIn.m_User.id + "','False' from [Seat] where text='" + seat.text + "'");

                                //Orders child_order = new Orders();
                                //child_order.menu = child_menu.name;
                                //child_order.text = seat_tmp.text;
                                //child_order.systemId = seat_tmp.systemId;
                                //child_order.number = 1;
                                //child_order.money = child_menu.price;
                                //child_order.inputTime = BathClass.Now(LogIn.connectionString);
                                //child_order.inputEmployee = LogIn.m_User.id.ToString();
                                //child_order.paid = false;
                                //dc_new.Orders.InsertOnSubmit(child_order);

                                //dc_new.SubmitChanges();
                                //if (child_order.id == 0)
                                //{
                                //    dc_new.SubmitChanges();
                                //}
                            }
                            if (!dao.execute_command(sb.ToString()))
                            {
                                BathClass.printErrorMsg("手牌号：" + seat.text + "儿童浴资录入失败");
                                return;
                            }
                        }
                        #endregion
                    }
                    i++;
                }
                #endregion
            }
            #endregion

            #region 手牌联排
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"update [Seat] set chainId='" + chainId + "' where (");
                int count = m_Seats.Count;
                for (int i = 0; i < count; i++ )
                {
                    sb.Append("text='" + m_Seats[i].text + "'");
                    if (i != count - 1)
                        sb.Append(" or ");
                }
                sb.Append(")");
                if (!dao.execute_command(sb.ToString()))
                {
                    BathClass.printErrorMsg("手牌联排失败!");
                    return;
                }
                //foreach (var seat in m_Seats)
                //    seat.chainId = chainId;
                //dc_new.SubmitChanges();
            }
            #endregion

            this.DialogResult = DialogResult.OK;
        }

        //儿童浴资
        private void set_child()
        {
            if (dgv.CurrentCell == null)
                return;

            dgv.CurrentRow.Cells[2].Value = !Convert.ToBoolean(dgv.CurrentRow.Cells[2].Value);
        }

        private bool set_one_seat_status(CSeat seat)
        {
            string systemId = dao.systemId();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [Seat] set openEmployee='" + LogIn.m_User.id + "',openTime=getdate(),systemId='");
            sb.Append(systemId + "',status=2 where text='" + seat.text + "' ");
            var menu = dao.get_seat_menu(seat.text);
            if (menu != null)
            {
                sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                sb.Append(@"values('" + menu.name + "','" + seat.text + "','" + systemId + "',1," + menu.price + ",getdate(),'");
                sb.Append(LogIn.m_User.id + "','False')");
            }
            sb.Append("insert into [SystemIds](systemId) values('" + systemId + "') ");
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("手牌：" + seat.text + "开牌失败");
                return false;
            }
            return true;
        }

        private void open_one_seat(CSeat seat, bool child)
        {
            string systemId = dao.systemId();
            StringBuilder sb = new StringBuilder();
            if (seat.status == SeatStatus.PAIED)
            {
                sb.Append(dao.reset_seat_string() + "text='" + seat.text + "')");
                //BathClass.reset_seat(seat);
                //dc.SubmitChanges();
            }

            sb.Append("update [Seat] set openEmployee='" + LogIn.m_User.id + "',openTime=getdate(),systemId='");
            sb.Append(systemId + "',status=2");
            //seat.openEmployee = LogIn.m_User.id.ToString();
            //seat.openTime = BathClass.Now(LogIn.connectionString);
            //seat.systemId = BathClass.systemId(dc, LogIn.connectionString);
            //seat.status = 2;
            if (!seatlock)
                sb.Append(", chainId='" + chainId + "' ");
                //seat.chainId = chainId;
            sb.Append("where text='" + seat.text + "'");

            var menu = dao.get_seat_menu(seat.text);
            //SeatType seatType = dc.SeatType.FirstOrDefault(x => x.id == seat.typeId);

            //var menu = dc.Menu.FirstOrDefault(x => x.id == seatType.menuId);
            if (menu != null)
            {
                sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                sb.Append(@"values('" + menu.name + "','" + seat.text + "','" + systemId + "',1," + menu.price + ",getdate(),'");
                sb.Append(LogIn.m_User.id + "','False')");
                //Orders order = new Orders();
                //order.menu = menu.name;
                //order.text = seat.text;
                //order.systemId = seat.systemId;
                //order.number = 1;
                //order.money = menu.price;
                //order.inputTime = BathClass.Now(LogIn.connectionString);
                //order.inputEmployee = LogIn.m_User.id.ToString();
                //order.paid = false;
                //dc.Orders.InsertOnSubmit(order);
            }

            if (child)
            {
                var child_menu = dao.get_Menu("name", "儿童浴资");
                //var child_menu = dc.Menu.FirstOrDefault(x => x.name == "儿童浴资");
                if (child_menu != null)
                {
                    sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                    sb.Append(@"values('儿童浴资','" + seat.text + "','" + systemId + "',1," + child_menu.price + ",getdate(),'");
                    sb.Append(LogIn.m_User.id + "','False')");
                    //Orders order = new Orders();
                    //order.menu = child_menu.name;
                    //order.text = seat.text;
                    //order.systemId = seat.systemId;
                    //order.number = 1;
                    //order.money = child_menu.price;
                    //order.inputTime = BathClass.Now(LogIn.connectionString);
                    //order.inputEmployee = LogIn.m_User.id.ToString();
                    //order.paid = false;
                    //dc.Orders.InsertOnSubmit(order);
                }
            }
            sb.Append("insert into [SystemIds](systemId) values('" + systemId + "') ");
            //var id = new SystemIds();
            //id.systemId = seat.systemId;
            //dc.SystemIds.InsertOnSubmit(id);

            //BathClass.SubmitChanges(dc);
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("手牌：" + seat.text + "开牌失败");
                return;
            }
            
        }

        private void OpenSeatForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (!auto_seat_card)
                        open_seat_noauto();
                    break;
                case Keys.Enter:
                    if (seatBox.Text == "")
                        btnOk_Click(null, null);
                    else
                        btnAdd_Click(null, null);
                    break;
                case Keys.Decimal:
                    set_child();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "0";
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "1";
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "2";
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "3";
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "4";
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "5";
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "6";
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "7";
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "8";
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    if (!seatBox.ContainsFocus)
                        seatBox.Text += "9";
                    break;
                case Keys.Back:
                    if (seatBox.Text != "" && !seatBox.ContainsFocus)
                        seatBox.Text = seatBox.Text.Substring(0, seatBox.Text.Length - 1);
                    break;
                default:
                    break;
            }
        }

        private void OpenSeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _close = true;
            //if (m_thread_seatCard != null && m_thread_seatCard.IsAlive)
            //    m_thread_seatCard.Abort();
        }

        private void seatBox_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
