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
        private DAO dao;
        private CCardInfo m_Member;
        private string cardType;

        //构造函数
        public MemberResumForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MemberResumForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            cardType = MemberForm.memberSetting.cardType;
            id.ReadOnly = !(cardType == "CT");
        }

        //通过读数据库获取会员卡
        private bool get_member_by_db()
        {
            if (phone.Text != "")
                m_Member = dao.get_CardInfo("CI_Telephone='" + phone.Text + "'");
            //m_Member = db.CardInfo.FirstOrDefault(x => x.CI_Telephone == phone.Text);
            else if (name.Text != "")
            {
                var members = dao.get_CardInfos("CI_Name='" + name.Text + "'");
                //var members = db.CardInfo.Where(x => x.CI_Name == name.Text);
                if (members.Count() != 1)
                {
                    BathClass.printErrorMsg("名称不唯一或者不存在!");
                    return false;
                }
                m_Member = members.FirstOrDefault();
            }
            else if (old_id.Text != "")
                m_Member = dao.get_CardInfo("CI_CardNo='" + old_id.Text+"'");
                //m_Member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == old_id.Text);

            if (m_Member == null)
            {
                BathClass.printErrorMsg("该会员卡不在本店登记!");
                return false;
            }
            if (m_Member.state == null || m_Member.state == "在用")
                return true;
            else
            {
                BathClass.printErrorMsg("该会员卡状态为" + m_Member.state + "，不能补卡!");
                return false;
            }
        }

        //设置会员卡参数
        private void set_status()
        {
            old_id.Text = m_Member.CI_CardNo;
            name.Text = m_Member.CI_Name;
            phone.Text = m_Member.CI_Telephone;
            type.Text = dao.get_MemberType("id=" + m_Member.CI_CardTypeNo).name;

            var cc = dao.get_CardCharges("CC_CardNo='" + m_Member.CI_CardNo + "'");
            double debit = MConvert<double>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            double lend = MConvert<double>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);
            balance.Text = (debit - lend).ToString();

            var c = MConvert<int>.ToTypeOrDefault(m_Member.CI_CreditsUsed, 0);
            var cu = MConvert<int>.ToTypeOrDefault(MemberForm.memberSetting.money, 0);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();

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

            //card_data = "0000110";
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
                {
                    BathClass.printErrorMsg("补卡失败!");
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        //补卡
        private bool resumCard()
        {
            bool exist_new_card = dao.exist_instance("CardInfo", "CI_CardNo='" + id.Text + "'");
            if (exist_new_card)
            {
                var balance_new = dao.get_member_balance(id.Text);
                if (BathClass.printAskMsg("卡号:" + id.Text + "已有金额:" + balance_new.ToString() + "元，确认补卡？确认将删除新卡原有金额!") != DialogResult.Yes)
                    return false;
            }

            //if (!dao.delete_table_rows("CardCharge", "CC_CardNo='" + id.Text + "'"))
            //{
            //    BathClass.printErrorMsg("删除新卡金额失败，请重试!");
            //    return;
            //}

            string cmd_str = @"delete from [CardCharge] where CC_CardNo='" + id.Text + "'";

            //db_new.CardCharge.DeleteAllOnSubmit(cc_new);
            //db_new.SubmitChanges();

            cmd_str += @" update [CardInfo] set state='停用', CI_Special1='" + id.Text + "',CI_SpecialDate1=getdate() where CI_CardNo='" + old_id.Text + "'";
            cmd_str += @" insert into [CardCharge](CC_CardNo, CC_AccountNO, CC_ItemNo, CC_ItemExplain, CC_BeginSum, CC_DebitSum,"
                + "CC_LenderSum, CC_InputOperator, CC_InputDate, expense, systemId) select'" + id.Text
                + "', CC_AccountNO, CC_ItemNo, CC_ItemExplain, CC_BeginSum, CC_DebitSum,CC_LenderSum,"
                + "CC_InputOperator, CC_InputDate, expense, systemId from [CardCharge] where CC_CardNo='"
                + old_id.Text + "'";
            //cmd_str += @" update [CardCharge] set CC_CardNo='" + id.Text + "' where CC_CardNO='" + old_id.Text + "' ";
            //var cc = db_new.CardCharge.Where(x => x.CC_CardNo == old_id.Text);
            //foreach (var c in cc)
                //c.CC_CardNo = id.Text;

            //var ci = db_new.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text);

            if (exist_new_card)
            {
                cmd_str += @"update [CardInfo] set CI_CardTypeNo=" + m_Member.CI_CardTypeNo + ","
                        + @"CI_Name='" + m_Member.CI_Name + "',"
                        + @"CI_Sexno='" + m_Member.CI_Sexno + "',"
                        + @"CI_Telephone='" + m_Member.CI_Telephone + "',"
                        + @"birthday='" + m_Member.birthday.ToString() + "',"
                        + @"CI_SendCardDate='" + m_Member.CI_SendCardDate.Value.ToString() + "',"
                        + @"CI_SendCardOperator='" + LogIn.m_User.id + "',"
                        + @"state='在用' where CI_CardNo='" + id.Text + "' ";
            }
            else
            {
                cmd_str += @"insert into [CardInfo]("
                          + @"CI_CardNo, CI_CardTypeNo, CI_Name,CI_Sexno,"
                          + @"CI_Telephone,birthday,CI_SendCardDate,CI_SendCardOperator,state) values('" + id.Text + "','"
                          + m_Member.CI_CardTypeNo + "','"
                          + m_Member.CI_Name + "','"
                          + m_Member.CI_Sexno + "','"
                          + m_Member.CI_Telephone + "','"
                          + m_Member.birthday.ToString() + "','"
                          + m_Member.CI_SendCardDate.ToString() + "','"
                          + LogIn.m_User.id + "','在用')";
            }
            //bool newCi = false;
            //if (ci == null)
            //{
            //    newCi = true;
            //    ci = new CardInfo();
            //    ci.CI_CardNo = id.Text;//前台发卡
            //}

            //ci.CI_CardTypeNo = m_Member.CI_CardTypeNo;
            //ci.CI_Name = m_Member.CI_Name;
            //ci.CI_Sexno = m_Member.CI_Sexno;
            //ci.CI_Telephone = m_Member.CI_Telephone;
            //ci.birthday = m_Member.birthday;
            //ci.CI_SendCardDate = m_Member.CI_SendCardDate;
            //ci.CI_SendCardOperator = LogIn.m_User.id;
            //ci.state = "在用";
            
            //if (newCi)
            //    db_new.CardInfo.InsertOnSubmit(ci);

            //cmd_str += @" delete from [CardInfo] where CI_CardNo='" + old_id.Text + "'";
            //var cm = db_new.CardInfo.FirstOrDefault(x => x.CI_CardNo == old_id.Text);
            //if (cm != null)
            //    db_new.CardInfo.DeleteOnSubmit(cm);

            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("补卡失败，请重试!");
                return false;
            }
            //db_new.SubmitChanges();
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
