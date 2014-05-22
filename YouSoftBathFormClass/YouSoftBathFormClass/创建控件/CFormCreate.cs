using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using YouSoftBathGeneralClass;
using YouSoftBathConstants;

namespace YouSoftBathFormClass
{
    public class CFormCreate
    {
        //手牌大小控制
        private static int btn_size = 45;
        private static int btn_space = 13;
        
        public static void createSeat(BathDBDataContext dc, Control seatPanel, TabControl seatTab, EventHandler btn_click, ContextMenuStrip cm)
        {
            if (MConvert<bool>.ToTypeOrDefault(dc.Options.FirstOrDefault().台位类型分页显示,false))
            {
                seatPanel.Visible = false;
                seatTab.Visible = true;
                seatTab.Dock = DockStyle.Fill;

                foreach (var stype in dc.SeatType)
                {
                    TabPage tp = create_seat_page(stype.name, seatTab);
                    var seats = dc.Seat.Where(x => x.typeId == stype.id).OrderBy(x => x.text).ToList();
                    creat_seat_per_panel(seats, tp, btn_click, cm);
                }
            }
            else
            {
                if (dc.SeatType.Select(x => x.department).Distinct().Count() > 1)
                {
                    seatPanel.Visible = false;
                    seatTab.Visible = true;
                    seatTab.Dock = DockStyle.Fill;
                    TabPage tp = create_seat_page("桑拿部", seatTab);
                    var seatTypes = dc.SeatType.Where(x => x.department == "桑拿部").Select(x => x.id);
                    var seats = dc.Seat.Where(x => seatTypes.Contains(x.typeId)).OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                    creat_seat_per_panel(seats, tp, btn_click, cm);

                    tp = create_seat_page("客房部", seatTab);
                    seatTypes = dc.SeatType.Where(x => x.department == "客房部").Select(x => x.id);
                    seats = dc.Seat.Where(x => seatTypes.Contains(x.typeId)).OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                    creat_seat_per_panel(seats, tp, btn_click, cm);
                }
                else
                {
                    seatPanel.Visible = true;
                    seatTab.Visible = false;
                    seatPanel.Dock = DockStyle.Fill;

                    var seats = dc.Seat.OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                    creat_seat_per_panel(seats, seatPanel, btn_click, cm);
                }
            }
            
        }

        private static void creat_seat_per_panel(List<Seat> seats, Control sp, EventHandler click, ContextMenuStrip cm)
        {
            int tableCountSp = sp.Controls.Count;
            int tableCount = seats.Count;

            Size clientSize = sp.Size;
            int nR = (clientSize.Width - btn_space) / (btn_size + btn_space);
            int theRow = tableCountSp / nR;
            int theCol = tableCountSp - theRow * nR;
            int theCount = tableCountSp;

            while (theCount < tableCount)
            {
                while (theCol < nR && theCount < tableCount)
                {
                    if (theCount != 0 && seats[theCount].typeId != seats[theCount - 1].typeId)
                    {
                        theCol = 0;
                        theRow++;
                    }
                    int x = theCol * btn_size + btn_space * (theCol + 1);
                    int y = theRow * btn_size + btn_space * (theRow + 1);
                    createButton(x, y, seats[theCount], sp, click, cm);

                    theCount++;
                    theCol++;
                }
                theCol = 0;
                theRow++;
            }
        }
        
        //创建单个台位按钮
        private static void createButton(int x, int y, Seat table, Control sp, EventHandler btn_Click, ContextMenuStrip cm)
        {
            Button btn = new Button();

            Single bf = 13F;
            int l = table.text.Length;
            if (l == 3)
                bf = 13F;
            else if (l == 4)
                bf = 10f;

            btn.Font = new Font("SimSun", bf);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = table.id.ToString();
            btn.Text = table.text;
            btn.Size = new System.Drawing.Size(btn_size, btn_size);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            if (cm != null)
                btn.ContextMenuStrip = cm;
            btn.TabStop = false;
            btn.Click += btn_Click;
            btn_status(btn, table.status);

            sp.Controls.Add(btn);
        }

        //生成按钮状态
        private static void btn_status(Button btn, int status)
        {
            switch (status)
            {
                case 1://可用
                    btn.BackColor = Color.White;
                    break;
                case 2://正在使用
                    btn.BackColor = Color.Cyan;
                    break;
                case 3://已经结账
                    btn.BackColor = Color.Gray;
                    break;
                case 4://锁定
                    btn.BackColor = Color.Orange;
                    break;
                case 5://停用
                    btn.BackColor = Color.Red;
                    break;
                case 6://警告
                    btn.BackColor = Color.Yellow;
                    break;
                case 7://押金离场
                    btn.BackColor = Color.Violet;
                    break;
                case 8://重新结账
                    btn.BackColor = Color.CornflowerBlue;
                    break;
                default:
                    break;
            }
        }

        //添加单个手牌类型TabPage
        private static TabPage create_seat_page(string seatType, TabControl tab)
        {
            TabPage tabPage3 = new TabPage();

            tabPage3.Name = seatType;
            tabPage3.Text = seatType;
            tabPage3.UseVisualStyleBackColor = true;
            tabPage3.BackColor = Color.White;
            tabPage3.AutoScroll = true;
            tab.Controls.Add(tabPage3);

            return tabPage3;
        }

        #region 采用sqlcommand的方法创建

