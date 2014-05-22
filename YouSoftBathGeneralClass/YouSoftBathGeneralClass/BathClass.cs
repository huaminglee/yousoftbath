using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

using System.Windows.Forms;
using System.Data.SqlClient;

using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using System.Transactions;

using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using YouSoftUtil;

namespace YouSoftBathGeneralClass
{
    public class BathClass
    {


        /// <summary>
        /// 拆分项目的资源消耗
        /// </summary>
        /// <param name="resourceExpense">项目的资源消耗字段</param>
        /// <returns></returns>
        public static Dictionary<string, double> disAssemble_Menu_resourceExpense(string resourceExpense)
        {
            Dictionary<string, double> menuIdList = new Dictionary<string, double>();

            if (resourceExpense == null)
                return menuIdList;

            string[] menuIds = resourceExpense.Split('$');
            foreach (string menuId in menuIds)
            {
                if (menuId == "")
                    continue;

                bool trans = true;
                string[] tps = menuId.Split('=');
                double expense = -1;
                try
                {
                    expense = Convert.ToDouble(tps[1]);
                }
                catch
                {
                    trans = false;
                }
                if (!trans) continue;
                menuIdList.Add(tps[0], expense);
            }

            return menuIdList;
        }

        //获取网络时间
        public static DateTime internetTime()
        {
            DateTime now = DateTime.Now;
            try
            {
                WebRequest wrt = null; 
                WebResponse wrp = null;

                wrt = WebRequest.Create("http://www.beijing-time.org/time.asp"); 
                wrp = wrt.GetResponse();

                string html = string.Empty;
                using (Stream stream = wrp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        html = sr.ReadToEnd();
                    }
                }

                string[] tempArray = html.Split(';');
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = tempArray[i].Replace("\r\n", "");
                }

                string year = tempArray[1].Split('=')[1]; 
                string month = tempArray[2].Split('=')[1]; 
                string day = tempArray[3].Split('=')[1]; 
                string hour = tempArray[5].Split('=')[1]; 
                string minite = tempArray[6].Split('=')[1]; 
                string second = tempArray[7].Split('=')[1];

                now = DateTime.Parse(year + "-" + month + "-" + day + " " + hour + ":" + minite + ":" + second);
            }
            catch (System.Exception e)
            {
            	
            }

