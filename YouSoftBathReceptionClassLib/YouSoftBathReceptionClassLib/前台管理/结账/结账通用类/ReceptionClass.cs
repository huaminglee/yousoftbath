using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouSoftBathGeneralClass;
using System.Windows.Forms;
using YouSoftBathConstants;
using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public class ReceptionClass
    {
        public static void reprint_bill(CSeat seat, DAO dao, bool use_pad)
        {

            if (seat.status != SeatStatus.PAIED)
            {
                BathClass.printErrorMsg("已经重新开牌，不能重打账单!");
                return;
            }

            var account = dao.get_account("abandon is null and systemId like '%" + seat.systemId + "%'");
            if (account == null)
                return;

            var seats_txt = account.text.Split('|');
            string state_str = "";
            int count = seats_txt.Count();
            for (int i = 0; i < count; i++)
            {
                state_str += "text='" + seats_txt[i] + "'";
                if (i != count - 1)
                    state_str += " or ";
            }
            var seats_reprint = dao.get_seats(state_str);
            List<string> m_rooms = new List<string>();
            foreach (var s in seats_reprint)
            {
                m_rooms.Add(dao.get_seat_room(s.text));
            }

            DataGridView dgv = new DataGridView();

            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "手牌";
            dgv.Columns.Add(col);

            DataGridViewTextBoxColumn coll = new DataGridViewTextBoxColumn();
            coll.HeaderText = "房间";
            dgv.Columns.Add(coll);

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "项目名称";
            dgv.Columns.Add(col1);

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "技师";
            dgv.Columns.Add(col2);

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "单价";
            dgv.Columns.Add(col3);

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "数量";
            dgv.Columns.Add(col4);

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "金额";
            dgv.Columns.Add(col5);

            List<string> printCols = new List<string>();
            printCols.Add("手牌");
            if (use_pad)
                printCols.Add("房间");

            printCols.Add("项目名称");
            printCols.Add("单价");
            printCols.Add("数量");
            printCols.Add("金额");

            var use_disAssemble = MConvert<bool>.ToTypeOrDefault(LogIn.options.启用大项拆分, false);

            var co_name = LogIn.options.companyName;
            if (account != null)
            {
                try
                {
                    var db = new BathDBDataContext(LogIn.connectionString);
                    var orders = db.HisOrders.Where(x => x.deleteEmployee == null && x.accountId == account.id).OrderBy(x => x.text);
                    foreach (var order in orders)
                    {
                        var cmenu = db.Menu.FirstOrDefault(x => x.name == order.menu);
                        var price = "";
                        if (cmenu != null)
                        {
                            price = cmenu.price.ToString();
                            if (use_disAssemble && db.BigCombo.FirstOrDefault(x => x.menuid == cmenu.id) != null)
                            {
                                var substIDs = BathClass.disAssemble(db.BigCombo.FirstOrDefault(x => x.menuid == cmenu.id).substmenuid, Constants.SplitChar);
                                for (int i = 0; i < substIDs.Count; i++)
                                {
                                    var menu = db.Menu.FirstOrDefault(x => x.id == substIDs[i]);
                                    dgv.Rows.Add(order.text, order.roomId, menu.name, order.technician,
                                        menu.price, order.number, menu.price * MConvert<double>.ToTypeOrDefault(order.number, 0));
                                }
                                continue;
                            }
                        }

                        dgv.Rows.Add(order.text, order.roomId, order.menu, order.technician, price, order.number,order.money);
                    }
                    PrintBill.Print_DataGridView(seats_reprint, m_rooms,account, "存根单", dgv, printCols, co_name);
                }
                catch (System.Exception ex)
                {
                    BathClass.printErrorMsg(ex.ToString());
                }
            }
            else
            {
                double money = 0;

                try
                {
                    var db = new BathDBDataContext(LogIn.connectionString);
                    var orders = db.Orders.Where(x => x.text == seat.text && x.systemId != seat.systemId && x.deleteEmployee == null).OrderBy(x => x.text);
                    foreach (var order in orders)
                    {
                        var cmenu = db.Menu.FirstOrDefault(x => x.name == order.menu);
                        var price = "";
                        if (cmenu != null)
                            price = cmenu.price.ToString();
                        
                        dgv.Rows.Add(order.text, order.roomId, order.menu, order.technician, price, order.number, order.money);
                    }

                    if (dgv.Rows.Count != 0)
                    {
                        BathClass.printErrorMsg("未检测到转账单或者结账单");
                        return;
                    }

                    PrintSeatBill.Print_DataGridView(seats_reprint, m_rooms,"", "转账确认单", dgv, printCols, money.ToString(), co_name);
                }
                catch (System.Exception ex)
                {
                    BathClass.printErrorMsg(ex.ToString());
                }
            }
        }
    }
}
