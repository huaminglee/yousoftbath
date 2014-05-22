using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace IntereekBathWeChat
{
    static class Constants
    {
        public const string AliIP = "114.215.184.78";//阿里云IP地址
        public const string AliPort = "80";//阿里云端口

        //public const string AliIP = "127.0.0.1";//本地调试IP地址
        //public const string AliPort = "8080";//本地调试端口

        public const int LocalUdpPort = 7628;//本地服务器监听的端口号 

        public const string ErrorFile = @"log\error.log";

        public const char SplitChar = '$';

    }

    static class ConfigKeys
    {
        public const string KEY_COMPANY_CODE = "COMPANY_CODE";//阿里云注册的companyCode
        public const string KYE_QUERY_DATA = "queryData"; //查询业绩
        public const string KYE_QUERY_MULTIDATA = "queryMultiData"; //查询多店业绩
        public const string KYE_QUERY_WXUSER = "queryWxUser";//查询微信用户
        public const string KYE_REGISTER_COMPANY = "registerCompany";//注册新公司
    }
}
