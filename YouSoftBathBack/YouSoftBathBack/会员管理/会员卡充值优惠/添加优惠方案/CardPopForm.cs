using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class CardPopForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private CardPopSale m_CardPopSale = new CardPopSale();
        private bool newCardPopSale = true;

        //构造函数
        public CardPopForm(BathDBDataContext dc, CardPopSale cardPopSale)
        {
            db = dc;
            if (cardPopSale != null)
            {
                newCardPopSale = false;
                m_CardPopSale = cardPopSale;
            }
            InitializeComponent();
        }

        //对话框载入
        private void PromotionForm_Load(object sender, EventArgs e)
        {
            if (newCardPopSale)
            {
                id.Text = (db.CardPopSale.Count() + 1).ToString();
            }
            else
            {
                id.Text = m_CardPopSale.id.ToString();
                minMoney.Text = m_CardPopSale.mimMoney.ToString();
                saleMoney.Text = m_CardPopSale.saleMoney.ToString();
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (minMoney.Text == "" || saleMoney.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入数据!");
                return;
            }

            m_CardPopSale.mimMoney = Convert.ToInt32(minMoney.Text);
            m_CardPopSale.saleMoney = Convert.ToInt32(saleMoney.Text);
            if (newCardPopSale)
                db.CardPopSale.InsertOnSubmit(m_CardPopSale);
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
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void minMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void minMoney_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
