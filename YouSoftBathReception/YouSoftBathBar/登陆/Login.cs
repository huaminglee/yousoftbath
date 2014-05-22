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

using YouSoftBathGeneralClass;

namespace YouSoftBathReception
{
    //登陆窗口
    public partial class LogIn : Form
    {
        //成员变量
        private static string connectionIP = "";
        private BathDBDataContext db;
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
            connectionIP = BathClass.get_config_by_key("connectionIP");
            if (connectionIP == "")
            {
                PCListForm pCListForm = new PCListForm();
                if (pCListForm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                connectionIP = pCListForm.ip;
                BathClass.set_config_by_key("connectionIP", connectionIP);
            }

            db = new BathDBDataContext(connectionString);

            if (!db.DatabaseExists())
            {
                BathClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                connectionIP = "";
                BathClass.set_config_by_key("connectionIP", connectionIP);
                this.Close();
                return;
            }
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
            BathClass.printWarningMsg("您现在使用的是试用版，可以免费使用10次;");
            int tLong = 0;
            try
            {
                tLong = (int)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Angel", "UseTimes", 0);
                BathClass.printWarningMsg("您已经使用了" + tLong + "次！");
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
                if (BathClass.printAskMsg("试用次数已到！您是否需要注册？") != DialogResult.Yes)
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
            if (id.Text == "") return;

            m_User = db.Employee.FirstOrDefault(x => x.id.ToString() == id.Text);
            if (m_User != null)
            {
                name.Text = m_User.name;
                job.Text = db.Job.FirstOrDefault(x => x.id == m_User.jobId).name;
            }
            else
            {
                name.Text = "";
                job.Text = "";
            }
        }

        //点击登陆
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
                return;

            if (m_User == null)
            {
                id.SelectAll();
                id.Focus();
                BathClass.printErrorMsg("该员工号不存在！");
                return;
            }
            if (BathClass.GetMD5(pwd.Text) != m_User.password)
            {
                pwd.SelectAll();
                pwd.Focus();
                BathClass.printErrorMsg("密码不对!");
                return;
            }

            write_user();
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.ShowDialog();
            this.Close();
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

        //绑定快捷键
        private void LogIn_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (okBtn.Enabled)
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //返回连接字符串
        public static string connectionString
        {
            get
            {
                return @"Data Source=" + connectionIP + @"\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";
                //return @"Data Source=" + "CAI-PC" + @"\SQLEXPRESS;"
                //+ @"Initial Catalog=BathDB;Persist Security Info=True;"
                //+ @"User ID=sa;pwd=123";
            }
        }

        private void canBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void id_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        //注册
        private void register_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm(db);
            registerForm.ShowDialog();
        }
    }
}
