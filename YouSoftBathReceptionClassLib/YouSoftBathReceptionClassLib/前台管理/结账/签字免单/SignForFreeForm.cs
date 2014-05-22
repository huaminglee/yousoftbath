using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;

namespace YouSoftBathReception
{
    public partial class SignForFreeForm : Form
    {
        //成员变量
        public string signature
        {
            get
            {
                return name.Text;
            }
        }

        //构造函数
        public SignForFreeForm()
        {
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

            this.DialogResult = DialogResult.OK;
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
