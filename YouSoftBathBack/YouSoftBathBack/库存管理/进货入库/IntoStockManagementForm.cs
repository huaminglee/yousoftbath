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
    public partial class IntoStockManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> stockList;

        //构造函数
        public IntoStockManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            stockList = db.Stock.Select(x => x.name).ToList();
            createTree(stockTree, stockList);

            dgv_show();
        }

        //创建树
        private void createTree(TreeView tv, List<string> nodes)
        {
            tv.Nodes.Clear();

            //catList = db.Catgory.Select(x => x.name).ToList();
            List<TreeNode> childNodes = new List<TreeNode>();
            foreach (string catName in nodes)
            {
                TreeNode node1 = new TreeNode(catName);
                node1.Name = catName;
                node1.Text = catName;
                node1.ImageIndex = 0;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有仓库", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            tv.Nodes.AddRange(new TreeNode[] { rootNode });
            tv.ExpandAll();
            tv.SelectedNode = rootNode;
        }

        //显示清单
        public void dgv_show()
        {
            string stockSel = stockTree.SelectedNode.Text;

            if (stockSel == "所有仓库")
            {
                dgv.DataSource = from x in db.StockIn
                                 orderby x.id
                                 select new
                                 {
                                     单号 = x.id,
                                     名称 = x.name,
                                     单位 = x.unit,
                                     供应商 = db.Provider.FirstOrDefault(y => y.id == x.providerId).name,
                                     进价 = x.cost,
                                     数量 = x.amount,
                                     仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
                                     日期 = x.date,
                                     经手人 = x.transactor,
                                     审核 = x.checker,
                                     备注 = x.note
                                 };
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                dgv.DataSource = from x in db.StockIn
                                 where x.stockId == selId
                                 orderby x.id
                                 select new
                                 {
                                     单号 = x.id,
                                     名称 = x.name,
                                     单位 = x.unit,
                                     进价 = x.cost,
                                     数量 = x.amount,
                                     仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
                                     日期 = x.date,
                                     经手人 = x.transactor,
                                     审核 = x.checker,
                                     备注 = x.note
                                 };
            }

            BathClass.set_dgv_fit(dgv);
        }

        //导出清单
        private void exportGoods_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "进货单统计", false, "");
        }

        //离开
        private void exitTool_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //选择节点
        private void catTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv_show();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F3:
                    toolIntoStock_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "进货单统计", false, "");
                    break;
                default:
                    break;
            }
        }

        //录入进货单
        private void toolIntoStock_Click(object sender, EventArgs e)
        {
            StockInForm inStockForm = new StockInForm(db, this);
            if (inStockForm.ShowDialog() == DialogResult.OK)
                dgv_show();
        }
    }
}
