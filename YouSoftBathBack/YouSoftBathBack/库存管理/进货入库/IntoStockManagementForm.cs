using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
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
        private List<string> stockList;
        private string proName = "";

        private Thread m_Thread ;
        //private Thread subtoal_Thread = null;
        private DateTime dt_st, dt_et;
        private string stockSel = "";
        private double totalMoney;


        private delegate void delegate_add_row(object[] vals);

        //构造函数
        public IntoStockManagementForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (db.StockIn.Any())
            {
                DateTime dt = DateTime.Now;
                st.Value = DateTime.Now.AddMonths(-1);
                et.Value = DateTime.Now;
                
            }
            stockList = db.Stock.Select(x => x.name).ToList();
            comboxProductName.Items.AddRange(db.StorageList.Select(x => x.name).Distinct().ToArray());
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
        private void add_row(object[] vals)
        {
            dgv.Rows.Add(vals);
        }

        //显示清单
        public void dgv_show()
        {
            dgv.Rows.Clear();
            stockSel = stockTree.SelectedNode.Text;
            proName = comboxProductName.Text;
            dt_st = DateTime.Parse(st.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            dt_et = DateTime.Parse(et.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            //
            if (m_Thread != null && m_Thread.IsAlive)
                m_Thread.Abort();
            m_Thread=new Thread(new ThreadStart(do_dgv_Show));
            m_Thread.Start();
           
         
            //subtoal_Thread = new Thread(new ThreadStart(subTotal));
            //subtoal_Thread.Start();

                #region 原来的方法，数据绑定
                //    dgv.DataSource = from x in stockinSource
            //                     where x.stockId == selId
            //                     orderby x.id
            //                     select new
            //                     {
            //                         单号 = x.id,
            //                         名称 = x.name,
            //                         类别 = db.GoodsCat.FirstOrDefault(y => y.id == db.StorageList.FirstOrDefault(m => m.name == x.name).goodsCatId).name,
            //                         单位 = x.unit,
            //                         进价 = x.cost,
            //                         数量 = x.amount,
            //                         金额 = x.money,
            //                         仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
            //                         日期 = x.date,
            //                         经手人 = x.transactor,
            //                         审核 = x.checker,
            //                         备注 = x.note
                //                     };
                #endregion
       
            BathClass.set_dgv_fit(dgv);
        }
        # region  dgv_show(string proName) 重载dgv_show方法
        public void dgv_show(string proName)
        {
            string stockSel = stockTree.SelectedNode.Text;
            DateTime dt_st = DateTime.Parse(st.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime dt_et = DateTime.Parse(et.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            dgv.Rows.Clear();

            var db = new BathDBDataContext(LogIn.connectionString);
            if (stockSel == "所有仓库")
            {
                var stockinSource = db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et && x.name.Contains(comboxProductName.Text.Trim())).OrderBy(x => x.id);
                #region 原来的方法，绑定数据源
                //    dgv.DataSource = from x in stockinSource
                //                     orderby x.id
                //                     select new
                //                     {
                //                         单号 = x.id,
                //                         名称 = x.name,
                //                         类别 = db.GoodsCat.FirstOrDefault(y => y.id == db.StorageList.FirstOrDefault(m => m.name == x.name).goodsCatId).name,
                //                         单位 = x.unit,
                //                         供应商 = db.Provider.FirstOrDefault(y => y.id == x.providerId).name,
                //                         进价 = x.cost,
                //                         数量 = x.amount,
                //                         金额 = x.money,
                //                         仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
                //                         日期 = x.date,
                //                         经手人 = x.transactor,
                //                         审核 = x.checker,
                //                         备注 = x.note
                //                     };
                #endregion
                foreach (var stockin in stockinSource)
                {//单号0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7,日期8,审核9,供应商10,备注11 
                    string[] stockinRow = new string[12];
                    stockinRow[0] = stockin.id.ToString();
                    stockinRow[1] = stockin.name;
                    stockinRow[2] = getCatgoryByName(stockin.name);
                    stockinRow[3] = stockin.unit;
                    stockinRow[4] = MConvert<double>.ToTypeOrDefault(stockin.cost, 0).ToString();
                    stockinRow[5] = MConvert<double>.ToTypeOrDefault(stockin.amount, 0).ToString();
                    stockinRow[6] = MConvert<double>.ToTypeOrDefault(stockin.money, 0).ToString();
                    stockinRow[7] = getStockByName(db, stockin.name);
                    stockinRow[8] = stockin.date.ToString();
                    stockinRow[9] = stockin.checker;
                    stockinRow[10] = getProviderByName(db, stockin.name);
                    stockinRow[11] = stockin.note;
                    dgv.Rows.Add(stockinRow);
                }
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                var stockinSource = db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et && x.name.Contains(comboxProductName.Text.Trim())).Where(x => x.stockId == selId).OrderBy(x => x.id);
                foreach (var stockin in stockinSource)
                {//单号0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7,日期8,审核9,供应商10,备注11 
                    string[] stockinRow = new string[12];
                    stockinRow[0] = stockin.id.ToString();
                    stockinRow[1] = stockin.name;
                    stockinRow[2] = getCatgoryByName(stockin.name);
                    stockinRow[3] = stockin.unit;
                    stockinRow[4] = MConvert<double>.ToTypeOrDefault(stockin.cost, 0).ToString();
                    stockinRow[5] = MConvert<double>.ToTypeOrDefault(stockin.amount, 0).ToString();
                    stockinRow[6] = MConvert<double>.ToTypeOrDefault(stockin.money, 0).ToString();
                    stockinRow[7] = getStockByName(db, stockin.name);
                    stockinRow[8] = stockin.date.ToString();
                    stockinRow[9] = stockin.checker;
                    stockinRow[10] = getProviderByName(db, stockin.name);
                    stockinRow[11] = stockin.note;
                    dgv.Rows.Add(stockinRow);

                    #region 原来的方法，绑定数据源
                    //    dgv.DataSource = from x in stockinSource
                    //                     where x.stockId == selId
                    //                     orderby x.id
                    //                     select new
                    //                     {
                    //                         单号 = x.id,
                    //                         名称 = x.name,
                    //                         类别 = db.GoodsCat.FirstOrDefault(y => y.id == db.StorageList.FirstOrDefault(m => m.name == x.name).goodsCatId).name,
                    //                         单位 = x.unit,
                    //                         进价 = x.cost,
                    //                         数量 = x.amount,
                    //                         金额 = x.money,
                    //                         仓库 = db.Stock.FirstOrDefault(s => s.id == x.stockId).name,
                    //                         日期 = x.date,
                    //                         经手人 = x.transactor,
                    //                         审核 = x.checker,
                    //                         备注 = x.note
                    //                     };
                    #endregion
                }              
            }
            subTotal();
            BathClass.set_dgv_fit(dgv);
        }
  
    #endregion

    #region  do_dgv_Show
    private void do_dgv_Show()
    {
       try
       {
           totalMoney=0;
           var db = new BathDBDataContext(LogIn.connectionString);
            if (stockSel == "所有仓库")
            {
               var stockinSource = db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et).OrderBy(x => x.id).AsQueryable();
               if (proName != "")
                                 {
                   stockinSource = stockinSource.Where(x => x.name.Contains(proName));
                   if (stockinSource == null)
                       return;
               }

                   foreach (var stockin in stockinSource)
                   {//单号0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7,日期8,审核9,供应商10,备注11 
                       string[] stockinRow = new string[12];
                       stockinRow[0] = stockin.id.ToString();
                       stockinRow[1] = stockin.name;
                       stockinRow[2] = getCatgoryByName(stockin.name);
                       stockinRow[3] = stockin.unit;
                       stockinRow[5] = MConvert<double>.ToTypeOrDefault(stockin.cost, 0).ToString();
                       stockinRow[4] = MConvert<double>.ToTypeOrDefault(stockin.amount, 0).ToString();
                       stockinRow[6] = MConvert<double>.ToTypeOrDefault(stockin.money, 0).ToString();
                       totalMoney+=Convert.ToDouble(stockinRow[6]);
                       stockinRow[7] = getStockByName(db, stockin.name);
                       stockinRow[8] = stockin.date.ToString();
                       stockinRow[9] = stockin.checker;
                       stockinRow[10] = getProviderByName(db, stockin.name);
                       stockinRow[11] = stockin.note;
                       this.Invoke(new delegate_add_row(add_row), (object)stockinRow);
                   }
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                   var stockinSource = db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.stockId == selId).OrderBy(x => x.id).AsQueryable();
                   if (proName != "")
                                 {
                       stockinSource = stockinSource.Where(x => x.name.Contains(proName));
                       if (stockinSource == null)
                           return;
            }
                   foreach (var stockin in stockinSource)
                   {//单号0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7,日期8,审核9,供应商10,备注11 
                       string[] stockinRow = new string[12];
                       stockinRow[0] = stockin.id.ToString();
                       stockinRow[1] = stockin.name;
                       stockinRow[2] = getCatgoryByName(stockin.name);
                       stockinRow[3] = stockin.unit;
                       stockinRow[5] = MConvert<double>.ToTypeOrDefault(stockin.cost, 0).ToString();
                       stockinRow[4] = MConvert<double>.ToTypeOrDefault(stockin.amount, 0).ToString();
                       stockinRow[6] = MConvert<double>.ToTypeOrDefault(stockin.money, 0).ToString();
                       totalMoney += Convert.ToDouble(stockinRow[6]);
                       stockinRow[7] = getStockByName(db, stockin.name);
                       stockinRow[8] = stockin.date.ToString();
                       stockinRow[9] = stockin.checker;
                       stockinRow[10] = getProviderByName(db, stockin.name);
                       stockinRow[11] = stockin.note;
                       this.Invoke(new delegate_add_row(add_row), (object)stockinRow);
                   }
               }
           string[] subTotalRow1 = { "" };
           this.Invoke(new delegate_add_row(add_row),(object)subTotalRow1);
           string[] subTotalRow2 = {"","","","","","","总金额"};
           this.Invoke(new delegate_add_row(add_row),(object)subTotalRow2);
              string[] subTotalRow3 = {"","","","","","",totalMoney.ToString()};
           this.Invoke(new delegate_add_row(add_row),(object)subTotalRow3);
           
           //subTotal();

       }
       catch 
       {
           //MessageBox.Show(ex.Message);
           //m_Thread.Abort();
       }
       //m_Thread.Join();
       
   }

