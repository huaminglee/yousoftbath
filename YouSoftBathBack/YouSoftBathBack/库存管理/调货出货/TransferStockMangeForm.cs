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
    public partial class TransferStockMangeForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> stockList;

        //构造函数
        public TransferStockMangeForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            if (db.StockOut.Any())
            {
                st.Value = db.StockOut.Min(x => x.date).Value;
                et.Value = db.StockOut.Max(x => x.date).Value;
            }
            
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
        private void dgv_show()
        {
            string stockSel = stockTree.SelectedNode.Text;
            DateTime dt_st = DateTime.Parse(st.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime dt_et = DateTime.Parse(et.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59");

            if (stockSel == "所有仓库")
            {
                var sos = db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et);
                dgv.DataSource = from x in sos
                                 orderby x.id
                                 select new
                                 {
                                     单号 = x.id,
                                     名称 = x.name,
                                     数量 = x.amount,
                                     单位 = x.unit,
                                     仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
                                     日期 = x.date,
                                     领料人 = x.receiver,
                                     经手人 = x.transactor,
                                     审核 = x.checker,
                                     备注 = x.note
                                 };
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                var sos = db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et);
                dgv.DataSource = from x in sos
                                 where x.stockId == selId
                                 orderby x.id
                                 select new
                                 {
                                     单号 = x.id,
                                     名称 = x.name,
                                     数量 = x.amount,
                                     单位 = x.unit,
                                     仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
                                     日期 = x.date,
                                     领料人 = x.receiver,
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
            PrintDGV.Print_DataGridView(dgv, "调货出货统计", false, "");
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
                    PrintDGV.Print_DataGridView(dgv, "调货出货统计", false, "");
                    break;
                default:
                    break;
            }
        }

        //录入出货单
        private void toolIntoStock_Click(object sender, EventArgs e)
        {
            StockOutForm inStockForm = new StockOutForm(db);
            if (inStockForm.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //查询
        private void btnFind_Click(object sender, EventArgs e)
        {

        }
    }
}
