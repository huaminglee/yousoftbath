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
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathConstants;
using System.Reflection;
using System.Threading;

namespace YouSoftBathFormClass
{
    //登陆窗口
    public partial class LogIn : Form
    {
        //成员变量
        private static string _connectionIP = "";
        private BathDBDataContext db;
        private List<string> idList = new List<string>();
        public static Employee m_User;
        public static bool hasRegistered = true;
        private Form m_Window;
        private Control current_box;
        private string productName;
        private string _work_progress = "软件正在初始化...";
        private static COptions _options = null;
        //private LogInProgressForm _logIn_progress_form;

        public string work_progress
        {
            get { return _work_progress; }
            set { _work_progress = value; }
        }

        public static COptions options
        {
            get { return _options; }
            set { _options = value; }
        }

        public static string connectionIP
        {
            get { return _connectionIP; }
            set { _connectionIP = value; }
        }

        public LogIn(Form mainWindow)
        {
            m_Window = mainWindow;
            InitializeComponent();
            label6.Text = "连科科技-店小二酒店会所管理系统" + Constants.version;
        }

        //对话框载入
        private void LogIn_Load(object sender, EventArgs e)
        {
            BathClass.change_input_en();
            //form_load_func();

            //Thread td = new Thread(new ThreadStart(form_load));
            //td.IsBackground = true;
            //td.Start();

            form_load_func();
        }

        private void form_load()
        {
            if (this.InvokeRequired)
                this.Invoke(new form_load_delegate(form_load_func));
            else
                form_load_func();
        }
        private delegate void form_load_delegate();
        private void form_load_func()
        {
            //while (true)
            //{
            //}
            work_progress = "正在获取软件版本号...";
            productName = "";
            int cur_version = get_current_version(ref productName);
            //if (productName == "收银" || productName == "前台")
            //    register.Visible = true;
            //else
            //    register.Visible = false;

            work_progress = "正在检测网络连接...";
            db = new BathDBDataContext(connectionString);
            if (!db.DatabaseExists())
            {
                BathClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                this.Close();
                return;
                //PCListForm pCListForm = new PCListForm();
                //if (pCListForm.ShowDialog() != DialogResult.OK)
                //{
                //    //_logIn_progress_form.Close();
                //    this.Close();
                //    return;
                //}

                //connectionIP = pCListForm.ip;
                //BathClass.set_config_by_key("connectionIP", connectionIP);
                //db = new BathDBDataContext(connectionString);
                //if (!db.DatabaseExists())
                //{
                //    BathClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                //    //_logIn_progress_form.Close();
                //    this.Close();
                //    return;
                //}
            }

            work_progress = "正在检测软件版注册序列号...";
            //if (productName == "收银" || productName == "前台")
            //{
                if (!RegisterForm.registered())
                    hasRegistered = false;

                if (hasRegistered)
                    register.Visible = false;
                else if (!hasRegistered && !checkTrialTimes())
                    okBtn.Enabled = false;
            //}

            work_progress = "正在读取软件用户名...";
            read_users();
            id.Items.AddRange(idList.ToArray());
            if (id.Items.Count != 0)
                id.Text = id.Items[0].ToString();

            current_box = id;
            current_box.Focus();

            work_progress = "正在从服务器获取最新软件版本号...";
            BathClass.download_file(connectionIP, "version.ini", productName + "/version.ini");
            int new_version = read_version();
            if (new_version > cur_version)
            {
                //_logIn_progress_form.Close();
                start_updtate();
                this.Close();
                return;
            }
            updateSystemTime();
            //_logIn_progress_form.Close();
        }

        //更新系统时间
        private void updateSystemTime()
        {
            UpdateTime.SetDate(BathClass.Now(connectionString));
        }


        private void start_updtate()
        {
            string arg1 = connectionIP;
            string arg2 = productName;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "update.exe";//需要启动的程序名       
            p.StartInfo.WorkingDirectory = Application.StartupPath;
            p.StartInfo.Arguments = arg1 + " " + arg2;//启动参数       
            p.Start();//启动      
        }

        private int get_current_version(ref string productName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取程序集元数据
            AssemblyCopyrightAttribute copyright = (AssemblyCopyrightAttribute)
            AssemblyCopyrightAttribute.GetCustomAttribute(Assembly.GetExecutingAssembly(),
            typeof(AssemblyCopyrightAttribute));
            AssemblyDescriptionAttribute description = (AssemblyDescriptionAttribute)
            AssemblyDescriptionAttribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),
            typeof(AssemblyDescriptionAttribute));

