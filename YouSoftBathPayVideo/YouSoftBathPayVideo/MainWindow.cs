using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YouSoftUtil;
using System.Threading;
using System.Timers;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathConstants;

namespace YouSoftBathPayVideo
{
    public partial class MainWindow : Form
    {
        //成员变量
        private static string connectionIP = "";
        private BathDBDataContext db = null;
        private cVideo video;
        private bool recording = false;
        private DateTime recordTime = DateTime.MinValue;
        private TimeSpan ts = TimeSpan.Parse("00:00:30");
        private string dir = "";
        private static System.Timers.Timer watch_Timer;//监视器

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            connectionIP = IOUtil.get_config_by_key(ConfigKeys.KEY_CONNECTION_IP);
            if (connectionIP == "")
            {
                PCListForm pCListForm = new PCListForm();
                if (pCListForm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                connectionIP = pCListForm.ip;
                IOUtil.set_config_by_key(ConfigKeys.KEY_CONNECTION_IP, connectionIP);
            }

            db = new BathDBDataContext(connectionString);
            if (!db.DatabaseExists())
            {
                GeneralClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                connectionIP = "";
                IOUtil.set_config_by_key(ConfigKeys.KEY_CONNECTION_IP, connectionIP);
                this.Close();
                return;
            }

            dir = IOUtil.get_config_by_key(ConfigKeys.KEY_VIDEO_SAVE_DIR);
            if (dir == "")
                set_dir();

            if (dir == "")
            {
                this.Close();
                return;
            }
            if (!dir.EndsWith("\\"))
                dir += "\\";

            serverIp.Text = connectionIP;
            cacheDir.Text = dir;
            string pvs = IOUtil.get_config_by_key(ConfigKeys.KEY_VIDEO_TIMESPAN);
            if (pvs != "")
            {
                seconds.Text = pvs;
                ts = TimeSpan.Parse("00:00:" + pvs);
            }
            else
            {
                pvs = "30";
                seconds.Text = pvs;
                ts = TimeSpan.Parse("00:00:" + pvs);
                IOUtil.set_config_by_key(ConfigKeys.KEY_VIDEO_TIMESPAN, pvs);
            }

            db.PayMsg.DeleteAllOnSubmit(db.PayMsg.ToArray());
            db.SubmitChanges();
            video = new cVideo(panel1.Handle, panel1.Width, panel1.Height);
            if (video.StartWebCam())
            {
                video.get();
                video.Capparms.fYield = true;
                video.Capparms.fAbortLeftMouse = false;
                video.Capparms.fAbortRightMouse = false;
                video.Capparms.fCaptureAudio = false;
                video.Capparms.dwRequestMicroSecPerFrame = 0x9C40; // 设定帧率25fps： 1*1000000/25 = 0x9C40
                video.set();
            }

            watch_Timer = new System.Timers.Timer(100);
            watch_Timer.Elapsed += new ElapsedEventHandler(watch_Timer_Elapsed);
            watch_Timer.Start();
            watch_Timer.Enabled = true;
        }

        //每隔1秒监听一次
        private void watch_Timer_Elapsed(object sender, EventArgs e)
        {
            Thread.CurrentThread.IsBackground = true;
            BathDBDataContext dc = new BathDBDataContext(connectionString);
            if (dc.PayMsg.Count() != 0)
            {
                watch_Timer.Interval = ts.Seconds * 1000;
                PayMsg msg = dc.PayMsg.ToList().Last();
                string path = dir + GeneralClass.Now.ToShortDateString() + "\\" + msg.systemId + ".avi";
                
                recording = true;
                video.StartKinescope(path);
                recordTime = GeneralClass.Now;

                dc.PayMsg.DeleteAllOnSubmit(dc.PayMsg.ToArray());
                dc.SubmitChanges();
            }
            if (recording && GeneralClass.Now - recordTime >= ts)
            {
                watch_Timer.Interval = 100;
                recording = false;
                video.StopKinescope();
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
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //notifyIcon1.Visible = false;
            //this.ShowInTaskbar = true;
            //this.WindowState = FormWindowState.Normal;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IOUtil.set_config_by_key(ConfigKeys.KEY_VIDEO_TIMESPAN, seconds.Text);
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainWindow_SizeChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.ShowInTaskbar = false;
            //    this.notifyIcon1.Visible = true;
            //}
        }

        private void set_dir()
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dir != "")
                dlg.SelectedPath = dir;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                dir = dlg.SelectedPath;
                IOUtil.set_config_by_key(ConfigKeys.KEY_VIDEO_SAVE_DIR, dir);
            }
        }

    }
}
