using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathReception
{
    public partial class SMmsForm : Form
    {
        //构造函数
        public SMmsForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MemberSettingForm_Load(object sender, EventArgs e)
        {
            port.SelectedIndex = 0;
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
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            String TypeStr = "";
            String CopyRightToCOM = "";
            String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";
            if (SmsClass.Sms_Connection(CopyRightStr, (uint)(port.SelectedIndex + 1), uint.Parse(baud.Text), out TypeStr, out CopyRightToCOM) != 1)
            {
                BathClass.printErrorMsg("设置出错，请重试!");
                return;
            }

            SmsClass.Sms_Disconnection();
            BathClass.set_config_by_key("smsPort", port.Text);
            BathClass.set_config_by_key("smsBaud", baud.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void baud_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
