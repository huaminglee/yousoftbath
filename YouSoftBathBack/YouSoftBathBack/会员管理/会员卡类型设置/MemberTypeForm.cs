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
    public partial class MemberTypeForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private MemberType m_Element = new MemberType();
        private bool newElement = true;

        //构造函数
        public MemberTypeForm(MemberType element)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            if (element != null)
            {
                newElement = false;
                m_Element = db.MemberType.FirstOrDefault(x => x.id == element.id);
            }
            InitializeComponent();
        }

        //对话框载入
        private void MemberTypeForm_Load(object sender, EventArgs e)
        {
            offer.Items.AddRange(db.Promotion.Select(x => x.name).ToArray());
            offer.SelectedIndex = 0;
            if (!newElement)
            {
                foreach (Control c in this.Controls)
                {
                    if (c.GetType() == typeof(TextBox) || c.GetType() == typeof(DateTimePicker))
                    {
                        var pro = m_Element.GetType().GetProperty(c.Name);
                        if (pro == null)
                            continue;

                        var proVal = pro.GetValue(m_Element, null);
                        if (proVal != null)
                            c.Text = proVal.ToString();
                    }
                }
                userOneTime.Checked = MConvert<bool>.ToTypeOrDefault(m_Element.userOneTimeOneDay,false);
                LimitedTimesPerMonth.Checked = MConvert<bool>.ToTypeOrDefault(m_Element.LimitedTimesPerMonth, false);
                TimesPerMonth.Text = m_Element.TimesPerMonth.ToString();
                credits.Checked = m_Element.credits;
                sms.Checked = MConvert<bool>.ToTypeOrDefault(m_Element.smsAfterUsing, false);
                var promotion = db.Promotion.FirstOrDefault(x => x.id == m_Element.offerId);
                if (promotion != null)
                    offer.Text = promotion.name;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    if (c.Text != "")
                    {
                        var pro = m_Element.GetType().GetProperty(c.Name);
                        var proVal = TypeDescriptor.GetConverter(pro.PropertyType).ConvertFrom(c.Text);
                        pro.SetValue(m_Element, proVal, null);
                    }
                }
            }

            m_Element.LimitedTimesPerMonth = LimitedTimesPerMonth.Checked;
            if (offer.Text != "")
            {
                m_Element.offerId = db.Promotion.FirstOrDefault(x => x.name == offer.Text).id;
            }
            else
                m_Element.offerId = null;

            //if (cExpireDate.Checked)
            //{
            //    m_Element.expireDate = expireDate.Value;
            //}
            //else
            //    m_Element.expireDate = null;

            m_Element.userOneTimeOneDay = userOneTime.Checked;
            m_Element.smsAfterUsing = sms.Checked;
            m_Element.credits = credits.Checked;
            if (newElement)
                db.MemberType.InsertOnSubmit(m_Element);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void cExpireDate_CheckedChanged(object sender, EventArgs e)
        {
            //expireDate.Enabled = true;
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

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void LimitedTimesPerMonth_CheckedChanged(object sender, EventArgs e)
        {
            TimesPerMonth.Enabled = LimitedTimesPerMonth.Checked;
        }
    }
}
