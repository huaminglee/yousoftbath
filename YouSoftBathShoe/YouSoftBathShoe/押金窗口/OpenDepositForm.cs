using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathShoe
{
    public partial class OpenDepositForm : Form
    {
        private DAO dao;
        private CSeat m_seat;
        //private BathDBDataContext db;
        private CSeatType m_seatType;
        
        public OpenDepositForm(CSeat seat)
        {
            m_seat = seat;
            InitializeComponent();
        }

        private void OpenDepositForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void OpenDepositForm_Load(object sender, EventArgs e)
        {
            //db = new BathDBDataContext(LogIn.connectionString);
            //m_seatType = db.SeatType.FirstOrDefault(x => x.id == m_seat.typeId);
            dao = new DAO(LogIn.connectionString);
            m_seatType = dao.get_seattype("id", m_seat.typeId);
            depositMin.Text = m_seatType.depositeAmountMin.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int ds = MConvert<int>.ToTypeOrDefault(deposit.Text, 0);
            if (ds < m_seatType.depositeAmountMin)
            {
                BathClass.printErrorMsg("所交押金金额小于最低限制!");
                return;
            }
            if (!dao.execute_command("update [Seat] set deposit="+ds+" where id="+m_seat.id))
            {
                BathClass.printErrorMsg("手牌交押金失败！");
                return;
            }
            //db.Seat.FirstOrDefault(x => x.id == m_seat.id).deposit = ds;
            //db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void deposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void deposit_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
