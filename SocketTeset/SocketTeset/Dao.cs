using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketTeset;
using System.IO;
using System.Threading;
using System.Security.Cryptography;

namespace YouSoftBathWatcher
{
    public class Dao
    {
        private static string connectionString = @"Data Source=.\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";

        public static string send_log(string msg, string depart, string sender, 
            string img1Url,string img2Url,string img3Url)
        {
            string res = "T";
            try
            {
                var db = new BathDbDataContext(connectionString);

                var log = new DepartmentLog();
                log.date = DateTime.Now;
                log.departId = db.Department.FirstOrDefault(x => x.name == depart).id;
                log.msg = msg;
                if (sender != "F")
                    log.sender = db.Employee.FirstOrDefault(x => x.id == sender).name;

                if (img1Url != "F")
                    log.imgUrl = img1Url;
                if (img2Url != "F")
                    log.img2Url = img2Url;
                if (img3Url != "F")
                    log.img3Url = img3Url;

                db.DepartmentLog.InsertOnSubmit(log);
                db.SubmitChanges();
                res = log.id.ToString();
            }
            catch (System.Exception e)
            {
                res = "F";
            }

            return res;
        }

        public static string get_all_departs()
        {
            var db = new BathDbDataContext(connectionString);

            return get_result_str(db.Department.Select(x => x.name).ToList());
        }

        public static string set_log_urgent(string id, string urgent)
        {
            string str = "T";
            var db = new DBLayer(connectionString);

            if (!db.execute_command("update [DepartmentLog] set urgent=" + urgent + "  where id=" + id))
                str = "F";

            return str;
        }

        public static string set_log_done(string id, string done)
        {
            string str = "T";
            var db = new DBLayer(connectionString);

            if (!db.execute_command("update [DepartmentLog] set done=" + done + " where id=" + id))
                str = "F";

            return str;
        }

        //转换成bool
        private static bool ToBool(object obj)
        {
            if (obj == null)
                return false;

            bool bol = false;
            try
            {
                bol = Convert.ToBoolean(obj);
            }
            catch
            {
            }

            return bol;
        }

        public static string get_all_logs()
        {
            var db = new BathDbDataContext(connectionString);
            if (!db.DepartmentLog.Any())
                return "";

            var logs = db.DepartmentLog.OrderByDescending(x => x.date);

            List<string> info = new List<string>();
            foreach (var log in logs)
            {
                info.Add(log.id.ToString());
                info.Add(log.msg);
                if (ToBool(log.urgent))
                    info.Add("T");
                else
                    info.Add("F");

                if (ToBool(log.done))
                    info.Add("T");
                else
                    info.Add("F");

                info.Add(log.imgUrl);
                info.Add(log.img2Url);
                info.Add(log.img3Url);

                if (log.sender == null)
                    info.Add("匿名");
                else
                    info.Add(log.sender);

                info.Add(db.Department.FirstOrDefault(x => x.id == log.departId).name);

                info.Add(log.date.Value.ToString("yy-MM-dd HH:mm"));
            }

            return get_result_str(info);
        }

        //获取MD5加密字符串
        private static string GetMD5(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Unicode.GetBytes(input));
            return Convert.ToBase64String(res);
        }

        public static string login(string name, string pwd)
        {
            var db = new BathDbDataContext(connectionString);
            var user = db.Employee.FirstOrDefault(x => x.id == name);
            if (user == null)
                return "用户不存在";

            if (user.password != GetMD5(pwd))
                return "密码不正确";

            List<string> info = new List<string>();
            info.Add(user.id);
            info.Add(user.name);
            return get_result_str(info);
        }

        public static string get_seat_room(string seatId)
        {
            Room room = null;
            var db = new BathDbDataContext(connectionString);
            var rooms = db.Room.Where(x => x.seat.Contains(seatId));
            foreach (var r in rooms)
            {
                try
                {
                    var seatIds = r.seat.Split('|').ToList();
                    var status = r.status.Split('|').ToList();
                    int i = seatIds.IndexOf(seatId);
                    if (i != -1 && status[i] != "空闲" && status[i] != "null")
                    {
                        room = r;
                        break;
                    }
                }
                catch (System.Exception e)
                {

                }
            }

            if (room != null)
                return room.name;
            else
                return "";
        }

        //拆分套餐
        public static List<int> disAssemble(string menuIds)
        {
            List<int> menuIdList = new List<int>();

            if (menuIds == null || menuIds == "")
                return menuIdList;

            string[] ids = menuIds.Split(';');
            foreach (string menuId in ids)
            {
                if (menuId == "")
                    continue;
                menuIdList.Add(Convert.ToInt32(menuId));
            }

            return menuIdList;
        }

