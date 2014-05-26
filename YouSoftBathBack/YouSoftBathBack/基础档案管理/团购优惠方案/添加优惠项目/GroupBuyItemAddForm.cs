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
    public partial class GroupBuyItemAddForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private GroupBuyPromotion m_Promotion = new GroupBuyPromotion();
        private List<string> menus = new List<string>();

        public string m_Offer;//折扣项目
        public double m_discout;//折扣或者折后价
        public string m_discoutType;//折扣形式

        //构造函数
        public GroupBuyItemAddForm(BathDBDataContext dc, GroupBuyPromotion promotion)
        {
            db = dc;
            m_Promotion = promotion;
            InitializeComponent();
        }

        //对话框载入
        private void PromotionItemAddForm_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> pOffer = BathClass.disAssemble(db, m_Promotion);
            List<string> cats = new List<string>();
            foreach (string key in pOffer.Keys)
            {
                menus.Add(key);
            }

            type.Items.Clear();
            type.Items.AddRange(db.Catgory.Where(x => !cats.Contains(x.name)).Select(x => x.name).ToArray());
            if (type.Items.Count != 0)
            {
                type.SelectedIndex = 0;

                int catId = db.Catgory.FirstOrDefault(x => x.name == type.Text).id;
                menu.Items.AddRange(db.Menu.Where(x => x.catgoryId == catId && !menus.Contains(x.id.ToString())).Select(x => x.name).ToArray());
            }
            discountType.SelectedIndex = 0;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(discount.Text, out m_discout))
            {
                discount.SelectAll();
                discount.Focus();
                GeneralClass.printErrorMsg("输入折扣格式不对，需要输入数字!");
                return;
            }
            
            var m = db.Menu.FirstOrDefault(x => x.name == menu.Text);
            if (m == null)
            {
                GeneralClass.printErrorMsg("需要选择项目名称!");
                return;
            }
            m_Offer = m.id.ToString();
            m_discoutType = discountType.Text;

            this.DialogResult = DialogResult.OK;
        }


        //选择类别
        private void type_TextChanged(object sender, EventArgs e)
        {
            Catgory catgory = db.Catgory.FirstOrDefault(x => x.name == type.Text);
            if (catgory == null)
                return;

            menu.Items.Clear();
            menu.Items.AddRange(db.Menu.Where(x => x.catgoryId == catgory.id && !menus.Contains(x.id.ToString())).Select(x => x.name).ToArray());
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

        private void discount_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(discount, e);
        }

        private void discount_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //项目名称变化
        private void menu_TextChanged(object sender, EventArgs e)
        {
            var menuObj = db.Menu.FirstOrDefault(x => x.name == menu.Text);
            if (menuObj != null)
                price.Text = menuObj.price.ToString();
        }

        //折扣形式变化
        private void discountType_TextChanged(object sender, EventArgs e)
        {
            if (discountType.SelectedIndex == 0)
                lt.Text = "项目折扣";
            else if (discountType.SelectedIndex == 1)
                lt.Text = "折后价";

        }
    }
}
