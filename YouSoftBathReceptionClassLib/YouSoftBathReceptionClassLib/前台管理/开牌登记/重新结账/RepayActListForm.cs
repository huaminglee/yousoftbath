using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using YouSoftUtil;
using YouSoftBathFormClass;
using YouSoftBathGeneralClass;
using YouSoftBathConstants;

namespace YouSoftBathReception
{
    public partial class RepayActListForm : Form
    {
        //成员变量
        private string m_seat_text;
        private Employee m_user;
        private DAO dao;

        public RepayActListForm(CSeat seat, Employee user)
        {
            m_user = user;
            m_seat_text = seat.text;
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 18);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 18);

            dao = new DAO(LogIn.connectionString);
            dgv_show();
        }

        private void dgv_show()
        {
            //var db = new BathDBDataContext(LogIn.connectionString);
            //var ct = db.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault();
            var ct = dao.get_last_clear_time();

            var lastTime = DateTime.Parse("2013-01-01 00:00:00");
            if (ct != null)
                lastTime = ct.Value;

            //var acts = db.Account.Where(x => x.text.Contains(m_seat_text) && x.payTime > lastTime && x.abandon == null);
            //dgv.DataSource = from x in acts
            //                 orderby x.payTime
            //                 select new
            //                 {
            //                     账单号 = x.id,
            //                     手牌号 = x.text,
            //                     结账时间 = x.payTime,
            //                     结账员工 = x.payEmployee
            //                 };
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(LogIn.connectionString);
                sqlCn.Open();

                cmd_str = "Select * from [Account] where (abandon is null and payTime>'"+ lastTime.ToString()+"' and text like"+
                    "'%"+ m_seat_text+"%')";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dgv.Rows.Add(dr["id"], dr["text"], dr["payTime"], dr["payEmployee"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.ToString());
            }
            finally
            {
                dao.close_connection(sqlCn);
            }
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

            //var db_new = new BathDBDataContext(LogIn.connectionString);
            var act = dao.get_account("id=" + dgv.CurrentRow.Cells[0].Value.ToString());
            //var act = db_new.Account.FirstOrDefault(x => x.id.ToString() == dgv.CurrentRow.Cells[0].Value.ToString());
            if (act == null)
            {
                if (BathClass.printAskMsg("手牌未结账，是否直接恢复手牌?") != DialogResult.Yes)
                    return;
            }

            string cmd_str = "";
            var texts = act.text.Split('|');
            var ids = act.systemId.Split('|');
            //var seats = db_new.Seat.Where(x => texts.Contains(x.text));
            for (int i = 0; i < texts.Count(); i++)
            {
                var s = dao.get_seat("text", texts[i]);
                //var s = db_new.Seat.FirstOrDefault(x => x.text == texts[i]);
                if (s == null) continue;

                if (s.status == SeatStatus.USING || s.status == SeatStatus.WARNING ||
                    s.status == SeatStatus.DEPOSITLEFT || s.status == SeatStatus.REPAIED)
                {
                    if (s.systemId != ids[i])
                    {
                        BathClass.printErrorMsg("手牌: " + s.text + "已经重新开牌，请先更换手牌");
                        return;
                    }
                }
                else
                    cmd_str += @"update [Seat] set status=8, systemId='" + ids[i] + "' where text='" + texts[i] + "' ";
                //s.status = 8;
                //s.systemId = ids[i];
                cmd_str += @"insert into [Orders](menu,text,systemId,number,priceType,money,technician,techtype,startTime,"
                + @"inputTime, inputEmployee,deleteEmployee,donorEmployee,comboId,accountId,billId,departmentId, paid,roomId) "
                + @"select menu,text,systemId,number,priceType,"
                            + @"money,technician,techtype,startTime,inputTime,inputEmployee,deleteEmployee,"
                            + @"donorEmployee,comboId,accountId,billId,departmentId, 'False',roomId from [HisOrders] where("
                            + @"accountId=" + act.id + ") ";
                cmd_str += @" delete from [HisOrders] where(accountId=" + act.id + ") ";
                //var orders = db_new.HisOrders.Where(x => x.systemId == s.systemId && x.accountId.Value == act.id);
                //foreach (var order in orders)
                //{
                //    var ho = new Orders();
                //    ho.menu = order.menu;
                //    ho.text = order.text;
                //    ho.systemId = order.systemId;
                //    ho.number = order.number;
                //    ho.priceType = order.priceType;
                //    ho.money = order.money;
                //    ho.technician = order.technician;
                //    ho.techType = order.techType;
                //    ho.startTime = order.startTime;
                //    ho.inputTime = order.inputTime;
                //    ho.inputEmployee = order.inputEmployee;
                //    ho.deleteEmployee = order.deleteEmployee;
                //    ho.donorEmployee = order.donorEmployee;
                //    ho.comboId = order.comboId;
                //    ho.paid = false;
                //    ho.accountId = order.accountId;
                //    ho.billId = order.billId;
                //    ho.stopTiming = true;
                //    db_new.Orders.InsertOnSubmit(ho);
                //    db_new.HisOrders.DeleteOnSubmit(order);
                //}
            }
            cmd_str += @" update [Account] set abandon='" + m_user.id + "' where id=" + act.id;
            cmd_str += @" delete from [CardCharge] where CC_AccountNo='" + act.id.ToString() + "'";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("重新结账失败!");
                return;
            }
            //act.abandon = m_user.id;
            //var cc = db_new.CardCharge.Where(x => act.id.ToString() == x.CC_AccountNo);
            //if (cc.Any())
            //    db_new.CardCharge.DeleteAllOnSubmit(cc);

            //BathClass.SubmitChanges(db_new);
            this.DialogResult = DialogResult.OK;
        }
    }
}
