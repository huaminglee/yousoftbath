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
        private StockOut stockout = new StockOut();
        private bool newStockout = true;

        //构造函数
        public StockOutForm(BathDBDataContext dc, StockOut _stockout)
        {
            db = dc;
            if (_stockout != null)
            {
                newStockout = false;
                stockout = _stockout;
            }
            InitializeComponent();
          
           
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            goodsCat.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());

            var employees = db.Employee.Where(x => !db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            receiver.Items.AddRange(employees.Select(x => x.name).ToArray());
            checker.Items.AddRange(employees.Select(x => x.name).ToArray());
            transactor.Items.AddRange(employees.Select(x => x.name).ToArray());
            ComboUnit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
            if (newStockout)
        {
            receiver.Text = LogIn.m_User.name;
            checker.Text = LogIn.m_User.name;
            transactor.Text = LogIn.m_User.name;
        }
            else
            {
                var goodsCatID=db.StorageList.FirstOrDefault(x => x.name == stockout.name).goodsCatId;
                var goodsCatName=db.GoodsCat.FirstOrDefault(x=>x.id==goodsCatID).name;
                goodsCat.Text=MConvert<string>.ToTypeOrDefault(goodsCatName,"");
                name.Text = stockout.name;
                stock.Text=db.Stock.FirstOrDefault(x=>x.id==stockout.stockId).name;
                ComboUnit.Text = stockout.unit;
                amount.Text = stockout.amount.ToString();
                dtPickerIntoStock.Value = MConvert<DateTime>.ToTypeOrDefault(stockout.date,DateTime.Now);
                receiver.Text = stockout.receiver;
                checker.Text = stockout.checker;
                transactor.Text = stockout.transactor;
                note.Text = stockout.note;
            }
            
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (!validateTextFields())
                return;
            stockout.name = name.Text;
            stockout.amount = MConvert<double>.ToTypeOrDefault(amount.Text, 0);
            stockout.unit = ComboUnit.Text;
            stockout.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            stockout.date = dtPickerIntoStock.Value;
            stockout.receiver = receiver.Text;
            stockout.transactor = transactor.Text;
            stockout.checker = checker.Text;
            stockout.note = note.Text;

            if (newStockout)
            {
                db.StockOut.InsertOnSubmit(stockout);
            string unit_txt = ComboUnit.Text.Trim();
            if (unit_txt != "")
            {
                if (!db.Unit.Any(x => x.name == unit_txt))
                {
                    var unit_instance = new Unit();
                    unit_instance.name = unit_txt;
                    db.Unit.InsertOnSubmit(unit_instance);
                }
            }
            db.SubmitChanges();
                stockout = new StockOut();
                goodsCat.SelectedIndex = -1;
                stock.SelectedIndex = -1;
                name.Text ="";
                amount.Text = "";
                ComboUnit.Text = "";
                receiver.Text = LogIn.m_User.name;
                transactor.Text = LogIn.m_User.name;
                checker.Text = LogIn.m_User.name;
                note.Text = "";
            }
            else
            {
                db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
                this.Close();
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

        private void name_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUnit = db.StockOut.FirstOrDefault(x => x.name == name.Text);
            if ( selectedUnit != null)
            {
                ComboUnit.Text = selectedUnit.unit;
            }
            else
                ComboUnit.Text = "";
        }
              

         private void numberText_KeyPress(object sender, KeyPressEventArgs e)
         {
             TextBox txtBox = (TextBox)sender;
             BathClass.only_allow_float(txtBox, e);
         }

         private void btnCancel_Click(object sender, EventArgs e)
         {
             this.Close();             
         }  

       
    

    }
}
