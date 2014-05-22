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
        private DAO dao;
        List<CSeat> m_Seats = new List<CSeat>();
        public CCardInfo m_Member = null;

        //构造函数
        public MemberPromotionOptionForm(List<CSeat> seats)
        {
            dao = new DAO(LogIn.connectionString);
            m_Seats = seats;
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
            //BathClass.sendMessageToCamera(db, m_Seats[0].systemId);
            var id = string.Join("|", m_Seats.Select(x => x.systemId).ToArray());
            MemberPromotionForm memberPromotionForm = new MemberPromotionForm(m_Seats, id);
            memberPromotionForm.ShowDialog();
            m_Member = memberPromotionForm.m_member;
            this.Close();
        }

        //取消打折
        private void btnUndoDiscount_Click(object sender, EventArgs e)
        {
            //List<string> ids = new List<string>();
            //foreach (var s in m_Seats)
            //{
            //    s.memberDiscount = null;
            //    ids.AddRange(s.systemId.Split('|').ToList());
            //}

            var pars = new List<string>();
            var vals = new List<string>();
            
            vals.AddRange(m_Seats.Select(x => x.systemId).ToList());
            int count = m_Seats.Count;
            for (int i = 0; i < count; i++ )
            {
                pars.Add("systemId");
            }
            
            var orders = dao.get_orders(pars, vals, "or");
            StringBuilder sb = new StringBuilder();
            //var orders = db.Orders.Where(x => ids.Contains(x.systemId) && x.deleteEmployee == null);
            //string cmd_str = "";
            foreach (var order in orders)
            {
                reset_order_money(order);
                sb.Append(@" update [Orders] set money=");
                sb.Append(order.money.ToString());
                sb.Append(" where id=");
                sb.Append(order.id.ToString());
            }
            //sb.Append(@"update [Seat] set memberDiscount='False', memberPromotionId=null where (");
            //for (int i = 0; i < count; i++ )
            //{
            //    sb.Append("text='");
            //    sb.Append(m_Seats[i].text);
            //    sb.Append("'");
            //    if (i != count - 1)
            //        sb.Append(" or ");
            //}
            //sb.Append(")");
            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("取消打折失败,请重试!");
                return;
            }

            //db.SubmitChanges();
        }

        private void reset_order_money(COrders order)
        {
            var menu = dao.get_Menu("name", order.menu);
            //var menu = db.Menu.FirstOrDefault(x => x.name == order.menu);
            if (menu != null)
            {
                if (order.priceType == "每小时")
                    order.money = Convert.ToDouble(menu.addMoney);
                else if (order.comboId == null)
                    order.money = menu.price * order.number;
                else if (order.comboId != null)
                {
                    //var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
                    var combo = dao.get_Combo("id", order.comboId);
                    if (combo == null)
                        return;
                    //var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                    var freeIds = combo.disAssemble_freeIds();
                    var pars = new List<string>();
                    var vals = new List<string>();
                    int count = freeIds.Count;
                    for (int i = 0; i < count; i++)
                    {
                        pars.Add("id");
                        vals.Add(freeIds[i].ToString());
                    }
                    var freeMenus = dao.get_Menus(pars, vals, "or").Select(x => x.name);
                    //var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    if (!freeMenus.Contains(order.menu))
                        order.money = menu.price * order.number;
                }
            }
            else
            {
                var combo = dao.get_Combo("id", order.comboId);
                //var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
                order.money = combo.get_combo_price(dao);
            }
        }
    }
}
