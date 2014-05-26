using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IntereekBathWeChat
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        #region 互动模块

        //查询反馈
        private void BTFeedBack_Click(object sender, EventArgs e)
        {
            var form = new FeedBackForm();
            form.ShowDialog();
        }

        //连锁店铺
        private void BTChainStore_Click(object sender, EventArgs e)
        {
            var form = new ChainForm();
            form.ShowDialog();
        }

        //业绩查询
        private void BTYeJi_Click(object sender, EventArgs e)
        {
            var form = new YeJiForm();
            form.ShowDialog();
        }

        #endregion

    }
}
