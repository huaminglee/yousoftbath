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

namespace YouSoftBathBack
{
    public partial class StockForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Stock m_Stock = new Stock();
        private bool newStock = true;

        //构造函数
        public StockForm(BathDBDataContext dc, Stock stock)
        {
            db = dc;
            if (stock != null)
            {
                m_Stock = stock;
                newStock = false;
            }
            InitializeComponent();
        }

        //对话框载入
        private void StockSettingForm_Load(object sender, EventArgs e)
        {
            EnumComputers();
            if (!newStock)
            {
                name.Text = m_Stock.name;
                phone.Text = m_Stock.phone;
                note.Text = m_Stock.note;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_Stock.name = name.Text;
            m_Stock.note = note.Text;
            m_Stock.phone = phone.Text;

            var ips = new List<string>();
            foreach (var i in pcList.CheckedItems)
            {
                ips.Add(i.ToString());
            }
            m_Stock.ips = string.Join("|", ips.ToArray());
            if (newStock)
            {
                if (db.Stock.FirstOrDefault(x=>x.ips==m_Stock.ips) != null)
                {
                    BathClass.printErrorMsg("此ip地址对应电脑已经定义仓库，请重新选择!");
                    return;
                }
                db.Stock.InsertOnSubmit(m_Stock);
            }
            else
            {
                if (db.Stock.FirstOrDefault(x=>x.ips==m_Stock.ips && x.id!=m_Stock.id) != null)
                {
                    BathClass.printErrorMsg("此ip地址对应电脑已经定义仓库，请重新选择!");
                    return;
                }
            }

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        //获取计算机列表
        private void EnumComputers()
        {
            try
            {
                string local_ip = BathClass.get_local_ip();
                string ip_zone = local_ip.Substring(0, local_ip.LastIndexOf('.')+1);
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
                if (!newStock)
                {
                    string[] ips = m_Stock.ips.Split('|');
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

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void phone_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void pcList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < pcList.Items.Count; i++)
            {
                if (i != e.Index) // 不是单击的项
                {
                    pcList.SetItemCheckState(i, System.Windows.Forms.CheckState.Unchecked); //设置单选核心代码
                }

            }
            string SelectedValue = pcList.Items[e.Index].ToString().Trim();//获取选定的值
        }
    }
}
