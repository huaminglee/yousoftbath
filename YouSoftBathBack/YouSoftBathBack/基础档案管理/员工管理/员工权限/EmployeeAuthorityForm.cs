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
    public partial class EmployeeAuthorityForm : Form
    {
        private BathDBDataContext db = null;
        private Employee m_Employee = null;
        private Authority m_Authority = null;
        private Control[] cts;

        public EmployeeAuthorityForm(BathDBDataContext dc, Employee employee)
        {
            db = dc;
            m_Employee = employee;

            InitializeComponent();
        }

        //对话框载入
        private void EmployeeAuthorityForm_Load(object sender, EventArgs e)
        {
            getAuthority();

            cts = new Control[rp.Controls.Count + bg.Controls.Count];
            rp.Controls.CopyTo(cts, 0);
            bg.Controls.CopyTo(cts, rp.Controls.Count);

            foreach (Control p in cts)
            {
                foreach(Control ct in p.Controls)
                {
                    if (ct.GetType() == typeof(CheckBox))
                    {
                        var pro = m_Authority.GetType().GetProperty(ct.Text);
                        if (pro == null)
                            continue;

                        var proVal = pro.GetValue(m_Authority, null);
                        ((CheckBox)ct).Checked = Convert.ToBoolean(proVal);
                    }
                }
            }
        }

        //确认
        private void okBtn_Click(object sender, EventArgs e)
        {
            bool newAuthority = false;
            var authority = db.Authority.FirstOrDefault(x => x.emplyeeId == m_Employee.id);
            if (authority == null)
            {
                newAuthority = true;
                authority = new Authority();
                authority.emplyeeId = m_Employee.id;
            }

            foreach (Control p in cts)
            {
                foreach (Control ct in p.Controls)
                {
                    if (ct.GetType() == typeof(CheckBox))
                    {
                        var pro = authority.GetType().GetProperty(ct.Text);
                        if (pro == null)
                            continue;

                        if (BathClass.getAuthority(db, LogIn.m_User, ct.Text))
                            pro.SetValue(authority, ((CheckBox)ct).Checked, null);
                    }
                }
            }
            if (newAuthority)
                db.Authority.InsertOnSubmit(authority);

            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //创建权限列表
        private void createAuthorityBox()
        {
            List<string> allAuthorities = BathClass.getAllAuthorities();

            int count = allAuthorities.Count;
            int theCount = 3, row = 0;
            while (true)
            {
                for (int col = 0; col < 4; col++)
                {
                    int x = col * 150 + 20;
                    int y = row * 40 + 20;
                    Point pt = new Point(x, y);

                    string authorityStr = allAuthorities[theCount];
                    //createCheckbox(pt, authorityStr, getAuthority(authorityStr));
                    theCount++;
                    if (theCount == count)
                        return;
                }
                row++;
            }
        }

        //创建权限checkbox
        private void createCheckbox(Point pt, string txt, bool autho)
        {
            CheckBox chBox = new System.Windows.Forms.CheckBox();
            authoPanel.Panel1.Controls.Add(chBox);

            chBox.AutoSize = true;
            chBox.Location = pt;
            chBox.Name = "checkBox1";
            chBox.Size = new System.Drawing.Size(78, 16);
            chBox.TabIndex = 0;
            chBox.Text = txt;
            chBox.Checked = autho;
            chBox.UseVisualStyleBackColor = true;
        }

        // 获取员工权限，如果权限表中没有对应的employeeId，则按照jobId对应的权限
        private void getAuthority()
        {
            m_Authority = db.Authority.FirstOrDefault(x => x.emplyeeId == m_Employee.id);
            if (m_Authority == null)
                m_Authority = db.Authority.FirstOrDefault(x => x.jobId == m_Employee.jobId);
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                okBtn_Click(null, null);
        }

        //开牌登记
        private void cSeat_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control c in seat.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = cSeat.Checked;
                }
            }
        }

        //结账
        private void cPay_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in pay.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = cPay.Checked;
                }
            }
        }

        //会员卡
        private void cMember_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in member.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = cMember.Checked;
                }
            }
        }

        //前台全选
        private void cReception_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in rp.Controls)
            {
                if (c.GetType() == typeof(Panel))
                {
                    foreach (Control cc in c.Controls)
                    {
                        if (cc.GetType() == typeof(CheckBox))
                        {
                            CheckBox cb = cc as CheckBox;
                            cb.Checked = cReception.Checked;
                        }
                    }
                }
            }
        }

        //报表查询
        private void bTable_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in table.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = bTable.Checked;
                }
            }
        }

        //会员管理
        private void bMemberM_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in card.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = bMemberM.Checked;
                }
            }
        }

        //基础档案管理
        private void bDocument_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in document.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = bDocument.Checked;
                }
            }
        }

        //仓库管理
        private void bStock_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in stock.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = bStock.Checked;
                }
            }
        }

        //系统管理
        private void bSystem_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in system.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cb = c as CheckBox;
                    cb.Checked = bSystem.Checked;
                }
            }
        }

        //后台全选
        private void cBack_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in bg.Controls)
            {
                if (c.GetType() == typeof(Panel))
                {
                    foreach(Control cc in c.Controls)
                    {
                        if (cc.GetType() == typeof(CheckBox))
                        {
                            CheckBox cb = cc as CheckBox;
                            cb.Checked = cBack.Checked;
                        }
                    }
                }
            }
        }
    }
}
