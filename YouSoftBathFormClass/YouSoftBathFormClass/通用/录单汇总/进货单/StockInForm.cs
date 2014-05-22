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
    public partial class StockInForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public StockInForm(BathDBDataContext dc)
        {
            db = dc;
            InitializeComponent();
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            checker.Items.AddRange(db.Employee.Select(x => x.name).ToArray());
            transactor.Items.AddRange(db.Employee.Select(x => x.name).ToArray());
            id.Text = db.StockIn.Count().ToString();
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (!validateTextFields())
                return;

            StockIn inStock = new StockIn();
            inStock.name = name.Text;
            inStock.cost = Convert.ToDouble(cost.Text);
            inStock.amount = Convert.ToInt32(amount.Value);
            inStock.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            inStock.note = note.Text;
            inStock.date = BathClass.Now(LogIn.connectionString);
            inStock.transactor = transactor.Text;
            inStock.checker = checker.Text;
            db.StockIn.InsertOnSubmit(inStock);

            //bool newSList = false;
            //var storage = db.StorageList.FirstOrDefault(x => x.name == name.Text && x.stockId == inStock.stockId);
            //if (storage == null)
            //{
            //    newSList = true;
            //    storage = new StorageList();
            //}

            //storage.name = name.Text;
            //storage.cost = inStock.cost;
            //storage.amountThisMonth = MConvert<int>.ToTypeOrDefault(storage.amountThisMonth) + inStock.amount;
            //storage.stockId = inStock.stockId;
            //if (newSList)
            //    db.StorageList.InsertOnSubmit(storage);

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
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

        //只允许输入小数
        private void cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(cost, e);
        }

        private bool validateTextFields()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() != typeof(Label) && c.Name != "cost" && c.Name != "note" && c.Text == "")
                {
                    c.Focus();
                    BathClass.printErrorMsg("需要输入内容");
                    return false;
                }
            }
            return true;
        }

        //查找对应的类别
        private void name_TextChanged(object sender, EventArgs e)
        {
            var menu = db.Menu.FirstOrDefault(x => x.name == name.Text);
            if (menu == null)
                return;

            catgory.Text = db.Catgory.FirstOrDefault(x => x.id == menu.catgoryId).name;
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void cost_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
