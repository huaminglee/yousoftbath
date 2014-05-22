using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathFormClass;
using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class GoodsForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private StorageList m_goods = new StorageList();
        private bool newGoods = true;
        private StorageForm m_form;

        //构造函数
        public GoodsForm(BathDBDataContext dc, StorageList goods, StorageForm form)
        {
            db = dc;
            m_form = form;
            if (goods != null)
            {
                newGoods = false;
                m_goods = goods;
            }
            InitializeComponent();
            catId.Items.AddRange(db.GoodsCat.Select(x => x.name).ToArray());
            provider.Items.AddRange(db.Provider.Select(x => x.name).ToArray());
            stock.Items.AddRange(db.Stock.Select(x => x.name).ToArray());
            ComboUnit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());
        }

        //对话框载入
        private void SeatTypeForm_Load(object sender, EventArgs e)
        {
            if (!newGoods)
            {
                name.Text = m_goods.name;
                minAmount.Text = m_goods.minAmount.ToString();
                note.Text = m_goods.note;
                catId.Text = db.GoodsCat.FirstOrDefault(x => x.id == m_goods.goodsCatId).name;

                if (db.StockIn.Any(x => x.name == m_goods.name) || db.StockOut.Any(x => x.name == m_goods.name))
                {
                    GroupStock.Enabled = false;
                }
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (catId.Text == "")
            {
                BathClass.printErrorMsg("需要选择商品类别");
                return;
            }

            m_goods.name = name.Text.Trim();
            try
            {
                m_goods.minAmount = Convert.ToInt32(minAmount.Text.Trim());
            }
            catch
            {
                m_goods.minAmount = null;
            }
            m_goods.note = note.Text.Trim();
            m_goods.goodsCatId = db.GoodsCat.FirstOrDefault(x => x.name == catId.Text).id;


            if (newGoods)
            {
                if (db.StorageList.FirstOrDefault(x => x.name == m_goods.name) != null)
                {
                    BathClass.printErrorMsg("已经存在此名称的商品!");
                    name.SelectAll();
                    return;
                }
                db.StorageList.InsertOnSubmit(m_goods);
            }

            if (amount.Text.Trim() != "")
            {
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

                StockIn inStock = new StockIn();
                inStock.name = name.Text;
                if (cost.Text != "")
                    inStock.cost = Convert.ToDouble(cost.Text);
                inStock.amount = Convert.ToDouble(amount.Text);
                inStock.stockId = db.Stock.FirstOrDefault(x => x.name == stock.Text).id;
                inStock.note = stockNote.Text;
                inStock.date = DateTime.Now;
                inStock.transactor = LogIn.m_User.id;
                inStock.checker = LogIn.m_User.id;

                string unit_text = ComboUnit.Text.Trim();
                if (unit_text != "")
                {
                    inStock.unit = unit_text;
                    if (!db.Unit.Any(x=>x.name==unit_text))
                    {
                        var unit_instance = new Unit();
                        unit_instance.name = unit_text;
                        db.Unit.InsertOnSubmit(unit_instance);
                    }
                     
                }

                if (money.Text.Trim() != "")
                    inStock.money = Convert.ToDouble(money.Text.Trim());
                db.StockIn.InsertOnSubmit(inStock);
            }
            db.SubmitChanges();
            if (newGoods)
            {
                name.Text = "";
                catId.SelectedIndex = -1;
                provider.SelectedIndex = -1;
                minAmount.Text = "";
                note.Text = "";
                cost.Text = "";
                amount.Text = "";
                stock.SelectedIndex = -1;
                stockNote.Text = "";
                money.Text = "";
                name.Focus();
                db.SubmitChanges();
                m_form.dgv_show();

                m_goods = new StorageList();
            }
            else
            {
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
                //case Keys.Enter:
                //    btnOk_Click(null, null);
                //    break;
                default:
                    break;
            }
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void minAmount_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
