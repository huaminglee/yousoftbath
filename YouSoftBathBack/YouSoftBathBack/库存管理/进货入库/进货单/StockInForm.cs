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
    public partial class StockInForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private StockIn stockin = new StockIn();
        private bool newStockin = true;
        //private IntoStockManagementForm m_Form;
        private double totalmoney = 0;

        //构造函数
        //public StockInForm(BathDBDataContext dc, IntoStockManagementForm form)
        public StockInForm(BathDBDataContext dc, StockIn _stockin)
        {
            db = dc;
            if (_stockin!=null)
            {
                stockin = _stockin;
                newStockin = false;
            }
            InitializeComponent();
            //m_Form = form;
        }

     
        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            provider.Items.AddRange(db.Provider.Select(x => x.name).ToArray());
            goodsCat.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());
            unit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
            
            var employees = db.Employee.Select(x => x.name);
            checker.Items.AddRange(employees.ToArray());
            transactor.Items.AddRange(employees.ToArray());

            if (newStockin)
            {
     
            checker.Text = LogIn.m_User.name;
            transactor.Text = LogIn.m_User.name;
            }
            else
            {
                var goodsCatID = db.StorageList.FirstOrDefault(x => x.name == stockin.name).goodsCatId;
                var goodsCatName = db.GoodsCat.FirstOrDefault(x => x.id == goodsCatID).name;
                goodsCat.Text = goodsCatName;
                name.Text = stockin.name;
                stock.Text = db.Stock.FirstOrDefault(x => x.id == stockin.stockId).name;
                unit.Text = stockin.unit;
                unit.Text = stockin.amount.ToString();
                cost.Text = stockin.cost.ToString();
                amount.Text = stockin.amount.ToString();
                money.Text = stockin.money.ToString();
                provider.Text = MConvert<string>.ToTypeOrDefault(db.Provider.FirstOrDefault(x => x.id == stockin.providerId).name,"");
                checker.Text = stockin.checker;
                transactor.Text = stockin.transactor;
                dtPickerIntoStock.Value = stockin.date;

            }
            
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                BathClass.printErrorMsg("需要选择商品名称");
                return;
            }

            if (amount.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入数量!");
                return;
            }

            if (stock.Text=="")
            {
                BathClass.printErrorMsg("需要选择仓库!");
                return;
            }

            if (transactor.Text=="")
            {
                BathClass.printErrorMsg("需要输入经手人!");
                return;
            }

            if (checker.Text=="")
            {
                BathClass.printErrorMsg("需要输入审核!");
                return;
            }


            if (provider.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要选择供应商!");
                return;
            }
            stockin.name = name.Text;
            stockin.cost = MConvert<double>.ToTypeOrDefault(cost.Text.Trim(), 0);
            stockin.amount = MConvert<double>.ToTypeOrDefault(amount.Text.Trim(),0);
            stockin.unit = unit.Text;
            stockin.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            stockin.note = note.Text;
            stockin.date = dtPickerIntoStock.Value;
            stockin.transactor = transactor.Text;
            stockin.checker = checker.Text;
            stockin.money = totalmoney;

            //StockIn inStock = new StockIn();
            if (newStockin)
            {
               
            if (db.Unit.Where(x=>x.name==unit.Text.Trim()).Count()==0)
            {
                Unit u = new Unit();
                u.name = unit.Text.Trim();
                db.Unit.InsertOnSubmit(u);
                db.SubmitChanges();
            }

            var p = db.Provider.FirstOrDefault(x => x.name == provider.Text.Trim());
            if (p==null)
            {
                p = new Provider();
                p.name = provider.Text.Trim();
                var form = new ProviderForm(db, p);
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                db.Provider.InsertOnSubmit(p);
                db.SubmitChanges();
            }
                stockin.providerId = p.id;
                db.StockIn.InsertOnSubmit(stockin);
            db.SubmitChanges();
                stockin = new StockIn();
                goodsCat.SelectedIndex = -1;
                name.Text = "";
                stock.SelectedIndex = -1;
                unit.Text = "";
                amount.Text = "";
                provider.SelectedIndex = -1;
                cost.Text = "";
                money.Text = "";
                checker.Text = "";
                transactor.Text = "";
                note.Text = "";
                checker.Text = LogIn.m_User.name;
                transactor.Text = LogIn.m_User.name; 

            }
            else
            {
                db.SubmitChanges();
            this.DialogResult=DialogResult.OK;
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
                //case Keys.Enter:
                //    okBtn_Click(null, null);
                //    break;
                default:
                    break;
            }
        }

        //只允许输入小数
        private void cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(cost, e);
        }

        private void amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            //BathClass.only_allow_int(e);
            BathClass.only_allow_float(txtBox, e);
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
            name.Items.AddRange(db.StorageList.Where(x=>x.goodsCatId==goods_cat.id).Select(x=>x.name).ToArray());
        }

        private void cost_TextChanged(object sender, EventArgs e)
        {
            try
            {
               double _amout =MConvert<double>.ToTypeOrDefault(amount.Text,0);
               double _cost = MConvert<double>.ToTypeOrDefault(cost.Text,0);
               totalmoney = _amout * _cost;
               money.Text = totalmoney.ToString();
            }
            catch (System.Exception ex)
            {
                 
            }
        }

        private void money_TextChanged(object sender, EventArgs e)
        {
            try
            {
                totalmoney = Convert.ToDouble(money.Text);
                //MessageBox.Show(totalmoney.ToString());
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void name_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedUnit = db.StockIn.FirstOrDefault(x => x.name == name.Text);
            if (selectedUnit != null)
                unit.Text = selectedUnit.unit;
            else
                unit.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void amount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double _amout = MConvert<double>.ToTypeOrDefault(amount.Text, 0);
                double _cost = MConvert<double>.ToTypeOrDefault(cost.Text, 0);
                totalmoney = _amout * _cost;
                money.Text = totalmoney.ToString();
            }
            catch (System.Exception ex)
            {

            }
        }

     

      
    }
}
