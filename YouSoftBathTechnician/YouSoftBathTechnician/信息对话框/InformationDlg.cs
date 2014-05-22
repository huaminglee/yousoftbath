using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YouSoftBath
{
    public partial class InformationDlg : Form
    {
        public InformationDlg(string msg)
        {
            InitializeComponent();
            lmsg.Text = msg;
        }

        private void InformationDlg_Load(object sender, EventArgs e)
        {

        }

        private void InformationDlg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
