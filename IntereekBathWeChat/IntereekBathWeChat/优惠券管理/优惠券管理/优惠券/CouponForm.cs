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
using YouSoftUtil.WX;
using YouSoftUtil.Shop;

namespace IntereekBathWeChat
{
    public partial class CouponForm : Form
    {
        private List<string> companyCodes = new List<string>();
        private WxCoupon wxCoupon = null;
        private CouponManagementForm form;

        public CouponForm(WxCoupon wxCoupon, CouponManagementForm form)
        {
            InitializeComponent();
            this.form = form;
            if (wxCoupon != null)
            {
                this.wxCoupon = wxCoupon;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (wxCoupon != null)
            {
                TextTitle.Text = wxCoupon.title;
                TextValue.Text = wxCoupon.value.ToString();
                TextDescp.Text = wxCoupon.descp;
            }
        }

        private void BTCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BTOk_Click(object sender, EventArgs e)
        {
            string value = TextValue.Text.Trim();
            if (value == "")
            {
                BathClass.printErrorMsg("需要输入金额");
                TextValue.SelectAll();
                return;
            }

            string title = TextTitle.Text.Trim();
            if (title == "")
            {
                BathClass.printErrorMsg("需要输入优惠券名称!");
                return;
            }

            string errorDesc = "";
            
            if (wxCoupon == null)
            {
                bool success = WxCouponManagement.uploadCoupon(LogIn.connectionIP, 0, LogIn.options.company_Code,
                    MConvert<double>.ToTypeOrDefault(value, 0), TextDescp.Text, title, "n", out errorDesc);
                if (!success)
                {
                    BathClass.printErrorMsg("上传失败，原因：" + errorDesc);
                    return;
                }

                this.form.dgv_show();
                TextTitle.Text = "";
                TextValue.Text = "";
                TextDescp.Text = "";
                TextTitle.Focus();
            }
            else
            {
                bool success = WxCouponManagement.uploadCoupon(LogIn.connectionIP, wxCoupon.id, LogIn.options.company_Code,
                    MConvert<double>.ToTypeOrDefault(value, 0), TextDescp.Text, title, "n", out errorDesc);
                if (!success)
                {
                    BathClass.printErrorMsg("上传失败，原因：" + errorDesc);
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }


    }
}
