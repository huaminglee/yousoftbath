using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class MemberFingerForm : Form
    {
        //成员变量
        private DAO dao;
        public CCardInfo m_member = null;
        private bool m_verified_finger = false;


        public bool verified
        {
            get { return m_verified_finger; }
        }

        //构造函数
        public MemberFingerForm(CCardInfo member)
        {
            InitializeComponent();
            try
            {
                dao = new DAO(LogIn.connectionString);
                m_member = member;
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        //对话框载入
        private void MemberCardUsingForm_Load(object sender, EventArgs e)
        {
            int rt = axZKFPEngX1.InitEngine();
            if (rt == 1)
            {
                BathClass.printErrorMsg("指纹识别驱动程序加载失败");
                return;
            }
            else if (rt == 2)
            {
                BathClass.printErrorMsg("没有连接指纹识别仪");
                return;
            }
            else if (rt == 3)
            {
                BathClass.printErrorMsg("属性SensorIndex指定的指纹仪不存在");
                return;
            }

            if (axZKFPEngX1.IsRegister)
            {
                axZKFPEngX1.CancelEnroll();
            }
            if (m_member.CI_Password == null || m_member.CI_Password == "")
                axZKFPEngX1.BeginEnroll();
            else
                axZKFPEngX1.BeginCapture();

            LableInfo.Text = "登记指纹，请放手指";
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void axZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            m_member.CI_Password = Convert.ToBase64String((byte[])e.aTemplate);
            if (!dao.execute_command("update [CardInfo] set CI_Password='"+m_member.CI_Password+"' where CI_CardNo='"+m_member.CI_CardNo+"'"))
            {
                m_member.CI_Password = null;
                BathClass.printErrorMsg("将指纹数据写入数据库失败，请重试!");
            }
            else
            {
                m_verified_finger = true;
                axZKFPEngX1.EndEngine();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void axZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            string strTemp = "指纹质量";
            if (e.aQuality == 0)
            {
                strTemp = strTemp + "合格";
            }
            else
            {
                if (e.aQuality == 1)
                {
                    strTemp = strTemp + "特征点不够";
                }
                else
                    strTemp = strTemp + "不合格";
            }
            if (axZKFPEngX1.IsRegister)
                if (axZKFPEngX1.EnrollIndex != 1)
                    strTemp = strTemp + ",请再按 " + (axZKFPEngX1.EnrollIndex - 1).ToString() + "次指纹";
                else
                    strTemp = strTemp + ",登记成功";

            LableInfo.Text = strTemp;
        }

        private void axZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            int handle1 = g.GetHdc().ToInt32();
            axZKFPEngX1.PrintImageAt(handle1, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            pictureBox1.Image = bmp;
        }

        private void axZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            if (m_member.CI_Password != null && m_member.CI_Password != "")
            {
                bool RegChanged = false;
                string tmp = axZKFPEngX1.GetTemplateAsString();

                object obj1 = null;
                bool rt = axZKFPEngX1.DecodeTemplate(m_member.CI_Password, ref obj1);

                object obj2 = null;
                rt = axZKFPEngX1.DecodeTemplate(tmp, ref obj2);

                rt = axZKFPEngX1.VerFinger(ref obj1, obj2, false, ref RegChanged);
                axZKFPEngX1.EndEngine();
                m_verified_finger = rt;

                this.DialogResult = DialogResult.OK;

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            axZKFPEngX1.EndEngine();
            if (m_member.CI_Password == null || m_member.CI_Password == "")
                m_verified_finger = true;
        }
    }
}
