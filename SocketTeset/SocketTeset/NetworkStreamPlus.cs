/* ----------------------------------------------------------
文件名称：NetworkStreamPlus.cs

作者：秦建辉

MSN：splashcn@msn.com
QQ：36748897

博客：http://blog.csdn.net/jhqin

开发环境：
    Visual Studio V2010
    .NET Framework 4 Client Profile

版本历史：
    V1.0    2012年04月19日
            完成异步读写和数据加密传输功能
------------------------------------------------------------ */
using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace SocketTeset
{
    /// <summary>  
    /// 异步读状态对象  
    /// </summary>  
    internal class AsyncReadStateObject
    {
        public ManualResetEvent eventDone;
        public NetworkStream stream;
        public Exception exception;
        public Int32 numberOfBytesRead;
    }

    /// <summary>  
    /// 异步写状态对象  
    /// </summary>  
    internal class AsyncWriteStateObject
    {
        public ManualResetEvent eventDone;
        public NetworkStream stream;
        public Exception exception;
    }  

    /// <summary>
    /// 构造函数和析构函数
    /// </summary>
    public partial class NetworkStreamPlus : IDisposable
    {
        /// <summary>
        /// 网络数据流，只读字段
        /// </summary>
        public readonly NetworkStream Stream;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NetworkStreamPlus(NetworkStream netStream)
        {   // 只读字段只能在构造函数中初始化
            Stream = netStream;
        }

        /// <summary>
        /// 释放所有托管资源和非托管资源
        /// </summary>
        public void Dispose()
        {
            // 关闭加密传输模块
            //SecurityClose();

            // 请求系统不要调用指定对象的终结器
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~NetworkStreamPlus()
        {
            Dispose();
        }

        /// <summary>  
        /// 异步发送  
        /// </summary>  
        /// <param name="buffer">字节数组</param>  
        /// <param name="offset">起始偏移量</param>  
        /// <param name="size">字节数</param>  
        public void Write(Byte[] buffer, Int32 offset, Int32 size)  
        {  
            // 用户定义对象  
            AsyncWriteStateObject State = new AsyncWriteStateObject  
            {   // 将事件状态设置为非终止状态，导致线程阻止  
                eventDone = new ManualResetEvent(false),  
                stream = Stream,  
                exception = null,  
            };  
              
            Byte[] BytesArray;  
            Int32 Length = size + 4;    // 加入4字节长度信息后的总长度  
            BytesArray = new Byte[Length];  
            Array.Copy(BitConverter.GetBytes(Length), BytesArray, 4);  
            Array.Copy(buffer, offset, BytesArray, 4, size);  
  
            // 写入加长度信息头的数据  
            Stream.BeginWrite(BytesArray, 0, BytesArray.Length, new AsyncCallback(AsyncWriteCallback), State);  
  
            // 等待操作完成信号  
            if (State.eventDone.WaitOne(Stream.WriteTimeout, false))  
            {   // 接收到信号  
                if (State.exception != null) throw State.exception;  
            }  
            else  
            {   // 超时异常  
                throw new TimeoutException();  
            }  
        }  
  
        /// <summary>  
        /// 异步发送  
        /// </summary>  
        /// <param name="data">字节数组</param>  
        public void Write(Byte[] data)  
        {  
            Write(data, 0, data.Length);  
        }  
  
        /// <summary>  
        /// 异步发送  
        /// </summary>  
        /// <param name="command">字符串</param>  
        /// <param name="codePage">代码页</param>  
        /// <remarks>  
        /// 代码页：  
        ///     936：简体中文GB2312  
        ///     54936：简体中文GB18030  
        ///     950：繁体中文BIG5  
        ///     1252：西欧字符CP1252  
        ///     65001：UTF-8编码  
        /// </remarks>  
        public void Write(String command)  
        {  
            Write(Encoding.GetEncoding(65001).GetBytes(command));  
        }  
  
        /// <summary>  
        /// 异步写入回调函数  
        /// </summary>  
        /// <param name="ar">异步操作结果</param>  
        private static void AsyncWriteCallback(IAsyncResult ar)  
        {  
            AsyncWriteStateObject State = ar.AsyncState as AsyncWriteStateObject;  
            try  
            {   // 异步写入结束  
                State.stream.EndWrite(ar);  
            }  
  
            catch (Exception e)  
            {   // 异步连接异常  
                State.exception = e;  
            }  
  
            finally  
            {   // 将事件状态设置为终止状态，线程继续  
                State.eventDone.Set();  
            }  
        }


        /// <summary>  
        /// 接收缓冲区大小  
        /// </summary>  
        public Int32 ReceiveBufferSize = 8 * 1024;  
  
        /// <summary>  
        /// 异步接收  
        /// </summary>  
        /// <param name="data">接收到的字节数组</param>  
        public void Read(out Byte[] data)  
        {  
            // 用户定义对象  
            AsyncReadStateObject State = new AsyncReadStateObject  
            {   // 将事件状态设置为非终止状态，导致线程阻止  
                eventDone = new ManualResetEvent(false),  
                stream = Stream,  
                exception = null,  
                numberOfBytesRead = -1  
            };  
  
            Byte[] Buffer = new Byte[ReceiveBufferSize];  
            using (MemoryStream memStream = new MemoryStream(ReceiveBufferSize))  
            {  
                Int32 TotalBytes = 0;       // 总共需要接收的字节数  
                Int32 ReceivedBytes = 0;    // 当前已接收的字节数  
                while (true)  
                {  
                    // 将事件状态设置为非终止状态，导致线程阻止  
                    State.eventDone.Reset();  
  
                    // 异步读取网络数据流  
                    Stream.BeginRead(Buffer, 0, Buffer.Length, new AsyncCallback(AsyncReadCallback), State);  
  
                    // 等待操作完成信号  
                    if (State.eventDone.WaitOne(Stream.ReadTimeout, false))  
                    {   // 接收到信号  
                        if (State.exception != null) throw State.exception;  
  
                        if (State.numberOfBytesRead == 0)  
                        {   // 连接已经断开  
                            throw new SocketException();  
                        }  
                        else if (State.numberOfBytesRead > 0)  
                        {  
                            if (TotalBytes == 0)  
                            {   // 提取流头部字节长度信息  
                                TotalBytes = BitConverter.ToInt32(Buffer, 0);  
  
                                // 保存剩余信息  
                                memStream.Write(Buffer, 4, State.numberOfBytesRead - 4);  
                            }  
                            else  
                            {  
                                memStream.Write(Buffer, 0, State.numberOfBytesRead);  
                            }  
  
                            ReceivedBytes += State.numberOfBytesRead;  
                            if (ReceivedBytes >= TotalBytes) break;  
                        }  
                    }  
                    else  
                    {   // 超时异常  
                        throw new TimeoutException();  
                    }  
                }  
  
                data = (memStream.Length > 0) ? memStream.ToArray() : null;  
            }  
        }  
  
        /// <summary>  
        /// 异步接收  
        /// </summary>  
        /// <param name="answer">接收到的字符串</param>  
        /// <param name="codePage">代码页</param>  
        /// <remarks>  
        /// 代码页：  
        ///     936：简体中文GB2312  
        ///     54936：简体中文GB18030  
        ///     950：繁体中文BIG5  
        ///     1252：西欧字符CP1252  
        ///     65001：UTF-8编码  
        /// </remarks>  
        public void Read(out String answer)  
        {  
            Byte[] data;  
            Read(out data);  
            answer = (data != null) ? Encoding.GetEncoding(65001).GetString(data) : null;  
        }  
  
        /// <summary>  
        /// 异步读取回调函数  
        /// </summary>  
        /// <param name="ar">异步操作结果</param>  
        private static void AsyncReadCallback(IAsyncResult ar)  
        {  
            AsyncReadStateObject State = ar.AsyncState as AsyncReadStateObject;  
            try  
            {   // 异步写入结束  
                State.numberOfBytesRead = State.stream.EndRead(ar);  
            }  
  
            catch (Exception e)  
            {   // 异步连接异常  
                State.exception = e;  
            }  
  
            finally  
            {   // 将事件状态设置为终止状态，线程继续  
                State.eventDone.Set();  
            }  
        }  
    }
}
