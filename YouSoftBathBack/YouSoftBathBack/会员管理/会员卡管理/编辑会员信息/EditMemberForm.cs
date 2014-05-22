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
    public partial class EditMemberForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private CardInfo m_Card;

        //构造函数
        public EditMemberForm(BathDBDataContext dc, CardInfo ci)
        {
            db = dc;
            m_Card = ci;
            InitializeComponent();
        }

        //对话框载入
        private void OpenCardForm_Load(object sender, EventArgs e)
        {
            memberType.Items.AddRange(db.MemberType.Select(x => x.name).ToArray());

            id.Text = m_Card.CI_CardNo;
            memberType.Text = db.MemberType.FirstOrDefault(x => x.id == m_Card.CI_CardTypeNo).name;
            name.Text = m_Card.CI_Name;
            gender.Text = m_Card.CI_Sexno;
            status.SelectedIndex = 0;
            address.Text = m_Card.CI_Address;
            mobile.Text = m_Card.CI_Telephone;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_Card.CI_CardTypeNo = db.MemberType.FirstOrDefault(x => x.name == memberType.Text).id;
            m_Card.CI_Name = name.Text;
            m_Card.CI_Sexno = gender.Text;
            m_Card.CI_SendCardOperator = LogIn.m_User.id.ToString();

            if (birthday.Value.ToShortDateString() != DateTime.Now.ToShortDateString())
                m_Card.birthday = birthday.Value;

            if (mobile.Text != "")
                m_Card.CI_Telephone = mobile.Text;

            if (address.Text != "")
                m_Card.CI_Address = address.Text;
            db.SubmitChanges();
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
