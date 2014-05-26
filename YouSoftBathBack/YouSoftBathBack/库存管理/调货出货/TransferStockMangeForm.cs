using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class TransferStockMangeForm : Form
    {
        //成员变量
        private List<string> stockList;
        private string proName = "";
        private Thread m_Thread ;
        private DateTime dt_st, dt_et;
        private string stockSel = "";
        private double totalMoney;


        private delegate void delegate_add_row(object[] vals);
        private delegate void delegate_set_dgv_fit(DataGridView dg);
        

        //构造函数
        public TransferStockMangeForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);

            if (db.StockOut.Any())
            {
                DateTime dt = DateTime.Now;
                st.Value = DateTime.Now.AddMonths(-1);
                et.Value = DateTime.Now;
            }
            stockList = db.Stock.Select(x => x.name).ToList();
            createTree(stockTree, stockList);
            comboxProductName.Items.AddRange(db.StorageList.Select(x => x.name).Distinct().ToArray());

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

        private void add_row(object [] vals)
        {
            dgv.Rows.Add(vals);
        }

        //显示清单
        private void dgv_show()
        {
             dt_st = DateTime.Parse(st.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00");
             dt_et = DateTime.Parse(et.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59");    
            stockSel = stockTree.SelectedNode.Text;
            proName = comboxProductName.Text.Trim();
            dgv.Rows.Clear();            
            if (m_Thread != null && m_Thread.IsAlive)
                m_Thread.Abort();
            m_Thread = new Thread(new ThreadStart(do_dgv_Show));
            m_Thread.Start();
            
        }


        private void do_dgv_Show()
        {
            totalMoney = 0;
            var db = new BathDBDataContext(LogIn.connectionString);
            try
            {
                if (stockSel == "所有仓库")
                {
                    var sos = db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et).OrderBy(x => x.id).AsQueryable();
                    if (proName!="")
                    {
                        sos = sos.Where(x => x.name.Contains(proName));
                        if (sos == null)
                            return;
                    }                    
                    foreach (var so in sos)
                    {////单号0, 名称1, 类别2, 单位3, 数量4, 单价5,金额6,仓库7,日期8,审核9,,备注10 
                        object[] dgvRow = new object[11];
                        dgvRow[0] = so.id;
                        dgvRow[1] = so.name;
                        dgvRow[2] = getCatgoryByName(db, so.name);
                        dgvRow[3] = so.unit;
                        dgvRow[4] = so.amount;
                        dgvRow[5] = getPriceByName(db, so.name);
                        dgvRow[6] = MConvert<double>.ToTypeOrDefault(dgvRow[4], 0) * MConvert<double>.ToTypeOrDefault(dgvRow[5], 0);
                        totalMoney += MConvert<double>.ToTypeOrDefault(dgvRow[6], 0);
                        dgvRow[7] = getStockByName(db, so.name);
                        dgvRow[8] = so.date;
                        dgvRow[9] = so.checker;
                        dgvRow[10] = so.note;
                        this.Invoke(new delegate_add_row(add_row), (object)dgvRow);
                    }

                }
                else
                {
                    int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                    var sos = db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.stockId == selId).OrderBy(x => x.id).AsQueryable();
                    if (proName != "")
                    {
                        sos = sos.Where(x => x.name.Contains(proName));
                        if (sos == null)
                            return;
                    }

                    foreach (var so in sos)
                    {////单号0, 名称1, 类别2,单位3, 进价4,数量5,金额6,仓库7,日期8,审核9,,备注10 
                        object[] dgvRow = new object[11];
                        dgvRow[0] = so.id;
                        dgvRow[1] = so.name;
                        dgvRow[2] = getCatgoryByName(db, so.name);
                        dgvRow[3] = so.unit;
                        dgvRow[4] = so.amount;
                        dgvRow[5] = getPriceByName(db, so.name);
                        dgvRow[6] = MConvert<double>.ToTypeOrDefault(dgvRow[4], 0) * MConvert<double>.ToTypeOrDefault(dgvRow[5], 0);
                        totalMoney += MConvert<double>.ToTypeOrDefault(dgvRow[6], 0);
                        dgvRow[7] = getStockByName(db, so.name);
                        dgvRow[8] = so.date;
                        dgvRow[9] = so.checker;
                        dgvRow[10] = so.note;
                        this.Invoke(new delegate_add_row(add_row), (object)dgvRow);
                    }

                }
                string [] subTotalRow1 = { ""};
                this.Invoke(new delegate_add_row(add_row), (object)subTotalRow1);
                string[] subTotalRow2 = { "", "", "", "", "", "", "总金额"};
                this.Invoke(new delegate_add_row(add_row), (object)subTotalRow2);
                string [] subTotalRow3 = {"","","","","","",totalMoney.ToString()};
                this.Invoke(new delegate_add_row(add_row), (object)subTotalRow3);

                //this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //m_Thread.Abort();
            }          
            //subTotal(db);
        }



        # region getCurrentStock 商品名为参数
        public double getCurrentStock(BathDBDataContext db, string name)
        {
            
            DateTime dt_st = DateTime.Parse(st.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime dt_et = DateTime.Parse(et.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            double number_Ins = 0;
            double number_Outs = 0;
            double number_OrderOuts = 0;
            double number_pans = 0;
            double number_Total = 0;

            var name_stockIns = db.StockIn.Where(x => x.name == name).Where(x => x.amount != null).Where(x=>x.date>=dt_st&&x.date<=dt_et);
            if (name_stockIns.Any())
                number_Ins = name_stockIns.Sum(x => x.amount).Value;

            var name_stockOuts = db.StockOut.Where(x => x.name == name).Where(x => x.amount != null).Where(x=>x.date>=dt_st&&x.date<=dt_et);
            if (name_stockOuts.Any())
                number_Outs = MConvert<double>.ToTypeOrDefault(name_stockOuts.Sum(x => x.amount), 0);

            var name_orderStockOuts = db.OrderStockOut.Where(x => x.name == name).Where(x => x.amount != null).Where(x => x.date >= dt_st && x.date <= dt_et);
            if (name_orderStockOuts.Any())
                number_OrderOuts = MConvert<double>.ToTypeOrDefault(name_orderStockOuts.Sum(x => x.amount), 0);

            var name_pans = db.Pan.Where(x => x.name == name).Where(x => x.amount != null).Where(x => x.date >= dt_st && x.date <= dt_et);
            if (name_pans.Any())
                number_pans = MConvert<double>.ToTypeOrDefault(name_pans.Sum(x => x.amount), 0);
            number_Total = number_Ins + number_pans - number_Outs - number_OrderOuts;
            return Math.Round(number_Total,2);
                      
        }
        #endregion


        #region getStockByName 根椐商品名称得到仓库名称
        public string getStockByName(BathDBDataContext db, string name)
            {
            var st = db.StockOut.FirstOrDefault(x => x.name == name);
            if (st != null)
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


        #region subTotal商品分类汇总信息
        public void subTotal(BathDBDataContext db)
        {
            double totalAmout = 0;
            DateTime dt_st = DateTime.Parse(st.Value.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime dt_et = DateTime.Parse(et.Value.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            List<string> names = new List<string>();     
            try
            {
                if (stockSel == "所有仓库")
                {
                    if (proName == "")
                        names = db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et).Select(x => x.name).Distinct().ToList();
                    else
                        names = db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et && x.name.Contains(proName)).Select(x => x.name).Distinct().ToList();
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                    if (proName == "")
                        names = db.StockOut.Where(x=>x.stockId==selId).Where(x => x.date >= dt_st && x.date <= dt_et).Select(x => x.name).Distinct().ToList();
                    else
                        names = db.StockOut.Where(x=>x.stockId==selId).Where(x => x.date >= dt_st && x.date <= dt_et && x.name.Contains(proName)).Select(x => x.name).Distinct().ToList();
                }
                
                string[] sbutotalrow1 = { "" };
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow1);
                string[] sbutotalrow2 = { "汇总信息" };
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow2);
                string[] sbutotalrow3 = { "", "名称", "类别", "单位", "数量", "单价", "总金额", "仓库", "现有库存" };
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow3);
                //string[] subtotalRow = new string[11];
                //"0", "名称1", "类别2", "单位3", "数量4", "单价5", "总金额6", "仓库7", "现有库存8"
                foreach (var name in names)
                {
                    object[] subtotalRow = new object[9];
                    subtotalRow[0] = "";
                    subtotalRow[1] = name;
                    subtotalRow[2] = getCatgoryByName(db, name);
                    subtotalRow[3] = db.StockOut.FirstOrDefault(x => x.name == name).unit;
                    //subtotalRow[4]=getPriceByName(name);
                    subtotalRow[5] =getPriceByName(db,name);
                    subtotalRow[4] = MConvert<double>.ToTypeOrDefault(db.StockOut.Where(x => x.date >= dt_st && x.date <= dt_et).Where(x => x.name == name).Sum(x => x.amount), 0);
                    //subtotalRow[6] = (((double)subtotalRow[4]) * ((double)subtotalRow[5]));
                    subtotalRow[6] = MConvert<double>.ToTypeOrDefault(subtotalRow[4], 0) * MConvert<double>.ToTypeOrDefault(subtotalRow[5], 0);
                    totalAmout += MConvert<double>.ToTypeOrDefault(subtotalRow[6], 0);
                    subtotalRow[7] = getStockByName(db, name);
                    subtotalRow[8] = getCurrentStock(db, name);
                    //dgv.Rows.Add(subtotalRow);
                    this.Invoke(new delegate_add_row(add_row), (object)subtotalRow);
                }
                string[] sbutotalrow4 = { "", "", "", "", "", "", totalAmout.ToString() };
                this.Invoke(new delegate_add_row(add_row), (object)sbutotalrow4);
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
        #endregion


        #region getPriceByName   根据商品名称得到价格
        public double getPriceByName(BathDBDataContext db, string name)
        {
            double totalMoney = 0;
            double totalAmount = 0;
            double unitPrice=0;
            var prices = db.StockIn.Where(x => x.date <= dt_et).Where(x => x.name == name);
            if (prices.Any())
            {
                totalMoney = MConvert<double>.ToTypeOrDefault(prices.Sum(x => x.money), 0);
                totalAmount = MConvert<double>.ToTypeOrDefault(prices.Sum(x => x.amount), 0);
                if (totalAmount != 0)
                    unitPrice = Math.Round(totalMoney / totalAmount, 2);
        }
            return unitPrice;
        }
        #endregion


        #region getCatgoryByName根椐商品名称得到其所在的类别
        public string getCatgoryByName(BathDBDataContext db, string name)
        {
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
            if (m_Thread.IsAlive)
                m_Thread.Abort();
            
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
                    if (m_Thread.IsAlive)
                        m_Thread.Abort();
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
            var db = new BathDBDataContext(LogIn.connectionString);
            StockOutForm inStockForm = new StockOutForm(db,null);
            if (inStockForm.ShowDialog() == DialogResult.OK)
                dgv_show();
           
        }

        //查询
        private void btnFind_Click(object sender, EventArgs e)
        {
            comboxProductName.Text = "";
            dgv_show();
        }

        private void toolEdit_Click(object sender, EventArgs e)
        {
           if (dgv.CurrentCell != null)
            {
                  int stockoutid;
                try
                {
                    stockoutid = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("请选择一种商品!");
                    return;
        }


                var db = new BathDBDataContext(LogIn.connectionString);
                StockOut stkout = db.StockOut.FirstOrDefault(x => x.id == stockoutid);
                if (stkout!=null)
                {
                    StockOutForm editstockoutform = new StockOutForm(db, stkout);
                    if (editstockoutform.ShowDialog()==DialogResult.OK)                   
                         dgv_show();
                }
                
            }
            else
                MessageBox.Show("请先选择一行!");
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           string proName=comboxProductName.Text.Trim();
            if (proName == "")            
                MessageBox.Show("请输入产品!");           
            else
                dgv_show();            
        }
    
    }
}
