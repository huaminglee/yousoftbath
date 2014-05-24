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

            var act = dao.get_account("id=" + dgv.CurrentRow.Cells[0].Value.ToString());
            if (act == null)
            {
                if (BathClass.printAskMsg("手牌未结账，是否直接恢复手牌?") != DialogResult.Yes)
                    return;
            }

            var sql = new StringBuilder();
            var texts = act.text.Split('|');
            var ids = act.systemId.Split('|');
            for (int i = 0; i < texts.Count(); i++)
            {
                var s = dao.get_seat("text", texts[i]);
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
                    sql.Append(" update [Seat] set status=8, systemId='").Append(ids[i]).Append("' where text='").Append(texts[i]).Append("' ");

                sql.Append(@" insert into [Orders](menu,text,systemId,number,priceType,money,technician,techtype,startTime,");
                sql.Append(@" inputTime, inputEmployee,deleteEmployee,donorEmployee,comboId,accountId,billId,departmentId, paid,roomId,donorExplain,donorTime) ");
                sql.Append(@" select menu,text,systemId,number,priceType,money,technician,techtype,startTime,inputTime,inputEmployee,deleteEmployee,");
                sql.Append(@" donorEmployee,comboId,accountId,billId,departmentId, 'False',roomId,donorExplain,donorTime");
                sql.Append(@"  from [HisOrders] where(accountId=").Append(act.id).Append(") ");
                sql.Append(@"  delete from [HisOrders] where(accountId=").Append(act.id).Append(") ");
            }

            sql.Append(@" update [Account] set abandon='").Append(m_user.name).Append("' where id=").Append(act.id);
            sql.Append(@" delete from [CardCharge] where CC_AccountNo='").Append(act.id).Append("'");
            if (!dao.execute_command(sql.ToString()))
            {
                BathClass.printErrorMsg("重新结账失败!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
