using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace YouSoftBathWatcher
{
    public class UdpMessage
    {
        private IPEndPoint _ip;
        private string _message;

        public UdpMessage(IPEndPoint _ip, string _message)
        {
            this._ip = _ip;
            this._message = _message;
        }

        public IPEndPoint ip
        {
            get { return _ip; }
            set { _ip = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
    }
}
