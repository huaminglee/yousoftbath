using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftUtil;
using YouSoftBathGeneralClass;

namespace YouSoftBathFormClass
{
    public partial class InputEmployeeByPwd : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Employee _employee = null;

        public Employee employee
        {
            get { return _employee; }
            set { _employee = value; }
        }

        //构造函数
        public InputEmployeeByPwd(string con_str)
        {
            db = new BathDBDataContext(con_str);
            InitializeComponent();
        }

        //构造函数
        public InputEmployeeByPwd()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //确定
        private void InputServerId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okBtn_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
                return;

            if (pwd.Text == "")
            {
                pwd.Focus();
                return;
            }

            if (employee == null)
            {
                id.SelectAll();
                id.Focus();
                BathClass.printErrorMsg("该员工号不存在！");
                return;
            }
            if (IOUtil.GetMD5(pwd.Text) != employee.password)
            {
                pwd.SelectAll();
                pwd.Focus();
                BathClass.printErrorMsg("密码不对!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void id_TextChanged(object sender, EventArgs e)
        {
            if (id.Text == "") return;

            employee = db.Employee.FirstOrDefault(x => x.id.ToString() == id.Text);
            if (employee != null)
            {
                name.Text = employee.name;
                job.Text = db.Job.FirstOrDefault(x => x.id == employee.jobId).name;
            }
            else
            {
                name.Text = "";
                job.Text = "";
            }
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
