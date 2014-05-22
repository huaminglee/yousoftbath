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
    public partial class CardPopSaleForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public CardPopSaleForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
            dgv_show();
        }

        //对话框载入
        private void PromotionForm_Load(object sender, EventArgs e)
        {

        }

        //显示清单
        private void dgv_show()
        {
            dgv.DataSource = from x in db.CardPopSale
                                    orderby x.id
                                    select new
                                    {
                                        方案编号 = x.id,
                                        充值金额 = x.mimMoney,
                                        赠送金额 = x.saleMoney
                                    };
        }

        //添加
        private void toolAdd_Click(object sender, EventArgs e)
        {
            CardPopForm promotionForm = new CardPopForm(db, null);
            if (promotionForm.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //删除
        private void toolDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printWarningMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            int delId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            db.CardPopSale.DeleteOnSubmit(db.CardPopSale.FirstOrDefault(s => s.id == delId));
            db.SubmitChanges();
            dgv_show();
        }

        //修改
        private void toolEdit_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printWarningMsg("没有选择行!");
                return;
            }

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var promotion = db.CardPopSale.FirstOrDefault(x => x.id == selId);

            CardPopForm editPromotion = new CardPopForm(db, promotion);
            if (editPromotion.ShowDialog() == DialogResult.OK)
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
            PrintDGV.Print_DataGridView(dgv, "优惠方案报表", false, "");
        }

        //退出
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
                    toolAdd_Click(null, null);
                    break;
                case Keys.F2:
                    toolDel_Click(null, null);
                    break;
                case Keys.F3:
                    toolEdit_Click(null, null);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "优惠方案报表", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }
    }
}
