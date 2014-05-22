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

namespace YouSoftBathBack
{
    public partial class OpenCardForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private string company_code = "";
        //private Seat m_Seat;

        //构造函数
        public OpenCardForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            //m_Seat = seat;
            InitializeComponent();
        }

        //对话框载入
        private void OpenCardForm_Load(object sender, EventArgs e)
        {
            memberType.Items.AddRange(db.MemberType.Select(x => x.name).ToArray());
            if (memberType.Items.Count != 0)
                memberType.SelectedIndex = 0;

            company_code = db.Options.FirstOrDefault().companyCode;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text) != null)
            {
                BathClass.printErrorMsg("已经发行卡号为" + id.Text + "的卡");
                return;
            }

            bool st = false;
            var ct = db.MemberSetting.FirstOrDefault().cardType;
            if (ct == "SLE4442")
                st = ICCard.destribute_card_4442(company_code, id.Text);
            else if (ct == "M1")
                st = ICCard.destribute_card_M1(company_code, id.Text);
            else if (ct == "CT")
                st = true;
            if (!st)
                return;

            CardInfo m_Element = new CardInfo();
            m_Element.CI_CardNo = id.Text;
            m_Element.CI_CardTypeNo = db.MemberType.FirstOrDefault(x => x.name == memberType.Text).id;
            m_Element.state = "入库";
            db.CardInfo.InsertOnSubmit(m_Element);

            CardCharge cardCharge = new CardCharge();
            cardCharge.CC_CardNo = m_Element.CI_CardNo;
            cardCharge.CC_ItemExplain = "售卡收";
            if (money.Text != "")
                cardCharge.CC_DebitSum = Convert.ToDouble(money.Text);
            cardCharge.CC_LenderSum = 0;
            cardCharge.CC_InputOperator = LogIn.m_User.id.ToString();
            cardCharge.CC_InputDate = DateTime.Now;
            db.CardCharge.InsertOnSubmit(cardCharge);

            //if (money.Text != "")
            //{
            //    int sale_money = find_card_sale();
            //    if (sale_money != 0)
            //    {
            //        //CardCharge cardChargeSale = new CardCharge();
            //        //cardChargeSale.CC_CardNo = m_Element.CI_CardNo;
            //        //cardChargeSale.CC_ItemExplain = "优惠送";
            //        //cardChargeSale.CC_DebitSum = sale_money;
            //        //cardChargeSale.CC_LenderSum = 0;
            //        //cardChargeSale.CC_InputOperator = LogIn.m_User.id.ToString();
            //        //cardChargeSale.CC_InputDate = GeneralClass.Now;
            //        //cardChargeSale.systemId = m_Seat.systemId;
            //        //db.CardCharge.InsertOnSubmit(cardChargeSale);

            //        BathClass.printInformation("送" + sale_money + "元");
            //    }


            //    //CardSale cardSale = new CardSale();
            //    //cardSale.memberId = m_Element.CI_CardNo;
            //    //cardSale.balance = 0;
            //    //if (payType.Text == "现金")
            //        //cardSale.cash = Convert.ToDouble(money.Text);
            //    //else if (payType.Text == "银联")
            //        //cardSale.bankUnion = Convert.ToDouble(money.Text);

            //    //cardSale.payTime = GeneralClass.Now;
            //    //cardSale.payEmployee = LogIn.m_User.id.ToString();
            //    //cardSale.macAddress = PCUtil.getMacAddr_Local();
            //    //db.CardSale.InsertOnSubmit(cardSale);

            //    //var menu = db.Menu.FirstOrDefault(x => x.name == memberType.Text);
            //    //Orders order = new Orders();
            //    //order.menu = menu.name;
            //    //order.text = m_Seat.text;
            //    //order.systemId = m_Seat.systemId;
            //    //order.number = 1;
            //    //order.inputTime = BathClass.Now(LogIn.connectionString);
            //    //order.inputEmployee = LogIn.m_User.id.ToString();
            //    //order.paid = false;

            //    //if (money.Text == "")
            //    //    order.money = menu.price;
            //    //else
            //    //order.money = Convert.ToDouble(money.Text);

            //    //db.Orders.InsertOnSubmit(order);
            //}

            db.SubmitChanges();
            //this.DialogResult = DialogResult.OK;

            id.Text = "";
            money.Text = "";
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
                case Keys.Escape:
                    this.Close();
                    break;
                //case Keys.Enter:
                //    btnOk_Click(null, null);
                //    break;
                default:
                    break;
            }
        }

        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
