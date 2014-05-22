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

namespace YouSoftBathReception
{
    public partial class BarMsgForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public BarMsgForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void ReturnedBillTableForm_Load(object sender, EventArgs e)
        {
            startTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";
            endTime.CustomFormat = "yyyy-MM-dd-HH:mm:ss";

            var ct = db.ClearTable.OrderByDescending(x => x.clearTime).FirstOrDefault();
            if (ct == null)
                startTime.Value = DateTime.Now.AddDays(-2);
            else
                startTime.Value = ct.clearTime;

            dgv_show();
        }

        //显示信息
        private void dgv_show()
        {
            dgv.DataSource = from x in db.BarMsg
                             where x.time >= startTime.Value && x.time <= endTime.Value
                             orderby x.time descending
                             select new
                             {
                                 编号 = x.id,
                                 时间 = x.time,
                                 包厢 = x.roomId,
                                 手牌 = x.seatId,
                                 信息 = x.msg
                             };
            BathClass.set_dgv_fit(dgv);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F3:
                    dgv_show();
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }
    }
}
