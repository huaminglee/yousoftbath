using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class NewPostForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private byte[] m_Image1 = null;
        private byte[] m_Image2 = null;
        private byte[] m_Image3 = null;

        //构造函数
        public NewPostForm(BathDBDataContext dc)
        {
            db = dc;
            InitializeComponent();
            ComboDeparts.Items.AddRange(dc.Department.Select(x => x.name).ToArray());
            //PanelPicture.Visible = false;
            //this.Height = PanelMsg.Height + PanelControl.Height + 20;
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            string depart = ComboDeparts.Text.Trim();
            if (depart == "")
            {
                BathClass.printErrorMsg("需要填写呈交部门");
                ComboDeparts.SelectAll();
                ComboDeparts.Focus();
            }

            var log = new DepartmentLog();
            log.date = DateTime.Now;
            log.departId = db.Department.FirstOrDefault(x => x.name == depart).id;

            if (m_Image1 != null)
                log.img = m_Image1;

            if (m_Image2 != null)
                log.img2 = m_Image2;

            if (m_Image3 != null)
                log.img3 = m_Image3;

            string msg = TextMsg.Text.Trim();
            if (msg != "")
                log.msg = msg;

            if (!CheckAnonymous.Checked)
                log.sender = LogIn.m_User.name;

            db.DepartmentLog.InsertOnSubmit(log);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
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

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        //上传图片
        private void BtnUploadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;
            
            var path = dlg.FileName;
            TextImgPath.Text = path;

            if (m_Image1 == null)
            {
                m_Image1 = GetPictureData(path);
                Picture1.BackgroundImage = ReturnPhoto(m_Image1);

                set_btn_status();
            }
            else if (m_Image2 == null)
            {
                m_Image2 = GetPictureData(path);
                Picture2.BackgroundImage = ReturnPhoto(m_Image2);
                set_btn_status();
            }
            else if (m_Image3 == null)
            {
                m_Image3 = GetPictureData(path);
                Picture3.BackgroundImage = ReturnPhoto(m_Image3);
                set_btn_status();
            }
                
        }


        private void set_btn_status()
        {
            if (m_Image1 == null)
            {
                BtnUploadImg.Text = "上传第一幅图片";
            }
            else if (m_Image2 == null)
            {
                BtnUploadImg.Text = "上传第二幅图片";
            }
            else if (m_Image3 == null)
            {
                BtnUploadImg.Text = "上传第三幅图片";
            }
            else
            {
                BtnUploadImg.Enabled = false;
            }
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

        private void Picture1_DoubleClick(object sender, EventArgs e)
        {
            var form = new ModifyPictureForm(m_Image1);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            m_Image1 = form.image;
            if (m_Image1 == null)
                Picture1.BackgroundImage = null;
            else
                Picture1.BackgroundImage = ReturnPhoto(m_Image1);

            set_btn_status();

        }

        private void Picture2_DoubleClick(object sender, EventArgs e)
        {
            var form = new ModifyPictureForm(m_Image2);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            m_Image2 = form.image;
            if (m_Image2 == null)
                Picture2.BackgroundImage = null;
            else
                Picture2.BackgroundImage = ReturnPhoto(m_Image2);

            set_btn_status();
        }

        private void Picture3_DoubleClick(object sender, EventArgs e)
        {
            var form = new ModifyPictureForm(m_Image3);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            m_Image3 = form.image;
            if (m_Image3 == null)
                Picture3.BackgroundImage = null;
            else
                Picture3.BackgroundImage = ReturnPhoto(m_Image3);

            set_btn_status();
        }
    }
}
