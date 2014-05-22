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
    public partial class SystemSettingForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Options m_options = new Options();
        private List<Control> ctList = new List<Control>();

        //构造函数
        public SystemSettingForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void RegionForm_Load(object sender, EventArgs e)
        {
            foreach(TabPage tp in tabControl1.TabPages)
            {
                foreach (Control c in tp.Controls)
                {
                    if (c.GetType() == typeof(TextBox) || c.GetType() == typeof(CheckBox) || c.GetType() == typeof(ComboBox))
                    {
                        ctList.Add(c);
                    }
                }
            }

            m_options = db.Options.FirstOrDefault();
            if (m_options != null)
            {
                foreach (Control c in ctList)
                {
                    var pro = m_options.GetType().GetProperty(c.Name);
                    if (pro == null)
                        continue;

                    var proVal = pro.GetValue(m_options, null);
                    if (proVal == null)
                        continue;

                    if (c.GetType() == typeof(CheckBox))
                    {
                        CheckBox cb = c as CheckBox;
                        cb.Checked = Convert.ToBoolean(proVal);
                    }
                    else
                        c.Text = proVal.ToString();

                }
                手牌锁类型.Enabled = 启用手牌锁.Checked;
                过夜费起点.Enabled = 自动加收过夜费.Checked;
                过夜费终点.Enabled = 自动加收过夜费.Checked;
                
                启用分单结账.Enabled = !启用手牌锁.Checked;
                允许手工输入手牌号结账.Enabled = !启用手牌锁.Checked;
                允许手工输入手牌号开牌.Enabled = !启用手牌锁.Checked;
                启用ID手牌锁.Enabled = !启用手牌锁.Checked;
                自动感应手牌.Enabled = 启用手牌锁.Checked;
                
                会员卡密码类型.Enabled = 启用会员卡密码.Checked;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            foreach (Control c in ctList)
            {
                var pro = m_options.GetType().GetProperty(c.Name);
                if (pro == null)
                    continue;

                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    pro.SetValue(m_options, cb.Checked, null);
                }
                else
                {
                    if (c.Text == "")
                        continue;

                    var proVal = TypeDescriptor.GetConverter(pro.PropertyType).ConvertFrom(c.Text);
                    pro.SetValue(m_options, proVal, null);
                }
            }

            if (db.Options.FirstOrDefault() == null)
                db.Options.InsertOnSubmit(m_options);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        //绑定快捷键
        private void SystemSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void 开业时间_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void ch_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void en_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void 启用手牌锁_CheckedChanged(object sender, EventArgs e)
        {
            手牌锁类型.Enabled = 启用手牌锁.Checked;
            if (启用手牌锁.Checked)
            {
                允许手工输入手牌号结账.Checked = false;
                允许手工输入手牌号结账.Enabled = false;

                允许手工输入手牌号开牌.Checked = false;
                允许手工输入手牌号开牌.Enabled = false;

                启用ID手牌锁.Checked = false;
                启用ID手牌锁.Enabled = false;

                启用分单结账.Checked = false;
                启用分单结账.Enabled = false;
                自动感应手牌.Enabled = true;
            }
            else
            {
                允许手工输入手牌号结账.Enabled = true;
                允许手工输入手牌号开牌.Enabled = true;
                启用ID手牌锁.Enabled = true;
                启用分单结账.Enabled = true;
                自动感应手牌.Enabled = false;
            }
        }

        private void 自动加收过夜费_CheckedChanged(object sender, EventArgs e)
        {
            过夜费起点.Enabled = 自动加收过夜费.Checked;
            过夜费终点.Enabled = 自动加收过夜费.Checked;
        }

        private void 启用会员卡密码_CheckedChanged(object sender, EventArgs e)
        {
            会员卡密码类型.Enabled = 启用会员卡密码.Checked;
        }
    }
}
