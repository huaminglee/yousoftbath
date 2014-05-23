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
    public partial class CouponManagementForm : Form
    {

        public CouponManagementForm()
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
            var coupons = WxCouponManagement.getCoupon(LogIn.connectionIP, LogIn.options.company_Code, out errorDesc);
            if (coupons == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            foreach (var coupon in coupons)
            {
                dgv.Rows.Add(coupon.id, coupon.title, coupon.value, coupon.descp, coupon.isDeleted=="y");
            }

            dgv.CurrentCell = null;
        }

        //新增
        private void ToolAdd_Click(object sender, EventArgs e)
        {
            var form = new CouponForm(null, this);
            form.ShowDialog();
        }

        //删除
        private void ToolDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要先选择优惠券所在行!");
                return;
            }
            if (BathClass.printAskMsg("确认删除优惠券?") != DialogResult.Yes)
                return;

            string errorDesc = "";
            string isDeleted = (dgv.CurrentRow.Cells[4].EditedFormattedValue.ToString() == "False" ? "y" : "n");
            int id = MConvert<int>.ToTypeOrDefault(dgv.CurrentRow.Cells[0].Value, 0);
            double value = MConvert<double>.ToTypeOrDefault(dgv.CurrentRow.Cells[2].Value, 0);
            bool success = WxCouponManagement.uploadCoupon(LogIn.connectionIP, id, LogIn.options.company_Code,
                value, "", "", isDeleted, out errorDesc);
            if (!success)
            {
                BathClass.printErrorMsg("删除失败，原因：" + errorDesc);
            }
            dgv_show();
        }

        //编辑
        private void ToolEdit_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要先选择优惠券所在行!");
                return;
            }

            WxCoupon wxCoupon = new WxCoupon();
            wxCoupon.id = MConvert<int>.ToTypeOrDefault(dgv.CurrentRow.Cells[0].Value, 0);
            wxCoupon.title = dgv.CurrentRow.Cells[1].Value.ToString();
            wxCoupon.value = MConvert<double>.ToTypeOrDefault(dgv.CurrentRow.Cells[2].Value, 0);
            wxCoupon.descp = dgv.CurrentRow.Cells[3].Value.ToString();
            var form = new CouponForm(wxCoupon, this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgv_show();
            }
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

        //判断启用停用
        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv.CurrentCell == null)
                return;

            if (dgv.CurrentRow.Cells[4].EditedFormattedValue.ToString() == "False")
                ToolDel.Text = "停用";
            else
                ToolDel.Text = "启用";
        }
    }
}
