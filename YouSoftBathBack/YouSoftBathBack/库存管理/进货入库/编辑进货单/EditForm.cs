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
    public partial class EditForm : Form
    {
        private BathDBDataContext db = null;       
        private StockIn inStock;
        private double totalmoney = 0;
       
        public EditForm(BathDBDataContext dc, StockIn stockin)
        {
            db = dc;
            inStock = stockin;          
            InitializeComponent();
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            provider.Items.AddRange(db.Provider.Select(x => x.name).ToArray());
            goodsCat.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());
            unit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
            var employees = db.Employee.Where(x => !db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            checker.Items.AddRange(employees.Select(x => x.name).ToArray());
            transactor.Items.AddRange(employees.Select(x => x.name).ToArray());
            var stocktext = db.Stock.FirstOrDefault(x => x.id == stockin.stockId);
            stock.Text = stocktext !=null?stocktext.name:"";
            string nametext = stockin.name;
            name.Text = nametext != "" ? nametext : "";
            var goodCatext = db.GoodsCat.FirstOrDefault(y => y.id == db.StorageList.FirstOrDefault(x => x.name == stockin.name).goodsCatId);
            //MessageBox.Show("该商品没分类");
            goodsCat.Text = goodCatext != null ? goodCatext.name : "";
            string amouttext = stockin.amount.ToString();
            amount.Text = amouttext != "" ? amouttext : "";
            var providertext = db.Provider.FirstOrDefault(x => x.id == stockin.providerId);
            provider.Text = providertext != null ? providertext.name : "";
            string costtext = stockin.cost.ToString();
            cost.Text = costtext != "" ? costtext : "";
            string moneytext = stockin.money.ToString();
            money.Text = moneytext != "" ? moneytext : "";
            dtPickerIntoStock.Value = stockin.date != null ? stockin.date : DateTime.Now;
            string checkertext = stockin.checker;
            checker.Text = checkertext != "" ? checkertext : "";
            string transactortext=stockin.transactor;
            transactor.Text = transactortext != "" ? transactortext : "";
            string notetext = stockin.note;
            note.Text = notetext != "" ? notetext : "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
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

            if (stock.Text == "")
            {
                BathClass.printErrorMsg("需要选择仓库!");
                return;
            }

            if (transactor.Text == "")
            {
                BathClass.printErrorMsg("需要输入经手人!");
                return;
            }

            if (checker.Text == "")
            {
                BathClass.printErrorMsg("需要输入审核!");
                return;
            }

            if (provider.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要选择供应商!");
                return;
            }

            //StockIn inStock = new StockIn();
            inStock.name = name.Text;
            if (cost.Text != "")
                inStock.cost = Convert.ToDouble(cost.Text);
            inStock.amount = Convert.ToDouble(amount.Text);

            if (unit.Text != "")
                inStock.unit = unit.Text;

            inStock.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            inStock.note = note.Text;
            inStock.date = dtPickerIntoStock.Value;
            inStock.transactor = transactor.Text;
            inStock.checker = checker.Text;
            inStock.money = totalmoney;

            var p = db.Provider.FirstOrDefault(x => x.name == provider.Text.Trim());
            if (p == null)
            {
                p = new Provider();
                p.name = provider.Text.Trim();
                var form = new ProviderForm(db, p);
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                db.Provider.InsertOnSubmit(p);
                db.SubmitChanges();
            }
            inStock.providerId = p.id;
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;    
        }

        private void cost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double _amout = Convert.ToDouble(amount.Text);
                double _cost = Convert.ToDouble(cost.Text);
                totalmoney = _amout * _cost;
                money.Text = totalmoney.ToString();
            }
            catch (System.Exception ex)
            {

            }
        }

        private void amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;            
            BathClass.only_allow_float(txtBox, e);
        }

        private void cost_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            BathClass.only_allow_float(txtBox, e);
        }
    }
}
