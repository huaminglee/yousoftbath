using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class StorageForm : Form
    {
        //成员变量
        //private BathDBDataContext db = null;
        private List<string> goodsCatList;
        private Thread m_Thread;
        private string goodsCatSel="";
        private string goods_name_sel="";
        
        private delegate void delegate_add_row(object[] vals);
        
        private struct stockInfo 
        {
            public double stockleft;
            public double unitPrice;
            public double totalMoney;
            public stockInfo (double x,double y)
            {
                this.stockleft = x;
                this.unitPrice = y;
                this.totalMoney = x*y;
            }

        }

        //构造函数
        public StorageForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            //comboxName.Items.Clear();
            var db = new BathDBDataContext(LogIn.connectionString);
            goodsCatList = db.GoodsCat.Select(x => x.name).ToList();
            createTree(stockTree, goodsCatList);
            comboxName.Items.AddRange(db.StorageList.Select(x => x.name).Distinct().ToArray());
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
            goodsCatSel = rootNode.Text;
        }

        private void add_row(object[] vals)
        {
            dgv.Rows.Add(vals);
        }
        public void dgv_show()
        {
            goodsCatSel = stockTree.SelectedNode.Text;
            goods_name_sel = comboxName.Text;
            dgv.Rows.Clear();


            if (m_Thread != null && m_Thread.IsAlive)
                m_Thread.Abort();
            
            m_Thread=new Thread(new ThreadStart(do_dgv_Show));
            m_Thread.Start();
            //BathClass.set_dgv_fit(dgv);
        }

        private void do_dgv_Show()
        {
            try
            {
                DateTime dt_et = DateTime.Parse(et.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                DateTime dt_st_thisMonth = DateTime.Parse(dt_et.ToString("yyyy-MM-")+"01 00:00:00");
                DateTime dt_et_lastMonth = DateTime.Parse(dt_et.AddDays(-dt_et.Day).ToString("yyyy-MM-dd ") + "23:59:59");
                DateTime dt_st_lastMonth = DateTime.Parse("2013-01-01 00:00:01");
                double totalMoneyLastMonth = 0;
                double totalMoneyOutThisMonth = 0;
                double totalMoneyLeftThisMonth = 0;  
 
                var db = new BathDBDataContext(LogIn.connectionString);
                var goods = db.StorageList.AsQueryable();
                if (goodsCatSel != "所有类别")
                {
                    int selId = db.GoodsCat.FirstOrDefault(x => x.name == goodsCatSel).id;
                    goods = goods.Where(x => x.goodsCatId == selId);

                }
                if (goods_name_sel != "")
                    goods = goods.Where(x => x.name.Contains(goods_name_sel));
                var mainStock = db.Stock.FirstOrDefault(x => x.main != null && x.main.Value);
                var goodsNames = goods.Select(x => x.name);
                if (goodsNames == null)
                    return;
                foreach (var name in goodsNames)
                {
                    object[] dgvRow = new object[14];

                    var storageList = db.StorageList.FirstOrDefault(x => x.name == name);
                    var goods_cat = db.GoodsCat.FirstOrDefault(x => x.id == storageList.goodsCatId);
                    if (goods_cat == null)
                        continue;

                    var unit_stock = db.StockIn.FirstOrDefault(x => x.name == name);
                    string unit_name = "";
                    if (unit_stock != null) 
                        unit_name = unit_stock.unit;
                    
                    dgvRow[0] = name;
                    dgvRow[1] = goods_cat.name;
                    dgvRow[2] = unit_name;
                    dgvRow[4] = getStockByName(name,dt_st_lastMonth,dt_et,db).unitPrice;
                    dgvRow[3] = getStockByName(name,dt_st_lastMonth,dt_et_lastMonth,db).stockleft;
                    dgvRow[5] = getStockByName(name, dt_st_lastMonth, dt_et_lastMonth, db).totalMoney;
                    totalMoneyLastMonth += MConvert<double>.ToTypeOrDefault(dgvRow[5], 0);
                    dgvRow[6] = MConvert<double>.ToTypeOrDefault(db.StockOut.Where(x=>x.name==name).Where(x => x.date >= dt_st_thisMonth && x.date < dt_et).Sum(x => x.amount), 0);
                    dgvRow[7] = getStockByName(name, dt_st_lastMonth, dt_et, db).unitPrice;
                    dgvRow[8] = MConvert<double>.ToTypeOrDefault(dgvRow[6], 0) * MConvert<double>.ToTypeOrDefault(dgvRow[7], 0);
                    totalMoneyOutThisMonth += MConvert<double>.ToTypeOrDefault(dgvRow[8], 0);
                    dgvRow[9] = getStockByName(name,dt_st_lastMonth,dt_et,db).stockleft;
                    dgvRow[10] = getStockByName(name, dt_st_lastMonth, dt_et, db).unitPrice;
                    dgvRow[11] = getStockByName(name, dt_st_lastMonth, dt_et, db).totalMoney;
                    totalMoneyLeftThisMonth += (double)dgvRow[11];
                    dgvRow[12] = storageList.minAmount;
                    dgvRow[13] = storageList.note;                 

                    this.Invoke(new delegate_add_row(add_row), (object)dgvRow);

                }

                string[] sbutotalrow1 = { "" };
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow1);
                string[] sbutotalrow2 = { "汇总信息", "", "", "", "", "总金额", "", "", "总金额", "", "", "总金额" };
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow2);
                string[] sbutotalrow3 = { "", "", "", "", "", totalMoneyLastMonth.ToString(),"","",totalMoneyOutThisMonth.ToString(),"","",totalMoneyLeftThisMonth.ToString()};
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow3);
            }
            catch (Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
            }        
           
     
        }
        private stockInfo getStockByName(string name, DateTime  dt_st,   DateTime dt_et, BathDBDataContext db)
        {
            stockInfo stockinfo=new stockInfo(0,0);
            double number_Ins = 0;
            double money_Ins=0;
            double number_Outs = 0;
            double number_OrderOuts = 0;
            double number_pans = 0;
            double number_Total = 0;
            double unitPrice=0;
            var mainStock = db.Stock.FirstOrDefault(x => x.main != null && x.main.Value);

            var name_stockIns = db.StockIn.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null).Where(x => x.date >= dt_st && x.date <= dt_et);
            if (name_stockIns.Any())
            {
                number_Ins = name_stockIns.Sum(x => x.amount).Value;
                money_Ins=name_stockIns.Sum(x=>x.money).Value;
                if (number_Ins != 0)
                    unitPrice = Math.Round(money_Ins / number_Ins, 2);
            }

            var name_stockOuts = db.StockOut.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null).Where(x => x.date <= dt_et&&x.date>=dt_st);
            if (name_stockOuts.Any())
                number_Outs = MConvert<double>.ToTypeOrDefault(name_stockOuts.Sum(x => x.amount), 0);

            var name_orderStockOuts = db.OrderStockOut.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null).Where(x => x.date <= dt_et&&x.date>=dt_st);
            if (name_orderStockOuts.Any())
                number_OrderOuts = MConvert<double>.ToTypeOrDefault(name_orderStockOuts.Sum(x => x.amount), 0);

            var name_pans = db.Pan.Where(x => x.stockId == mainStock.id && x.name == name).Where(x => x.amount != null).Where(x => x.date <= dt_et&&x.date>=dt_st);
            if (name_pans.Any())
                number_pans = MConvert<double>.ToTypeOrDefault(name_pans.Sum(x => x.amount), 0);    
       
            number_Total = number_Ins + number_pans - number_Outs - number_OrderOuts;

            stockinfo.stockleft=number_Total;
            stockinfo.unitPrice=unitPrice;
            stockinfo.totalMoney=Math.Round(number_Total*unitPrice,2);
            return stockinfo;
        }

        private void exportGoods_Click(object sender, EventArgs e)
        {
            if (m_Thread.IsAlive)
            {
                MessageBox.Show("数据加载中，请稍侯...");
                return;
            }
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_Thread.IsAlive)
            {
                MessageBox.Show("数据加载中，请稍侯...");
                return;
            }
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
            //if (m_Thread != null || m_Thread.IsAlive)
            //    m_Thread.Abort();            
            dgv_show();
            comboxName.Text = "";
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F7:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "现有库存", false, "");
                    break;
                case Keys.F4:
                    addGoods_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //增加库存商品类别
        private void addSeatType_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
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
            var db = new BathDBDataContext(LogIn.connectionString);

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
            var db = new BathDBDataContext(LogIn.connectionString);

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
            var db = new BathDBDataContext(LogIn.connectionString);

            //if (m_Thread.IsAlive)
            //{
            //    MessageBox.Show("数据加载中，请稍侯...");
            //    return;
            //}
            var form = new GoodsForm(db, null, this);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgv_show();
            }
        }

        //删除商品
        private void delGoods_Click(object sender, EventArgs e)
        {

            var db = new BathDBDataContext(LogIn.connectionString);

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
            var db = new BathDBDataContext(LogIn.connectionString);

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
          if (comboxName.Text.Trim()=="")
            {
                MessageBox.Show("请输入商品名称！");
                return;
            }
            dgv_show();
        }

        private void TextName_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {        
            comboxName.Text = "";
            dgv_show();
        }
    }
}
