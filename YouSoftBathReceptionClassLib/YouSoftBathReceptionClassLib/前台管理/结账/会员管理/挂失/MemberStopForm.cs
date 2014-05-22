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
    public partial class MemberStopForm : Form
    {
        //成员变量
        private DAO dao;
        private CCardInfo m_Member;

        //构造函数
        public MemberStopForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MemberStopForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读会员")
            {
                if (!get_member_by_db())
                    return;
                set_status();
            }
            else if (btnOk.Text == "挂失")
            {
                string cmd_str = @"update [CardInfo] set state='挂失' where CI_CardNo='" + m_Member.CI_CardNo + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("会员挂失失败，请重试!");
                    return;
                }
                //m_Member.state = "挂失";
                //db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
            else if (btnOk.Text == "启用")
            {
                string cmd_str = @"update [CardInfo] set state='在用' where CI_CardNo='" + m_Member.CI_CardNo + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("会员重新启用失败，请重试!");
                    return;
                }
                //m_Member.state = "启用";
                //db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        //设置会员卡参数
        private void set_status()
        {
            memberId.Text = m_Member.CI_CardNo;
            name.Text = m_Member.CI_Name;
            phone.Text = m_Member.CI_Telephone;
            type.Text = dao.get_MemberType("id=" + m_Member.CI_CardTypeNo).name;
            //type.Text = db.MemberType.FirstOrDefault(x => x.id == m_Member.CI_CardTypeNo).name;

            var cc = dao.get_CardCharges("CC_CardNo='" + m_Member.CI_CardNo + "'");
            //var cc = db.CardCharge.Where(x => x.CC_CardNo == m_Member.CI_CardNo);
            double debit = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            double lend = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);
            balance.Text = (debit - lend).ToString();

            var c = MConvert<int>.ToTypeOrDefault(m_Member.CI_CreditsUsed, 0);
            var cu = MConvert<int>.ToTypeOrDefault(MemberForm.memberSetting.money, 0);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();

            if (m_Member.state != "挂失")
                btnOk.Text = "挂失";
            else
                btnOk.Text = "启用";
        }

        //通过读数据库获取会员卡
        private bool get_member_by_db()
        {
            if (phone.Text != "")
                m_Member = dao.get_CardInfo("CI_Telephone='" + phone.Text + "'");
            //m_Member = db.CardInfo.FirstOrDefault(x => x.CI_Telephone == phone.Text);
            else if (name.Text != "")
            {
                var members = dao.get_CardInfos("CI_Telephone='" + phone.Text + "'");
                //var members = db.CardInfo.Where(x => x.CI_Name == name.Text);
                if (members.Count != 1)
                {
                    BathClass.printErrorMsg("名称不唯一或者不存在!");
                    return false;
                }
                m_Member = members.FirstOrDefault();
            }
            else if (memberId.Text != "")
                m_Member = dao.get_CardInfo("CI_CardNo='" + memberId.Text + "'");
                //m_Member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == memberId.Text);

            if (m_Member == null)
            {
                BathClass.printErrorMsg("该会员卡不在本店登记!");
                return false;
            }

            return true;
        }

        //绑定快捷键
        private void MemberStopForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void memberId_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
