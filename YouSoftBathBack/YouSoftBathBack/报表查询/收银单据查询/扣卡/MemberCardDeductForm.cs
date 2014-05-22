using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class MemberCardDeductForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        //public Member m_Member;

        //构造函数
        public MemberCardDeductForm(BathDBDataContext dc)
        {
            db = dc;
            InitializeComponent();
        }

        //对话框载入
        private void MemberCardDeductForm_Load(object sender, EventArgs e)
        {

        }

        //确定
        private void btnCard_Click(object sender, EventArgs e)
        {
            if (type.Text != "")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            //if (!get_member_by_db())
            //    return;

            //memberId.Text = m_Member.memberId.ToString();
            //name.Text = m_Member.name;
            //phone.Text = m_Member.phone;
            //balance.Text = m_Member.balance.ToString();
            //type.Text = db.MemberType.FirstOrDefault(x => x.id == m_Member.type).name;
        }

        //通过读数据库获取会员卡
        /*private bool get_member_by_db()
        {
            if (phone.Text != "")
                m_Member = db.Member.FirstOrDefault(x => x.phone == phone.Text);
            else if (name.Text != "")
            {
                var members = db.Member.Where(x => x.name == name.Text);
                if (members.Count() != 1)
                {
                    GeneralClass.printErrorMsg("名称不唯一或者不存在!");
                    return false;
                }
                m_Member = members.FirstOrDefault();
            }
            else if (memberId.Text != "")
                m_Member = db.Member.FirstOrDefault(x => x.memberId == memberId.Text);

            if (m_Member == null)
            {
                GeneralClass.printErrorMsg("该会员卡不在本店登记!");
                return false;
            }

            return true;
        }*/

        //绑定快捷键
        private void MemberCardDeductForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnCard_Click(null, null);
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
