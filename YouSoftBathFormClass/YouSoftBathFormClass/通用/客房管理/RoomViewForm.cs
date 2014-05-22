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
    public partial class RoomViewForm : Form
    {
        //成员变量
        private string m_connectionString;
        private Thread m_thread;
        private Control cp;

        private static Color avi_color = Color.FromArgb(1, 175, 3);//空闲颜色
        private static Color in_color = Color.FromArgb(43, 76, 255);//入住颜色
        private static Color full_color = Color.FromArgb(198, 0, 0);//等待服务颜色
        //private static Color reserve_color = Color.FromArgb(128, 0, 128);//预约服务颜色
        //private static Color on_color = Color.FromArgb(255, 255, 0);//服务中颜色
        //private static Color fini_color = Color.FromArgb(255, 115, 39);//等待清洁颜色

        private int avi_number = 0;
        private int in_number = 0;
        private int full_number = 0;

        //构造函数
        public RoomViewForm()
        {
            m_connectionString = LogIn.connectionString;
            InitializeComponent();
        }

        //构造函数
        public RoomViewForm(string connectionString)
        {
            m_connectionString = connectionString;
            InitializeComponent();
        }

        //对话框载入
        private void RoomManagementForm_Load(object sender, EventArgs e)
        {
            cp = sp.Panel2;
            var db = new BathDBDataContext(m_connectionString);
            createRoomPanel(db);
            toolRoomNumber.Text = (avi_number + in_number + full_number).ToString();
            toolAviNumber.Text = avi_number.ToString();
            toolInNumber.Text = in_number.ToString();
            toolFullNumber.Text = full_number.ToString();
            //set_status(db);
            //btnAvi.BackColor = Color.FromArgb(1, 175, 3);
            //btnIn.BackColor = Color.FromArgb(43, 76, 255);
            //btnWait.BackColor = Color.FromArgb(198, 0, 0);
            //btnReserve.BackColor = Color.FromArgb(128, 0, 128);
            //btnOn.BackColor = Color.FromArgb(255, 255, 0); 
            //btnFini.BackColor = Color.FromArgb(255, 115, 39);

            toolAvi.BackColor = avi_color;
            toolIn.BackColor = in_color;
            toolFull.BackColor = full_color;
            //toolReserve.BackColor = Color.FromArgb(128, 0, 128);
            //toolServing.BackColor = Color.FromArgb(255, 255, 0);
            //toolFini.BackColor = Color.FromArgb(255, 115, 39);


            //m_thread = new Thread(new ThreadStart(update_ui));
            //m_thread.Start();
        }

        //刷新线程
        private void update_ui()
        {
            while (true)
            {
                try
                {
                    var db_new = new BathDBDataContext(m_connectionString);
                    foreach (var room in db_new.Room)
                    {
                        if (room.population == 1)
                        {
                            var btn = (Button)cp.Controls.Find(room.id.ToString(), false).FirstOrDefault();
                            this.Invoke(new delegate_set_btn_status(set_btn_status),
                                    new object[] { btn, room, 0 });
                        }
                        else
                        {
                            for (int i = 0; i < room.population; i++)
                            {
                                var btn = (Button)cp.Controls.Find(room.id.ToString()+"-"+(i+1).ToString(), false).FirstOrDefault();
                                this.Invoke(new delegate_set_btn_status(set_btn_status),
                                        new object[] { btn, room, i });
                            }
                        }
                    }
                    //this.Invoke(new delegate_set_status(set_status), new object[] { db_new });
                }
                catch
                {
                }
            }
        }

        private delegate void delegate_set_status(BathDBDataContext dc);

        private delegate void delegate_set_btn_status(Button btn, Room room, int i);

        private void set_btn_status(Button btn, Room room, int i)
        {
            btn.Text = getBtnString(room, i);
            btn_status(btn, room, i);
        }

        /*private void set_status(BathDBDataContext dc)
        {
            toolRoomNumber.Text = toolAviNumber.Text = toolFullNumber.Text = toolReserveNumber.Text =
                toolServingNumber.Text = toolFiniNumber.Text = toolInNumber.Text = "0";
            foreach (var room in dc.Room)
            {
                for (int i = 0; i < room.population; i++ )
                {
                    string status = "空闲";
                    try
                    {
                        status = room.status.Split('|')[i];
                    }
                    catch
                    {
                    }

                    toolRoomNumber.Text = (MConvert<int>.ToTypeOrDefault(toolRoomNumber.Text) + 1).ToString();
                    if (status == "空闲")
                        toolAviNumber.Text = (MConvert<int>.ToTypeOrDefault(toolAviNumber.Text) + 1).ToString();
                    else if (status == "入住")
                        toolInNumber.Text = (MConvert<int>.ToTypeOrDefault(toolInNumber.Text) + 1).ToString();
                    else if (status == "等待服务")
                        toolFullNumber.Text = (MConvert<int>.ToTypeOrDefault(toolFullNumber.Text) + 1).ToString();
                    else if (status == "预约服务")
                        toolReserveNumber.Text = (MConvert<int>.ToTypeOrDefault(toolReserveNumber.Text) + 1).ToString();
                    else if (status == "服务")
                        toolServingNumber.Text = (MConvert<int>.ToTypeOrDefault(toolServingNumber.Text) + 1).ToString();
                    else if (status == "等待清洁")
                        toolFiniNumber.Text = (MConvert<int>.ToTypeOrDefault(toolFiniNumber.Text) + 1).ToString();
                }
            }
        }*/

        //创建客房按钮
        private void createButton(int x, int y, Room room, int i, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);

            if (room.population == 1)
                btn.Name = room.id.ToString();
            else
                btn.Name = room.id.ToString() + "-" + (i+1).ToString();
            btn.Text = getBtnString(room, i);
            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 110);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn_status(btn, room, i);
            btn.Click+=new EventHandler(btn_Click);
            sp.Controls.Add(btn);
        }

        //创建客房按钮
        private void createButton(int x, int y, Room room, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);

            btn.Name = room.id.ToString();
            //btn.Text = getBtnString(room);
            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 80);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn_status(btn, room);
            btn.Click += new EventHandler(btn_Click);

            sp.Controls.Add(btn);
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string roomID=btn.Text.Split('\n')[0].Split(':')[1];
            BathClass.printInformation(GetSeatIdByRoomNo(roomID));
            
        }

        //获取button显示字符串
        //房间号、技师号、手牌、服务项目、服务时间
        private string getBtnString(Room room, int i)
        {
            int roomId = room.id;
            string str = "房间:" + room.name;
            if (room.population != 1)
                str += "-" + (i+1).ToString();
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

        //获取button显示字符串
        private void btn_status(Button btn, Room room)
        {
            int roomId = room.id;
            string str = "房间:" + room.name;
            int pop = room.population;
            str += "\n可住:" + pop.ToString() + "人";
            int number_in = 0;

            string status = "空闲";
            for (int i = 0; i < pop; i++ )
            {
                status = "空闲";
                try
                {
                    status = room.status.Split('|')[i];
                }
                catch
                {
                }
                if (status=="入住"||status=="等待服务"||status=="预约服务"||status=="服务"||status=="等待清洁")
                {
                    number_in++;
                }
            }
            str += "\n已住:" + number_in.ToString() + "人";
            btn.Text = str;

            if (number_in == 0)
            {
                btn.BackColor = avi_color;
                avi_number++;
            }
            else if (number_in != 0 && number_in < pop)
            {
                btn.BackColor = in_color;
                in_number++;
            }
            else if (number_in == pop)
            {
                btn.BackColor = full_color;
                full_number++;
            }


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
                btn.BackColor = Color.FromArgb(255,255,0);
            else if (status == "等待清洁")
                btn.BackColor = Color.FromArgb(255, 115, 39);
        }

        //创建客房面板
        private void createRoomPanel(BathDBDataContext db)
        {
            cp.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;

            var rooms = db.Room.ToList();
            int count = rooms.Count;
            while (index < count)
            {
                var room = rooms[index];
                if ((col + 1) * 160 >= cp.Size.Width)
                {
                    row++;
                    col = 0;
                }
                int x = col * 140 + 20 * (col + 1);
                int y = row * 80 + 20 * (row + 1);
                createButton(x, y, rooms[index], cp);
                col++;
                index++;
            }
            //while (index < count)
            //{
            //    var room = rooms[index];
            //    for (int i = 0; i < room.population; i++ )
            //    {
            //        if ((col + 1) * 160 >= pr.Size.Width)
            //        {
            //            row++;
            //            col = 0;
            //        }
            //        int x = col * 140 + 20 * (col + 1);
            //        int y = row * 110 + 20 * (row + 1);
            //        createButton(x, y, rooms[index], i, pr);
            //        col++;
            //    }
            //    index++;
            //}
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
            else if (e.KeyCode == Keys.Enter)
                btnFind_Click(null, null);
        }

        private void RoomViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }

        //查询手牌所在房间
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

            Room room = null;
            var rooms = db.Room.Where(x => x.seat.Contains(t));
            foreach (var r in rooms)
            {
                try
                {
                    var seatIds = r.seat.Split('|').ToList();
                    var status = r.status.Split('|').ToList();
                    int i = seatIds.IndexOf(t);
                    if (i != -1 && status[i] != "空闲" && status[i] != "null")
                    {
                        room = r;
                        break;
                    }
                }
                catch
                {

                }
            }

            //if (room != null)
            //    return room.name;
            //else
            //    return "";

            //var room = db.Room.FirstOrDefault(x => x.seatId != null && x.seatId.Contains(t));
            if (room == null)
            {
                BathClass.printInformation("未找到手牌号");
            }
            else
            {
                BathClass.printInformation("手牌:" + t + "，在房间：" + room.name);
            }
            seatId.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGetSeatIdByRoomNo_Click(object sender, EventArgs e)
        {
            string RoomID = txtBoxRoomId.Text;
            if (RoomID == "")
            {
                BathClass.printWarningMsg("需要输入房间号!");
                return;
            }
            string seatID = GetSeatIdByRoomNo(RoomID);
            BathClass.printInformation(seatID);
           
        }

        private string GetSeatIdByRoomNo(string roomID)
        {
            string temp = "";
            int sum = 0;
            StringBuilder sb = new StringBuilder();         
            var db = new BathDBDataContext(LogIn.connectionString);
            var db_Room = db.Room.FirstOrDefault(x => x.name == roomID);
            if (db_Room == null)
            {
                temp = "房间号不存在，请重试!";
                return temp;
            }
            else
            {
                string sts = db_Room.seat;
                if (sts == null || sts == "")
                {
                    temp = "该房间没有手牌!";
                    return temp;
                }
                else
                {
                    string[] st = sts.Split('|');
                    for (int i = 0; i < st.Length; i++)
                    {
                        if (st[i].Trim() != "")
                        {
                            sb.Append(st[i].Trim());
                            sb.Append("  ");
                            if ((i+1) % 4 == 0)
                                sb.Append("\n");
                            sum++;
                        }
                    }
                    if (sum == 0)
                    {
                        temp = "该房间没有手牌!";
                        return temp;
                    }
                    else
                    {
                        temp = "房间" + roomID + "中的手牌有" + sum + "个.手牌号如下:\n" + sb.ToString();
                        return temp;
                    }

                }
            }

        }
    }
}
