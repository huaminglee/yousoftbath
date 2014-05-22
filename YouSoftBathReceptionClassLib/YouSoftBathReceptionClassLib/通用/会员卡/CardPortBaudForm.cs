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
using YouSoftUtil;
using YouSoftBathConstants;

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
            IOUtil.set_config_by_key(ConfigKeys.KEY_CARD_PORT, card_port);
            IOUtil.set_config_by_key(ConfigKeys.KEY_CARD_BAUD, card_baud);
            if (noHint.Checked)
                IOUtil.set_config_by_key(ConfigKeys.KEY_CARD_NOHINT, "true");
            else
                IOUtil.set_config_by_key(ConfigKeys.KEY_CARD_NOHINT, "false");
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
