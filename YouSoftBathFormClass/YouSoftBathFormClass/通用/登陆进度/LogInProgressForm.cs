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
    public partial class LogInProgressForm : Form
    {
        private bool _close = false;
        private LogIn m_form;
        public LogInProgressForm(LogIn form)
        {
            m_form = form;
            InitializeComponent();
            //this.Text = title;
            //
            Thread m_thread = new Thread(new ThreadStart(update_text_thread));
            m_thread.IsBackground = true;
            m_thread.Start();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            
        }

        private void update_text_thread()
        {
            while (true)
            {
                if (_close) break;
                try
                {
                    this.Invoke(new update_text_delegate(update_text));
                }
                catch
                {
                }
            }
        }

        private delegate void update_text_delegate();
        private void update_text()
        {
            lb_infor.Text = m_form.work_progress;
            int fw = this.Width;
            int lb_w = lb_infor.Width;
            lb_infor.Location = new Point((fw - lb_w) / 2, lb_infor.Location.Y);
        }

        private void ProgressForm_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _close = true;
        }
    }
}
