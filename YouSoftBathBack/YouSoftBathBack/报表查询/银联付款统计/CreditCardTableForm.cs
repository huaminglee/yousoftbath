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
    public partial class CreditCardTableForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public CreditCardTableForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CreditCardTableForm_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            startTime.Value = Convert.ToDateTime(BathClass.Now(LogIn.connectionString).AddMonths(-1).ToShortDateString() + " 00:00:00");

            dgv_show();
        }

        //显示信息
        private void dgv_show()
        {
            var actList = db.Account.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value && x.abandon == null);
            actList = actList.Where(x => x.bankUnion != null);

            dgv.DataSource = from x in actList
                             orderby x.payTime
                             select new
                             {
                                 账单号 = x.id,
                                 //手牌号 = x.text,
                                 //系统账号 = x.systemId,
                                 结账时间 = x.payTime,
                                 收银工号 = x.payEmployee,
                                 //消费金额 = x.cash + x.bankUnion + x.creditCard + x.coupon + x.zero + x.server - x.changes,
                                 现金 = x.cash,
                                 银联 = x.bankUnion,
                                 储值卡 = x.creditCard,
                                 优惠券 = x.coupon,
                                 团购优惠=x.groupBuy,
                                 挂账 = x.zero,
                                 挂账单位 = x.name,
                                 招待 = x.server,
                                 找零 = x.changes,
                                 抹零 = x.wipeZero
                             };
            tMoney.Text = actList.Sum(x => x.bankUnion).ToString();
        }

        //消费情况显示
        private void dgvDetails_show()
        {
            if (dgv.CurrentCell == null)
                return;

            var id = dgv.CurrentRow.Cells[0].Value.ToString();
            var account = db.Account.FirstOrDefault(x => x.id.ToString() == id && x.abandon == null);
            var ids = account.systemId.Split('|');
            var orders = db.HisOrders.Where(x => ids.Contains(x.systemId));
            dgvDetails.DataSource = from x in orders
                                    orderby x.inputTime
                                    select new
                                    {
                                        手牌号 = x.text,
                                        项目名称 = x.menu,
                                        数量 = x.number,
                                        金额 = x.money,
                                        技师号 = x.technician,
                                        服务类型 = x.techType,
                                        录入时间 = x.inputTime,
                                        录入工号 = x.inputEmployee
                                    };
            BathClass.set_dgv_fit(dgvDetails);
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
            PrintDGV.Print_DataGridView(dgv, "银联付款统计", false, "");
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
                    PrintDGV.Print_DataGridView(dgv, "银联付款统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDetails_show();
        }
    }
}
