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

namespace YouSoftBathReception
{
    public partial class TechDetailsForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime lastTime;
        private DateTime thisTime;

        //构造函数
        public TechDetailsForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CashierForm_Load(object sender, EventArgs e)
        {
            try
            {
                catgory.Items.Add("所有类别");
                catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
                catgory.SelectedIndex = 0;
                this.Location = new Point(0, 0);
                toolFind_Click(null, null);
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
                this.Close();
            }
        }

        //获取夜审时间
        private bool get_clear_table_time()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var lct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == startDate.Value.Date);
            if (lct == null)
            {
                lct = db.ClearTable.Where(x => x.clearTime < startDate.Value).OrderByDescending(x => x.clearTime).FirstOrDefault();
                if (lct == null)
                    lastTime = DateTime.Parse("2013-01-01");
                else
                    lastTime = lct.clearTime;
            }
            else
                lastTime = lct.clearTime;

            var ct = db.ClearTable.Where(x => x.clearTime.Date >= endDate.Value.AddDays(1).Date).FirstOrDefault();
            if (ct == null)
                thisTime = DateTime.Now;
            else
                thisTime = ct.clearTime;

            return true;
        }

        //查询
        private void dgv_show()
        {
            dgv.Rows.Clear();

            var dc = new BathDBDataContext(LogIn.connectionString);
            var all_his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
            var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

            all_his_orders = all_his_orders.Where(x => x.technician != null);
            orderLst = orderLst.Where(x => x.technician != null);

            if (catgory.Text != "所有类别")
            {
                var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                orderLst = orderLst.Where(x => menus.Contains(x.menu));
                all_his_orders = all_his_orders.Where(x => menus.Contains(x.menu));
            }
            foreach (var order in all_his_orders)
            {
                dgv.Rows.Add(order.technician, order.text, order.menu, order.number, order.inputTime.ToString("MM-dd HH:mm"));
            }

            foreach (var order in orderLst)
            {
                dgv.Rows.Add(order.technician, order.text, order.menu, order.number, order.inputTime.ToString("MM-dd HH:mm"));
            }
            BathClass.set_dgv_fit(dgv);
        }


        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                    dgv_show();

                var dc = new BathDBDataContext(LogIn.connectionString);
                string companyName = dc.Options.FirstOrDefault().companyName;
                List<string> printColumns = new List<string>();
                foreach (DataGridViewColumn dgvCol in dgv.Columns)
                {
                    printColumns.Add(dgvCol.HeaderText);
                }

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


                PrintReceipt.Print_DataGridView("技师对账单汇总", dgv, lastTime.ToString("yyyy-MM-dd HH:mm:ss"), thisTime.ToString("yyyy-MM-dd HH:mm:ss"), companyName);
            }
            catch
            {
                BathClass.printErrorMsg("打印出错，请重新打印!");
            }
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
                    exportTool_Click(null, null);
                    break;
                case Keys.F5:
                    printTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //查询
        private void toolFind_Click(object sender, EventArgs e)
        {
            if (!get_clear_table_time())
                return;

            dgv_show();
        }
    }
}
