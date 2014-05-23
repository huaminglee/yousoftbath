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
    public partial class CouponRecordsForm : Form
    {

        public CouponRecordsForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DPStart.Value = DateTime.Now.AddDays(-7);//默认查询一个星期的优惠券使用记录
            dgv_show();
        }

        public void dgv_show()
        {
            dgv.Rows.Clear();

            string errorDesc = "";
            var coupons = WxCouponManagement.queryCouponRecords(LogIn.connectionIP, LogIn.options.company_Code, null,
                DPStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), DPEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"), out errorDesc);
            if (coupons == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            int i = 1;
            foreach (var coupon in coupons)
            {
                dgv.Rows.Add(i, coupon.id, coupon.title, coupon.value, coupon.nickName, PCUtil.converJavaTimeToNetTime(coupon.consumeTime));
                i++;
            }

            BathClass.set_dgv_fit(dgv);
            dgv.CurrentCell = null;
        }

        private void BTFind_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        //导出
        private void BTExport_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void BTPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv);
        }

        //退出
        private void BTExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
