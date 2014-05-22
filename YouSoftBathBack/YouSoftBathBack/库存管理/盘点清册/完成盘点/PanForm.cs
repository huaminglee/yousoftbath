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
    public partial class PanForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private double amount_p;

        //构造函数
        public PanForm(BathDBDataContext dc, string _name, string _stock, double _amount)
        {
            db = dc;
            amount_p = _amount;
            InitializeComponent();
            name.Text = _name;
            stock.Text = _stock;
            amount.Text = _amount.ToString();
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            var employees = db.Employee.Where(x => !db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            paner.Items.AddRange(employees.Select(x => x.name).ToArray());
            paner.Text = LogIn.m_User.name;

            amount.SelectAll();
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            string _amount = amount.Text.Trim();
            string _paner = paner.Text;
            string _note = note.Text.Trim();
            if (_amount == "")
            {
                BathClass.printErrorMsg("需要填入实际库存!");
                return;
            }

            if (_paner == "")
            {
                BathClass.printErrorMsg("需要选择盘点员工!");
                return;
            }

            var p = new Pan();
            p.name = name.Text;
            p.amount = Convert.ToDouble(_amount) - amount_p;
            p.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            p.date = DateTime.Now;
            p.paner = _paner;
            p.note = _note;
            db.Pan.InsertOnSubmit(p);
            db.SubmitChanges();
            this.Close();
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

        private void amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(amount, e);
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void amount_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }


    }
}
