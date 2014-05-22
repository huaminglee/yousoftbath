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
    public partial class OpenCardForm : Form
    {
        //成员变量
        private DAO dao;
        private string company_code = "";
        private CSeat m_Seat;

        //构造函数
        public OpenCardForm(CSeat seat)
        {
            m_Seat = seat;
            InitializeComponent();
        }

        //对话框载入
        private void OpenCardForm_Load(object sender, EventArgs e)
        {
            //memberType.Items.AddRange(db.MemberType.Select(x => x.name).ToArray());
            //if (memberType.Items.Count != 0)
                //memberType.SelectedIndex = 0;
            //payType.SelectedIndex = 0;
            gender.SelectedIndex = 0;
            status.SelectedIndex = 0;
            dao = new DAO(LogIn.connectionString);
            //string card_data = "";
            company_code = LogIn.options.companyCode;
            //if (ICCard.read_data(company_code, ref card_data))
            //{
            //    id.Text = card_data;
            //}
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            //前台发卡
            //if (db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text) != null)
            //{
            //    BathClass.printErrorMsg("已经发行卡号为" + id.Text + "的卡");
            //    return;
            //}

            //if (!ICCard.destribute_card(company_code, id.Text))
            //{
            //    return;
            //}
            
            //前台发卡
            //CardInfo m_Element = new CardInfo();//前台发卡

            CCardInfo m_Element = dao.get_CardInfo("CI_CardNo='" + id.Text + "'");
            //CardInfo m_Element = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text);//后台发卡
            if (m_Element == null)
            {
                BathClass.printErrorMsg("卡未入库！");
                return;
            }
            if (m_Element.CI_Name != null && BathClass.printAskMsg("您确定需要修改卡号" + m_Element.CI_CardNo + "姓名：" +
                m_Element.CI_Name + "的信息？") != DialogResult.Yes)
                return;

            //m_Element.CI_CardNo = id.Text;//前台发卡
            //m_Element.CI_CardTypeNo = db.MemberType.FirstOrDefault(x => x.name == memberType.Text).id;

            string cmd_str = @"update [CardInfo] set CI_Name='" + name.Text + "',"
                            + @"CI_Sexno='" + gender.Text + "',"
                            + @"CI_SendCardDate=getdate(),"
                            + @"CI_SendCardOperator='" + LogIn.m_User.id + "',"
                            + @"state='在用'";


            if (birthday.Value.ToShortDateString() != DateTime.Now.ToShortDateString())
            {
                cmd_str += ",birthday='" + birthday.Value.ToString() + "'";
                //m_Element.birthday = birthday.Value;
            }

            if (mobile.Text != "")
                cmd_str += ",CI_Telephone='" + mobile.Text + "'";
                //m_Element.CI_Telephone = mobile.Text;

            if (address.Text != "")
                cmd_str += ",CI_Address='" + address.Text + "'";
                //m_Element.CI_Address = address.Text;

            cmd_str += " where CI_CardNo='" + m_Element.CI_CardNo + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("会员资料录入失败，请重试!");
                return;
            }
            //前台发卡
            //db.CardInfo.InsertOnSubmit(m_Element);

            //CardCharge cardCharge = new CardCharge();
            //cardCharge.CC_CardNo = m_Element.CI_CardNo;
            //cardCharge.CC_ItemExplain = "售卡收";
            //if (money.Text != "")
            //    cardCharge.CC_DebitSum = Convert.ToDouble(money.Text);
            //cardCharge.CC_LenderSum = 0;
            //cardCharge.CC_InputOperator = LogIn.m_User.id.ToString();
            //cardCharge.CC_InputDate = GeneralClass.Now;
            //cardCharge.systemId = m_Seat.systemId;
            //db.CardCharge.InsertOnSubmit(cardCharge);
            
            //if (money.Text != "")
            //{
                //int sale_money = find_card_sale();//前台发卡
                //if (sale_money != 0)
                //{
                    
                    //CardCharge cardChargeSale = new CardCharge();
                    //cardChargeSale.CC_CardNo = m_Element.CI_CardNo;
                    //cardChargeSale.CC_ItemExplain = "优惠送";
                    //cardChargeSale.CC_DebitSum = sale_money;
                    //cardChargeSale.CC_LenderSum = 0;
                    //cardChargeSale.CC_InputOperator = LogIn.m_User.id.ToString();
                    //cardChargeSale.CC_InputDate = GeneralClass.Now;
                    //cardChargeSale.systemId = m_Seat.systemId;
                    //db.CardCharge.InsertOnSubmit(cardChargeSale);

                //    BathClass.printInformation("送" + sale_money + "元");
                //}


            //    var menu = db.Menu.FirstOrDefault(x => x.name == memberType.Text);
            //    Orders order = new Orders();
            //    order.menu = menu.name;
            //    order.text = m_Seat.text;
            //    order.systemId = m_Seat.systemId;
            //    order.number = 1;
            //    order.inputTime = BathClass.Now(LogIn.connectionString);
            //    order.inputEmployee = LogIn.m_User.id.ToString();
            //    order.paid = false;

            //    if (money.Text == "")
            //        order.money = menu.price;
            //    else
            //        order.money = Convert.ToDouble(money.Text);

            //    db.Orders.InsertOnSubmit(order);
            //}
            //前台发卡结束

            //db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
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

        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
