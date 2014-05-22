using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace YouSoftBathWatcher
{
    class UdpListenerPlus
    {

        private Int32 port;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localaddr">本地IP地址</param>
        /// <param name="port">侦听端口</param>
        public UdpListenerPlus(Int32 port)
        {   // 启动独立的侦听线程
            this.port = port;
            Thread ListenThread = new Thread(new ThreadStart(ListenThreadAction));
            ListenThread.IsBackground = true;
            ListenThread.Start();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~UdpListenerPlus()
        {
            Stop();
        }

        /// <summary>  
        /// 委托声明  
        /// </summary>  
        /// <param name="sender">事件发送者</param>  
        /// <param name="e">事件参数</param>  
        public delegate void ThreadTaskRequest(object sender, EventArgs e);

        /// <summary>  
        /// 线程任务请求事件  
        /// </summary>  
        public event ThreadTaskRequest OnThreadTaskRequest;

        /// <summary>  
        /// 连接列表操作互斥量  
        /// </summary>  
        private Mutex _mutexClients;

        private UdpClient udpReceive = null;

        /// <summary>  
        /// 侦听连接线程  
        /// </summary>  
        private void ListenThreadAction()  
        {   // 启动侦听  
            //Start();  
  
            // 初始化连接列表和互斥量  
            //_tcpClients = new List<TcpClient>();  
            _mutexClients = new Mutex();

            IPEndPoint remoteIpEndIPoint = new IPEndPoint(IPAddress.Any, port);
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 0);
            // 接受连接  
            while (true)  
            {
                try  
                {
                    if(udpReceive == null)
                        udpReceive = new UdpClient(remoteIpEndIPoint);
                    byte[] receiveBytes = udpReceive.Receive(ref iep);
                    //udpReceive.Close();
                    //udpReceive = null;
                    string question = Encoding.UTF8.GetString(receiveBytes);
                    //MainWindow.insert_file("log.log", "GET:time=" + DateTime.Now.ToString("HH:mm:ss") + ",ques=" + question);
                    UdpMessage udpMessage = new UdpMessage(iep, question);

                    // 接受挂起的连接请求  
                    //tcpClient = AcceptTcpClient();  
  
                    // 将该连接通信加入线程池队列  
                    ThreadPool.QueueUserWorkItem(ThreadPoolCallback, udpMessage);  
  
                    // 连接加入列表  
                    _mutexClients.WaitOne();  
                    //_tcpClients.Add(tcpClient);  
                    _mutexClients.ReleaseMutex();  
                }  
  
                catch (SocketException se)  
                {   // 结束侦听线程
                    MainWindow.insert_file("error.log", "0time=" + DateTime.Now.ToString() + "=" + se.Message);
                    //break;  
                }  
  
                catch (Exception ee)  
                {   // 加入队列失败  
                    if (udpReceive != null)  
                    {
                        MainWindow.insert_file("error.log", "0time=" + DateTime.Now.ToString() + "=" + ee.Message);
                        udpReceive.Close();
                        udpReceive = null;
                    }  
                }  
            }  
        }  
  
        /// <summary>  
        /// 线程池回调方法  
        /// </summary>  
        /// <param name="state">回调方法要使用的信息对象</param>  
        private void ThreadPoolCallback(Object state)  
        {   // 如果无法进行转换，则 as 返回 null 而非引发异常  
            UdpMessage udpMessage = state as UdpMessage;  
            try  
            {   // 执行任务  
                if (OnThreadTaskRequest != null)  
                {
                    OnThreadTaskRequest(udpMessage, EventArgs.Empty);  
                }  
            }  
  
            catch  
            {  
                // 阻止异常抛出  
            }  
  
            finally  
            {   // 关闭连接  
                // 从列表中移除连接  
                _mutexClients.WaitOne();
                _mutexClients.ReleaseMutex();  
            }  
        }  
  
        /// <summary>  
        /// 关闭侦听器  
        /// </summary>  
        /// <remarks>显示隐藏从基类继承的成员</remarks>  
        public void Stop()
        {   
            // 关闭已建立的连接  
            _mutexClients.WaitOne();
            _mutexClients.ReleaseMutex();
        }
    }
}