#endregion

        public void subTotal()
        {////单号0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7,日期8,审核9,供应商10,备注11 
            double totalAmout = 0;          
            List<string> names = new List<string>();

            var db = new BathDBDataContext(LogIn.connectionString);
            if (stockSel=="所有仓库")
            {
                if (proName == "")
                    names = names = db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et).Select(x => x.name).Distinct().ToList();
                else
                    names = names = db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.name.Contains(proName)).Select(x => x.name).Distinct().ToList();
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                if (proName == "")
                    names = names = db.StockIn.Where(x=>x.stockId==selId).Where(x => x.date >= dt_st && x.date <= dt_et).Select(x => x.name).Distinct().ToList();
                else
                    names = names = db.StockIn.Where(x => x.stockId == selId).Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.name.Contains(proName)).Select(x => x.name).Distinct().ToList();
            }
            
            try
            {
                //汇总信息0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7
                string[] subtotal1 = { "" };
                this.Invoke(new delegate_add_row(add_row), (object)subtotal1);
                //dgv.Rows.Add("");
                string[] subtotal2 = { "汇总信息", "名称", "类别", "单位", "总数量", "进价", "总金额" };
                this.Invoke(new delegate_add_row(add_row), (object)subtotal2);
                //dgv.Rows.Add("汇总信息", "名称", "类别", "单位",  "进价", "总数量", "总金额");
                foreach (var name in names)
                {
                    object[] subTotalRow = new object[7];
                    subTotalRow[0] = "";
                    subTotalRow[1] = name;
                    subTotalRow[2] = getCatgoryByName(name);
                    subTotalRow[3] = db.StockIn.FirstOrDefault(x => x.name == name).unit;
                    //subTotalRow[4] = getPriceByName(name);
                    subTotalRow[5] =getPriceByName(db,name);
                    subTotalRow[4] = MConvert<double>.ToTypeOrDefault(db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.name == name).Sum(x => x.amount), 0);
                    subTotalRow[6] = MConvert<double>.ToTypeOrDefault(db.StockIn.Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.name == name).Sum(x => x.money), 0);
                    totalAmout += (double)subTotalRow[6];
                    this.Invoke(new delegate_add_row(add_row), (object)subTotalRow);
                }
                string[] subtotal3 = { "", "", "", "", "", "", totalAmout.ToString() };
                this.Invoke(new delegate_add_row(add_row), (object)subtotal3);
                //m_Thread.Abort();
            }
            catch 
            {
                //MessageBox.Show(ex.Message);
            	
            }
            finally
            {
                m_Thread.Abort();

        }

        }


        //导出清单
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
                case Keys.F6:
                    toolEdit_Click(null,null);
                    break;
                default:
                    break;
            }
        }

        //录入进货单
        private void toolIntoStock_Click(object sender, EventArgs e)
        {
           var db = new BathDBDataContext(LogIn.connectionString);
            //StockInForm inStockForm = new StockInForm(db, this);
            StockInForm inStockForm = new StockInForm(db);
            if (inStockForm.ShowDialog() == DialogResult.OK)
            {
                if (dgv.Rows.Count >= 50)
                    return;
                dgv_show();
            }
                
        }

        private void toolEdit_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printWarningMsg("未选中入库单！");
                return;
            }         
            if (dgv.CurrentRow.Cells[0].Value.ToString()=="")
            {
                MessageBox.Show("请先选择一种商品!");
                return;
            }
            var db = new BathDBDataContext(LogIn.connectionString);
            StockIn stockin = db.StockIn.FirstOrDefault(x => x.id == Convert.ToInt32(dgv.CurrentRow.Cells[0].Value));
            EditForm editform = new EditForm(db,stockin);
            if(editform.ShowDialog()==DialogResult.OK)
                dgv_show();
        }

        #region getPriceByName   根据商品名称得到价格
        public double getPriceByName(BathDBDataContext db, string name)
        {
            var price = db.StockIn.OrderBy(x => x.date).FirstOrDefault(x => x.name == name);
            if (price != null)
                return MConvert<double>.ToTypeOrDefault(price.cost, 0);
            else
                return 0;

        }
        #endregion
        #region getCatgoryByName根椐商品名称得到其所在的类别
        public string getCatgoryByName(string name)
        {
            BathDBDataContext db = new BathDBDataContext(LogIn.connectionString);
            var goodcat = db.StorageList.FirstOrDefault(x => x.name == name);
            if (goodcat != null)
            {
                int goodsCatID = MConvert<int>.ToTypeOrDefault(db.StorageList.FirstOrDefault(x => x.name == name).goodsCatId, 0);
                var catName = db.GoodsCat.FirstOrDefault(x => x.id == goodsCatID);
                if (catName != null)
                    return catName.name;
                else
                    return "";
            }
            else
                return "";
        }


        #endregion

        #region getStockByName 根椐商品名称得到仓库名称
        public string getStockByName(BathDBDataContext db, string name)
        {
            var st = db.StockIn.FirstOrDefault(x => x.name == name);
            if (st!= null)
            {
                int stockid = MConvert<int>.ToTypeOrDefault(st.stockId, 0);
                var stock = db.Stock.FirstOrDefault(x => x.id == stockid);
                if (stock != null)
                    return stock.name;
                else
                    return "";
            }
            else
                return "";          
        }
        #endregion
        
        #region getProviderByName  根椐商品名称得到供应商名称
        public string getProviderByName(BathDBDataContext db, string name)
        {
            int providerID = MConvert<int>.ToTypeOrDefault(db.StockIn.FirstOrDefault(x => x.name == name).providerId, 0);
            var provider=db.Provider.FirstOrDefault(x=>x.id==providerID);
            if (provider!=null)
                return provider.name;
            else
                return "";
        }
        #endregion



        private void btnFind_Click(object sender, EventArgs e)
        {
            comboxProductName.Text = "";
            dgv_show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           string pro=comboxProductName.Text.Trim();
            if (pro == "")
            {
               MessageBox.Show("请输入产品名称!");
                comboxProductName.Focus();
                return;
            }
            else
                dgv_show();
        }
    }
}
