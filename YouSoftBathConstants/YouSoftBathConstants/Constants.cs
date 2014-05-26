using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouSoftBathConstants
{
    public enum SeatStatus
    {
        INVALID = -1,//无效状态
        AVILABLE = 1,//可用
        USING = 2,//正在使用
        PAIED = 3,//结账
        LOCKING = 4,//锁定
        STOPPED = 5,//停用
        WARNING = 6,//警告
        DEPOSITLEFT = 7,//押金离场
        REPAIED = 8,//重新结账
        RESERVE = 9//预定
    }

    public enum Deparments
    {
        SUNNA = 1,//桑拿
        HOTEL = 2//客房
    }


    public static class Constants
    {
        //public const string AliIP = "114.215.184.78";//阿里云IP地址
        public const string AliIP = "127.0.0.1";//阿里云IP地址
        public const string AliPort = "8080";//阿里云端口

        public const int LocalUdpPort = 7628;//本地服务器监听的端口号 

        public const string ErrorFile = "error.log";

        public const char SplitChar = '$';
        public const char BIG_SPLITCHAR = '|';


        public const string version = "V6.1";
        public const string appName = "咱家店小二";

        public const string WX_DONOR = "微信加服务号";
        public const string SMS_HINT_MSG = "代码：\r\n销毁：100\r\n备份：101";
    }

    public static class ConfigKeys
    {
        public const string KEY_COMPANY_CODE = "COMPANY_CODE";//阿里云注册的companyCode
        public const string KEY_CONNECTION_IP = "connectionIP";//服务器ip地址
        public const string KEY_CARD_PORT = "card_port";//会员卡端口
        public const string KEY_CARD_BAUD = "card_baud";//会员卡波特率
        public const string KEY_CARD_NOHINT = "no_hint";//会员卡不再提示
        public const string KEY_PRINTER = "printer";//会员卡不再提示

        public const string KEY_SMSPORT = "smsPort";//短信猫端口
        public const string KEY_SMSBAUD = "smsBaud";//短信猫波特率
        public const string KEY_PHONES_FILE = "phones.txt";//前台夜审发短信电话号码存放文件名称
        public const string KEY_SMS_OPERATION = "smsOperation"; //短信操控数据库

        public const string KEY_TECH_INDEX_MALE = "techIndexmale";//男技师排钟
        public const string KEY_TECH_INDEX_FEMALE = "techIndexfemale";//男技师排钟


        public const string KEY_VIDEO_TIMESPAN = "timespan";//前台录像长度
        public const string KEY_VIDEO_SAVE_DIR = "dir";//前台录像保存路径

        public const string KEY_ITEM_TABLE_FORMAT = "ITEM_TABLE_FORMAT";//项目报表格式

        public const string KEY_HOTELID = "hotelId";//酒店锁ID号

        public const string KEY_TIME_LIMIT = "time_limit";//时间限制
        public const string KEY_MONEY_LIMIT = "money_limit";//金额限制

        public const string KEY_CHECK_AUTO = "CheckAuto";//后台服务是否自动超时加收
        public const string KEY_CHECK_SERVER = "CheckServer";//是否需要需要启动UDP服务器线程
        public const string KEY_CHECK_DETECT = "CheckDetect";//后台服务是否自动检测手牌异常
    }
}

