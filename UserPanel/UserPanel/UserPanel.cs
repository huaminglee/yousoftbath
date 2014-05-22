﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UserPanel
{
    public partial class UserPanel : UserControl
    {
        public UserPanel()
        {
            InitializeComponent();
        }

        private void setImg(string imgUrl)
        {
            userIcon.BackgroundImage =
                        Image.FromStream(System.Net.WebRequest.Create(imgUrl).GetResponse().GetResponseStream());
        }

        private void setNickName(string nickName)
        {
            LBNick.Text = nickName;
        }

        private void setContent(string content)
        {
            LBContent.Text = content;
        }

        public void setUserInfo(string imgUrl, string nickName, string content)
        {
            setImg(imgUrl);
            setNickName(nickName);
            setContent(content);
        }

        public int getImgHeight()
        {
            return userIcon.Height;
        }

        public void FitSize(int w)
        {
            this.Size = new Size(w, userIcon.Height + 20);
            userIcon.Location = new Point(5, 5);

            LBNick.Location = new Point(userIcon.Location.X + userIcon.Width, userIcon.Location.Y + 10);
            LBContent.Location = new Point(userIcon.Location.X + userIcon.Width, userIcon.Location.Y + userIcon.Height - LBContent.Height - 10);
        }

        //定义委托
        public delegate void CtClickHandle(object sender, EventArgs e);
        //定义事件
        public event CtClickHandle UserControlBtnClicked;

        private void UserPanel_Click(object sender, EventArgs e)
        {
            if (UserControlBtnClicked != null)
                UserControlBtnClicked(this, new EventArgs());//把按钮自身作为参数传递
        }
    }
}