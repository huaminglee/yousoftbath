using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace YouSoftBathPay
{
    public partial class MainWindow : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public SqlConnection thisConnection = new SqlConnection(
            global::YouSoftBathPay.Properties.Settings.Default.BathDBConnectionString);

        //构造函数
        public MainWindow(BathDBDataContext dc)
        {
            db = dc == null ? new BathDBDataContext() : dc;
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
