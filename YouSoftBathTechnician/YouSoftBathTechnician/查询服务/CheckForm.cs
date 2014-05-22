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
    public partial class CheckForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Employee m_User;

        //构造函数
        public CheckForm(Employee user)
        {
            m_User = user;
            db = new BathDBDataContext(MainForm.connectionString);
            InitializeComponent();
            btnPwd.Text = "修改\n密码";
        }

        //对话框载入
        private void SeatForm_Load(object sender, EventArgs e)
        {
            var m_options = db.Options.FirstOrDefault();
            if (m_options == null)
                return;

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(ComboBox))
                {
                    var pro = m_options.GetType().GetProperty(c.Name);
                    if (pro == null)
                        continue;

                    var proVal = pro.GetValue(m_options, null);
                    if (proVal == null)
                        continue;

                    c.Text = proVal.ToString();
                }
            }
        }

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //日轮钟
        private void btnDayOn_Click(object sender, EventArgs e)
        {
            var form = new DayOnForm(m_User);
            form.ShowDialog();
        }

        //日点钟
        private void btnDayOrder_Click(object sender, EventArgs e)
        {
            DayOrderForm form = new DayOrderForm(m_User);
            form.ShowDialog();
        }

        //日退钟
        private void btnDayReturn_Click(object sender, EventArgs e)
        {
            var form = new DayReturnForm(m_User);
            form.ShowDialog();
        }

        //月轮钟
        private void btnMonthOn_Click(object sender, EventArgs e)
        {
            var form = new MonthOnForm(m_User);
            form.ShowDialog();
        }

        //月点钟
        private void btnMonthOrder_Click(object sender, EventArgs e)
        {
            var form = new MonthOrderForm(m_User);
            form.ShowDialog();
        }

        //月退钟
        private void btnMonthReturn_Click(object sender, EventArgs e)
        {
            var form = new MonthReturnForm(m_User);
            form.ShowDialog();
        }

        //修改密码
        private void btnPwd_Click(object sender, EventArgs e)
        {
            var form = new ModifyPwdForm(MainForm.connectionString, m_User);
            form.ShowDialog();
        }

    }
}
