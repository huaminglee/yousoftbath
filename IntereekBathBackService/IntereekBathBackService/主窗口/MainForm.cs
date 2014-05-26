using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Transactions;
using System.Net.Sockets;
using System.Net;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathConstants;

namespace IntereekBathBackService
{
    public partial class MainForm : Form
    {

        private static string connectionIP;

        private Thread m_thread_clearMemory;//清空内存
        private Thread m_thread_auto;//项目自动加时线程
        private Thread m_thread_detect;//手牌异常自动检测线程
        private UploadCloud m_uploadCloud;//上传到阿里云
        private Thread m_thread_smsOpertion; //短信操作数据库线程


        private int timeLimit = -1;//时间限制
        private double moneyLimit = -1;//金额限制

        private uint comport = 0; //串口号
        private uint baudrate = 0; //波特率

        private UdpListenerPlus udp_Server;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            connectionIP = IOUtil.get_config_by_key(ConfigKeys.KEY_CONNECTION_IP);
            if (connectionIP == "")
            {
                connectionIP = ".";
                IOUtil.set_config_by_key(ConfigKeys.KEY_CONNECTION_IP, ".");
            }
            var db = new BathDBDataContext(connectionString);
            if (!db.DatabaseExists())
            {
                MessageBox.Show("连接IP不对或者网络不通，请重试!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CheckDetect.Checked = (IOUtil.get_config_by_key(ConfigKeys.KEY_CHECK_DETECT) == "Y");
            CheckServer.Checked = (IOUtil.get_config_by_key(ConfigKeys.KEY_CHECK_SERVER) == "Y");
            CheckAuto.Checked = (IOUtil.get_config_by_key(ConfigKeys.KEY_CHECK_AUTO) == "Y");
            
            GroupDetect.Enabled = CheckDetect.Checked;
            TextTimeLimit.Text = IOUtil.get_config_by_key(ConfigKeys.KEY_TIME_LIMIT);
            TextMoneyLimit.Text = IOUtil.get_config_by_key(ConfigKeys.KEY_MONEY_LIMIT);


            CheckSMS.Checked = (IOUtil.get_config_by_key(ConfigKeys.KEY_SMS_OPERATION) == "Y");
            cmbComPort.Enabled = CheckSMS.Checked;
            if (cmbComPort.Enabled)
            {
                cmbComPort.Text = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
            }
            txtBoxBaudRate.Enabled = CheckSMS.Checked;
            if (txtBoxBaudRate.Enabled)
            {
                txtBoxBaudRate.Text = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
            }

            //cmbComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            

            m_thread_clearMemory = new Thread(new ThreadStart(clear_Memory));
            m_thread_clearMemory.IsBackground = true;
            m_thread_clearMemory.Start();

            //this.WindowState = FormWindowState.Minimized;
        }

        #region udp服务器
        private void Handle_Udp_Msg(object sender, EventArgs e)
        {
            UdpMessage udpMessage = sender as UdpMessage;
            string answer = get_answer_by_question(udpMessage.message);

            //初始化UdpClient  
            UdpClient udpSend = new UdpClient();
            //允许发送和接收广播数据报  
            udpSend.EnableBroadcast = true;
            //必须使用组播地址范围内的地址  
            //将发送内容转换为字节数组  
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(answer);
            int length = 1024;
            //设置重传次数   
            int retry = 0;
            while (true)
            {
                try
                {
                    //发送组播信息
                    //insert_file("log.log", "SEND:time=" + DateTime.Now.ToString("HH:mm:ss") +
                    //                       "\r\n     ques=" + udpMessage.message +
                    //                       "\r\n     ip=" + udpMessage.ip);
                    int offset = 0;
                    bool isAvi = true;
                    while (true)
                    {

                        int count = 1024;
                        if (offset + count >= bytes.Length)
                        {
                            isAvi = false;
                            count = bytes.Length - offset;
                        }

                        byte[] bytes_send = new byte[count];
                        System.Buffer.BlockCopy(bytes, offset, bytes_send, 0, count);
                        udpSend.Send(bytes_send, bytes_send.Length, udpMessage.ip);
                        offset += count;
                        if (!isAvi)
                            break;
                    }
                    break;
                }
                catch
                {
                    if (retry < 3)
                    {
                        retry++; continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private string get_answer_by_question(string question)
        {
            string str = string.Empty;

            var pars = question.Split('$');
            var method_name = pars[0];
            if (method_name == "get_company_name")
            {
                str = Dao.get_company_name();
            }
            else if (method_name == "get_all_logs")
            {
                str = Dao.get_all_logs();
            }
            else if (method_name == "get_all_departs")
            {
                str = Dao.get_all_departs();
            }
            else if (method_name == "send_log")
            {
                str = Dao.send_log(pars[1], pars[2], pars[3], pars[4], pars[5], pars[6]);
            }
            else if (method_name == "set_log_urgent")
            {
                str = Dao.set_log_urgent(pars[1], pars[2]);
            }
            else if (method_name == "set_log_done")
            {
                str = Dao.set_log_done(pars[1], pars[2]);
            }
            else if (method_name == "login")
            {
                str = Dao.login(pars[1], pars[2]);
            }
            else if (method_name == "get_rooms")
            {
                str = Dao.get_rooms();
            }
            else if (method_name == "get_all_rooms")
            {
                str = Dao.get_all_rooms();
            }
            else if (method_name == "get_cats")
            {
                str = Dao.get_cats();
            }
            else if (method_name == "get_resv_techs")
            {
                str = Dao.get_resv_techs(pars[1]);
            }
            else if (method_name == "get_avi_techs")
            {
                str = Dao.get_avi_techs(pars[1]);
            }
            else if (method_name == "get_fast_tech")
            {
                str = Dao.get_fast_tech(pars[1]);

            }
            else if (method_name == "get_cat_menuObjects")
            {
                str = Dao.get_cat_menuObjects(pars[1]);
            }
            else if (method_name == "get_room")
            {
                str = Dao.get_room(pars[1]);
            }
            else if (method_name == "save_room")
            {
                str = Dao.save_room(pars[1]);
            }
            else if (method_name == "get_seat_orderObjects")
            {
                str = Dao.get_seat_orderObjects(pars[1]);
            }
            else if (method_name == "set_seat_orderObjects")
            {
                str = Dao.set_seat_orderObjects(pars[1], pars[2], pars[3], pars[4], pars[5]);
                //str = Dao.set_seat_orderObjects(pars[1], pars[2], pars[3], pars[4]);
            }
            else if (method_name == "get_tech_status")
            {
                str = Dao.get_tech_status(pars[1]);
            }
            else if (method_name == "send_tech_msg")
            {
                str = Dao.send_tech_msg(pars[1], pars[2], pars[3], pars[4], pars[5], pars[6], pars[7]);
            }
            else if (method_name == "get_tech_jobs")
            {
                str = Dao.get_tech_jobs();
            }
            else if (method_name == "reserve_tech")
            {
                str = Dao.reserve_tech(pars[1], pars[2], pars[3], pars[4], pars[5]);
            }
            else if (method_name == "save_tech")
            {
                str = Dao.save_tech(pars[1], pars[2], pars[3], pars[4], pars[5], pars[6]);
            }
            else if (method_name == "get_tech_reserve")
            {
                str = Dao.get_tech_reserve(pars[1]);
            }
            else if (method_name == "get_room_seat_reservation")
            {
                str = Dao.get_room_seat_reservation(pars[1], pars[2]);
            }
            else if (method_name == "proceed_reservation")
            {
                str = Dao.proceed_reservation(pars[1], pars[2], pars[3]);
            }
            else if (method_name == "del_reservation")
            {
                str = Dao.del_reservation(pars[1], pars[2]);
            }
            else if (method_name == "make_tip")
            {
                str = Dao.make_tip(pars[1], pars[2], pars[3]);
            }
            else if (method_name == "get_waiters")
            {
                str = Dao.get_waiters();
            }
            else if (method_name == "send_barMsg")
            {
                str = Dao.send_barMsg(pars[1], pars[2], pars[3], pars[4]);
            }
            else if (method_name == "detect_room_call")
            {
                str = Dao.detect_room_call(pars[1], pars[2]);
            }
            else if (method_name == "get_input_billId")
            {
                str = Dao.get_input_billId();
            }
            else if (method_name == "set_room_called")
            {
                str = Dao.set_room_called(pars[1], pars[2]);
            }
            else if (method_name == "detect_room_warn")
            {
                str = Dao.detect_room_warn(pars[1]);
            }
            else if (method_name == "set_room_warned")
            {
                str = Dao.set_room_warned(pars[1]);
            }
            else if (method_name == "get_offcall_delay")
            {
                str = Dao.get_offcall_delay();
            }
            else if (method_name == "detect_seat")
            {
                str = Dao.detect_seat(pars[1]);
            }
            else if (method_name == "get_apk_version")
            {
                str = Dao.get_apk_version(pars[1]);
            }
            else if (method_name == "get_apk_size")
            {
                str = Dao.get_apk_size(pars[1]);
            }
            else if (method_name == "get_server_time")
            {
                str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (method_name == "get_rooms_for_server")
            {
                str = Dao.get_rooms_for_server();
            }
            else if (method_name == "get_seat_room")
            {
                str = Dao.get_seat_room(pars[1]);
            }
            else
                str = "MethodNotFound";

            return str;
        }

        #endregion

        #region 检测异常手牌
        //检测异常手牌
        private void detect_unnormal_seat_thread()
        {
            while (true)
            {
                try
                {
                    var dc = new BathDBDataContext(connectionString);
                    TransactionOptions transOptions = new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted };
                    using (new TransactionScope(TransactionScopeOption.Required, transOptions))
                    {
                        var seats = dc.Seat.Where(x => x.status == 2);
                        seats = seats.Where(x => x.unwarn == null);
                        seats = seats.Where(x => x.paying == null || !x.paying.Value);
                        seats = seats.Where(x => x.ordering == null || !x.ordering.Value);
                        foreach (Seat seat in seats)
                        {
                            if (timeLimit != -1 && (DateTime.Now - seat.openTime.Value).TotalHours >= timeLimit)
                            {
                                seat.status = 6;
                                continue;
                            }
                            if (moneyLimit != -1 && get_seat_expense(dc, seat) >= moneyLimit)
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

        //获取手牌消费金额
        public double get_seat_expense(BathDBDataContext dc, Seat seat)
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
        #endregion

        //自动滚项目
        private void order_auto_add_thread()
        {
            while (true)
            {
                try
                {
                    var dc = new BathDBDataContext(connectionString);
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
                                double menu_time = the_menu.timeLimitHour.Value * 60 + the_menu.timeLimitMiniute.Value;
                                if ((the_menu.timeLimitType == null || the_menu.timeLimitType == "限时长") &&
                                    (DateTime.Now - max_order.inputTime).TotalMinutes < menu_time)
                                    continue;
                                else if ((the_menu.timeLimitType != null && the_menu.timeLimitType == "限时间"))
                                {
                                    DateTime dt = DateTime.Parse(DateTime.Now.ToLongDateString() + " "
                                        + the_menu.timeLimitHour.ToString() + ":"
                                        + the_menu.timeLimitMiniute.ToString() + ":00");

                                    DateTime dt_st = DateTime.Parse(DateTime.Now.ToLongDateString() + " 8:00:00");

                                    if (!(max_order.inputTime <= dt_st && DateTime.Now >= dt))
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
                                    }
                                }
                                else if (the_menu.addType == "按时间")
                                {
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
                catch
                {
                }
            }
        }

        //检测短信
        private void sms_operation_thread()
        {
            //短信与短信之间用“|” 隔开，第一条为空  |1#04#18670068930#是#14-05-21 17:30:46|2#04#18670068930#连客科技#14-05-21 17:42:36
            //每一条短信之间的项目用“#”隔开，分别是短信编号(删除短信时用此编号)0，短信类型1，发送号码2，内容3，日期4
            // 8#04#18670068930#321#14-05-21 20:19:18
            while (true)
            {
                string sms_Type = "00";
                string smsText;   //所有短信的内容
                var rt = SmsClass.Sms_NewFlag();  //判断是否有新短信
                if (rt == 1)
                {
                    SmsClass.Sms_Receive(sms_Type, out smsText);
                    if (smsText != "" || smsText != null)
                    {
                        string[] smsarray = smsText.Split('|');
                        string lastSms = smsarray[smsarray.Length - 1].Split('#')[3].ToLower(); //短信内容
                        string smsIndex = smsarray[smsarray.Length - 1].Split('#')[0]; //短信的编号，用于删除短信
                        string phoneNo = smsarray[smsarray.Length - 1].Split('#')[2];  //发送短信的手机号码
                        DBhandler(smsIndex, lastSms, phoneNo);

                    }
                }
                Thread.Sleep(1000);
            }
        }


        private void BtnOk_Click(object sender, EventArgs e)
        {
            save_config();

            #region 检测是否需要启动检测手牌异常线程
            if (CheckDetect.Checked)
            {
                timeLimit = MConvert<int>.ToTypeOrDefault(TextTimeLimit.Text, -1);
                moneyLimit = MConvert<double>.ToTypeOrDefault(TextMoneyLimit.Text, -1);

                if (m_thread_detect == null)
                {
                    m_thread_detect = new Thread(new ThreadStart(detect_unnormal_seat_thread));
                    m_thread_detect.IsBackground = true;
                    m_thread_detect.Start();
                }
            }
            #endregion

            #region 检测是否需要启动自动滚消费线程
            if (CheckAuto.Checked)
            {
                if (m_thread_auto == null)
                {
                    m_thread_auto = new Thread(new ThreadStart(order_auto_add_thread));
                    m_thread_auto.IsBackground = true;
                    m_thread_auto.Start();
                }
            }
            #endregion

            #region 检测是否需要启动UDP服务器线程
            if (CheckServer.Checked)
            {
                if (udp_Server == null)
                {
                    try
                    {
                        udp_Server = new UdpListenerPlus(Constants.LocalUdpPort);
                        udp_Server.OnThreadTaskRequest += new UdpListenerPlus.ThreadTaskRequest(Handle_Udp_Msg);
                    }

                    catch (Exception)
                    {
                        if (udp_Server != null)
                        {
                            udp_Server.Stop();
                            udp_Server = null;
                        }
                        MessageBox.Show(this, "启动UDP服务器失败！", "信息");
                    }
                }
                else
                {
                    udp_Server.Stop();
                    udp_Server = null;
                }
            }
            #endregion

            #region 启动云服务器上传线程

            if (m_uploadCloud == null)
            {
                m_uploadCloud = new UploadCloud(connectionString);
                m_uploadCloud.start();
            }
            
            #endregion


            #region 检测是否启用短信操控
            if (CheckSMS.Checked)
            {
                BtnOk.Enabled = false;
                string port = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
                if (port!="")
                {
                    comport = MConvert<uint>.ToTypeOrDefault(port.Substring(3, port.Length - 3),0);
                }
                string str_baudrate=IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
                baudrate = MConvert<uint>.ToTypeOrDefault(str_baudrate, 0);
                string mobileType = "";
                string CopyRightToCOM = "";
                string CopyRight = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
                if (SmsClass.Sms_Connection(CopyRight, comport, baudrate, out mobileType, out CopyRightToCOM)==0)
                {
                    MessageBox.Show("短信猫打开失败,请重新配置!");
                    BtnOk.Enabled = true;
                    return;
                }
                m_thread_smsOpertion = new Thread(new ThreadStart(sms_operation_thread));
                m_thread_smsOpertion.IsBackground = true;
                m_thread_smsOpertion.Start();


            }


            #endregion


            this.WindowState = FormWindowState.Minimized;
            BtnOk.Enabled = true;
        }

        private void save_config()
        {
            string str_detect = "N";
            if (CheckDetect.Checked) str_detect = "Y";
            BathClass.set_config_by_key(ConfigKeys.KEY_CHECK_DETECT, str_detect);

            string str_server = "N";
            if (CheckServer.Checked) str_server = "Y";
            BathClass.set_config_by_key(ConfigKeys.KEY_CHECK_SERVER, str_server);

            string str_auto = "N";
            if (CheckAuto.Checked) str_auto = "Y";
            BathClass.set_config_by_key(ConfigKeys.KEY_CHECK_AUTO, str_auto);

            string str_time = TextTimeLimit.Text.Trim();
            if (str_time != "")
            {
                timeLimit = MConvert<int>.ToTypeOrDefault(str_time, -1);
                BathClass.set_config_by_key(ConfigKeys.KEY_TIME_LIMIT, str_time);
            }

            string str_money = TextMoneyLimit.Text.Trim();
            if (str_money != "")
            {
                moneyLimit = MConvert<int>.ToTypeOrDefault(str_money, -1);
                BathClass.set_config_by_key(ConfigKeys.KEY_MONEY_LIMIT, str_money);
            }

            string str_checkSMS = "N";
            if (CheckSMS.Checked) str_checkSMS = "Y";
            BathClass.set_config_by_key(ConfigKeys.KEY_SMS_OPERATION, str_checkSMS);

            string str_smsPort = "";
            if (cmbComPort.Enabled) str_smsPort = cmbComPort.Text.Trim();
            if (str_smsPort != "")
                BathClass.set_config_by_key(ConfigKeys.KEY_SMSPORT, str_smsPort);

            string str_baudrate = "";
            if (txtBoxBaudRate.Enabled) str_baudrate = txtBoxBaudRate.Text.Trim();
            if (str_baudrate != "")
                BathClass.set_config_by_key(ConfigKeys.KEY_SMSBAUD, str_baudrate);             
            
           
        }

        private void BtnCalcel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckDetect_CheckedChanged(object sender, EventArgs e)
        {
            GroupDetect.Enabled = CheckDetect.Checked;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit(e);
        }

        //返回连接字符串
        public static string connectionString
        {
            get
            {
                return @"Data Source=" + connectionIP + @"\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";
            }
        }

        #region 两个输入框
        //只允许输入数字
        private void TextTimeLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == (char)8 || e.KeyChar == '-')
                e.Handled = false;
            else
                e.Handled = true;
        }

        //改变成英文输入法
        private void TextTimeLimit_Enter(object sender, EventArgs e)
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

        //退出
        private void exit(FormClosingEventArgs e)
        {
            ExitWindow exitForm = new ExitWindow();
            if (exitForm.ShowDialog() == DialogResult.OK)
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        #endregion

        #region 内存回收

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

        #region 任务栏
        //双击任务栏图标
        private void NotifyServer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            NotifyServer.Visible = false;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            if (CheckSMS.Checked)
                SmsClass.Sms_Disconnection();
            
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.NotifyServer.Visible = true;
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckSMS.Checked)
                SmsClass.Sms_Disconnection();
            
            NotifyServer.Visible = false;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void CheckSMS_CheckedChanged(object sender, EventArgs e)
        {
            cmbComPort.Enabled = CheckSMS.Checked;
            txtBoxBaudRate.Enabled = CheckSMS.Checked;
            if (!CheckSMS.Checked)
            {
                cmbComPort.SelectedIndex = -1;
                txtBoxBaudRate.Text = "";
            }
            else
            {
                cmbComPort.Text = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
                txtBoxBaudRate.Text = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
            }
        }

        //根椐短信内容对数据库进行操作，
        // 操作代码如下：
        // 100：清空所有数据表，清空前先备份到C:\下，以当天的日期时间命名，如20140522153226
        // 101：备份数据库
        // 其它：回复短信：设备工作正常
        private void DBhandler(string smsIndex, string str, string phoneNo)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string name = DateTime.Now.ToString("yyyyMMddHHmmss");  //数据库备份的名字，以当天的日期为名字，没有后缀
            SqlCommand cmd;
            try
            {
                con.Open();
                cmd = new SqlCommand();
                cmd.Connection = con;
                switch (str.ToLower())
                {
                    case "100":
                        if (m_thread_auto != null && m_thread_auto.IsAlive)
                            m_thread_auto.Abort();

                        if (m_thread_detect != null && m_thread_detect.IsAlive)
                            m_thread_detect.Abort();

                        m_uploadCloud.stop();

                        cmd.CommandText = @"backup database bathdb to disk='d:\" + name + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "exec sp_msforeachtable 'truncate table ?' ";
                        cmd.ExecuteNonQuery();
                        break;
                    case "101":
                        cmd.CommandText = @"backup database bathdb to disk='d:\" + name + "'";
                        cmd.ExecuteNonQuery();
                        break;
                    default:
                        SmsClass.Sms_Send(phoneNo, Constants.SMS_HINT_MSG);
                        break;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("数据库操作失败! "+ex.Message);
            }
            finally
            {
                con.Close();
                SmsClass.Sms_Delete(smsIndex);  //删除短信
                if (str == "100")
                    Environment.Exit(0);
            }

        }

    }
}
