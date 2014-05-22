using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace YouSoftBathWatcher
{
    public partial class PCListForm : Form
    {
        private List<string> ipList = new List<string>();
        public string ip = "";

        //构造函数
        public PCListForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void PCListForm_Load(object sender, EventArgs e)
        {
        }

        //创建计算机列表
        private void createPCList()
        {
            pcList.Controls.Clear();
            EnumComputers();
            int x = 10, y = 30;
            foreach (string ip in ipList)
            {
                createRadioBtn(x, y, ip, pcList);
                y += 30;
            }
        }

        //创建单选按钮
        private void createRadioBtn(int x, int y, string txt, Control sp)
        {
            RadioButton rBtn = new RadioButton();
            rBtn.AutoSize = true;
            rBtn.Location = new System.Drawing.Point(x, y);
            rBtn.Name = txt;
            //rBtn.Size = new System.Drawing.Size(95, 16);
            rBtn.TabIndex = 0;
            rBtn.TabStop = true;
            rBtn.Text = txt;
            rBtn.UseVisualStyleBackColor = true;
            rBtn.CheckedChanged += new System.EventHandler(this.rBtn_CheckedChanged);

            sp.Controls.Add(rBtn);
        }

        //选择服务器
        private void rBtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rBtn = sender as RadioButton;
            if (rBtn.Checked)
                ipAddress.Text = rBtn.Text;
        }

        //获取计算机列表
        private void EnumComputers()
        {
            try
            {
                for (int i = 0; i <= 255; i++)
                {
                    Ping myPing;
                    myPing = new Ping();
                    myPing.PingCompleted += new PingCompletedEventHandler(myPing_PingCompleted);

                    string pingIP = "192.168.1." + i.ToString();
                    myPing.SendAsync(pingIP, 1000, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void myPing_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success && !ipList.Contains(e.Reply.Address.ToString()))
            {
                ipList.Add(e.Reply.Address.ToString());
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            ip = ipAddress.Text;
            if (ip != "")
                this.DialogResult = DialogResult.OK;
        }

        private void PCListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            EnumComputers();
            createPCList();
        }

        private void ipAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == (char)8 || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }
    }
}
