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
using YouSoftUtil;

namespace YouSoftBathReception
{
    public partial class MemberPromotionForm : Form
    {
        //成员变量
        private DAO dao;
        private List<CSeat> m_Seats = new List<CSeat>();
        private string m_systemId;
        public CCardInfo m_member = null;
        private CMemberType cmemberType;
        private CMemberSetting memberSetting;
        private string cardType;

        private bool m_use_finger_pwd = true;

        //构造函数
        public MemberPromotionForm(List<CSeat> seat, string systemid)
        {
            InitializeComponent();
            try
            {
                dao = new DAO(LogIn.connectionString);
                m_systemId = systemid;
                m_Seats = seat;
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        //对话框载入
        private void MemberCardUsingForm_Load(object sender, EventArgs e)
        {
            try
            {
                memberSetting = dao.get_MemberSetting();
                cardType = memberSetting.cardType;
                if (cardType != "CT")
                    get_member();
                else
                    id.Enabled = true;

                if (!MConvert<bool>.ToTypeOrDefault(LogIn.options.启用会员卡密码, false) || LogIn.options.会员卡密码类型 != "指纹")
                {
                    m_use_finger_pwd = false;
                }
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
            }
        }

        //获取会员卡
        private void get_member()
        {
            string card_data = "";
            string company_code = LogIn.options.companyCode;

            bool st = false;
            if (cardType == "SLE4442")
                st = ICCard.read_data_4442(company_code, ref card_data);
            else if (cardType == "M1")
                st = ICCard.read_data_M1(company_code, ref card_data);
            else if (cardType == "CT")
            {
                card_data = id.Text;
                st = true;
            }
            if (!st)
                return;

            //card_data = "000000";
            m_member = dao.get_CardInfo("CI_CardNo='" + card_data + "'");
            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }

            id.Text = m_member.CI_CardNo;
            name.Text = m_member.CI_Name;
            phone.Text = m_member.CI_Telephone;

            cmemberType = dao.get_MemberType("id='" + m_member.CI_CardTypeNo + "'");
            if (memberType != null)
                memberType.Text = cmemberType.name;

            var cc = dao.get_CardCharges("CC_CardNo='" + m_member.CI_CardNo + "'");
            double debit = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            double lend = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);
            balance.Text = (debit - lend).ToString();

            var c = MConvert<int>.ToTypeOrDefault(m_member.CI_CreditsUsed, 0);
            var cu = MConvert<int>.ToTypeOrDefault(memberSetting.money, 0);
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
                if (m_member.state == "入库")
                {
                    BathClass.printErrorMsg("卡已入库，但未激活，无法使用！");
                    return;
                }
                if (m_member.state == "停用")
                {
                    BathClass.printErrorMsg("卡已停用，无法使用！，新卡卡号为:" + m_member.CI_Special1+"，补卡时间为："+
                        m_member.CI_SpecialDate1.Value.ToString("yyyy-MM-dd HH:mm"));
                    return;
                }
                if (cmemberType != null && MConvert<bool>.ToTypeOrDefault(cmemberType.userOneTimeOneDay, false) &&
                    dao.exist_instance("CardCharge", "datediff(day,CC_InputDate,getdate())=0 and cc_itemExplain!='售卡收' and cc_cardno='" + m_member.CI_CardNo + "'"))
                {
                    BathClass.printErrorMsg("此卡被限定一天只能使用一次，今天已经使用过!");
                    return;
                }

                if (cmemberType != null && MConvert<bool>.ToTypeOrDefault(cmemberType.LimitedTimesPerMonth, false) &&
                    dao.get_memberCard_useTimes_this_month(m_member.CI_CardNo) >= cmemberType.TimesPerMonth)
                {
                    BathClass.printErrorMsg("此卡被限定每月只能使用" + cmemberType.TimesPerMonth + "次，本月已达额度!");
                    return;
                }
                if (m_use_finger_pwd)
                {
                    var form = new MemberFingerForm(m_member);
                    form.ShowDialog();

                    if (!form.verified)
                    {
                        BathClass.printErrorMsg("验证指纹失败!");
                        return;
                    }
                }
                if (!find_promotion())
                    return;

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
        private bool find_promotion()
        {
            StringBuilder sb = new StringBuilder();

            var mType = dao.get_MemberType("id='" + m_member.CI_CardTypeNo.ToString() + "'");
            if (mType == null)
                return true;
            var promotion = dao.get_Promotion("id='" + mType.offerId.ToString() + "'");
            if (promotion == null)
                return true;

            Dictionary<string, string> offers = promotion.disAssemble();
            StringBuilder sb_orders = new StringBuilder();
            sb_orders.Append("(deleteEmployee is null and (priceType is null or priceType!='每小时' or (priceType='每小时' and inputEmployee not like '%电脑%')) and (");
            int count = m_Seats.Count;
            for (int i = 0; i < count; i++)
            {
                sb_orders.Append("systemId='").Append(m_Seats[i].systemId).Append("'");
                if (i != count - 1)
                    sb_orders.Append(" or ");
            }
            sb_orders.Append("))");
            var orders = dao.get_orders(sb_orders.ToString());
            undo_discount(orders);//取消打折
            foreach (string offerKey in offers.Keys)
            {
                var discount_info = offers[offerKey].Split('#');
                string discount_type = discount_info[0];
                double discount_val = Convert.ToDouble(discount_info[1]);
                if (offerKey == "所有项目")
                {
                    var tmp = orders.Where(x => !x.menu.Contains("套餐"));
                    foreach (COrders order in tmp)
                    {
                        order.money = Math.Round(order.money * discount_val, 0);
                        sb.Append(@" update [Orders] set money=").Append(order.money).Append(" where id=").Append(order.id);
                    }
                }
                else if (offerKey.Contains("类别$"))
                {
                    string[] cats = offerKey.Split('$');
                    int catId = dao.get_Catgory("name='" + cats[1].ToString() + "'").id;
                    var menus = dao.get_Menus("catgoryId=" + catId.ToString()).Select(x => x.name);
                    var tps = orders.Where(x => menus.Contains(x.menu));
                    foreach (COrders o in tps)
                    {
                        if (discount_type == "折扣")
                        {
                            o.money = Math.Round(o.money * discount_val, 0);
                            sb.Append(" update [Orders] set money=").Append(o.money).Append(" where id=").Append(o.id);
                        }
                        else if (discount_type == "折后价" && o.money != 0)
                        {
                            o.money = discount_val * o.number;
                            sb.Append(" update [Orders] set money=").Append(o.money).Append(" where id=").Append(o.id);
                        }
                    }
                }
                else
                {
                    var menuName = dao.get_Menu("id", offerKey);
                    if (menuName == null)
                        continue;

                    var menu = menuName.name;
                    var tps = orders.Where(x => !x.menu.Contains("套餐"));
                    tps = orders.Where(x => x.menu == menu);
                    foreach (COrders o in tps)
                    {
                        if (discount_type == "折扣")
                        {
                            o.money = Math.Round(o.money * discount_val, 0);
                            sb.Append(" update [Orders] set money=").Append(o.money).Append(" where id=").Append(o.id);
                        }
                        else if (discount_type == "折后价" && o.money != 0)
                        {
                            o.money = discount_val * o.number;
                            sb.Append(" update [Orders] set money=").Append(o.money).Append(" where id=").Append(o.id);
                        }
                    }
                }
            }

            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("会员打折失败!");
                return false;
            }
            return true;
        }

