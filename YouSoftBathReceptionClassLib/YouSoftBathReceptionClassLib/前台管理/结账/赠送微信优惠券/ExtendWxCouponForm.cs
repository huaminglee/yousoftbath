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
using YouSoftUtil;

namespace YouSoftBathReception
{
    public partial class ExtendWxCouponForm : Form
    {
        private WxUser wxUser = null;

        public ExtendWxCouponForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        private void dgv_show()
        {
            string errorDesc = "";
            var coupons = WxCouponManagement.getCoupon(LogIn.connectionIP, LogIn.options.company_Code, out errorDesc);
            if (coupons == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            foreach (var coupon in coupons)
            {
                dgv.Rows.Add(coupon.id, coupon.title, coupon.value);
            }
            dgv.CurrentCell = null;
        }

        private void BTFind_Click(object sender, EventArgs e)
        {
            SP.Panel2.Controls.Clear();
            string nickName = TextNick.Text.Trim();

            if (nickName == "")
            {
                BathClass.printErrorMsg("需要提供查询条件!");
                TextNick.Focus();
                return;
            }

            string errorDesc = "";
            var users = WxUserManagement.queryWxUser(LogIn.connectionIP, nickName, LogIn.options.company_Code, out errorDesc);
            if (users == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            int y = 0;
            foreach (var user in users)
            {
                var userCt = new UserPanel.UserPanel();
                userCt.setUserInfo(user.openid, user.headimgurl, user.nickname, "");

                userCt.UserControlBtnClicked += new UserPanel.UserPanel.CtClickHandle(UserCt_Click);
                userCt.Location = new Point(0, y);
                SP.Panel2.Controls.Add(userCt);
                userCt.FitSize(SP.Panel2.Width-5);
                y += userCt.Height / 2;
            }
        }

        private void resetBack()
        {
            foreach (var c in SP.Panel2.Controls)
            {
                var cc = c as UserPanel.UserPanel;
                cc.BackColor = Color.Transparent;
            }
        }

        private void UserCt_Click(object sender, EventArgs e)
        {
            resetBack();

            var userCt = sender as UserPanel.UserPanel;

            if (wxUser == null)
                wxUser = new WxUser();
            wxUser.openid = userCt.openId;
            userCt.BackColor = Color.LightBlue;
        }

        //绑定快捷键
        private void ExtendWxCouponForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTFind_Click(null, null);
            }
        }

        //赠送
        private void BTExtend_Click(object sender, EventArgs e)
        {
            if (wxUser == null)
            {
                BathClass.printErrorMsg("需要选择赠送对象!");
                return;
            }

            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择赠送优惠券种类!");
                return;
            }

            int couponId = MConvert<int>.ToTypeOrDefault(dgv.CurrentRow.Cells[0].Value, 0);
            string errorDesc = "";
            bool success = WxCouponManagement.extendCoupon(LogIn.connectionIP, LogIn.options.company_Code, "0", couponId, wxUser.openid, out errorDesc);
            if (!success)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            BathClass.printInformation("优惠券赠送成功!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //取消
        private void BTCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
