using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;

namespace YouSoftBathTechnician
{
    public partial class MonthOnForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime m_startTime, m_endTime;
        private bool details = true;
        private Employee m_User;

        //构造函数
        public MonthOnForm(Employee user)
        {
            m_User = user;
            db = new BathDBDataContext(MainForm.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void TableItemsForm_Load(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("yyyy年MM月");

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 20);
            dgv.RowTemplate.Height = 40;
            dgv_show();
        }

        //获取时间段，根据夜审的时间获取
        private void get_timeSpan()
        {
            m_startTime = Convert.ToDateTime(date.Text + "1日");
            m_endTime = m_startTime.AddMonths(1).AddDays(-1);
        }

        private void dgv_show()
        {
            get_timeSpan();
            if (details)
                find_details();
            else
                find_summery();

            BathClass.set_dgv_fit(dgv);
        }

        private void find_details()
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();
            BathClass.add_cols_to_dgv(dgv, "技师号");
            BathClass.add_cols_to_dgv(dgv, "项目");
            BathClass.add_cols_to_dgv(dgv, "手牌号");
            BathClass.add_cols_to_dgv(dgv, "录入时间");
            BathClass.add_cols_to_dgv(dgv, "数量");

            var orders = db.Orders.Where(x => x.inputTime >= m_startTime && x.inputTime <= m_endTime);
            orders = orders.Where(x => x.technician != null && x.technician == m_User.id && x.deleteEmployee == null);
            orders = orders.Where(x => x.techType == null || x.techType == "上钟" || x.techType == "轮钟");
            foreach (var order in orders)
            {
                dgv.Rows.Add(order.technician, order.menu, order.text, order.inputTime.ToString("MM-dd HH:mm"), order.number);
            }

            var orders_paid = db.HisOrders.Where(x => x.inputTime >= m_startTime && x.inputTime <= m_endTime);
            orders_paid = orders_paid.Where(x => x.technician != null && x.technician == m_User.id && x.deleteEmployee == null);
            orders_paid = orders_paid.Where(x => x.techType == null || x.techType == "上钟" || x.techType == "轮钟");
            foreach (var order in orders_paid)
            {
                dgv.Rows.Add(order.technician, order.menu, order.text, order.inputTime.ToString("MM-dd HH:mm"), order.number);
            }
        }

        private void find_summery()
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();
            BathClass.add_cols_to_dgv(dgv, "技师号");
            BathClass.add_cols_to_dgv(dgv, "总数量");
            BathClass.add_cols_to_dgv(dgv, "未结账数量");
            BathClass.add_cols_to_dgv(dgv, "已结账数量");

            var orders = db.Orders.Where(x => x.inputTime >= m_startTime && x.inputTime <= m_endTime);
            orders = orders.Where(x => x.technician != null && x.technician == m_User.id && x.deleteEmployee == null);
            orders = orders.Where(x => x.techType == null || x.techType == "上钟" || x.techType == "轮钟");
            var tech_orders = orders.GroupBy(x => x.technician);

            var orders_paid = db.HisOrders.Where(x => x.inputTime >= m_startTime && x.inputTime <= m_endTime);
            orders_paid = orders_paid.Where(x => x.technician != null && x.technician == m_User.id && x.deleteEmployee == null);
            orders_paid = orders_paid.Where(x => x.techType == null || x.techType == "上钟" || x.techType == "轮钟");
            var tech_orders_paid = orders_paid.GroupBy(x => x.technician);

            foreach (var order in tech_orders)
            {
                var order_paid = tech_orders_paid.FirstOrDefault(x => x.Key == order.Key);
                var count_paid = order_paid.Sum(x => x.number);
                var count_unpaid = order.Sum(x => x.number);
                dgv.Rows.Add(order.Key, count_paid + count_unpaid, count_unpaid, count_paid);
            }
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        //选择日期
        private void btnDaySel_Click(object sender, EventArgs e)
        {
            var form = new MonthSelectForm(date.Text);
            form.ShowDialog();

            date.Text = form.m_date.ToString("yyyy年MM月");
            dgv_show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //上一页
        private void btnPageUp_Click(object sender, EventArgs e)
        {
            int rows_per_page = (dgv.Height - dgv.ColumnHeadersHeight) / 40;
            var row = dgv.Rows.Count;
            if (row < rows_per_page)
                return;
            row = dgv.CurrentCell.RowIndex;
            var col = dgv.CurrentCell.ColumnIndex;
            if (row < rows_per_page)
                dgv.CurrentCell = dgv[col, 0];
            else
                dgv.CurrentCell = dgv[col, row - rows_per_page];
        }

        //下一页
        private void btnPageDown_Click(object sender, EventArgs e)
        {
            int rows_per_page = (dgv.Height - dgv.ColumnHeadersHeight) / 40;
            var rows = dgv.Rows.Count;
            if (rows < rows_per_page)
                return;

            var row = dgv.CurrentCell.RowIndex;
            var col = dgv.CurrentCell.ColumnIndex;
            if (row + rows_per_page >= rows)
                dgv.CurrentCell = dgv[col, rows - 1];
            else
                dgv.CurrentCell = dgv[col, row + rows_per_page];
        }

        private void btnType_Click(object sender, EventArgs e)
        {
            if (details)
                btnType.Text = "明细";
            else
                btnType.Text = "汇总";

            details = !details;
            dgv_show();
        }
    }
}
