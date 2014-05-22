using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathGeneralClass
{
    public class JYW
    {
        public const string DLLFile = "Reader.dll";

        [DllImport(DLLFile)]
        public static extern void CloseReader();

        [DllImport(DLLFile)]
        public static extern int FK([MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport(DLLFile)]
        public static extern int MD([MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport(DLLFile)]
        public static extern int ReadID([MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);
    }
}
