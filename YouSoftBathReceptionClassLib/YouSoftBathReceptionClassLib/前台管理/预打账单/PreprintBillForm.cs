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

namespace YouSoftBathReception
{
    public partial class PreprintBillForm : Form
    {
        //成员变量
        BathDBDataContext db;

        //构造函数
        public PreprintBillForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void TransferSelectForm_Load(object sender, EventArgs e)
        {
        }

        private void TransferSelectForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void rBtnSeat_CheckedChanged(object sender, EventArgs e)
        {
            seatBox.Enabled = rBtnSeat.Checked;
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbtnAll.Checked)
            {
                var seats = db.Seat.Where(x => x.status == 2 || x.status == 6 || x.status == 7 || x.status == 8);
                foreach (var seat in seats)
                {
                    print_seat_bill(seat);
                }
            }
            else if (radioshoe.Checked)
            {
                var seats = db.Seat.Where(x => x.status == 2 || x.status == 6 || x.status == 7);
                PrintCheckSeatBills.Print_DataGridView(seats.Select(x => x.text).ToList(), db.Options.FirstOrDefault().companyName);
            }
            else if (rBtnSeat.Checked)
            {
                var seat = db.Seat.FirstOrDefault(x => x.text == seatBox.Text);
                if (seat==null)
                {
                    BathClass.printErrorMsg("手牌号不存在");
                    return;
                }
                if (seat.status != 2 && seat.status != 6 && seat.status != 7 && seat.status != 8)
                {
                    BathClass.printErrorMsg("手牌号不在使用中，不能预打账单");
                    return;
                }
                print_seat_bill(seat);
            }
            else if (rBtnTechAll.Checked)//预打技师对账单汇总
            {
                var form = new TechAllForm();
                form.ShowDialog();
            }
            else if (rBtnTechDetails.Checked)//预打技师对账单明细
            {
                var form = new TechDetailsForm();
                form.ShowDialog();
            }
        }

        private void print_seat_bill(Seat seat)
        {
            DataGridView dgv = new DataGridView();

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "手牌号";
            dgv.Columns.Add(col);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "项目名称";
            dgv.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "技师";
            dgv.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "单价";
            dgv.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "数量";
            dgv.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "金额";
            dgv.Columns.Add(col5);

            double money_pay = 0;
            var orders = db.Orders.Where(x => x.systemId == seat.systemId && !x.paid && x.deleteEmployee == null);
            foreach (var order in orders)
            {
                string[] row = new string[6];
                row[0] = order.text;
                row[1] = order.menu;
                row[2] = order.technician;

                var menu = db.Menu.FirstOrDefault(x => x.name == order.menu);
                if (menu != null)
                    row[3] = menu.price.ToString();

                row[4] = order.number.ToString();

                double money_tmp = 0;
                if (order.priceType == "每小时")
                {
                    row[3] = order.money.ToString() + "/时";
                    money_tmp = Math.Ceiling((GeneralClass.Now - order.inputTime).TotalHours) * order.money;
                }
                else
                {
                    money_tmp = order.money;
                }
                money_pay += money_tmp;
                row[5] = money_tmp.ToString();
                dgv.Rows.Add(row);
            }
            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            List<CSeat> seats = new List<CSeat>();
            var dao = new DAO(LogIn.connectionString);
            seats.Add(dao.get_seat("text='" + seat.text + "'"));

            PrintSeatBill.Print_DataGridView(seats, null, "预打账单", dgv, printCols, money_pay.ToString(),
                LogIn.options.companyName);
        }

        private void seatBox_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
