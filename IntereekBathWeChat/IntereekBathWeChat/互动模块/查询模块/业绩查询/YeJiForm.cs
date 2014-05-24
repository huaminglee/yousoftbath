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
using YouSoftUtil.Shop;
using YouSoftBathFormClass;

namespace IntereekBathWeChat
{
    public partial class YeJiForm : Form
    {
        private List<string> companyCodes = new List<string>();

        public YeJiForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var code_str = IOUtil.get_config_by_key(ConfigKeys.KEY_COMPANY_CODE);
            companyCodes = code_str.Split(Constants.SplitChar).ToList();

            DgvDateTotal.ColumnHeadersDefaultCellStyle.Font = new Font("SimSun", 11f);
            DgvDateStores.ColumnHeadersDefaultCellStyle.Font = new Font("SimSun", 11f);
            DgvMonthTotal.ColumnHeadersDefaultCellStyle.Font = new Font("SimSun", 11f);
            DgvMonthStores.ColumnHeadersDefaultCellStyle.Font = new Font("SimSun", 11f);

            DgvDateTotal.DefaultCellStyle.Font = new Font("SimSun", 13f);
            DgvDateStores.DefaultCellStyle.Font = new Font("SimSun", 13f);
            DgvMonthTotal.DefaultCellStyle.Font = new Font("SimSun", 13f);
            DgvMonthStores.DefaultCellStyle.Font = new Font("SimSun", 13f);

            int year = DateTime.Now.Year;
            while (year >= 2005)
            {
                CBYear.Items.Add(year);
                year--;
            }
            CBYear.SelectedIndex = 0;
            CBMonth.Text = DateTime.Now.Month.ToString().PadLeft(2, '0');

            companyCodes.RemoveAll(x => x.Trim() == "");
            if (companyCodes.Count == 0)
            {
                BathClass.printErrorMsg("未定义连锁店铺，请先到连锁店铺定义！");
                BTDateFind.Enabled = false;
                BTMonthFind.Enabled = false;
                return;
            }
        }

        private void BTAdd_Click(object sender, EventArgs e)
        {

        }

        //按日期查询
        private void BTDateFind_Click(object sender, EventArgs e)
        {
            string errorDesc = "";
            var shopYeJis = ShopManagement.queryYeJi(LogIn.connectionIP, companyCodes, DPDate.Value.ToString("yyyy-MM-dd"), "D", out errorDesc);
            if (shopYeJis == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            DgvDateStores.Rows.Clear();
            DgvDateTotal.Rows.Clear();

            DgvDateTotal.Rows.Add(
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.accountCash), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.accountbankUnion), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.cardSaleCash), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.cardSaleBankUnion), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.totalRevenue), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.creditCard), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.coupon), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.groupBuy), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.server), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.zero), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.wipeZero), 0));

            foreach (var shopYeJi in shopYeJis)
            {
                DgvDateStores.Rows.Add(
                shopYeJi.companyName,
                shopYeJi.accountCash,
                shopYeJi.accountbankUnion,
                shopYeJi.cardSaleCash,
                shopYeJi.cardSaleBankUnion,
                shopYeJi.totalRevenue,
                shopYeJi.creditCard,
                shopYeJi.coupon,
                shopYeJi.groupBuy,
                shopYeJi.server,
                shopYeJi.zero,
                shopYeJi.wipeZero);
            }
        }

        //按照月查询
        private void BTMonthFind_Click(object sender, EventArgs e)
        {
            string errorDesc = "";
            var shopYeJis = ShopManagement.queryYeJi(LogIn.connectionIP, companyCodes, CBYear.Text + "-" + CBMonth.Text, "M", out errorDesc);
            if (shopYeJis == null)
            {
                BathClass.printErrorMsg(errorDesc);
                return;
            }

            DgvMonthStores.Rows.Clear();
            DgvMonthTotal.Rows.Clear();

            DgvMonthTotal.Rows.Add(
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.accountCash), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.accountbankUnion), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.cardSaleCash), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.cardSaleBankUnion), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.totalRevenue), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.creditCard), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.coupon), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.groupBuy), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.server), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.zero), 0),
                MConvert<double>.ToTypeOrDefault(shopYeJis.Sum(x => x.wipeZero), 0));
            foreach (var shopYeJi in shopYeJis)
            {
                DgvMonthStores.Rows.Add(
                shopYeJi.companyName,
                shopYeJi.accountCash,
                shopYeJi.accountbankUnion,
                shopYeJi.cardSaleCash,
                shopYeJi.cardSaleBankUnion,
                shopYeJi.totalRevenue,
                shopYeJi.creditCard,
                shopYeJi.coupon,
                shopYeJi.groupBuy,
                shopYeJi.server,
                shopYeJi.zero,
                shopYeJi.wipeZero);
            }
        }

    }
}
