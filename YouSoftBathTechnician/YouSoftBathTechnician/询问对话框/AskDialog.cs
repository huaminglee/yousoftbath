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
    public partial class AskDialog : Form
    {
        public AskDialog(string msg)
        {
            InitializeComponent();
            lmsg.Text = msg;
        }

        private void AskDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
