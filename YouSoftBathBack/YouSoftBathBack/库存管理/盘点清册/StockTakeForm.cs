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
    public partial class StockTakeForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> stockList;

        //构造函数
        public StockTakeForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            stockList = db.Stock.Select(x => x.name).ToList();
            createTree(stockTree, stockList);
        }

        //创建树
        private void createTree(TreeView tv, List<string> nodes)
        {
            tv.Nodes.Clear();

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
            dgv.Rows.Clear();
            string stockSel = stockTree.SelectedNode.Text;
            if (stockSel == "所有仓库")
            {
                //BathClass.printErrorMsg("请选择仓库");
                return;
            }
            else
            {
                int selId = db.Stock.FirstOrDefault(x => x.name == stockSel).id;
                var stockIns = db.StockIn.Where(x => x.stockId == selId);
                var stockOuts = db.StockOut.Where(x => x.stockId == selId);
                var orderStockOuts = db.OrderStockOut.Where(x => x.stockId == selId && x.deleteEmployee == null);
                var pans = db.Pan.Where(x => x.stockId == selId);

                var name_Ins = stockIns.Select(x => x.name);
                var name_outs = stockOuts.Select(x => x.name);
                var name_OOuts = orderStockOuts.Select(x => x.name);

                var name_all = name_Ins.Union(name_outs).Union(name_OOuts).Distinct();
                foreach (var name in name_all)
                {
                    double number_Ins = 0;
                    double number_Outs = 0;
                    double number_OrderOuts = 0;
                    double number_pans = 0;

                    var name_stockIns = stockIns.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_stockIns.Any())
                        number_Ins = name_stockIns.Sum(x => x.amount).Value;

                    var name_stockOuts = stockOuts.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_stockOuts.Any())
                        number_Outs = MConvert<double>.ToTypeOrDefault(name_stockOuts.Sum(x => x.amount), 0);

                    var name_orderStockOuts = orderStockOuts.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_orderStockOuts.Any())
                        number_OrderOuts = MConvert<double>.ToTypeOrDefault(name_orderStockOuts.Sum(x => x.amount), 0);

                    var name_pans = pans.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_pans.Any())
                        number_pans = MConvert<double>.ToTypeOrDefault(name_pans.Sum(x => x.amount), 0);

                    double number_Ins_last = 0;//上月结存
                    double number_Outs_last = 0;//上月结存
                    double number_Sale_last = 0;//上月结存
                    double number_pans_last = 0;//上月结存

                    double number_Ins_this = 0;//本月进货
                    double number_Outs_this = 0;//本月出货
                    double number_Sale_this = 0;//销售消耗
                    double number_pans_this = 0;//销售消耗

                    #region 上月数据
                    var first_day_this_month = DateTime.Now.AddDays(1 - DateTime.Now.Day);
                    var stockIns_last = name_stockIns.Where(x => x.date < first_day_this_month);
                    if (stockIns_last.Any())
                        number_Ins_last = stockIns_last.Sum(x => x.amount).Value;

                    var stockOuts_last = name_stockOuts.Where(x => x.date < first_day_this_month);
                    if (stockOuts_last.Any())
                        number_Outs_last = MConvert<double>.ToTypeOrDefault(stockOuts_last.Sum(x => x.amount), 0);

                    var orderStockOuts_last = name_orderStockOuts.Where(x => x.date < first_day_this_month);
                    if (orderStockOuts_last.Any())
                        number_Sale_last = MConvert<double>.ToTypeOrDefault(orderStockOuts_last.Sum(x => x.amount), 0);

                    var pans_last = name_pans.Where(x => x.date < first_day_this_month);
                    if (pans_last.Any())
                        number_pans_last = MConvert<double>.ToTypeOrDefault(pans_last.Sum(x => x.amount), 0);
                    #endregion

                    #region 本月数据
                    var stockIns_this = name_stockIns.Where(x => x.date >= first_day_this_month);
                    if (stockIns_this.Any())
                        number_Ins_this = stockIns_this.Sum(x => x.amount).Value;

                    var stockOuts_this = name_stockOuts.Where(x => x.date >= first_day_this_month);
                    if (stockOuts_this.Any())
                        number_Outs_this = MConvert<double>.ToTypeOrDefault(stockOuts_this.Sum(x => x.amount), 0);

                    var orderStockOuts_this = name_orderStockOuts.Where(x => x.date >= first_day_this_month);
                    if (orderStockOuts_this.Any())
                        number_Sale_this = MConvert<double>.ToTypeOrDefault(orderStockOuts_this.Sum(x => x.amount), 0);

                    var pans_this = name_pans.Where(x => x.date >= first_day_this_month);
                    if (pans_this.Any())
                        number_pans_this = MConvert<double>.ToTypeOrDefault(pans_this.Sum(x => x.amount), 0);
                    #endregion

                    //仓库  名称 上月结存 本月进货 本月出货 销售消耗 本月盘点损耗 现有库存
                    dgv.Rows.Add(stockSel, 
                        name,
                        number_Ins_last + number_pans_last - number_Outs_last - number_Sale_last, 
                        number_Ins_this, 
                        number_Outs_this, 
                        number_Sale_this,
                        -number_pans_this,
                        number_Ins + number_pans - number_Outs - number_OrderOuts);
                }
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

        //完成盘点
        private void toolTakeDone_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("没有选择行!");
                return;
            }

            //仓库  名称 上月结存 本月进货 本月出货 销售消耗 本月盘点损耗  现有库存
            string name = dgv.CurrentRow.Cells[1].Value.ToString();
            string stock = dgv.CurrentRow.Cells[0].Value.ToString();
            double amount = Convert.ToDouble(dgv.CurrentRow.Cells[7].Value);
            var form = new PanForm(db, name, stock, amount);
            form.ShowDialog();

            dgv_show();
        }

    }
}
