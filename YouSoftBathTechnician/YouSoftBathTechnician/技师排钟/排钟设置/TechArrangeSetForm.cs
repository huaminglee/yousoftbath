using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathTechnician
{
    public partial class TechArrangeSetForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public TechArrangeSetForm()
        {
            db = new BathDBDataContext(MainForm.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void SeatForm_Load(object sender, EventArgs e)
        {
            var m_options = db.Options.FirstOrDefault();
            if (m_options == null)
                return;

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(ComboBox))
                {
                    var pro = m_options.GetType().GetProperty(c.Name);
                    if (pro == null)
                        continue;

                    var proVal = pro.GetValue(m_options, null);
                    if (proVal == null)
                        continue;

                    c.Text = proVal.ToString();
                }
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            var m_options = db.Options.FirstOrDefault();

            bool newOptions = false;
            if (m_options == null)
            {
                newOptions = true;
                m_options = new Options();
            }

            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(ComboBox))
                {
                    if (c.Text == "")
                        continue;

                    var pro = m_options.GetType().GetProperty(c.Name);
                    if (pro == null)
                        continue;

                    var proVal = TypeDescriptor.GetConverter(pro.PropertyType).ConvertFrom(c.Text);
                    pro.SetValue(m_options, proVal, null);
                }
            }

            if (newOptions)
                db.Options.InsertOnSubmit(m_options);
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

    }
}