            productName = Application.ProductName;
            string ver = Application.ProductVersion;
            ver = ver.Replace(".","");
            return Convert.ToInt32(ver);
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
                RegisterForm regForm = new RegisterForm();
                if (regForm.ShowDialog() != DialogResult.OK)
                    return false;
                return true;
            }
        }

        //检验员工号
        private void id_TextChanged(object sender, EventArgs e)
        {
            if (id.Text == "") return;

            var dc = new BathDBDataContext(connectionString);
            m_User = dc.Employee.FirstOrDefault(x => x.id.ToString() == id.Text);
            if (m_User != null)
            {
                name.Text = m_User.name;
                job.Text = dc.Job.FirstOrDefault(x => x.id == m_User.jobId).name;
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

            if (pwd.Text == "")
            {
                pwd.Focus();
                return;
            }

            if (m_User == null)
            {
                id.SelectAll();
                id.Focus();
                BathClass.printErrorMsg("该员工号不存在！");
                return;
            }
            if (IOUtil.GetMD5(pwd.Text) != m_User.password)
            {
                pwd.SelectAll();
                pwd.Focus();
                BathClass.printErrorMsg("密码不对!");
                return;
            }

            write_user();
            var dao = new DAO(connectionString);
            options = dao.get_options();
            this.Hide();
            //MainWindow mainWindow = new MainWindow();
            m_Window.ShowDialog();
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

        //读取新版本号
        private int read_version()
        {
            int version = -1;
            if (!File.Exists(@".\version.ini"))
                return version;

            using (StreamReader sr = new StreamReader(@".\version.ini"))
            {
                string line;
                if ((line = sr.ReadLine()) != null)
                    version = Convert.ToInt32(line.Trim().Replace(".", ""));
            }
            File.Delete(@".\version.ini");
            return version;
        }

        //将新登陆员工号写入本地配置文件
        private void write_user()
        {
            string new_id = id.Text.Trim();
            if (idList.Contains(new_id))
                idList.Remove(new_id);
            
            idList.Insert(0, new_id);
            using (StreamWriter sw = new StreamWriter(@".\config\users.ini"))
            {
                foreach (string user in idList)
                    sw.WriteLine(user);
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
                if  (connectionIP == "")
                    connectionIP = IOUtil.get_config_by_key(ConfigKeys.KEY_CONNECTION_IP);

                //return @"Data Source=hnhecun.vicp.cc\SQLEXPRESS,23;"
                //+ @"Initial Catalog=BathDB;"
                //+ @"User ID=sa;pwd=123";
                //return @"Data Source=" + "." + @"\SQLEXPRESS;"
                //+ @"Initial Catalog=BathDB;Persist Security Info=True;"
                //+ @"User ID=sa;pwd=123";
                return @"Data Source=" + connectionIP + @"\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";
            }
        }

        //取消
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
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        //输入内容
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            if (current_box == null)
                return;

            Button btn = sender as Button;
            current_box.Focus();

            current_box.Text += btn.Text;
            if (current_box.GetType() == typeof(ComboBox))
            {
                var cb = current_box as ComboBox;
                cb.SelectionStart = cb.Text.Length;
            }
            else if (current_box.GetType() == typeof(TextBox))
            {
                var cb = current_box as TextBox;
                cb.SelectionStart = cb.Text.Length;
            }
            
        }

        private void box_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
            current_box = sender as Control;
        }

        //清除
        private void benClear_Click(object sender, EventArgs e)
        {
            if (current_box == null)
                return;

            current_box.Text = "";
            current_box.Focus();
        }

        //回删
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (current_box.Text == "")
                return;

            current_box.Text = current_box.Text.Substring(0, current_box.Text.Length - 1);
            current_box.Focus();

            if (current_box.GetType() == typeof(ComboBox))
            {
                var cb = current_box as ComboBox;
                cb.SelectionStart = cb.Text.Length;
            }
            else if (current_box.GetType() == typeof(TextBox))
            {
                var cb = current_box as TextBox;
                cb.SelectionStart = cb.Text.Length;
            }
        }
    }
}
