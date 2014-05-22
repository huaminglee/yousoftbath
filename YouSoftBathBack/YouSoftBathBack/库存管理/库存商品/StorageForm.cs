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
    public partial class StorageForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> goodsCatList;

        //构造函数
        public StorageForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            goodsCatList = db.GoodsCat.Select(x => x.name).ToList();
            createTree(stockTree, goodsCatList);
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

            TreeNode rootNode = new TreeNode("所有类别", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            tv.Nodes.AddRange(new TreeNode[] { rootNode });
            tv.ExpandAll();
            tv.SelectedNode = rootNode;
        }

        //显示清单
        public void dgv_show()
        {
            string goodsCatSel = stockTree.SelectedNode.Text;
            dgv.Rows.Clear();

            var goods = db.StorageList.AsQueryable();
            if (goodsCatSel != "所有类别")
            {
                int selId = db.GoodsCat.FirstOrDefault(x => x.name == goodsCatSel).id;
                goods = goods.Where(x => x.goodsCatId == selId);
            }
            string goods_name_sel = TextName.Text.Trim();
            if (goods_name_sel != "")
                goods = goods.Where(x => x.name.Contains(goods_name_sel));


            var mainStock = db.Stock.FirstOrDefault(x => x.main != null && x.main.Value);
            var goodsNames = goods.Select(x => x.name);
            foreach (var name in goodsNames)
            {
                double number_Ins = 0;
                double number_Outs = 0;
                double number_OrderOuts = 0;
                double number_pans = 0;

                var name_stockIns = db.StockIn.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null);
                if (name_stockIns.Any())
                    number_Ins = name_stockIns.Sum(x => x.amount).Value;

                var name_stockOuts = db.StockOut.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null);
                if (name_stockOuts.Any())
                    number_Outs = MConvert<double>.ToTypeOrDefault(name_stockOuts.Sum(x => x.amount), 0);

                var name_orderStockOuts = db.OrderStockOut.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null);
                if (name_orderStockOuts.Any())
                    number_OrderOuts = MConvert<double>.ToTypeOrDefault(name_orderStockOuts.Sum(x => x.amount), 0);

                var name_pans = db.Pan.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null);
                if (name_pans.Any())
                    number_pans = MConvert<double>.ToTypeOrDefault(name_pans.Sum(x => x.amount), 0);

                var storageList = db.StorageList.FirstOrDefault(x => x.name == name);
                var goods_cat = db.GoodsCat.FirstOrDefault(x => x.id == storageList.goodsCatId);
                if (goods_cat == null)
                    continue;

                var unit_stock = db.StockIn.FirstOrDefault(x => x.name == name);
                string unit_name = "";
                if (unit_stock != null) 
                    unit_name = unit_stock.unit;

                dgv.Rows.Add(name, number_Ins + number_pans - number_Outs - number_OrderOuts,
                    storageList.minAmount, goods_cat.name, unit_name, storageList.note);
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
            PrintDGV.Print_DataGridView(dgv, "现有库存", false, "");
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
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "现有库存", false, "");
                    break;
                default:
                    break;
            }
        }

        //增加库存商品类别
        private void addSeatType_Click(object sender, EventArgs e)
        {
            var form = new GoodsCatForm(db, null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                goodsCatList = db.GoodsCat.Select(x => x.name).ToList();
                createTree(stockTree, goodsCatList);
            }
        }

        //删除库存商品类别
        private void delSeatType_Click(object sender, EventArgs e)
        {
            if (stockTree.SelectedNode == null ||
                db.GoodsCat.FirstOrDefault(x => x.name == stockTree.SelectedNode.Text) == null)
            {
                GeneralClass.printWarningMsg("没有选择类别!");
                return;
            }

            var goodsCat = db.GoodsCat.FirstOrDefault(x => x.name == stockTree.SelectedNode.Text);
            if (db.StorageList.Any(x=>x.goodsCatId==goodsCat.id))
            {
                BathClass.printErrorMsg("所选类别包含商品，不能删除!");
                return;
            }
            db.GoodsCat.DeleteOnSubmit(goodsCat);
            db.SubmitChanges();

            goodsCatList = db.GoodsCat.Select(x => x.name).ToList();
            createTree(stockTree, goodsCatList);
        }

        //编辑库存商品类别
        private void editSeatType_Click(object sender, EventArgs e)
        {
            if (stockTree.SelectedNode == null ||
                db.GoodsCat.FirstOrDefault(x => x.name == stockTree.SelectedNode.Text) == null)
            {
                GeneralClass.printWarningMsg("没有选择类别!");
                return;
            }

            var goodsCat = db.GoodsCat.FirstOrDefault(x => x.name == stockTree.SelectedNode.Text);
            var form = new GoodsCatForm(db, goodsCat);
            if (form.ShowDialog() == DialogResult.OK)
            {
                goodsCatList = db.GoodsCat.Select(x => x.name).ToList();
                createTree(stockTree, goodsCatList);
            }
        }

        //新增商品
        private void addGoods_Click(object sender, EventArgs e)
        {
            var form = new GoodsForm(db, null, this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgv_show();
            }
        }

        //删除商品
        private void delGoods_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell==null)
            {
                BathClass.printErrorMsg("未选择商品!");
                return;
            }

            if (BathClass.printAskMsg("确认删除商品?") != DialogResult.Yes)
                return;

            var goods = db.StorageList.FirstOrDefault(x => x.name == dgv.CurrentRow.Cells[0].Value.ToString());
            db.StorageList.DeleteOnSubmit(goods);
            db.SubmitChanges();
            dgv_show();
        }

        //编辑商品
        private void editGoods_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选择商品!");
                return;
            }

            var goods = db.StorageList.FirstOrDefault(x => x.name == dgv.CurrentRow.Cells[0].Value.ToString());
            var form = new GoodsForm(db, goods, this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgv_show();
            }
        }

        //查找
        private void BtnFind_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        private void TextName_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
