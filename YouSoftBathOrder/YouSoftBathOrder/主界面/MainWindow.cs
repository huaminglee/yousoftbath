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
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathOrder
{
    public partial class MainWindow : Form
    {
        //成员变量
        private static string connectionIP = "";

        //构造函数
        public MainWindow()
        {
            InitializeComponent();
        }

        //对话框载入
        private void OrderMainForm_Load(object sender, EventArgs e)
        {
            read_connection_IP();
            if (connectionIP == "")
            {
                PCListForm pCListForm = new PCListForm();
                if (pCListForm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                connectionIP = pCListForm.ip;
                write_connection_IP();
            }

            var db = new BathDBDataContext(connectionString);

            if (!db.DatabaseExists())
            {
                BathClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                PCListForm pCListForm = new PCListForm();
                if (pCListForm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
            }
        }

        //消费查看
        private void BtnExpense_Click(object sender, EventArgs e)
        {
            List<int> sList = new List<int>();
            sList.Add(2);
            sList.Add(6);

            InputSeatForm inputSeatForm = new InputSeatForm(sList);
            if (inputSeatForm.ShowDialog() == DialogResult.OK)
            {
                //修改
                //OrderCheckForm seatExpenseForm = new OrderCheckForm(inputSeatForm.m_Seat, connectionString);
                //seatExpenseForm.ShowDialog();
            }
        }

        //自动点单
        private void ExpenseOrder_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(connectionString);
            InputEmployee inputEmployee = new InputEmployee(db);
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            Employee server = inputEmployee.emplyee;

            if (!BathClass.getAuthority(db, server, "完整点单") &&
                !BathClass.getAuthority(db, server, "可见本人点单"))
            {
                BathClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            List<int> sList = new List<int>();
            sList.Add(2);
            sList.Add(6);

            InputSeatForm inputSeatForm = new InputSeatForm(sList);
            if (inputSeatForm.ShowDialog() != DialogResult.OK)
                return;

            //var m_Seat = db.Seat.FirstOrDefault(x => x.Equals(inputSeatForm.m_Seat));
            //m_Seat.ordering = true;
            //db.SubmitChanges();

            //修改
            //OrderForm orderForm = new OrderForm(inputSeatForm.m_Seat, server, connectionString, true);
            //orderForm.ShowDialog();

            //m_Seat.ordering = false;
            //db.SubmitChanges();
        }

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //获取连接IP
        private void read_connection_IP()
        {
            if (!Directory.Exists(@".\config"))
                Directory.CreateDirectory(@".\config");

            if (!File.Exists(@".\config\connectionIP.ini"))
            {
                using (FileStream fs = new FileStream(@".\config\connectionIP.ini", FileMode.Create))
                    return;
            }
            using (StreamReader sr = new StreamReader(@".\config\connectionIP.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    connectionIP = line.Trim();
                }
            }
        }

        //写入连接IP
        private void write_connection_IP()
        {
            using (StreamWriter sw = new StreamWriter(@".\config\connectionIP.ini", false))
            {
                sw.WriteLine(connectionIP);
            }
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

        //录单汇总
        private void BtnInputOrders_Click(object sender, EventArgs e)
        {
            var db_new = new BathDBDataContext(connectionString);
            InputEmployee inputEmployee = new InputEmployee(db_new);
            if (inputEmployee.ShowDialog() != DialogResult.OK)
                return;

            if (!BathClass.getAuthority(db_new, inputEmployee.emplyee, "录单汇总"))
            {
                BathClass.printErrorMsg("不具有权限");
                return;
            }

            TableOrderTableForm orderTableForm = new TableOrderTableForm(connectionString, inputEmployee.emplyee);
            orderTableForm.ShowDialog();
        }
    }
}
