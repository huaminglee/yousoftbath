﻿/* ----------------------------------------------------------
文件名称：TcpListenerPlus.cs

作者：秦建辉

MSN：splashcn@msn.com
QQ：36748897

博客：http://blog.csdn.net/jhqin

开发环境：
    Visual Studio V2010
    .NET Framework 4 Client Profile

版本历史：
    V1.0	2012年04月25日
			实现线程池通信
 
参考资料：
    http://www.switchonthecode.com/tutorials/csharp-tutorial-simple-threaded-tcp-server
------------------------------------------------------------ */
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace YouSoftBathWatcher
{
    public partial class TcpListenerPlus : TcpListener
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localEP">本地终结点</param>
        public TcpListenerPlus(IPEndPoint localEP) : base(localEP)
        {   // 启动独立的侦听线程
            Thread ListenThread = new Thread(new ThreadStart(ListenThreadAction));
            ListenThread.Start();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localaddr">本地IP地址</param>
        /// <param name="port">侦听端口</param>
        public TcpListenerPlus(IPAddress localaddr, Int32 port) : base(localaddr, port)
        {   // 启动独立的侦听线程
            Thread ListenThread = new Thread(new ThreadStart(ListenThreadAction));
            ListenThread.Start();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~TcpListenerPlus()
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
  
        // 已接受的Tcp连接列表  
        protected List<TcpClient> _tcpClients;  
  
        /// <summary>  
        /// 连接列表操作互斥量  
        /// </summary>  
        private Mutex _mutexClients;  
  
        /// <summary>  
        /// 侦听连接线程  
        /// </summary>  
        private void ListenThreadAction()  
        {   // 启动侦听  
            Start();  
  
            // 初始化连接列表和互斥量  
            _tcpClients = new List<TcpClient>();  
            _mutexClients = new Mutex();  
  
            // 接受连接  
            while (true)  
            {  
                TcpClient tcpClient = null;  
                try  
                {   // 接受挂起的连接请求  
                    tcpClient = AcceptTcpClient();  
  
                    // 将该连接通信加入线程池队列  
                    ThreadPool.QueueUserWorkItem(ThreadPoolCallback, tcpClient);  
  
                    // 连接加入列表  
                    _mutexClients.WaitOne();  
                    _tcpClients.Add(tcpClient);  
                    _mutexClients.ReleaseMutex();  
                }  
  
                catch (SocketException)  
                {   // 结束侦听线程  
                    break;  
                }  
  
                catch (Exception)  
                {   // 加入队列失败  
                    if (tcpClient != null)  
                    {  
                        tcpClient.Close();  
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
            TcpClient tcpClient = state as TcpClient;  
            try  
            {   // 执行任务  
                if (OnThreadTaskRequest != null)  
                {  
                    OnThreadTaskRequest(tcpClient, EventArgs.Empty);  
                }  
            }  
  
            catch  
            {  
                // 阻止异常抛出  
            }  
  
            finally  
            {   // 关闭连接  
                tcpClient.Close();  
  
                // 从列表中移除连接  
                _mutexClients.WaitOne();  
                if (_tcpClients != null)  
                {  
                    _tcpClients.Remove(tcpClient);  
                }  
                _mutexClients.ReleaseMutex();  
            }  
        }  
  
        /// <summary>  
        /// 关闭侦听器  
        /// </summary>  
        /// <remarks>显示隐藏从基类继承的成员</remarks>  
        public new void Stop()
        {   // 检测是否已开启侦听  
            if (Active)
            {
                // 关闭侦听器  
                base.Stop();

                // 关闭已建立的连接  
                _mutexClients.WaitOne();
                if (_tcpClients != null)
                {
                    foreach (TcpClient client in _tcpClients)
                    {
                        client.Close();
                    }

                    // 清空连接列表  
                    _tcpClients.Clear();
                    _tcpClients = null;
                }
                _mutexClients.ReleaseMutex();
            }
        }
    }
}
