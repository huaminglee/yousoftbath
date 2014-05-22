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
    public partial class GoodsCatForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private GoodsCat m_goodsCat = new GoodsCat();
        private bool newGoodsCat = true;

        //构造函数
        public GoodsCatForm(BathDBDataContext dc, GoodsCat goodsCat)
        {
            db = dc;
            if (goodsCat != null)
            {
                newGoodsCat = false;
                m_goodsCat = goodsCat;
            }
            InitializeComponent();
        }

        //对话框载入
        private void SeatTypeForm_Load(object sender, EventArgs e)
        {
            if (!newGoodsCat)
            {
                name.Text = m_goodsCat.name;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_goodsCat.name = name.Text.Trim();


            if (newGoodsCat)
            {
                if (db.GoodsCat.FirstOrDefault(x=>x.name==m_goodsCat.name) != null)
                {
                    BathClass.printErrorMsg("已经存在此名称的商品类别!");
                    name.SelectAll();
                    return;
                }
                db.GoodsCat.InsertOnSubmit(m_goodsCat);
            }
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

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
