using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YouSoftBathGeneralClass
{
    public partial class InformationDlg : Form
    {
        private string m_msg;
        private int rows = 1;

        public InformationDlg(string msg)
        {
            InitializeComponent();
            m_msg = msg;
            lmsg.Text = m_msg;
        }

        private void InformationDlg_Load(object sender, EventArgs e)
        {
            int fw = this.Width;
            int lb_w = lmsg.Width;
            lmsg.Location = new Point((fw - lb_w) / 2, lmsg.Location.Y);
        }

        private SizeF string_width()
        {
            Graphics g = this.CreateGraphics();
            g.PageUnit = GraphicsUnit.Pixel;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            SizeF sizeF = g.MeasureString("宋", new Font("宋体", 20F), 500, sf);

            return sizeF;
        }

        private void split_string()
        {
            int number = (int)((this.Width) / string_width().Width);

            while (rows*number < m_msg.Length)
            {
                m_msg = m_msg.Insert(number * rows, "\n");
                rows++;
            }
        }

        private void InformationDlg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
