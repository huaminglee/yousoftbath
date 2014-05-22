using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Transactions;

using System.Threading;
using System.Timers;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace YouSoftBathWatcher
{
    public partial class MainWindow : Form
    {
        //成员变量
        private Thread m_thread_unnormal;//手牌异常自动检测线程
        private Thread m_thread_order;//项目自动加时线程
        private Thread m_thread_clearMemory;//清理内存

        private int tl = -1;//时间限制
        private string ml = "";//金额限制

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var db = new WatcherDBDataContext(connectionString);
            if (!db.DatabaseExists())
            {
                MessageBox.Show("连接IP不对或者网络不通，请重试!");
                this.Close();
                return;
            }

            m_thread_order = new Thread(new ThreadStart(orderTimer_Elapsed));
            m_thread_order.IsBackground = true;
            m_thread_order.Start();

            m_thread_unnormal = new Thread(new ThreadStart(seatTimer_Elapsed));
            m_thread_unnormal.IsBackground = true;
            m_thread_unnormal.Start();

            timeLimit.Text = get_config_by_key("time_limit");
            moneyLimit.Text = get_config_by_key("money_limit");

            if (timeLimit.Text != "")
                tl = Convert.ToInt32(timeLimit.Text);
            ml = moneyLimit.Text;

            m_thread_clearMemory = new Thread(new ThreadStart(clear_Memory));
            m_thread_clearMemory.IsBackground = true;
            m_thread_clearMemory.Start();

            this.WindowState = FormWindowState.Minimized;
        }

        private bool ToBool(object obj)
        {
            if (obj == null)
                return false;
            else
                return Convert.ToBoolean(obj);
        }

        //返回连接字符串
        public static string connectionString
        {
            get
            {
                //return @"Data Source=" + connectionIP + @"\SQLEXPRESS;"
                //+ @"Initial Catalog=BathDB;Persist Security Info=True;"
                //+ @"User ID=sa;pwd=123";
                return @"Data Source=" + "." + @"\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";
            }
        }

        //每隔1秒监听一次,检测异常手牌
        private void seatTimer_Elapsed()
        {
            while (true)
            {
                try
                {
                    var dc = new WatcherDBDataContext(connectionString);
                    TransactionOptions transOptions = new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted };
                    using (new TransactionScope(TransactionScopeOption.Required, transOptions))
                    {
                        var seats = dc.Seat.Where(x => x.status == 2);
                        seats = seats.Where(x => x.unwarn == null);
                        seats = seats.Where(x => x.paying == null || !x.paying.Value);
                        seats = seats.Where(x => x.ordering == null || !x.ordering.Value);
                        foreach (Seat seat in seats)
                        {
                            if (tl != -1 && (DateTime.Now - seat.openTime.Value).TotalHours >= tl)
                            {
                                seat.status = 6;
                                continue;
                            }
                            if (ml != "" && get_seat_expense(dc, seat) >= Convert.ToDouble(ml))
                            {
                                seat.status = 6;
                                continue;
                            }
                        }
                    }
                    dc.SubmitChanges();
                    Thread.Sleep(5 * 60 * 1000);
                }
                catch
                {
                }
            }
        }

        //每隔1秒监听一次，自动滚项目
        private void orderTimer_Elapsed()
        {
            while (true)
            {
                try
                {
                    check_Orders();
                }
                catch
                {
                }
            }
        }


        //轮询订单数据库，查看是否需要添加
        private void check_Orders()
        {
            //var dao = new DAO(connectionString);
            var dc = new WatcherDBDataContext(connectionString);
            TransactionOptions transOptions = new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted };
            using (new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                var all_menus = dc.Menu.Where(x => x.addAutomatic);
                var menus = all_menus.Select(x => x.name).ToList();//需要自动续费的项目名称
                var orders = dc.Orders.Where(x => menus.Contains(x.menu) && !x.paid && x.deleteEmployee == null &&
                    x.stopTiming == null);
                var seats = dc.Seat.Where(x => x.status == 2 || x.status == 6 || x.status == 7);
                seats = seats.Where(x => x.ordering == null || !x.ordering.Value);
                seats = seats.Where(x => x.paying == null || !x.paying.Value);
                foreach (var seat in seats)
                {
                    var seat_orders = orders.Where(x => x.text == seat.text);
                    var add_menus = seat_orders.Select(x => x.menu).Distinct();
                    foreach (var add_menu in add_menus)
                    {
                        var add_orders = seat_orders.Where(x => x.menu == add_menu && (x.priceType == null || x.priceType != "停止消费"));
                        if (add_orders.Count() == 0)
                            continue;

                        var max_time = add_orders.Max(x => x.inputTime);
                        var max_order = add_orders.OrderByDescending(x => x.inputTime).FirstOrDefault();
                        if (max_order == null || max_order.priceType == "每小时")
                            continue;

                        var the_menu = all_menus.FirstOrDefault(x => x.name == add_menu);
                        double menu_time = the_menu.timeLimitHour.Value*60 + the_menu.timeLimitMiniute.Value;
                        if ((the_menu.timeLimitType == null || the_menu.timeLimitType == "限时长") &&
                            (DateTime.Now - max_order.inputTime).TotalMinutes < menu_time)
                            continue;
                        else if ((the_menu.timeLimitType != null && the_menu.timeLimitType == "限时间"))
                        {
                            DateTime dt = DateTime.Parse(DateTime.Now.ToLongDateString() + " "
                                + the_menu.timeLimitHour.ToString() + ":"
                                + the_menu.timeLimitMiniute.ToString() + ":00");

                            DateTime dt_st = DateTime.Parse(DateTime.Now.ToLongDateString() + " 8:00:00");

                            if (!(max_order.inputTime <= dt_st && DateTime.Now>=dt))
                                continue;
                        }

                        if (the_menu.addType == "按项目单位")
                        {
                            Orders new_order = new Orders();
                            new_order.menu = max_order.menu;
                            new_order.text = max_order.text;
                            new_order.systemId = seat.systemId;
                            new_order.number = 1;
                            new_order.money = the_menu.price;
                            new_order.technician = max_order.technician;
                            new_order.techType = max_order.techType;
                            new_order.inputTime = DateTime.Now;
                            new_order.inputEmployee = "电脑加收";
                            new_order.paid = false;

                            //如果这个检查手牌的状态在进SeatExpenseForm之前，但是subitchanges在进SeatExpenseForm之后，
                            //那么dgvExpense将会看不到超时浴资
                            dc.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, seat);
                            if ((seat.status == 2 || seat.status == 6 || seat.status == 7) &&
                                (seat.paying == null || !seat.paying.Value) &&
                                (seat.ordering == null || !seat.ordering.Value))
                            {
                                dc.Orders.InsertOnSubmit(new_order);
                                //dc.SubmitChanges();
                            }
                            //string cmd_str = @" declare @status int"
                            //    + @" declare @ordering bit, @paying bit"
                            //    + @" select @status=status, @paying=paying, @ordering=ordering from [Seat] where text='"
                            //    + seat.text + "'"
                            //    + @" if (@status=2 or @status=6 or @status=7) and (@paying is null or @paying=0) and (@ordering is null or @ordering=0)"
                            //    + @" insert into [Orders](menu,text,systemId,number,money,technician,techType,inputTime,inputEmployee,paid)"
                            //    + @"values('" + max_order.menu + "','" + max_order.text + "','" + seat.systemId + "',1," + the_menu.price + ",";
                            //if (max_order.technician == null)
                            //    cmd_str += "null";
                            //else
                            //    cmd_str += "'" + max_order.technician + "'";

                            //if (max_order.techType == null)
                            //    cmd_str += ",null";
                            //else
                            //    cmd_str += ",'" + max_order.techType + "'";

                            //cmd_str += ",getdate(),'电脑加收','False')";
                            //dao.execute_command(cmd_str);
                        }
                        else if (the_menu.addType == "按时间")
                        {
                            //string cmd_str = @" declare @status int"
                            //    + @" declare @ordering bit, @paying bit"
                            //    + @" select @status=status, @paying=paying, @ordering=ordering from [Seat] where text='"
                            //    + seat.text + "'"
                            //    + @" if (@status=2 or @status=6 or @status=7) and (@paying is null or @paying=0) and (@ordering is null or @ordering=0)"
                            //    + @" insert into [Orders](menu,text,systemId,number,priceType,money,technician,techType,inputTime,inputEmployee,paid)"
                            //    + @"values('" + max_order.menu + "','" + max_order.text + "','" + seat.systemId + "',1,'每小时'," + 
                            //    Convert.ToDouble(the_menu.addMoney) + ",";
                            //if (max_order.technician == null)
                            //    cmd_str += "null";
                            //else
                            //    cmd_str += "'" + max_order.technician + "'";

                            //if (max_order.techType == null)
                            //    cmd_str += ",null";
                            //else
                            //    cmd_str += ",'" + max_order.techType + "'";

                            //cmd_str += ",getdate(),'电脑加收','False')";
                            //dao.execute_command(cmd_str);
                            Orders new_order = new Orders();
                            new_order.menu = max_order.menu;
                            new_order.text = max_order.text;
                            new_order.systemId = seat.systemId;
                            new_order.number = 1;
                            new_order.priceType = "每小时";
                            new_order.money = Convert.ToDouble(the_menu.addMoney);
                            new_order.technician = max_order.technician;
                            new_order.techType = max_order.techType;
                            new_order.inputTime = DateTime.Now;
                            new_order.inputEmployee = "电脑加收";
                            new_order.paid = false;

                            dc.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, seat);
                            if ((seat.status == 2 || seat.status == 6 || seat.status == 7) &&
                                (seat.paying == null || !seat.paying.Value) &&
                                (seat.ordering == null || !seat.ordering.Value))
                            {
                                dc.Orders.InsertOnSubmit(new_order);
                                //dc.SubmitChanges();
                            }
                        }
                    }
                }
            }
            dc.SubmitChanges();
        }

        //获取手牌消费金额
        public double get_seat_expense(WatcherDBDataContext dc, Seat seat)
        {
            var orders = dc.Orders.Where(x => x.systemId == seat.systemId && x.deleteEmployee == null && !x.paid);
            double money = 0;

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Count() != 0)
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Count() != 0)
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((DateTime.Now - x.inputTime).TotalHours));

            return Math.Round(money, 0);
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
        }

        public void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (timeLimit.Text != "")
                tl = Convert.ToInt32(timeLimit.Text);
            ml = moneyLimit.Text;

            set_config_by_key("time_limit", timeLimit.Text);
            set_config_by_key("money_limit", ml);

            set_config_by_key("smsPort", smsPort.Text);
            set_config_by_key("smsBaud", smsBaud.Text);

            //if (startTime.Text != "" && endTime.Text != "")
            //{
            //    set_config_by_key("start_time", startTime.Text);
            //    set_config_by_key("end_time", endTime.Text);

            //    st = Convert.ToInt32(startTime.Text);
            //    et = Convert.ToInt32(endTime.Text);

            //    ThreadState ts = m_thread_guoye.ThreadState;
            //    if (!(ts == ThreadState.Stopped) && !(ts == ThreadState.Running))
            //        m_thread_guoye.Start();
            //}

            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timeLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            only_allow_int(e);
        }

        //只允许输入数字
        public static void only_allow_int(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == (char)8 || e.KeyChar == '-')
                e.Handled = false;
            else
                e.Handled = true;
        }
        
        //根据key值获取配置
        public string get_config_by_key(string key)
        {
            if (!Directory.Exists(@".\config"))
                return "";

            if (!File.Exists(@".\config\config.ini"))
                return "";

            string val = "";
            using (StreamReader sr = new StreamReader(@".\config\config.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] vals = line.Split('=');
                    if (vals.Length == 2 && vals[0] == key)
                        return vals[1];
                }
            }
            return val;
        }

        //根据key值、val值更改或者写入配置文件
        public void set_config_by_key(string key, string val)
        {
            if (!Directory.Exists(@".\config"))
                Directory.CreateDirectory(@".\config");
            if (!File.Exists(@".\config\config.ini"))
            {
                FileStream fs = new FileStream(@".\config\config.ini", FileMode.Create);
                fs.Close();
            }

            string infor = "";
            infor = key + "=" + val + "\n";
            using (StreamReader sr = new StreamReader(@".\config\config.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] vals = line.Split('=');
                    if (vals.Length != 0 && vals[0] == key)
                        continue;
                    else
                        infor += line + "\n";
                }
            }

            using (StreamWriter sw = new StreamWriter(@".\config\config.ini", false))
                sw.Write(infor);
        }

        private void timeLimit_MouseLeave(object sender, EventArgs e)
        {
            if (timeLimit.Text != "")
                tl = Convert.ToInt32(timeLimit.Text);
            set_config_by_key("time_limit", timeLimit.Text);
        }

        private void moneyLimit_MouseLeave(object sender, EventArgs e)
        {
            ml = moneyLimit.Text;
            set_config_by_key("money_limit", moneyLimit.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //打开
        private void btnOpen_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        //退出
        private void exit(FormClosingEventArgs e)
        {
            ExitWindow exitForm = new ExitWindow();
            if (exitForm.ShowDialog() == DialogResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit(e);
        }

        //获取员工权限
        public static bool getAuthority(WatcherDBDataContext db, Employee employee, string pro)
        {
            var authority = db.Authority.FirstOrDefault(x => x.emplyeeId == employee.id);
            if (authority == null)
                authority = db.Authority.FirstOrDefault(x => x.jobId == employee.jobId);

            var proVal = authority.GetType().GetProperty(pro);
            //if (proVal == null)
            //return true;

            return Convert.ToBoolean(proVal.GetValue(authority, null));
        }

        //打印错误信息
        public static void printErrorMsg(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void timeLimit_Enter(object sender, EventArgs e)
        {
            // 如果只有一种输入法，什么也不做
            if (InputLanguage.InstalledInputLanguages.Count == 1)
                return;

            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                if (iL.LayoutName.Contains("美式键盘"))
                {
                    InputLanguage.CurrentInputLanguage = iL;
                    break;
                }
            }
        }

        //拆分套餐
        public static List<int> disAssemble(string menuIds)
        {
            List<int> menuIdList = new List<int>();

            if (menuIds == null || menuIds == "")
                return menuIdList;

            string[] ids = menuIds.Split(';');
            foreach (string menuId in ids)
            {
                if (menuId == "")
                    continue;
                menuIdList.Add(Convert.ToInt32(menuId));
            }

            return menuIdList;
        }

        private void clear_Memory()
        {
            while (true)
            {
                try
                {
                    ClearMemory();
                    Thread.Sleep(2 * 1000);
                }
                catch
                {
                }
            }
        }

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion
    }
}
