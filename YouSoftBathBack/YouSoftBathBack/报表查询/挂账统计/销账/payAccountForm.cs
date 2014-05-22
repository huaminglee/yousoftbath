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

namespace YouSoftBathBack
{
    public partial class payAccountForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Customer m_customer = null;
        private CustomerPays m_customerPays = new CustomerPays();

        //构造函数
        public payAccountForm(BathDBDataContext dc, Customer _customer)
        {
            db = dc;
            m_customer = _customer;
            InitializeComponent();
        }

        //对话框载入
        private void MemberSettingForm_Load(object sender, EventArgs e)
        {
            if (m_customer != null)
            {
                cus_name.Text = m_customer.name;
                cus_tel.Text = m_customer.mobile;
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

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (m_customer == null)
            {
                BathClass.printErrorMsg("需要输入挂账客户!");
                return;
            }

            m_customerPays.customerId = m_customer.id;
            m_customerPays.date = DateTime.Now;
            m_customerPays.note = note.Text;
            m_customerPays.payEmployee = LogIn.m_User.id;
            
            try
            {
                m_customerPays.cash = Convert.ToDouble(cash.Text.Trim());
            }
            catch
            {
                m_customerPays.cash = null;
            }

            try
            {
                m_customerPays.bank = Convert.ToDouble(bank.Text.Trim());
            }
            catch
            {
                m_customerPays.bank = null;
            }

            if (m_customerPays.cash==null && m_customerPays.bank==null)
            {
                BathClass.printErrorMsg("需要输入至少现金或者银联付款信息");
                return;
            }

            db.CustomerPays.InsertOnSubmit(m_customerPays);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void cus_pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }
    }
}
