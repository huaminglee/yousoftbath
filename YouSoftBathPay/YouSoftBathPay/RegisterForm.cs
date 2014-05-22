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

namespace YouSoftBathPay
{
    public partial class RegisterForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public static int[] intCode = new int[127];    //存储密钥
        public static char[] charCode = new char[25];  //存储ASCII码
        public static int[] intNumber = new int[25];   //存储ASCII码值

        //构造函数
        public RegisterForm(BathDBDataContext dc)
        {
            db = dc == null ? new BathDBDataContext() : dc;
            InitializeComponent();
        }

        //对话框载入
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            serial.Text = GetMNum();
        }

        //注册
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (code.Text == GetRNum())
            {
                MessageBox.Show("注册成功！重启软件后生效！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RegistryKey retkey = Registry.CurrentUser.OpenSubKey("Software", true).CreateSubKey("wxf").CreateSubKey("wxf.INI").CreateSubKey(code.Text);
                retkey.SetValue("UserName", "Rsoft");
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\AngelReg", "AngelReg", DateTime.Now.ToString());
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                GeneralClass.printErrorMsg("注册失败，请联系赵经理：18670068930");
                this.Close();
            }
        }

        //取消
        private void canBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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

        //检车软件是否注册，没有注册的话，检测软件是否使用超过10天的时限
        public static bool registered()
        {
            bool HasRegistered = false;
            string regStr = GetRNum();
            RegistryKey retkey = Registry.CurrentUser.OpenSubKey("SOFTWARE", true).CreateSubKey("wxf").CreateSubKey("wxf.INI");
            foreach (string strRNum in retkey.GetSubKeyNames())
            {
                if (strRNum == regStr)
                {
                    string regTimeStr = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\AngelReg", "AngelReg", 0).ToString();
                    DateTime regTime = Convert.ToDateTime(regTimeStr);
                    TimeSpan tSpan = DateTime.Now - regTime;
                    if (tSpan.TotalDays > 10)
                    {
                        GeneralClass.printWarningMsg("软件使用已经过期，请联系YouSoft优软公司重新注册!");
                        return false;
                    }
                    HasRegistered = true;
                }
            }
            return HasRegistered;
        }
    }
}
