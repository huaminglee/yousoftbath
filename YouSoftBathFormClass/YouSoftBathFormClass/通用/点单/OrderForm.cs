using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Data.SqlClient;
using System.Threading;
using YouSoftUtil;

namespace YouSoftBathFormClass
{
    //消费点单
    public partial class OrderForm : Form
    {
        //成员变量
        private DAO dao;
        private CSeat m_Seat = null;
        private int m_index = 0;
        private CCatgory m_Catgory;
        private List<string> typeList = new List<string>();
        private bool inputBillId = false;
        private Employee m_user;
        private string m_con_str;
        private bool m_has_user = false;//已经输入服务员工号
        private bool inputTechType;//区分点钟轮钟
        private COptions m_options;
        private CStock stock = null;

        private bool first_time = true;
        private List<string> m_Orders = new List<string>();//用以记录当前所点订单，区分退单和删除
        
        //构造函数
        public OrderForm(CSeat seat, Employee user, string con_str, bool has_user)
        {
            //st = DateTime.Now;
            //db = new BathDBDataContext(con_str);
            //m_Seat = db.Seat.FirstOrDefault(x => x.systemId == seat.systemId);
            m_Seat = seat;
            m_user = user;
            m_has_user = has_user;
            m_con_str = con_str;

            InitializeComponent();
            abcOrderPanel.Dock = DockStyle.Fill;
            touchOrderPanel.Dock = DockStyle.Fill;
            abcOrderPanel.Visible = false;
        }

        //对话框载入
        private void OrderForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(m_con_str);
            if (!dao.execute_command("update [Seat] set ordering='True' where text='" + m_Seat.text + "'"))
            {
                BathClass.printErrorMsg("手牌状态更新失败!");
                return;
            }
            m_options = dao.get_options();
            if (typeList.Count == 0)
                typeList = dao.get_catgories(null);
                //typeList = db.Catgory.Select(x => x.name).ToList();

            dgvExpense.Columns[11].Visible = MConvert<bool>.ToTypeOrDefault(m_options.启用客房面板, false);
            inputBillId = MConvert<bool>.ToTypeOrDefault(m_options.录单输入单据编号, false);
            inputTechType = MConvert<bool>.ToTypeOrDefault(m_options.录单区分点钟轮钟, false);
            string local_ip = BathClass.get_local_ip();
            stock = dao.get_Stock("select * from [Stock] where ips='"+local_ip+"'");

            this.Invoke(new no_par_delegate(initial_ui), null);

            //MessageBox.Show((DateTime.Now - st).TotalMilliseconds.ToString());
        }

        private delegate void no_par_delegate();
        private void initial_ui()
        {
            createMenuTypePanel();

            dgvMenu.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvMenu.RowsDefaultCellStyle.Font = new Font("宋体", 20);

            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);

            dgvExpense.Columns[1].Visible = inputBillId;
            dgvExpense.Columns[0].Visible = false;
            dgvExpense_show();

