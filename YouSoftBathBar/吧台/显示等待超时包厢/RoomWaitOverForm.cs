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
    public partial class RoomWaitOverForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private int wait_delay;

        //构造函数
        public RoomWaitOverForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void ReturnedBillTableForm_Load(object sender, EventArgs e)
        {
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            dgv.RowsDefaultCellStyle.Font = new Font("宋体", 20);
            wait_delay = db.Options.FirstOrDefault().包房等待时限.Value;
            dgv_show();
        }

        //显示信息
        private void dgv_show()
        {
            foreach (var room in db.Room)
            {
                if (room.status != null && room.status.Contains("等待服务"))
                {
                    var tmp_s = room.status.Split('|');
                    var tmp_o = room.orderTime.Split('|');
                    var tmp_e = room.seat.Split('|');
                    var tmp_d = room.orderTechId.Split('|');
                    for (int i = 0; i < tmp_s.Length; i++)
                    {
                        if (tmp_s[i] == "等待服务")
                        {
                            try
                            {
                                var st = Convert.ToDateTime(tmp_o[i]);
                                var tm = (int)(DateTime.Now - st).TotalMinutes;
                                if (tm >= wait_delay)
                                    dgv.Rows.Add(room.name, tmp_e[i], tmp_o[i], tmp_d[i], tm);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            BathClass.set_dgv_fit(dgv);
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                case Keys.Enter:
                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
