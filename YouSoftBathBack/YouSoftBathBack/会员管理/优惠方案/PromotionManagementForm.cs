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
    public partial class PromotionManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public PromotionManagementForm()
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
            dgv.DataSource = from x in db.Promotion
                                    orderby x.id
                                    select new
                                    {
                                        方案编号 = x.id,
                                        方案名称 = x.name
                                    };
        }

        //显示详细信息
        private void dgvDetails_show()
        {
            dgvDetails.Rows.Clear();
            if (dgv.CurrentCell == null)
                return;

            string selId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();
            Promotion promotion = db.Promotion.FirstOrDefault(x => x.id.ToString() == selId);
            Dictionary<string, string> menuDict = BathClass.disAssemble(db, promotion);
            foreach (string menuId in menuDict.Keys)
            {
                string[] row = new string[3];
                var menu = db.Menu.FirstOrDefault(x => x.id.ToString() == menuId);
                if (menu != null)
                {
                    row[0] = menu.name;
                    row[1] = menu.price.ToString();
                    row[2] = menuDict[menuId].ToString();
                }
                else
                {
                    row[0] = menuId;
                    row[2] = menuDict[menuId].ToString();
                }

                dgvDetails.Rows.Add(row);
            }
        }

        //添加
        private void toolAdd_Click(object sender, EventArgs e)
        {
            PromotionForm promotionForm = new PromotionForm(db, null);
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
            db.Promotion.DeleteOnSubmit(db.Promotion.FirstOrDefault(s => s.id == delId));
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
            var promotion = db.Promotion.FirstOrDefault(x => x.id == selId);

            PromotionForm editPromotion = new PromotionForm(db, promotion);
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

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            dgvDetails_show();
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
