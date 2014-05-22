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
using System.Threading;

namespace YouSoftBathBack
{
    public partial class AdvanceOffForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Thread m_thread;
        private Thread m_thread_details;
        private DateTime lastTime;//起始时间
        private DateTime thisTime;//终止时间
        private bool input_id;//输入单据号
        private int time_delay;//提前下钟时限

        //构造函数
        public AdvanceOffForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void BonusTableForm_Load(object sender, EventArgs e)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            startDate.Value = DateTime.Now.AddDays(-1);
            endDate.Value = DateTime.Now.AddDays(-1);
            catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            menu.Items.AddRange(db.Menu.Where(x=>x.technician).Select(x => x.name).ToArray());

            var ops = db.Options.FirstOrDefault();
            input_id = MConvert<bool>.ToTypeOrDefault(ops.录单输入单据编号, false);
            time_delay = MConvert<int>.ToTypeOrDefault(ops.下钟提醒, 10);
            dgvDetails.Columns[0].Visible = input_id;
        }

        //提成下钟统计
        private void dgv1_show()
        {
            var dc = new BathDBDataContext(LogIn.connectionString);
            var his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
            var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);

            his_orders = his_orders.Where(x => x.technician != null);
            orderLst = orderLst.Where(x => x.technician != null);

            if (cboxSeat.Checked)
            {
                his_orders = his_orders.Where(x => x.technician == seat.Text);
                orderLst = orderLst.Where(x => x.technician == seat.Text);
            }
            if (cBoxCatgory.Checked && catgory.Text != "")
            {
                var catgoryId = dc.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                var menus = dc.Menu.Where(x => x.technician && x.catgoryId == catgoryId).Select(x => x.name);
                orderLst = orderLst.Where(x => menus.Contains(x.menu));
                his_orders = his_orders.Where(x => menus.Contains(x.menu));
            }
            if (cboxMenu.Checked)
            {
                orderLst = orderLst.Where(x => x.menu == menu.Text);
                his_orders = his_orders.Where(x => x.menu == menu.Text);
            }

            double total = 0; //总金额
            double off = 0; //总提成
            double no = 0;
            var unpaid_techList = orderLst.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
            var his_techList = his_orders.OrderBy(x => x.technician).Select(x => x.technician).Distinct();
            var techList = unpaid_techList.Union(his_techList).Distinct();
            foreach (string techId in techList)
            {
                double tech_total = 0;
                double tech_off = 0;
                double tech_no = 0;

                var unpaid_tech_orders = orderLst.Where(x => x.technician == techId);
                var unpaid_tech_menus = unpaid_tech_orders.Select(x => x.menu).Distinct();

                var his_tech_orders = his_orders.Where(x => x.technician == techId);
                var his_tech_menus = his_orders.Select(x => x.menu).Distinct();
                var menus = unpaid_tech_menus.Union(his_tech_menus).Distinct().ToList();
                foreach (var m in menus)
                {
                    double tech_menu_total = 0;//技师项目总数量
                    double tech_menu_off = 0;//技师项目提前下钟数量
                    double tech_menu_no = 0;//技师项目无上钟时间数量

                    var tech_menu = dc.Menu.FirstOrDefault(x => x.name == m);
                    if (tech_menu == null ||
                        (tech_menu.timeLimitHour == null && tech_menu.timeLimitMiniute == null))
                        continue;

                    int tech_menu_limits = 0;
                    if (tech_menu.timeLimitHour != null)
                        tech_menu_limits += tech_menu.timeLimitHour.Value * 60;
                    if (tech_menu.timeLimitMiniute != null)
                        tech_menu_limits += tech_menu.timeLimitMiniute.Value;

                    var unpaid_tech_menu_orders = unpaid_tech_orders.Where(x => x.menu == m).ToList();
                    var his_tech_menu_orders = his_tech_orders.Where(x => x.menu == m).ToList();
                    if (!unpaid_tech_menu_orders.Any() && !his_tech_menu_orders.Any())
                        continue;

                    tech_menu_total = unpaid_tech_menu_orders.Sum(x => x.number) + his_tech_menu_orders.Sum(x => x.number);

                    var no_unpaid_tech_menu_orders = unpaid_tech_menu_orders.Where(x => x.startTime == null);
                    tech_menu_no += no_unpaid_tech_menu_orders.Sum(x => x.number);

                    var off_unpaid_tech_menu_orders = unpaid_tech_menu_orders.Where(x =>x.startTime != null && (x.inputTime - x.startTime.Value).TotalMinutes < tech_menu_limits - time_delay);
                    if (off_unpaid_tech_menu_orders.Any())
                        tech_menu_off += off_unpaid_tech_menu_orders.Sum(x => x.number);

                    var no_his_tech_menu_orders = his_tech_menu_orders.Where(x => x.startTime == null);
                    tech_menu_no += no_his_tech_menu_orders.Sum(x => x.number);

                    var off_his_tech_menu_orders = his_tech_menu_orders.Where(x => x.startTime != null && (x.inputTime - x.startTime.Value).TotalMinutes < tech_menu_limits - time_delay);
                    if (off_his_tech_menu_orders.Any())
                        tech_menu_off += off_his_tech_menu_orders.Sum(x => x.number);

                    double tech_menu_ratio = 0;
                    if (tech_menu_total != 0)
                        tech_menu_ratio = tech_menu_off / tech_menu_total;
                    string[] objs = {techId, m, tech_menu_total.ToString(), tech_menu_no.ToString(), 
                                        tech_menu_off.ToString(), 
                                        tech_menu_ratio.ToString("p") };
                    this.Invoke(new delegate_add_row(add_row), (Object)objs);

                    tech_total += tech_menu_total;
                    tech_off += tech_menu_off;
                    tech_no += tech_menu_no;

                }

                total += tech_total;
                off += tech_off;
                no += tech_no;
                double tech_ratio = 0;
                if (tech_total != 0)
                    tech_ratio = tech_off / tech_total;
                string[] tech_objs = { "", "", tech_total.ToString(), tech_no.ToString(), tech_off.ToString(), tech_ratio.ToString("p") };
                this.Invoke(new delegate_add_row(add_row), (Object)tech_objs);
                this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightSkyBlue);
            }

            double ratio = 0;
            if (total != 0)
                ratio = off / total;
            string[] total_objs = { "", "", total.ToString(), no.ToString(), off.ToString(), ratio.ToString("p") };
            this.Invoke(new delegate_add_row(add_row), (Object)total_objs);
            this.Invoke(new delegate_change_dgv_color(change_dgv_color), Color.LightBlue);
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);
        private delegate void delegate_no_para();
        private delegate void delegate_change_dgv_color(Color color);
        private void change_dgv_color(Color color)
        {
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = color;
        }

        private delegate void delegate_add_row(string[] vals);
        private void add_row(string[] vals)
        {
            dgv.Rows.Add(vals);
        }

        private delegate void delegate_add_row_details(object[] vals);
        private void add_row_details(object[] vals)
        {
            dgvDetails.Rows.Add(vals);
        }


        //获取夜审时间
        private bool get_clear_table_time()
        {
            var lct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == startDate.Value.Date);
            if (lct == null)
            {
                lct = db.ClearTable.Where(x=>x.clearTime<startDate.Value).OrderByDescending(x => x.clearTime).FirstOrDefault();
                if (lct == null)
                    lastTime = DateTime.Parse("2013-01-01");
                else
                    lastTime = lct.clearTime;
            }
            else
                lastTime = lct.clearTime;

            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == endDate.Value.AddDays(1).Date);
            if (ct == null)
            {
                GeneralClass.printErrorMsg("没有夜审，不能查询");
                return false;
            }

            thisTime = ct.clearTime;

            return true;
        }

        //设置列可见
        private void set_columns_invisible()
        {
            dgv.Columns[5].Visible = false;
            dgv.Columns[6].Visible = false;
            dgv.Columns[7].Visible = false;
            dgv.Columns[8].Visible = false;
            dgv.Columns[9].Visible = false;
            dgv.Columns[10].Visible = false;
            dgv.Columns[11].Visible = false;
        }

        //显示详细订单信息
        private void dgvDetails_show()
        {
            if (dgv.CurrentCell == null)
                return;

            string techId = dgv.CurrentRow.Cells[0].Value.ToString();
            string menu_name = dgv.CurrentRow.Cells[1].Value.ToString();
            if (techId == "" || menu_name == "")
                return;

            var dc = new BathDBDataContext(LogIn.connectionString);
            var tech_menu = dc.Menu.FirstOrDefault(x => x.name == menu_name);
            if (tech_menu == null ||
                (tech_menu.timeLimitHour == null && tech_menu.timeLimitMiniute == null))
                return;
            int tech_menu_limits = 0;
            if (tech_menu.timeLimitHour != null)
                tech_menu_limits += tech_menu.timeLimitHour.Value * 60;
            if (tech_menu.timeLimitMiniute != null)
                tech_menu_limits += tech_menu.timeLimitMiniute.Value;

            var his_orders = dc.HisOrders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
            var orderLst = dc.Orders.Where(x => x.inputTime >= lastTime && x.inputTime <= thisTime && x.deleteEmployee == null);
            orderLst = orderLst.Where(x => x.technician != null);
            his_orders = his_orders.Where(x => x.technician != null);

            orderLst = orderLst.Where(x => x.technician == techId && x.menu == menu_name);
            his_orders = his_orders.Where(x => x.technician == techId && x.menu == menu_name);

            foreach (var o in orderLst)
            {
                bool off = false;
                if (o.startTime != null)
                    off = (o.inputTime - o.startTime.Value).TotalMinutes < tech_menu_limits;
                object[] row = { o.billId, off, menu_name, o.text, o.number, o.money, o.techType, o.startTime, o.inputTime };

                this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
            }

            foreach (var o in his_orders)
            {
                bool off = false;
                if (o.startTime != null)
                    off = (o.inputTime - o.startTime.Value).TotalMinutes < tech_menu_limits;
                object[] row = { o.billId, off, menu_name, o.text, o.number, o.money, o.techType, o.startTime, o.inputTime };
                this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
            }
        }

        //服务员
        private void cboxSeat_CheckedChanged(object sender, EventArgs e)
        {
            seat.ReadOnly = !cboxSeat.Checked;
        }

        //项目类别
        private void cBoxCatgory_CheckedChanged(object sender, EventArgs e)
        {
            catgory.Enabled = cBoxCatgory.Checked;
        }

        //项目编码
        private void cboxMenu_CheckedChanged(object sender, EventArgs e)
        {
            menu.Enabled = cboxMenu.Checked;
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            if (!get_clear_table_time())
                return;

            dgv.Rows.Clear();
            m_thread = new Thread(new ThreadStart(dgv1_show));
            m_thread.Start();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            ExportToExcel.ExportExcel("技师对账单 " + startDate.Value.ToString("yyyy-MM-dd"), dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            PrintDGV.Print_DataGridView(dgv, "提成统计", false, "作业时间:" + startDate.Text);
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
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
                    dgv1_show();
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "提成统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void seat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetails.Rows.Clear();
            if (dgv.CurrentCell == null || dgv.CurrentRow.Cells[0].Value.ToString() == "")
                return;

            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            m_thread_details = new Thread(new ThreadStart(dgvDetails_show));
            m_thread_details.Start();
        }

        private void BonusTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }
    }
}
