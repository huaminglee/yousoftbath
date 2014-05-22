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
    public partial class ExpenseForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Expense m_Expense = new Expense();
        private bool newExpense = true;

        //构造函数
        public ExpenseForm(BathDBDataContext dc, Expense menu)
        {
            db = dc;
            if (menu != null)
            {
                newExpense = false;
                m_Expense = menu;
            }

            InitializeComponent();
        }

        //对话框载入
        private void ExpenseForm_Load(object sender, EventArgs e)
        {
            type.Items.AddRange(db.ExpenseType.Select(x => x.name).ToArray());
            if (type.Items.Count != 0)
                type.SelectedIndex = 0;

            var el = db.Employee.Select(x => x.name).ToArray();
            if (el.Count() != 0)
            {
                transactor.Items.AddRange(el);
                tableMaker.Items.AddRange(el);
                checker.Items.AddRange(el);
            }

            if (!newExpense)
            {
                name.Text = m_Expense.name;
                type.Text = db.ExpenseType.FirstOrDefault(x => x.id == m_Expense.typeId).name;
                money.Text = m_Expense.money.ToString();
                payType.Text = m_Expense.payType;
                toWhom.Text = m_Expense.toWhom;
                transactor.Text = m_Expense.transactor;
                tableMaker.Text = m_Expense.tableMaker;
                checker.Text = m_Expense.checker;
                note.Text = m_Expense.note;
                expenseDate.Value = m_Expense.expenseDate;
            }
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            string val = validateTextFilds();
            if (val != "OK")
            {
                GeneralClass.printErrorMsg(val);
                return;
            }

            //m_Expense.id = Convert.ToInt32(id.Text);
            m_Expense.name = name.Text;
            m_Expense.typeId = db.ExpenseType.FirstOrDefault(x => x.name == type.Text).id;
            m_Expense.money = Convert.ToDouble(money.Text);
            m_Expense.payType = payType.Text;
            m_Expense.toWhom = toWhom.Text;
            m_Expense.transactor = transactor.Text;
            m_Expense.tableMaker = tableMaker.Text;
            m_Expense.checker = checker.Text;
            m_Expense.note = note.Text;
            m_Expense.expenseDate = expenseDate.Value;

            if (newExpense)
            {
                m_Expense.inputDate = BathClass.Now(LogIn.connectionString);
                db.Expense.InsertOnSubmit(m_Expense);
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

        //检查数据
        private string validateTextFilds()
        {
            double d = 0.0;
            if (double.TryParse(money.Text, out d) == false)
            {
                money.SelectAll();
                money.Focus();
                return "金额应输入数字";
            }

            if (payType.Text == "")
            {
                payType.SelectAll();
                payType.Focus();
                return "付款方式未选择";
            }

            if (transactor.Text == "")
            {
                transactor.SelectAll();
                transactor.Focus();
                return "应输入经手人";
            }
            return "OK";
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void money_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
