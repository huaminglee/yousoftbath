using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using YouSoftBathGeneralClass;

namespace YouSoftBathFormClass
{
    //消费点单
    public partial class OrderCheckForm : Form
    {
        //成员变量
        private DAO dao;
        //private BathDBDataContext db = null;
        private CSeat m_Seat = null;
        private COptions m_options;
        private bool inputBillId = false;
        private string m_con_str;

        //构造函数
        public OrderCheckForm(CSeat seat, string con_str, COptions options)
        {
            m_con_str = con_str;
            m_Seat = seat;
            m_options = options;
            dao = new DAO(con_str);
            InitializeComponent();
        }

        //对话框载入
        private void OrderCheckForm_Load(object sender, EventArgs e)
        {
            inputBillId = MConvert<bool>.ToTypeOrDefault(m_options.录单输入单据编号, false);
            dgvExpense.Columns[1].Visible = inputBillId;
            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);     
            dgvExpense_show();
        }

        //显示台位消费信息
        public void dgvExpense_show()
        {
            dgvExpense.Rows.Clear();
            //var orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null);
            //orders = orders.OrderBy(x => x.inputTime);
            //foreach (var o in orders)
            //{
            //    string[] row = new string[11];
            //    row[0] = o.id.ToString();
            //    row[1] = o.billId;
            //    row[2] = o.text;
            //    row[3] = o.menu;
            //    row[4] = o.technician;
            //    row[5] = o.techType;

            //    row[7] = o.number.ToString();

            //    row[9] = o.inputTime.ToString();
            //    row[10] = o.inputEmployee;

            //    var m = db.Menu.FirstOrDefault(x => x.name == o.menu);
            //    bool redRow = false;
            //    if (m == null)
            //    {
            //        row[6] = "";
            //        row[8] = o.money.ToString();
            //        redRow = true;
            //    }
            //    else
            //    {
            //        if (o.priceType == "每小时")
            //        {
            //            row[6] = o.money.ToString() + "/时";
            //            row[8] = (Math.Ceiling((DateTime.Now - o.inputTime).TotalHours) * o.money).ToString();
            //        }
            //        else
            //        {
            //            row[6] = m.price.ToString();
            //            row[8] = o.money.ToString();
            //        }
            //    }

            //    dgvExpense.Rows.Add(row);
            //    if (redRow)
            //    {
            //        dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
            //    }
            //}
            //BathClass.set_dgv_fit(dgvExpense);
            //if (dgvExpense.Rows.Count != 0)
            //    dgvExpense.CurrentCell = dgvExpense[0, dgvExpense.Rows.Count - 1];

            //money.Text = BathClass.get_seat_expense(m_Seat, m_con_str).ToString();

            double _money = 0;
            DateTime now = DateTime.Now;
            SqlConnection sqlCn = null;
            string cmd_str = "";
            try
            {
                sqlCn = new SqlConnection(LogIn.connectionString);
                sqlCn.Open();

                cmd_str = "Select * from [Orders] where deleteEmployee is null and paid='False' and systemId='" + m_Seat.systemId + "'";
                cmd_str += " order by inputTime";
                var cmd = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] row = new string[11];

                        row[0] = dr["id"].ToString();
                        row[1] = dr["billId"].ToString();
                        row[2] = dr["text"].ToString();
                        row[3] = dr["menu"].ToString();
                        row[4] = dr["technician"].ToString();
                        row[5] = dr["techType"].ToString();

                        row[7] = dr["number"].ToString();

                        row[9] = Convert.ToDateTime(dr["inputTime"]).ToString("MM-dd HH:mm");
                        row[10] = dr["inputEmployee"].ToString();

                        var cmenu = dao.get_Menu("name", dr["menu"].ToString());

                        bool redRow = false;
                        var order_money = Convert.ToDouble(dr["money"]);
                        if (cmenu == null)
                        {
                            row[6] = "";
                            row[8] = order_money.ToString();
                            redRow = true;
                            _money += order_money;
                        }
                        else
                        {
                            if (dr["priceType"].ToString() == "每小时")
                            {
                                double order_money_p = Math.Ceiling((now - Convert.ToDateTime(dr["inputTime"])).TotalHours) * order_money;
                                row[6] = dr["money"].ToString() + "/时";
                                row[8] = order_money_p.ToString();
                                _money += order_money_p;
                            }
                            else
                            {
                                row[6] = cmenu.price.ToString();
                                row[8] = order_money.ToString();
                                _money += order_money;
                            }
                        }

                        dgvExpense.Rows.Add(row);
                        if (redRow)
                        {
                            dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }

                BathClass.set_dgv_fit(dgvExpense);
                if (dgvExpense.Rows.Count != 0)
                {
                    dgvExpense.CurrentCell = dgvExpense[2, dgvExpense.Rows.Count - 1];
                }
                money.Text = _money.ToString();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(cmd_str);
                BathClass.printErrorMsg(e.ToString());
            }
            finally
            {
                dao.close_connection(sqlCn);
            }
        }
    }
}
