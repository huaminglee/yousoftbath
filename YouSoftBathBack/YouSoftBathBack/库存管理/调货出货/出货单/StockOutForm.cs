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
    public partial class StockOutForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public StockOutForm(BathDBDataContext dc)
        {
            db = dc;
            InitializeComponent();

            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            goodsCat.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());

            var employees = db.Employee.Where(x => !db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            receiver.Items.AddRange(employees.Select(x => x.name).ToArray());
            checker.Items.AddRange(employees.Select(x => x.name).ToArray());
            transactor.Items.AddRange(employees.Select(x => x.name).ToArray());
            ComboUnit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {

            receiver.Text = LogIn.m_User.name;
            checker.Text = LogIn.m_User.name;
            transactor.Text = LogIn.m_User.name;
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (!validateTextFields())
                return;

            StockOut outStock = new StockOut();
            outStock.name = name.Text;
            outStock.amount = Convert.ToDouble(amount.Text);
            outStock.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            outStock.note = note.Text;
            outStock.date = BathClass.Now(LogIn.connectionString);
            outStock.receiver = receiver.Text;
            outStock.transactor = transactor.Text;
            outStock.checker = checker.Text;
            db.StockOut.InsertOnSubmit(outStock);

            string unit_txt = ComboUnit.Text.Trim();
            if (unit_txt != "")
            {
                outStock.unit = unit_txt;
                if (!db.Unit.Any(x => x.name == unit_txt))
                {
                    var unit_instance = new Unit();
                    unit_instance.name = unit_txt;
                    db.Unit.InsertOnSubmit(unit_instance);
                }
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
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private bool validateTextFields()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() != typeof(Label) && c.Name != "toStock" && c.Name != "note" && c.Text == "")
                {
                    c.Focus();
                    GeneralClass.printErrorMsg("需要输入内容");
                    return false;
                }
            }
            return true;
        }

        private void amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void amount_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void goodsCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            name.Items.Clear();
            var goods_cat = db.GoodsCat.FirstOrDefault(x => x.name == goodsCat.Text);
            if (goods_cat == null)
            {
                name.Enabled = false;
                return;
            }

            name.Enabled = true;
            name.Items.AddRange(db.StorageList.Where(x => x.goodsCatId == goods_cat.id).Select(x => x.name).ToArray());
        }
    }
}
