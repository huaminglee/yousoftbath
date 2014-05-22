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
    public partial class PromotionForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Promotion m_Promotion = new Promotion();
        private bool newPromotion = true;
        private Dictionary<string, string> m_Offer = new Dictionary<string, string>();

        //构造函数
        public PromotionForm(BathDBDataContext dc, Promotion promotion)
        {
            db = dc;
            if (promotion != null)
            {
                newPromotion = false;
                m_Promotion = promotion;
                m_Offer = BathClass.disAssemble(db, m_Promotion);
            }
            InitializeComponent();
            dgv_show();
        }

        //对话框载入
        private void PromotionForm_Load(object sender, EventArgs e)
        {
            name.Text = m_Promotion.name;
            cboxStart.Checked = m_Promotion.status;
            if (m_Offer.Keys.Contains("所有项目"))
                btnAdd.Enabled = false;
        }

        //显示信息
        private void dgv_show()
        {
            dgv.Rows.Clear();
            foreach (string menuId in m_Offer.Keys)
            {
                var menu = db.Menu.FirstOrDefault(x => x.id.ToString() == menuId);
                
                var info = m_Offer[menuId].Split('#');
                string menuName = "";
                if (menu != null)
                    menuName = menu.name;
                else
                    menuName = menuId;

                dgv.Rows.Add(menuName, info[0], info[1]);
            }
        }

        //添加
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PromotionItemAddForm f = new PromotionItemAddForm(db, m_Promotion);
            if (f.ShowDialog() == DialogResult.OK)
            {
                m_Offer.Add(f.m_Offer, f.m_discoutType + "#" + f.m_discout.ToString());
                m_Promotion.menuIds = BathClass.assemble(m_Offer);
                dgv_show();
            }
        }

        //删除
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
                return;

            string offer = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();

            var m = db.Menu.FirstOrDefault(x => x.name == offer);
            if (m != null)
                m_Offer.Remove(m.id.ToString());
            else
                m_Offer.Remove(offer);

            m_Promotion.menuIds = BathClass.assemble(m_Offer);
            dgv_show();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                GeneralClass.printErrorMsg("需要添加优惠方案!");
                return;
            }
            //m_Promotion.id = Convert.ToInt32(id.Text);
            m_Promotion.name = name.Text;
            m_Promotion.status = cboxStart.Checked;

            if (newPromotion)
                db.Promotion.InsertOnSubmit(m_Promotion);
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
                case Keys.F2:
                    btnAdd_Click(null, null);
                    break;
                case Keys.F3:
                    btnDel_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
