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
    public partial class JobForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Job m_job = new Job();
        private Authority m_Authority = new Authority();
        private bool newJob = true;
        private Control[] cts;

        //构造函数
        public JobForm(BathDBDataContext dc, Job job)
        {
            db = dc;
            if (job != null)
            {
                newJob = false;
                m_job = job;
            }

            InitializeComponent();
            ComboJobList.Items.Add("");
            ComboJobList.Items.AddRange(db.Job.Select(x => x.name).ToArray());
            ComboJobList.SelectedIndex = 0;

            ComboDeparts.Items.AddRange((db.Department.Select(x => x.name).ToArray()));
        }

        //对话框载入
        private void JobForm_Load(object sender, EventArgs e)
        {
            cts = new Control[rp.Controls.Count + bg.Controls.Count];
            rp.Controls.CopyTo(cts, 0);
            bg.Controls.CopyTo(cts, rp.Controls.Count);

            if (!newJob)
            {
                id.Text = m_job.id.ToString();
                name.Text = m_job.name;
                note.Text = m_job.note;
                ip.Text = m_job.ip;
                this.Text = "编辑职位";
                if (m_job.leaderId != null)
                    ComboJobList.Text = db.Job.FirstOrDefault(x => x.id == m_job.leaderId).name;

                if (m_job.departId != null)
                    ComboDeparts.Text = db.Department.FirstOrDefault(x => x.id == m_job.departId).name;

                m_Authority = db.Authority.FirstOrDefault(x => x.jobId == m_job.id);
                foreach (Control p in cts)
                {
                    foreach (Control ct in p.Controls)
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
            else
            {
                int maxId = 1;
                if (db.Job.Count() != 0)
                {
                    var res = db.Job.Max(x => x.id);
                    maxId = res + 1;
                }
                id.Text = maxId.ToString();
                name.Focus();
            }
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
                    //createCheckbox(pt, allAuthorities[theCount]);
                    theCount++;
                    if (theCount == count)
                        return;
                }
                row++;
            }
        }

        //创建CheckBox
        private void createCheckbox(Point pt, string txt, Control pa)
        {
            CheckBox chBox = new System.Windows.Forms.CheckBox();
            pa.Controls.Add(chBox);

            chBox.AutoSize = true;
            chBox.Location = pt;
            chBox.Name = "checkBox1";
            chBox.Size = new System.Drawing.Size(78, 16);
            chBox.TabIndex = 0;
            chBox.Text = txt;
            chBox.UseVisualStyleBackColor = true;
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            int newId = Convert.ToInt32(id.Text);
            m_job.id = newId;
            m_job.name = name.Text;
            m_job.note = note.Text;
            if (ip.Text == "")
                m_job.ip = null;
            else
                m_job.ip = ip.Text;

            if (ComboJobList.Text.Trim() != "")
            {
                var job_leader = db.Job.FirstOrDefault(x => x.name == ComboJobList.Text);
                if (job_leader != null)
                    m_job.leaderId = job_leader.id;
            }

            if (ComboDeparts.Text != "")
            {
                var depart = db.Department.FirstOrDefault(x => x.name == ComboDeparts.Text);
                if (depart != null)
                    m_job.departId = depart.id;
            }

            m_Authority.jobId = newId;
            foreach (Control p in cts)
            {
                foreach (Control ct in p.Controls)
                {
                    if (ct.GetType() == typeof(CheckBox))
                    {
                        var pro = m_Authority.GetType().GetProperty(ct.Text);
                        if (pro == null)
                            continue;

                        if (BathClass.getAuthority(db, LogIn.m_User, ct.Text))
                            pro.SetValue(m_Authority, ((CheckBox)ct).Checked, null);
                    }
                }
            }

            if (newJob)
            {
                db.Job.InsertOnSubmit(m_job);
                db.Authority.InsertOnSubmit(m_Authority);
            }

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
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
            foreach (Control c in seat.Controls)
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
                    foreach (Control cc in c.Controls)
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

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
