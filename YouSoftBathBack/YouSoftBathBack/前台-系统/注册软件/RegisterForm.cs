using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Microsoft.Win32;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class RegisterForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public static int[] intCode = new int[127];    //存储密钥
        public static char[] charCode = new char[25];  //存储ASCII码
        public static int[] intNumber = new int[25];   //存储ASCII码值

        //构造函数
        public RegisterForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            serial.Text = GetMNum();
        }

        private string date_string()
        {
            string str = DateTime.Now.ToString("yyyyMMdd");
            string str_dest = "";
            foreach (char c in str)
            {
                str_dest += c + 88;
            }
            return str_dest;
        }

        //注册
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (GetRNum().IndexOf(GetRNum()) == 0)
            {
                string regTimeStr = code.Text.Substring(24);
                DateTime regTime = reg_date(regTimeStr);
                TimeSpan tSpan = BathClass.Now(LogIn.connectionString) - regTime;
                if (tSpan.TotalDays >= 365)
                {
                    GeneralClass.printErrorMsg("注册码已经过期, 请联系赵经理：18674468090");
                    this.Close();
                    return;
                }
                MessageBox.Show("注册成功！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var regKey = Registry.LocalMachine;
                regKey = regKey.OpenSubKey("Software", true);
                regKey = regKey.OpenSubKey("wxf", true).OpenSubKey("wxf.INI", true);
                foreach (var subkey in regKey.GetSubKeyNames())
                    regKey.DeleteSubKeyTree(subkey);

                regKey.CreateSubKey(code.Text);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                GeneralClass.printErrorMsg("注册失败，请联系赵经理：18674468090");
                this.Close();
            }
        }

        //获取硬盘卷标号
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        //获取CPU序列号
        public static string GetCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuCollection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuCollection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
            }
            return strCpu;
        }

        //生成机器码
        public static string GetMNum()
        {
            string strNum = GetCpu() + GetDiskVolumeSerialNumber();
            string strMNum = strNum.Substring(0, 24);    //截取前24位作为机器码
            return strMNum;
        }

        //初始化密钥
        public static void SetIntCode()
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        //生成注册码
        public static string GetRNum()
        {
            SetIntCode();
            string strMNum = GetMNum();
            for (int i = 1; i < charCode.Length; i++)   //存储机器码
            {
                charCode[i] = Convert.ToChar(strMNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)  //改变ASCII码值
            {
                intNumber[j] = Convert.ToInt32(charCode[j]) + intCode[Convert.ToInt32(charCode[j])];
            }
            string strAsciiName = "";   //注册码
            for (int k = 1; k < intNumber.Length; k++)  //生成注册码
            {

                if ((intNumber[k] >= 48 && intNumber[k] <= 57) || (intNumber[k] >= 65 && intNumber[k]
                    <= 90) || (intNumber[k] >= 97 && intNumber[k] <= 122))  //判断如果在0-9、A-Z、a-z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[k]).ToString();
                }
                else if (intNumber[k] > 122)  //判断如果大于z
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 10).ToString();
                }
                else
                {
                    strAsciiName += Convert.ToChar(intNumber[k] - 9).ToString();
                }
            }
            return strAsciiName;
        }

        private static DateTime reg_date(string date_str)
        {
            string[] strs = date_str.Split('|');
            string tmp_str = "";
            foreach (string str in strs)
            {
                if (str == "")
                    continue;

                tmp_str += (char)(Convert.ToInt32(str) - 88);
            }

            return Convert.ToDateTime(tmp_str);
        }

        //检车软件是否注册，没有注册的话，检测软件是否使用超过10天的时限
        public static bool registered()
        {
            bool HasRegistered = false;
            string regStr = GetRNum();

            var regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey("SOFTWARE", true);
            if (regKey.OpenSubKey("wxf") == null)
            {
                regKey.CreateSubKey("wxf").CreateSubKey("wxf.INI");
                return HasRegistered;
            }

            regKey = regKey.OpenSubKey("wxf", true);
            if (regKey.OpenSubKey("wxf.INI") == null)
            {
                regKey.CreateSubKey("wxf.INI");
                return HasRegistered;
            }

            regKey = regKey.OpenSubKey("wxf.INI");
            foreach (string strRNum in regKey.GetSubKeyNames())
            {
                if (strRNum.IndexOf(regStr) == 0)
                {
                    string regTimeStr = strRNum.Substring(24);
                    DateTime regTime = reg_date(regTimeStr);
                    TimeSpan tSpan = BathClass.Now(LogIn.connectionString) - regTime;
                    if (tSpan.TotalDays >= 365)
                    {
                        GeneralClass.printWarningMsg("软件使用已经过期，请联系YouSoft优软公司重新注册!");
                        return false;
                    }
                    if (tSpan.TotalDays >= 335)
                    {
                        GeneralClass.printWarningMsg("软件使用还剩" + (10 - tSpan.Days).ToString() + "天，请联系YouSoft优软公司重新注册!");
                        return false;
                    }
                    HasRegistered = true;
                }
            }
            return HasRegistered;
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}
