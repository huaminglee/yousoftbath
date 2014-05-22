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
    public partial class ContinueRoomForm : Form
    {
        private CSeat m_seat;
        private string chainId;
        private DAO dao;

        public ContinueRoomForm(CSeat seat)
        {
            m_seat = seat;
            InitializeComponent();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            roomNumber.Text = m_seat.text;
            TextName.Text = m_seat.name;
            TextPhone.Text = m_seat.phone;
            TextDeposit.Text = m_seat.deposit.ToString();
            TextDepositBank.Text = m_seat.depositBank.ToString();
            days.CustomFormat = "yyyy-MM-dd-HH:mm";
            days.Value = m_seat.dueTime.Value;
            DateOpenTime.Value = m_seat.openTime.Value;
            //chainId = BathClass.chainId(db, LogIn.connectionString);

            dao = new DAO(LogIn.connectionString);
            chainId = dao.chainId();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            //var seat = dao.get_seat("text", roomNumber.Text);

            int cardNo = 0;
            byte[] buff = new byte[200];
            //int rt = RoomProRFL.initializeUSB(1);
            string BDate = DateTime.Now.ToString("yyMMddHHmm");
            string EDate = days.Value.ToString("yyMMddHHmm");

            int hotelId = MConvert<int>.ToTypeOrDefault(IOUtil.get_config_by_key(ConfigKeys.KEY_HOTELID), -1);
            if (hotelId == -1)
            {
                BathClass.printErrorMsg("未定义酒店标志!");
                return;
            }
            int rt = RoomProRFL.GuestCard(1, hotelId, cardNo, 0, 0, 1, BDate, EDate, m_seat.oId, buff);
            RoomProRFL.Buzzer(1, 40);
            if (rt != 0)
            {
                BathClass.printErrorMsg("开房失败!");
                return;
            }


            double money = MConvert<double>.ToTypeOrDefault(TextDepositOver.Text, 0) + 
                MConvert<double>.ToTypeOrDefault(m_seat.deposit, 0);
            double money_bank = MConvert<double>.ToTypeOrDefault(TextDepositOverBank.Text, 0) +
                MConvert<double>.ToTypeOrDefault(m_seat.depositBank, 0);
            StringBuilder sb = new StringBuilder();
            sb.Append(@" update [Seat] set openEmployee='");
            sb.Append(LogIn.m_User.id);
            sb.Append("',deposit=").Append(money.ToString()).Append(",depositBank=").Append(money_bank.ToString())
                .Append(",status=2,name='");
            sb.Append(TextName.Text).Append("'");
            sb.Append(",dueTime='").Append(days.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (TextPhone.Text.Trim() != "")
                sb.Append(",phone='").Append(TextPhone.Text.Trim()).Append("'");
            sb.Append(" where text='").Append(roomNumber.Text).Append("'");

            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("开房失败，请重试!");
                return;
            }

            string now = DateOpenTime.Value.ToString("yyy-MM-dd HH:ss");
            string dueTime = days.Value.ToString("yyyy-MM-dd HH:mm");
            PrintRoomDepositReceipt.Print_DataGridView("押金单客人联", m_seat, LogIn.m_User.name, 
                TextName.Text, TextPhone.Text, now, dueTime, money.ToString(), 
                LogIn.options.companyName);

            PrintRoomDepositReceipt.Print_DataGridView("押金单存根联", m_seat, LogIn.m_User.name,
                TextName.Text, TextPhone.Text, now, dueTime, money.ToString(),
                LogIn.options.companyName);
            this.DialogResult = DialogResult.OK;
            this.Close();
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
