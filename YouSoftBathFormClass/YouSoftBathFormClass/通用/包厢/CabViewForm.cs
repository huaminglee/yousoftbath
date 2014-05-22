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
using System.Data.SqlClient;

using System.Timers;
using System.Threading;

namespace YouSoftBathFormClass
{
    public partial class CabViewForm : Form
    {
        private List<string> all_rooms;//手牌状态
        private List<int> all_rooms_id;//手牌id
        private static System.Timers.Timer updateTimer;//刷新线程


        //构造函数
        public CabViewForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void RoomManagementForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            createRoomPanel(db);

            all_rooms = db.Room.Select(x => x.status).ToList();
            all_rooms_id = db.Room.Select(x => x.id).ToList();

            updateTimer = new System.Timers.Timer(100);
            updateTimer.Elapsed += new ElapsedEventHandler(updateTimer_Elapsed);
            updateTimer.Start();
        }

        //刷新线程
        private void updateTimer_Elapsed(object sender, EventArgs e)
        {
            updateTimer.Stop();
            var db_new = new BathDBDataContext(LogIn.connectionString);
            var rooms_tmp = db_new.Room.Select(x => x.status).ToList();

            bool changed = false;
            for (int i = 0; i < rooms_tmp.Count; i++)
            {
                if (rooms_tmp[i] != all_rooms[i])
                {
                    var btn = pr.Controls.Find(all_rooms_id[i].ToString(), false).FirstOrDefault();
                    var btns = btn as Button;

                    var room_tmp = db_new.Room.FirstOrDefault(x => x.id == all_rooms_id[i]);
                    btn.Text = getBtnString(room_tmp);
                    changed = true;
                }
            }

            if (changed)
            {
                all_rooms = rooms_tmp;
            }

            updateTimer.Start();
        }


        //创建客房按钮
        private void createButton(int x, int y, Room room, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = room.id.ToString();
            btn.Text = getBtnString(room);
            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 100);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn.BackColor = Color.LightGreen;
            btn.Click += new System.EventHandler(btn_Click);
            btn.TabStop = false;
            btn_status(room, btn);

            sp.Controls.Add(btn);
        }

        private void btn_status(Room room, Button btn)
        {
            int cur_pop = 0;
            var ids = room.seatIds;
            if (ids != null)
                cur_pop = ids.Split('|').Length;
            int pop = room.population;
            if (cur_pop == 0)
                btn.BackColor = Color.LightGreen;
            else if (cur_pop != 0 && cur_pop < pop)
                btn.BackColor = Color.Cyan;
            else if (cur_pop != 0 && cur_pop == pop)
                btn.BackColor = Color.Red;
            
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            CabPopForm form = new CabPopForm(btn.Name);
            form.ShowDialog();

            BathDBDataContext db_new = new BathDBDataContext(LogIn.connectionString);
            var room = db_new.Room.FirstOrDefault(x => x.id.ToString() == btn.Name);
            btn.Text = getBtnString(room);
            btn_status(room, btn);
        }

        //获取button显示字符串
        //房间号、技师号、手牌、服务项目、服务时间
        private string getBtnString(Room room)
        {
            int roomId = room.id;
            string str = "房间:" + room.name;
            str += "\n可住:" + room.population.ToString() + "人";
            str += "\n" + room.seatIds;

            return str;
        }

        //创建客房面板
        private void createRoomPanel(BathDBDataContext db)
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
                    int y = row * 100 + 20 * (row + 1);
                    createButton(x, y, tLst[index], pr);
                    col++;
                    index++;
                }
                col = 0;
                row++;
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

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //查询
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (seatId.Text == "")
            {
                BathClass.printErrorMsg("需要输入手牌号!");
                return;
            }
            var t = seatId.Text;
            seatId.Text = "";

            var db = new BathDBDataContext(LogIn.connectionString);
            var room = db.Room.FirstOrDefault(x => x.seatIds != null && x.seatIds.Contains(t));
            if (room == null)
            {
                BathClass.printInformation("未找到手牌号");
            }
            else
            {
                BathClass.printInformation("手牌:" + t + "，在房间：" + room.name);
            }
        }

        private void seatId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnFind_Click(null, null);
        }

    }
}
