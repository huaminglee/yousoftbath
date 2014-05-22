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
using YouSoftUtil;
using YouSoftUtil.Shop;

namespace IntereekBathWeChat
{
    public partial class ChainRegisterForm : Form
    {
        private List<string> companyCodes = new List<string>();

        public ChainRegisterForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var code_str = IOUtil.get_config_by_key(ConfigKeys.KEY_COMPANY_CODE);
            companyCodes = code_str.Split(Constants.SplitChar).ToList();
        }

        private void BTRegister_Click(object sender, EventArgs e)
        {
            string code = TextCode.Text.Trim();
            if (code == "")
            {
                BathClass.printErrorMsg("公司代码为空!");
                return;
            }

            string name = TextName.Text.Trim();
            if (name == "")
            {
                BathClass.printErrorMsg("公司名称为空!");
                return;
            }

            string errorDesc = "";
            var success = ShopManagement.registerCompany(Constants.AliIP, code, name, TextTel.Text.Trim(), TextTel.Text.Trim(), out errorDesc);

            if (success)
            {
                var db = new BathDBDataContext(LogIn.connectionString);
                db.Options.FirstOrDefault().company_Code = code;
                db.SubmitChanges();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                BathClass.printErrorMsg(errorDesc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
