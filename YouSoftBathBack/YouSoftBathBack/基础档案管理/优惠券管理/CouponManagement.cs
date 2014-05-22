using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class CouponManagement : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public CouponManagement()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CouponManagement_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        //显示清单
        private void dgv_show()
        {
            dgv.DataSource = from x in db.Coupon
                             orderby x.id
                             select new
                             {
                                 编号 = x.pkey,
                                 券号码 = x.id,
                                 名称 = x.name,
                                 抵用金额 = x.money,
                                 抵用项目 = db.Menu.FirstOrDefault(y => y.id == x.menuId) == null ? "" : db.Menu.FirstOrDefault(y => y.id == x.menuId).name,
                                 发行日期 = x.issueDate,
                                 过期日期 = x.expireDate,
                                 发行人 = x.issueTransator,
                                 备注 = x.note,
                                 最低赠送金额 = x.minAmount
                             };
            BathClass.set_dgv_fit(dgv);
        }

        //新增
        private void addTool_Click(object sender, EventArgs e)
        {
            CouponForm addCoupon = new CouponForm(db, null);
            if (addCoupon.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //删除
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            db.Coupon.DeleteOnSubmit(db.Coupon.FirstOrDefault(s => s.pkey == selId));
            db.SubmitChanges();
            dgv_show();
        }

        //编辑
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var coupon = db.Coupon.FirstOrDefault(x => x.pkey == selId);

            CouponForm editCoupon = new CouponForm(db, coupon);
            if (editCoupon.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "优惠券管理", false, "");
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F1:
                    addTool_Click(null, null);
                    break;
                case Keys.F2:
                    delTool_Click(null, null);
                    break;
                case Keys.F3:
                    editTool_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "优惠券管理", false, "");
                    break;
                default:
                    break;
            }
        }
    }
}