            btnTiming.Visible = false;
            dgvExpense_CellClick(null, null);
        }

        //显示台位消费信息
        public void dgvExpense_show()
        {
            dgvExpense.Rows.Clear();
            if (m_Seat == null)
                return;

            //IQueryable<Orders> orders = m_Orders.AsQueryable();
            //if (BathClass.getAuthority(db, m_user, "完整点单"))
            //    orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null && !x.paid);
            //orders = orders.OrderBy(x => x.inputTime);

            DateTime now = DateTime.Now;
            SqlConnection sqlCn = null;
            StringBuilder sb = new StringBuilder();
            try
            {
                sqlCn = new SqlConnection(LogIn.connectionString);
                sqlCn.Open();

                sb.Append("Select * from [Orders] where deleteEmployee is null and paid='False' and systemId='" + m_Seat.systemId + "'");
                if (!dao.get_authority(m_user, "完整点单"))
                    sb.Append(" and inputEmployee='" + m_user.id + "'");
                sb.Append(" order by inputTime");
                var cmd = new SqlCommand(sb.ToString(), sqlCn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] row = new string[12];

                        row[0] = dr["id"].ToString();

                        if (first_time)
                            m_Orders.Add(row[0]);
                        row[1] = dr["billId"].ToString();
                        row[2] = dr["text"].ToString();
                        row[3] = dr["menu"].ToString();
                        row[4] = dr["technician"].ToString();
                        row[5] = dr["techType"].ToString();

                        row[7] = dr["number"].ToString();

                        row[9] = Convert.ToDateTime(dr["inputTime"]).ToString("MM-dd HH:mm");
                        row[10] = dr["inputEmployee"].ToString();
                        row[11] = dr["roomId"].ToString();

                        var cmenu = dao.get_Menu("name", dr["menu"].ToString());

                        bool redRow = false;
                        var order_money = Convert.ToDouble(dr["money"]);
                        if (cmenu == null)
                        {
                            row[6] = "";
                            row[8] = order_money.ToString();
                            redRow = true;
                        }
                        else
                        {
                            if (dr["priceType"].ToString() == "每小时")
                            {
                                double order_money_p = Math.Ceiling((now - Convert.ToDateTime(dr["inputTime"])).TotalHours) * order_money;
                                row[6] = dr["money"].ToString() + "/时";
                                row[8] = order_money_p.ToString();
                            }
                            else
                            {
                                row[6] = cmenu.price.ToString();
                                row[8] = order_money.ToString();
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
                first_time = false;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(sb.ToString());
                BathClass.printErrorMsg(e.ToString());
            }
            finally
            {
                dao.close_connection(sqlCn);
            }

            //dgvExpense.DataSource = from x in orders
            //                        orderby x.systemId, x.inputTime
            //                        select new
            //                        {
            //                            编号 = x.id,
            //                            单据号 = x.billId,
            //                            手牌号 = x.text,
            //                            项目名称 = x.menu,
            //                            技师号 = x.technician,
            //                            服务类型 = x.techType,
            //                            单价 = db.Menu.FirstOrDefault(y => y.name == x.menu) == null ? 0 : db.Menu.FirstOrDefault(y => y.name == x.menu).price,
            //                            数量 = x.number,
            //                            金额 = (x.priceType == "每小时") ? (int)((BathClass.Now(LogIn.connectionString) - x.inputTime).TotalHours * x.money) : x.money,
            //                            消费时间 = x.inputTime,
            //                            录入员工 = x.inputEmployee
            //                        };
            //dgvExpense.Columns[0].Visible = false;
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

            //    row[9] = o.inputTime.ToString("MM-dd HH:mm");
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
            //{
            //    dgvExpense.CurrentCell = dgvExpense[2, dgvExpense.Rows.Count - 1];
            //}
        }

        private Single btnTextSize(string txt)
        {
            Single fs = 18F;

            int l = txt.Length;
            if (l <= 4) fs = 18F;
            else if (l > 4 && l <= 6) fs = 15F;
            else if (l > 6 && l <= 8) fs = 12F;
            else if (l > 8 && l <= 10) fs = 9F;

            return fs;
        }

        //创建单个台位按钮
        private void createButton(int x, int y, string txt, Control sp, int type, Color color)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", btnTextSize(txt), FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = txt;
            btn.Text = txt;
            btn.Size = new System.Drawing.Size(70, 70);
            btn.UseVisualStyleBackColor = true;
            btn.FlatStyle = FlatStyle.Popup;
            btn.BackColor = color;
            btn.TabStop = false;
            btn.ForeColor = Color.White;

            if (type == 1)
                btn.Click += new System.EventHandler(btnType_Click);
            else
                btn.Click += new System.EventHandler(btnMenu_Click);

            sp.Controls.Add(btn);
        }

        //创建技师按钮
        /*private void createTechButton(int x, int y, string txt, Control sp)
        {
            Button btn = new Button();
            btn.AutoEllipsis = true;
            btn.AutoSize = true;
            btn.FlatAppearance.BorderColor = Color.LightGray;
            btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.RoyalBlue;
            btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSteelBlue;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.ImageList = imageList1;
            btn.ImageIndex = 0;
            btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = txt;
            btn.Text = txt;
            btn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            btn.UseVisualStyleBackColor = true;

            //btn.Click += new System.EventHandler(btnAvi_Click);

            sp.Controls.Add(btn);
        }*/

        //创建菜单类别界面
        private void createMenuTypePanel()
        {
            cPanel.Controls.Clear();
            bool first = true;
            int x = 10;
            int y = 9;

            for (int i = m_index; i < typeList.Count(); i++ )
            {
                Color color = first ? Color.Orange : Color.Blue;
                first = false;
                createButton(x, y, typeList[i], cPanel, 1, color);
                x += 80;
            }
            m_Catgory = dao.get_Catgory("name='" + typeList[0] + "'");
            //m_Catgory = db.Catgory.FirstOrDefault(z => z.name == typeList[0]);
            createMenuPanel();
        }

        //创建菜单界面
        private void createMenuPanel()
        {
            mPanel.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;
            Size clientSize = this.Size;

            var menuList = dao.get_cat_menus(m_Catgory.name);
            //var menuList = db.Menu.Where(s => s.catgoryId == m_Catgory.id).Select(s => s.name).ToList();
            int count = menuList.Count;
            while(index < count)
            {
                while ((col + 1) * 80 < clientSize.Width && index < count)
                {
                    int x = col * 70 + 10 * (col + 1);
                    int y = row * 70 + 10 * (row + 1);
                    createButton(x, y, menuList[index], mPanel, 2, Color.Orange);
                    col++;
                    index++;
                }
                col = 0;
                row++;
            }
        }

        //创建技师列表
        /*private void createTechnicianPanel(List<int> tLst)
        {
            mPanel.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;
            int count = tLst.Count;
            while (index < count)
            {
                while ((col + 1) * 80 < this.Size.Width && index < count)
                {
                    int x = col * 70 + 10 * (col + 1);
                    int y = row * 70 + 10 * (row + 1);
                    createTechButton(x, y, tLst[index].ToString(), mPanel);
                    col++;
                    index++;
                }
                col = 0;
                row++;
            }
        }*/

        //点击菜式类别
        private void btnType_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            foreach (Control c in cPanel.Controls)
                c.BackColor = Color.Blue;
            btn.BackColor = Color.Orange;

            m_Catgory = dao.get_Catgory("name='" + btn.Text + "'");
            //m_Catgory = db.Catgory.FirstOrDefault(x => x.name == btn.Text);
            createMenuPanel();
        }

        //点击菜式
        private void btnMenu_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.Enabled = false;
            btn.Enabled = true;
            var menu = dao.get_Menu("name", btn.Text);
            //var menu = db.Menu.FirstOrDefault(x => x.name == ((Button)sender).Text);
            order_menu(menu);
        }

        private void order_menu(CMenu menu)
        {
            string pars = "menu,text,systemId,number,money,inputTime,paid";
            string vals = "'" + menu.name + "','" + m_Seat.text + "','" + m_Seat.systemId + "',1," + menu.price + ",getdate(),'False'";

            if (inputBillId)
            {
                var form = new InputNumberStr("输入单据编号");
                if (form.ShowDialog() != DialogResult.OK)
                {
                    BathClass.printErrorMsg("需要输入单据编号");
                    return;
                }
                pars += ",billId";
                vals += ",'" + form.str + "'";
            }

            if (menu.technician)
            {
                pars += ",technician";
                if (!m_has_user)
                {
                    InputEmployee techForm = new InputEmployee("请输入技师号...");
                    if (techForm.ShowDialog() != DialogResult.OK)
                    {
                        BathClass.printErrorMsg("需要选择技师!");
                        return;
                    }
                    vals += ",'" + techForm.employee.id + "'";
                }
                else
                    vals += ",'" + m_user.id + "'";

                //是否需要输入点钟轮钟
                if (inputTechType)
                {
                    var form = new DianLunForm();
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        BathClass.printErrorMsg("需要输入上钟类型!");
                        return;
                    }
                    pars += ",techType";
                    vals += ",'" + form.tech_type + "'";
                }
            }
            if (!m_has_user && MConvert<bool>.ToTypeOrDefault(menu.waiter, false))
            {
                InputEmployee techForm = new InputEmployee("请输入服务员工号...");
                if (techForm.ShowDialog() != DialogResult.OK)
                {
                    BathClass.printErrorMsg("需要输入录单员!");
                    return;
                }
                pars += ",inputEmployee";
                vals += ",'" + techForm.employee.id + "'";
            }
            else
            {
                pars += ",inputEmployee";
                vals += ",'" + m_user.id + "'";
            }

            string cmd_str = @"insert into [Orders](" + pars + ") values(" + vals + ")";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("录单失败，请重试！");
                return;
            }

            //BathClass.find_combo_duplicated(m_con_str, m_Seat.systemId, m_Seat.text);
            BathClass.find_combo(m_con_str, m_Seat.systemId, m_Seat.text);
            dgvExpense_show();
            Thread td = new Thread(delegate() { print_kitchen(menu); });
            td.Start();
        }

        private void print_kitchen(CMenu menu)
        {
            var cat = dao.get_Catgory("id=" + menu.catgoryId.ToString());
            //var cat = db.Catgory.FirstOrDefault(x => x.id == menu.catgoryId);
            if (cat != null && cat.kitchPrinterName != null && cat.kitchPrinterName != "")
            {
                PrintKitchen.Print_DataGridView(m_Seat.text, menu.name, cat.kitchPrinterName, m_user.name, "1",
                    DateTime.Now.ToString("MM-dd HH:mm"), "");
            }
        }

        /*private void reset_order_money(Orders order)
        {
            var menu = db.Menu.FirstOrDefault(x => x.name == order.menu);
            if (menu != null)
            {
                if (order.priceType == "每小时")
                    order.money = Convert.ToDouble(menu.addMoney);
                else if (order.comboId == null)
                    order.money = menu.price;
                else if (order.comboId != null)
                {
                    var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
                    if (combo == null)
                        return;
                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                    var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    if (!freeMenus.Contains(order.menu))
                        order.money = menu.price;
                }
            }
            else
            {
                var combo = db.Combo.FirstOrDefault(x => x.id == order.comboId);
                order.money = BathClass.get_combo_price(db, combo);
            }
        }*/

        //左移
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (m_index == typeList.Count - 1)
                return;

            m_index += 1;
            createMenuTypePanel();
        }

        //右移动
        private void btnRight_Click(object sender, EventArgs e)
        {
            if (m_index == 0)
                return;
            
            m_index -= 1;
            createMenuTypePanel();
        }

        //退单
        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要输入订单!");
                return;
            }

            string orderId = dgvExpense.CurrentRow.Cells[0].Value.ToString();
            //var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            if (dgvExpense.CurrentRow.Cells[3].Value.ToString().Contains("套餐优惠"))
            {
                BathClass.printErrorMsg("输入订单编号错误!");
                return;
            }
            if (!m_Orders.Contains(orderId))
            {
                if (BathClass.printAskMsg("没入数据库，直接删除?") == DialogResult.Yes)
                {
                    string cmd_str = "delete from [Orders] where id=" + orderId;
                    if (!dao.execute_command(cmd_str))
                    {
                        BathClass.printErrorMsg("退单失败，请重试!");
                        return;
                    }
                    //db.Orders.DeleteOnSubmit(order);
                    //db.SubmitChanges();

                    BathClass.find_combo(m_con_str, m_Seat.systemId, m_Seat.text);
                    //BathClass.find_combo(db, m_Seat, m_Orders);
                    //m_Orders.Remove(order);
                    dgvExpense_show();
                }
                return;
            }

            if (dao.get_authority(m_user, "退单"))
            {
                return_order(orderId, m_user);
                return;
            }

            InputEmployeeByPwd inputServerForm = new InputEmployeeByPwd(m_con_str);
            if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.get_authority(inputServerForm.employee, "退单"))
            {
                BathClass.printErrorMsg("权限不够");
                return;
            }

            return_order(orderId, inputServerForm.employee);
        }

        //退单
        private void return_order(string orderId, Employee del_employee)
        {
            var form = new DeleteExplainForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            string deleteExpalin = form.txt;
            string cmd_str = @"update [Orders] set deleteEmployee='" + del_employee.id + "', deleteExplain='"
             +deleteExpalin + "', deleteTime=getdate() where id=" + orderId;
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("退单失败!");
                return;
            }

            BathClass.find_combo(m_con_str, m_Seat.systemId, m_Seat.text);
            dgvExpense_show();

            if (!dao.execute_command("update [OrderStockOut] set deleteEmployee='" + del_employee.id + "' where orderId=" + orderId))
            {
                BathClass.printErrorMsg("退换消耗品失败!");
            }
        }


        //清屏
        private void btnClear_Click(object sender, EventArgs e)
        {
            string state_str = "";
            int count = dgvExpense.Rows.Count;
            for (int i = 0; i < count; i++ )
            {
                string orderId = dgvExpense.Rows[i].Cells[0].Value.ToString();
                if (m_Orders.Contains(orderId))
                    continue;
                state_str += "id=" + dgvExpense.Rows[i].Cells[0].Value.ToString();
                if (i != count - 1)
                    state_str += " or ";
            }

            if (state_str == "")
            {
                BathClass.printErrorMsg("当前没有点单，不能清屏!");
                return;
            }
            string cmd_str = "delete from [Orders] where(" + state_str + ")";
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("清屏失败!");
                return;
            }
            //db.Orders.DeleteAllOnSubmit(m_Orders.ToArray());
            //db.SubmitChanges();

            //m_Orders.Clear();

            //修改
            BathClass.find_combo(m_con_str, m_Seat.systemId, m_Seat.text);
            //BathClass.find_combo(db, m_Seat, m_Orders);
            dgvExpense_show();
        }

        //设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            if (btnSet.Text.Contains("首拼"))
            {
                btnSet.Text = "触摸(F1)";
                touchOrderPanel.Visible = false;
                abcOrderPanel.Visible = true;
                menuABC.Focus();
            }
            else
            {
                btnSet.Text = "首拼(F1)";
                abcOrderPanel.Visible = false;
                touchOrderPanel.Visible = true;
            }
            //OrderSettingForm orderSettingForm = new OrderSettingForm(typeList);
            //if (orderSettingForm.ShowDialog() == DialogResult.OK)
            //{
            //    typeList = orderSettingForm.typeList;
            //    write_File();
            //    read_File();
            //    createMenuTypePanel();
            //}
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //数量
        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择订单!");
                return;
            }

            string orderId = dgvExpense.CurrentRow.Cells[0].Value.ToString();
            //var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            //if (order == null)
            //{
            //    BathClass.printErrorMsg("选择订单编号错误!");
            //    return;
            //}

            InputNumber inputServerForm = new InputNumber("输入点单数量", false);
            if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;

            double new_number = inputServerForm.number;
            if (new_number == 0)
            {
                BathClass.printErrorMsg("输入数量不能为0!");
                return;
            }

            double money = Convert.ToDouble(dgvExpense.CurrentRow.Cells[8].Value);
            double number = Convert.ToDouble(dgvExpense.CurrentRow.Cells[7].Value);
            double unit = money / number;
            //double unit = Convert.ToDouble(dgvExpense.CurrentRow.Cells[]) / order.number;
            string cmd_str = "update [Orders] set number=" + new_number + ",money=" + new_number * unit
                + " where id=" + orderId;
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("数量修改失败！");
                return;
            }
            //order.number = inputServerForm.number;
            //order.money = unit * order.number;
            //db.SubmitChanges();
            dgvExpense_show();
        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要输入订单!");
                return;
            }

            string orderId = dgvExpense.CurrentRow.Cells[0].Value.ToString();
            //var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            //if (order == null)
            //{
            //    BathClass.printErrorMsg("输入订单编号错误!");
            //    return;
            //}
            if (m_Orders.Contains(orderId))
            {
                BathClass.printErrorMsg("已点订单,不能删除!");
                return;
            }

            if (!dao.execute_command("delete from [Orders] where id="+orderId))
            {
                BathClass.printErrorMsg("删除订单失败!");
                return;
            }
            //db.Orders.DeleteOnSubmit(order);
            //db.SubmitChanges();

            BathClass.find_combo(m_con_str, m_Seat.systemId, m_Seat.text);
            //m_Orders.Remove(order);
            //BathClass.find_combo(db, m_Seat, m_Orders);
            dgvExpense_show();
        }

        //读取历史员工号
        private void read_File()
        {
            if (!Directory.Exists(@".\config"))
                Directory.CreateDirectory(@".\config");

            if (!File.Exists(@".\config\itemType.ini"))
            {
                using (FileStream fs = new FileStream(@".\config\itemType.ini", FileMode.Create))
                    return;
            }
            using (StreamReader sr = new StreamReader(@".\config\itemType.ini"))
            {
                typeList.Clear();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string tp = line.Trim();
                    if (tp.EndsWith("Y"))
                        typeList.Add(tp.Substring(0, tp.Length-2));
                }
            }
        }

        //写入本地配置文件
        /*private void write_File()
        {
            using (StreamWriter sw = new StreamWriter(@".\config\itemType.ini", false))
            {
                List<string> catList = db.Catgory.Select(x => x.name).ToList();
                foreach (string cat in catList)
                {
                    if (typeList.Contains(cat))
                        sw.WriteLine(cat + "=Y");
                    else
                        sw.WriteLine(cat + "=N");
                }
            }
        }*/

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F1:
                    btnSet_Click(null, null);
                    break;
                case Keys.F2:
                    btnReturn_Click(null, null);
                    break;
                case Keys.F3:
                    btnClear_Click(null, null);
                    break;
                case Keys.F4:
                    btnNumber_Click(null, null);
                    break;
                case Keys.F5:
                    btnDelete_Click(null, null);
                    break;
                case Keys.Enter:
                    if (menuABC.ContainsFocus && menuABC.Text != "")
                        break;
                    btnSave_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //修改技师号
        private void dgvExpense_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
                return;

            string orderId = dgvExpense.CurrentRow.Cells[0].Value.ToString();
            string techId = dgvExpense.CurrentRow.Cells[4].Value.ToString();
            //var order = db.Orders.FirstOrDefault(x => x.id.ToString() == dgvExpense.CurrentRow.Cells[0].Value.ToString());
            if (techId == null || techId == "")
                return;

            if (BathClass.printAskMsg("当前技师为" + techId + "确认更换技师?") != DialogResult.Yes)
                return;

            var form = new InputEmployee("请输入技师号...");
            if (form.ShowDialog() != DialogResult.OK)
                return;

            if (!dao.execute_command("update [Orders] set technician='"+form.employee.id+"' where id="+orderId))
            {
                BathClass.printErrorMsg("技师号修改失败，请重试!");
                return;
            }
            dgvExpense.CurrentRow.Cells[4].Value = form.employee.id;
            //var tech = form.employee;
            //order.technician = tech.id;
            //db.SubmitChanges();
            //dgvExpense_show();
        }

        //输入项目首拼
        private void menuABC_TextChanged(object sender, EventArgs e)
        {
            dgvMenu.Rows.Clear();
            if (menuABC.Text == "")
                return;

            string ct = menuABC.Text;
            var menus = dao.get_Menus(null);
            foreach (var menu in menus)
            {
                string id = menu.id.ToString();
                string spell = GetStringSpell.GetChineseSpell(menu.name).ToUpper();
                if (id.IndexOf(ct) == 0 || spell.IndexOf(ct.ToUpper()) == 0)
                {
                    dgvMenu.Rows.Add(id, menu.name, menu.price.ToString());
                }
            }
        }

        private void menuABC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvMenu.Rows.Count == 0)
                    return;

                var id = dgvMenu.Rows[0].Cells[0].Value.ToString();
                var menu = dao.get_Menu("id", id);
                //var menu = db.Menu.FirstOrDefault(x => x.id.ToString() == id);
                order_menu(menu);
                menuABC.Text = "";
            }
        }

        private void dgvMenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dgvMenu.CurrentRow.Cells[0].Value.ToString();
            var menu = dao.get_Menu("id", id);
            //var menu = db.Menu.FirstOrDefault(x => x.id.ToString() == id);
            order_menu(menu);
            menuABC.Text = "";
        }

        private void menuABC_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void dgvExpense_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvExpense.CurrentRow == null)
                return;

            var menu = dao.get_Menu("name", dgvExpense.CurrentRow.Cells[3].Value.ToString());
            //var menu = db.Menu.FirstOrDefault(x => x.name == dgvExpense.CurrentRow.Cells[3].Value.ToString());
            if (menu == null)
            {
                btnTiming.Visible = false;
                return;
            }

            if (menu.addAutomatic)
            {
                var orderId = dgvExpense.CurrentRow.Cells[0].Value.ToString();
                var order = dao.get_order("id", orderId);
                //var order = db.Orders.FirstOrDefault(x => x.id == orderId);
                if (order.stopTiming == null)
                {
                    btnTiming.Visible = true;
                    btnTiming.Text = "停止计时";
                }
            }
            else
                btnTiming.Visible = false;
        }

        //停止计时、继续计时
        private void btnTiming_Click(object sender, EventArgs e)
        {
            string orderId = dgvExpense.CurrentRow.Cells[0].Value.ToString();
            //var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            if (BathClass.printAskMsg("确定停止计时?") != DialogResult.Yes)
                return;

            if (btnTiming.Text == "停止计时" && btnTiming.Visible)//停止计时状态
            {
                var cmd_str = "update [Orders] set money=money*ceiling(datediff(minute,inputTime, getdate())/60.0),"
                    + "priceType='停止消费' where id=" + orderId + " and priceType='每小时'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("停止计时失败!");
                    return;
                }
                //var orders = db.Orders.Where(x => x.systemId == order.systemId && x.menu == order.menu &&
                //    x.deleteEmployee == null && !x.paid);
                //foreach (var od in orders)
                //{
                //    od.stopTiming = true;
                //    if (od.priceType == "每小时")
                //    {
                //        od.priceType = "停止消费";
                //        od.money = order.money * Math.Ceiling((DateTime.Now - order.inputTime).TotalHours);
                //    }
                //}
            }

            //db.SubmitChanges();
            btnTiming.Visible = false;
        }


        private void OrderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread td = new Thread(new ThreadStart(deal_with_storage));
            td.Start();
        }

        //处理库存
        private void deal_with_storage()
        {
            try
            {
                dao.execute_command("update [Seat] set ordering='False' where text='" + m_Seat.text + "'");
                if (stock == null)
                    return;

                StringBuilder sb = new StringBuilder();
                foreach (DataGridViewRow r in dgvExpense.Rows)
                {
                    string id = r.Cells[0].Value.ToString();
                    if (m_Orders.Contains(id))
                        continue;

                    string menu = r.Cells[3].Value.ToString();
                    double number = Convert.ToDouble(r.Cells[7].Value);
                    string sales = r.Cells[10].Value.ToString();
                    var cmenu = dao.get_Menu("name", menu);
                    if (cmenu == null)
                        continue;

                    var res = cmenu.disAssemble_Menu_resourceExpense();

                    foreach (KeyValuePair<string, double> re in res)
                    {
                        sb.Append("insert into [OrderStockOut](name,amount,stockId,date,sales,orderId) values('");
                        sb.Append(re.Key + "'," + re.Value.ToString() + "," + stock.id + ",getdate(),'" + sales + "'," + id + ")");
                    }

                }
                if (sb.ToString() != "" && !dao.execute_command(sb.ToString()))
                {
                    BathClass.printErrorMsg("库存更新失败!");
                }
                sb = null;
            }
            catch
            {
            }
        }
    }
}
