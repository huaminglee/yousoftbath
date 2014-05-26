using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using System.Transactions;
using YouSoftBathFormClass;
using System.Threading;
using System.Timers;
using System.Data.SqlClient;
using YouSoftBathConstants;


namespace YouSoftBathReception
{
    //消费查询
    public partial class SeatExpenseForm : Form
    {
        //成员变量
        private Thread m_thread_seatCard;//手牌线程
        private bool _close = false;//线程终止标志

        public List<CSeat> m_Seats = new List<CSeat>();
        public List<string> m_rooms = new List<string>();
        private CSeat m_Seat = null;
        private CCardInfo m_Member = null;
        private double discount_money = 0;
        private bool inputBillId = false;
        private string companyName;

        private bool printBill = false;//打印结账单
        private bool printStubBill = false;//打印村跟单
        private bool printShoe = false;//打印取鞋小票
        private bool payByOrder = false;//启用分单结账 

        private bool auto_shoe = false;
        private bool use_pad = false;
        private bool seatLock = false;
        private bool auto_seat_card;//自动感应手牌
        private string lock_type;//手牌锁类型
        private DateTime now;
        private int seat_length = -1;

        private DAO dao;

        //构造函数
        public SeatExpenseForm(CSeat seat, int _seat_length, bool _seatLock, bool _auto_seat_card, DAO _dao)
        {
            seatLock = _seatLock;
            auto_seat_card = _auto_seat_card;
            seat_length = _seat_length;
            dao = _dao;

            if (seatLock)
            {
                m_Seats.Add(dao.get_seat("text", seat.text));
            }
            else
            {
                string cmd_str = "((chainId is null and text='" + seat.text + "') or (chainId is not null and chainId='" + seat.chainId + "'))"
                    + @" and (status=2 or status=6 or status=7 or status=8)";
                var seats = dao.get_seats(cmd_str);
                m_Seats.AddRange(seats);
            }

            var pars = new List<string>();
            pars.Add("paying");

            var vals = new List<string>();
            vals.Add("True");

            int count = m_Seats.Count;
            string update_str = @"update [Seat] set paying='True' where ";
            for (int i = 0; i < count; i++ )
            {
                update_str += "text='" + m_Seats[i].text + "'";
                if (i != count - 1)
                    update_str += " or ";
            }

            if (!dao.execute_command(update_str))
            {
                BathClass.printErrorMsg("手牌状态更新失败，如有超时浴资，请删除手牌重新载入");
            }

            InitializeComponent();
        }

        //对话框载入
        private void SeatExpenseForm_Load(object sender, EventArgs e)
        {
            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);

            dgvChain.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 17);
            dgvChain.RowsDefaultCellStyle.Font = new Font("宋体", 17);

