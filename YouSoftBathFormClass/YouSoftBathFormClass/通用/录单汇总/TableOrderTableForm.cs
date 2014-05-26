using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;
using YouSoftUtil;

namespace YouSoftBathFormClass
{
    /// <summary>
    /// 录单汇总
    /// </summary>
    public partial class TableOrderTableForm : Form
    {
        //成员变量
        private BathDBDataContext db;
        private string m_con_str;
        private Employee m_user;
        private Stock stock;

        //构造函数
        public TableOrderTableForm(string con_str, Employee user)
        {
            m_con_str = con_str;
            m_user = user;
            db = new BathDBDataContext(m_con_str);
            InitializeComponent();
        }

        //对话框载入
        private void OrderTableForm_Load(object sender, EventArgs e)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 13);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 13);

            menu.Items.AddRange(db.Menu.Select(x => x.name).ToArray());
            itemSi.Items.AddRange(db.StockIn.Select(x => x.name).Distinct().ToArray());
            itemSo.Items.AddRange(db.StockOut.Select(x => x.name).Distinct().ToArray());
            orderStockOut_item.Items.AddRange(db.OrderStockOut.Select(x => x.name).Distinct().ToArray());

            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";

            orderStockOut_start.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            orderStockOut_end.CustomFormat = "yyyy-MM-dd-HH:mm:ss";

            startTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00");
            startTimeSi.Value = DateTime.Now.AddMonths(-1);
            startTimeSo.Value = DateTime.Now.AddMonths(-1);

            orderStockOut_start.Value = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToShortDateString() + " 00:00:00");

            var local_ip = BathClass.get_local_ip();
            stock = db.Stock.FirstOrDefault(x => x.ips == local_ip);

            dgv_show();
        }

        //显示信息
        private void dgv_show()
        {
            var orders = db.Orders.Where(x=>x.inputEmployee == m_user.id.ToString() &&
                             x.inputTime >= startTime.Value && x.inputTime <= endTime.Value&&
                             ((cboxSeat.Checked && x.text==seat.Text)||!cboxSeat.Checked)&&
                             ((cboxMenu.Checked && x.menu == menu.Text) || !cboxMenu.Checked));
            orders = orders.OrderBy(x=>x.inputTime);
            foreach (var x in orders)
            {
                var price = db.Menu.FirstOrDefault(y=>y.name == x.menu).price;
                dgv.Rows.Add(x.text, x.systemId, x.id, price, x.number, x.menu, x.money, x.technician, x.inputTime);
            }

            var Horders = db.HisOrders.Where(x=>x.inputEmployee == m_user.id.ToString() &&
                             x.inputTime >= startTime.Value && x.inputTime <= endTime.Value &&
                             ((cboxSeat.Checked && x.text == seat.Text) || !cboxSeat.Checked) &&
                             ((cboxMenu.Checked && x.menu == menu.Text) || !cboxMenu.Checked));
            Horders = Horders.OrderBy(x => x.inputTime);
            foreach (var x in Horders)
            {
                var price = db.Menu.FirstOrDefault(y => y.name == x.menu).price;
                dgv.Rows.Add(x.text, x.systemId, x.id, price, x.number, x.menu, x.money, x.technician, x.inputTime);
            }
            BathClass.set_dgv_fit(dgv);
        }

        //显示入库信息
        private void dgv_stockin_show()
        {
            if (stock == null)
                return;

            var s = db.StockIn.Where(x => x.stockId == stock.id);
            //var s = db.StockIn.Where(x => x.transactor == m_user.name);
            s = s.Where(x => x.date >= startTimeSi.Value && x.date <= endTimeSi.Value);
            if (ciSi.Checked)
                s = s.Where(x => x.name == itemSi.Text);

            dgvStockIn.DataSource = from x in s
                             orderby x.id
                             select new
                             {
                                 单号 = x.id,
                                 名称 = x.name,
                                 进价 = x.cost,
                                 数量 = x.amount,
                                 仓库 = db.Stock.FirstOrDefault(y => y.id == x.stockId).name,
                                 日期 = x.date,
                                 经手人 = x.transactor,
                                 审核 = x.checker,
                                 备注 = x.note
                             };
            BathClass.set_dgv_fit(dgvStockIn);
        }

        //显示出库信息
        private void dgv_stockout_show()
        {
            if (stock == null)
                return;

            var so = db.StockOut.Where(x => x.stockId == stock.id);
            //var so = db.StockOut.Where(x => x.receiver == m_user.name);
            so = so.Where(x => x.date >= startTimeSi.Value && x.date <= endTimeSi.Value);
            if (ciSi.Checked)
                so = so.Where(x => x.name == itemSo.Text);

            dgvStockOut.DataSource = from x in so
                             orderby x.id
                             select new
                             {
                                 单号 = x.id,
                                 名称 = x.name,
                                 数量 = x.amount,
                                 仓库 = stock.name,
                                 接受仓库 = db.Stock.FirstOrDefault(s => s.id == x.toStockId).name,
                                 日期 = x.date,
                                 领料人 = x.receiver,
                                 经手人 = x.transactor,
                                 审核 = x.checker,
                                 备注 = x.note
                             };
            BathClass.set_dgv_fit(dgvStockOut);
        }

        //显示售卖消耗信息
        private void dgv_orderstockout_show()
        {
            if (stock == null)
                return;

            var so = db.OrderStockOut.Where(x => x.stockId == stock.id && x.deleteEmployee == null);
            so = so.Where(x => x.date >= orderStockOut_start.Value && x.date <= orderStockOut_end.Value);
            if (cb_orderStockOut_item.Checked)
                so = so.Where(x => x.name == orderStockOut_item.Text);

            dgvOrderStockOut.DataSource = from x in so
                                     orderby x.id
                                     select new
                                     {
                                         名称 = x.name,
                                         数量 = x.amount,
                                         日期 = x.date,
                                         销售人员=x.sales,
                                         订单号=x.orderId
                                     };
            BathClass.set_dgv_fit(dgvOrderStockOut);
        }

        //显示现有库存信息
        private void dgv_storageList_show()
        {
            if (stock == null)
                return;

            dgvStorageList.Rows.Clear();
            var stockIns = db.StockIn.Where(x => x.stockId == stock.id);
            var stockOuts = db.StockOut.Where(x => x.stockId == stock.id);
            var orderStockOuts = db.OrderStockOut.Where(x => x.stockId == stock.id && x.deleteEmployee==null);
            var pans = db.Pan.Where(x => x.stockId == stock.id);

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

                var name_stockIns = stockIns.Where(x => x.name == name).Where(x=>x.amount!=null);
                if (name_stockIns.Any())
                    number_Ins = name_stockIns.Sum(x => x.amount).Value;

                var name_stockOuts = stockOuts.Where(x => x.name == name).Where(x => x.amount != null);
                if (name_stockOuts.Any())
                    number_Outs = MConvert<double>.ToTypeOrDefault(name_stockOuts.Sum(x => x.amount), 0);

                var name_orderStockOuts =orderStockOuts.Where(x => x.name == name).Where(x => x.amount != null);
                if (name_orderStockOuts.Any())
                    number_OrderOuts = MConvert<double>.ToTypeOrDefault(name_orderStockOuts.Sum(x => x.amount), 0);

                var name_pans = pans.Where(x => x.name == name).Where(x => x.amount != null);
                if (name_pans.Any())
                    number_pans = MConvert<double>.ToTypeOrDefault(name_pans.Sum(x => x.amount), 0);
                dgvStorageList.Rows.Add(name, number_Ins + number_pans - number_Outs - number_OrderOuts, stock.name);
            }

            BathClass.set_dgv_fit(dgvStorageList);
        }

        //手牌号
        private void cboxSeat_CheckedChanged(object sender, EventArgs e)
        {
            seat.ReadOnly = !cboxSeat.Checked;
        }

        //项目编码
        private void cboxMenu_CheckedChanged(object sender, EventArgs e)
        {
            menu.Enabled = cboxMenu.Checked;
        }

        //查询
        private void toolFind_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0://录单汇总
                    dgv_show();
                    break;
                case 1://售卖消耗明细
                    dgv_orderstockout_show();
                    break;
                case 2://补货单
                    dgv_stockin_show();
                    break;
                case 3://出货单
                    dgv_stockout_show();
                    break;
                case 4://现有库存明细
                    dgv_storageList_show();
                    break;
                default:
                    break;
            }
        }

        //导出
        private void toolExport_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void toolPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv);
        }

        //产生补货单
        private void toolPopup_Click(object sender, EventArgs e)
        {
            StockInForm stockInForm = new StockInForm(db);
            stockInForm.ShowDialog();
            dgv_stockin_show();
        }

        //商品出库
        private void toolOutStorage_Click(object sender, EventArgs e)
        {
            StockOutForm stockOutForm = new StockOutForm(db);
            stockOutForm.ShowDialog();
            dgv_stockout_show();
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
                case Keys.F3:
                    toolFind_Click(null, null);
                    break;
                case Keys.F4:
                    toolExport_Click(null, null);
                    break;
                case Keys.F5:
                    toolPrint_Click(null, null);
                    break;
                case Keys.F6:
                    toolPopup_Click(null, null);
                    break;
                case Keys.F7:
                    toolOutStorage_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 0://录单汇总
                    dgv_show();
                    break;
                case 1://售卖消耗明细
                    dgv_orderstockout_show();
                    break;
                case 2://补货单
                    dgv_stockin_show();
                    break;
                case 3://出货单
                    dgv_stockout_show();
                    break;
                case 4://现有库存明细
                    dgv_storageList_show();
                    break;
                default:
                    break;
            }
        }

        private void cSi_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ciSi_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void cSo_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void ciSo_CheckedChanged(object sender, EventArgs e)
        {
            itemSo.Enabled = ciSo.Checked;
        }

        private void seat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void cb_orderStockOut_item_CheckedChanged(object sender, EventArgs e)
        {
            orderStockOut_item.Enabled = cb_orderStockOut_item.Checked;
        }

    }
}
