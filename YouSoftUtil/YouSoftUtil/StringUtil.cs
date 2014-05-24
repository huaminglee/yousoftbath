using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouSoftUtil
{
    public class StringUtil
    {
        public static bool isEmpty(string str)
        {
            if (str == null || str == "null" || str == "") return true;
            return false;
        }
    }
}
