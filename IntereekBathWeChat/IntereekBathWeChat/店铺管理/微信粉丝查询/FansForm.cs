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
using YouSoftUtil;
using YouSoftBathFormClass;

namespace IntereekBathWeChat
{
    public partial class FansForm : Form
    {

        private string openId;

        public FansForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DPStart.Value = DateTime.Now.AddDays(-7);
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
            var users = WxUserManagement.queryWxUser(LogIn.connectionIP, nickName, "057189283688", out errorDesc);
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
            userCt.BackColor = Color.LightBlue;

            openId = userCt.openId;
            dgvUnUsed_show();
            dgvUsed_show();
            
        }

        private void dgvUnUsed_show()
        {
            string errorDesc = "";
            var coupons = WxCouponManagement.queryCouponByUser(LogIn.connectionIP, LogIn.options.company_Code, openId, out errorDesc);
            if (coupons == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }
            dgvUnUsed.Rows.Clear();

            int i = 1;
            foreach (var coupon in coupons.unUseList)
            {
                dgvUnUsed.Rows.Add(i, coupon.id, coupon.title, coupon.count);
                i++;
            }
            BathClass.set_dgv_fit(dgvUnUsed);
        }

        private void dgvUsed_show()
        {
            dgvUsed.Rows.Clear();

            string errorDesc = "";
            var coupons = WxCouponManagement.queryCouponRecords(LogIn.connectionIP, LogIn.options.company_Code, openId,
                DPStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), DPEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"), out errorDesc);
            if (coupons == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            int i = 1;
            foreach (var coupon in coupons)
            {
                dgvUsed.Rows.Add(i, coupon.id, coupon.title, coupon.value, PCUtil.converJavaTimeToNetTime(coupon.consumeTime));
                i++;
            }

            BathClass.set_dgv_fit(dgvUsed);
            dgvUsed.CurrentCell = null;
        }

        private void FansForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTFind_Click(null, null);
            }
        }

        //查询使用记录
        private void BTSearch_Click(object sender, EventArgs e)
        {
            dgvUsed_show();
        }

    }
}
