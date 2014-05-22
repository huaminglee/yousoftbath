using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Management;
using System.Data.SqlClient;

using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    class GeneralClass
    {
        //打印错误信息
        public static void printErrorMsg(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //打印警告信息
        public static void printWarningMsg(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //打印询问信息
        public static DialogResult printAskMsg(string msg)
        {
            return MessageBox.Show(msg, "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        //产生4位随机数
        public static string random_code
        {
            get
            {
                string code = "";
                Random ran = new Random();
                for (int i = 0; i < 4; i++ )
                {
                    code += ran.Next(0, 10).ToString();
                }
                return code;
            }
        }

        //卡密码
        public static string cardCode = "FFFFFFFFFFFF";
        
        //获取公司名称
        public static string companyCode(BathDBDataContext db)
        {
            return db.Options.FirstOrDefault().companyCode.PadRight(32, '0');
        }

        //将金额转换成32长度的字符串，有小数点的话则在后面直接加0，没有小数点则加小数点，后面补0,小数点用D表示
        public static string doubleTo32Str(double number)
        {
            string str = number.ToString();
            string[] strs = str.Split('.');

            str = "";
            if (strs.Length == 1)
            {
                str = strs[0] + "D";
                str = str.PadRight(32, '0');
            }
            else
            {
                str = strs[0] + "D" + strs[1];
                str = str.PadRight(32, '0');
            }
            return str;
        }

        //卡里面的32位字符转换成金额
        public static double str32ToDouble(string str)
        {
            str = str.Replace('D', '.');
            return Convert.ToDouble(str);
        }
    }

    class GetStringSpell
    {
        /// <summary>   
        /// 提取汉字首字母   
        /// </summary>   
        /// <param name="strText">需要转换的字</param>   
        /// <returns>转换结果</returns>   
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        /// <summary>   
        /// 把提取的字母变成大写   
        /// </summary>   
        /// <param name="strText">需要转换的字符串</param>   
        /// <returns>转换结果</returns>   
        public static string GetLowerChineseSpell(string strText)
        {
            return GetChineseSpell(strText).ToLower();
        }
        /// <summary>   
        /// 把提取的字母变成大写   
        /// </summary>   
        /// <param name="myChar">需要转换的字符串</param>   
        /// <returns>转换结果</returns>   
        public static string GetUpperChineseSpell(string strText)
        {
            return GetChineseSpell(strText).ToUpper();
        }
        /// <summary>   
        /// 获取单个汉字的首拼音   
        /// </summary>   
        /// <param name="myChar">需要转换的字符</param>   
        /// <returns>转换结果</returns>   
        public static string getSpell(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "_";
            }
            else return myChar;
        }
    }
}
