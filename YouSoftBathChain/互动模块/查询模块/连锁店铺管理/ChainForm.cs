using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftUtil;
using YouSoftUtil.Shop;
using YouSoftBathConstants;

namespace IntereekBathWeChat
{
    public partial class ChainForm : Form
    {
        private List<string> companies = new List<string>();

        public ChainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var code_str = IOUtil.get_config_by_key(ConfigKeys.KEY_COMPANY_CODE);
            companies = code_str.Split(Constants.BIG_SPLITCHAR).ToList();
            dgv_show();
        }

        private void dgv_show()
        {
            DgvStores.Rows.Clear();
            int i = 1;
            foreach (var c in companies)
            {
                if (StringUtil.isEmpty(c))
                    continue;

                var cArray = c.Split(Constants.SplitChar);
                DgvStores.Rows.Add(i, cArray[0], cArray[1], cArray[2], cArray[3]);
                i++;
            }
        }

        //添加
        private void BTAdd_Click(object sender, EventArgs e)
        {
            var code = TextCode.Text.Trim();
            if (code == "")
            {
                MessageBox.Show("需要输入内容!");
                return;
            }

            if (!StringUtil.isEmpty(companies.FirstOrDefault(x => x.Split(Constants.SplitChar)[0] == code)))
            {
                MessageBox.Show("已经包含该店铺，不能重复添加！");
                return;
            }

            //需要连接服务器检验code是否存在!
            string errorDesc = "";
            var customer = ShopManagement.queryCustomer(MainForm.ip, code, out errorDesc);
            if (customer == null)
            {
                if (!StringUtil.isEmpty(errorDesc))
                    MessageBox.Show(errorDesc);
                else
                {
                    MessageBox.Show("该店铺代码不存在，请确认!");
                    TextCode.SelectAll();
                    TextCode.Focus();
                }
                return;
            }

            //companyCodes.Add(code);
            companies.Add(joinCustomer(customer));
            IOUtil.set_config_by_key(ConfigKeys.KEY_COMPANY_CODE, string.Join(Constants.BIG_SPLITCHAR.ToString(), companies.ToArray()));
            dgv_show();
            TextCode.Text = "";
            TextCode.Focus();
        }

        private string joinCustomer(Customer customer)
        {
            return customer.companyCode + Constants.SplitChar + customer.companyName + Constants.SplitChar + customer.companyTel + Constants.SplitChar + customer.companyAdd;
        }
    }
}
