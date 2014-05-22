using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathFormClass
{
    public partial class InputNumber : Form
    {
        private bool could_be_negative;
        //成员变量
        public double number
        {
            get
            {
                return Convert.ToInt32(text.Text);
            }
        }

        //构造函数
        public InputNumber(string hint, bool _could_be_negative)
        {
            could_be_negative = _could_be_negative;
            InitializeComponent();
            if (hint != "")
                lHint.Text = hint;
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
            if (!could_be_negative && text.Text.Contains("-"))
            {
                BathClass.printErrorMsg("不能输入负数!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void InputServerId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else if (e.KeyCode == Keys.Back)
                btnBack_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        private void text_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void InputNumber_Load(object sender, EventArgs e)
        {
        }
    }
}
