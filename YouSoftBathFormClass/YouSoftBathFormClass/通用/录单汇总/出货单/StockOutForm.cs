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
    public partial class StockOutForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public StockOutForm(BathDBDataContext dc)
        {
            db = dc;
            InitializeComponent();
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            toStock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            
            receiver.Items.AddRange(db.Employee.Select(x => x.name).ToArray());
            checker.Items.AddRange(db.Employee.Select(x => x.name).ToArray());
            transactor.Items.AddRange(db.Employee.Select(x => x.name).ToArray());
            id.Text = db.StockIn.Count().ToString();
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (!validateTextFields())
                return;

            StockOut outStock = new StockOut();
            outStock.name = name.Text;
            outStock.amount = Convert.ToInt32(amount.Text);
            outStock.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;

            if (toStock.Text != "")
                outStock.toStockId = db.Stock.FirstOrDefault(x => x.name == toStock.Text).id;
            
            outStock.note = note.Text;
            outStock.date = BathClass.Now(LogIn.connectionString);
            outStock.receiver = receiver.Text;
            outStock.transactor = transactor.Text;
            outStock.checker = checker.Text;
            db.StockOut.InsertOnSubmit(outStock);

            //bool newSList = false;
            //var storage = db.StorageList.FirstOrDefault(x => x.name == name.Text && x.stockId == outStock.stockId);
            //var toStorage = db.StorageList.FirstOrDefault(x => x.name == name.Text && x.stockId == outStock.toStockId);
            //if (storage == null)
            //{
            //    newSList = true;
            //    storage = new StorageList();
            //}

            //bool newToSList = false;
            //if (toStorage == null)
            //{
            //    newToSList = true;
            //    toStorage = new StorageList();
            //}

            //storage.name = name.Text;
            //storage.amountThisMonth = MConvert<int>.ToTypeOrDefault(storage.amountThisMonth) - outStock.amount;
            //storage.stockId = outStock.stockId;
            //if (newSList)
            //    db.StorageList.InsertOnSubmit(storage);

            //toStorage.name = name.Text;
            //toStorage.amountThisMonth = MConvert<int>.ToTypeOrDefault(toStorage.amountThisMonth) + outStock.amount;
            //toStorage.stockId = outStock.toStockId;
            //if (newToSList)
            //    db.StorageList.InsertOnSubmit(toStorage);

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

        private bool validateTextFields()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c.GetType() != typeof(Label) && c.Name != "toStock" && c.Name != "note" && c.Text == "")
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
    }
}
