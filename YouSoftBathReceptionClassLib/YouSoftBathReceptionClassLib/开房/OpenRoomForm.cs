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
    public partial class OpenRoomForm : Form
    {
        //private Dao dao;
        private CSeat m_seat;
        private string chainId;
        private DAO dao;

        public OpenRoomForm(CSeat seat)
        {
            m_seat = seat;
            InitializeComponent();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            roomNumber.Text = m_seat.text;
            name.Text = m_seat.name;
            phone.Text = m_seat.phone;
            days.CustomFormat = "yyyy-MM-dd-HH:mm";
            days.Value = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 12:00:00");
            //chainId = BathClass.chainId(db, LogIn.connectionString);

            dao = new DAO(LogIn.connectionString);
            chainId = dao.chainId();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (name.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入姓名");
                name.Focus();
                return;
            }

            if (deposit.Text.Trim() == "" && TextBank.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入押金");
                deposit.Focus();
                return;
            }

            var seat = dao.get_seat("text", roomNumber.Text);
            if (seat == null)
            {
                BathClass.printErrorMsg("房间号" + roomNumber.Text + "不存在");
                return;
            }

            if (seat.status != SeatStatus.AVILABLE && seat.status != SeatStatus.PAIED && seat.status != SeatStatus.RESERVE)
            {
                BathClass.printErrorMsg("房间号" + roomNumber.Text + "不可用");
                return;
            }

            if (seat.status == SeatStatus.RESERVE && name.Text != seat.name)
            {
                if (BathClass.printAskMsg("开房客人信息与预定客人信息不一致，是否继续?") != DialogResult.Yes)
                    return;
            }

            if (name.Text == "")
            {
                BathClass.printErrorMsg("需要输入客人姓名!");
                return;
            }

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
            //rt = RoomProRFL.ReadCard(1, buff);
            //if (rt != 0)
            //{
            //    BathClass.printErrorMsg("未放置房卡或者房卡异常!");
            //    return;
            //}

            int rt = RoomProRFL.GuestCard(1, hotelId, cardNo, 0, 0, 1, BDate, EDate, seat.oId, buff);
            RoomProRFL.Buzzer(1, 40);
            if (rt != 0)
            {
                BathClass.printErrorMsg("开房失败!");
                return;
            }

            if (seat.status == SeatStatus.PAIED)
            {
                dao.reset_seat("text='" + seat.text + "'");
                //BathClass.reset_seat(seat);
                //dc_new.SubmitChanges();
            }

            string systemId=dao.systemId();

            StringBuilder sb = new StringBuilder();
            double money_deposit = MConvert<double>.ToTypeOrDefault(deposit.Text, 0);
            double money_bank = MConvert<double>.ToTypeOrDefault(TextBank.Text, 0);
            sb.Append(@" update [Seat] set openEmployee='");
            sb.Append(LogIn.m_User.id);
            sb.Append("', openTime=getdate(), systemId='");
            sb.Append(systemId);
            sb.Append(@"',chainId='").Append(chainId).Append("',deposit=").Append(money_deposit.ToString()).
                Append(",depositBank=").Append(money_bank.ToString()).Append(",status=2,name='");
            sb.Append(name.Text).Append("'");
            sb.Append(",dueTime='").Append(days.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (phone.Text.Trim() != "")
                sb.Append(",phone='").Append(phone.Text.Trim()).Append("'");
                //seat.phone = phone.Text.Trim();
            sb.Append(" where text='").Append(roomNumber.Text).Append("'");
            sb.Append(" insert into [SystemIds](systemId) values('" + systemId + "') ");
            var menu = dao.get_seat_menu(seat.text);
            //SeatType seatType = dc_new.SeatType.FirstOrDefault(x => x.id == seat.typeId);
            //var menu = dc_new.Menu.FirstOrDefault(x => x.id == seatType.menuId);
            if (!CheckZhong.Checked && menu != null)
            {
                sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                sb.Append(@"values('" + menu.name + "','" + seat.text + "','" + systemId + "',1," + menu.price + ",getdate(),'");
                sb.Append(LogIn.m_User.id + "','False')");
            }
            else if (CheckZhong.Checked)
            {
                var zhong_menu = dao.get_Menu("name", "钟点房");
                sb.Append(@" insert into [Orders](menu, text,systemId,number,money,inputTime,inputEmployee,paid) ");
                sb.Append(@"values('" + zhong_menu.name + "','" + seat.text + "','" + systemId + "',1," + zhong_menu.price + ",getdate(),'");
                sb.Append(LogIn.m_User.id + "','False')");
            }

            //dc_new.SubmitChanges();

            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("开房失败，请重试!");
                return;
            }

            string now = DateTime.Now.ToString("yyy-MM-dd HH:ss");
            PrintRoomDepositReceipt.Print_DataGridView("押金单客人联", seat, LogIn.m_User.name, 
                name.Text, phone.Text, now, days.Value.ToString("yyyy-MM-dd HH:mm"), deposit.Text, 
                LogIn.options.companyName);

            PrintRoomDepositReceipt.Print_DataGridView("押金单存根联", seat, LogIn.m_User.name,
                name.Text, phone.Text, now, days.Value.ToString("yyyy-MM-dd HH:mm"), deposit.Text,
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

        //预定
        private void btn_reserve_Click(object sender, EventArgs e)
        {
            var seat = dao.get_seat("text", roomNumber.Text);
            //var dc_new = new BathDBDataContext(LogIn.connectionString);
            //var seat = dc_new.Seat.FirstOrDefault(x => x.text == roomNumber.Text);
            if (seat == null)
            {
                BathClass.printErrorMsg("房间号" + roomNumber.Text + "不存在");
                return;
            }

            if (seat.status != SeatStatus.AVILABLE && seat.status != SeatStatus.PAIED && seat.status != SeatStatus.RESERVE)
            {
                BathClass.printErrorMsg("房间号" + roomNumber.Text + "不可用");
                return;
            }

            if (name.Text == "")
            {
                BathClass.printErrorMsg("需要输入客人姓名!");
                return;
            }

            if (phone.Text.Trim() == "")
            {
                BathClass.printErrorMsg("预定需要输入客人电话!");
                return;
            }

            if (seat.status == SeatStatus.PAIED)
            {
                dao.reset_seat("text='" + seat.text + "'");
                //BathClass.reset_seat(seat);
                //dc_new.SubmitChanges();
            }

            string systemId=dao.systemId();
            StringBuilder sb = new StringBuilder();
            sb.Append("update [Seat] set openEmployee='");
            sb.Append(LogIn.m_User.id);
            sb.Append("',openTime=getdate(),systemId='");
            sb.Append(systemId).Append("',chainId='").Append(chainId).Append("',deposit=");
            sb.Append(deposit.Text).Append(",status=9,dueTime='").Append(days.Value.ToString()).Append("'");
            sb.Append(",name='").Append(name.Text + "'");
            //seat.openEmployee = LogIn.m_User.id.ToString();
            //seat.openTime = BathClass.Now(LogIn.connectionString);
            //seat.systemId = BathClass.systemId(dc_new, LogIn.connectionString);
            //seat.chainId = chainId;
            //seat.deposit = MConvert<int>.ToTypeOrDefault(deposit.Text);
            //seat.status = 9;
            //seat.dueTime = days.Value;
            //seat.name = name.Text;
            if (phone.Text.Trim() != "")
                sb.Append(",phone='" + phone.Text.Trim() + "'");
                //seat.phone = phone.Text.Trim();

            sb.Append(" where text='" + roomNumber.Text + "'");
            sb.Append(" insert into [SystemIds](systemId) values('" + systemId + "') ");
            //dc_new.SubmitChanges();

            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("预定房间失败，请重试!");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TextDays_TextChanged(object sender, EventArgs e)
        {
            int d = MConvert<int>.ToTypeOrDefault(TextDays.Text.Trim(), 1);
            days.Value = Convert.ToDateTime(DateTime.Now.AddDays(d).ToString("yyyy-MM-dd") + " 12:00:00");
        }

    }
}
