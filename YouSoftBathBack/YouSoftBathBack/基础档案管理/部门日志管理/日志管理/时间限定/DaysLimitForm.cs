using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class DaysLimitForm : Form
    {
        //成员变量
        public DateTime dt
        {
            get { return DTPLimit.Value; }
        }

        //构造函数
        public DaysLimitForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            DTPLimit.Value = DateTime.Now.AddDays(1);
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

    }
}
