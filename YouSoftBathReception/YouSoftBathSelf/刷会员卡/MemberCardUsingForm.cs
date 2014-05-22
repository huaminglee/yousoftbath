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

namespace YouSoftBathSelf
{
    public partial class MemberCardUsingForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private double m_money = 0;
        public CardInfo m_member;

        //构造函数
        public MemberCardUsingForm(double m)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_money = m;
            InitializeComponent();
        }

        //对话框载入
        private void MemberCardUsingForm_Load(object sender, EventArgs e)
        {
            get_member();
            money.Text = m_money.ToString();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
                get_member();
            else if (btnOk.Text == "刷卡")
                usingCard();
        }

        //获取会员卡
        private void get_member()
        {
            string card_data = "";
            string company_code = db.Options.FirstOrDefault().companyCode;
            if (!ICCard.read_data(company_code, ref card_data))
                return;

            id.Text = card_data;
            m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == card_data);
            //m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == "80120");
            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }
            name.Text = m_member.CI_Name;
            phone.Text = m_member.CI_Telephone;
            var t = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            if (t != null)
                memberType.Text = t.name;

            var cc = db.CardCharge.Where(x => x.CC_CardNo == m_member.CI_CardNo);
            double debit = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            double lend = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);
            balance.Text = (debit - lend).ToString();

            var c = MConvert<int>.ToTypeOrDefault(m_member.CI_CreditsUsed, 0);
            var cu = MConvert<int>.ToTypeOrDefault(db.MemberSetting.FirstOrDefault().money, 0);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();
            btnOk.Text = "刷卡";
        }

        //刷卡
        private void usingCard()
        {
            if (money.Text == "")
            {
                BathClass.printErrorMsg("输入金额!");
                return;
            }

            if (Convert.ToDouble(balance.Text) < Convert.ToDouble(money.Text))
            {
                BathClass.printErrorMsg("会员卡余额不足!");
                return;
            }

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
    }
}
