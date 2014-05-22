using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YouSoftBathGeneralClass
{
    public class RoomProRFL
    {
        public const string DLLFile = "proRFL.dll";

        //初始化USB
        [DllImport(DLLFile)]
        public static extern int initializeUSB(int d12);

        //蜂鸣
        [DllImport(DLLFile)]
        public static extern int Buzzer(int d12, byte p_dest);

        //发房卡
        [DllImport(DLLFile)]
        public static extern int GuestCard(int d12, int dlsCoID, int CardNo, int dai, int LLock, int pdoors,
            string BDate, string EDate, string LockNo, [MarshalAs(UnmanagedType.LPArray)]byte[] cardHexStr);

        //读房卡
        [DllImport(DLLFile)]
        public static extern int ReadCard(int d12, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        //退卡
        [DllImport(DLLFile)]
        public static extern int CardErase(int d12, int dlsCoID, [MarshalAs(UnmanagedType.LPArray)]byte[] p_dest);

        //功能：读取客人卡的锁号
        [DllImport(DLLFile)]
        public static extern int
            GetGuestLockNoByCardDataStr(int dlsCoID,
            [MarshalAs(UnmanagedType.LPArray)]byte[] cardHexStr,
            [MarshalAs(UnmanagedType.LPArray)]byte[] LockNo);
    }
}
