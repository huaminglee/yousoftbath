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
    public partial class ProviderPaysForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private ProviderPays m_providerPays = new ProviderPays();
        private Provider m_provider;

        //构造函数
        public ProviderPaysForm(BathDBDataContext dc, Provider provider)
        {
            db = dc;
            m_provider = provider;
            InitializeComponent();
        }

        //对话框载入
        private void SeatTypeForm_Load(object sender, EventArgs e)
        {
            name.Text = m_provider.name;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_providerPays.providerId = m_provider.id;
            try
            {
                m_providerPays.cash = Convert.ToDouble(cash.Text.Trim());
            }
            catch
            {
                m_providerPays.cash = null;
            }

            try
            {
                m_providerPays.bank = Convert.ToDouble(bank.Text.Trim());
            }
            catch
            {
                m_providerPays.bank = null;
            }

            if (m_providerPays.cash==null&&m_providerPays.bank==null)
            {
                BathClass.printErrorMsg("需要输入至少现金或者银联付款信息");
                return;
            }

            if (payer.Text.Trim()=="")
            {
                BathClass.printErrorMsg("需要输入付款人信息");
                payer.Focus();
                return;
            }
            m_providerPays.payer = payer.Text.Trim();

            if (confirmer.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入确认人信息");
                confirmer.Focus();
                return;
            }
            m_providerPays.confirmer = confirmer.Text.Trim();

            if (receiver.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入签收人信息");
                receiver.Focus();
                return;
            }
            m_providerPays.receiver = receiver.Text.Trim();
            m_providerPays.date = DateTime.Now;
            m_providerPays.note = note.Text.Trim();

            db.ProviderPays.InsertOnSubmit(m_providerPays);
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
