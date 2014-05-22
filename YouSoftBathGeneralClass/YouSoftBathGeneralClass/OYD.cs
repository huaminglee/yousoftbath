using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathGeneralClass
{
    public class OYD
    {
        public const string DLLFile = "FK-1DLL.DLL";

        [DllImport(DLLFile)]
        public static extern int FKOPEN();

        [DllImport(DLLFile)]
        public static extern int OYEDA_fk([MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport(DLLFile)]
        public static extern int OYEDA_md([MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);
        
        [DllImport(DLLFile)]
        public static extern int OYEDA_id([MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport("CH375DLL.DLL")]
        public static extern bool CH375SetTimeout(int a, int b, int c);
    }
}
