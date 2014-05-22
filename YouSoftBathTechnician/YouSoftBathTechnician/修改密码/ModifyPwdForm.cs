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

namespace YouSoftBathTechnician
{
    public partial class ModifyPwdForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Employee m_User;

        //构造函数
        public ModifyPwdForm(string con_str, Employee user)
        {
            db = new BathDBDataContext(con_str);
            m_User = db.Employee.FirstOrDefault(x => x.id == user.id);
            InitializeComponent();
        }

        //对话框载入
        private void ModifyPwdForm_Load(object sender, EventArgs e)
        {
            id.Text = m_User.id.ToString();
            name.Text = m_User.name;
            job.Text = db.Job.FirstOrDefault(x => x.id == m_User.jobId).name;
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            string msg = validateTextFields();
            if (msg != "OK")
            {
                BathClass.printErrorMsg(msg);
                return;
            }

            m_User.password = IOUtil.GetMD5(pwdNew.Text);
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //取消
        private void canBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //检查数据
        private string validateTextFields()
        {
            if (pwdNew.Text == "" || pwdNew2.Text == "")
            {
                pwdNew.SelectAll();
                pwdNew.Focus();
                return "必须输入新密码!";
            }

            if (IOUtil.GetMD5(pwdOld.Text) != m_User.password)
            {
                pwdOld.SelectAll();
                pwdOld.Focus();
                return "输入旧密码不对!";
            }

            if (pwdNew.Text != pwdNew2.Text)
            {
                pwdNew2.SelectAll();
                pwdNew2.Focus();
                return "两次输入密码不一致!";
            }
            return "OK";
        }

        //绑定快捷键
        private void ModifyPwdForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okBtn_Click(null, null);
        }

        private void pwdOld_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
