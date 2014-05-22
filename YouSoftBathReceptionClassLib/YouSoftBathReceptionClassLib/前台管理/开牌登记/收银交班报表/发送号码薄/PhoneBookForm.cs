using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YouSoftBathGeneralClass;
using YouSoftUtil;
using YouSoftBathConstants;

namespace YouSoftBathReception
{
    public partial class PhoneBookForm : Form
    {
        //构造函数
        public PhoneBookForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void ClearOptionsForm_Load(object sender, EventArgs e)
        {
            TextPhones.Text = string.Join("\r\n", IOUtil.read_file(ConfigKeys.KEY_PHONES_FILE).ToArray());
            if (TextPhones.Text.Trim() != "")
                TextPhones.Text += "\r\n";
            TextPhones.SelectionStart = TextPhones.Text.Length;
        }

        //确定
        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (File.Exists(ConfigKeys.KEY_PHONES_FILE))
                File.Delete(ConfigKeys.KEY_PHONES_FILE);

            string[] phones = TextPhones.Text.Split('\n');
            foreach (var phone in phones)
            {
                string val = phone.Trim('\r');
                IOUtil.insert_file(val, ConfigKeys.KEY_PHONES_FILE);
            }

            this.Close();
        }
    }
}
