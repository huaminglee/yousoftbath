using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using YouSoftUtil;
using YouSoftBathFormClass;
using YouSoftBathGeneralClass;
using YouSoftBathConstants;

namespace YouSoftBathSelf
{
    public partial class MainWindow : Form
    {
        //成员变量
        private static string connectionIP = "";
        private BathDBDataContext db;

        //构造函数
        public MainWindow()
        {
            InitializeComponent();
        }

        //对话框载入
        private void OrderMainForm_Load(object sender, EventArgs e)
        {
            connectionIP = IOUtil.get_config_by_key(ConfigKeys.KEY_CONNECTION_IP);
            if (connectionIP == "")
            {
                PCListForm pCListForm = new PCListForm();
                if (pCListForm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                connectionIP = pCListForm.ip;
                IOUtil.set_config_by_key(ConfigKeys.KEY_CONNECTION_IP, connectionIP);
            }

            db = new BathDBDataContext(connectionString);

            if (!db.DatabaseExists())
            {
                BathClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                connectionIP = "";
                IOUtil.set_config_by_key(ConfigKeys.KEY_CONNECTION_IP, connectionIP);
                this.Close();
                return;
            }
        }

        //消费查看
        private void BtnExpense_Click(object sender, EventArgs e)
        {
            List<int> sList = new List<int>();
            sList.Add(2);
            sList.Add(6);
            sList.Add(7);

            InputSeatForm inputSeatForm = new InputSeatForm(sList, connectionString);
            if (inputSeatForm.ShowDialog() == DialogResult.OK)
            {
                //修改
                //OrderCheckForm seatExpenseForm = new OrderCheckForm(inputSeatForm.m_Seat, connectionString);
                //seatExpenseForm.ShowDialog();
            }
        }

        //技师查看
        private void BtnTechnician_Click(object sender, EventArgs e)
        {
            //TechnicianSeclectForm technicianSeclectForm = new TechnicianSeclectForm();
            //technicianSeclectForm.ShowDialog();
        }

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //返回连接字符串
        public static string connectionString
        {
            get
            {
                return @"Data Source=" + connectionIP + @"\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";
            }
        }

        //会员自助结账
        private void btnPay_Click(object sender, EventArgs e)
        {
            List<int> sList = new List<int>();
            sList.Add(2);
            sList.Add(6);
            sList.Add(7);

            InputSeatForm inputSeatForm = new InputSeatForm(sList, connectionString);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            SeatExpenseForm seatExpenseForm = new SeatExpenseForm(inputSeatForm.m_Seat);
            seatExpenseForm.ShowDialog();
        }
    }
}
