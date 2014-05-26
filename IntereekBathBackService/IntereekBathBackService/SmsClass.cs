using System;
using System.Text;
using System.Runtime.InteropServices;

namespace IntereekBathBackService
{
    class SmsClass
    {
        [DllImport("sms.dll", EntryPoint = "Sms_Connection")]
        public static extern uint Sms_Connection(string CopyRight, uint Com_Port, uint Com_BaudRate, out string Mobile_Type, out string CopyRightToCOM);

        [DllImport("sms.dll", EntryPoint = "Sms_Disconnection")]
        public static extern uint Sms_Disconnection();

        [DllImport("sms.dll", EntryPoint = "Sms_Send")]
        public static extern uint Sms_Send(string Sms_TelNum, string Sms_Text);

        [DllImport("sms.dll", EntryPoint = "Sms_Receive")]
        public static extern uint Sms_Receive(string Sms_Type, out string Sms_Text);

        [DllImport("sms.dll", EntryPoint = "Sms_Delete")]
        public static extern uint Sms_Delete(string Sms_Index);

        [DllImport("sms.dll", EntryPoint = "Sms_AutoFlag")]
        public static extern uint Sms_AutoFlag();

        [DllImport("sms.dll", EntryPoint = "Sms_NewFlag")]
        public static extern uint Sms_NewFlag();

       
        
        //[DllImport("kernel32")]
        //public static extern int GetPrivateProfileInt(string section, string key, int def, string filePath);
        //[DllImport("kernel32")]
        //public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        //[DllImport("kernel32")]
        //public static extern int WritePrivateProfileString(string section, string key, string Val, string filePath);


    }
}
