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
using System.Data.SqlClient;
using System.IO;
using System.Threading;

namespace YouSoftBathReception
{
    public partial class TableDepositForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private string macAdd;
        private List<int> idList;
        private DateTime lastTime;
        private DateTime thisTime;

        //构造函数
        public TableDepositForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CashierForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.Size = new Size(this.Size.Width, Screen.GetWorkingArea(this).Height);
                this.Location = new Point(0, 0);
                dgv_show();
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
                this.Close();
            }
        }

        //查询
        private void dgv_show()
        {
            dgv.Rows.Clear();
            var seats = db.Seat.Where(x => x.status == 2);
            seats = seats.Where(x => db.SeatType.FirstOrDefault(y => y.id == x.typeId).department == "客房部");
            seats = seats.OrderBy(x => x.openTime);

            double deposit = 0;
            double depositBank = 0;
            foreach (var seat in seats)
            {
                double tmp_deposit = MConvert<double>.ToTypeOrDefault(seat.deposit, 0);
                double tmp_deposit_bank = MConvert<double>.ToTypeOrDefault(seat.depositBank, 0);
                dgv.Rows.Add(seat.text, seat.name, seat.phone, tmp_deposit, tmp_deposit_bank);
                deposit += tmp_deposit;
                depositBank += tmp_deposit_bank;
            }

            dgv.Rows.Add();
            dgv.Rows.Add("现金押金汇总", deposit);
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            dgv.Rows.Add("银联预授汇总", depositBank);
            dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
            BathClass.set_dgv_fit(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.Rows.Count == 0)
                    dgv_show();

                var dc = new BathDBDataContext(LogIn.connectionString);
                string companyName = LogIn.options.companyName;
                List<string> printColumns = new List<string>();
                foreach (DataGridViewColumn dgvCol in dgv.Columns)
                {
                    printColumns.Add(dgvCol.HeaderText);
                }

                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                PrintReceipt.Print_DataGridView("客房押金表", dgv, now, now, companyName);
                //BathClass.set_dgv_fit(dgv);
                dgv.Rows.Clear();
            }
            catch
            {
                BathClass.printErrorMsg("打印出错，请重新打印!");
            }
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
                case Keys.F5:
                    printTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}
