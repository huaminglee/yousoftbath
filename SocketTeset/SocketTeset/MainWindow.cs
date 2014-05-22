using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using SocketTeset;

namespace YouSoftBathWatcher
{
    public partial class MainWindow : Form
    {
        //成员变量
        private const int MAXTRIES = 100;
        private const int port = 7628;//服务器监听的端口号 
        private const string ip = "192.168.200.1";
        private TcpListenerPlus Server;
        private UdpListenerPlus udp_Server;
        private Thread m_thread_clearMemory;

        //创建一个Thread类  
        //private Thread thread1;
        //创建一个UdpClient对象，来接收消息  
        //private UdpClient udpReceive;
        //private UdpClient udpSend;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            /*if (Server == null)
            {
                try
                {
                    Server = new TcpListenerPlus(IPAddress.Parse(ip), port);
                    Server.OnThreadTaskRequest += new TcpListenerPlus.ThreadTaskRequest(GetAnswer);
                }

                catch (Exception)
                {
                    if (Server != null)
                    {
                        Server.Stop();
                        Server = null;
                    }
                    MessageBox.Show(this, "启动服务器失败！", "信息");
                }
            }
            else
            {
                Server.Stop();
                Server = null;
            } */

            if (udp_Server == null)
            {
                try
                {
                    udp_Server = new UdpListenerPlus(port);
                    udp_Server.OnThreadTaskRequest += new UdpListenerPlus.ThreadTaskRequest(Handle_Udp_Msg);
                }

                catch (Exception)
                {
                    if (udp_Server != null)
                    {
                        udp_Server.Stop();
                        udp_Server = null;
                    }
                    MessageBox.Show(this, "启动服务器失败！", "信息");
                }
            }
            else
            {
                udp_Server.Stop();
                udp_Server = null;
            }


            m_thread_clearMemory = new Thread(new ThreadStart(clear_Memory));
            m_thread_clearMemory.IsBackground = true;
            m_thread_clearMemory.Start();

            this.WindowState = FormWindowState.Minimized;
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
                    while(true){

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
            else if (method_name=="login")
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

        private void GetAnswer(object sender, EventArgs e)
        {
            TcpClient tcpClient = (TcpClient)sender;
            using (NetworkStream Stream = tcpClient.GetStream())
            {   // 调整接收缓冲区大小  
                try
                {
                    byte[] ack = new byte[50];
                    byte[] tempData = new byte[30000];  //读取客户端消息(商品编码)
                    string receiveData = string.Empty;
                    int bytes = Stream.Read(tempData, 0, tempData.Length);
                    receiveData = Encoding.UTF8.GetString(tempData, 2, bytes).TrimEnd('\0');
                    var pars = receiveData.Split('$');
                    string method = pars.FirstOrDefault();
                    //bool condition = (method != "detect_room_warn" &&
                    //    method != "detect_room_call" &&
                    //    method != "get_cats" &&
                    //    method != "get_cat_menuObjects" &&
                    //    method != "get_apk_version");
                    //if (condition)
                    //{
                    //    insert_file("log.log", "-------get message at---" + method + "-----" + pars.LastOrDefault());
                    //}
                    

                    try
                    {
                        string answer = get_answer_by_question(receiveData);
                        byte[] byteTime = Encoding.UTF8.GetBytes(answer);//向客户端发送消息 商品名称

                        int tried = 0;
                        do 
                        {
                            tried++;
                            //if(condition)
                            //    insert_file("发送消息---" + method + "--"+tried.ToString() +"次"+ pars.LastOrDefault());
                            Stream.Write(byteTime, 0, byteTime.Length);
                            Stream.Flush();
                            bytes = Stream.Read(ack, 0, ack.Length);//读取客户端的确认信息
                            receiveData = Encoding.UTF8.GetString(ack, 2, bytes);
                            if (receiveData.TrimEnd('\0').Contains("ack"))
                                break;
                        } while (tried <= MAXTRIES);

                        //if (condition)
                        //    insert_file("发送消息成功-------" +method +"----"+ pars.LastOrDefault());

                    }
                    catch (System.Exception ex)
                    {
                        //insert_file("error" + method + pars.LastOrDefault() + "=" + ex.Message);
                    }
                }
                catch (Exception ex) { }
            }
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
            {
                if (Server != null)
                {
                    Server.Stop();
                }
                if (udp_Server != null)
                {
                    udp_Server.Stop();
                }
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit(e);
        }

        //写入文件
        public static void insert_file(string fileName, string msg)
        {
            //if (File.Exists(@".\Log\error.log"))
            //File.Delete(@".\Log\error.log");

            if (!Directory.Exists(@".\Log"))
                Directory.CreateDirectory(@".\Log");
            if (!File.Exists(@".\Log\"+fileName))
            {
                FileStream fs = new FileStream(@".\Log\" + fileName, FileMode.Create);
                fs.Close();
            }

            using (StreamWriter sw = new StreamWriter(@".\Log\" + fileName, true))
                sw.Write(msg + "\r\n");
        }
    }
}
