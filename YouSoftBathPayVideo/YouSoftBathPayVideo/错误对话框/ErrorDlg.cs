using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YouSoftBathPayVideo
{
    public partial class ErrorDlg : Form
    {
        private string m_str;
        public ErrorDlg(string str)
        {
            m_str = str;
            InitializeComponent();
        }

        private void ErrorDlg_Load(object sender, EventArgs e)
        {
            msg.Text = m_str;
        }
    }
}
