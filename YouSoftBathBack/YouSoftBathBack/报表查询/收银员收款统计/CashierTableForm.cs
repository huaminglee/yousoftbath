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
    public partial class CashierTableForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public CashierTableForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CashierTableForm_Load(object sender, EventArgs e)
        {
            cashier.Items.AddRange(db.Employee.Select(x => x.id.ToString()).ToArray());
            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            startTime.Value = Convert.ToDateTime(BathClass.Now(LogIn.connectionString).AddMonths(-1).ToShortDateString() + " 00:00:00");

            dgv_show();
        }

        //显示信息
        private void dgv_show()
        {
            dgv.Rows.Clear();
            var actList = db.Account.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value && x.abandon == null);
            //var cardList = db.CardSale.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value);

            if (cboxCashier.Checked)
            {
                //cardList = cardList.Where(x => x.payEmployee == cashier.Text);
                actList = actList.Where(x => x.payEmployee == cashier.Text);
            }

            var cashierList = actList.Select(x => x.payEmployee).Distinct();
            //cashierList.AddRange(cardList.Select(x => x.payEmployee).Distinct().ToList());
            //cashierList = cashierList.Distinct().ToList();
            foreach (string cashierId in cashierList)
            {
                string[] row = new string[13];
                var tempList = actList.Where(x => x.payEmployee == cashierId);
                //var tpCList = cardList.Where(x => x.payEmployee == cashierId);
                
                var cashier = db.Employee.FirstOrDefault(x => x.id.ToString() == cashierId);
                //if (cashier == null)
                //    continue;
                row[0] = cashierId;

                if (cashier != null)
                    row[1] = cashier.name;

                var q = tempList.Where(x => x.cash != null);
                if (q.Count() != 0)
                {
                    var qc = tempList.Where(x => x.changes != null);
                    if (qc.Count() != 0)
                        row[2] = (q.Sum(x => x.cash) - qc.Sum(x => x.changes)).ToString();
                    else
                        row[2] = q.Sum(x => x.cash).ToString();
                }
                else
                    row[2] = "0";

                row[3] = row[2];
                //var q1 = tpCList.Where(x => x.cash != null);
                //if (q1.Count() != 0)
                //    row[3] = (Convert.ToDouble(row[3]) + q1.Sum(x => x.cash)).ToString();


                q = tempList.Where(x => x.bankUnion != null);
                if (q.Count() != 0)
                    row[4] = q.Sum(x => x.bankUnion).ToString();
                else
                    row[4] = "0";

                //row[5] = row[4];
                //q1 = tpCList.Where(x => x.bankUnion != null);
                //if (q1.Count() != 0)
                //    row[5] = (Convert.ToDouble(row[5]) + q1.Sum(x => x.bankUnion)).ToString();


                q = tempList.Where(x => x.creditCard != null);
                if (q.Count() != 0)
                    row[6] = q.Sum(x => x.creditCard).ToString();

                q = tempList.Where(x => x.coupon != null);
                if (q.Count() != 0)
                    row[7] = q.Sum(x => x.coupon).ToString();

                q = tempList.Where(x => x.groupBuy != null);
                if (q.Count() != 0)
                    row[8] = q.Sum(x => x.groupBuy).ToString();

                q = tempList.Where(x => x.zero != null);
                if (q.Count() != 0)
                    row[9] = q.Sum(x => x.zero).ToString();

                q = tempList.Where(x => x.server != null);
                if (q.Count() != 0)
                    row[10] = q.Sum(x => x.server).ToString();

                q = tempList.Where(x => x.deducted != null);
                if (q.Count() != 0)
                    row[11] = q.Sum(x => x.deducted).ToString();

                q = tempList.Where(x => x.wipeZero != null);
                if (q.Count() != 0)
                    row[12] = q.Sum(x => x.wipeZero).ToString();

                dgv.Rows.Add(row);
            }
        }

        //显示相信信息
        private void dgvDetails_show()
        {
            dgvDetails.Rows.Clear();
            if (dgv.CurrentCell == null)
                return;

            var actList = db.Account.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value);
            //var cardList = db.CardSale.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value);

            string id = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();
            actList = actList.Where(x => x.payEmployee == id);
            //cardList = cardList.Where(x => x.payEmployee == id);

            foreach (var x in actList)
            {
                string[] row = {
                                   x.id.ToString(),
                                   x.payTime.ToString(),
                                   (x.cash - x.changes).ToString(),
                                   x.bankUnion.ToString(),
                                   x.creditCard.ToString(),
                                   x.memberId,
                                   x.coupon.ToString(),
                                   x.groupBuy.ToString(),
                                   x.zero.ToString(),
                                   x.name,
                                   x.server.ToString(),
                                   x.deducted.ToString(),
                                   x.wipeZero.ToString()
                               };
                dgvDetails.Rows.Add(row);
            }

            //foreach (var x in cardList)
            //{
            //    string[] row = {
            //                       "",
            //                       "",
            //                       x.payTime.ToString(),
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       "",
            //                       x.memberId,
            //                       x.cash.ToString(),
            //                       x.bankUnion.ToString()
            //                   };
            //    dgvDetails.Rows.Add(row);
            //}
        }

        //收银员工号
        private void cboxCashier_CheckedChanged(object sender, EventArgs e)
        {
            cashier.Enabled = cboxCashier.Checked;
            lCashier.Visible = cboxCashier.Checked;
            if (cashier.Text == "")
                lCashier.Text = "";
        }

        //选择收银员
        private void cashier_TextChanged(object sender, EventArgs e)
        {
            lCashier.Text = db.Employee.FirstOrDefault(x => x.id.ToString() == cashier.Text).name;
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
            PrintDGV.Print_DataGridView(dgv, "收银员收款统计", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            dgvDetails_show();
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
                    dgv_show();
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "收银员收款统计", false, "");
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
