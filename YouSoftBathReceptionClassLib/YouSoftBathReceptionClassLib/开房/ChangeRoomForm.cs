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
using YouSoftUtil;
using System.Threading;
using System.Timers;

namespace YouSoftBathReception
{
    public partial class ChangeRoomForm : Form
    {
        private CSeat m_seat;
        private CSeat m_newSeat;
        private DAO dao;
        private string m_oper;

        public ChangeRoomForm(CSeat seat, string oper)
        {
            m_seat = seat;
            m_oper = oper;
            InitializeComponent();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            roomNumber.Text = m_seat.text;
            TextName.Text = m_seat.name;
            TextPhone.Text = m_seat.phone;
            TextDeposit.Text = m_seat.deposit.ToString();
            TextBank.Text = m_seat.depositBank.ToString();
            days.CustomFormat = "yyyy-MM-dd-HH:mm";
            days.Value = m_seat.dueTime.Value;
            DateOpenTime.Value = m_seat.openTime.Value;

            dao = new DAO(LogIn.connectionString);
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text=="读房间")
            {
                m_newSeat = dao.get_seat("text", TextNewRoom.Text);
                if (m_newSeat == null)
                {
                    BathClass.printErrorMsg("房间:" + TextNewRoom.Text + "未定义!");
                    TextNewRoom.SelectAll();
                    return;
                }
                if (m_newSeat.status != SeatStatus.AVILABLE && m_newSeat.status != SeatStatus.PAIED && m_newSeat.status != SeatStatus.RESERVE)
                {
                    BathClass.printErrorMsg("房间号" + roomNumber.Text + "不可用");
                    return;
                }
                
                btnOk.Text = "开新房";
                LabelInfo.Text = "请放置新房卡......";

                byte[] buff = new byte[200];

                int hotelId = MConvert<int>.ToTypeOrDefault(IOUtil.get_config_by_key(ConfigKeys.KEY_HOTELID), -1);
                if (hotelId == -1)
                {
                    BathClass.printErrorMsg("未定义酒店标志!");
                    return;
                }

                int rt = RoomProRFL.CardErase(1, hotelId, buff);
                RoomProRFL.Buzzer(1, 40);
                if (rt != 0)
                {
                    BathClass.printErrorMsg("退房失败!");
                    return;
                }
            }
            else if (btnOk.Text=="开新房")
            {
                int cardNo = 0;
                byte[] buff = new byte[200];
                //int rt = RoomProRFL.initializeUSB(1);
                string BDate = m_seat.openTime.Value.ToString("yyMMddHHmm");
                string EDate = m_seat.dueTime.Value.ToString("yyMMddHHmm");

                int hotelId = MConvert<int>.ToTypeOrDefault(IOUtil.get_config_by_key(ConfigKeys.KEY_HOTELID), -1);
                if (hotelId == -1)
                {
                    BathClass.printErrorMsg("未定义酒店标志!");
                    return;
                }

                int rt = RoomProRFL.GuestCard(1, hotelId, cardNo, 0, 0, 1, BDate, EDate, m_newSeat.oId, buff);
                RoomProRFL.Buzzer(1, 40);
                if (rt != 0)
                {
                    BathClass.printErrorMsg("开房失败!");
                    return;
                }

                string cmd_str = @"update [Orders] set text='" + m_newSeat.text + "' where systemId='" + m_seat.systemId + "' ";
                cmd_str += @"update [Seat] set systemId='" + m_seat.systemId + "'";
                cmd_str += ",openTime='" + m_seat.openTime.Value.ToString("yyyy-MM-dd HH:mm:ss") +
                    "',openEmployee='" + m_oper + "',chainId='" + m_seat.chainId
                    + "',status=" + (int)m_seat.status + ",ordering='False'"
                    + ",deposit=" + m_seat.deposit + ",depositBank=" + m_seat.depositBank + ",dueTime='" + m_seat.dueTime.Value.ToString("yyyy-MM-dd HH:mm:ss")
                    + "'";

                if (m_seat.name != null && m_seat.name != "")
                    cmd_str += ",name='" + m_seat.name + "'";

                if (m_seat.phone != null && m_seat.phone != "")
                    cmd_str += ",phone='" + m_seat.phone + "'";

                if (m_seat.note != null && m_seat.note != "")
                    cmd_str += ",note='" + m_seat.note + "'";

                cmd_str += " where id=" + m_newSeat.id;
                cmd_str += dao.reset_seat_string() + "text='" + m_seat.text + "')";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("更换手牌失败，请重试！");
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void seatBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void OpenSeatForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (days.Text != "")
                        btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void OpenSeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void seatBox_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

    }
}
