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
    public partial class WXCouponVerifyForm : Form
    {
        //构造函数
        public WXCouponVerifyForm()
        {
            InitializeComponent();
        }

        private void TextCode_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void BTScan_Click(object sender, EventArgs e)
        {

        }

        private void BTVerify_Click(object sender, EventArgs e)
        {

        }

        private void BTCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
