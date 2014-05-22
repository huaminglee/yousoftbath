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
    public partial class MemberPopForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private CardInfo m_member = new CardInfo();
        private HotelRoom m_Seat;

        //构造函数
        public MemberPopForm(HotelRoom seat)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seat = seat;
            InitializeComponent();
        }

        //对话框载入
        private void MemberPopForm_Load(object sender, EventArgs e)
        {
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

            id.Text = card_data;
            //id.Text = "80120";
            m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text);
            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }
            name.Text = m_member.CI_Name;
            var t = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            if (t != null)
                type.Text = t.name;

            var cc = db.CardCharge.Where(x => x.CC_CardNo == m_member.CI_CardNo);
            int debit = BathClass.ToInt(cc.Sum(x => x.CC_DebitSum));
            int lend = BathClass.ToInt(cc.Sum(x => x.CC_LenderSum));
            balance.Text = (debit - lend).ToString();

            var c = BathClass.ToInt(m_member.CI_CreditsUsed);
            var cu = BathClass.ToInt(db.MemberSetting.FirstOrDefault().money);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();
            btnOk.Text = "充值";
        }
        
        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
                get_member();
            else if (btnOk.Text == "充值" && money.Text != "")
            {
                CardCharge cardCharge = new CardCharge();

                cardCharge.CC_CardNo = m_member.CI_CardNo;
                cardCharge.CC_DebitSum = Convert.ToDouble(money.Text);
                cardCharge.CC_ItemExplain = "会员卡充值-收";
                cardCharge.CC_InputOperator = LogIn.m_User.id.ToString();
                cardCharge.CC_InputDate = GeneralClass.Now;
                cardCharge.systemId = m_Seat.systemId;
                db.CardCharge.InsertOnSubmit(cardCharge);

                int sale_money = find_card_sale();
                if (sale_money != 0)
                {
                    CardCharge cardChargeSale = new CardCharge();
                    cardChargeSale.CC_CardNo = m_member.CI_CardNo;
                    cardChargeSale.CC_ItemExplain = "优惠送";
                    cardChargeSale.CC_DebitSum = sale_money;
                    cardChargeSale.CC_LenderSum = 0;
                    cardChargeSale.CC_InputOperator = LogIn.m_User.id.ToString();
                    cardChargeSale.CC_InputDate = GeneralClass.Now;
                    cardChargeSale.systemId = m_Seat.systemId;
                    db.CardCharge.InsertOnSubmit(cardChargeSale);

                    BathClass.printInformation("送" + sale_money + "元");
                }

                //var menu = db.Menu.FirstOrDefault(x => x.name == type.Text+"充值");
                Orders order = new Orders();
                order.menu = "储值卡充值";
                order.text = m_Seat.text;
                order.systemId = m_Seat.systemId;
                order.number = 1;
                order.inputTime = BathClass.Now(LogIn.connectionString);
                order.inputEmployee = LogIn.m_User.id.ToString();
                order.paid = false;
                order.money = Convert.ToDouble(money.Text);
                db.Orders.InsertOnSubmit(order);

                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        //查找会员卡充值优惠方案
        private int find_card_sale()
        {
            int moneyPop = Convert.ToInt32(money.Text);
            int sale_money = 0;

            var sale_list = db.CardPopSale.OrderByDescending(x => x.mimMoney);
            foreach (var sl in sale_list)
            {
                if (moneyPop >= sl.mimMoney)
                {
                    sale_money = Convert.ToInt32(sl.saleMoney);
                    break;
                }
            }
            return sale_money;
        }
        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void money_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
