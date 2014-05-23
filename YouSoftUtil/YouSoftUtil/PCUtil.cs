using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Net;

namespace YouSoftUtil
{
    public class PCUtil
    {
        //获取本机物理地址
        public static string getMacAddr_Local()
        {
            string madAddr = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if (Convert.ToBoolean(mo["IPEnabled"]) == true)
                {
                    madAddr = mo["MacAddress"].ToString();
                    madAddr = madAddr.Replace(':', '-');
                }
                mo.Dispose();
            }
            return madAddr;
        }

        //获取本机Ip地址
        public static string getLocalIp()
        {
            string ip = "";
            if (Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length > 0)
            {
                foreach (IPAddress ipa in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                {
                    string[] strs = ipa.ToString().Split('.');
                    if (strs.Count() == 4)//IPV4地址
                    {
                        ip = ipa.ToString();
                        break;
                    }
                }
            }

            return ip;
        }

        //java时间与net时间互换
        public static DateTime converJavaTimeToNetTime(long time_JAVA_Long)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0); 
            long tricks_1970 = dt_1970.Ticks;//1970年1月1日刻度                         
            long time_tricks = tricks_1970 + time_JAVA_Long * 10000;//日志日期刻度                         
            DateTime dt = new DateTime(time_tricks).AddHours(8);//转化为DateTime

            return dt;
        }
    }
}
