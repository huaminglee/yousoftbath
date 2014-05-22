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

namespace YouSoftBathOrder
{
    //消费点单
    public partial class OrderForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Seat m_Seat = null;
        private Employee m_Employee = null;
        private int m_index = 0;
        private Catgory m_Catgory;
        private List<string> typeList = new List<string>();

        private List<Orders> m_Orders = new List<Orders>();//用以记录当前所点订单，区分退单和删除

        //构造函数
        public OrderForm(Seat seat, Employee employee)
        {
            db = new BathDBDataContext(MainWindow.connectionString);
            m_Seat = db.Seat.FirstOrDefault(x => x == seat);
            m_Employee = employee;
            read_File();
            if (typeList.Count == 0)
                typeList = db.Catgory.Select(x => x.name).ToList();

            InitializeComponent();
            createMenuTypePanel();
        }

        //对话框载入
        private void OrderForm_Load(object sender, EventArgs e)
        {
            dgvExpense.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgvExpense.RowsDefaultCellStyle.Font = new Font("宋体", 20);     
            dgvExpense_show();
        }

        //显示台位消费信息
        public void dgvExpense_show()
        {
            dgvExpense.Rows.Clear();
            if (m_Seat == null)
                return;

            List<Orders> orders = new List<Orders>();
            if (BathClass.getAuthority(db, m_Employee, "完整点单"))
                orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null).ToList();
            else if (BathClass.getAuthority(db, m_Employee, "可见本人点单"))
                orders = m_Orders;
            orders = orders.OrderBy(x => x.inputTime).ToList();
            foreach (var o in orders)
            {
                string[] row = new string[10];
                row[0] = o.id.ToString();
                row[1] = o.text;
                row[2] = o.menu;
                row[3] = o.technician;
                row[4] = o.techType;

                row[6] = o.number.ToString();

                row[8] = o.inputTime.ToString();
                row[9] = o.inputEmployee;

                var m = db.Menu.FirstOrDefault(x => x.name == o.menu);
                bool redRow = false;
                if (m == null)
                {
                    row[5] = "";
                    row[7] = o.money.ToString();
                    redRow = true;
                }
                else
                {
                    if (o.priceType == "每小时")
                    {
                        row[5] = o.money.ToString() + "/时";
                        row[7] = Math.Round((GeneralClass.Now - o.inputTime).TotalHours * o.money, 0).ToString();
                    }
                    else
                    {
                        row[5] = m.price.ToString();
                        row[7] = o.money.ToString();
                    }
                }

                dgvExpense.Rows.Add(row);
                if (redRow)
                {
                    dgvExpense.Rows[dgvExpense.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            GeneralClass.set_dgv_fit(dgvExpense);
            if (dgvExpense.Rows.Count != 0)
                dgvExpense.CurrentCell = dgvExpense[0, dgvExpense.Rows.Count - 1];
        }

        //创建单个台位按钮
        private void createButton(int x, int y, string txt, Control sp, int type, Color color)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 18F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = txt;
            btn.Text = txt;
            btn.Size = new System.Drawing.Size(70, 70);
            btn.UseVisualStyleBackColor = true;
            btn.FlatStyle = FlatStyle.Popup;
            btn.BackColor = color;
            btn.ForeColor = Color.White;

            if (type == 1)
                btn.Click += new System.EventHandler(btnType_Click);
            else
                btn.Click += new System.EventHandler(btnMenu_Click);

            sp.Controls.Add(btn);
        }

        //创建技师按钮
        private void createTechButton(int x, int y, string txt, Control sp)
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

            sp.Controls.Add(btn);
        }

        //创建菜单类别界面
        private void createMenuTypePanel()
        {
            cPanel.Controls.Clear();
            bool first = true;
            int x = 10;
            int y = 9;

            //createButton(x, y, "技师", cPanel, 1, Color.Blue);
            //x += 80;

            //var typeLst = db.Catgory.Select(s => s.name);
            for (int i = m_index; i < typeList.Count(); i++ )
            {
                Color color = first ? Color.Orange : Color.Blue;
                first = false;
                createButton(x, y, typeList.ToArray()[i], cPanel, 1, color);
                x += 80;
            }
            m_Catgory = db.Catgory.FirstOrDefault(z => z.name == typeList[0]);
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

            var menuList = db.Menu.Where(s => s.catgoryId == m_Catgory.id).Select(s => s.name).ToList();
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
        private void createTechnicianPanel(List<int> tLst)
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
        }

