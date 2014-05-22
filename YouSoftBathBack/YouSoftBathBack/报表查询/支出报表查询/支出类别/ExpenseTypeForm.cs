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
    public partial class ExpenseTypeForm : Form
    {
         //成员变量
        private BathDBDataContext db = null;
        private ExpenseType m_ExpenseType = new ExpenseType();
        private bool newExpenseType = true;

        //构造函数
        public ExpenseTypeForm(BathDBDataContext dc, ExpenseType curCat)
        {
            db = dc;
            if (curCat != null)
            {
                newExpenseType = false;
                m_ExpenseType = curCat;
            }

            InitializeComponent();
        }

        //对话框载入
        private void ExpenseTypeForm_Load(object sender, EventArgs e)
        {
            if (!newExpenseType)
            {
                name.Text = m_ExpenseType.name;
                this.Text = "编辑支出类别";
            }
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                GeneralClass.printErrorMsg("需要填入信息!");
                name.Focus();
                return;
            }

            m_ExpenseType.name = name.Text;

            if (newExpenseType)
                db.ExpenseType.InsertOnSubmit(m_ExpenseType);
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
        }

        //绑定快捷键
        private void ExpenseTypeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okBtn_Click(null, null);
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
