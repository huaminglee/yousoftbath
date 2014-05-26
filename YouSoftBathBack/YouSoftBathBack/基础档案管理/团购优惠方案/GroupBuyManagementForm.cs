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
    public partial class GroupBuyManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private GroupBuyPromotion m_Promotion = null;
        private Dictionary<string, string> m_Offer = new Dictionary<string, string>();

        //构造函数
        public GroupBuyManagementForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void PromotionForm_Load(object sender, EventArgs e)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_Promotion = db.GroupBuyPromotion.FirstOrDefault();
            if (m_Promotion == null)
            {
                m_Promotion = new GroupBuyPromotion();
                db.GroupBuyPromotion.InsertOnSubmit(m_Promotion);
                db.SubmitChanges();
            }

            m_Offer = BathClass.disAssemble(db, m_Promotion);
            dgvDetails_show();
        }


        //显示详细信息
        private void dgvDetails_show()
        {
            dgvDetails.Rows.Clear();
            foreach (string menuId in m_Offer.Keys)
            {
                string[] row = new string[3];
                var menu = db.Menu.FirstOrDefault(x => x.id.ToString() == menuId);
                if (menu != null)
                {
                    row[0] = menu.name;
                    row[1] = menu.price.ToString();
                    row[2] = m_Offer[menuId].ToString();
                }
                else
                {
                    row[0] = menuId;
                    row[2] = m_Offer[menuId].ToString();
                }

                dgvDetails.Rows.Add(row);
            }
        }

        //添加
        private void toolAdd_Click(object sender, EventArgs e)
        {
            GroupBuyItemAddForm f = new GroupBuyItemAddForm(db, m_Promotion);
            if (f.ShowDialog() == DialogResult.OK)
            {
                m_Offer.Add(f.m_Offer, f.m_discoutType + "#" + f.m_discout.ToString());
                m_Promotion.menuIds = BathClass.assemble(m_Offer);
                db.SubmitChanges();
                dgvDetails_show();
            }
        }

        //删除
        private void toolDel_Click(object sender, EventArgs e)
        {
            if (dgvDetails.CurrentCell == null)
                return;

            if (BathClass.printAskMsg("确定删除?") != DialogResult.Yes) return;

            string offer = dgvDetails.Rows[dgvDetails.CurrentCell.RowIndex].Cells[0].Value.ToString();

            var m = db.Menu.FirstOrDefault(x => x.name == offer);
            if (m != null)
                m_Offer.Remove(m.id.ToString());
            else
                m_Offer.Remove(offer);

            m_Promotion.menuIds = BathClass.assemble(m_Offer);
            db.SubmitChanges();
            dgvDetails_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgvDetails);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgvDetails, "团购优惠方案报表", false, "");
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
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgvDetails, "优惠方案报表", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgvDetails);
                    break;
                default:
                    break;
            }
        }

    }
}
