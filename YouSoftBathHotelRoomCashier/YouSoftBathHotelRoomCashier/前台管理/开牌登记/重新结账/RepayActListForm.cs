using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using YouSoftBathFormClass;
using YouSoftBathGeneralClass;

namespace YouSoftBathReception
{
    public partial class RepayActListForm : Form
    {
        //成员变量
        private string m_seat_text;
        private Employee m_user;

        public RepayActListForm(HotelRoom seat, Employee user)
        {
            m_user = user;
            m_seat_text = seat.text;
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 18);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 18);

            dgv_show();
        }

        private void dgv_show()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var ct = db.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault();

            var lastTime = DateTime.Parse("2013-01-01 00:00:00");
            if (ct != null)
                lastTime = ct.clearTime;

            var acts = db.Account.Where(x => x.text.Contains(m_seat_text) && x.payTime > lastTime && x.abandon == null);
            dgv.DataSource = from x in acts
                             orderby x.payTime
                             select new
                                 {
                                     账单号 = x.id,
                                     手牌号 = x.text,
                                     结账时间 = x.payTime,
                                     结账员工 = x.payEmployee
                                 };
            BathClass.set_dgv_fit(dgv);
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOk_Click(null, null);
        }

        //取消
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //确定重新结账
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选中账单号");
                return;
            }

            var db_new = new BathDBDataContext(LogIn.connectionString);
            var act = db_new.Account.FirstOrDefault(x => x.id.ToString() == dgv.CurrentRow.Cells[0].Value.ToString());
            if (act == null)
            {
                if (BathClass.printAskMsg("手牌未结账，是否直接恢复手牌?") != DialogResult.Yes)
                    return;
            }

            var texts = act.text.Split('|');
            var ids = act.systemId.Split('|');
            //var seats = db_new.HotelRoom.Where(x => texts.Contains(x.text));
            for (int i = 0; i < texts.Count(); i++)
            {
                var s = db_new.HotelRoom.FirstOrDefault(x => x.text == texts[i]);
                if (s == null) continue;

                if (s.status == 2 || s.status == 6 || s.status == 7)
                {
                    BathClass.printErrorMsg("手牌: " + s.text + "已经重新开牌，请先更换手牌");
                    return;
                }
                s.status = 8;
                s.systemId = ids[i];
                var orders = db_new.HisOrders.Where(x => x.systemId == s.systemId);
                foreach (var order in orders)
                {
                    var ho = new Orders();
                    ho.menu = order.menu;
                    ho.text = order.text;
                    ho.systemId = order.systemId;
                    ho.number = order.number;
                    ho.priceType = order.priceType;
                    ho.money = order.money;
                    ho.technician = order.technician;
                    ho.techType = order.techType;
                    ho.inputTime = order.inputTime;
                    ho.inputEmployee = order.inputEmployee;
                    ho.deleteEmployee = order.deleteEmployee;
                    ho.donorEmployee = order.donorEmployee;
                    ho.comboId = order.comboId;
                    ho.paid = false;
                    ho.accountId = order.accountId;
                    ho.billId = order.billId;
                    ho.stopTiming = true;
                    ho.departmentId = 1;
                    db_new.HisOrders.DeleteOnSubmit(order);
                    db_new.Orders.InsertOnSubmit(ho);
                }
            }
            act.abandon = m_user.id;
            var cc = db_new.CardCharge.Where(x => act.id.ToString() == x.CC_AccountNo);
            if (cc.Any())
                db_new.CardCharge.DeleteAllOnSubmit(cc);
            db_new.SubmitChanges();

            this.DialogResult = DialogResult.OK;
        }
    }
}
