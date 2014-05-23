using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;
using YouSoftUtil.WX;
using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class WXCouponVerifyForm : Form
    {

        private double _couponValue;
        public double couponValue
        {
            get { return _couponValue; }
            set { _couponValue = value; }
        }

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

        //验证并消费
        private void BTVerify_Click(object sender, EventArgs e)
        {
            string code = TextCode.Text.Trim();
            if (code == "")
            {
                BathClass.printErrorMsg("需要输入优惠券代码!");
                return;
            }

            string errorDesc = "";
            var consumeWxCouponResult = WxCouponManagement.consumeCoupon(LogIn.connectionIP, LogIn.options.company_Code, code, out errorDesc);
            if (consumeWxCouponResult == null)
            {
                BathClass.printErrorMsg(errorDesc);
                TextCode.SelectAll();
                TextCode.Focus();
                return;
            }

            if (!consumeWxCouponResult.success)
            {
                BathClass.printErrorMsg(consumeWxCouponResult.errorDesc);
                TextCode.SelectAll();
                TextCode.Focus();
                return;
            }

            couponValue = consumeWxCouponResult.value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BTCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
