using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace YouSoftBathPay
{
    //登陆窗口
    public partial class LogIn : Form
    {
        //成员变量
        private BathDBDataContext db = new BathDBDataContext();
        private List<string> idList = new List<string>();
        public static Employee m_User;
        public static bool hasRegistered = true;

        public LogIn()
        {
            InitializeComponent();
        }

        //对话框载入
        private void LogIn_Load(object sender, EventArgs e)
        {
            if (!RegisterForm.registered())
                hasRegistered = false;

            if (hasRegistered)
                register.Visible = false;
            else if (!hasRegistered && !checkTrialTimes())
                okBtn.Enabled = false;

            read_users();
            id.Items.AddRange(idList.ToArray());
            if (id.Items.Count != 0)
                id.Text = id.Items[0].ToString();
        }

        //检查软件使用次数是否超过10次
        private bool checkTrialTimes()
        {
            GeneralClass.printWarningMsg("您现在使用的是试用版，可以免费使用10次;");
            int tLong = 0;
            try
            {
                tLong = (int)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Angel", "UseTimes", 0);
                GeneralClass.printWarningMsg("您已经使用了" + tLong + "次！");
            }
            catch
            {
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Angel", "UseTimes", 0, RegistryValueKind.DWord);
            }
            if (tLong < 10)
            {
                int tTimes = tLong + 1;
                Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Angel", "UseTimes", tTimes);
                return true;
            }
            else
            {
                if (GeneralClass.printAskMsg("试用次数已到！您是否需要注册？") != DialogResult.Yes)
                    return false;
                RegisterForm regForm = new RegisterForm(db);
                if (regForm.ShowDialog() != DialogResult.OK)
                    return false;
                return true;
            }
        }

        //检验员工号
        private void id_TextChanged(object sender, EventArgs e)
        {
            if (id.Text.Length == 0) return;
            int testInt = 0;
            if (!int.TryParse(id.Text, out testInt))
            {
                GeneralClass.printErrorMsg("数据不合规范!");
                id.SelectAll();
                id.Focus();
                return;
            }

            var employee = db.Employee.FirstOrDefault(x => x.id == testInt);
            if (employee != null)
            {
                name.Text = employee.name;
                job.Text = db.Job.FirstOrDefault(x => x.id == employee.jobId).name;
            }
            else
            {
                name.Text = "";
                job.Text = "";
            }
        }

        //验证用户名和密码
        private void verifyEmployee()
        {
            m_User = db.Employee.FirstOrDefault(x => x.id == Convert.ToInt32(id.Text));
            if (m_User == null)
            {
                id.SelectAll();
                id.Focus();
                GeneralClass.printErrorMsg("该员工号不存在！");
                return;
            }
            if (pwd.Text != m_User.password)
            {
                pwd.SelectAll();
                pwd.Focus();
                GeneralClass.printErrorMsg("密码不对!");
                return;
            }

            write_user();
            this.Hide();
            MainWindow mainWindow = new MainWindow(db);
            mainWindow.ShowDialog();
            this.Close();
        }

        //点击登陆
        private void okBtn_Click(object sender, EventArgs e)
        {
            verifyEmployee();
        }

        //取消
        private void canBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //回车登陆
        private void pwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                verifyEmployee();
        }

        //读取历史员工号
        private void read_users()
        {
            if (!Directory.Exists(@".\config"))
                Directory.CreateDirectory(@".\config");

            if (!File.Exists(@".\config\users.ini"))
            {
                using (FileStream fs = new FileStream(@".\config\users.ini", FileMode.Create))
                    return;
            }
            using (StreamReader sr = new StreamReader(@".\config\users.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    idList.Add(line.Trim());
                }
            }
        }

        //将新登陆员工号写入本地配置文件
        private void write_user()
        {
            if (!idList.Contains(id.Text.Trim()))
            {
                idList.Insert(0, id.Text.Trim());
                using (StreamWriter sw = new StreamWriter(@".\config\users.ini"))
                {
                    foreach (string user in idList)
                        sw.WriteLine(user);
                }
            }
        }
    }
}
