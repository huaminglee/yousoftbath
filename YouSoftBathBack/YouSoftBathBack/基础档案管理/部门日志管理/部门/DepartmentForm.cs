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
    public partial class DepartmentForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Department m_Department = new Department();
        private bool newDepartment = true;
        private DepartLogMgForm m_form;

        //构造函数
        public DepartmentForm(BathDBDataContext dc, Department department, DepartLogMgForm form)
        {
            db = dc;
            m_form = form;
            if (department != null)
            {
                newDepartment = false;
                m_Department = department;
            }

            InitializeComponent();
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            if (!newDepartment)
            {
                TextName.Text = m_Department.name;
                TextNote.Text = m_Department.note;
                TextName.SelectAll();
                TextName.Focus();
            }
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (TextName.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入部门名称");
                TextName.SelectAll();
                TextName.Focus();
                return;
            }

            m_Department.name = TextName.Text.Trim();

            if (TextNote.Text.Trim() != "")
                m_Department.note = TextNote.Text.Trim();

            if (newDepartment)
            {
                if (db.Department.FirstOrDefault(x => x.name == TextName.Text) != null)
                {
                    BathClass.printErrorMsg("已经存在此名称的部门");
                    TextName.SelectAll();
                    TextName.Focus();
                    return;
                }

                db.Department.InsertOnSubmit(m_Department);
                db.SubmitChanges();

                m_form.createTree();
                m_Department = new Department();
                TextName.Text = "";
                TextNote.Text = "";
                TextName.Focus();
            }
            else
            {
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
