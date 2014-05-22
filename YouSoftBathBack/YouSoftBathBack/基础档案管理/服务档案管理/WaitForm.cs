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
    public partial class WaitForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private WaiterItem m_WaiterItem = new WaiterItem();
        private bool newWaiterItem = true;
        private WaitManagementForm m_rmForm = null;

        //构造函数
        public WaitForm(BathDBDataContext dc, WaiterItem room, WaitManagementForm rm)
        {
            db = dc;
            m_rmForm = rm;
            if (room != null)
            {
                newWaiterItem = false;
                m_WaiterItem = room;
            }
            InitializeComponent();
        }

        //对话框载入
        private void RoomForm_Load(object sender, EventArgs e)
        {
            if (!newWaiterItem)
            {
                name.Text = m_WaiterItem.name;
                note.Text = m_WaiterItem.note;
            }
        }

        //点击确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                name.Focus();
                GeneralClass.printErrorMsg("请输入项目名称");
                return;
            }
            m_WaiterItem.name = name.Text;
            m_WaiterItem.note = note.Text;

            if (newWaiterItem)
            {
                if (db.WaiterItem.FirstOrDefault(x => x.name == name.Text) != null)
                {
                    name.SelectAll();
                    name.Focus();
                    GeneralClass.printErrorMsg("已存在" + name.Text + "服务项目，不能重复添加");
                    return;
                }
                db.WaiterItem.InsertOnSubmit(m_WaiterItem);
                db.SubmitChanges();
                m_rmForm.dgv_show();

                m_WaiterItem = new WaiterItem();
                name.Text = "";
                note.Text = "";
                name.Focus();
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
                    btnOk_Click(null, null);
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