            return now;
        }

        /// <summary>
        /// 合并项目的资源消耗
        /// </summary>
        /// <param name="resourceExpense">项目的资源消耗字段</param>
        /// <returns></returns>
        public static string assemble_Menu_resourceExpense(Dictionary<string, double> resourceExpense)
        {
            StringBuilder sb = new StringBuilder();
            if (resourceExpense == null)
                return "";

            foreach (KeyValuePair<string, double> a in resourceExpense)
            {
                sb.Append(a.Key).Append("=").Append(a.Value.ToString()).Append("$");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        public static string get_local_ip()
        {
            string local_ip = "";
            string hostName = Dns.GetHostName();//本机名
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//返回所有地址，包含IPv4，IPv6
            foreach (var ip in addressList)
            {
                string ips = ip.ToString();
                if (Regex.IsMatch(ips, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$"))
                {
                    local_ip = ips;
                    break;
                }
            }
            return local_ip;
        }

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        public static void SubmitChanges(BathDBDataContext db)
        {
            try
            {
                db.SubmitChanges();
            }
            catch 
            {
                try
                {
                    db.SubmitChanges();
                }
                catch
                {
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch
                    {
                    	db.SubmitChanges();
                    }
                }
            }
        }

        /// <summary>
        /// 主要用于消费满，获取手牌消费
        /// </summary>
        /// <param name="db"></param>
        /// <param name="combo"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        private static double get_seat_expense_for_combo(BathDBDataContext db, Combo combo, string systemId)
        {
            IQueryable<string> menus = null;
            if (combo.menuIds != null)
            {
                var menuIds = disAssemble(combo.menuIds);
                menus = db.Menu.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
            }

            var orders = db.Orders.Where(x => x.systemId == systemId && x.deleteEmployee == null && !x.paid);
            
            double money = 0;
            DateTime now = DateTime.Now;
            foreach (var order in orders)
            {
                if (menus != null && menus.Contains(order.menu))
                    continue;

                if (order.priceType == null || order.priceType == "停止消费")
                {
                    var order_menu = db.Menu.FirstOrDefault(x => x.name == order.menu);
                    if (order_menu == null)
                        money += order.money;
                    else
                        money += order_menu.price * order.number;
                }
                else
                    money += order.money * Math.Ceiling((DateTime.Now - order.inputTime).TotalHours);
            } 

            return Math.Round(money, 0);
        }

        //查找套餐
        public static void find_combo(string con_str, string systemId, string text)
        {
            var db = new BathDBDataContext(con_str);
            var dao = new DAO(con_str);
            var orders = db.Orders.Where(x => x.systemId == systemId && x.deleteEmployee == null && !x.inputEmployee.Contains("电脑"));
            
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from [Orders] where systemId='" + systemId + "' and menu like '套餐%优惠%'");
            sb.Append("update [Orders] set comboId=null, money=number*(select price from [Menu] where name=menu) ");
            sb.Append(@"where inputEmployee not like '%电脑%' and menu not like '套餐%优惠%' and systemId='");
            sb.Append(systemId);
            sb.Append(@"' and deleteEmployee is null and comboId is not null ");

            var menus = db.Menu;
            var comboList = db.Combo.OrderByDescending(x => x.freePrice);

            List<int> ids = new List<int>();
            foreach (Combo combo in comboList)
            {
                #region 消费满免项目
                if (combo.priceType == "消费满免项目")
                {
                    if (get_seat_expense_for_combo(db, combo, systemId) < combo.expenseUpTo) continue;

                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                    var combo_menus = menus.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    foreach (var combo_menu in combo_menus)
                    {
                        var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu && !ids.Contains(x.id));
                        if (tmp_order == null) continue;

                        sb.Append(" update [Orders] set comboId=" + combo.id + ",money=0 where id=" + tmp_order.id);
                        ids.Add(tmp_order.id);
                    }
                }
                #endregion

                #region 其他两种套餐形式：免项目、减金额
                else
                {
                    List<int> menuIds = disAssemble(combo.menuIds);
                    var combo_menus = menus.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
                    var order_menus = orders.Where(x => !ids.Contains(x.id)).Select(x => x.menu);
                    while (combo_menus.All(x => order_menus.Any(y => y == x)))
                    {
                        foreach (var combo_menu in combo_menus)
                        {
                            var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu && !ids.Contains(x.id));
                            sb.Append(" update [Orders] set comboId=" + combo.id + " where id=" + tmp_order.id);
                            ids.Add(tmp_order.id);
                            if (combo.priceType == "免项目")
                            {
                                var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                                var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                                if (freeMenus.Contains(tmp_order.menu))
                                    sb.Append(" update [Orders] set money=0 where id=" + tmp_order.id);
                            }
                        }
                        if (combo.priceType == "减金额")
                        {
                            sb.Append("insert into [Orders](menu,text,systemId,number,inputTime,inputEmployee,paid,comboId,money)");
                            sb.Append(" values('套餐" + combo.id.ToString() + "优惠','");
                            sb.Append(text + "','" + systemId + "',1,getdate(),'套餐','False'," + combo.id +","+
                                (Convert.ToDouble(combo.price) - combo.originPrice).ToString() + ")");
                        }
                        order_menus = orders.Where(x => !ids.Contains(x.id)).Select(x => x.menu);
                    }
                }
                #endregion
            }

            if (!dao.execute_command(sb.ToString()))
            {
                BathClass.printErrorMsg("查找套餐失败");
                return;
            }
        }

        public static void find_combo_duplicated(string con_str, string systemId, string text)
        {
            var db = new BathDBDataContext(con_str);
            db.Orders.DeleteAllOnSubmit(db.Orders.Where(x => x.systemId == systemId && x.menu.Contains("套餐")));
            var orders = db.Orders.Where(x => x.systemId == systemId && x.deleteEmployee == null);
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

            var menus = db.Menu;
            var comboList = db.Combo.OrderByDescending(x => x.freePrice);

            List<int> ids = new List<int>();
            foreach (Combo combo in comboList)
            {
                if (combo.priceType == "消费满免项目")
                {
                    if (get_seat_expense_for_combo(db, combo, systemId) < combo.expenseUpTo)
                        continue;

                    var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                    var combo_menus = menus.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    foreach (var combo_menu in combo_menus)
                    {
                        var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu && !ids.Contains(x.id));
                        if (tmp_order == null) continue;
                        tmp_order.comboId = combo.id;
                        tmp_order.money = 0;
                        ids.Add(tmp_order.id);
                    }
                }
                else
                {
                    List<int> menuIds = disAssemble(combo.menuIds);
                    var combo_menus = menus.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
                    var order_menus = orders.Where(x => !ids.Contains(x.id)).Select(x => x.menu);
                    if (combo_menus.All(x => order_menus.Any(y => y == x)))
                    {
                        foreach (var combo_menu in combo_menus)
                        {
                            var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                            tmp_order.comboId = combo.id;
                            ids.Add(tmp_order.id);
                            if (combo.priceType == "免项目")
                            {
                                var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                                var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                                if (freeMenus.Contains(tmp_order.menu))
                                    tmp_order.money = 0;
                            }
                        }
                        if (combo.priceType == "减金额")
                        {
                            Orders comboOrder = new Orders();
                            comboOrder.menu = "套餐" + combo.id.ToString() + "优惠";
                            comboOrder.text = text;
                            comboOrder.systemId = systemId;
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

        //查找套餐
        /*public static void find_combo_by_dao(DAO dao, string systemId, string text)
        {
            //db.Orders.DeleteAllOnSubmit(db.Orders.Where(x => x.systemId == m_Seat.systemId && x.menu.Contains("套餐优惠")));
            //var orders = db.Orders.Where(x => x.systemId == m_Seat.systemId && x.deleteEmployee == null);
            //orders = orders.Where(x => !x.inputEmployee.Contains("电脑") && !x.menu.Contains("套餐优惠"));
            //foreach (Orders tmp_order in orders)
            //{
            //    if (tmp_order.comboId != null)
            //    {
            //        tmp_order.comboId = null;
            //        tmp_order.money = db.Menu.FirstOrDefault(x => x.name == tmp_order.menu).price * tmp_order.number;
            //    }
            //}
            //db.SubmitChanges();
            string cmd_str = "delete from [Orders] where systemId='" + systemId + "' and menu like '%套餐%优惠%'";
            cmd_str += "update [Orders] set comboId=null, money=number*(select price from [Menu] where name=menu) "
                    + @"where inputEmployee not like '%电脑%' and menu not like '%套餐%优惠%' and systemId='"
                    +systemId
                    + @"' and deleteEmployee is null and comboId is not null ";

            var order_menus = dao.get_order_menus("systemId='"+systemId+"' and deleteEmployee is null");
            //var order_menus = orders.Where(x => x.comboId == null).Select(x => x.menu);
            //var menus = db.Menu;
            var comboList = dao.get_Combos(null, "order by freePrice desc");
            //var comboList = db.Combo.OrderByDescending(x => x.freePrice);
            foreach (var combo in comboList)
            {
                if (combo.priceType == "消费满免项目")
                {
                    if (dao.get_seat_expense("systemId='" + systemId + "'") < combo.expenseUpTo) continue;
                    //if (get_seat_expense_for_combo(db, combo, m_Seat) < combo.expenseUpTo)
                    //    continue;

                    var freeIds = combo.disAssemble_freeIds();
                    var combo_menus = dao.get_combo_menus(freeIds);
                    //var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                    //var combo_menus = menus.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                    foreach (var combo_menu in combo_menus)
                    {
                        cmd_str += @"update [Orders] set comboId=" + combo.id + ",money=0 where menu='" + combo_menu + "'";
                        //var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                        //if (tmp_order == null) continue;
                        //tmp_order.comboId = combo.id;
                        //tmp_order.money = 0;
                    }
                    //if (combo_menus.All(x => order_menus.Any(y => y == x)))
                    //{
                    //    foreach (var combo_menu in combo_menus)
                    //    {
                    //        var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                    //        tmp_order.comboId = combo.id;
                    //        tmp_order.money = 0;
                    //    }
                    //}
                    //db.SubmitChanges();
                }
                else
                {
                    var menuIds = combo.disAssemble_menuIds();
                    var combo_menus = dao.get_combo_menus(menuIds);
                    //List<int> menuIds = disAssemble(combo.menuIds);
                    //var combo_menus = menus.Where(x => menuIds.Contains(x.id)).Select(x => x.name);
                    if (combo_menus.All(x => order_menus.Any(y => y == x)))
                    {
                        foreach (var combo_menu in combo_menus)
                        {
                            string top_str = "(select top 1 id from "
                                    + "[Orders] where menu='" + combo_menu + "' and inputEmployee not like '%电脑%' "
                                    + "and menu not like '%套餐%优惠%' and systemId='"
                                    + systemId + "' and deleteEmployee is null)";
                            //var tmp_order = orders.FirstOrDefault(x => x.menu == combo_menu);
                            cmd_str += @"update [Orders] set comboId=" + combo.id + " where id=" + top_str;
                            //tmp_order.comboId = combo.id;
                            if (combo.priceType == "免项目")
                            {
                                var freeIds = combo.disAssemble_freeIds();
                                var freeMenus = dao.get_combo_menus(freeIds);
                                string state_str = "";
                                int count = freeMenus.Count;
                                for (int i = 0; i < count; i++ )
                                {
                                    state_str += "menu='" + freeMenus[i] + "'";
                                    if (i != count - 1)
                                        state_str += " or ";
                                }
                                cmd_str += "update [Orders] set money=0 where " + state_str + "and id=" + top_str;
                                //var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                                //var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id)).Select(x => x.name);
                                //if (freeMenus.Contains(tmp_order.menu))
                                //    tmp_order.money = 0;
                            }
                        }
                        if (combo.priceType == "减金额")
                        {
                            cmd_str += "insert into [Orders](menu,text,systemId,number,inputTime,inputEmployee,paid,comboId)"
                                + " values('套餐" + combo.id.ToString() + "优惠','"
                                + text + "','" + systemId + "',1,getdate(),'套餐','False'," + combo.id;
                        }
                    }
                }
            }
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("查找套餐失败");
                return;
            }
        }*/

        //字节数组转16进制字符串 
        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        } 

        //导出到Excel
        public static void exportDgvToExcel(DataGridView dgv)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "Execl files (*.xls,*.xlsx)|*.xls";
            saveFileDlg.FilterIndex = 0;
            saveFileDlg.RestoreDirectory = true;
            saveFileDlg.CreatePrompt = true;
            saveFileDlg.Title = "Export Excel File To";
            if (saveFileDlg.ShowDialog() != DialogResult.OK)
                return;

            Stream myStream;
            myStream = saveFileDlg.OpenFile();
            //StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("gb2312"));
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";
            try
            {
                //写标题
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (!dgv.Columns[i].Visible) continue;
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dgv.Columns[i].HeaderText;
                }
                sw.WriteLine(str);
                //写内容
                for (int j = 0; j < dgv.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dgv.Columns.Count; k++)
                    {
                        if (!dgv.Columns[k].Visible) continue;
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }
                        if (dgv.Rows[j].Cells[k].Value != null)
                            tempStr += dgv.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(tempStr);
                }
                sw.Close();
                myStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

        public static double get_combo_price(BathDBDataContext db, Combo combo)
        {
            double combo_price = 0;
            if (combo.priceType == "免项目" || combo.priceType == "消费满免项目")
            {
                var freeIds = disAssemble(combo.freeMenuIds);
                var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id));
                var freeMoney = freeMenus.Sum(x => x.price);
                combo_price = freeMoney;
            }
            else if (combo.priceType == "减金额")
                combo_price = combo.originPrice - Convert.ToDouble(combo.price);

            return combo_price;

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

        //获取所有的权限字段
        public static List<string> getAllAuthorities()
        {
            List<string> allAuthorities = new List<string>();
            BathDBDataContext SuperMarketDataContext = new BathDBDataContext();
            var table = from t in SuperMarketDataContext.Mapping.GetTables()
                        where t.TableName == "dbo.Authority"
                        select t;
            foreach (var col in table.First().RowType.DataMembers)
            {
                allAuthorities.Add(col.MappedName);
            }

            return allAuthorities;
        }

        //获取员工权限
        public static bool getAuthority(BathDBDataContext db, Employee employee, string pro)
        {
            var authority = db.Authority.FirstOrDefault(x => x.emplyeeId == employee.id);
            if (authority == null)
                authority = db.Authority.FirstOrDefault(x => x.jobId == employee.jobId);

            var proVal = authority.GetType().GetProperty(pro);
            //if (proVal == null)
            //return true;

            return Convert.ToBoolean(proVal.GetValue(authority, null));
        }

        //组合优惠方案
        public static string assemble(Dictionary<string, string> offer)
        {
            string menuIds = "";
            foreach (string key in offer.Keys)
            {
                menuIds += key + "=" + offer[key] + ";";
            }
            return menuIds;
        }

        //拆分优惠方案
        public static Dictionary<string, string> disAssemble(BathDBDataContext db, Promotion promotion)
        {
            Dictionary<string, string> menuIdList = new Dictionary<string, string>();

            if (promotion.menuIds == null)
                return menuIdList;

            string[] menuIds = promotion.menuIds.Split(';');
            foreach (string menuId in menuIds)
            {
                if (menuId == "")
                    continue;

                string[] tps = menuId.Split('=');
                menuIdList.Add(tps[0], tps[1]);
            }

            return menuIdList;
        }

        //只允许输入数字
        public static void only_allow_int(KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                e.KeyChar == (char)8 || e.KeyChar == '-')
                e.Handled = false;
            else
                e.Handled = true;
        }

        //只允许输入小数
        public static void only_allow_float(TextBox tb, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') ||
                (!tb.Text.Contains('.') && e.KeyChar == '.') ||
                e.KeyChar == (char)8)
                e.Handled = false;
            else
                e.Handled = true;
        }

        //打印错误信息
        public static void printErrorMsg(string msg)
        {
            MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //打印警告信息
        public static void printWarningMsg(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        //打印询问信息
        public static DialogResult printAskMsg(string msg)
        {
            return MessageBox.Show(msg, "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        //打印信息
        public static void printInformation(string msg)
        {
            InformationDlg informationDlg = new InformationDlg(msg);
            informationDlg.ShowDialog();
        }

        public static void set_dgv_fit(DataGridView dgv)
        {
            if (dgv.Columns.Count == 0)
                return;

            int row_header = 0;
            if (dgv.RowHeadersVisible)
                row_header = dgv.RowHeadersWidth;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            int w = dgv.Width - row_header;
            List<int> ws = new List<int>();
            List<int> iArray = new List<int>();
            int w0 = 0;
            int j = -1;
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                j++;
                if (!c.Visible) continue;
                ws.Add(c.Width);
                iArray.Add(j);
                w0 += c.Width;
            }

            if (w < w0) return;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            int w1 = (w - w0) / (ws.Count);
            for (int i = 0; i < ws.Count; i++)
            {
                var c = dgv.Columns[iArray[i]];
                c.Width = ws[i] + w1;
            }
        }

        //重置台位
        public static void reset_seat(Seat seat)
        {
            seat.systemId = null;
            seat.name = null;
            seat.population = null;
            seat.openTime = null;
            seat.openEmployee = null;
            seat.payTime = null;
            seat.payEmployee = null;
            seat.phone = null;
            seat.discount = null;
            seat.discountEmployee = null;
            seat.memberDiscount = null;
            seat.memberPromotionId = null;
            seat.freeEmployee = null;
            seat.chainId = null;
            seat.status = 1;
            seat.ordering = null;
            seat.paying = null;
            seat.note = null;
            seat.unwarn = null;
            seat.roomStatus = null;
            seat.deposit = null;
            seat.dueTime = null;
        }

        //重置客房
        public static void reset_room(Room room)
        {
            room.openTime = null;
            room.seat = null;
            room.systemId = null;
            room.menu = null;
            room.orderTechId = null;
            room.techId = null;
            room.startTime = null;
            room.serverTime = null;
            room.status = "空闲";
            room.note = null;
        }

        //获取服务器当前时间
        public static DateTime Now(string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            SqlCommand Commup3 = new SqlCommand("select getdate() as systemtimes", con);
            SqlDataReader dr1;
            dr1 = Commup3.ExecuteReader();
            String nowtime = "";

            while (dr1.Read())
            {
                nowtime = Convert.ToString(dr1.GetDateTime(0));
            }
            dr1.Dispose();

            con.Close();
            return Convert.ToDateTime(nowtime);
        }

        public static int get_member_balance(BathDBDataContext db, string card_no)
        {
            var cc = db.CardCharge.Where(x => x.CC_CardNo == card_no);
            if (cc.Any())
            {
                double debit = 0;
                double lend = 0;
                var cc_debit = cc.Where(x => x.CC_DebitSum != null);
                if (cc_debit.Any())
                    debit = cc_debit.Sum(x => x.CC_DebitSum).Value;

                var cc_lend = cc.Where(x => x.CC_LenderSum != null);
                if (cc_lend.Any())
                    lend = cc_lend.Sum(x => x.CC_LenderSum).Value;

                return (int)(debit - lend);
            }
            else
                return 0;
        }

        public static int get_member_credits(BathDBDataContext db, string card_no)
        {
            var cc = db.CardCharge.Where(x => x.CC_CardNo == card_no);
            double debit = MConvert<double>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            double lend = MConvert<double>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);

            CardInfo ci = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == card_no);
            var c = MConvert<int>.ToTypeOrDefault(ci.CI_CreditsUsed, 0);
            var cu = MConvert<int>.ToTypeOrDefault(db.MemberSetting.FirstOrDefault().money, 0);
            return (int)(lend / cu - c);
        }

        //获取账单金额
        public static double get_account_money(Account account)
        {
            double money = 0;
            money += MConvert<double>.ToTypeOrDefault(account.cash, 0);
            money += MConvert<double>.ToTypeOrDefault(account.bankUnion ,0);
            money += MConvert<double>.ToTypeOrDefault(account.creditCard, 0);
            money += MConvert<double>.ToTypeOrDefault(account.coupon, 0);
            money += MConvert<double>.ToTypeOrDefault(account.groupBuy, 0);
            money += MConvert<double>.ToTypeOrDefault(account.zero, 0);
            money += MConvert<double>.ToTypeOrDefault(account.server, 0);
            money -= MConvert<double>.ToTypeOrDefault(account.changes, 0);
            money += MConvert<double>.ToTypeOrDefault(account.wipeZero, 0);


            return money;
        }

        //获取账单金额
        public static double get_account_money(CAccount account)
        {
            double money = 0;
            money += MConvert<double>.ToTypeOrDefault(account.cash, 0);
            money += MConvert<double>.ToTypeOrDefault(account.bankUnion, 0);
            money += MConvert<double>.ToTypeOrDefault(account.creditCard, 0);
            money += MConvert<double>.ToTypeOrDefault(account.coupon, 0);
            money += MConvert<double>.ToTypeOrDefault(account.groupBuy, 0);
            money += MConvert<double>.ToTypeOrDefault(account.zero, 0);
            money += MConvert<double>.ToTypeOrDefault(account.server, 0);
            money -= MConvert<double>.ToTypeOrDefault(account.changes, 0);
            money += MConvert<double>.ToTypeOrDefault(account.wipeZero, 0);


            return money;
        }

        //在使用会员卡和优惠券结账时，发送消息给摄像头进行录像
        public static bool sendMessageToCamera(DAO dao, string systemId)
        {
            string cmd_str = "insert into [PayMsg](systemId, ip) values('" + systemId + "','" + PCUtil.getLocalIp() + "')";
            if (!dao.execute_command(cmd_str))
            {
                return false;
            }
            return true;
        }

        //获取系统账号
        public static string systemId(BathDBDataContext db, string connectionString)
        {
            string today = BathClass.Now(connectionString).ToString("yyyyMMdd");
            string max_id = db.SystemIds.Max(x => x.systemId);

            long maxId = 0;
            if (max_id != null)
            {
                if (max_id.Contains(today))
                    maxId = Convert.ToInt64(max_id.Replace(today, ""));
                else
                    db.ExecuteCommand("TRUNCATE TABLE SystemIds");
            }

            return today + (maxId + 1).ToString().PadLeft(5, '0');
        }

        //获取联台账号
        public static string chainId(BathDBDataContext db, string connectionString)
        {
            string today = BathClass.Now(connectionString).ToString("yyyyMMdd");
            var chainIdList = db.Seat.Where(x => x.chainId.Contains(today)).Select(x => x.chainId);

            int maxId = 0;
            foreach (string sId in chainIdList)
            {
                int index = Convert.ToInt32(sId.Replace("L" + today, ""));
                if (maxId < index)
                    maxId = index;
            }

            return "L" + today + (maxId + 1).ToString().PadLeft(4, '0');
        }

        //获取当前所有未结账消费
        public static double get_unpaid_expense(BathDBDataContext db, string connectionString)
        {
            var orders = db.Orders.Where(x => x.deleteEmployee == null && !x.paid);
            double money = 0;

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Any())
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Any())
            {
                DateTime now = BathClass.Now(connectionString);
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((now - x.inputTime).TotalHours));
            }

            return Math.Round(money, 0);
        }

        //获取当前所有已结账消费
        public static double get_paid_expense(BathDBDataContext db, DateTime startTime, ref int count)
        {
            double money = 0;
            var accounts = db.Account.Where(x => x.payTime >= startTime && x.abandon == null);
            foreach (Account act in accounts)
            {
                count += act.systemId.Split('|').Count();
                money += MConvert<double>.ToTypeOrDefault(act.cash, 0)
                    - MConvert<double>.ToTypeOrDefault(act.changes, 0)
                    + MConvert<double>.ToTypeOrDefault(act.bankUnion, 0)
                    + MConvert<double>.ToTypeOrDefault(act.creditCard, 0)
                    + MConvert<double>.ToTypeOrDefault(act.coupon, 0)
                    + MConvert<double>.ToTypeOrDefault(act.zero, 0)
                    + MConvert<double>.ToTypeOrDefault(act.server, 0)
                    + MConvert<double>.ToTypeOrDefault(act.deducted, 0)
                    + MConvert<double>.ToTypeOrDefault(act.wipeZero, 0);
            }

            return Math.Round(money, 0);
        }

        //获取手牌消费金额
        public static double get_seat_expense(Seat seat, string connectionString)
        {
            BathDBDataContext db = new BathDBDataContext(connectionString);
            var orders = db.Orders.Where(x => x.systemId == seat.systemId && x.deleteEmployee == null && !x.paid);
            double money = 0;

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Count() != 0)
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Count() != 0)
            {
                DateTime now = BathClass.Now(connectionString);
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((now - x.inputTime).TotalHours));
            }

            return Math.Round(money, 0);
        }

        //获取手牌消费金额
        public static double get_seat_expense(Seat seat, BathDBDataContext db, string connectionString)
        {
            var orders = db.Orders.Where(x => x.systemId == seat.systemId && x.deleteEmployee == null && !x.paid && 
                x.departmentId == null);
            double money = 0;

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Count() != 0)
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Count() != 0)
            {
                DateTime now = BathClass.Now(connectionString);
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((now - x.inputTime).TotalHours));
            }
            return Math.Round(money, 0);
        }

        //获取手牌列表消费
        public static double get_seats_expenses(BathDBDataContext db, List<Seat> seats, string connectionString)
        {
            if (seats == null)
                return 0;

            double money = 0;

            var ids = seats.Select(x => x.systemId);
            var orders = db.Orders.Where(x => ids.Contains(x.systemId) && x.deleteEmployee == null && !x.paid);

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Any())
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Any())
            {
                DateTime now = BathClass.Now(connectionString);
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((now - x.inputTime).TotalHours));
            }
            
            return Math.Round(money, 0);
        }

        //获取已结账订单总金额
        public static double get_orders_money(List<Account> accounts, List<HisOrders> orders)
        {
            double money = 0;

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Count() != 0)
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Count() != 0)
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((
                    accounts.FirstOrDefault(y => y.systemId.Split('|').Contains(x.systemId)).payTime
                    - x.inputTime).TotalHours));

            return Math.Round(money, 0);
        }

        //获取未结账订单总金额
        public static double get_cur_orders_money(IQueryable<Orders> orders, string con_str, DateTime now)
        {
            double money = 0;

            var tmp_orders = orders.Where(x => x.priceType == null || x.priceType == "停止消费");
            if (tmp_orders.Count() != 0)
                money = tmp_orders.Sum(x => x.money);

            tmp_orders = orders.Where(x => x.priceType == "每小时");
            if (tmp_orders.Count() != 0)
                money += tmp_orders.Sum(x => x.money * Math.Ceiling((now - x.inputTime).TotalHours));

            return Math.Round(money, 0);
        }

        //切换到中文输入法
        public static void change_input_ch()
        {
            // 如果只有一种输入法，什么也不做
            if (InputLanguage.InstalledInputLanguages.Count == 1)
                return;

            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                if (iL.LayoutName.Contains("搜狗"))
                {
                    InputLanguage.CurrentInputLanguage = iL;
                    break;
                }
            }
        }

        //切换到中文输入法
        public static void change_input_en()
        {
            // 如果只有一种输入法，什么也不做
            if (InputLanguage.InstalledInputLanguages.Count == 1)
                return;

            foreach (InputLanguage iL in InputLanguage.InstalledInputLanguages)
            {
                if (iL.LayoutName.Contains("美式键盘"))
                {
                    InputLanguage.CurrentInputLanguage = iL;
                    break;
                }
            }
        }

        //往dgv中添加列
        public static void add_cols_to_dgv(DataGridView pdgv, string header)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = header;
            pdgv.Columns.Add(col);
        }

        public static void download_file(string ftphost, string fileNameSaveAs, string ftpfilepath)
        {
            try
            {
                string ftpfullpath = "ftp://" + ftphost + "/" + ftpfilepath;

                using (WebClient request = new WebClient())
                {
                    byte[] fileData = request.DownloadData(ftpfullpath);
                    using (FileStream file = File.Create(fileNameSaveAs))
                    {
                        file.Write(fileData, 0, fileData.Length);
                        file.Close();
                    }
                }
            }
            catch
            {
            }
        }
        
    }
}
