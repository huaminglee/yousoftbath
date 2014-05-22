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
    public partial class CustomerForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Customer m_Customer = new Customer();
        private bool newCustomer = true;

        //构造函数
        public CustomerForm(BathDBDataContext dc, Customer customer)
        {
            db = dc;
            if (customer != null)
            {
                newCustomer = false;
                m_Customer = customer;
            }
            InitializeComponent();
        }

        //对话框载入
        private void CustomerForm_Load(object sender, EventArgs e)
        {
            id.Text = (db.Customer.Count() + 1).ToString();
            if (!newCustomer)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        var proVal = m_Customer.GetType().GetProperty(c.Name).GetValue(m_Customer, null);
                        if (proVal == null)
                            continue;
                        c.Text = proVal.ToString();
                    }
                }
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || mobile.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入必填信息");
                return;
            }

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    if (c.Text != "")
                    {
                        var pro = m_Customer.GetType().GetProperty(c.Name);
                        var proVal = TypeDescriptor.GetConverter(pro.PropertyType).ConvertFrom(c.Text);
                        pro.SetValue(m_Customer, proVal, null);
                    }
                }
            }
            if (newCustomer)
            {
                m_Customer.registerDate = BathClass.Now(LogIn.connectionString);
                db.Customer.InsertOnSubmit(m_Customer);
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

        private void mobile_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
