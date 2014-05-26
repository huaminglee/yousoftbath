using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftUtil.WX;
using YouSoftUtil.Shop;
using YouSoftUtil;
using YouSoftBathConstants;

namespace IntereekBathWeChat
{
    public partial class FeedBackForm : Form
    {
        private List<string> companies = new List<string>();

        public FeedBackForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var code_str = IOUtil.get_config_by_key(ConfigKeys.KEY_COMPANY_CODE);
            companies = code_str.Split(Constants.BIG_SPLITCHAR).ToList();
            companies.RemoveAll(x => x.Trim() == "");
            ComboShopName.Items.AddRange(companies.Select(x=>x.Split(Constants.SplitChar)[1]).ToArray());
            ComboShopName.SelectedIndex = 0;
            dgv_show();
        }

        private void dgv_show()
        {
            dgv.Rows.Clear();
            var companyCode = companies[ComboShopName.SelectedIndex].Split(Constants.SplitChar)[0];
            if (companyCode == "")
            {
                MessageBox.Show("未定义连锁店铺，请先到连锁店铺定义！");
                return;
            }

            string errorDesc = "";
            var comments = ShopManagement.queryCommentByCompany(Constants.AliIP, companyCode, out errorDesc);
            if (comments == null)
            {
                MessageBox.Show(errorDesc);
                return;
            }

            int i = 1;
            foreach (var comment in comments)
            {
                dgv.Rows.Add(i, comment.content, PCUtil.converJavaTimeToNetTime(comment.createTime).ToString("MM-dd HH:mm"));
                i++;
            }
        }

        private void BTFind_Click(object sender, EventArgs e)
        {
            dgv_show();
        }
    }
}