        private static string get_result_str(List<string> info)
        {
            string str = "";
            if (info.Count == 0)
                str = "null";
            else
                str = string.Join("$", info.ToArray());

            return str;
        }

        //获取所有技师预约记录
        public static string get_resv_techs(string techJob)
        {
            var info = new List<string>();

            var db = new BathDbDataContext(connectionString);
            var tr = db.TechReservation.Where(x => !x.proceeded.Value);
            if (techJob != "所有技师")
            {
                var job = db.Job.FirstOrDefault(x => x.name == techJob);
                var techs = db.Employee.Where(x => x.jobId == job.id).Select(x => x.id);
                tr = tr.Where(x => techs.Contains(x.techId));
            }
            foreach (var t in tr)
            {
                info.Add(t.techId);
                info.Add(t.seatId);
                info.Add(t.roomId);
                info.Add(t.time.Value.ToString("HH:mm"));
            }

            return get_result_str(info);
        }

        //获取最快下钟技师
        public static string get_fast_tech(string techJob)
        {
            var info = new List<string>();

            var db = new BathDbDataContext(connectionString);
            var jobs = db.Job.Where(x => x.name.Contains("技师"));

            if (techJob != "所有技师")
                jobs = jobs.Where(x => x.name == techJob);

            foreach (var job in jobs)
            {
                var techs = db.Employee.Where(x => x.jobId == job.id);
                techs = techs.Where((x => x.techStatus != null && x.techStatus != "空闲")).
                    OrderBy(x => x.serverTime - (DateTime.Now - x.startTime.Value).TotalMinutes);
                foreach (var tech in techs)
                {
                    var room = db.Room.FirstOrDefault(x => x.status.Contains("服务") && x.techId.Contains(tech.id));
                    if (room == null) continue;

                    info.Add(tech.id);
                    info.Add(room.name);
                    info.Add(tech.startTime.Value.ToString("HH:mm"));

                    var left = (tech.serverTime.Value - (DateTime.Now - tech.startTime.Value).TotalMinutes).ToString("0");
                    info.Add(left);

                    var rs = db.TechReservation.FirstOrDefault(x => x.techId == tech.id && !x.proceeded.Value);
                    if (rs == null)
                        info.Add("无");
                    else
                        info.Add(rs.roomId);
                }
            }

            return get_result_str(info);
        }

        //获取空闲技师
        public static string get_avi_techs(string techJob)
        {
            var info = new List<string>();

            var db = new BathDbDataContext(connectionString);
            var jobs = db.Job.Where(x => x.name.Contains("技师"));

            if (techJob != "所有技师")
                jobs = jobs.Where(x => x.name == techJob);

            foreach (var job in jobs)
            {
                var techs = db.Employee.Where(x => x.jobId == job.id && (x.techStatus == "空闲" || x.techStatus == null));
                foreach (var tech in techs)
                {
                    info.Add(tech.id);
                    info.Add(tech.gender);
                    info.Add(job.name);
                }
            }


            return get_result_str(info);
        }

        //获取是否需要输入单据号
        public static string get_input_billId()
        {
            var db = new BathDbDataContext(connectionString);
            var i = db.Options.FirstOrDefault().录单输入单据编号;
            if (i == null || !i.Value)
                return "false";
            else
                return "true";
        }

        //获取所有项目类别
        public static string get_cats()
        {
            var db = new BathDbDataContext(connectionString);
            return string.Join("$", db.Catgory.Select(x => x.name).ToArray());
        }

        //获取所有包厢
        public static string get_rooms()
        {
            List<string> infor = new List<string>();
            var db = new BathDbDataContext(connectionString);
            foreach (var c in db.Room)
            {
                int pop = c.population;
                if (pop == 1)
                {
                    infor.Add(c.name);
                    string sts = "空闲";
                    if (c.status != null)
                        sts = c.status.Split('|')[0];
                    infor.Add(sts);
                }
                else
                {
                    for (int i = 0; i < pop; i++)
                    {
                        infor.Add(c.name + "-" + (i + 1).ToString());
                        string sts = "空闲";
                        try
                        {
                            if (c.status != null)
                                sts = c.status.Split('|')[i];
                        }
                        catch
                        { }
                        infor.Add(sts);
                    }
                }
            }
            return get_result_str(infor);
        }

