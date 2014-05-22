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
    public partial class DianLunForm : Form
    {
        //成员变量
        private string _tech_type = string.Empty;

        public string tech_type
        {
            get { return _tech_type; }
        }

        //构造函数
        public DianLunForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void TransferSelectForm_Load(object sender, EventArgs e)
        {
            btnUndoDiscount.Text = "点钟\n(2)";
            btnDiscount.Text = "轮钟\n(1)";
            btnCancel.Text = "退出\n(Esc)";
        }

        private void TransferSelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad1)
                btnDiscount_Click(null, null);
            else if (e.KeyCode == Keys.NumPad2)
                btnUndoDiscount_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        //轮钟
        private void btnDiscount_Click(object sender, EventArgs e)
        {
            _tech_type = "轮钟";
            this.DialogResult = DialogResult.OK;
        }

        //点钟
        private void btnUndoDiscount_Click(object sender, EventArgs e)
        {
            _tech_type = "点钟";
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
