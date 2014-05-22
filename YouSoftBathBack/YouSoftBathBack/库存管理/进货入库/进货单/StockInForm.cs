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
        private IntoStockManagementForm m_Form;

        //构造函数
        public StockInForm(BathDBDataContext dc, IntoStockManagementForm form)
        {
            db = dc;
            m_Form = form;
            InitializeComponent();
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            provider.Items.AddRange(db.Provider.Select(x => x.name).ToArray());
            goodsCat.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());
            unit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
            
            var employees = db.Employee.Where(x => !db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            checker.Items.AddRange(employees.Select(x => x.name).ToArray());
            transactor.Items.AddRange(employees.Select(x => x.name).ToArray());
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            checker.Text = LogIn.m_User.name;
            transactor.Text = LogIn.m_User.name;
            name.Enabled = false;
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

            if (money.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要输入金额");
                return;
            }

            if (provider.Text.Trim() == "")
            {
                BathClass.printErrorMsg("需要选择供应商!");
                return;
            }
            StockIn inStock = new StockIn();
            inStock.name = name.Text;
            if (cost.Text != "")
                inStock.cost = Convert.ToDouble(cost.Text);
            inStock.amount = Convert.ToDouble(amount.Text);
            if (unit.Text != "")
                inStock.unit = unit.Text;

            inStock.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
            inStock.note = note.Text;
            inStock.date = DateTime.Now;
            inStock.transactor = transactor.Text;
            inStock.checker = checker.Text;
            inStock.money = Convert.ToDouble(money.Text);

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
            inStock.providerId = p.id;
            db.StockIn.InsertOnSubmit(inStock);

            //if (provider.Text != "" && db.Unit.FirstOrDefault(x=>x.name==provider.Text) == null)
            //{
            //    Unit ut = new Unit();
            //    ut.name = provider.Text;
            //    db.Unit.InsertOnSubmit(ut);
            //}

            name.SelectedIndex = -1;
            cost.Text = "";
            amount.Text = "";
            stock.SelectedIndex = -1;
            checker.SelectedIndex = -1;
            transactor.SelectedIndex = -1;
            note.Text = "";
            money.Text = "";
            name.Focus();
            db.SubmitChanges();
            m_Form.dgv_show();
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
            name.Items.AddRange(db.StorageList.Where(x=>x.goodsCatId==goods_cat.id).Select(x=>x.name).ToArray());
        }
    }
}