        public static string get_rooms_for_server()
        {
            StringBuilder sb = new StringBuilder();
            var db = new BathDbDataContext(connectionString);
            foreach (var room in db.Room)
            {
                sb.Append(room.name).Append("|").Append(room.population);

                int number_in = 0;
                int pop = room.population;
                string status = "空闲";
                for (int i = 0; i < pop; i++)
                {
                    status = "空闲";
                    try
                    {
                        status = room.status.Split('|')[i];
                    }
                    catch
                    {
                    }
                    if (status == "入住" || status == "等待服务" || status == "预约服务" || status == "服务" || status == "等待清洁")
                    {
                        number_in++;
                    }
                }

                sb.Append("|").Append(number_in);
                sb.Append("$");
            }

            return sb.ToString();
        }

        //获取所有包厢，仅仅获取包厢的名称，和包厢人数无关，用于平板上设置包厢号
        public static string get_all_rooms()
        {
            List<string> infor = new List<string>();
            var db = new BathDbDataContext(connectionString);
            return get_result_str(db.Room.Select(x=>x.name).ToList());
        }

        //获取所有包厢
        public static string get_company_name()
        {
            var db = new BathDbDataContext(connectionString);
            return db.Options.FirstOrDefault().companyName;
        }

        //获取某个Catgory对应的Menu对象
        public static string get_cat_menuObjects(string catName)
        {
            var menu_strs = new List<string>();
            var db = new BathDbDataContext(connectionString);
            var cat = db.Catgory.FirstOrDefault(x => x.name == catName);
            if (cat == null)
                return "false";

            var catId = cat.id;
            var menus = db.Menu.Where(x => x.catgoryId == catId);

            foreach (var m in menus)
            {
                menu_strs.Add(m.name);
                if (m.technician)
                    menu_strs.Add("true");
                else
                    menu_strs.Add("false");

                menu_strs.Add(m.price.ToString());
                menu_strs.Add(db.Catgory.FirstOrDefault(x => x.id == m.catgoryId).name);
            }

            return get_result_str(menu_strs);
        }

        //获取所有房间号
        public static string get_room(string room_id)
        {
            List<string> infor = new List<string>();
            var db = new BathDbDataContext(connectionString);
            var room = db.Room.FirstOrDefault(x => x.name == room_id);

            infor.Add(room_id);

            if (room.openTime == null)
                infor.Add("null");
            else
                infor.Add(room.openTime);

            if (room.seat == null)
                infor.Add("null");
            else
                infor.Add(room.seat);

            if (room.systemId == null)
                infor.Add("null");
            else
                infor.Add(room.systemId);

            if (room.orderTechId == null)
                infor.Add("null");
            else
                infor.Add(room.orderTechId);

            if (room.menu == null)
                infor.Add("null");
            else
                infor.Add(room.menu);

            if (room.techId == null)
                infor.Add("null");
            else
                infor.Add(room.techId);

            if (room.startTime == null)
                infor.Add("null");
            else
                infor.Add(room.startTime);

            if (room.serverTime == null)
                infor.Add("null");
            else
                infor.Add(room.serverTime.ToString());

            if (room.orderTime == null)
                infor.Add("null");
            else
                infor.Add(room.orderTime);

            if (room.hintPlayed == null)
                infor.Add("false");
            else
                infor.Add(room.hintPlayed);

            if (room.reserveId == null)
                infor.Add("null");
            else
                infor.Add(room.reserveId);

            if (room.reserveTime == null)
                infor.Add("null");
            else
                infor.Add(room.reserveTime);

            if (room.selectId == null)
                infor.Add("null");
            else
                infor.Add(room.selectId);

            infor.Add(room.status);
            infor.Add(room.population.ToString());
            return get_result_str(infor);
        }

