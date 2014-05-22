using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class ClearOptionsForm : Form
    {
        //成员变量
        DateTime m_dt;

        //构造函数
        public ClearOptionsForm(DateTime dt)
        {
            m_dt = dt;
            InitializeComponent();
        }

        //对话框载入
        private void ClearOptionsForm_Load(object sender, EventArgs e)
        {
            time.Text = m_dt.ToShortTimeString();
        }

        //放弃本次夜审
        private void btnAbandon_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //删除上次夜审
        private void btnDel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
