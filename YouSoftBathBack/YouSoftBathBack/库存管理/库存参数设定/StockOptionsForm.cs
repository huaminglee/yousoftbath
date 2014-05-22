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

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class StockOptionsForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Stock stockMain = null;

        //构造函数
        public StockOptionsForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void StockSettingForm_Load(object sender, EventArgs e)
        {
            var stocks = db.Stock.Select(x => x.name);
            stockList.Items.AddRange(stocks.ToArray());
            stockMain = db.Stock.FirstOrDefault(x => x.main != null && Convert.ToBoolean(x.main));
            if (stockMain != null)
            {
                for (int i = 0; i < stockList.Items.Count; i++ )
                {
                    if (stockList.Items[i].ToString() == stockMain.name)
                    {
                        stockList.SetItemChecked(i, true);
                        break;
                    }
                }
            }
            EnumComputers();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            string mainStock = "";
            foreach (var i in stockList.CheckedItems)
                mainStock = i.ToString();

            if (mainStock == "")
                return;

            foreach(var s in db.Stock)
            {
                s.main = null;
            }

            var stock = db.Stock.FirstOrDefault(x => x.name == mainStock);
            stock.main = true;
            
            var ips = new List<string>();
            foreach (var i in pcList.CheckedItems)
            {
                ips.Add(i.ToString());
            }
            stock.ips = string.Join("|", ips.ToArray());
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        //获取计算机列表
        private void EnumComputers()
        {
            try
            {
                string local_ip = BathClass.get_local_ip();
                string ip_zone = local_ip.Substring(0, local_ip.LastIndexOf('.') + 1);
                for (int i = 0; i <= 255; i++)
                {
                    Ping myPing;
                    myPing = new Ping();
                    myPing.PingCompleted += new PingCompletedEventHandler(myPing_PingCompleted);

                    string pingIP = ip_zone + i.ToString();
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
            if (e.Reply.Status == IPStatus.Success && !pcList.Items.Contains(e.Reply.Address.ToString()))
            {
                pcList.Items.Add(e.Reply.Address.ToString());
                if (stockMain != null)
                {
                    string[] ips = stockMain.ips.Split('|');
                    for (int i = 0; i < pcList.Items.Count; i++)
                    {
                        if (ips.Contains(pcList.Items[i].ToString()))
                            pcList.SetItemChecked(i, true);
                    }
                }
            }
        }

        //设置快捷键
        private void StockSettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        //选择主要仓库
        private void stockList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            int index = e.Index;
            for (int i = 0; i < stockList.Items.Count; i++)
            {
                if (i != index)
                    stockList.SetItemChecked(i, false);
            }
            var stockName = stockList.Items[index].ToString();
            var stock = db.Stock.FirstOrDefault(x => x.name == stockName);
            var ips = stock.ips.Split('|');
            for (int i = 0; i < pcList.Items.Count; i++ )
            {
                if (ips.Contains(pcList.Items[i].ToString()))
                    pcList.SetItemChecked(i, true);
                else
                    pcList.SetItemChecked(i, false);
            }
        }
    }
}
