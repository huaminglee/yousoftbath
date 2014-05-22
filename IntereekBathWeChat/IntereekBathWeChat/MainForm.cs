using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;

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

        #region 店铺管理

        private void BTFans_Click(object sender, EventArgs e)
        {
            var form = new FansForm();
            form.ShowDialog();
        }

        private void BTShop_Click(object sender, EventArgs e)
        {
            var form = new ShopForm();
            form.ShowDialog();
        }

        private void BTMenu_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("确定同步项目数据?") != DialogResult.Yes)
                return;
        }


        private void BTCombo_Click(object sender, EventArgs e)
        {

        }

        private void BTPromotion_Click(object sender, EventArgs e)
        {

        }

        private void BTTechs_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 优惠券管理

        private void BTCoupons_Click(object sender, EventArgs e)
        {

        }

        private void BTCouponMang_Click(object sender, EventArgs e)
        {

        }

        private void BTCouponUseRecord_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 互动模块

        //查询反馈
        private void BTFeedBack_Click(object sender, EventArgs e)
        {

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
