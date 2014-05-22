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

namespace YouSoftBathFormClass
{
    public partial class ReserveOverDueForm : Form
    {
        private DAO dao;
        private CSeat m_seat;

        public ReserveOverDueForm(Seat seat)
        {
            dao = new DAO(LogIn.connectionString);
            m_seat = dao.get_seat("text='" + seat.text + "'");
            InitializeComponent();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            roomNumber.Text = m_seat.text;
            name.Text = m_seat.name;
            phone.Text = m_seat.phone;
            days.CustomFormat = "yyyy-MM-dd-HH:mm";
            days.Value = m_seat.dueTime.Value;

            
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
                        btn_reserve_Click(null, null);
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
            //var seat = dc_new.Seat.FirstOrDefault(x => x.text == roomNumber.Text);
            if (seat == null)
            {
                BathClass.printErrorMsg("房间号" + roomNumber.Text + "不存在");
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

            string cmd_str = @"update [Seat] set openEmployee='" + LogIn.m_User.id +
                "', dueTime='" + days.Value.ToString() + "',name='" + name.Text + "'";
            //seat.openEmployee = LogIn.m_User.id.ToString();
            //seat.dueTime = days.Value;
            //seat.name = name.Text;
            if (phone.Text.Trim() != "")
                cmd_str += ",phone='" + phone.Text.Trim() + "'";
                //seat.phone = phone.Text.Trim();

            cmd_str += " where text='" + seat.text + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("房间续订失败!");
                return;
            }
            //dc_new.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //退订
        private void btn_cancel_reserve_Click(object sender, EventArgs e)
        {
            if (!dao.reset_seat("text='" + roomNumber.Text + "'"))
            {
                BathClass.printErrorMsg("退订房间失败，请重试!");
                return;
            }
            //var dc_new = new BathDBDataContext(LogIn.connectionString);
            //var seat = dc_new.Seat.FirstOrDefault(x => x.text == roomNumber.Text);
            //BathClass.reset_seat(seat);
            //dc_new.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}
