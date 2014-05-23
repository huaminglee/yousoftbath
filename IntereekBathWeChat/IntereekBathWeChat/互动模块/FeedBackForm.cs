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
using YouSoftUtil.Shop;
using YouSoftBathFormClass;
using YouSoftUtil;

namespace IntereekBathWeChat
{
    public partial class FeedBackForm : Form
    {

        public FeedBackForm()
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
            var comments = ShopManagement.queryCommentByCompany(LogIn.connectionIP, LogIn.options.company_Code, out errorDesc);
            if (comments == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            int i = 1;
            foreach (var comment in comments)
            {
                dgv.Rows.Add(i, comment.content, PCUtil.converJavaTimeToNetTime(comment.createTime).ToString("MM-dd HH:mm"));
                i++;
            }
        }
    }
}
