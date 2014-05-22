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
    public partial class CustomerManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public CustomerManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CustomerManagementForm_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        //显示清单
        public void dgv_show()
        {
            dgv.DataSource = from x in db.Customer
                             orderby x.id
                             select new
                             {
                                 编号 = x.id,
                                 名称 = x.name,
                                 联系人 = x.contact,
                                 地址 = x.address,
                                 电话 = x.phone,
                                 手机 = x.mobile,
                                 传真 = x.fax,
                                 QQ = x.qq,
                                 邮件 = x.email,
                                 挂账金额 = x.money,
                                 注册日期 = x.registerDate,
                                 备注 = x.note
                             };
        }

        //新增
        private void addTool_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm(db, null);
            if (customerForm.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //删除
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            db.Customer.DeleteOnSubmit(db.Customer.FirstOrDefault(s => s.id.ToString() == dgv.CurrentRow.Cells[0].Value.ToString()));
            db.SubmitChanges();
            dgv_show();
        }

        //编辑
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            string id = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();
            var customer = db.Customer.FirstOrDefault(x => x.id.ToString() == id);

            CustomerForm editCustomer = new CustomerForm(db, customer);
            if (editCustomer.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "客户信息报表", false, "");
        }

        //退出
        private void exitTool_Click(object sender, EventArgs e)
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
                case Keys.F1:
                    addTool_Click(null, null);
                    break;
                case Keys.F2:
                    delTool_Click(null, null);
                    break;
                case Keys.F3:
                    editTool_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "客户信息报表", false, "");
                    break;
                default:
                    break;
            }
        }
    }
}
