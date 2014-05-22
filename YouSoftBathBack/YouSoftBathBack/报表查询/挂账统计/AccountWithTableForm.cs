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
    //挂账单位查询
    public partial class AccountWithTableForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public AccountWithTableForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void AccountWithTableForm_Load(object sender, EventArgs e)
        {
            dgvCustomer_show();
        }

        //显示信息
        private void dgvCustomer_show()
        {
            dgvCustomerList.Rows.Clear();
            foreach (var c in db.Customer)
            {
                double ts = 0;
                var acts = db.Account.Where(x => x.zero != null && x.abandon == null && x.name == c.id.ToString());
                if (acts.Any())
                    ts = acts.Sum(x => x.zero).Value;

                double paid = 0;
                var cash = db.CustomerPays.Where(x => x.customerId == c.id && x.cash != null);
                if (cash.Any())
                    paid += cash.Sum(x => x.cash).Value;

                var bank = db.CustomerPays.Where(x => x.customerId == c.id && x.bank != null);
                if (bank.Any())
                    paid += bank.Sum(x => x.bank).Value;

                dgvCustomerList.Rows.Add(c.name, ts, paid, ts - paid, c.contact, c.mobile, c.phone, c.address, c.note);
            }
            BathClass.set_dgv_fit(dgvCustomerList);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            dgvCustomer_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgvCustomerList);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgvCustomerList, "客户挂账统计", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
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
                case Keys.F3:
                    dgvCustomer_show();
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgvCustomerList, "客户挂账统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgvCustomerList);
                    break;
                default:
                    break;
            }
        }

        private void dgvCustomerList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell == null)
                return;

            var c = db.Customer.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            dgvActs_show(c);
            dgvPays_show(c);
        }

        private void dgvActs_show(Customer c)
        {
            dgvActs.Rows.Clear();

            var acts = db.Account.Where(x => x.zero != null && x.abandon == null && x.name == c.id.ToString());
            foreach (var a in acts)
            {
                dgvActs.Rows.Add(a.id, a.text, a.payTime, a.payEmployee, a.zero);
            }
            BathClass.set_dgv_fit(dgvActs);
        }

        private void dgvPays_show(Customer c)
        {
            dgvCustomerPays.Rows.Clear();

            var pays = db.CustomerPays.Where(x => x.customerId == c.id);
            foreach (var p in pays)
            {
                dgvCustomerPays.Rows.Add(p.id, p.cash, p.bank, p.date, p.payEmployee, p.note);
            }
            BathClass.set_dgv_fit(dgvCustomerPays);
        }

        //新增客户
        private void addTool_Click(object sender, EventArgs e)
        {
            int rindex = -1;
            int cindex = -1;
            if (dgvCustomerList.CurrentCell != null)
            {
                rindex = dgvCustomerList.CurrentCell.RowIndex;
                cindex = dgvCustomerList.CurrentCell.ColumnIndex;
            }

            var form = new CustomerForm(db, null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvCustomer_show();
                if (rindex != -1 && cindex != -1)
                    dgvCustomerList.CurrentCell = dgvCustomerList[cindex, rindex];
            }
        }

        //删除客户
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择行!");
                return;
            }

            var p = db.Customer.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            if (db.Account.Any(x => x.name == p.id.ToString() && x.abandon==null) || db.CustomerPays.Any(x => x.customerId == p.id))
            {
                BathClass.printErrorMsg("与客户已有账务往来，不能删除!");
                return;
            }

            if (BathClass.printAskMsg("确认删除客户" + p.name) != DialogResult.Yes)
                return;

            db.Customer.DeleteOnSubmit(p);
            db.SubmitChanges();
            dgvCustomer_show();

        }

        //编辑客户
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择行!");
                return;
            }

            int rindex = dgvCustomerList.CurrentCell.RowIndex;
            int cindex = dgvCustomerList.CurrentCell.ColumnIndex;

            var p = db.Customer.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            var form = new CustomerForm(db, p);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvCustomer_show();
                dgvCustomerList.CurrentCell = dgvCustomerList[cindex, rindex];
            }
        }

        //销账
        private void toolPayAccount_Click(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell == null)
                return;

            var _customer = db.Customer.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            var form = new payAccountForm(db, _customer);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            int r = dgvCustomerList.CurrentCell.RowIndex;
            int c = dgvCustomerList.CurrentCell.ColumnIndex;
            dgvCustomer_show();
            dgvCustomerList.CurrentCell = dgvCustomerList[c, r];

            dgvPays_show(_customer);
        }

        //删除付款
        private void delPayTool_Click(object sender, EventArgs e)
        {
            if (dgvCustomerPays.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选择客户付款信息!");
                return;
            }

            if (BathClass.printAskMsg("确认删除客户付款信息?") != DialogResult.Yes)
                return;

            var _customer = db.Customer.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            var pp = db.CustomerPays.FirstOrDefault(x => x.id.ToString() == dgvCustomerPays.CurrentRow.Cells[0].Value.ToString());
            db.CustomerPays.DeleteOnSubmit(pp);
            db.SubmitChanges();

            int r = dgvCustomerList.CurrentCell.RowIndex;
            int c = dgvCustomerList.CurrentCell.ColumnIndex;
            dgvCustomer_show();
            dgvCustomerList.CurrentCell = dgvCustomerList[c, r];
            dgvPays_show(_customer);
        }
    }
}
