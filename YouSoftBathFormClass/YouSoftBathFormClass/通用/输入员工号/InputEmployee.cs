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

namespace YouSoftBathFormClass
{
    public partial class InputEmployee : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public Employee employee = null;

        //构造函数
        public InputEmployee(string hint)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
            lHint.Text = hint;
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
            text.SelectionStart = text.Text.Length;
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

            var ops = db.Options.FirstOrDefault();
            var use_card = MConvert<bool>.ToTypeOrDefault(ops.启用员工服务卡, false);
            if (use_card)
                employee = db.Employee.FirstOrDefault(x => x.cardId == text.Text);
            else
                employee = db.Employee.FirstOrDefault(x => x.id == text.Text);
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
            if (!text.ContainsFocus)
            {
                btnBack.Enabled = false;
                btnBack.Enabled = true;

                if (text.Text == "")
                    return;

                text.Text = text.Text.Substring(0, text.Text.Length - 1);
                text.SelectionStart = text.Text.Length;
            }
        }

        private void text_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
