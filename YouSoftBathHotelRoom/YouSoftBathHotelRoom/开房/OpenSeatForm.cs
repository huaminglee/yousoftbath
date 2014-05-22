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

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Runtime.InteropServices;
using System.Transactions;

using System.Threading;
using System.Timers;

namespace YouSoftBathHotelRoom
{
    public partial class OpenSeatForm : Form
    {
        private BathDBDataContext db;
        private List<HotelRoom> m_Seats = new List<HotelRoom>();
        private bool m_open = true;
        private string chainId;
        private bool seatlock;
        private string lock_type;

        public OpenSeatForm(HotelRoom seat, bool open)
        {
            m_open = open;
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seats.Add(seat);
            chainId = BathClass.chainId(db, LogIn.connectionString);
            InitializeComponent();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            dgv_show();

            var ops = db.Options.FirstOrDefault();
            seatlock = ops.启用手牌锁.Value;
            lock_type = ops.手牌锁类型;
            seatBox.Enabled = BathClass.ToBool(ops.允许手工输入手牌号开牌);
        }

        //显示清单
        private void dgv_show()
        {
            dgv.Rows.Clear();
            foreach (var seat in m_Seats)
            {
                string[] row = new string[2];
                row[0] = seat.text;
                row[1] = db.HotelRoomType.FirstOrDefault(x => x.id == seat.typeId).name;
                dgv.Rows.Add(row);
            }
        }

        //手牌线程
        private void seatTimer_Elapsed()
        {
            if (!seatlock)
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
            var seat = db_new.HotelRoom.FirstOrDefault(x => x.text == seat_text);
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

                set_one_seat_status(seat, db_new);
                if (!m_Seats.Select(x => x.text).Contains(seat.text))
                    m_Seats.Add(seat);
            }
            else if (seat.status == 2 || seat.status == 7)
            {
                if (!m_Seats.Select(x => x.text).Contains(seat.text))
                    m_Seats.Add(seat);
            }

            dgv_show();
        }

        //增加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var seat = db.HotelRoom.FirstOrDefault(x => x.text == seatBox.Text || x.oId == seatBox.Text);
            seatBox.Text = "";
            if (seat == null)
            {
                seatBox.SelectAll();
                seatBox.Focus();
                BathClass.printErrorMsg("手牌不存在!");
                return;
            }
            if (m_open && seat.status != 1 && seat.status != 3)
            {
                seatBox.SelectAll();
                seatBox.Focus();
                BathClass.printErrorMsg("手牌不可用!");
                return;
            }
            if (!m_open && seat.status != 2 && seat.status != 6 && seat.status != 7)
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
            var dc_new = new BathDBDataContext(LogIn.connectionString);
            if (m_open)
            {
                int i = 0;
                foreach (var seat in m_Seats)
                {
                    var seat_tmp = dc_new.HotelRoom.FirstOrDefault(x => x.text == seat.text);
                    var child = Convert.ToBoolean(dgv.Rows[i].Cells[2].Value);

                    if (!seatlock)
                        open_one_seat(seat_tmp, child, dc_new);
                    else
                    {
                        var mtype = dc_new.HotelRoomType.FirstOrDefault(x => x.id == seat_tmp.typeId);
                        if (mtype.menuId == null)
                        {
                            if (seat_tmp.status == 3)
                            {
                                BathClass.reset_seat(seat_tmp);
                                dc_new.SubmitChanges();
                            }

                            seat_tmp.openEmployee = LogIn.m_User.id.ToString();
                            seat_tmp.openTime = BathClass.Now(LogIn.connectionString);
                            seat_tmp.systemId = BathClass.systemId(db, LogIn.connectionString);
                            seat_tmp.status = 2;
                        }

                        seat_tmp.chainId = chainId;
                        HotelRoomType seatType = dc_new.HotelRoomType.FirstOrDefault(x => x.id == seat_tmp.typeId);

                        var menu = dc_new.Menu.FirstOrDefault(x => x.id == seatType.menuId);
                        var child_menu = dc_new.Menu.FirstOrDefault(x => x.name == "儿童浴资");

                        if (menu != null)
                        {
                            Orders order = new Orders();
                            order.menu = menu.name;
                            order.text = seat_tmp.text;
                            order.systemId = seat_tmp.systemId;
                            order.number = 1;
                            order.money = menu.price;
                            order.inputTime = BathClass.Now(LogIn.connectionString);
                            order.inputEmployee = LogIn.m_User.id.ToString();
                            order.paid = false;
                            order.departmentId = 1;
                            dc_new.Orders.InsertOnSubmit(order);

                            if (child)
                            {
                                Orders child_order = new Orders();
                                child_order.menu = child_menu.name;
                                child_order.text = seat_tmp.text;
                                child_order.systemId = seat_tmp.systemId;
                                child_order.number = 1;
                                child_order.money = child_menu.price;
                                child_order.inputTime = BathClass.Now(LogIn.connectionString);
                                child_order.inputEmployee = LogIn.m_User.id.ToString();
                                child_order.paid = false;
                                order.departmentId = 1;
                                dc_new.Orders.InsertOnSubmit(child_order);
                            }
                        }
                    }
                    i++;
                }
                dc_new.SubmitChanges();
            }
            else
            {
                foreach (var seat in m_Seats)
                    seat.chainId = chainId;
                db.SubmitChanges();
            }

