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

namespace YouSoftBathBack
{
    public partial class ModifyPictureForm : Form
    {
        //成员变量
        private byte[] m_Image = null;

        public byte[] image
        {
            get { return m_Image; }
        }

        //构造函数
        public ModifyPictureForm(byte[] _image)
        {
            m_Image = _image;
            InitializeComponent();

            if (m_Image != null)
                Picture1.BackgroundImage = ReturnPhoto(m_Image);
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
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
                    BtnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
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

        //移除
        private void BtnRemove_Click(object sender, EventArgs e)
        {
            m_Image = null;
            Picture1.BackgroundImage = null;
        }

        //修改
        private void BtnModify_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            var path = dlg.FileName;
            m_Image = GetPictureData(path);
            Picture1.BackgroundImage = ReturnPhoto(m_Image);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
