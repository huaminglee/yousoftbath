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
    public partial class MemberPromotionOptionForm : Form
    {
        //成员变量
        BathDBDataContext db;
        List<HotelRoom> m_Seats = new List<HotelRoom>();
        public CardInfo m_Member = null;

        //构造函数
        public MemberPromotionOptionForm(List<HotelRoom> seats)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seats.AddRange(db.HotelRoom.Where(x => seats.Contains(x)).ToArray());
            InitializeComponent();
        }

        //对话框载入
        private void TransferSelectForm_Load(object sender, EventArgs e)
        {
            btnUndoDiscount.Text = "取消打折\n(Space)";
            btnDiscount.Text = "会员打折\n(Enter)";
            btnCancel.Text = "退出\n(Esc)";
        }

        private void TransferSelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnDiscount_Click(null, null);
            else if (e.KeyCode == Keys.Space)
                btnUndoDiscount_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        //会员打折
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            BathClass.sendMessageToCamera(db, m_Seats[0].systemId);
            var id = string.Join("|", m_Seats.Select(x => x.systemId).ToArray());
            MemberPromotionForm memberPromotionForm = new MemberPromotionForm(m_Seats, id);
            memberPromotionForm.ShowDialog();
            m_Member = memberPromotionForm.m_member;
            this.Close();
        }

        //取消打折
        private void btnUndoDiscount_Click(object sender, EventArgs e)
        {
            List<string> ids = new List<string>();
            foreach (var s in m_Seats)
            {
                s.memberDiscount = null;
                ids.AddRange(s.systemId.Split('|').ToList());
            }
            var orders = db.Orders.Where(x => ids.Contains(x.systemId) && x.deleteEmployee == null);
            foreach (var order in orders)
            {
                reset_order_money(order);
            }
            db.SubmitChanges();
        }

        private void reset_order_money(Orders order)
        {
            var menu = db.Menu.FirstOrDefault(x => x.name == order.menu);
            if (menu != null)
            {
                if (order.priceType == "每小时")
                    order.money = Convert.ToDouble(menu.addMoney);
                else if (order.comboId == null)
                    order.money = menu.price;
                else if (order.comboId != null)
                {
                    var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
                    if (combo == null)
                        return;
                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                    var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    if (!freeMenus.Contains(order.menu))
                        order.money = menu.price;
                }
            }
            else
            {
                var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
                order.money = BathClass.get_combo_price(db, combo);
            }
        }
    }
}
