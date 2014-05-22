using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Threading;
using System.Timers;

using Microsoft.Win32;

namespace YouSoftBathWatcher
{
    public partial class ExitWindow : Form
    {
        public ExitWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (exitPwd.Text == "0")
                this.DialogResult = DialogResult.OK;
            else
            {
                exitPwd.SelectAll();
                MessageBox.Show("退出密码不正确，请重试！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void ExitWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                btnCancel_Click(null, null);
        }

        private void exitPwd_Enter(object sender, EventArgs e)
        {
            // 如果只有一种输入法，什么也不做
            if (InputLanguage.InstalledInputLanguages.Count == 1)
                return;

            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                if (iL.LayoutName.Contains("美式键盘"))
                {
                    InputLanguage.CurrentInputLanguage = iL;
                    break;
                }
            }
        }

    }
}
