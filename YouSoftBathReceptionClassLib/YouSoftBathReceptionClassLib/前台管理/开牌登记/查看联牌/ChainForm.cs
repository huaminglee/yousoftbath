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

namespace YouSoftBathReception
{
    public partial class ChainForm : Form
    {
        private BathDBDataContext db;
        private Seat m_Seat;

        public ChainForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        private void OpenSeatForm_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        //显示单个或联排的手牌清单
        private void dgv_show()
        {
            dgv.Rows.Clear();
            if (m_Seat == null)
                return;

            var seats = db.Seat.Where(x => (m_Seat.chainId == null && x.text==m_Seat.text) ||
                (m_Seat.chainId != null && x.chainId == m_Seat.chainId));
            foreach (var seat in seats)
            {
                string[] row = new string[2];
                row[0] = seat.text;
                row[1] = db.SeatType.FirstOrDefault(x => x.id == seat.typeId).name;
                dgv.Rows.Add(row);
            }
        }

        //增加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            m_Seat = db.Seat.FirstOrDefault(x => x.text == seatBox.Text);
            if (m_Seat == null)
            {
                seatBox.SelectAll();
                seatBox.Focus();
                BathClass.printErrorMsg("手牌不存在!");
                return;
            }
            dgv_show();
            //seatBox.Enabled = false;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
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
                    if (dgv.Rows.Count != 0)
                        btnOk_Click(null, null);
                    else
                        btnAdd_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void seatBox_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

    }
}
