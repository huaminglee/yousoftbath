using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using YouSoftBathGeneralClass;

namespace YouSoftBathReception
{
    public partial class CardPortBaudForm : Form
    {
        public string card_port;
        public string card_baud;

        public CardPortBaudForm()
        {
            InitializeComponent();
        }

        private void PrinterChooseForm_Load(object sender, EventArgs e)
        {
            port.SelectedIndex = baud.SelectedIndex = 0;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            card_port = port.SelectedIndex.ToString();
            card_baud = baud.Text;
            BathClass.set_config_by_key("card_port", card_port);
            BathClass.set_config_by_key("card_baud", card_baud);
            if (noHint.Checked)
                BathClass.set_config_by_key("no_hint", "true");
            else
                BathClass.set_config_by_key("no_hint", "false");
            this.DialogResult = DialogResult.OK;
        }

        private void PrinterChooseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }
    }
}