        //提交保存房间信息
        public static string save_room(string infor_str)
        {
            try
            {
                string[] infor = infor_str.Split('#');
                //var db = new BathDbDataContext(connectionString);
                //var room = db.Room.FirstOrDefault(x => x.name == infor[0]);
                string cmd_str = @"update [Room] set status='"+infor[14]+"'";

                if (infor[1] != "null")
                {
                    cmd_str += ",openTime='" + infor[1] + "'";
                    //room.openTime = infor[1];
                }

                if (infor[2] != "null")
                    cmd_str += ",seat='" + infor[2] + "'";
                    //room.seat = infor[2];

                if (infor[3] != "null")
                    cmd_str += ",systemId='" + infor[3] + "'";
                    //room.systemId = infor[3];

                if (infor[4] != "null")
                    cmd_str += ",orderTechId='" + infor[4] + "'";
                    //room.orderTechId = infor[4];

                if (infor[5] != "null")
                    cmd_str += ",menu='" + infor[5] + "'";
                    //room.menu = infor[5];

                if (infor[6] != "null")
                    cmd_str += ",techId='" + infor[6] + "'";
                    //room.techId = infor[6];

                if (infor[7] != "null")
                    cmd_str += ",startTime='" + infor[7] + "'";
                    //room.startTime = infor[7];

                if (infor[8] != "null")
                    cmd_str += ",serverTime='" + infor[8] + "'";
                    //room.serverTime = infor[8];

                if (infor[9] != "null")
                    cmd_str += ",orderTime='" + infor[9] + "'";
                    //room.orderTime = infor[9];

                cmd_str += ",hintPlayed='" + infor[10] + "'";
                //room.hintPlayed = infor[10];

                if (infor[11] != "null")
                    cmd_str += ",reserveId='" + infor[11] + "'";
                    //room.reserveId = infor[11];

                if (infor[12] != "null")
                    cmd_str += ",reserveTime='" + infor[12] + "'";
                    //room.reserveTime = infor[12];

                if (infor[13] != "null")
                    cmd_str += ",selectId='" + infor[13] + "'";
                    //room.selectId = infor[13];

                //room.status = infor[14];
                //db.SubmitChanges();

                cmd_str += " where name='" + infor[0] + "'";
                var dbLayer = new DBLayer(connectionString);
                if (!dbLayer.execute_command(cmd_str))
                    return "false";

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //获取手牌消费
        public static string get_seat_orderObjects(string seatId)
        {
            List<string> infor = new List<string>();
            var db = new BathDbDataContext(connectionString);
            var seat = db.Seat.FirstOrDefault(x => x.text == seatId);
            if (seat == null || seat.systemId == null)
                return "手牌不存在";

            if (seat.status == 1)
                return "手牌不在使用中";

            if (seat.status == 3)
                return "手牌已结账";

            if (seat.status == 4)
                return "手牌已锁定";

            if (seat.status == 5)
                return "手牌已停用";

            if (seat.status == 8)
                return "手牌正在重新结账";

            if (seat.paying != null && seat.paying.Value)
                return "手牌正在结账";

            double total_money = 0;
            var orders = db.Orders.Where(x => x.systemId == seat.systemId && x.deleteEmployee == null && !x.paid);
            foreach (var order in orders)
            {
                infor.Add(order.id.ToString());
                infor.Add(order.menu);
                if (order.technician == null)
                    infor.Add("null");
                else
                    infor.Add(order.technician);

                infor.Add(order.number.ToString());

                double money = 0;
                if (order.priceType == null || order.priceType == "停止消费")
                    money = order.money;
                else if (order.priceType == "每小时")
                    money = (DateTime.Now - order.inputTime).TotalHours * order.money;

                total_money += money;
                infor.Add(money.ToString("0"));

                if (order.billId == null)
                    infor.Add("null");
                else
                    infor.Add(order.billId);
            }

            infor.Add(total_money.ToString("0"));
            return get_result_str(infor);
        }

        //点单
        //public static string set_seat_orderObjects(string seat_text, string infor, string user, string order_techId)
        public static string set_seat_orderObjects(string roomId, string seat_text, string infor, string user, string order_techId)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);
                var m_Seat = db.Seat.FirstOrDefault(x => x.text == seat_text);

                string[] strs = infor.Split('|');
                for (int i = 0; i < strs.Length; i = i + 7)
                {
                    int id = Convert.ToInt32(strs[i]);
                    string menu = strs[i + 1];
                    string tech = strs[i + 2];
                    double number = Convert.ToDouble(strs[i + 3]);
                    double money = Convert.ToDouble(strs[i + 4]);
                    string billId = strs[i + 5];
                    string st = strs[i + 6].ToString();

                    Orders order;
                    if (id == -1)
                    {
                        order = new Orders();

                        order.menu = menu;
                        order.text = seat_text;
                        order.systemId = m_Seat.systemId;
                        order.inputTime = DateTime.Now;
                        order.inputEmployee = user;
                        order.paid = false;

                        //if (roomId != "" && roomId != "null")
                        //    order.roomId = roomId;

                        if (tech != "" && tech != "null")
                        {
                            order.technician = tech;
                            if (order_techId != "null" && tech == order_techId)
                                order.techType = "点钟";
                        }

                        if (billId != "" && billId != "null")
                            order.billId = billId;

                        if (st != "null")
                            order.startTime = Convert.ToDateTime(st);
                    }
                    else
                        order = db.Orders.FirstOrDefault(x => x.id == id);

                    if (order == null)
                        continue;
                    order.number = number;
                    order.money = money;

                    if (id == -1)
                    {
                        var _order = db.Orders.FirstOrDefault(x => x.menu == order.menu &&
                            x.money == order.money &&
                            x.inputEmployee == order.inputEmployee &&
                            x.number == order.number &&
                            x.systemId == order.systemId &&
                            x.text == order.text);
                        if (_order != null && (order.inputTime - _order.inputTime).TotalSeconds <= 40)
                            continue;
                        db.Orders.InsertOnSubmit(order);

                        //Thread td = new Thread(delegate() { print_kitchen(order); });
                        //td.IsBackground = true;
                        //td.Start();
                    }
                    db.SubmitChanges();

                }
                //find_combo(db, m_Seat);

                return "true";
            }
            catch (System.Exception ex)
            {
                insert_file("time=" + DateTime.Now.ToString()+"\n");
                insert_file(infor);
                insert_file("error=" + ex.ToString());
                insert_file("\n");
                return "false";
            }
        }

        public static void print_kitchen(Orders order)
        {
            try
            {
                var dc = new BathDbDataContext(connectionString);
                var menu = dc.Menu.FirstOrDefault(x => x.name == order.menu);
                if (menu == null)
                    return;

                var cat = dc.Catgory.FirstOrDefault(x => x.id == menu.catgoryId);
                if (cat == null)
                    return;

                if (cat.kitchPrinterName == "")
                    return;

                var room = dc.Room.FirstOrDefault(x=>x.seat.Contains(order.text));
                if (room == null)
                    return;

                PrintKitchen.Print_DataGridView(order.text, order.menu, cat.kitchPrinterName,
                    order.inputEmployee, order.number.ToString(), DateTime.Now.ToString("MM-dd HH:ss"), room.name);
            }
            catch (System.Exception e)
            {

            }
        }

        //写入文件
        public static void insert_file(string msg)
        {
            if (!Directory.Exists(@".\Log"))
                Directory.CreateDirectory(@".\Log");
            if (!File.Exists(@".\Log\error.log"))
            {
                FileStream fs = new FileStream(@".\Log\error.log", FileMode.Create);
                fs.Close();
            }

            using (StreamWriter sw = new StreamWriter(@".\Log\error.log", true))
                sw.Write(msg + "\n");
        }

        private static double get_seat_expense_for_combo(BathDbDataContext db, Combo combo, Seat seat)
        {
            IQueryable<string> menus = null;
            if (combo.menuIds != null)
            {
                var menuIds = disAssemble(combo.menuIds);
                menus = db.Menu.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
            }

            var orders = db.Orders.Where(x => x.systemId == seat.systemId && x.deleteEmployee == null && !x.paid);

            double money = 0;
            DateTime now = DateTime.Now;
            foreach (var order in orders)
            {
                if (menus != null && menus.Contains(order.menu))
                    continue;

                if (order.priceType == null || order.priceType == "停止消费")
                    money += order.money;
                else
                    money += order.money * Math.Ceiling((DateTime.Now - order.inputTime).TotalHours);
            }

            return Math.Round(money, 0);
        }

        //查找套餐
        public static void find_combo(BathDbDataContext db, Seat m_Seat)
        {
            db.Orders.DeleteAllOnSubmit(db.Orders.Where(x => x.systemId == m_Seat.systemId && x.menu.Contains("套餐")));
            var orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null);
            orders = orders.Where(x => !x.inputEmployee.Contains("电脑") && !x.menu.Contains("套餐"));
            foreach (Orders tmp_order in orders)
            {
                if (tmp_order.comboId != null)
                {
                    tmp_order.comboId = null;
                    tmp_order.money = db.Menu.FirstOrDefault(x => x.name == tmp_order.menu).price * tmp_order.number;
                }
            }
            db.SubmitChanges();

            var order_menus = orders.Where(x => x.comboId == null).Select(x => x.menu);
            var menus = db.Menu;
            var comboList = db.Combo.OrderByDescending(x => x.freePrice);
            foreach (Combo combo in comboList)
            {
                if (combo.priceType == "消费满免项目")
                {
                    if (get_seat_expense_for_combo(db, combo, m_Seat) < combo.expenseUpTo)
                        continue;

                    var freeIds = disAssemble(combo.freeMenuIds);
                    var combo_menus = menus.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    foreach (var combo_menu in combo_menus)
                    {
                        var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                        if (tmp_order == null) continue;
                        tmp_order.comboId = combo.id;
                        tmp_order.money = 0;
                    }
                }
                else
                {
                    List<int> menuIds = disAssemble(combo.menuIds);
                    var combo_menus = menus.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
                    if (combo_menus.All(x => order_menus.Any(y => y == x)))
                    {
                        foreach (var combo_menu in combo_menus)
                        {
                            var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                            tmp_order.comboId = combo.id;
                            if (combo.priceType == "免项目")
                            {
                                var freeIds = disAssemble(combo.freeMenuIds);
                                var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                                if (freeMenus.Contains(tmp_order.menu))
                                    tmp_order.money = 0;
                            }
                        }
                        if (combo.priceType == "减金额")
                        {
                            Orders comboOrder = new Orders();
                            comboOrder.menu = "套餐" + combo.id.ToString() + "优惠";
                            comboOrder.text = m_Seat.text;
                            comboOrder.systemId = m_Seat.systemId;
                            comboOrder.number = 1;
                            comboOrder.inputTime = DateTime.Now;
                            comboOrder.inputEmployee = "套餐";
                            comboOrder.paid = false;
                            comboOrder.comboId = combo.id;
                            comboOrder.money = Convert.ToDouble(combo.price) - combo.originPrice;

                            db.Orders.InsertOnSubmit(comboOrder);
                        }
                    }
                }
            }
            db.SubmitChanges();
        }
        //获取技师状态
        public static string get_tech_status(string techId)
        {
            var info = new List<string>();

            var db = new BathDbDataContext(connectionString);
            var tech = db.Employee.FirstOrDefault(x => x.id == techId);
            if (tech == null)
                return "false";

            if (tech.techStatus == null || tech.techStatus == "空闲")
            {
                info.Add("空闲");
                return get_result_str(info);
            }
            info.Add(tech.techStatus);
            var left = Math.Round(tech.serverTime.Value - (DateTime.Now - tech.startTime.Value).TotalMinutes, 0);
            info.Add(left.ToString());

            return get_result_str(info);
        }

        //发送消息给技师房
        public static string send_tech_msg(string techId, string serveType, string techType, 
            string number, string roomId, string seatId,string gender)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);

                var m_TechMsg = new TechMsg();

                m_TechMsg.time = DateTime.Now;
                m_TechMsg.techId = techId;
                m_TechMsg.type = serveType;
                m_TechMsg.room = roomId;
                m_TechMsg.seat = seatId;
                m_TechMsg.techType = techType;
                m_TechMsg.gender = gender;
                //m_TechMsg.number = Convert.ToInt32(number);
                m_TechMsg.menu = number;
                m_TechMsg.read = false;
                db.TechMsg.InsertOnSubmit(m_TechMsg);
                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //获取技师类别
        public static string get_tech_jobs()
        {
            var info = new List<string>();

            var db = new BathDbDataContext(connectionString);
            var jobs = db.Job.Where(x => x.name.Contains("技师"));

            info.AddRange(jobs.Select(x => x.name));

            return get_result_str(info);
        }
        
        //预约技师
        public static string reserve_tech(string techId, string seatId, string roomId, string time, string gender)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);

                var tr = new TechReservation();

                tr.techId = techId;
                tr.seatId = seatId;
                tr.roomId = roomId;
                tr.time = Convert.ToDateTime(time);
                tr.gender = gender;
                tr.proceeded = false;

                db.TechReservation.InsertOnSubmit(tr);
                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //修改技师状态
        public static string save_tech(string techId, string room, string seat, string status, string serverTime, string startTime)
        {
            //StringBuilder sb = new StringBuilder();
            try
            {
                //insert_file("enter save tech");
                //var dbLayer = new DBLayer(connectionString);
                //sb.Append("update [Employee] set techStatus='" + status + "'");
                var db = new BathDbDataContext(connectionString);
                var tech = db.Employee.FirstOrDefault(x => x.id.ToString() == techId);
                if (tech == null)
                    return "null";

                string old_status = tech.techStatus.Trim();
                if (room != "null")
                //    sb.Append(",room='" + room + "'");
                    tech.room = room;
                else
                //    sb.Append(",room=null");
                    tech.room = null;

                if (seat == "null")
                    //sb.Append(",seat=null");
                    tech.seat = null;
                else
                    //sb.Append(",seat='" + seat + "'");
                    tech.seat = seat;

                //insert_file(status.Trim());
                tech.techStatus = status.Trim();
                if (serverTime != "null")
                    //sb.Append(",serverTime=" + serverTime);
                    tech.serverTime = Convert.ToInt32(serverTime);
                else
                    //sb.Append(",serverTime=null");
                    tech.serverTime = null;

                if (startTime != "null")
                    //sb.Append(",startTime='" + startTime + "'");
                    tech.startTime = Convert.ToDateTime(startTime);
                else
                    //sb.Append(",startTime=null");
                    tech.startTime = null;


                var tech_index = db.TechIndex.FirstOrDefault(x => x.dutyid == tech.jobId && x.gender == tech.gender);
                List<string> old_index = null;
                if (tech_index != null)
                {
                    old_index = tech_index.ids.Split('%').ToList();
                    var tech_str = old_index.FirstOrDefault(x => x.Contains(tech.id + "="));
                    var tech_str_index = old_index.IndexOf(tech_str);
                    if (tech_str_index != -1)
                    {
                        if (tech.techStatus == "空闲")
                        {
                            if (old_status == "点钟")
                            {
                                old_index[tech_str_index] = tech.id + "=T";
                                tech_index.ids = string.Join("%", old_index.ToArray());
                            }
                            else
                            {
                                old_index.RemoveAt(tech_str_index);
                                old_index.Add(tech.id + "=T");
                                tech_index.ids = string.Join("%", old_index.ToArray());
                            }
                        }
                        else if (tech.techStatus == "点钟" || tech.techStatus == "轮钟" || tech.techStatus == "上钟")
                        {
                            old_index[tech_str_index] = tech.id + "=F";
                            tech_index.ids = string.Join("%", old_index.ToArray());
                        }
                    }
                    else
                    {
                        if (tech.techStatus == "空闲")
                        {
                            old_index.Add(tech.id + "=T");
                            tech_index.ids = string.Join("%", old_index.ToArray());
                        }
                    }
                }

                db.SubmitChanges();
                //insert_file("finish save tech");
                return "true";
            }
            catch(Exception e)
            {
                insert_file(e.Message);
                return "false";
            }
        }

        //获取单个技师预约记录
        public static string get_tech_reserve(string techId)
        {
            var info = new List<string>();
            var db = new BathDbDataContext(connectionString);
            var tech = db.Employee.FirstOrDefault(x=>x.id==techId);
            if (tech == null)
                return "null";

            var r_order = db.TechReservation.FirstOrDefault(x => !x.proceeded.Value && x.techId == techId);
            if (r_order != null)
            {
                info.Add(r_order.seatId);
                info.Add(r_order.roomId);
                info.Add(r_order.time.Value.ToString("HH:mm"));
            }
            else
            {
                var r_on = db.TechReservation.FirstOrDefault(x => !x.proceeded.Value && x.techId == "轮钟" &&
                    (x.gender == "无" || x.gender == tech.gender));
                if (r_on != null)
                {
                    info.Add(r_on.seatId);
                    info.Add(r_on.roomId);
                    info.Add(r_on.time.Value.ToString("HH:mm"));
                }
            }

            return get_result_str(info);
        }

        //获取房间，手牌预约记录
        public static string get_room_seat_reservation(string roomId, string seatId)
        {
            var db = new BathDbDataContext(connectionString);

            var info = new List<string>();
            var t = db.TechReservation.FirstOrDefault(x => x.roomId == roomId && x.seatId == seatId && !x.proceeded.Value);
            if (t == null)
                return "null";

            info.Add(t.techId);
            info.Add(t.time.Value.ToString("HH:mm"));
            return get_result_str(info);
        }

        //处理预约
        public static string proceed_reservation(string techId, string seatId, string accept)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);

                var tr = db.TechReservation.FirstOrDefault(x => !x.proceeded.Value && x.techId == techId && x.seatId == seatId);
                if (tr == null)
                    tr = db.TechReservation.FirstOrDefault(x => !x.proceeded.Value && x.techId == "轮钟" && x.seatId == seatId);

                if (tr == null)
                    return "null";

                tr.proceeded = true;

                if (accept == "true")
                    tr.accept = true;
                else
                    tr.accept = false;

                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //上钟时如果有预约，删除预约
        public static string del_reservation(string roomId, string seatId)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);

                var info = new List<string>();
                var t = db.TechReservation.FirstOrDefault(x => x.roomId == roomId && x.seatId == seatId && !x.proceeded.Value);
                if (t == null)
                    return "null";

                db.TechReservation.DeleteOnSubmit(t);
                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //输入小费
        public static string make_tip(string seatTxt, string tech, string money)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);
                var m_Seat = db.Seat.FirstOrDefault(x => x.text == seatTxt);
                var menu = db.Menu.FirstOrDefault(x => x.name == "小费");

                Orders order = new Orders();
                order.menu = menu.name;
                order.text = m_Seat.text;
                order.systemId = m_Seat.systemId;
                order.number = 1;
                order.money = Convert.ToDouble(money);
                order.inputTime = DateTime.Now;
                order.inputEmployee = tech;
                order.paid = false;
                order.technician = tech;

                db.Orders.InsertOnSubmit(order);
                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //获取服务项目
        public static string get_waiters()
        {
            var db = new BathDbDataContext(connectionString);
            return string.Join("$", db.WaiterItem.Select(x => x.name).ToArray());
        }

        //给吧台发送消息
        public static string send_barMsg(string roomId, string seatId, string msg, string time)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);
                var barMsg = new BarMsg();

                barMsg.roomId = roomId;
                barMsg.seatId = seatId;
                barMsg.msg = msg;
                barMsg.read = false;
                barMsg.time = Convert.ToDateTime(time);

                db.BarMsg.InsertOnSubmit(barMsg);
                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //检测客房催钟消息
        public static string detect_room_call(string roomId, string seatId)
        {
            var db = new BathDbDataContext(connectionString);
            var call = db.RoomCall.FirstOrDefault(x => x.roomId == roomId && x.seatId.Contains(seatId) && !x.read);

            if (call != null && call.msg == "催钟")
                return "true";
            else
                return "false";
        }

        //设置客房催钟已读
        public static string set_room_called(string roomId, string seatId)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);
                var call = db.RoomCall.FirstOrDefault(x => x.roomId == roomId && x.seatId == seatId && !x.read);

                if (call == null)
                    return "null";

                call.read = true;
                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }

        //检测客房预警信息
        public static string detect_room_warn(string roomId)
        {
            var db = new BathDbDataContext(connectionString);
            if (!db.RoomWarn.Any())
                return "false";

            var warn = db.RoomWarn.Any(x => x.room == null || (x.room != null && !x.room.Contains(roomId)));

            if (warn)
                return "true";
            else
                return "false";
        }

        //设置接收到客房预警信息
        public static string set_room_warned(string roomId)
        {
            try
            {
                var db = new BathDbDataContext(connectionString);

                var warn = db.RoomWarn.FirstOrDefault(x => x.room == null || (x.room != null && !x.room.Contains(roomId)));
                if (warn != null)
                {
                    if (warn.room == null)
                        warn.room = roomId;
                    else
                        warn.room += "|" + roomId;
                }

                db.SubmitChanges();

                return "true";
            }
            catch (System.Exception e)
            {
                return "false";
            }
        }


        public static string get_offcall_delay()
        {
            var db = new BathDbDataContext(connectionString);
            return db.Options.FirstOrDefault().下钟提醒.Value.ToString();
        }

        public static string detect_seat(string seatId)
        {
            var db = new BathDbDataContext(connectionString);
            var seat = db.Seat.FirstOrDefault(x => x.text == seatId);
            if (seat == null)
                return "手牌不存在";

            if (seat.status != 2 && seat.status != 6 && seat.status != 7)
                return "手牌不在使用中";

            Room room = null;
            var rooms = db.Room.Where(x => x.seat.Contains(seatId));
            foreach (var r in rooms)
            {
                try
                {
                    var seatIds = r.seat.Split('|').ToList();
                    var status = r.status.Split('|').ToList();
                    int i = seatIds.IndexOf(seatId);
                    if (i != -1 && status[i] != "空闲" && status[i] != "null" && status[i] != "等待清洁")
                    {
                        room = r;
                        break;
                    }
                }
                catch (System.Exception e)
                {
                	
                }
            }

            if (room != null)
                return "房间:" + room.name;

            return "Avilable";
        }

        //获取软件版本号
        public static string get_apk_version(string fileName)
        {
            var db = new BathDbDataContext(connectionString);
            var apk = db.Apk.FirstOrDefault(x => x.name == fileName);
            if (apk == null)
                return "-1";
            return apk.version;
        }

        //获取软件版本号
        public static string get_apk_size(string fileName)
        {
            var db = new BathDbDataContext(connectionString);
            var apk = db.Apk.FirstOrDefault(x => x.name == fileName);
            if (apk == null)
                return "0";
            return apk.size.ToString();
        }
    }
}
