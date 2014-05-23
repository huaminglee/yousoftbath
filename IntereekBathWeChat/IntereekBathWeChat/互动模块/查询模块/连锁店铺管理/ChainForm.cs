using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftBathGeneralClass;
using YouSoftUtil;

namespace IntereekBathWeChat
{
    public partial class ChainForm : Form
    {
        private List<string> companyCodes = new List<string>();

        public ChainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var code_str = IOUtil.get_config_by_key(ConfigKeys.KEY_COMPANY_CODE);
            companyCodes = code_str.Split(Constants.SplitChar).ToList();
            dgv_show();
        }

        private void dgv_show()
        {
            DgvStores.Rows.Clear();
            int i = 1;
            foreach (var c in companyCodes)
            {
                if (c == "")
                    continue;

                DgvStores.Rows.Add(i, c);
                i++;
            }
        }

        //添加
        private void BTAdd_Click(object sender, EventArgs e)
        {
            var code = TextCode.Text.Trim();
            if (code == "")
            {
                BathClass.printErrorMsg("需要输入内容!");
                return;
            }
            
            if (companyCodes.Contains(code))
            {
                BathClass.printErrorMsg("已经包含该店铺，不能重复添加！");
                return;
            }

            //需要连接服务器检验code是否存在!


            companyCodes.Add(code);
            IOUtil.set_config_by_key(ConfigKeys.KEY_COMPANY_CODE, string.Join(Constants.SplitChar.ToString(), companyCodes.ToArray()));
            dgv_show();
        }

        //店铺注册
        private void BTRegister_Click(object sender, EventArgs e)
        {
            var form = new ChainRegisterForm();
            form.ShowDialog();
        }

    }
}
