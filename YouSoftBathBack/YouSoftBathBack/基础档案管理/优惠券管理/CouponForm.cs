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
using System.IO;

namespace YouSoftBathBack
{
    public partial class CouponForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Coupon m_Coupon = new Coupon();
        private bool newCoupon = true;
        private byte[] m_Image;

        //构造函数
        public CouponForm(BathDBDataContext dc, Coupon coupon)
        {
            db = dc;
            if (coupon != null)
            {
                newCoupon = false;
                m_Coupon = coupon;
            }

            InitializeComponent();
        }

        //对话框载入
        private void CouponForm_Load(object sender, EventArgs e)
        {
            menuId.Items.AddRange(db.Menu.Select(x => x.name).ToArray());
            transator.Items.AddRange(db.Employee.Select(x => x.name).ToArray());
            expireDate.Value = BathClass.Now(LogIn.connectionString).AddMonths(3);
            if (!newCoupon)
            {
                id.Text = m_Coupon.id;
                name.Text = m_Coupon.name;
                money.Text = m_Coupon.money.ToString("0.0");

                var menu = db.Menu.FirstOrDefault(x => x.id == m_Coupon.menuId);
                if (menu != null)
                    menuId.Text = menu.name;

                issueDate.Value = m_Coupon.issueDate;
                expireDate.Value = m_Coupon.expireDate;
                transator.Text = m_Coupon.issueTransator;
                note.Text = m_Coupon.note;
                minAmount.Text = m_Coupon.minAmount.ToString();
                if (m_Coupon.img != null)
                {
                    m_Image = (byte[])m_Coupon.img.ToArray();
                    img.BackgroundImage = ReturnPhoto(m_Image);
                }
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_Coupon.id = id.Text;
            m_Coupon.name = name.Text;
            m_Coupon.money = Convert.ToDouble(money.Text);
            if (menuId.Text != "")
                m_Coupon.menuId = db.Menu.FirstOrDefault(x => x.name == menuId.Text).id;
            m_Coupon.issueDate = issueDate.Value;
            m_Coupon.expireDate = expireDate.Value;
            m_Coupon.issueTransator = transator.Text;
            m_Coupon.note = note.Text;
            if (minAmount.Text != "")
                m_Coupon.minAmount = Convert.ToDouble(minAmount.Text);
            m_Coupon.img = m_Image;

            if (newCoupon)
                db.Coupon.InsertOnSubmit(m_Coupon);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void btnSel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            path.Text = dlg.FileName;
            m_Image = GetPictureData(path.Text);
            img.BackgroundImage = ReturnPhoto(m_Image);
        }

        public byte[] GetPictureData(string imagepath)
        {
            /**/
            ////根据图片文件的路径使用文件流打开，并保存为byte[] 
            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法 
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }

        //1.参数是Byte[]类型，返回值是Image对象: 
        public System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
