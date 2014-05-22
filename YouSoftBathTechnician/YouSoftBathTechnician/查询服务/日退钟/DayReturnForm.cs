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
    public partial class DayReturnForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime m_startTime, m_endTime;
        private bool details = true;
        private Employee m_User;

        //构造函数
        public DayReturnForm(Employee user)
        {
            m_User = user;
            db = new BathDBDataContext(MainForm.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void TableItemsForm_Load(object sender, EventArgs e)
        {
            date.Text = DateTime.Now.ToString("yyyy年MM月dd日");

            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 20);
            dgv.RowTemplate.Height = 40;
            dgv_show();
        }

        //获取时间段，根据夜审的时间获取
        private void get_timeSpan()
        {
            var date_sel = Convert.ToDateTime(date.Text);
            var cl = db.ClearTable.Where(x => x.clearTime.Date >= date_sel.Date).OrderBy(x => x.clearTime).FirstOrDefault();
            if (cl == null)
            {
                if (db.ClearTable.Any())
                    m_startTime = db.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault().clearTime;
                else
                    m_startTime = DateTime.Parse("2013-01-01 00:00:00");
                m_endTime = DateTime.Now;
            }
            else
            {
                m_startTime = cl.clearTime;
                var clt = db.ClearTable.Where(x => x.clearTime > cl.clearTime).OrderBy(x => x.clearTime).FirstOrDefault();
                if (clt == null)
                    m_endTime = DateTime.Now;
                else
                    m_endTime = clt.clearTime;
            }
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

        //明细查询
        private void find_details()
        {
            var orders = db.TechReturn.Where(x => x.inputTime >= m_startTime && x.inputTime <= m_endTime && x.techId == m_User.id);
            dgv.DataSource = from x in orders
                             orderby x.inputTime
                             select new
                             {
                                 技师号 = x.techId,
                                 编号 = x.id,
                                 项目 = x.menu,
                                 输入时间 = x.inputTime,
                                 备注 = x.note
                             };
        }

        //汇总查询
        private void find_summery()
        {
            var orders = db.TechReturn.Where(x => x.inputTime >= m_startTime && x.inputTime <= m_endTime && x.techId == m_User.id);

            dgv.DataSource = from x in orders
                             group x by x.techId into xt
                             select new
                             {
                                 技师号 = xt.Key,
                                 数量 = xt.Count()
                             };
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
            var form = new DaySelectForm(date.Text);
            form.ShowDialog();

            date.Text = form.m_date.ToString("yyyy年MM月dd日");
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
