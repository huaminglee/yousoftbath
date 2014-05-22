using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace YouSoftBathFormClass
{
    public partial class ProgressForm : Form
    {
        private bool _close = false;
        public ProgressForm(string title, string infor)
        {
            InitializeComponent();
            this.Text = title;
            lb_infor.Text = infor;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            //int fw = this.Width;
            //int lb_w = lb_infor.Width;
            //lb_infor.Location = new Point((fw - lb_w) / 2, lb_infor.Location.Y);

            pBar.Maximum = 10;
            Thread m_thread = new Thread(new ThreadStart(update_pBar));
            m_thread.Start();
        }

        private void update_pBar()
        {
            bool grow = true;
            while (true)
            {
                if (_close) break;
                if (pBar.Value == pBar.Maximum)
                    grow = false;
                else if (pBar.Value == 0)
                    grow = true;

                this.Invoke(new set_pBar_delegate(set_pBar), new object[]{grow});
                Thread.Sleep(1000);
            }
        }

        private delegate void set_pBar_delegate(bool grow);
        private void set_pBar(bool grow)
        {
            if (grow)
                pBar.Value += 1;
            else
                pBar.Value -= 1;
        }

        private void ProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("工作完成!");
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _close = true;
        }
    }
}