            this.DialogResult = DialogResult.OK;
        }

        //儿童浴资
        private void set_child()
        {
            if (dgv.CurrentCell == null)
                return;

            dgv.CurrentRow.Cells[2].Value = !Convert.ToBoolean(dgv.CurrentRow.Cells[2].Value);
        }

        private void set_one_seat_status(HotelRoom seat, BathDBDataContext dc)
        {
            if (seat.status == 3)
            {
                BathClass.reset_seat(seat);
                dc.SubmitChanges();
            }

            seat.openEmployee = LogIn.m_User.id.ToString();
            seat.openTime = BathClass.Now(LogIn.connectionString);
            seat.systemId = BathClass.systemId(dc, LogIn.connectionString);
            seat.status = 2;

            dc.SubmitChanges();
        }

        private void open_one_seat(HotelRoom seat, bool child, BathDBDataContext dc)
        {
            if (seat.status == 3)
            {
                BathClass.reset_seat(seat);
                dc.SubmitChanges();
            }

            seat.openEmployee = LogIn.m_User.id.ToString();
            seat.openTime = BathClass.Now(LogIn.connectionString);
            seat.systemId = BathClass.systemId(db, LogIn.connectionString);
            seat.status = 2;
            if (!seatlock)
                seat.chainId = chainId;

            HotelRoomType seatType = dc.HotelRoomType.FirstOrDefault(x => x.id == seat.typeId);

            var menu = dc.Menu.FirstOrDefault(x => x.id == seatType.menuId);
            if (menu != null)
            {
                Orders order = new Orders();
                order.menu = menu.name;
                order.text = seat.text;
                order.systemId = seat.systemId;
                order.number = 1;
                order.money = menu.price;
                order.inputTime = BathClass.Now(LogIn.connectionString);
                order.inputEmployee = LogIn.m_User.id.ToString();
                order.paid = false;
                dc.Orders.InsertOnSubmit(order);
            }

            if (child)
            {
                var child_menu = dc.Menu.FirstOrDefault(x => x.name == "儿童浴资");
                if (child_menu != null)
                {
                    Orders order = new Orders();
                    order.menu = child_menu.name;
                    order.text = seat.text;
                    order.systemId = seat.systemId;
                    order.number = 1;
                    order.money = child_menu.price;
                    order.inputTime = BathClass.Now(LogIn.connectionString);
                    order.inputEmployee = LogIn.m_User.id.ToString();
                    order.paid = false;
                    dc.Orders.InsertOnSubmit(order);
                }
            }

            dc.SubmitChanges();
        }

        private void seatBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void OpenSeatForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    seatTimer_Elapsed();
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
            //isClosing = true;
            //if (seatlock)
                //Thread.Sleep(3000);
        }

        private void seatBox_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