        //点击菜式类别
        private void btnType_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            foreach (Control c in cPanel.Controls)
                c.BackColor = Color.Blue;
            btn.BackColor = Color.Orange;

            m_Catgory = db.Catgory.FirstOrDefault(x => x.name == btn.Text);
            createMenuPanel();
        }

        //点击菜式
        private void btnMenu_Click(object sender, EventArgs e)
        {
            YouSoftBathGeneralClass.Menu menu = db.Menu.FirstOrDefault(x => x.name == ((Button)sender).Text);
            Orders order = new Orders();
            order.menu = menu.name;
            order.text = m_Seat.text;
            order.systemId = m_Seat.systemId;
            order.number = 1;
            order.money = menu.price;
            order.inputTime = GeneralClass.Now;
            order.inputEmployee = m_Employee.id.ToString();
            order.paid = false;
            //m_Seat.money += menu.price;
            if (menu.technician)
            {
                InputServerId techForm = new InputServerId("", false);
                if (techForm.ShowDialog() != DialogResult.OK)
                {
                    GeneralClass.printErrorMsg("需要选择技师!");
                    return;
                }
                order.technician = techForm.emplyee.id.ToString();
                //var tech = db.Employee.FirstOrDefault(x => x.id.ToString() == technicianForm.m_tId);
            }

            db.Orders.InsertOnSubmit(order);
            db.SubmitChanges();

            m_Orders.Add(order);
            //findComboOfOrder(order);
            find_combo();
            dgvExpense_show();
        }

        private void find_combo()
        {
            TransactionOptions transOptions = new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted };
            {
                db.Orders.DeleteAllOnSubmit(db.Orders.Where(x => x.menu.Contains("套餐")));
                var orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null);
                orders = orders.Where(x => !x.inputEmployee.Contains("电脑"));
                foreach (Orders tmp_order in orders)
                {
                    tmp_order.comboId = null;
                }
                db.SubmitChanges();

                var order_menus = orders.Where(x => x.comboId == null).Select(x => x.menu);
                var menus = db.Menu;
                var comboList = db.Combo.OrderByDescending(x => x.originPrice - x.price);
                foreach (Combo combo in comboList)
                {
                    List<int> menuIds = BathClass.disAssemble(combo.menuIds);
                    var combo_menus = menus.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
                    if (combo_menus.All(x => order_menus.Any(y => y == x)))
                    {
                        foreach (var combo_menu in combo_menus)
                        {
                            var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                            tmp_order.comboId = combo.id;
                            if (combo.priceType == "免项目")
                            {
                                var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                                var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                                if (freeMenus.Contains(tmp_order.menu))
                                    tmp_order.money = 0;
                            }
                        }
                        if (combo.priceType == "减金额")
                        {
                            Orders comboOrder = new Orders();
                            comboOrder.menu = "套餐" + combo.id.ToString() + "优惠";
                            comboOrder.text = m_Seat.text;
                            comboOrder.systemId = m_Seat.systemId;
                            comboOrder.number = 1;
                            comboOrder.inputTime = GeneralClass.Now;
                            comboOrder.inputEmployee = "套餐";
                            comboOrder.paid = false;
                            comboOrder.comboId = combo.id;
                            comboOrder.money = Convert.ToDouble(combo.price) - combo.originPrice;

                            db.Orders.InsertOnSubmit(comboOrder);
                            m_Orders.Add(comboOrder);
                        }
                        db.SubmitChanges();
                    }
                }
            }
        }

        /*private void find_combo()
        {
            var orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null);
            orders = orders.Where(x => !x.inputEmployee.Contains("电脑"));
            foreach (Orders order in orders)
            {
                order.comboId = null;
                if (order.menu.Contains("套餐"))
                    db.Orders.DeleteOnSubmit(order);
            }
            db.SubmitChanges();

            var comboList = db.Combo.OrderByDescending(x => x.originPrice - x.price);
            foreach (Combo combo in comboList)
            {
                List<int> menuIds = BathClass.disAssemble(combo.menuIds);
                List<Orders> found_orders = new List<Orders>();
                bool found = true;
                foreach (int tmpId in menuIds)
                {
                    var m = db.Menu.FirstOrDefault(x => x.id == tmpId);
                    if (m == null)
                    {
                        found = false;
                        break;
                    }
                    string menu = m.name;
                    Orders found_order = orders.FirstOrDefault(x => x.menu == menu && x.comboId == null);
                    if (found_order == null)
                    {
                        found = false;
                        break;
                    }
                    found_orders.Add(found_order);
                }

                if (found)
                {
                    foreach (Orders tmpOrder in found_orders)
                    {
                        tmpOrder.comboId = combo.id;
                        if (combo.priceType == "免项目")
                        {
                            var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                            var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                            if (freeMenus.Contains(tmpOrder.menu))
                                tmpOrder.money = 0;
                        }
                    }
                    if (combo.priceType == "减金额")
                    {
                        Orders comboOrder = new Orders();
                        comboOrder.menu = "套餐" + combo.id.ToString() + "优惠";
                        comboOrder.text = m_Seat.text;
                        comboOrder.systemId = m_Seat.systemId;
                        comboOrder.number = 1;
                        comboOrder.inputTime = GeneralClass.Now;
                        comboOrder.inputEmployee = LogIn.m_User.name;
                        comboOrder.paid = false;
                        comboOrder.comboId = combo.id;
                        comboOrder.money = Convert.ToDouble(combo.price) - combo.originPrice;

                        db.Orders.InsertOnSubmit(comboOrder);
                        m_Orders.Add(comboOrder);
                    }
                    db.SubmitChanges();
                }
            }
        }*/

        //左移
        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (m_index == db.Catgory.Count() - 1)
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
                GeneralClass.printErrorMsg("需要输入订单!");
                return;
            }

            int orderId = Convert.ToInt32(dgvExpense.CurrentRow.Cells[0].Value);
            var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            if (order == null || order.menu.Contains("套餐"))
            {
                GeneralClass.printErrorMsg("输入订单编号错误!");
                return;
            }

            if (m_Orders.Contains(order))
            {
                if (GeneralClass.printAskMsg("没入数据库，直接删除?") == DialogResult.Yes)
                {
                    db.Orders.DeleteOnSubmit(order);
                    db.SubmitChanges();
                    dgvExpense_show();
                }
                return;
            }

            InputEmployeeByPwd inputServerForm = new InputEmployeeByPwd();
            if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;
            order.deleteEmployee = inputServerForm.employee.id.ToString();

            Employee employee = db.Employee.FirstOrDefault(x => x.id.ToString() == order.technician);
            if (employee != null)
                employee.status = "空闲";

            db.SubmitChanges();

            find_combo();
            dgvExpense_show();

            m_Orders.Remove(order);
        }

        //清屏
        private void btnClear_Click(object sender, EventArgs e)
        {
            db.Orders.DeleteAllOnSubmit(m_Orders.ToArray());
            db.SubmitChanges();

            m_Orders.Clear();

            find_combo();
            dgvExpense_show();
        }

        //设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            OrderSettingForm orderSettingForm = new OrderSettingForm(db, typeList);
            if (orderSettingForm.ShowDialog() == DialogResult.OK)
            {
                typeList = orderSettingForm.typeList;
                write_File();
                read_File();
                createMenuTypePanel();
            }
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
                GeneralClass.printErrorMsg("需要输入订单!");
                return;
            }

            int orderId = Convert.ToInt32(dgvExpense.CurrentRow.Cells[0].Value);
            var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            if (order == null)
            {
                GeneralClass.printErrorMsg("输入订单编号错误!");
                return;
            }

            InputServerId inputServerForm = new InputServerId("输入点单数量", true);
            if (inputServerForm.ShowDialog() != DialogResult.OK)
                return;

            double unit = order.money / order.number;
            order.number = Convert.ToInt32(inputServerForm.number);
            order.money = unit * order.number;
            db.SubmitChanges();
            dgvExpense_show();
        }

        //删除
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExpense.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("需要输入订单!");
                return;
            }

            int orderId = Convert.ToInt32(dgvExpense.CurrentRow.Cells[0].Value);
            var order = db.Orders.FirstOrDefault(x => x.id == orderId);
            if (order == null)
            {
                GeneralClass.printErrorMsg("输入订单编号错误!");
                return;
            }

            if (!m_Orders.Contains(order))
            {
                GeneralClass.printErrorMsg("已点订单,不能删除!");
                return;
            }

            //InputServerId inputServerForm = new InputServerId(db, "", false);
            //if (inputServerForm.ShowDialog() != DialogResult.OK)
            //    return;
            //order.deleteEmployee = inputServerForm.emplyee.id.ToString();

            //Employee employee = db.Employee.FirstOrDefault(x => x.id.ToString() == order.technician);
            //if (employee != null)
                //employee.status = "空闲";

            db.Orders.DeleteOnSubmit(order);
            db.SubmitChanges();

            m_Orders.Remove(order);
            find_combo();
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
        private void write_File()
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
        }

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
                    btnSave_Click(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}
