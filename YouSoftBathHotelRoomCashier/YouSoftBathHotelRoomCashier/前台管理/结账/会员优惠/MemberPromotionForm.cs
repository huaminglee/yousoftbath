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
    public partial class MemberPromotionForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<HotelRoom> m_Seats = new List<HotelRoom>();
        private string m_systemId;
        public CardInfo m_member = null;

        //构造函数
        public MemberPromotionForm(List<HotelRoom> seat, string systemid)
        {
            m_systemId = systemid;
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seats.AddRange(db.HotelRoom.Where(x => seat.Contains(x)));
            InitializeComponent();
        }

        //对话框载入
        private void MemberCardUsingForm_Load(object sender, EventArgs e)
        {
            //if (MainWindow.ic_dev <= 0)
            //{
            //    BathClass.printErrorMsg("会员卡读卡器设置不正确，请重新设置!");
            //    this.Close();
            //    return;
            //}
            get_member();
        }

        //获取会员卡
        private void get_member()
        {
            string card_data = "";
            string company_code = db.Options.FirstOrDefault().companyCode;

            bool st = false;
            var cardType = db.MemberSetting.FirstOrDefault().cardType;
            if (cardType == "SLE4442")
                st = ICCard.read_data_4442(company_code, ref card_data);
            else if (cardType == "M1")
                st = ICCard.read_data_M1(company_code, ref card_data);
            if (!st)
                return;

            m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == card_data);
            //m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == "05302");
            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }

            id.Text = m_member.CI_CardNo;
            name.Text = m_member.CI_Name;
            phone.Text = m_member.CI_Telephone;
            var t = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            if (t != null)
                memberType.Text = t.name;

            var cc = db.CardCharge.Where(x => x.CC_CardNo == m_member.CI_CardNo);
            double debit = BathClass.ToInt(cc.Sum(x => x.CC_DebitSum));
            double lend = BathClass.ToInt(cc.Sum(x => x.CC_LenderSum));
            balance.Text = (debit - lend).ToString();

            var c = BathClass.ToInt(m_member.CI_CreditsUsed);
            var cu = BathClass.ToInt(db.MemberSetting.FirstOrDefault().money);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();
            btnOk.Text = "会员打折";
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
                get_member();
            else if (btnOk.Text == "会员打折")
            {
                if (m_member.state == "挂失")
                {
                    BathClass.printErrorMsg("打折卡已挂失，无法使用！");
                    return;
                }
                find_promotion();

                foreach (var seat in m_Seats)
                {
                    seat.memberPromotionId = m_member.CI_CardNo;
                    seat.memberDiscount = true;
                }

                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
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

        //寻找优惠方案
        private void find_promotion()
        {
            //MemberType mType = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            //if (mType == null)
            //    return;

            //Promotion promotion = db.Promotion.FirstOrDefault(x => x.id == mType.offerId);
            //if (promotion == null)
            //    return;

            //Dictionary<string, double> offers = BathClass.disAssemble(db, promotion);

            //List<string> ids = new List<string>();
            //foreach (var s in m_Seats)
            //{
            //    if (s.memberDiscount == null || !Convert.ToBoolean(s.memberDiscount))
            //        ids.AddRange(s.systemId.Split('|').ToList());
            //}
            //var orders = db.Orders.Where(x => ids.Contains(x.systemId) && x.deleteEmployee == null);
            //foreach (string offerKey in offers.Keys)
            //{
            //    if (offerKey == "所有项目")
            //    {
            //        var tmp = orders.Where(x => !x.menu.Contains("套餐"));
            //        foreach (Orders order in tmp)
            //        {
            //            order.money = Math.Round(order.money * offers[offerKey], 0);
            //            if (order.comboId != null)
            //            {
            //                var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
            //                if (combo.priceType == "免项目")
            //                {
            //                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
            //                    var ms = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
            //                    if (ms.Contains(order.menu))
            //                    {
            //                        var freeOrder = orders.FirstOrDefault(x => x.menu == "套餐" + combo.id + "优惠");
            //                        freeOrder.money = Math.Round(freeOrder.money * offers[offerKey], 0);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else if (offerKey.Contains("类别$"))
            //    {
            //        string[] cats = offerKey.Split('$');
            //        int catId = db.Catgory.FirstOrDefault(x => x.name == cats[1]).id;
            //        var menus = db.Menu.Where(x => x.catgoryId == catId).Select(x => x.name);
            //        var tps = orders.Where(x => menus.Contains(x.menu));
            //        foreach (Orders o in tps)
            //        {
            //            o.money = Math.Round(o.money * offers[offerKey], 0);
            //            if (o.comboId != null)
            //            {
            //                var combo = db.Combo.FirstOrDefault(x => x.id == o.comboId);
            //                if (combo.priceType == "免项目")
            //                {
            //                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
            //                    var ms = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
            //                    if (ms.Contains(o.menu))
            //                    {
            //                        var freeOrder = orders.FirstOrDefault(x => x.menu == "套餐" + combo.id + "优惠");
            //                        if (freeOrder == null) continue;
            //                        freeOrder.money = Math.Round(freeOrder.money * offers[offerKey], 0);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var menuName = db.Menu.FirstOrDefault(x => x.id == Convert.ToInt32(offerKey));
            //        if (menuName == null)
            //            continue;

            //        var menu = menuName.name;
            //        var tps = orders.Where(x => !x.menu.Contains("套餐"));
            //        tps = orders.Where(x => x.menu == menu);
            //        foreach (Orders o in tps)
            //        {
            //            o.money = Math.Round(o.money * offers[offerKey], 0);
            //            if (o.comboId != null)
            //            {
            //                var combo = db.Combo.FirstOrDefault(x => x.id == o.comboId);
            //                if (combo.priceType == "免项目")
            //                {
            //                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
            //                    var ms = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
            //                    if (ms.Contains(o.menu))
            //                    {
            //                        var freeOrder = orders.FirstOrDefault(x => x.menu == "套餐" + combo.id + "优惠");
            //                        if (freeOrder == null) continue;
            //                        freeOrder.money = Math.Round(freeOrder.money * offers[offerKey], 0);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //db.SubmitChanges();
        }
    }
}
