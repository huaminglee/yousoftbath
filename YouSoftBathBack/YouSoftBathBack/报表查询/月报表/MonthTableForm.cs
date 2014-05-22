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
    public partial class MonthTableForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime thisTime;
        private DateTime lastTime;

        //构造函数
        public MonthTableForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MonthTableForm_Load(object sender, EventArgs e)
        {
            int year = BathClass.Now(LogIn.connectionString).Year;
            yearBox.Items.Add(year);
            for (int i = 1; i < 11; i++ )
            {
                yearBox.Items.Add(year - i);
            }
            yearBox.SelectedIndex = 0;
            monthBox.Text = BathClass.Now(LogIn.connectionString).Month.ToString();
        }

        //获取夜审时间
        private bool get_clear_table_time(DateTime dt)
        {
            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == dt.AddDays(1).Date);
            if (ct == null)
            {
                return false;
            }

            thisTime = ct.clearTime;
            var lct = db.ClearTable.Where(x => x.id < ct.id).OrderByDescending(x => x.id).FirstOrDefault();
            if (lct == null)
                lastTime = DateTime.Parse("2013-01-01");
            else
                lastTime = lct.clearTime;

            return true;
        }

        //显示信息
        private void dgv_show()
        {
            dgv.Rows.Clear();
            DateTime tempDay = DateTime.Parse(yearBox.Text + "-" + monthBox.Text.PadLeft(2, '0') + "-01" + " 00:00:00");
            DateTime lastDay = tempDay.AddMonths(1).AddDays(-1);

            string[] tr = { monthBox.Text + "月", "合计", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            while (tempDay <= lastDay)
            {
                string[] row = new string[13];
                tempDay = tempDay.AddDays(1);
                if (!get_clear_table_time(tempDay.AddDays(-1)))
                    continue;

                var tempList = db.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                var tmp_cards = db.CardSale.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                var card_cash = tmp_cards.Where(x => x.cash != null);
                var card_bank = tmp_cards.Where(x => x.bankUnion != null);

                row[0] = lastTime.ToString();//开始时间
                row[1] = thisTime.ToString();//结束时间

                var q = tempList.Where(x => x.cash != null);
                if (q.Any())
                {
                    double cash = Convert.ToDouble(q.Sum(x => x.cash));
                    row[2] = cash.ToString();//现金营业额
                    tr[2] = (Convert.ToDouble(tr[2]) + cash).ToString();
                    var qc = tempList.Where(x => x.changes != null);
                    if (qc.Count() != 0)
                    {
                        var changes = qc.Sum(x => x.changes);
                        tr[2] = (Convert.ToDouble(tr[2]) - changes).ToString();
                        row[2] = (Convert.ToDouble(row[2]) - changes).ToString();//找零
                    }
                }

                row[6] = row[2];//总收入
                tr[6] = (Convert.ToDouble(tr[6]) + Convert.ToDouble(row[2])).ToString();
                
                q = tempList.Where(x => x.bankUnion != null);
                if (q.Count() != 0)
                {
                    var tmp_q = q.Sum(x => x.bankUnion);
                    row[3] = tmp_q.ToString();
                    tr[3] = (Convert.ToDouble(tr[3]) + tmp_q).ToString();
                    tr[6] = (Convert.ToDouble(tr[6]) + tmp_q).ToString();
                    row[6] = (Convert.ToDouble(row[6]) + tmp_q).ToString();
                }

                row[4] = "0";
                if (card_cash.Any())
                {
                    var cash_card = card_cash.Sum(x => x.cash);
                    row[4] = cash_card.ToString();//现金售卡
                    tr[4] = (Convert.ToDouble(tr[4]) + cash_card).ToString();
                    tr[6] = (Convert.ToDouble(tr[6]) + cash_card).ToString();
                    row[6] = (Convert.ToDouble(row[6]) + cash_card).ToString();
                }

                row[5] = "0";
                if (card_bank.Any())
                {
                    var bank_card = card_bank.Sum(x => x.bankUnion);
                    row[5] = bank_card.ToString();//银联售卡
                    tr[5] = (Convert.ToDouble(tr[5]) + bank_card).ToString();
                    tr[6] = (Convert.ToDouble(tr[6]) + bank_card).ToString();
                    row[6] = (Convert.ToDouble(row[6]) + bank_card).ToString();

                }

                row[7] = "0";
                q = tempList.Where(x => x.creditCard != null);//储值卡
                if (q.Any())
                {
                    var tmp_q = q.Sum(x => x.creditCard);
                    row[7] = tmp_q.ToString();
                    tr[7] = (Convert.ToDouble(tr[7]) + tmp_q).ToString();
                }

                row[8] = "0";
                q = tempList.Where(x => x.coupon != null);//优惠券
                if (q.Any())
                {
                    var tmp_q = q.Sum(x => x.coupon);
                    row[8] = tmp_q.ToString();
                    tr[8] = (Convert.ToDouble(tr[8]) + tmp_q).ToString();
                }

                row[9] = "0";
                q = tempList.Where(x => x.groupBuy != null);//团购
                if (q.Any())
                {
                    var tmp_q = q.Sum(x => x.groupBuy);
                    row[9] = tmp_q.ToString();
                    tr[9] = (Convert.ToDouble(tr[9]) + tmp_q).ToString();
                }

                row[10] = "0";
                q = tempList.Where(x => x.zero != null);//挂账
                if (q.Any())
                {
                    var tmp_q = q.Sum(x => x.zero);
                    row[10] = tmp_q.ToString();
                    tr[10] = (Convert.ToDouble(tr[10]) + tmp_q).ToString();
                }

                row[11] = "0";
                q = tempList.Where(x => x.server != null);//招待
                if (q.Any())
                {
                    var tmp_q = q.Sum(x => x.server);
                    row[11] = tmp_q.ToString();
                    tr[11] = (Convert.ToDouble(tr[11]) + tmp_q).ToString();
                }

                row[12] = "0";
                q = tempList.Where(x => x.wipeZero != null);//抹零
                if (q.Any())
                {
                    var tmp_q = q.Sum(x => x.wipeZero);
                    row[12] = tmp_q.ToString();
                    tr[12] = (Convert.ToDouble(tr[12]) + tmp_q).ToString();
                }

                dgv.Rows.Add(row);
            }
            dgv.Rows.Add(tr);
            BathClass.set_dgv_fit(dgv);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
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
            PrintDGV.Print_DataGridView(dgv, "月报表", false, "");
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
                    findTool_Click(null, null);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "月报表", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }
    }
}
