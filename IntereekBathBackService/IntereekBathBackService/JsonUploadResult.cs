using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntereekBathBackService
{
    public class JsonUploadResult
    {
        private bool _success;
        private string _type;
        private int _errorCode;
        private string _errorDesc;

        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int errorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        public string errorDesc
        {
            get { return _errorDesc; }
            set { _errorDesc = value; }
        }
    }
}