        //取消打折
        private void undo_discount(List<COrders> orders)
        {
            //var pars = new List<string>();
            //var vals = new List<string>();

            //vals.AddRange(m_Seats.Select(x => x.systemId).ToList());
            int count = m_Seats.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    pars.Add("systemId");
            //}

            //var orders = dao.get_orders(pars, vals, "or");
            StringBuilder sb = new StringBuilder();
            foreach (var order in orders)
            {
                reset_order_money(order);
                sb.Append(@" update [Orders] set money=").Append(order.money).Append(" where id=").Append(order.id);
            }
            //sb.Append(@"update [Seat] set memberDiscount='False', memberPromotionId=null where (");
            //for (int i = 0; i < count; i++)
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
        }

        private void reset_order_money(COrders order)
        {
            var menu = dao.get_Menu("name", order.menu);
            if (menu != null)
            {
                if (order.priceType == "每小时")
                    order.money = Convert.ToDouble(menu.addMoney);
                else if (order.comboId == null)
                    order.money = menu.price * order.number;
                else if (order.comboId != null)
                {
                    var combo = dao.get_Combo("id", order.comboId);
                    if (combo == null)
                        return;
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
                    if (!freeMenus.Contains(order.menu))
                        order.money = menu.price * order.number;
                }
            }
            else
            {
                var combo = dao.get_Combo("id", order.comboId);
                order.money = combo.get_combo_price(dao);
            }
        }
    }
}
