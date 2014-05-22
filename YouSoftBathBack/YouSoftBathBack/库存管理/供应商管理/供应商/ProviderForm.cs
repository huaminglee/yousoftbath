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
    public partial class ProviderForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Provider m_provider = new Provider();
        private bool newProvider = true;

        //构造函数
        public ProviderForm(BathDBDataContext dc, Provider provider)
        {
            db = dc;
            if (provider != null)
            {
                newProvider = false;
                m_provider = provider;
            }
            InitializeComponent();
        }

        //对话框载入
        private void SeatTypeForm_Load(object sender, EventArgs e)
        {
            if (!newProvider)
            {
                name.Text = m_provider.name;
                contactor.Text = m_provider.contactor;
                tel.Text = m_provider.tel;
                mobile.Text = m_provider.mobile;
                address.Text = m_provider.address;
                note.Text = m_provider.note;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (name.Text.Trim()=="")
            {
                BathClass.printErrorMsg("需要填入供应商名称!");
                name.Focus();
                return;
            }
            m_provider.name = name.Text.Trim();

            if (contactor.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要填入供应商联系人!");
                contactor.Focus();
                return;
            }
            m_provider.contactor = contactor.Text.Trim();

            m_provider.tel = tel.Text.Trim();

            if (mobile.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要填入供应商手机号!");
                mobile.Focus();
                return;
            }
            m_provider.mobile = mobile.Text.Trim();

            m_provider.address = address.Text.Trim();
            m_provider.note = note.Text.Trim();

            if (newProvider)
            {
                if (db.Provider.FirstOrDefault(x=>x.name==m_provider.name) != null)
                {
                    BathClass.printErrorMsg("已经存在此名称的供应商!");
                    name.SelectAll();
                    return;
                }
                db.Provider.InsertOnSubmit(m_provider);
            }
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
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

        private void mobile_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
