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
    public partial class TableCashierCheckForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime lastTime;//起始时间
        private DateTime thisTime;//终止时间
        private bool input_order_id;

        private bool use_pad;

        //构造函数
        public TableCashierCheckForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void TableCashierCheckForm_Load(object sender, EventArgs e)
        {
            input_order_id = MConvert<bool>.ToTypeOrDefault(LogIn.options.录单输入单据编号, false);
            use_pad = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false);

            sp2.SplitterDistance = sp.Panel2.Height * 2 / 3;
            get_clear_table_time();
            dgvActList_Show();
        }

        //获取夜审时间
        private void get_clear_table_time()
        {
            bool today = false;
            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == startTime.Value.AddDays(1).Date);
            if (ct == null)
            {
                today = true;
                var lct = db.ClearTable.OrderByDescending(x => x.id).FirstOrDefault();
                if (lct == null)
                    lastTime = DateTime.Parse("2013-01-01");
                else
                    lastTime = lct.clearTime;
                thisTime = DateTime.Now;
                return;
            }

            thisTime = ct.clearTime;
            var lct1 = db.ClearTable.Where(x => x.id < ct.id).OrderByDescending(x => x.id).FirstOrDefault();
            if (lct1 == null)
                lastTime = DateTime.Parse("2013-01-01");
            else
                lastTime = lct1.clearTime;

            if (today)
            {
                var cpt = db.CashPrintTime.Where(x => x.time > lastTime);
                printTimeList.Items.Clear();
                foreach (var x in cpt)
                    printTimeList.Items.Add(x.time.ToString("yyyy-MM-dd HH:mm"));
            }
            else
            {
                var cpt = db.CashPrintTime.Where(x => x.time > lastTime ||Math.Abs((x.time - thisTime).TotalMinutes) < 10);
                printTimeList.Items.Clear();
                foreach (var x in cpt)
                    printTimeList.Items.Add(x.time.ToString("yyyy-MM-dd HH:mm"));
            }
            

            return;
        }
        
        //账单情况列表
        private void dgvActList_Show()
        {
            if (cbBillId.Checked)
            {
                if (billId.Text == "")
                {
                    BathClass.printErrorMsg("需要输入单据编号！");
                    return;
                }

                var order = db.HisOrders.FirstOrDefault(x => x.billId == billId.Text);
                if (order == null)
                {
                    BathClass.printErrorMsg("未找到该单据号对应账单！");
                    return;
                }
                if (order.deleteEmployee != null)
                {
                    BathClass.printErrorMsg("该单据号对应录单已删除删除授权为："+order.deleteEmployee);
                    return;
                }
                if (order.accountId == null)
                {
                    BathClass.printErrorMsg("该单据号对应录单未结账");
                    return;
                }

                var act = db.Account.FirstOrDefault(x=>x.id==order.accountId);
                if (act.abandon != null)
                {
                    BathClass.printErrorMsg("该单据号对应账单已经补救，补救授权为："+act.abandon);
                    return;
                }
                var acts = db.Account.Where(x => x.id == order.accountId);
                dgvActList.DataSource = from x in acts
                                        orderby x.payTime
                                        select new
                                        {
                                            账单号 = x.id,
                                            手牌号 = string.Join("\n", x.text.Split('|').ToArray()),
                                            收银时间 = x.payTime,
                                            收银员 = x.payEmployee
                                        };
            }
            else if (cboxSystemId.Checked)
            {
                var id = MConvert<int>.ToTypeOrDefault(systemId.Text.Trim(), -1);
                var acts = db.Account.Where(x => x.id == id);
                dgvActList.DataSource = from x in acts
                                        orderby x.payTime
                                        select new
                                        {
                                            账单号 = x.id,
                                            手牌号 = string.Join("\n", x.text.Split('|').ToArray()),
                                            收银时间 = x.payTime,
                                            收银员 = x.payEmployee
                                        };
            }
            else
            {
                var accountList = db.Account.Where(x => x.payTime >= lastTime && x.payTime <= thisTime && x.abandon == null);
                if (cboxPrint.Checked && printTimeList.Text != "")
                {
                    var this_print_time = DateTime.Parse(printTimeList.Text);
                    var lpt = db.CashPrintTime.Where(x => x.time < this_print_time).OrderByDescending(x => x.time).FirstOrDefault();
                    var last_print_time = DateTime.Parse("2013-01-01 00:00:00");
                    if (lpt != null)
                        last_print_time = lpt.time;

                    accountList = db.Account.Where(x => x.payTime >= last_print_time && x.payTime <= this_print_time && x.abandon == null);
                }
                if (cboxSeat.Checked)
                    accountList = accountList.Where(x => x.text.Contains(seat.Text));
                    
                dgvActList.DataSource = from x in accountList
                                        orderby x.payTime
                                        select new
                                        {
                                            账单号 = x.id,
                                            手牌号 = string.Join("\n", x.text.Split('|').ToArray()),
                                            收银时间 = x.payTime,
                                            收银员 = x.payEmployee
                                        };
            }
            BathClass.set_dgv_fit(dgvActList);
        }

        //消费情况列表
        private void dgvOrders_Show()
        {
            if (dgvActList.CurrentCell == null)
                return;

            int act_id = Convert.ToInt32(dgvActList.CurrentRow.Cells[0].Value);
            var act = db.Account.FirstOrDefault(x=>x.id==act_id);

            var orders = db.HisOrders.Where(x => x.accountId == act_id && x.deleteEmployee == null && x.paid);
            dgvOrders.DataSource = from x in orders
                                   orderby x.systemId, x.inputTime
                                   select new
                                   {
                                       系统账号 = x.systemId,
                                       台号 = x.text,
                                       房间=x.roomId,
                                       单据号 = x.billId,
                                       录入员工 = x.inputEmployee,
                                       项目名称 = x.menu,
                                       单价 = db.Menu.FirstOrDefault(y => y.name == x.menu) == null ? 0 : db.Menu.FirstOrDefault(y => y.name == x.menu).price,
                                       数量 = x.number,
                                       金额 = (x.priceType == "每小时") ? (Math.Ceiling((act.payTime - x.inputTime).TotalHours) * x.money) : x.money,
                                       技师号 = x.technician,
                                       消费时间 = x.inputTime
                                   };

            dgvOrders.Columns[2].Visible = use_pad;
            dgvOrders.Columns[3].Visible = input_order_id;
            BathClass.set_dgv_fit(dgvOrders);
        }

        //结账情况列表
        private void dgvAct_Show()
        {
            dgvAct.Rows.Clear();
            if (dgvActList.CurrentCell == null)
                return;

            string[] row = new string[5];
            string id = dgvActList.CurrentRow.Cells[0].Value.ToString();
            var act = db.Account.FirstOrDefault(x => x.id.ToString() == id);
            if (act.cash != null)
            {
                row[0] = "现金";
                row[1] = act.cash.ToString();
                dgvAct.Rows.Add(row);
            }

            if (act.bankUnion != null)
            {
                row[0] = "银联";
                row[1] = act.bankUnion.ToString();
                dgvAct.Rows.Add(row);
            }

            if (act.creditCard != null)
            {
                row[0] = "会员卡";
                row[1] = act.creditCard.ToString();
                row[2] = act.memberId;
                dgvAct.Rows.Add(row);
                row[2] = "";
            }

            if (act.coupon != null)
            {
                row[0] = "优惠券";
                row[1] = act.coupon.ToString();
                dgvAct.Rows.Add(row);
            }

            if (act.groupBuy != null)
            {
                row[0] = "团购优惠";
                row[1] = act.groupBuy.ToString();
                dgvAct.Rows.Add(row);
            }

            if (act.zero != null)
            {
                row[0] = "挂账";
                row[1] = act.zero.ToString();
                row[4] = act.name;
                dgvAct.Rows.Add(row);
                row[4] = "";
            }

            if (act.server != null)
            {
                row[0] = "签字免单";
                row[1] = act.server.ToString();
                row[3] = act.serverEmployee;
                dgvAct.Rows.Add(row);
                row[3] = "";
            }
            if (act.deducted != null)
            {
                row[0] = "扣卡";
                row[1] = act.deducted.ToString();
                row[2] = act.memberId;
                dgvAct.Rows.Add(row);
                row[2] = "";
            }

            if (act.changes != null)
            {
                row[0] = "找零";
                row[1] = act.changes.ToString();
                dgvAct.Rows.Add(row);
            }

            if (act.wipeZero != null)
            {
                row[0] = "抹零";
                row[1] = act.wipeZero.ToString();
                dgvAct.Rows.Add(row);
            }
            BathClass.set_dgv_fit(dgvAct);
        }

        //查询
        private void toolFind_Click(object sender, EventArgs e)
        {
            try
            {
                dgvActList_Show();
            }
            catch (System.Exception ex)
            {
                GeneralClass.printErrorMsg(ex.Message);
            }
        }

        //打印
        private void toolPrint_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgvActList, "收银单据查询", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //选中某行
        private void dgvActList_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
               dgvOrders_Show();
               dgvAct_Show();
            }
            catch (System.Exception ex)
            {
                GeneralClass.printErrorMsg(ex.Message);
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
                case Keys.F3:
                    toolFind_Click(null, null);
                    break;
                case Keys.F5:
                    toolPrint_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //手牌
        private void cboxSeat_CheckedChanged(object sender, EventArgs e)
        {
            seat.Enabled = cboxSeat.Checked;
        }

        //账单号
        private void cboxSystemId_CheckedChanged(object sender, EventArgs e)
        {
            systemId.Enabled = cboxSystemId.Checked;
        }

        private void seat_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //单据号
        private void cbBillId_CheckedChanged(object sender, EventArgs e)
        {
            billId.Enabled = cbBillId.Checked;
        }

        //交班时间
        private void cboxPrint_CheckedChanged(object sender, EventArgs e)
        {
            get_clear_table_time();
            printTimeList.Enabled = cboxPrint.Checked;
        }

        private void startTime_ValueChanged(object sender, EventArgs e)
        {
            get_clear_table_time();
        }
    }
}
