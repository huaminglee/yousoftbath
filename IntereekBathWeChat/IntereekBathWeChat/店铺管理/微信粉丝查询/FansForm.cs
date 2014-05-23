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

namespace IntereekBathWeChat
{
    public partial class FansForm : Form
    {

        public FansForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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
            var users = WxUserManagement.queryWxUser(Constants.AliIP, nickName, "057189283688", out errorDesc);
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
        }

        private void FansForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BTFind_Click(null, null);
            }
        }

    }
}
