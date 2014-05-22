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
    public partial class MemberResumForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private CardInfo m_Member;

        //构造函数
        public MemberResumForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MemberResumForm_Load(object sender, EventArgs e)
        {
        }

        //通过读数据库获取会员卡
        private bool get_member_by_db()
        {
            if (phone.Text != "")
                m_Member = db.CardInfo.FirstOrDefault(x => x.CI_Telephone == phone.Text);
            else if (name.Text != "")
            {
                var members = db.CardInfo.Where(x => x.CI_Name == name.Text);
                if (members.Count() != 1)
                {
                    BathClass.printErrorMsg("名称不唯一或者不存在!");
                    return false;
                }
                m_Member = members.FirstOrDefault();
            }
            else if (old_id.Text != "")
                m_Member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == old_id.Text);

            if (m_Member == null)
            {
                BathClass.printErrorMsg("该会员卡不在本店登记!");
                return false;
            }

            return true;
        }

        //设置会员卡参数
        private void set_status()
        {
            old_id.Text = m_Member.CI_CardNo;
            name.Text = m_Member.CI_Name;
            phone.Text = m_Member.CI_Telephone;
            type.Text = db.MemberType.FirstOrDefault(x => x.id == m_Member.CI_CardTypeNo).name;

            var cc = db.CardCharge.Where(x => x.CC_CardNo == m_Member.CI_CardNo);
            double debit = BathClass.ToInt(cc.Sum(x => x.CC_DebitSum));
            double lend = BathClass.ToInt(cc.Sum(x => x.CC_LenderSum));
            balance.Text = (debit - lend).ToString();

            var c = BathClass.ToInt(m_Member.CI_CreditsUsed);
            var cu = BathClass.ToInt(db.MemberSetting.FirstOrDefault().money);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();

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
            btnOk.Text = "补卡";
        }


        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读会员卡")
            {
                if (!get_member_by_db())
                    return;
                set_status();
            }
            else
            {
                if (!resumCard())
                    return;
                this.DialogResult = DialogResult.OK;
            }
        }

        //补卡
        private bool resumCard()
        {
            var cc_new = db.CardCharge.Where(x => x.CC_CardNo == id.Text);
            double debit = BathClass.ToInt(cc_new.Sum(x => x.CC_DebitSum));
            double lend = BathClass.ToInt(cc_new.Sum(x => x.CC_LenderSum));
            if (BathClass.printAskMsg("卡号:" + id.Text + "已有金额:" + (debit - lend).ToString() + "元，确认补卡？") != DialogResult.OK)
                return false;

            var cc = db.CardCharge.Where(x=>x.CC_CardNo==m_Member.CI_CardNo);
            foreach (var c in cc)
                c.CC_CardNo = id.Text;

            var ci = db.CardInfo.FirstOrDefault(x=>x.CI_CardNo == id.Text);

            bool newCi = false;
            if (ci == null)
            {
                newCi = true;
                ci = new CardInfo();
                ci.CI_CardNo = id.Text;//前台发卡
            }

            ci.CI_CardTypeNo = m_Member.CI_CardTypeNo;
            ci.CI_Name = m_Member.CI_Name;
            ci.CI_Sexno = m_Member.CI_Sexno;
            ci.CI_SendCardDate = m_Member.CI_SendCardDate;
            ci.CI_SendCardOperator = LogIn.m_User.id.ToString();
            ci.state = "在用";
            
            if (newCi)
                db.CardInfo.InsertOnSubmit(ci);

            var cm = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == m_Member.CI_CardNo);
            if (cm != null)
                db.CardInfo.DeleteOnSubmit(cm);

            db.SubmitChanges();
            return true;
        }

        //绑定快捷键
        private void MemberResumForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void old_id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
