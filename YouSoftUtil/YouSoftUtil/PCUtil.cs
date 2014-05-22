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
    }
}
