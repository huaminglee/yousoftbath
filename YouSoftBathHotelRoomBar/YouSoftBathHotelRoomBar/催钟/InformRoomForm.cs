using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public partial class InformRoomForm : Form
    {
        //成员变量
        private BathDBDataContext db;

        //构造函数
        public InformRoomForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void RoomManagementForm_Load(object sender, EventArgs e)
        {
            createRoomPanel();
            btnAvi.BackColor = Color.FromArgb(1, 175, 3);
            btnIn.BackColor = Color.FromArgb(43, 76, 255);
            btnWait.BackColor = Color.FromArgb(255, 255, 0);
            btnReserve.BackColor = Color.FromArgb(128, 0, 128);
            btnOn.BackColor = Color.FromArgb(198, 0, 0);
            btnFini.BackColor = Color.FromArgb(255, 115, 39);
        }

        //创建客房按钮
        private void createButton(int x, int y, Room room, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = room.name;
            btn.Text = getBtnString(room);
            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 110);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn_status(btn, room);
            btn.Click += new System.EventHandler(btn_Click);

            sp.Controls.Add(btn);
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var strs = btn.Text.Split('\n');
            var str = strs.FirstOrDefault(x => x.Contains("房间"));
            var roomId = str.Split(':')[1];

            var room = db.Room.FirstOrDefault(x => x.name == roomId);
            if (room.status != "服务")
            {
                BathClass.printErrorMsg("客房不在服务状态，不能催钟");
                return;
            }

            if (BathClass.printAskMsg("确定催钟?") != DialogResult.Yes)
                return;

            var roomCall = new RoomCall();
            roomCall.roomId = roomId;
            roomCall.read = false;
            roomCall.msg = "催钟";

            db.RoomCall.InsertOnSubmit(roomCall);
            db.SubmitChanges();
            this.Close();
        }

        //获取button显示字符串
        private string getBtnString(Room room)
        {
            int roomId = room.id;
            string str = "房间:" + room.name;
            
            if (room.status == "入住")
            {
                //int minute = (int)(DateTime.Now - room.openTime.Value).TotalMinutes;
                //str += "\n已住:" + minute.ToString() + "分钟";

                //if (room.orderTechId != null && room.orderTechId != "")
                //    str += "\n技师:" + room.orderTechId;
            }
            else if (room.status == "等待服务")
            {
                //int minute = (int)(DateTime.Now - room.orderTime.Value).TotalMinutes;
                //str += "\n已等:" + minute.ToString() + "分钟";

                //if (room.orderTechId != null && room.orderTechId != "")
                //    str += "\n技师:" + room.orderTechId;
            }
            else if (room.status == "预约服务")
            {
                //int minute = (int)(DateTime.Now - room.reserveTime.Value).TotalMinutes;
                //str += "\n已住:" + minute.ToString() + "分钟";

                //if (room.reserveId != null && room.reserveId != "")
                //    str += "\n技师:" + room.reserveId;

                //if (room.reserveTime.HasValue)
                //    str += "\n预约:" + room.reserveTime.Value.ToString("HH:mm");

            }
            else if (room.status == "服务")
            {
                //str += "\n技师:" + room.techId;
                //str += "\n时间:" + room.serverTime.ToString();

                //int minute = (int)(room.serverTime - (DateTime.Now - room.startTime.Value).TotalMinutes);
                //str += "\n还剩:" + minute.ToString() + "分钟";

            }
            else if (room.status == "等待清洁")
            {
                //if (room.techId != null)
                //    str += "\n技师:" + room.techId;
                //if (room.startTime != null && room.serverTime != null)
                //    str += "\n下钟:" + (room.startTime.Value.AddMinutes(room.serverTime.Value)).ToString("HH:mm");
            }

            return str;
        }

        //生成按钮状态
        private void btn_status(Button btn, Room room)
        {
            if (room.status == "空闲")
                btn.BackColor = Color.FromArgb(1, 175, 3);
            else if (room.status == "入住")
                btn.BackColor = Color.FromArgb(43, 76, 255);
            else if (room.status == "等待服务")
                btn.BackColor = Color.FromArgb(255,255,0);
            else if (room.status == "预约服务")
                btn.BackColor = Color.FromArgb(128, 0, 128);
            else if (room.status == "服务")
                btn.BackColor = Color.FromArgb(198, 0, 0);
            else if (room.status == "等待清洁")
                btn.BackColor = Color.FromArgb(255, 115, 39);
        }

        //创建客房面板
        private void createRoomPanel()
        {
            pr.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;

            var tLst = db.Room.ToList();
            int count = tLst.Count;
            while (index < count)
            {
                while ((col + 1) * 160 < pr.Size.Width && index < count)
                {
                    int x = col * 140 + 20 * (col + 1);
                    int y = row * 110 + 20 * (row + 1);
                    createButton(x, y, tLst[index], pr);
                    col++;
                    index++;
                }
                col = 0;
                row++;
            }
        }

        //刷新客房面板
        public void updateRoomPanel()
        {
            var tLst = db.Room.ToList();
            for (int i = 0; i < tLst.Count; i++ )
            {
                Button btn = (Button)pr.Controls[i];
                Room room = tLst[i];
                btn_status(btn, room);
                btn.Text = getBtnString(room);
            }
        }

        //退出
        private void toolLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //绑定快捷键
        private void RoomViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
