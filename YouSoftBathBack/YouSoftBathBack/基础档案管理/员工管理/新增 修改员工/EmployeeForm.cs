using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftUtil;
using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class EmployeeForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Employee m_Employee = new Employee();
        private bool newEmployee = true;
        private EmployeeManagementForm m_Form;

        //构造函数
        public EmployeeForm(BathDBDataContext dc, Employee employee, EmployeeManagementForm form)
        {
            db = dc;
            m_Form = form;
            if (employee != null)
            {
                newEmployee = false;
                m_Employee = employee;
            }

            InitializeComponent();
        }

        //对话框载入
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            job.Items.AddRange(db.Job.Select(x => x.name).ToArray());

            if (newEmployee)
            {
                int maxId = 1000;
                if (db.Employee.Count() != 0)
                {
                    var res = db.Employee.Max(c => Convert.ToInt32(c.id));
                    maxId = res + 1;
                }
                id.Text = maxId.ToString();
                gender.Text = "男";
                job.Text = job.Items[0].ToString();
                name.Focus();
            }
            else
            {
                id.Enabled = false;
                id.Text = m_Employee.id.ToString();
                name.Text = m_Employee.name;
                cardId.Text = m_Employee.cardId;
                gender.Text = m_Employee.gender;
                birthday.Value = m_Employee.birthday;
                job.Text = db.Job.FirstOrDefault(x => x.id == m_Employee.jobId).name;
                salary.Text = m_Employee.salary;
                phone.Text = m_Employee.phone;
                entryDate.Value = m_Employee.entryDate;
                email.Text = m_Employee.email;
                address.Text = m_Employee.address;
                idCard.Text = m_Employee.idCard;
                onDuty.Checked = m_Employee.onDuty;
                note.Text = m_Employee.note;
            }
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || phone.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入姓名和电话");
                return;
            }

            if (newEmployee)
            {
                if (db.Employee.FirstOrDefault(x=>x.id.ToString()==id.Text) != null)
                {
                    GeneralClass.printErrorMsg("已经存在此工号");
                    id.SelectAll();
                    id.Focus();
                    return;
                }
            }
            m_Employee.id = id.Text;
            m_Employee.idCard = idCard.Text;
            m_Employee.name = name.Text;
            m_Employee.cardId = cardId.Text;
            m_Employee.jobId = db.Job.FirstOrDefault(x => x.name == job.Text).id;
            m_Employee.onDuty = onDuty.Checked;
            m_Employee.phone = phone.Text;
            m_Employee.note = note.Text;
            m_Employee.salary = salary.Text;
            m_Employee.gender = gender.Text;
            m_Employee.entryDate = Convert.ToDateTime(entryDate.Value.ToShortDateString());
            m_Employee.email = email.Text;
            m_Employee.birthday = Convert.ToDateTime(birthday.Value.ToShortDateString());
            m_Employee.address = address.Text;

            if (newEmployee)
            {
                m_Employee.password = IOUtil.GetMD5("12345678");
                db.Employee.InsertOnSubmit(m_Employee);

                db.SubmitChanges();
                m_Form.dgv_show();

                id.Text = (m_Employee.id + 1).ToString();
                name.Text = "";
                cardId.Text = "";
                phone.Text = "";
                onDuty.Checked = true;
                salary.Text = "";
                idCard.Text = "";
                email.Text = "";
                address.Text = "";
                note.Text = "";
                //gender.Text = "男";
                //job.Text = job.Items[0].ToString();
                name.Focus();
                m_Employee = new Employee();
                return;
            }
            else
            {
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
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

        private void phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void salary_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(salary, e);
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
