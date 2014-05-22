using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntereekBathBackService
{
    public class StringUtil
    {
        public static bool isEmpty(string str)
        {
            if (null == str || "" == str || "null" == str) return true;
            return false;
        }
    }
}
