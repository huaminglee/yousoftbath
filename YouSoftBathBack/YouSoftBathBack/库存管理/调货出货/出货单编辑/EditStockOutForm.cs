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
    public partial class EditStockOutForm : Form
    {
        BathDBDataContext db = null;
        StockOut stockout = null;
        //private double totalsum = 0;
        public EditStockOutForm(BathDBDataContext dc, StockOut stkout)
        {
            db = dc;
            stockout = stkout;
            InitializeComponent();
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            goodsCat.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());
            var employees = db.Employee.Where(x => !db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            receiver.Items.AddRange(employees.Select(x => x.name).ToArray());
            checker.Items.AddRange(employees.Select(x => x.name).ToArray());
            transactor.Items.AddRange(employees.Select(x => x.name).ToArray());
            ComboUnit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
        }

        private void EditStockOutForm_Load(object sender, EventArgs e)
        {
            name.Text = stockout.name;
            amount.Text = stockout.amount.ToString();
            dtPickerIntoStock.Value = MConvert<DateTime>.ToTypeOrDefault(stockout.date, DateTime.Now);
            ComboUnit.Text = stockout.unit;
            transactor.Text = stockout.transactor;
            receiver.Text = stockout.receiver;
            checker.Text = stockout.checker;
            stock.Text = db.Stock.FirstOrDefault(x => x.id == stockout.stockId).name;           
            var goodcategory=db.GoodsCat.FirstOrDefault(y => y.id == db.StorageList.FirstOrDefault(x => x.name == stockout.name).goodsCatId);            
            if (goodcategory!=null)
            {
                string goodcattext = goodcategory.name;
                goodsCat.Text = goodcattext != "" ? goodcattext : "";
            }
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            validateTextFields();
            var goodcategory = db.GoodsCat.FirstOrDefault(y => y.id == db.StorageList.FirstOrDefault(x => x.name == stockout.name).goodsCatId);
            if (goodcategory != null)
            {
                string goodcattext = goodcategory.name;
                if (goodcattext == "")
                {
                    MessageBox.Show("该产品没有类别!");
                    goodsCat.Text = "";
                }
                else
                    goodsCat.Text = goodcattext;
                
            }
            stockout.name = name.Text;
            stockout.amount = Convert.ToDouble(amount.Text);
            stockout.date = dtPickerIntoStock.Value;
            stockout.unit = ComboUnit.Text;
            stockout.checker = checker.Text;
            stockout.transactor = transactor.Text;
            stockout.receiver = receiver.Text;
            stockout.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            stockout.note = note.Text;
            //stockout.date = DateTime.Now;
            db.SubmitChanges();
            //MessageBox.Show("修改成功!");
            //this.Close();
            this.DialogResult = DialogResult.OK;


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
        
     

        private void numberText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            BathClass.only_allow_float(txtBox, e);
        }        

    }
}
