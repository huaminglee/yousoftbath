using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntereekBathBackService
{
    public class MConvert<T>
    {
        public static T ToTypeOrDefault(object obj, T default_val)
        {
            T val = default_val;
            try
            {
                val = (T)Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                val = default_val;
            }

            return val;
        }
    }
}
