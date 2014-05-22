using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathReception
{
    class ICRF
    {
        [DllImport("mwrf32.dll", EntryPoint = "rf_init", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //说明：初始化通讯接口
        public static extern int rf_init(Int16 port, int baud);


        [DllImport("mwrf32.dll", EntryPoint = "rf_card", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        //说明：寻卡，能返回在工作区域内某张卡的序列号
        public static extern int rf_card(int icdev, char _Mode,  ref ulong _Snr);

        [DllImport("mwrf32.dll")]
        public static extern short rf_load_key(int icdev, int mode, int secnr, [In] byte[] nkey);  //密码装载到读写模块中

        [DllImport("mwrf32.dll")]
        public static extern short rf_load_key_hex(int icdev, int mode, int secnr, string nkey);  //密码装载到读写模块中

        [DllImport("mwrf32.dll")]
        public static extern short rf_authentication(int icdev, int _Mode, int _SecNr);

        [DllImport("mwrf32.dll")]
        public static extern short rf_read(int icdev, int adr, [Out] byte[] sdata);

        [DllImport("mwrf32.dll")]
        public static extern short rf_write(int icdev, int adr, [In] byte[] sdata);  //向卡中写入数据

        [DllImport("mwrf32.dll")]
        public static extern short rf_exit(int icdev);

        [DllImport("mwrf32.dll")]
        public static extern short rf_beep(int icdev, uint _Msec);////蜂鸣

        [DllImport("mwrf32.dll")]
        public static extern int a_hex([MarshalAs(UnmanagedType.LPArray)]byte[] asc, 
            [MarshalAs(UnmanagedType.LPArray)] byte[] hex, 
            int length);  //普通字符转换成十六进制字符

        [DllImport("mwrf32.dll")]
        public static extern void hex_a(ref string oldValue, ref string newValue, int len);  //十六进制字符转换成普通字符

        [DllImport("mwrf32.dll")]//解密
        public static extern void rf_decrypt(string key, 
            [MarshalAs(UnmanagedType.LPArray)]byte[] p_source, uint len,
            [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport("decrypt.dll", EntryPoint = "my_decrypt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16
        my_decrypt([MarshalAs(UnmanagedType.LPArray)]byte[] p_source, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        [DllImport("decrypt.dll", EntryPoint = "my_encrypt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern Int16
        my_encrypt([MarshalAs(UnmanagedType.LPArray)]byte[] p_source, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

    }
}
