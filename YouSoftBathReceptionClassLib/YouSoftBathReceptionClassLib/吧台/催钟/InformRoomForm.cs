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
        private string m_con_str;

        //构造函数
        public InformRoomForm(string con_str)
        {
            m_con_str = con_str;
            InitializeComponent();
        }
        //构造函数
        public InformRoomForm(string con_str, bool full_screen)
        {
            m_con_str = con_str;
            InitializeComponent();
            if (full_screen)
                this.FormBorderStyle = FormBorderStyle.None;
        }

        //对话框载入
        private void RoomManagementForm_Load(object sender, EventArgs e)
        {
            int w_this = this.Width;
            int w_btn = btnCancel.Width;
            btnCancel.Location = new Point((w_this - w_btn) / 2, btnCancel.Location.Y);
            db = new BathDBDataContext(m_con_str);
            createRoomPanel();
            //btnAvi.BackColor = Color.FromArgb(1, 175, 3);
            //btnIn.BackColor = Color.FromArgb(43, 76, 255);
            //btnWait.BackColor = Color.FromArgb(198, 0, 0);
            //btnReserve.BackColor = Color.FromArgb(128, 0, 128);
            //btnOn.BackColor = Color.FromArgb(255, 255, 0);
            //btnFini.BackColor = Color.FromArgb(255, 115, 39);

            toolAvi.BackColor = Color.FromArgb(1, 175, 3);
            toolIn.BackColor = Color.FromArgb(43, 76, 255);
            toolWait.BackColor = Color.FromArgb(198, 0, 0);
            toolReserve.BackColor = Color.FromArgb(128, 0, 128);
            toolServing.BackColor = Color.FromArgb(255, 255, 0);
            toolFini.BackColor = Color.FromArgb(255, 115, 39);
        }

        //创建客房按钮
        private void createButton(int x, int y, Room room, int i, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            if (room.population == 1)
                btn.Name = room.id.ToString();
            else
                btn.Name = room.id.ToString() + "-" + (i + 1).ToString();
            btn.Text = getBtnString(room, i);

            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 110);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn_status(btn, room, i);
            btn.Click += new System.EventHandler(btn_Click);

            sp.Controls.Add(btn);
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var strs = btn.Text.Split('\n');
            var str = strs.FirstOrDefault(x => x.Contains("房间"));
            var roomId_str = str.Split(':')[1];
            var room_str = roomId_str.Split('-');
            var roomId = room_str[0];
            var room = db.Room.FirstOrDefault(x => x.name == roomId);

            int index = 0;
            if (room.population != 1)
                index = Convert.ToInt32(room_str[1]) - 1;

            var status = room.status.Split('|');
            if (status.Length <= index || status[index] != "服务")
            {
                BathClass.printErrorMsg("客房不在服务状态，不能催钟");
                return;
            }

            if (BathClass.printAskMsg("确定催钟?") != DialogResult.Yes)
                return;

            var roomCall = new RoomCall();
            roomCall.roomId = roomId;
            roomCall.seatId = room.seat.Split('|')[index];
            roomCall.read = false;
            roomCall.msg = "催钟";

            db.RoomCall.InsertOnSubmit(roomCall);
            db.SubmitChanges();

            this.Close();
        }

        //获取button显示字
        //房间号、技师号、手牌、服务项目、服务时间
        private string getBtnString(Room room, int i)
        {
            int roomId = room.id;
            string str = "房间:" + room.name;
            if (room.population != 1)
                str += "-" + (i + 1).ToString();
            string status = "空闲";
            string seat = "";
            try
            {
                status = room.status.Split('|')[i];
                seat = room.seat.Split('|')[i];
            }
            catch
            {
            }

            if (status == "入住")
            {
                str += "\n手牌:" + seat;
                try
                {
                    var openTime = Convert.ToDateTime(room.openTime.Split('|')[i]);
                    int minute = (int)(DateTime.Now - openTime).TotalMinutes;
                    str += "\n已住:" + minute.ToString() + "分";

                    if (room.orderTechId != null)
                    {
                        var orderTechId = room.orderTechId.Split('|')[i];
                        str += "\n技师:" + orderTechId;
                    }
                }
                catch
                {
                }
            }
            else if (status == "等待服务")
            {
                str += "\n手牌:" + seat;
                try
                {
                    var orderTime = Convert.ToDateTime(room.orderTime.Split('|')[i]);
                    int minute = (int)(DateTime.Now - orderTime).TotalMinutes;
                    str += "\n已等:" + minute.ToString() + "分";

                    if (room.orderTechId != null)
                    {
                        var orderTechId = room.orderTechId.Split('|')[i];
                        if (orderTechId == "")
                        {
                            orderTechId = room.selectId.Split('|')[i];
                        }
                        str += "\n技师:" + orderTechId;
                    }
                }
                catch
                {
                }
            }
            else if (status == "预约服务")
            {
                str += "\n手牌:" + seat;
                try
                {
                    var reserveTime = Convert.ToDateTime(room.reserveTime.Split('|')[i]);
                    int minute = (int)(DateTime.Now - reserveTime).TotalMinutes;
                    str += "\n预约:" + reserveTime.ToString("HH:mm");
                    str += "\n已约:" + minute.ToString() + "分";

                    if (room.reserveId != null)
                    {
                        var reserveId = room.reserveId.Split('|')[i];
                        str += "\n技师:" + reserveId;
                    }
                }
                catch
                {
                }
            }
            else if (status == "服务")
            {
                str += "\n手牌:" + seat;
                try
                {
                    str += "\n技师:" + room.techId.Split('|')[i];
                    str += "\n时间:" + room.serverTime.Split('|')[i];

                    int serverTime = Convert.ToInt32(room.serverTime.Split('|')[i]);
                    var st = Convert.ToDateTime(room.startTime.Split('|')[i]);
                    int minute = (int)(serverTime - (DateTime.Now - st).TotalMinutes);
                    str += "\n还剩:" + minute.ToString() + "分";
                }
                catch
                {
                }

            }
            else if (status == "等待清洁")
            {
                str += "\n手牌:" + seat;
                try
                {
                    if (room.techId != null)
                        str += "\n技师:" + room.techId.Split('|')[i];
                    if (room.startTime != null && room.serverTime != null)
                    {
                        var st = Convert.ToDateTime(room.startTime.Split('|')[i]);
                        var l = Convert.ToInt32(room.serverTime.Split('|')[i]);
                        str += "\n下钟:" + (st.AddMinutes(l)).ToString("HH:mm");
                    }
                }
                catch
                {
                }
            }

            return str;
        }

        //生成按钮状态
        private void btn_status(Button btn, Room room, int i)
        {
            string status = "空闲";
            try
            {
                status = room.status.Split('|')[i];
            }
            catch
            {
            }

            if (status == "空闲")
                btn.BackColor = Color.FromArgb(1, 175, 3);
            else if (status == "入住")
                btn.BackColor = Color.FromArgb(43, 76, 255);
            else if (status == "等待服务")
                btn.BackColor = Color.FromArgb(198, 0, 0);
            else if (status == "预约服务")
                btn.BackColor = Color.FromArgb(128, 0, 128);
            else if (status == "服务")
                btn.BackColor = Color.FromArgb(255, 255, 0);
            else if (status == "等待清洁")
                btn.BackColor = Color.FromArgb(255, 115, 39);
        }


        //创建客房面板
        private void createRoomPanel()
        {
            pr.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;

            var rooms = db.Room.ToList();
            int count = rooms.Count;
            while (index < count)
            {
                var room = rooms[index];
                for (int i = 0; i < room.population; i++)
                {
                    if ((col + 1) * 160 >= pr.Size.Width)
                    {
                        row++;
                        col = 0;
                    }
                    int x = col * 140 + 20 * (col + 1);
                    int y = row * 110 + 20 * (row + 1);
                    createButton(x, y, rooms[index], i, pr);
                    col++;
                }
                index++;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
