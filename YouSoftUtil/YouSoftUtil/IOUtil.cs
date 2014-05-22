using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace YouSoftUtil
{
    public class IOUtil
    {
        //根据key值、val值更改或者写入配置文件
        public static void set_config_by_key(string key, string val)
        {
            if (!Directory.Exists(@".\config"))
                Directory.CreateDirectory(@".\config");
            if (!File.Exists(@".\config\config.ini"))
            {
                FileStream fs = new FileStream(@".\config\config.ini", FileMode.Create);
                fs.Close();
            }

            string infor = "";
            infor = key + "=" + val + "\r\n";
            using (StreamReader sr = new StreamReader(@".\config\config.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] vals = line.Split('=');
                    if (vals.Length != 0 && vals[0] == key)
                        continue;
                    else
                        infor += line + "\r\n";
                }
            }

            using (StreamWriter sw = new StreamWriter(@".\config\config.ini", false))
                sw.Write(infor);
        }

        //根据key值获取配置
        public static string get_config_by_key(string key)
        {
            if (!Directory.Exists(@".\config"))
                return "";

            if (!File.Exists(@".\config\config.ini"))
                return "";

            string val = "";
            using (StreamReader sr = new StreamReader(@".\config\config.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains(key + "="))
                        return line.Substring(key.Length + 1);

                }
            }
            return val;
        }

        //读取文件
        public static List<string> read_file(string fileName)
        {
            List<string> info = new List<string>();
            fileName = @".\" + fileName;
            if (!File.Exists(fileName))
                return info;

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    info.Add(line.Trim());
                }
            }

            return info;
        }

        //写入文件
        public static void insert_file(string msg)
        {
            try
            {
                if (!Directory.Exists(@".\Log"))
                    Directory.CreateDirectory(@".\Log");
                if (!File.Exists(@".\Log\error.log"))
                {
                    FileStream fs = new FileStream(@".\Log\error.log", FileMode.Create);
                    fs.Close();
                }

                using (StreamWriter sw = new StreamWriter(@".\Log\error.log", true))
                    sw.Write(msg + "\r\n");
            }
            catch (System.Exception ex)
            {

            }
        }

        //写入文件
        public static void insert_file(string msg, string fileName)
        {

            try
            {
                fileName = @".\" + fileName;
                if (!File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.Create);
                    fs.Close();
                }

                using (StreamWriter sw = new StreamWriter(fileName, true))
                    sw.Write(msg + "\r\n");
            }
            catch (System.Exception ex)
            {

            }
        }

        //获取MD5加密字符串
        public static string GetMD5(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Unicode.GetBytes(input));
            return Convert.ToBase64String(res);
        }
    }
}
