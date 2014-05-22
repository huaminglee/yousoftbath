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

namespace YouSoftBathReception
{
    public partial class TransferSelectForm : Form
    {
        //成员变量
        private BathDBDataContext db;
        private List<Seat> m_Seats = new List<Seat>();
        private Seat m_Seat;
        private SeatExpenseForm m_form;
        private bool seatlock;


        //构造函数
        public TransferSelectForm(List<Seat> seats, SeatExpenseForm form)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            //m_Seat = db.Seat.FirstOrDefault(x => x.text == seat.text);
            foreach (var s in seats)
            {
                m_Seats.Add(db.Seat.FirstOrDefault(x => x.text == s.text));
            }

            m_Seat = m_Seats[0];
            m_form = form;
            InitializeComponent();
        }

        //对话框载入
        private void TransferSelectForm_Load(object sender, EventArgs e)
        {
            seatlock = db.Options.FirstOrDefault().启用手牌锁.Value;
            if (seatlock)
            {
                btnOrderClock.Enabled = false;
                //btnRestore.Enabled = false;
            }
        }

        //停止消费
        private void btnOnClock_Click(object sender, EventArgs e)
        {
            InputSeatForm seatForm = new InputSeatForm(2);
            if (seatForm.ShowDialog() != DialogResult.OK)
                return;

            if (m_Seats.FirstOrDefault(x => x.text == seatForm.m_Seat.text) != null)
            {
                BathClass.printErrorMsg("转账手牌中已经包含该手牌，请重新输入");
                return;
            }

            string systemId = seatForm.m_Seat.systemId;
            foreach (Seat s in m_Seats)
            {
                var orderList = db.Orders.Where(x => x.systemId == s.systemId && x.deleteEmployee == null && !x.paid);
                foreach (Orders order in orderList)
                {
                    order.systemId = systemId;
                    if (order.priceType == "每小时")
                    {
                        order.priceType = "停止消费";
                        order.money = order.money * Math.Ceiling((GeneralClass.Now - order.inputTime).TotalHours);
                    }
                }
                s.status = 3;
            }
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        //继续消费
        private void btnOrderClock_Click(object sender, EventArgs e)
        {
            //InputSeatForm seatForm = new InputSeatForm(2);
            //if (seatForm.ShowDialog() != DialogResult.OK)
            //    return;

            //var orderList = db.Orders.Where(x => x.systemId == m_Seat.systemId);
            //foreach (Orders order in orderList)
            //{
            //    order.systemId = seatForm.m_Seat.systemId;
            //}
            //BathClass.reset_seat(seat);
            //m_Seat.status = 3;
            //db.SubmitChanges();
            //this.DialogResult = DialogResult.OK;
        }

        private void TransferSelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOnClock_Click(null, null);
            else if (e.KeyCode == Keys.Space)
                btnOrderClock_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        //恢复
        private void btnRestore_Click(object sender, EventArgs e)
        {
            //var orderList = db.Orders.Where(x => x.text == m_Seat.text && x.systemId != m_Seat.systemId && !x.paid);
            //foreach (Orders order in orderList)
            //{
            //    order.systemId = m_Seat.systemId;
            //}

            var orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.text != m_Seat.text && !x.paid);
            var seats = new List<Seat>();
            foreach (var order in orders)
            {
                var seat = db.Seat.FirstOrDefault(x => x.text == order.text);
                if (seat.status != 3)
                {
                    BathClass.printErrorMsg("手牌" + seat.text + "已经重新使用，无法恢复转账！");
                    continue;
                }

                order.systemId = seat.systemId;
                if (!seats.Contains(seat))
                    seats.Add(seat);
            }
            foreach (var seat in seats)
            {
                seat.status = 2;
            }

            db.SubmitChanges();
            //m_form.dgvExpense_show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