            printBill = MConvert<bool>.ToTypeOrDefault(LogIn.options.结账打印结账单, false);
            printShoe = MConvert<bool>.ToTypeOrDefault(LogIn.options.结账打印取鞋小票, false);
            printStubBill = MConvert<bool>.ToTypeOrDefault(LogIn.options.结账打印存根单, false);
            inputBillId = MConvert<bool>.ToTypeOrDefault(LogIn.options.录单输入单据编号, false);
            companyName = LogIn.options.companyName;
            auto_shoe = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用鞋部, false);
            payByOrder = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用分单结账, false);
            use_pad = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用客房面板, false); ;
            dgvExpense.Columns[11].Visible = use_pad;
            payAll.Visible = payByOrder;
            payAll.Checked = payByOrder;
            lock_type = LogIn.options.手牌锁类型;
            now = BathClass.Now(LogIn.connectionString);


            if (seatLock && auto_seat_card)
            {
                m_thread_seatCard = new Thread(new ThreadStart(seat_card_thread));
                m_thread_seatCard.Start();

                dgvChain.ReadOnly = true;
                btnReload.Text = "留牌";
            }
            m_Seat = m_Seats[0];
            name.Text = m_Seat.name;
            phone.Text = m_Seat.phone;
            dgvChain_show();
            dgvChain.CurrentCell = dgvChain[0, 0];

            dgvExpense.Columns[0].Visible = false;
            dgvExpense.Columns[2].Visible = inputBillId;
            dgvExpense.Columns[1].Visible = payByOrder;
            foreach (var seat in m_Seats)
                order_guoye(seat);

            string room_name = dao.get_seat_room(m_Seats[0].text);
            if (room_name == "")
            {
                roomId.Visible = false;
            }
            else
            {
                roomId.Text = room_name;
                roomId.Visible = true;
            }
            dgvExpense_show();
        }

        //非自动感应手牌
        private void open_seat_expense_by_card()
        {
            try
            {
                if (lock_type == "欧亿达")
                {
                    Thread.Sleep(500);
                    if (OYD.FKOPEN() != 1)
                        return;

                    OYD.CH375SetTimeout(0, 5000, 5000);
                }
                byte[] buff = new byte[200];
                string seat_text = "";

                int rt = -1;
                if (lock_type == "欧亿达")
                {
                    Thread.Sleep(500);
                    rt = OYD.OYEDA_id(buff);
                }
                else if (lock_type == "锦衣卫")
                    rt = JYW.ReadID(buff);
                else if (lock_type == "RF")
                    rt = RF.RF_RFID(ref seat_text);

                if (rt != 0)
                    return;

                CSeat seat = null;
                if (lock_type == "欧亿达")
                {
                    seat_text = Encoding.Default.GetString(buff, 0, 20).Trim();
                    seat_text = seat_text.Substring(0, 16);
                }
                else if (lock_type == "锦衣卫")
                {
                    seat_text = BathClass.byteToHexStr(buff);
                    seat_text = seat_text.Substring(0, 16);
                }
                seat = dao.get_seat("oId", seat_text);

                if (seat == null || (seat.status != SeatStatus.USING &&
                        seat.status != SeatStatus.WARNING &&
                        seat.status != SeatStatus.DEPOSITLEFT &&
                        seat.status != SeatStatus.REPAIED))
                {
                    BathClass.printErrorMsg("该手牌不在使用中，不能结账!");
                    return;
                }

                if (lock_type == "欧亿达")
                    Thread.Sleep(500);

                if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                        (lock_type == "锦衣卫" && JYW.MD(buff) != 0) ||
                    (lock_type == "RF" && RF.RF_MD() != 0))
                    return;

                if (!m_Seats.Select(x => x.text).Contains(seat.text))
                {
                    if (seat.note != null && seat.note != "")
                        BathClass.printInformation(seat.note);

                    m_Seats.Add(seat);
                    update_ui(seat);
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        //手牌线程
        private void seat_card_thread()
        {
            while (true)
            {
                if(_close)break;
                try
                {
                    if (lock_type == "欧亿达")
                    {
                        Thread.Sleep(500);
                        if (OYD.FKOPEN() != 1)
                            continue;

                        OYD.CH375SetTimeout(0, 5000, 5000);
                    }
                    byte[] buff = new byte[200];
                    string seat_text = "";

                    int rt = -1;
                    if (lock_type == "欧亿达")
                    {
                        Thread.Sleep(500);
                        rt = OYD.OYEDA_id(buff);
                    }
                    else if (lock_type == "锦衣卫")
                        rt = JYW.ReadID(buff);
                    else if (lock_type == "RF")
                    {
                        rt = RF.RF_RFID(ref seat_text);
                    }

                    if (rt != 0)
                        continue;

                    CSeat seat = null;
                    if (lock_type == "欧亿达")
                    {
                        seat_text = Encoding.Default.GetString(buff, 0, 20).Trim();
                        seat_text = seat_text.Substring(0, 16);
                    }
                    else if (lock_type == "锦衣卫")
                    {
                        seat_text = BathClass.byteToHexStr(buff);
                        seat_text = seat_text.Substring(0, 16);
                    }
                    seat = dao.get_seat("oId", seat_text);

                    if (seat == null || (seat.status != SeatStatus.USING && 
                        seat.status != SeatStatus.WARNING && 
                        seat.status != SeatStatus.DEPOSITLEFT && 
                        seat.status != SeatStatus.REPAIED))
                    {
                        if (this.InvokeRequired)
                            this.Invoke(new delegate_print_msg(BathClass.printErrorMsg),
                                    new object[] { "该手牌不在使用中，不能结账!" });
                        else
                            BathClass.printErrorMsg("该手牌不在使用中，不能结账!");
                        //BathClass.printErrorMsg("该手牌不在使用中，不能结账!");
                        continue;
                    }

                    if (lock_type == "欧亿达")
                        Thread.Sleep(500);

                    if ((lock_type == "欧亿达" && OYD.OYEDA_md(buff) != 0) ||
                            (lock_type == "锦衣卫" && JYW.MD(buff) != 0) ||
                        (lock_type == "RF" && RF.RF_MD() != 0))
                        continue;

                    if (!m_Seats.Select(x => x.text).Contains(seat.text))
                    {
                        if (seat.note != null && seat.note != "")
                            BathClass.printInformation(seat.note);

                        m_Seats.Add(seat);
                        if (this.InvokeRequired)
                            this.Invoke(new delegate_update_ui(update_ui),
                                    new object[] { seat });
                        else
                            update_ui(seat);
                        //update_ui(seat, db_new);
                    }
                }
                catch 
                {
                }
            }
        }

        private delegate void delegate_print_msg(string msg);
        private delegate void delegate_update_ui(CSeat seat);
        private void update_ui(CSeat seat)
        {
            string t = seat.openTime.Value.ToString("HH-mm");
            dgvChain.Rows.Add();
            dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[0].Value = seat.text;
            dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[1].Value = false;
            dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[2].Value = t;

            order_guoye(seat);
            //m_Seat.paying = true;
            //dc.SubmitChanges();

            //var pars = new List<string>();
            //pars.Add("paying");

            //var vals = new List<string>();
            //vals.Add("True");

            //var txts = new List<string>();
            //txts.Add(seat.text);
            //dao.update_table_multi_row("Seat", "text", txts, pars, vals);
            if (!dao.execute_command("update [Seat] set paying='True' where text='"+seat.text+"'"))
            {
                BathClass.printErrorMsg("手牌状态更新失败，如有超时浴资，请删除手牌重新载入");
            }

            dgvChain.CurrentCell = null;
            dgvExpense_show();
        }

        private delegate void method_delegate();

        //显示台位消费信息
        public void dgvExpense_show()
        {
            double money = 0;
            dgvExpense.Rows.Clear();
            DateTime now = DateTime.Now;
            SqlConnection sqlCn = null;
            string cmd_str = "";
            try
            {
                sqlCn = new SqlConnection(LogIn.connectionString);
                sqlCn.Open();

                string id_str = "";
                int count = m_Seats.Count;
                for (int i = 0; i < count; i++)
                {
                    id_str += "systemId='" + m_Seats[i].systemId + "'";
                    if (i != count - 1)
                        id_str += " or ";
                }

                cmd_str = "Select * from [Orders] where deleteEmployee is null and paid='False'";
                cmd_str += " and (" + id_str + ")";

                cmd_str += " order by text";
                var cmd = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] row = new string[12];
                        row[0] = dr["id"].ToString();
                        row[1] = "true";
                        row[2] = dr["billId"].ToString();
                        row[3] = dr["text"].ToString();
                        row[4] = dr["menu"].ToString();
                        row[5] = dr["technician"].ToString();

                        row[7] = dr["number"].ToString();

                        row[9] = Convert.ToDateTime(dr["inputTime"]).ToString("MM-dd HH:mm");
                        row[10] = dr["inputEmployee"].ToString();
                        row[11] = dr["roomId"].ToString();

                        //CMenu cmenu = null;
                        //cmd_str = "select price from [Menu] where name='" + dr["menu"].ToString() + "'";

                        var cmenu = dao.get_Menu("name", dr["menu"].ToString());

                        //if (sqlCn.State != ConnectionState.Open)
                        //    sqlCn.Open();

                        //var cmd_menu = new SqlCommand(cmd_str, sqlCn);
                        //using (SqlDataReader dr_menu = cmd_menu.ExecuteReader())
                        //{
                        //    while (dr_menu.Read())
                        //    {
                        //        cmenu = new CMenu();
                        //        cmenu.price = Convert.ToDouble(dr_menu["price"]);
                        //    }
                        //}

                        bool redRow = false;
                        var order_money = Convert.ToDouble(dr["money"]);
                        if (cmenu == null)
                        {
                            row[6] = "";
                            row[8] = order_money.ToString();
                            redRow = true;
                            money += order_money;
                        }
                        else
                        {
                            if (dr["priceType"].ToString() == "每小时")
                            {
                                double order_money_p = Math.Ceiling((now - Convert.ToDateTime(dr["inputTime"])).TotalHours) * order_money;
                                row[6] = dr["money"].ToString() + "/时";
                                row[8] = order_money_p.ToString();
                                money += order_money_p;
                            }
                            else
                            {
                                row[6] = cmenu.price.ToString();
                                row[8] = order_money.ToString();
                                money += order_money;
                            }
                        }

                        dgvExpense.Rows.Add(row);
                        if (redRow)
                        {
                            dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                        }
                        if (order_money == 0)
                        {
                            dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                    }
                }
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

            BathClass.set_dgv_fit(dgvExpense);

            seatText.Text = "";
            moneyPayable.Text = money.ToString();
        }

        //联台手牌信息显示
        private void dgvChain_show()
        {
            dgvChain.Rows.Clear();

            foreach (CSeat seat in m_Seats)
            {
                string t = Convert.ToDateTime(seat.openTime).ToShortTimeString();
                dgvChain.Rows.Add();
                dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[0].Value = seat.text;
                dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[2].Value = t;
            }
            BathClass.set_dgv_fit(dgvChain);
            dgvChain.CurrentCell = null;
        }

        //宾客付款
        private void payTool_Click(object sender, EventArgs e)
        {
            int j = 0;
            Dictionary<Int64, bool> orders = new Dictionary<Int64, bool>();
            foreach (DataGridViewRow r in dgvExpense.Rows)
            {
                bool pay = r.Cells[1].EditedFormattedValue.ToString() == "True";
                orders.Add(Convert.ToInt64(r.Cells[0].Value), pay);
                j++;
            }

            double money = Convert.ToDouble(moneyPayable.Text);

            PayForm payForm = new PayForm(m_Seats, orders, m_Member, discount_money, money);
            if (payForm.ShowDialog() != DialogResult.OK)
                return;

            var dgv = new_datagridView();
            foreach (DataGridViewRow r in dgvExpense.Rows)
            {
                if (r.Cells[1].EditedFormattedValue.ToString() != "True") continue;

                dgv.Rows.Add(r.Cells[3].Value, r.Cells[11].Value, r.Cells[4].Value, r.Cells[5].Value,
                        r.Cells[6].Value, r.Cells[7].Value, r.Cells[8].Value);

            }

            int newAccountId = payForm.newAccountId;
            var act = dao.get_account("id=" + newAccountId);
            var has_repaid = dao.exist_instance("Account", "systemId='" + act.systemId + "' and abandon is not null");
            if (!has_repaid)
            {
                List<string> seat_texts = new List<string>();
                var ts = act.text.Split('|').ToList();
                foreach (var tx in ts)
                {
                    var s = m_Seats.FirstOrDefault(x => x.text == tx);
                    if (s == null) continue;
                    int i = m_Seats.IndexOf(s);
                    if (dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
                        seat_texts.Add(tx);
                }

                //启动打印线程
                Thread td = new Thread(delegate() { print_Bill(dgv, printBill, printStubBill, printShoe, seat_texts, false, act); });
                td.Start();
            }
            else
            {
                Thread td = new Thread(delegate() { print_Bill(dgv, false, false, false, null, true, act); });
                td.Start();
            }

            if (auto_shoe)
                sendMessageToShoes(act);

            //处理留牌
            string update_str = @"update [Seat] set note='已留牌', status=2 where ";
            string txt_str = "";
            for (int i = 0; i < m_Seats.Count; i++ )
            {
                if (dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
                    continue;

                if (txt_str != "")
                    txt_str += " or ";
                txt_str += "text='" + m_Seats[i].text + "'";
            }

            if (txt_str != "")
            {
                update_str += txt_str;
                if (!dao.execute_command(update_str))
                {
                    BathClass.printErrorMsg("手牌状态更新失败!");
                }
            }
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //手工打折
        private void discountTool_Click(object sender, EventArgs e)
        {
            //var dc = new BathDBDataContext(LogIn.connectionString);
            Employee oper = null;
            //if (BathClass.getAuthority(dc, LogIn.m_User, "手工打折"))
                //oper = LogIn.m_User;

            if ((dao.get_authority(LogIn.m_User, "手工打折")))
                oper = LogIn.m_User;

            if (oper == null)
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;

                //if (BathClass.getAuthority(dc, inputEmployee.employee, "手工打折"))
                //    oper = inputEmployee.employee;

                if ((dao.get_authority(inputEmployee.employee, "手工打折")))
                    oper = LogIn.m_User;
                else
                {
                    BathClass.printErrorMsg("没有手工打折权限");
                    return;
                }
            }
            if (oper == null)
                return;
            

            InputNumber inputNumberForm = new InputNumber("输入折扣率(<10)", false);
            if (inputNumberForm.ShowDialog() != DialogResult.OK)
                return;

            double number = inputNumberForm.number;
            double discountRate = 0;
            if (number < 1)
                discountRate = number;
            else
                discountRate = number / 10.0;
            
            //var os = dc.Orders.Where(x => m_Seats.Select(y => y.systemId).Contains(x.systemId) && x.deleteEmployee == null);
            //foreach (Orders o in os)
                //o.money = Math.Round(o.money * discountRate);

            //string cmd_str = @"if exists(select 1 from sysobjects where id=object_id('discount_money') and objectproperty(id,'IsInlineFunction')=0)"
            //                + @" drop function [dbo].[discount_money]"
            //                + @" go"
            //                + @" create function discount_money(@systemId nvarchar(MAX), @discount float)"
            //                + @" returns real"
            //                + @" as"
            //                + @" begin"
            //                + " declare @discount_money real"
            //                + @" declare @money real"
            //                + " select @money=money from [orders] where systemId=@systemId and deleteEmployee is null"
            //                + @" set @discount_money=@discount*@money"
            //                + @" Return (@discount_money)"
            //                + @" end"
            //                + @" go";

            string cmd_str = @"update [Orders] set money=round(money*" + discountRate.ToString() + ",0) " + " where ";
            string seat_cmd_str = " update table [Seat] set discount='" + 
                inputNumberForm.number.ToString() + 
                ", discountEmployee='" + oper.id + "' where ";
            int count = m_Seats.Count;
            string id_str = "";
            for (int i = 0; i < count; i++ )
            {
                id_str += @"systemId='" + m_Seats[i].systemId + "'";
                //seat_cmd_str += "text='" + m_Seats[i].text + "'";
                if (i != count - 1)
                    id_str += " or ";
            }
            cmd_str += id_str;
            cmd_str += @" update [Seat] set discount='" + inputNumberForm.number.ToString()
                      +@"', discountEmployee='" + oper.id + "' where ";
            cmd_str += id_str;

            cmd_str += " insert into [Operation](employee, seat, openEmployee, explain, note1, note2, opTime) values('" +
                oper.name + "','" + string.Join("|", m_Seats.Select(y => y.text).ToArray()) + "','" +
                string.Join("|", m_Seats.Select(y => y.openEmployee).ToArray()) + "','手工打折','" + number.ToString() + "','" +
                string.Join("|", m_Seats.Select(y => y.systemId).ToArray()) + "',getdate())";

            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("手工打折失败,请重试!");
                //return;
            }
            dgvExpense_show();
            //Operation op = new Operation();
            //op.employee = oper.name;
            //op.seat = string.Join("|", m_Seats.Select(y => y.text).ToArray());
            //op.openEmployee = string.Join("|", m_Seats.Select(y => y.openEmployee).ToArray());
            ////op.openTime = seat.openTime;
            //op.explain = "手工打折";
            //op.note1 = number.ToString();
            //op.note2 = string.Join("|", m_Seats.Select(y => y.systemId).ToArray());
            //op.opTime = now;
            //dc.Operation.InsertOnSubmit(op);


            //dc.SubmitChanges();
        }

        //宾客转账
        private void transferTool_Click(object sender, EventArgs e)
        {
            InputSeatForm seatForm = new InputSeatForm(2);
            if (seatForm.ShowDialog() != DialogResult.OK)
                return;

            if (m_Seats.FirstOrDefault(x => x.text == seatForm.m_Seat.text) != null)
            {
                BathClass.printErrorMsg("转账手牌中已经包含该手牌，请重新输入");
                return;
            }

            //var dc = new BathDBDataContext(LogIn.connectionString);
            string systemId = seatForm.m_Seat.systemId;

            string cmd_str = @"update [Orders] set systemId='" + systemId + "' where deleteEmployee is null and paid='False' and (";

            string id_str = "";
            int count = m_Seats.Count;
            for (int i = 0; i < count; i++ )
            {
                id_str += @"systemId='" + m_Seats[i].systemId + "'";
                if (i != count - 1)
                    id_str += " or ";
                else
                    id_str += ")";
                m_rooms.Add(dao.get_seat_room(m_Seats[i].text));
            }
            cmd_str += id_str;
            cmd_str += @"update [Orders] set priceType='停止消费', "
                     + @"money=money*ceiling(datediff(minute,inputTime, getdate())/60.0)"
                     + @" where deleteEmployee is null and paid='False' and priceType='每小时' and (";
            cmd_str += id_str;
            cmd_str += @"update [Seat] set status='3' where (";
            cmd_str += id_str;

            cmd_str += " insert into [Operation](employee, explain,opTime, note1, note2) values('" +
                LogIn.m_User.id + "', '转账', getdate(),'" + string.Join("\n", m_Seats.Select(x => x.text).ToArray()) + "','" +
                seatForm.m_Seat.text + "')";

            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("转账失败，请重试!");
                return;
            }

            //if (auto_shoe)
            //{
            //    var pars = new List<string>();
            //    pars.Add("text");
            //    pars.Add("payEmployee");
            //    pars.Add("payTime");
            //    pars.Add("processed");

            //    var vals = new List<string>();
            //    vals.Add(string.Join("|", m_Seats.Select(x => x.name).ToArray()));
            //    vals.Add(LogIn.m_User.id);
            //    vals.Add(DateTime.Now.ToString());
            //    vals.Add("False");

            //    dao.insert_table_row("ShoeMsg", pars, vals);
            //}

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            if (use_pad)
                printCols.Add("房间");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");
            PrintSeatBill.Print_DataGridView(m_Seats, m_rooms,seatForm.m_Seat.text, "转账确认单", dgvExpense, printCols, moneyPayable.Text, companyName);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 新建打印线程，不会卡主线程
        /// </summary>
        /// <param name="_print_bill">是否打印结账单</param>
        /// <param name="_print_stubBill">是否打印村跟单</param>
        /// <param name="_print_shoe">是否打印取鞋小票</param>
        /// <param name="seat_texts">需要取鞋的手牌号</param>
        /// <param name="_reprint">是否打印补救单</param>
        /// <param name="act">打印的账单</param>
        private void print_Bill(DataGridView dgv, bool _print_bill, bool _print_stubBill, bool _print_shoe, List<string> seat_texts, bool _reprint, CAccount act)
        {
            try
            {
                ////0编号，1结账，2单据号，3 手牌，4项目名称，5技师，6单位，7数量，8金额，9消费时间，10录入员工，11房间

                List<string> printCols = new List<string>();
                printCols.Add("手牌");
                if (use_pad)
                    printCols.Add("房间");
                printCols.Add("项目名称");
                printCols.Add("技师");
                printCols.Add("单价");
                printCols.Add("数量");
                printCols.Add("金额");

                var ids = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());

                foreach (var s in m_Seats)
                {
                    m_rooms.Add(dao.get_seat_room(s.text));
                }

                if (MConvert<bool>.ToTypeOrDefault(LogIn.options.启用大项拆分, false))
                {
                    var dgv1 = new_datagridView();
                    var db = new BathDBDataContext(LogIn.connectionString);
                    foreach (DataGridViewRow r in dgv.Rows)
                    {
                        string menuName = MConvert<string>.ToTypeOrDefault(r.Cells[2].Value, "");
                        var dgv_menu = db.Menu.FirstOrDefault(x => x.name == menuName);
                        if (dgv_menu != null)
                        {
                            if (db.BigCombo.FirstOrDefault(x => x.menuid == dgv_menu.id) != null)
                            {
                                var substIDs = BathClass.disAssemble(db.BigCombo.FirstOrDefault(x => x.menuid == dgv_menu.id).substmenuid, Constants.SplitChar);
                                for (int i = 0; i < substIDs.Count; i++)
                                {
                                    var menu = db.Menu.FirstOrDefault(x => x.id == substIDs[i]);
                                    dgv1.Rows.Add(r.Cells[0].Value, r.Cells[1].Value, menu.name, r.Cells[3].Value,
                                        menu.price, r.Cells[5].Value, menu.price * MConvert<double>.ToTypeOrDefault(r.Cells[5].Value, 0));
                                }
                                continue;
                            }
                        }

                        dgv1.Rows.Add(r.Cells[0].Value, r.Cells[1].Value, r.Cells[2].Value, r.Cells[3].Value,
                                r.Cells[4].Value, r.Cells[5].Value, r.Cells[6].Value);
                    }

                    if (_print_bill)
                        PrintBill.Print_DataGridView(m_Seats, m_rooms, act, "结账单", dgv1, printCols, companyName);
                    if (_print_stubBill)
                        PrintBill.Print_DataGridView(m_Seats, m_rooms, act, "存根单", dgv1, printCols, companyName);
                    if (_reprint)
                        PrintBill.Print_DataGridView(m_Seats, m_rooms, act, "补救单", dgv1, printCols, companyName);
                    dgv = null;
                    dgv1 = null;
                }
                else
                {
                    if (_print_bill)
                        PrintBill.Print_DataGridView(m_Seats, m_rooms, act, "结账单", dgv, printCols, companyName);
                    if (_print_stubBill)
                        PrintBill.Print_DataGridView(m_Seats, m_rooms, act, "存根单", dgv, printCols, companyName);
                    if (_reprint)
                        PrintBill.Print_DataGridView(m_Seats, m_rooms, act, "补救单", dgv, printCols, companyName);
                    dgv = null;
                }


                if (_print_shoe)
                    PrintShoeMsg.Print_DataGridView(seat_texts, act.payEmployee, act.payTime.ToString(), companyName);
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }


        private DataGridView new_datagridView()
        {
            DataGridView dgv = new DataGridView();

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "手牌";
            dgv.Columns.Add(col);

            DataGridViewTextBoxColumn coll = new DataGridViewTextBoxColumn();
            coll.HeaderText = "房间";
            dgv.Columns.Add(coll);

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

            return dgv;
        }

        //会员打折
        private void memberTool_Click(object sender, EventArgs e)
        {
            double money_pre = Convert.ToDouble(moneyPayable.Text);
            MemberPromotionOptionForm memberPromotionOptionForm = new MemberPromotionOptionForm(m_Seats);
            if (memberPromotionOptionForm.ShowDialog() != DialogResult.OK)
                return;

            //var dc = new BathDBDataContext(LogIn.connectionString);
            m_Member = memberPromotionOptionForm.m_Member;

            dgvExpense_show();
            double money = Convert.ToDouble(moneyPayable.Text);
            moneyPayable.Text = money.ToString();
            discount_money = money_pre - money;
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
                case Keys.Enter:
                    btnReload_Click(null, null);
                    break;
                case Keys.Space:
                    if (!auto_seat_card)
                        open_seat_expense_by_card();
                    break;
                case Keys.Delete:
                    btnDel_Click(null, null);
                    break;
                case Keys.F1:
                case Keys.Add:
                    payTool_Click(null, null);
                    break;
                case Keys.F2:
                case Keys.Subtract:
                    discountTool_Click(null, null);
                    break;
                case Keys.F3:
                    //freeTool_Click(null, null);
                    break;
                case Keys.F4:
                    transferTool_Click(null, null);
                    break;
                case Keys.F6:
                case Keys.Decimal:
                    memberTool_Click(null, null);
                    break;
                case Keys.F8:
                    toolOrder_Click(null, null);
                    break;
                case Keys.F9:
                    //toolKeep_Click(null, null);
                    break;
                case Keys.F10:
                    toolReturn_Click(null, null);
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "0";
                    break;
                case Keys.D1:
                case Keys.NumPad1:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "1";
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "2";
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "3";
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "4";
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "5";
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "6";
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "7";
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "8";
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    if (!seatText.ContainsFocus)
                        seatText.Text += "9";
                    break;
                case Keys.Back:
                    if (seatText.Text != "" && !seatText.ContainsFocus)
                        seatText.Text = seatText.Text.Substring(0, seatText.Text.Length - 1);
                    break;
                default:
                    break;
            }
        }

        //载入消费
        private void btnReload_Click(object sender, EventArgs e)
        {
            if (seatText.Text != "")
            {
                //m_Seat = dc_new.Seat.FirstOrDefault(x => x.text == seatText.Text || x.oId == seatText.Text);

                var keys = new List<string>();
                keys.Add("text");
                keys.Add("oId");

                var key_vals = new List<string>();
                key_vals.Add(seatText.Text);
                key_vals.Add(seatText.Text);
                m_Seat = dao.get_seat(keys, key_vals, "or");
                seatText.Text = "";

                if (m_Seat == null || m_Seat.status != SeatStatus.USING && 
                    m_Seat.status != SeatStatus.WARNING && 
                    m_Seat.status != SeatStatus.DEPOSITLEFT && 
                    m_Seat.status != SeatStatus.REPAIED)
                {
                    BathClass.printErrorMsg("该手牌不在使用中，不能结账!");
                    return;
                }
                if (m_Seats.FirstOrDefault(x=>x.text==m_Seat.text) == null)
                {
                    if (m_Seat.note != null && m_Seat.note != "")
                        BathClass.printInformation(m_Seat.note);

                    string t = m_Seat.openTime.Value.ToString("HH:mm");
                    //var seatType = dc_new.SeatType.FirstOrDefault(x => x.id == m_Seat.typeId);
                    var seatType = dao.get_seattype("id", m_Seat.typeId);
                    var hotel = (seatType.department == "客房部");

                    name.Text = m_Seat.name;
                    phone.Text = m_Seat.phone;

                    dgvChain.Rows.Add();
                    dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[0].Value = m_Seat.text;
                    dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[1].Value = (seatLock && !hotel);
                    dgvChain.Rows[dgvChain.Rows.Count - 1].Cells[2].Value = t;

                    order_guoye(m_Seat);
                    m_Seats.Add(m_Seat);
                    //m_Seat.paying = true;
                    //dc_new.SubmitChanges();

                    //var pars = new List<string>();
                    //pars.Add("paying");

                    //var vals = new List<string>();
                    //vals.Add("True");

                    //var txts = new List<string>();
                    //txts.Add(m_Seat.text);
                    //dao.update_table_multi_row("Seat", "text", txts, pars, vals);

                    if (!dao.execute_command("update [Seat] set paying='True' where text='" + m_Seat.text + "'"))
                    {
                        BathClass.printErrorMsg("手牌状态更新失败，如有超时浴资，请删除手牌重新载入");
                    }

                    dgvChain.CurrentCell = null;
                    dgvExpense_show();
                }
            }
        }

        //发送消息给鞋部
        private void sendMessageToShoes(CAccount account)
        {
            var pars = new List<string>();
            pars.Add("text");
            pars.Add("payEmployee");
            pars.Add("payTime");
            pars.Add("processed");

            var vals = new List<string>();
            vals.Add(account.text);
            vals.Add(account.payEmployee);
            vals.Add(account.payTime.ToString());
            vals.Add("False");

            dao.insert_table_row("ShoeMsg", pars, vals);
        }

        //删除手牌
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgvChain.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选择手牌!");
                return;
            }

            if (dgvChain.Rows.Count == 1)
            {
                BathClass.printErrorMsg("已经只剩下一个手牌，不能删除!");
                return;
            }

            string text = dgvChain.CurrentRow.Cells[0].Value.ToString();

            int i = m_Seats.IndexOf(m_Seats.FirstOrDefault(x => x.text == text));
            if (seatLock && dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
            {
                BathClass.printErrorMsg("已经刷手牌，不能删除!");
                return;
            }
            dgvChain.Rows.RemoveAt(i);
            m_Seats.Remove(m_Seats.FirstOrDefault(x => x.text == text));

            //var pars = new List<string>();
            //pars.Add("paying");

            //var vals = new List<string>();
            //vals.Add("False");

            //var txts = new List<string>();
            //txts.Add(text);
            //dao.update_table_multi_row("Seat", "text", txts, pars, vals);
            if (!dao.execute_command("update [Seat] set paying='False' where text='" + text + "'"))
            {
                BathClass.printErrorMsg("手牌状态更新失败");
            }

            //var dc_new = new BathDBDataContext(LogIn.connectionString);
            //dc_new.Seat.FirstOrDefault(x => x.text == text).paying = false;
            //dc_new.SubmitChanges();

            //dgvChain_show();
            m_Seat = null;
            dgvChain.CurrentCell = null;
            //chain = true;
            dgvExpense_show();
        }

        //消费录入
        private void toolOrder_Click(object sender, EventArgs e)
        {
            if (m_Seat == null)
            {
                BathClass.printErrorMsg("先选择手牌!");
                return;
            }

            var dc = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(dc, LogIn.m_User, "完整点单") &&
                !BathClass.getAuthority(dc, LogIn.m_User, "可见本人点单"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            OrderForm orderForm = new OrderForm(m_Seat, LogIn.m_User, LogIn.connectionString, false);
            orderForm.ShowDialog();
            dgvExpense_show();
        }

        //退单 F10
        private void toolReturn_Click(object sender, EventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要输入订单!");
                return;
            }

            int orderId = MConvert<int>.ToTypeOrDefault(dgvExpense.CurrentRow.Cells[0].Value, 0);
            var order = dao.get_order("id", orderId);
            m_Seat = m_Seats.FirstOrDefault(x => x.systemId == order.systemId);
            if (order == null || order.menu.Contains("套餐优惠"))
            {
                BathClass.printErrorMsg("不能删除套餐优惠!");
                return;
            }

            InputEmployeeByPwd inputServerForm = new InputEmployeeByPwd();
            Employee del_employee;

            if (dao.get_authority(LogIn.m_User, "退单"))
                del_employee = LogIn.m_User;
            else if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;
            else if (!dao.get_authority(inputServerForm.employee, "退单"))
            {
                BathClass.printErrorMsg("没有退单权限");
                return;
            }
            else
                del_employee = inputServerForm.employee;

            var form = new DeleteExplainForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            string deleteExpalin = form.txt;
            string cmd_str = @"update [Orders] set deleteEmployee='" + del_employee.id + "', deleteExplain='"
             + deleteExpalin + "', deleteTime=getdate() where id=" + order.id;

            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("退单失败，请重试!");
                return;
            }

            BathClass.find_combo(LogIn.connectionString, m_Seat.systemId, m_Seat.text);
            dgvExpense_show();

            if (!dao.execute_command("update [OrderStockOut] set deleteEmployee='" + del_employee.id + "' where orderId=" + order.id.ToString()))
            {
                BathClass.printErrorMsg("退换消耗品失败!");
            }
        }

        //会员管理
        private void toolMember_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm(m_Seat);
            memberForm.ShowDialog();

            var dc_new = new BathDBDataContext(LogIn.connectionString);
            dgvExpense_show();
        }

        private void SeatExpenseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _close = true;

            //var dc = new BathDBDataContext(LogIn.connectionString);
            var lockSeat = MConvert<bool>.ToTypeOrDefault(LogIn.options.结账未打单锁定手牌, false);
            string cmd_str = @"update [Seat] set paying='False' where (";
            int count = m_Seats.Count;
            for (int i = 0; i < count; i++ )
            {
                cmd_str += @"text='" + m_Seats[i].text + "'";
                if (i != count - 1)
                    cmd_str += " or ";
            }
            cmd_str += ") ";

            if (lockSeat)
            {
                cmd_str += @"update [Seat] set status=4 where (status!=3 and status!=8 and ";

                string state_str = "";
                for (int i = 0; i < count; i++)
                {
                    if (dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False" && lockSeat)
                    {
                        state_str += @"text='" + m_Seats[i].text + "'";
                        if (i != count - 1)
                            state_str += " or ";
                    }
                }
                if (state_str != "")
                    cmd_str += "(" + state_str + ")";
                cmd_str += ")";
            }
            
            dao.execute_command(cmd_str);
            //for (int i = 0; i < m_Seats.Count; i++ )
            //{
            //    var s_new = dc.Seat.FirstOrDefault(x => x.text == m_Seats[i].text);
            //    s_new.paying = false;
            //    if (s_new.status != 3 && s_new.status != 8 && dgvChain.Rows[i].Cells[1].EditedFormattedValue.ToString() == "False")
            //    {
            //        if (lockSeat)
            //            s_new.status = 4;

            //        s_new.paying = false;
            //    }
            //}
            //dc.SubmitChanges();

            //if (m_thread_seatCard != null && m_thread_seatCard.IsAlive)
            //    m_thread_seatCard.Abort();

        }

        private void seatText_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void dgvChain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChain.CurrentRow == null)
                return;

            string id = dgvChain.CurrentRow.Cells[0].Value.ToString();
            m_Seat = m_Seats.FirstOrDefault(x => x.text == id);
            //if (!use_oyd)
            //{
            //    if (dgvChain.CurrentCell.ColumnIndex != 1)
            //        chain = false;
            //    else
            //        chain = true;

            //    setStatus();
            //    dgvExpense_show();
            //}
        }

        //预打账单
        private void toolPreprint_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(LogIn.connectionString);

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            printCols.Add("项目名称");
            printCols.Add("技师");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var ids = string.Join("|", m_Seats.OrderBy(x => x.text).Select(x => x.systemId).ToArray());
            var act = dc.Account.Where(x => x.systemId == ids && x.abandon == null).
                OrderByDescending(x => x.payTime).FirstOrDefault();
            PrePrintBill.Print_DataGridView(moneyPayable.Text,m_Seats, "预打账单", dgvExpense, printCols, companyName);
        }

        //加收过夜费
        private void order_guoye(CSeat seat)
        {
            if (!MConvert<bool>.ToTypeOrDefault(LogIn.options.自动加收过夜费, false) || seat.openTime == null || seat.systemId == null || seat.status == SeatStatus.REPAIED)
                return;

            if (dao.has_ordered_guoye(seat.systemId))
                return;

            var m_OverMenu = dao.get_Menu("name", "过夜费");
            //var m_OverMenu = dc.Menu.FirstOrDefault(x => x.name == "过夜费");
            if (m_OverMenu == null)
                return;

            DateTime lastday = now.AddDays(-1);
            string year = now.Year.ToString();
            string month = now.Month.ToString();
            string day = now.Day.ToString();
            string date = year+"-"+month+"-"+day+" ";
            string lastdate = lastday.Year.ToString() + "-" + lastday.Month.ToString() + "-" + lastday.Day.ToString() + " ";
            string time = ":00:00";

            int qi = MConvert<int>.ToTypeOrDefault(LogIn.options.过夜费起点, 0);
            int zh = MConvert<int>.ToTypeOrDefault(LogIn.options.过夜费终点, 0);
            DateTime st = DateTime.Parse(date + LogIn.options.过夜费起点 + time);
            if (qi > zh)
                st = DateTime.Parse(lastdate + LogIn.options.过夜费起点 + time);
            DateTime et = DateTime.Parse(date + LogIn.options.过夜费终点 + time);

            DateTime open_time = seat.openTime.Value;
            if ((now >= et && open_time >= et) || (open_time <= st && now <= st))
                return;

            string cmd_str = @"insert into [Orders](menu,text,systemId,number,money,inputTime,inputEmployee,paid) values("
                + @"'过夜费','" + seat.text + "','" + seat.systemId + "',1,'"
                + m_OverMenu.price.ToString() + "',getdate(),'过夜费','False')";
            
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("手牌:" + seat.text + "，过夜费添加失败，请手动添加!");
                return;
            }
            
            BathClass.find_combo(LogIn.connectionString, m_Seat.systemId, m_Seat.text);
        }

        private void payAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dgvExpense.Rows)
            {
                r.Cells[1].Value = payAll.Checked;
            }
        }

        //退房
        private void btnReturnRoom_Click(object sender, EventArgs e)
        {
            int cardNo = 0;
            byte[] buff = new byte[200];
            //int rt = RoomProRFL.initializeUSB(1);

            int hotelId = MConvert<int>.ToTypeOrDefault(IOUtil.get_config_by_key(ConfigKeys.KEY_HOTELID), -1);
            if (hotelId == -1)
            {
                BathClass.printErrorMsg("未定义酒店标志!");
                return;
            }

            int rt = RoomProRFL.CardErase(1, hotelId, buff);
            RoomProRFL.Buzzer(1, 40);
            if (rt != 0)
            {
                BathClass.printErrorMsg("退房失败!");
                return;
            }
            //string BDate = DateTime.Now.ToString("yyMMddHHmm");
            //rt = RoomProRFL.GuestCard(1, 2041242, cardNo, 0, 0, 1, BDate, BDate, m_Seat.oId, buff);
        }

        private void dgvExpense_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            int col = dgvExpense.CurrentCell.ColumnIndex;
            int row = dgvExpense.CurrentCell.RowIndex;
            if (col != 1)
                return;

            if (!payByOrder)
                return;

            bool _payAll = true;

            double money = 0;
            foreach (DataGridViewRow r in dgvExpense.Rows)
            {
                if (r.Cells[1].EditedFormattedValue.ToString() == "True")
                    money += Convert.ToDouble(r.Cells[8].Value);
                else
                    _payAll = false;
            }
            payAll.Checked = _payAll;
            moneyPayable.Text = money.ToString();
        }

        //查看手牌所在包厢
        private void toolCab_Click(object sender, EventArgs e)
        {
            InputSeatForm seatForm = new InputSeatForm(2);
            if (seatForm.ShowDialog() != DialogResult.OK)
                return;

            string seatId = seatForm.m_Seat.text;
            string room_name = dao.get_seat_room(seatId);
            if (room_name == "")
            {
                BathClass.printInformation("未找到手牌所在包厢");
            }
            else
            {
                BathClass.printInformation("手牌所在包为:" + room_name);
            }
        }

        //赠送微信优惠券
        private void ToolWxCoupon_Click(object sender, EventArgs e)
        {
            var form = new ExtendWxCouponForm();
            form.ShowDialog();
        }

        //扫微信赠送商品
        private void ToolWX_Click(object sender, EventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择行!");
                return;
            }

            Employee donor_user = null;
            
            var db = new BathDBDataContext(LogIn.connectionString);
            if (BathClass.getAuthority(db, LogIn.m_User, "微信赠送"))
            {
                donor_user = LogIn.m_User;
            }
            else
            {
                var form = new InputEmployeeByPwd();
                if (form.ShowDialog() != DialogResult.OK) return;
                if (!BathClass.getAuthority(db, form.employee, "微信赠送")) 
                {
                    BathClass.printErrorMsg("用户不具有微信赠送权限!");
                    return; 
                }

                donor_user = form.employee;
            }

            var sql = new StringBuilder();
            int orderId = MConvert<int>.ToTypeOrDefault(dgvExpense.CurrentRow.Cells[0].Value, 0);
            var order = dao.get_order("id", orderId);
            if (order.money == 0 && !StringUtil.isEmpty(order.donorExplain) && order.donorExplain==Constants.WX_DONOR)
            {
                sql.Append("update [Orders] set donorEmployee=null, donorExplain=null,donorTime=null,");
                sql.Append("money=orders.number*(select price from menu where menu.name=orders.menu) where ");
                sql.Append(" id=").Append(orderId);
            }
            else
            {
                sql.Append("update [Orders] set donorEmployee='").Append(donor_user.name).Append("', ");
                sql.Append(" donorExplain='").Append(Constants.WX_DONOR).Append("',donorTime=getdate(),money=0 where ");
                sql.Append(" id=").Append(orderId);
            }
            
            
            if (!dao.execute_command(sql.ToString()))
            {
                BathClass.printErrorMsg("微信赠送失败，请重试!");
                return;
            }
            dgvExpense_show();
        }
    }
}
