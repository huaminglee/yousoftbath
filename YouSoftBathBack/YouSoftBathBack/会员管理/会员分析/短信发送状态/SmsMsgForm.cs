using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class SmsMsgForm : Form
    {
        private MemberAnalysisForm  m_form;
        //构造函数
        public SmsMsgForm(MemberAnalysisForm form)
        {
            m_form = form;
            InitializeComponent();
        }

        //对话框载入
        private void MemberSettingForm_Load(object sender, EventArgs e)
        {
            int w = Screen.GetWorkingArea(this).Width;
            this.Location = new Point(w-this.Width, 0);
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        public void ListMsg_Add_Msg(string msg)
        {
            ListMsg.Items.Add(msg);
            ListMsg.SelectedIndex = ListMsg.Items.Count - 1;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SmsMsgForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_form.stop_falg = true;
        }
    }
}