        /// <summary>
        /// 创建台位界面
        /// </summary>
        /// <param name="dao">数据库连接</param>
        /// <param name="options">选项</param>
        /// <param name="seatPanel">面板</param>
        /// <param name="seatTab">标签页</param>
        /// <param name="btn_click">响应回调函数</param>
        /// <param name="cm">右键菜单</param>
        /// <param name="department">部门：桑拿部、客房部</param>
        public static void createSeatByDao(DAO dao, COptions options, Control seatPanel, TabControl seatTab, 
            EventHandler btn_click, ContextMenuStrip cm, string department)
        {
            if (MConvert<bool>.ToTypeOrDefault(options.台位类型分页显示, false))
            {
                seatPanel.Visible = false;
                seatTab.Visible = true;
                seatTab.Dock = DockStyle.Fill;

                List<CSeatType> seat_types = new List<CSeatType>();
                if (department == null)
                    seat_types = dao.get_seattypes(null, null);
                else
                    seat_types = dao.get_seattypes("department", department);
                foreach (var stype in seat_types)
                {
                    TabPage tp = create_seat_page(stype.name, seatTab);
                    var seats = dao.get_seats("typeId", stype.id).OrderBy(x => x.text).ToList();
                    creat_seat_per_panel(seats, tp, btn_click, cm);
                }
            }
            else
            {
                seatPanel.Visible = true;
                seatTab.Visible = false;
                seatPanel.Dock = DockStyle.Fill;

                //var seatTypes = dao.get_seattypes("department", department).Select(x => x.id).ToList();
                List<CSeat> seats = new List<CSeat>();
                if (department == null)
                {
                    seats = dao.get_all_seats().OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                }
                else
                {
                    seats = dao.get_seats("typeid in (select id from seattype where department='" + department + "')").
                        OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                }
                
                //var seats = dao.get_seats(null, null).OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                creat_seat_per_panel(seats, seatPanel, btn_click, cm);

                //if (dao.has_hotel_department())
                //{
                //    seatPanel.Visible = false;
                //    seatTab.Visible = true;
                //    seatTab.Dock = DockStyle.Fill;
                //    TabPage tp = create_seat_page("桑拿部", seatTab);
                //    var seatTypes = dao.get_seattypes("name", "桑拿部").Select(x => x.id).ToList();
                //    var seats = dao.get_seats("typeId", seatTypes).OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                //    creat_seat_per_panel(seats, tp, btn_click, cm);

                //    tp = create_seat_page("客房部", seatTab);
                //    seatTypes = dao.get_seattypes("department", "客房部").Select(x => x.id).ToList();
                //    seats = dao.get_seats("typeId", seatTypes).OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                //    //seatTypes = dc.SeatType.Where(x => x.department == "客房部").Select(x => x.id);
                //    //seats = dc.Seat.Where(x => seatTypes.Contains(x.typeId)).OrderBy(x => x.typeId).ThenBy(x => x.text).ToList();
                //    creat_seat_per_panel(seats, tp, btn_click, cm);
                //}
                //else
                //{
                //}
            }

        }

        private static void creat_seat_per_panel(List<CSeat> seats, Control sp, EventHandler click, ContextMenuStrip cm)
        {
            int tableCountSp = sp.Controls.Count;
            int tableCount = seats.Count;

            Size clientSize = sp.Size;
            int nR = (clientSize.Width - btn_space) / (btn_size + btn_space);
            int theRow = tableCountSp / nR;
            int theCol = tableCountSp - theRow * nR;
            int theCount = tableCountSp;

            while (theCount < tableCount)
            {
                while (theCol < nR && theCount < tableCount)
                {
                    if (theCount != 0 && seats[theCount].typeId != seats[theCount - 1].typeId)
                    {
                        theCol = 0;
                        theRow++;
                    }
                    int x = theCol * btn_size + btn_space * (theCol + 1);
                    int y = theRow * btn_size + btn_space * (theRow + 1);
                    createButton(x, y, seats[theCount], sp, click, cm);

                    theCount++;
                    theCol++;
                }
                theCol = 0;
                theRow++;
            }
        }

        //创建单个台位按钮
        private static void createButton(int x, int y, CSeat table, Control sp, EventHandler btn_Click, ContextMenuStrip cm)
        {
            Button btn = new Button();

            Single bf = 13F;
            int l = table.text.Length;
            if (l == 3)
                bf = 13F;
            else if (l == 4)
                bf = 10f;

            btn.Font = new Font("SimSun", bf);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = table.id.ToString();
            btn.Text = table.text;
            btn.Size = new System.Drawing.Size(btn_size, btn_size);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            if (cm != null)
                btn.ContextMenuStrip = cm;
            btn.TabStop = false;
            btn.Click += btn_Click;
            btn_status(btn, table.status);

            sp.Controls.Add(btn);
        }

        //生成按钮状态
        private static void btn_status(Button btn, SeatStatus status)
        {
            switch (status)
            {
                case SeatStatus.AVILABLE://可用
                    btn.BackColor = Color.White;
                    break;
                case SeatStatus.USING://正在使用
                    btn.BackColor = Color.Cyan;
                    break;
                case SeatStatus.PAIED://已经结账
                    btn.BackColor = Color.Gray;
                    break;
                case SeatStatus.LOCKING://锁定
                    btn.BackColor = Color.Orange;
                    break;
                case SeatStatus.STOPPED://停用
                    btn.BackColor = Color.Red;
                    break;
                case SeatStatus.WARNING://警告
                    btn.BackColor = Color.Yellow;
                    break;
                case SeatStatus.DEPOSITLEFT://押金离场
                    btn.BackColor = Color.Violet;
                    break;
                case SeatStatus.REPAIED://重新结账
                    btn.BackColor = Color.CornflowerBlue;
                    break;
                default:
                    break;
            }
        }
        #endregion

    }
}
