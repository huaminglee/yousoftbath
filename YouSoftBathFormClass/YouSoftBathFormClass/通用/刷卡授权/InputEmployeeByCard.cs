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

namespace YouSoftBathFormClass
{
    public partial class InputEmployeeByCard : Form
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
        public InputEmployeeByCard(string con_str)
        {
            db = new BathDBDataContext(con_str);
            InitializeComponent();
        }

        //确定
        private void InputServerId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        //输入内容
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            text.Text += btn.Text;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            btnOk.Enabled = true;

            if (text.Text == "")
            {
                BathClass.printErrorMsg("需要输入内容!");
                return;
            }
            
            employee = db.Employee.FirstOrDefault(x => x.id.ToString() == text.Text);
            if (employee == null)
            {
                text.SelectAll();
                BathClass.printErrorMsg("输入工号不存在");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        //删除
        private void btnBack_Click(object sender, EventArgs e)
        {
            btnBack.Enabled = false;
            btnBack.Enabled = true;

            if (text.Text == "")
                return;

            text.Text = text.Text.Substring(0, text.Text.Length - 1);
        }

        private void text_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
