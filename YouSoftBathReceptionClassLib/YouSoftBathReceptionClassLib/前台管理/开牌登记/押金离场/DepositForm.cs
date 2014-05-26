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
using YouSoftUtil;

namespace YouSoftBathReception
{
    public partial class DepositForm : Form
    {
        //成员变量
        private DAO dao;
        private BathDBDataContext db;
        private CSeat m_Seat;

        //构造函数
        public DepositForm(CSeat seat)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seat = seat;
            dao = new DAO(LogIn.connectionString);
            //m_Seat = db.Seat.FirstOrDefault(x => x.systemId == seat.systemId);
            InitializeComponent();
        }

        //显示台位消费信息
        public void dgvExpense_show()
        {
            dgvExpense.DataSource = from o in db.Orders
                                    where (o.systemId == m_Seat.systemId && o.deleteEmployee == null && !o.paid)
                                    orderby o.text, o.inputTime
                                    select new
                                    {
                                        编号 = o.id,
                                        手牌号 = o.text,
                                        项目名称 = o.menu,
                                        单价 = db.Menu.FirstOrDefault(x => x.name == o.menu) == null ? 0 : db.Menu.FirstOrDefault(x => x.name == o.menu).price,
                                        数量 = o.number,
                                        金额 = o.money,
                                        技师 = o.technician,
                                        服务类型 = o.techType,
                                        输入时间 = o.inputTime.ToLongTimeString(),
                                        输入工号 = o.inputEmployee
                                    };
            foreach (DataGridViewRow dgvRow in dgvExpense.Rows)
            {
                if (dgvRow.Cells[1].Value.ToString().Contains("套餐"))
                    dgvRow.DefaultCellStyle.BackColor = Color.Red;
            }

            int w = dgvExpense.Width;
            int w0 = 0;
            foreach (DataGridViewColumn c in dgvExpense.Columns)
                w0 += c.Width;

            if (w < w0) return;

            dgvExpense.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            int w1 = (w - w0) / 10;
            foreach (DataGridViewColumn c in dgvExpense.Columns)
                c.Width += w1;
        }

        //对话框载入
        private void DepositForm_Load(object sender, EventArgs e)
        {
            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);     
            dgvExpense_show();
            depositAready.Text = MConvert<int>.ToTypeOrDefault(m_Seat.deposit, 0).ToString();
            moneyExpense.Text = dao.get_seat_expense("text='" + m_Seat.text + "'").ToString();
            //moneyExpense.Text = BathClass.get_seat_expense(m_Seat, LogIn.connectionString).ToString();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            int money = 0;
            int money_a = MConvert<int>.ToTypeOrDefault(m_Seat.deposit, 0);
            if (!int.TryParse(deposit.Text, out money))
            {
                BathClass.printErrorMsg("押金应输入整数");
                return;
            }

            if (money + money_a <= Convert.ToDouble(moneyExpense.Text))
            {
                deposit.Focus();
                BathClass.printErrorMsg("押金金额必须大于消费金额!");
                return;
            }

            string cmd_str = @"update [Seat] set deposit=" + (money_a + money).ToString() + ", status=7 where text='" + m_Seat.text + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("押金离场失败，请重试!");
                return;
            }
            m_Seat.deposit = money_a + money;
            //m_Seat.status = 7;
            PrintDepositReceipt.Print_DataGridView(m_Seat, m_Seat.deposit.ToString(), moneyExpense.Text, db.Options.FirstOrDefault().companyName);
            //db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void DepositForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void deposit_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void deposit_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //退换押金取消押金离场状态
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (BathClass.printAskMsg("确认退换押金？") != DialogResult.Yes)
                return;

            string cmd_str = @"update [Seat] set deposit=null, status=2 where text='" + m_Seat.text + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("退换押金失败，请重试!");
                return;
            }
            //m_Seat.deposit = null;
            //m_Seat.status = 2;
            //db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

    }
}
