using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathFormClass
{
    public partial class DeleteExplainForm : Form
    {
        public string txt
        {
            get { return text.Text; }
        }

        //构造函数
        public DeleteExplainForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void NoteForm_Load(object sender, EventArgs e)
        {

        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (text.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入退单原因!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void text_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
