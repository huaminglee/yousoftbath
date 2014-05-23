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
    public partial class CompanyCouponForm : Form
    {

        public CompanyCouponForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        public void dgv_show()
        {
            dgv.Rows.Clear();

            string errorDesc = "";
            var coupons = WxCouponManagement.queryCouponByCompany(LogIn.connectionIP, LogIn.options.company_Code, out errorDesc);
            if (coupons == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            int i = 1;
            foreach (var coupon in coupons)
            {
                string type = coupon.isConsume == "y" ? "已用" : "未用";
                dgv.Rows.Add(i, coupon.id, coupon.title, type, coupon.count);
                i++;
            }

            dgv.CurrentCell = null;
        }


        //导出
        private void ToolExport_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void ToolPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv);
        }

        //退出
        private void ToolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
