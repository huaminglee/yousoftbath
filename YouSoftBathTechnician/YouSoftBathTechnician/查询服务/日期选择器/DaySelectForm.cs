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
    public partial class DaySelectForm : Form
    {
        //成员变量
        public DateTime m_date;
        private TextBox current_box;

        //构造函数
        public DaySelectForm(string date)
        {
            m_date = Convert.ToDateTime(date);
            InitializeComponent();
        }

        //对话框载入
        private void SeatForm_Load(object sender, EventArgs e)
        {
            year.Text = m_date.Year.ToString();
            month.Text = m_date.Month.ToString().PadLeft(2, '0');
            day.Text = m_date.Day.ToString().PadLeft(2, '0');

            current_box = year;
        }

        //输入内容
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            if (current_box == null)
                return;

            Button btn = sender as Button;
            current_box.Focus();

            current_box.Text += btn.Text;
            current_box.SelectionStart = current_box.Text.Length;

        }

        private void box_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
            current_box = sender as TextBox;
        }

        //增加
        private void btnAddYear_Click(object sender, EventArgs e)
        {
            if (year.Text == "" || year.Text == DateTime.Now.Year.ToString())
                return;

            year.Text = (Convert.ToInt32(year.Text) + 1).ToString();
            year.SelectionStart = year.Text.Length;
        }

        //减少
        private void btnMinusYear_Click(object sender, EventArgs e)
        {
            if (year.Text == "" || year.Text == "00" || year.Text == "0")
                return;

            year.Text = (Convert.ToInt32(year.Text) - 1).ToString();
            year.SelectionStart = year.Text.Length;
        }

        //增加
        private void btnAddMonth_Click(object sender, EventArgs e)
        {
            if (month.Text == "" || month.Text == "12")
                return;

            month.Text = (Convert.ToInt32(month.Text) + 1).ToString();
            month.SelectionStart = month.Text.Length;
        }

        //减少
        private void btnMinusMonth_Click(object sender, EventArgs e)
        {
            if (month.Text == "01")
                return;

            month.Text = (Convert.ToInt32(month.Text) - 1).ToString();
            month.Text = month.Text.PadLeft(2, '0');
            month.SelectionStart = month.Text.Length;
        }

        //增加
        private void btnAddDay_Click(object sender, EventArgs e)
        {
            if (day.Text == "" || day.Text == "31")
                return;

            day.Text = (Convert.ToInt32(day.Text) + 1).ToString();
            day.SelectionStart = day.Text.Length;
        }

        //减少
        private void btnMinusDay_Click(object sender, EventArgs e)
        {
            if (day.Text == "01")
                return;

            day.Text = (Convert.ToInt32(day.Text) - 1).ToString();
            day.Text = day.Text.PadLeft(2, '0');
            day.SelectionStart = day.Text.Length;
        }

        //回删
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (current_box.Text == "")
                return;

            current_box.Text = current_box.Text.Substring(0, current_box.Text.Length - 1);
            current_box.SelectionStart = current_box.Text.Length;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_date = Convert.ToDateTime(year.Text + "年" + month.Text + "月" + day.Text + "日");
            this.DialogResult = DialogResult.OK;
        }

    }
}
