using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MsgPanel
{
    public partial class LogPanel : UserControl
    {
        private int _id;
        Color urgent_color = Color.OrangeRed;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public LogPanel()
        {
            InitializeComponent();
        }

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(
        IntPtr hWnd,
        int Msg,
        int wParam,
        int lParam
        );

        /// <summary>
        /// 设置发帖日期
        /// </summary>
        /// <param name="dt"></param>
        public void setDate(DateTime dt)
        {
            TextSendDate.Text = dt.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 设置发帖人
        /// </summary>
        /// <param name="sender"></param>
        public void setSender(string sender)
        {
            TextSender.Text = sender;
        }

        /// <summary>
        /// 设置呈交部门
        /// </summary>
        /// <param name="toDepart"></param>
        public void setToDepart(string toDepart)
        {
            TextToDepart.Text = toDepart;
        }

        /// <summary>
        /// 设置帖子内容，需要根据Msg宽度分割Msg，实现自动换行
        /// </summary>
        /// <param name="msg"></param>
        public void setMsg(string msg)
        {
            if (msg == null || msg =="")
            {
                PanelMsg.Visible = false;
            }
            else
            {
                TextMsg.Text = msg;
                int EM_GETLINECOUNT = 0x00BA;//获取总行数的消息号 
                int lc = SendMessage(TextMsg.Handle, EM_GETLINECOUNT, 0, 0);
                int sf = this.TextMsg.Font.Height * (lc + 1) + this.TextMsg.Location.Y + 5;
                if (sf > TextMsg.Height)
                {
                    this.TextMsg.ClientSize = new Size(TextMsg.Width, sf);
                }
                else
                {
                    this.TextMsg.ClientSize = new Size(TextMsg.Width, TextMsg.Height);
                }
            }
            

            int height_img = 0;
            if (PanelImg.Visible)
                height_img = PanelImg.Height + 10;

            int height_msg = 0;
            if (PanelMsg.Visible)
                height_msg = TextMsg.Height + TextMsg.Location.Y * 2;
            this.Height =  height_msg+ PanelHead.Height + PanelUser.Height + height_img;
        }

        public void setImg(byte[] img1, byte[] img2,byte[] img3)
        {
            if (img1 != null)
            {
                Picture1.BackgroundImage = ReturnPhoto(img1);
                if (img2 != null)
                {
                    Picture2.BackgroundImage = ReturnPhoto(img2);
                    if (img3 != null)
                        Picture3.BackgroundImage = ReturnPhoto(img3);
                }
                else if (img3 != null)
                {
                    Picture2.BackgroundImage = ReturnPhoto(img3);
                }
            }
            else if (img2 != null)
            {
                Picture1.BackgroundImage = ReturnPhoto(img2);
                if (img3 != null)
                    Picture2.BackgroundImage = ReturnPhoto(img3);
            }
            else if (img3 != null)
            {
                Picture1.BackgroundImage = ReturnPhoto(img3);
            }
            else
                PanelImg.Visible = false;
        }

        public void setImg(string img1Url, string img2Url, string img3Url)
        {
            try
            {
                if (img1Url != null)
                {
                    Picture1.BackgroundImage = 
                        Image.FromStream(System.Net.WebRequest.Create(img1Url).GetResponse().GetResponseStream());

                    if (img2Url != null)
                    {
                        Picture2.BackgroundImage =
                            Image.FromStream(System.Net.WebRequest.Create(img2Url).GetResponse().GetResponseStream());
                        if (img3Url != null)
                        {
                            Picture3.BackgroundImage =
                                Image.FromStream(System.Net.WebRequest.Create(img3Url).GetResponse().GetResponseStream());
                        }
                    }
                    else if (img3Url != null)
                    {
                        Picture2.BackgroundImage =
                            Image.FromStream(System.Net.WebRequest.Create(img3Url).GetResponse().GetResponseStream());
                    }
                }
                else if (img2Url != null)
                {
                    Picture1.BackgroundImage = 
                        Image.FromStream(System.Net.WebRequest.Create(img2Url).GetResponse().GetResponseStream());

                    if (img3Url != null)
                    {
                        Picture2.BackgroundImage =
                            Image.FromStream(System.Net.WebRequest.Create(img3Url).GetResponse().GetResponseStream());
                    }
                }
                else if (img3Url != null)
                {
                    Picture1.BackgroundImage = 
                        Image.FromStream(System.Net.WebRequest.Create(img3Url).GetResponse().GetResponseStream());
                }
                else 
                    PanelImg.Visible=false;

            }
            catch (System.Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        //1.参数是Byte[]类型，返回值是Image对象: 
        public System.Drawing.Image ReturnPhoto(byte[] streamByte)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(streamByte);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            return img;
        }


        #region 设置时间事件
        //定义委托
        public delegate void BtnSetTimeClickHandle(object sender, EventArgs e);
        //定义事件
        public event BtnSetTimeClickHandle BtnSetTimeClicked;
        //设定时间
        private void BtnSetTime_Click(object sender, EventArgs e)
        {
            if (BtnSetTimeClicked != null)
                BtnSetTimeClicked(this, new EventArgs());//把按钮自身作为参数传递
        }
        #endregion

        #region 加急事件
        //定义委托
        public delegate void BtnUrgentClickHandle(object sender, EventArgs e);
        //定义事件
        public event BtnUrgentClickHandle BtnUrgentClicked;
        //加急
        private void BtnUrgent_Click(object sender, EventArgs e)
        {
            if (BtnUrgentClicked != null)
                BtnUrgentClicked(this, new EventArgs());//把按钮自身作为参数传递
        }
        #endregion

        #region 已完成事件
        //定义委托
        public delegate void BtnDoneClickHandle(object sender, EventArgs e);
        //定义事件
        public event BtnDoneClickHandle BtnDoneClicked;
        //已完成
        private void BtnDone_Click(object sender, EventArgs e)
        {
            if (BtnDoneClicked != null)
                BtnDoneClicked(this, new EventArgs());//把按钮自身作为参数传递
        }
        #endregion

        /// <summary>
        /// 设置Panel颜色，绿色为完成状态，红色为加急状态，白色为普通状态
        /// </summary>
        /// <param name="done">标示事件是否完成</param>
        /// <param name="urgent">标示事件是否加急</param>
        private void set_panel_color(bool done, bool urgent)
        {
            if (done)
            {
                PanelImg.BackColor = Color.LightGreen;
                TextMsg.BackColor = Color.LightGreen;
                PanelImg.BackColor = Color.LightGreen;
                PanelMsg.BackColor = Color.LightGreen;

                BtnDone.Text = "未完成";
            }
            else
            {
                if (urgent)
                {
                    PanelImg.BackColor = urgent_color;
                    TextMsg.BackColor = urgent_color;
                    PanelImg.BackColor = urgent_color;
                    PanelMsg.BackColor = urgent_color;

                    BtnUrgent.Text = "取消加急";
                }
                else 
                {
                    PanelImg.BackColor = Color.White;
                    TextMsg.BackColor = Color.White;
                    PanelImg.BackColor = Color.White;
                    PanelMsg.BackColor = Color.White;

                    BtnUrgent.Text = "加急";
                }
                

                BtnDone.Text = "已完成";
            }
            
        }

        public void set_btnDone_text(string txt)
        {
            BtnDone.Text = txt;
        }

        public void set_btnUrgent_text(string txt)
        {
            BtnUrgent.Text = txt;
        }

        /// <summary>
        /// 设置截止日期
        /// </summary>
        /// <param name="dt">戒指日期，可为null</param>
        public void set_Due_text(DateTime? dt)
        {
            if (dt == null)
            {
                LabelDue.Visible = false;
                LabelDueLabel.Visible = false;
                return;
            }

            LabelDue.Visible = true;
            LabelDueLabel.Visible = true;
            LabelDue.Text = dt.Value.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 设置加急或者完成日期
        /// </summary>
        /// <param name="dt">dt为加急或者完成日期，可为null</param>
        private void set_urgent_text(string label_txt, System.Nullable<DateTime> dt)
        {
            if (dt == null)
            {
                LabelUrgentLabel.Visible = false;
                LabelUrgent.Visible = false;
                return;
            }

            LabelUrgentLabel.Visible = true;
            LabelUrgent.Visible = true;
            LabelUrgentLabel.Text = label_txt;
            LabelUrgent.Text = dt.Value.ToString("yyyy-MM-dd HH:mm");
        }

        public void set_panel_status(bool done, bool urgent, DateTime? doneDate, DateTime? urgentDate, DateTime? dueTime)
        {
            if (done)
                set_urgent_text("完成:", doneDate);
            else if (urgent)
                set_urgent_text("加急:", urgentDate);
            else
                set_urgent_text("", null);

            set_Due_text(dueTime);
            set_panel_color(done, urgent);
        }
    }
}
