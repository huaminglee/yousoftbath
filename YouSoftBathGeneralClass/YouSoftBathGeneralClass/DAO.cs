using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YouSoftUtil;
using YouSoftBathConstants;

namespace YouSoftBathGeneralClass
{
    public class DAO
    {
        private string _con_str;
        
        //构造函数
        public DAO(string con_str)
        {
            _con_str = con_str;
        }

        public string connectionString
        {
            get { return _con_str; }
            set { _con_str = value; }
        }

        private SqlConnection open_connection()
        {
            SqlConnection sqlCn = null;
            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();
            }
            catch (System.Exception e)
            {
                sqlCn = null;
                BathClass.printErrorMsg("网络出错，请检查网络后重试!");
                IOUtil.insert_file(DateTime.Now.ToString());
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("\n");
            }
            return sqlCn;
        }

        //关闭网络连接
        public void close_connection(SqlConnection sqlCn)
        {
            if (sqlCn != null && sqlCn.State == System.Data.ConnectionState.Open)
                    sqlCn.Close();
        }

        //检查网络
        public bool check_net(){


            bool network_checked = false;
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(_con_str);
                con.Open();
                network_checked = true;
            }
            catch (System.Exception e)
            {
            	BathClass.printErrorMsg(e.Message);
            }
            finally{
                close_connection(con);
            }

            return network_checked;
        }

        /*public bool delete_table_row(string table_name, string property, object val, bool is_str)
        {
            bool return_val = false;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = @"delete from [" + table_name + "] where " + property + "=" + ToDBString(val, is_str);
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                cmdSelect.ExecuteNonQuery();

                return_val = true;
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }*/

        //删除行
        public bool delete_table_rows(string table_name, string cmd_str)
        {
            bool return_val = false;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
            {
                return false;
            }
            SqlTransaction myTran = sqlCn.BeginTransaction();

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = @"delete from [" + table_name + "] where " + cmd_str;
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                cmdSelect.Transaction = myTran;
                cmdSelect.ExecuteNonQuery();

                return_val = true;
                myTran.Commit();
            }
            catch (System.Exception e)
            {
                myTran.Rollback();
                IOUtil.insert_file("method=clear_table,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }

        //清空表
        public bool clear_table(string table_name)
        {
            bool return_val = false;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return false;

            try
            {
                string cmd_str = @"truncate table [" + table_name + "]";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                cmdSelect.ExecuteNonQuery();

                return_val = true;
            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }

        public int get_memberCard_useTimes_this_month(string cardNo)
        {
            int times = -1;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return times;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"select count(*) from cardcharge where cc_cardno='");
                sb.Append(cardNo);
                sb.Append("' and cc_itemExplain!='售卡收' and datediff(month, cc_inputDate, getdate())=0");
                SqlCommand cmdSelect = new SqlCommand(sb.ToString(), sqlCn);
                object obj = cmdSelect.ExecuteScalar();
                times = (int)obj;
            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return times;
        }

        //转换成数据可用库字符串
        private string ToDBString(object obj, bool is_str)
        {
            if (obj == null || obj.ToString() == "")
                return "null";

            if (is_str)
                return "'" + obj.ToString() + "'";

            return obj.ToString();
        }

        private bool? ToBool(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;

            return Convert.ToBoolean(obj);
        }

        private int? ToInt(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;
            else
                return Convert.ToInt32(obj);
        }

        private string ToString(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;
            else
                return obj.ToString();
        }

        private DateTime? ToDateTime(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;

            else
                return Convert.ToDateTime(obj);
        }

        private double? ToDouble(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;
            else
                return Convert.ToDouble(obj);
        }

        private double ToRealDouble(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return 0;
            else
                return Convert.ToDouble(obj);
        }

        //获取表Options
        public COptions get_options()
        {
            COptions cOptions = new COptions();
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return null;

            try
            {
                string cmd_str = "Select * from [Options]";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cOptions.companyName = dr["companyName"].ToString();
                        cOptions.companyCode = dr["companyCode"].ToString();
                        cOptions.company_Code = dr["company_Code"].ToString();
                        cOptions.companyPhone = dr["companyPhone"].ToString();
                        cOptions.companyAddress = dr["companyAddress"].ToString();

                        cOptions.取消开牌时限 = ToInt(dr["取消开牌时限"]);
                        cOptions.取消开房时限 = ToInt(dr["取消开房时限"]);
                        cOptions.删除支出时限 = ToInt(dr["删除支出时限"]);
                        cOptions.退钟时限 = ToInt(dr["退钟时限"]);
                        cOptions.技师条数 = ToInt(dr["技师条数"]);
                        cOptions.启用鞋部 = ToBool(dr["启用鞋部"]);
                        cOptions.鞋部条数 = ToInt(dr["鞋部条数"]);
                        cOptions.启用结账监控 = ToBool(dr["启用结账监控"]);
                        cOptions.结账视频长度 = dr["结账视频长度"].ToString();
                        cOptions.启用手牌锁 = ToBool(dr["启用手牌锁"]);
                        cOptions.启用会员卡密码 = ToBool(dr["启用会员卡密码"]);
                        cOptions.启用客房面板 = ToBool(dr["启用客房面板"]);
                        cOptions.启用ID手牌锁 = ToBool(dr["启用ID手牌锁"]);
                        cOptions.允许手工输入手牌号结账 = ToBool(dr["允许手工输入手牌号结账"]);
                        cOptions.允许手工输入手牌号开牌 = ToBool(dr["允许手工输入手牌号开牌"]);
                        cOptions.录单输入单据编号 = ToBool(dr["录单输入单据编号"]);
                        cOptions.结账未打单锁定手牌 = ToBool(dr["结账未打单锁定手牌"]);
                        cOptions.营业报表格式 = ToInt(dr["营业报表格式"]);
                        cOptions.结账打印结账单 = ToBool(dr["结账打印结账单"]);
                        cOptions.结账打印存根单 = ToBool(dr["结账打印存根单"]);
                        cOptions.结账打印取鞋小票 = ToBool(dr["结账打印取鞋小票"]);
                        cOptions.抹零限制 = ToInt(dr["抹零限制"]);
                        cOptions.手牌锁类型 = dr["手牌锁类型"].ToString();
                        cOptions.自动加收过夜费 = ToBool(dr["自动加收过夜费"]);
                        cOptions.过夜费起点 = dr["过夜费起点"].ToString();
                        cOptions.过夜费终点 = dr["过夜费终点"].ToString();
                        cOptions.启用分单结账 = ToBool(dr["启用分单结账"]);
                        cOptions.启用员工服务卡 = ToBool(dr["启用员工服务卡"]);
                        cOptions.台位类型分页显示 = ToBool(dr["台位类型分页显示"]);
                        cOptions.提成报表格式 = ToInt(dr["提成报表格式"]);
                        cOptions.自动感应手牌 = ToBool(dr["自动感应手牌"]);
                        cOptions.录单区分点钟轮钟 = ToBool(dr["录单区分点钟轮钟"]);
                        cOptions.打印技师派遣单 = ToBool(dr["打印技师派遣单"]);
                        cOptions.会员卡密码类型 = ToString(dr["会员卡密码类型"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }
            return cOptions;
        }

        //获取手牌号的长度
        public int get_seat_length()
        {
            int length = 3;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return -1;

            try
            {
                string cmd_str = "select top 1 text from seat";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        length = dr["text"].ToString().Length;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }
            return length;
        }

        //获取是否有客房部
        public bool has_hotel_department()
        {
            bool has_hotel = false;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return false;

            try
            {
                string cmd_str = @"if exists (select * from SeatType where department = '客房部') "
                                 +@" SELECT CAST(1 AS BIT) "
                                 +@"else "
                                 +@"select cast(0 as bit)";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                object exist = cmd.ExecuteScalar();
                has_hotel = Convert.ToBoolean(exist);

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }
            return has_hotel;
        }

        public CJob get_job(string key, object key_val)
        {
            CJob cjob = null;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return null;

            try
            {
                string cmd_str = "Select * from [Job] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cjob = new CJob();
                        cjob.id = (int)dr["id"];
                        cjob.ip = dr["ip"].ToString();
                        cjob.name = dr["name"].ToString();
                        cjob.note = dr["note"].ToString();
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return cjob;
        }

        private bool? get_authority(string key, object key_val, string property)
        {
            bool? authority = null;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return authority;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select " + property + " from [Authority] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        authority = ToBool(dr[property]);
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return authority;
        }

        //获取员工权限
        public bool get_authority(Employee user, string property)
        {
            bool? authority = get_authority("emplyeeid", user.id, property);
            if (authority == null)
            {
                var cjob = get_job("id", user.jobId);
                authority = get_authority("jobId", cjob.id, property);
            }

            return MConvert<bool>.ToTypeOrDefault(authority, false);
        }

        //获取手牌数量
        public int get_seat_count(SeatStatus status)
        {
            int count = 0;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return -1;
            
            string cmd_str = "";

            try
            {
                cmd_str = "Select count(*) from [Seat]";
                if (status != SeatStatus.INVALID)
                    cmd_str +=  "where status = '" + ((int)status).ToString() + "'";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                count = (int)cmd.ExecuteScalar();
            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(cmd_str);
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return count;
        }

        //获取手牌数量
        public int get_seat_count(List<SeatStatus> status)
        {
            int count = 0;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return -1;

            try
            {
                int c = status.Count;
                string cmd_str = "Select count(*) from [Seat] where";
                for (int i = 0; i < c; i++)
                {
                    cmd_str += " status = '" + ((int)status[i]).ToString() + "'";
                    if (i != c - 1)
                        cmd_str += " or ";
                }
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                count = (int)cmd.ExecuteScalar();
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return count;
        }

        public DateTime? get_last_clear_time()
        {
            DateTime? dt = null;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return null;

            try
            {
                string cmd_str = "select top 1 cleartime from cleartable order by cleartime desc";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dt = Convert.ToDateTime(dr["cleartime"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg("查询出错!");
                IOUtil.insert_file(e.Message+"\n");
            }
            finally
            {
                close_connection(sqlCn);
            }
            return dt;
        }

        public List<DateTime?> get_last_index_clear_time(int index)
        {
            List<DateTime?> dts = new List<DateTime?>();
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return null;

            try
            {
                string cmd_str = "select top " + index + " cleartime from cleartable order by cleartime desc";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        dts.Add(Convert.ToDateTime(dr["cleartime"]));
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg("查询出错!");
                IOUtil.insert_file(e.Message + "\n");
            }
            finally
            {
                close_connection(sqlCn);
            }
            return dts;
        }

        //获取当前所有已结账消费
        public double get_paid_expense(DateTime startTime, ref int count)
        {
            double money = 0;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return -1;

            try
            {
                string cmd_str = "select * from account where payTime >= '" + startTime.ToString() + "'";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        count += dr["systemId"].ToString().Split('|').Count();
                        money += ToRealDouble(dr["cash"])
                            - ToRealDouble(dr["changes"])
                            + ToRealDouble(dr["bankUnion"])
                            + ToRealDouble(dr["creditCard"])
                            + ToRealDouble(dr["coupon"])
                            + ToRealDouble(dr["zero"])
                            + ToRealDouble(dr["server"])
                            + ToRealDouble(dr["deducted"])
                            + ToRealDouble(dr["wipeZero"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg("查询出错");
                IOUtil.insert_file(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return money;
        }

        //获取当前所有未结账消费
        public double get_unpaid_expense()
        {
            DateTime now = DateTime.Now;
            double money = 0;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return -1;
            string cmd_str = "";

            try
            {
                cmd_str = @"declare @money_no_t real"
                                + @" declare @money_t real"
                                + @" select @money_no_t=sum(money) from [Orders] where deleteEmployee is null and paid='False' and (priceType is null or priceType='停止消费')"
                                + @" select @money_t=sum(money*ceiling(datediff(minute, inputTime, getdate())/60.0)) from [Orders] where"
                                + @" deleteEmployee is null and paid='False' and priceType='每小时'"
                                + @" if @money_t is null"
                                + @" set @money_t=0"
                                + @" if @money_no_t is null"
                                + @" set @money_no_t=0"
                                + @" select @money_t+@money_no_t";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                object obj = cmd.ExecuteScalar();
                money = Convert.ToDouble(obj);
            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file(DateTime.Now + "=" + cmd_str+"\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return Math.Round(money, 0);
        }

        //获取当前所有未结账消费
        public double get_seat_expense(string state_str)
        {
            DateTime now = DateTime.Now;
            double money = 0;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return -1;
            string cmd_str = "";

            try
            {
                cmd_str = @"declare @money_no_t real"
                                + @" declare @money_t real"
                                + @" select @money_no_t=sum(money) from [Orders] where (deleteEmployee is null and paid='False' and"
                                + @" (priceType is null or priceType='停止消费')"+"and "+state_str+")"
                                + @" select @money_t=sum(money*ceiling(datediff(minute, inputTime, getdate())/60.0)) from [Orders] where"
                                + @" (deleteEmployee is null and paid='False' and priceType='每小时'" + "and " + state_str + ")"
                                + @" if @money_t is null"
                                + @" set @money_t=0"
                                + @" if @money_no_t is null"
                                + @" set @money_no_t=0"
                                + @" select @money_t+@money_no_t";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                object obj = cmd.ExecuteScalar();
                money = Convert.ToDouble(obj);
            }
            catch (System.Exception e)
            {
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + "cmd=" + cmd_str+"\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return Math.Round(money, 0);
        }

        public List<CSeatType> get_seattypes(string key, object key_val)
        {
            var seat_types = new List<CSeatType>();

            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return seat_types;

            try
            {
                string cmd_str = "Select * from [SeatType]";
                if (key != null)
                    cmd_str += " where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var seat_type = new CSeatType();
                        seat_type.id = (int)dr["id"];
                        seat_type.name = dr["name"].ToString();
                        seat_type.population = (int)dr["population"];
                        seat_type.menuId = ToInt(dr["menuId"]);
                        seat_type.department = dr["department"].ToString();
                        seat_type.depositeRequired = ToBool(dr["depositeRequired"]);
                        seat_type.depositeAmountMin = ToInt(dr["depositeAmountMin"]);
                        seat_types.Add(seat_type);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                IOUtil.insert_file(e.Message + "\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seat_types;
        }

        public CSeatType get_seattype(string key, object key_val)
        {
            CSeatType seat_type = null;

            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return seat_type;

            try
            {
                string cmd_str = "Select * from [SeatType]";
                if (key != null)
                    cmd_str += " where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        seat_type = new CSeatType();
                        seat_type.id = (int)dr["id"];
                        seat_type.name = dr["name"].ToString();
                        seat_type.population = (int)dr["population"];
                        seat_type.menuId = ToInt(dr["menuId"]);
                        seat_type.department = dr["department"].ToString();
                        seat_type.depositeRequired = ToBool(dr["depositeRequired"]);
                        seat_type.depositeAmountMin = ToInt(dr["depositeAmountMin"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                BathClass.printErrorMsg(e.Message + "\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seat_type;
        }

        public List<CSeat> get_seats(string key, object key_val)
        {
            var seats = new List<CSeat>();

            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return seats;

            try
            {
                string cmd_str = "Select * from [Seat]";
                if (key != null)
                    cmd_str += " where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);

                        seats.Add(seat);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                IOUtil.insert_file(e.Message + "\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seats;
        }

        public List<CSeat> get_seats(string key, List<object> key_val)
        {
            var seats = new List<CSeat>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();
            }
            catch
            {
                close_connection(sqlCn);
                //BathClass.printErrorMsg("网络出错!");
                return seats;
            }

            try
            {
                string cmd_str = "Select * from [Seat]";
                if (key != null)
                {
                    int count = key_val.Count;
                    for (int i = 0; i < count; i++ )
                    {
                        cmd_str += " where " + key + "='" + key_val[i].ToString() + "'";
                        if (i != count - 1)
                            cmd_str += " or ";
                    }
                }
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);

                        seats.Add(seat);
                    }
                }

            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seats;
        }

        public List<CSeat> get_all_seats()
        {
            var seats = new List<CSeat>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();
            }
            catch
            {
                close_connection(sqlCn);
            }

            if (sqlCn == null || sqlCn.State != System.Data.ConnectionState.Open)
                return seats;

            try
            {
                string cmd_str = "Select * from [Seat]";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);

                        seats.Add(seat);
                    }
                }

            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seats;
        }

        public List<CSeat> get_seats(string cmd_str)
        {
            var seats = new List<CSeat>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();
            }
            catch
            {
                close_connection(sqlCn);
            }

            if (sqlCn == null || sqlCn.State != System.Data.ConnectionState.Open)
                return null;
            
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.Append("select * from [seat] where ");
                sb.Append(cmd_str);
                SqlCommand cmdSelect = new SqlCommand(sb.ToString(), sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);

                        seats.Add(seat);
                    }
                }

            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(sb.ToString());
                //BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seats;
        }

        public CSeat get_seat(string key, object key_val)
        {
            CSeat seat = null;

            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return null;

            try
            {
                string cmd_str = "Select * from [Seat] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.depositBank = ToInt(dr["depositBank"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                IOUtil.insert_file(e.Message + "\n");
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seat;
        }

        public CSeat get_seat(string state_str)
        {
            CSeat seat = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Seat]";
                if (state_str != null && state_str!="")
                    cmd_str += " where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.depositBank = ToInt(dr["depositBank"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seat;
        }

        public CSeat get_seat(List<string> key, List<string> key_val, string logic)
        {
            CSeat seat = null;
            var sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != System.Data.ConnectionState.Open)
                return null;

            StringBuilder sb = new StringBuilder();

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                sb.Append("Select * from [Seat] where (");
                int count = key.Count;
                for (int i = 0; i < count; i++ )
                {
                    sb.Append(key[i]);
                    sb.Append("='");
                    sb.Append(key_val[i]);
                    sb.Append("'");
                    if (i != count - 1)
                        sb.Append(" " + logic + " ");
                }
                sb.Append(")");
                SqlCommand cmdSelect = new SqlCommand(sb.ToString(), sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        seat = new CSeat();
                        seat.id = (int)dr["id"];
                        seat.oId = dr["oId"].ToString();
                        seat.text = dr["text"].ToString();
                        seat.typeId = (int)dr["typeId"];
                        seat.systemId = dr["systemId"].ToString();
                        seat.name = dr["name"].ToString();
                        seat.population = ToInt(dr["population"]);
                        seat.openTime = ToDateTime(dr["openTime"]);
                        seat.openEmployee = dr["openEmployee"].ToString();
                        seat.payTime = ToDateTime(dr["payTime"]);
                        seat.payEmployee = dr["payEmployee"].ToString();
                        seat.discountEmployee = dr["discountEmployee"].ToString();
                        seat.discount = ToDouble(dr["discount"]);
                        seat.memberDiscount = ToBool(dr["memberDiscount"]);
                        seat.memberPromotionId = dr["memberPromotionId"].ToString();
                        seat.chainId = dr["chainId"].ToString();
                        seat.status = (SeatStatus)dr["status"];
                        seat.ordering = ToBool(dr["ordering"]);
                        seat.paying = ToBool(dr["paying"]);
                        seat.note = dr["note"].ToString();
                        seat.unwarn = dr["unwarn"].ToString();
                        seat.roomStatus = dr["roomStatus"].ToString();
                        seat.deposit = ToInt(dr["deposit"]);
                        seat.depositBank = ToInt(dr["depositBank"]);
                        seat.dueTime = ToDateTime(dr["dueTime"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(sb.ToString());
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seat;
        }

        public bool update_table_multi_row(string table_name, string key, List<string> key_val, List<string> pars, List<string> vals)
        {
            bool return_val = false;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append(@"update [" + table_name + "] set ");
                int count = pars.Count;
                for (int i = 0; i < count; i++)
                {
                    sb.Append(pars[i]);
                    sb.Append("='");
                    sb.Append(vals[i].ToString() + "'");
                    if (i != count - 1)
                        sb.Append(",");
                }
                sb.Append(" where ");
                count = key_val.Count;
                for (int i = 0; i < count; i++ )
                {
                    sb.Append(key + "='" + key_val + "'");
                    if (i != count - 1)
                        sb.Append(" or ");
                }

                SqlCommand cmdSelect = new SqlCommand(sb.ToString(), sqlCn);
                cmdSelect.ExecuteNonQuery();
                return_val = true;
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }


        public bool exist_instance(string table_name, string state_str)
        {
            bool has_hotel = false;
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = @"if exists (select * from [" + table_name + "] where " + state_str + ") "
                                 + @" SELECT CAST(1 AS BIT) "
                                 + @"else "
                                 + @"select cast(0 as bit)";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                object exist = cmd.ExecuteScalar();
                has_hotel = Convert.ToBoolean(exist);

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file("method=exist_instance,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }
            return has_hotel;
        }

        public bool has_ordered_guoye(string systemId)
        {
            bool has_ordered = false;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = @"if (exists (select * from [Orders] where systemId='"+systemId +"' and menu='过夜费') or "
                                 + @"exists (select * from [HisOrders] where systemId='" + systemId + "' and menu='过夜费')) "
                                 + @" SELECT CAST(1 AS BIT) "
                                 + @"else "
                                 + @"select cast(0 as bit)";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                object exist = cmd.ExecuteScalar();
                has_ordered = Convert.ToBoolean(exist);

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }
            return has_ordered;
        }

        //获取手牌列表消费
        public double get_seats_expenses(List<CSeat> seats)
        {
            if (seats == null)
                return 0;

            double money = 0;

            DateTime now = DateTime.Now;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string id_str = "";
                int count = seats.Count;
                for (int i = 0; i > count; i++ )
                {
                    id_str += "systemId='" + seats[i].systemId + "'";
                    if (i != count - 1)
                        id_str += " or ";
                }
                string cmd_str = "Select sum(money) from [Orders] where deleteEmployee=null and paid='False' and (priceType=null or priceType='停止消费')";
                cmd_str += " and (" + id_str + ")";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                money = ToRealDouble(cmd.ExecuteScalar());

                cmd_str = "Select * from [Orders] where deleteEmployee=null and paid='False' and priceType='每小时'";
                cmd_str += " and (" + id_str + ")";
                cmd = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        money += Convert.ToDouble(dr["money"]) * Math.Ceiling((now - Convert.ToDateTime(dr["inputTime"])).TotalHours);
                    }
                }
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return Math.Round(money, 0);

        }

        public CMenu get_Menu(string key, object key_val)
        {
            CMenu menu = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Menu] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        menu = new CMenu();
                        menu.id = (int)dr["id"];
                        menu.name = dr["name"].ToString();
                        menu.catgoryId = (int)dr["catgoryId"];
                        menu.unit = dr["unit"].ToString();
                        menu.price = (double)dr["price"];
                        menu.technician = (bool)dr["technician"];
                        menu.waiter = ToBool(dr["waiter"]);
                        menu.addAutomatic = (bool)dr["addAutomatic"];
                        menu.addType = dr["addType"].ToString();
                        menu.addMoney = ToDouble(dr["addMoney"]);
                        menu.ResourceExpense = ToString(dr["ResourceExpense"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return menu;
        }

        public List<CMenu> get_Menus(List<string> key, List<string> key_val, string logic)
        {
            var menus = new List<CMenu>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Menu] where (";
                int count = key.Count;
                for (int i = 0; i < count; i++)
                {
                    cmd_str += key[i] + "='" + key_val[i] + "'";
                    if (i != count - 1)
                        cmd_str += " " + logic + " ";
                }
                cmd_str += ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var menu = new CMenu();
                        menu.id = (int)dr["id"];
                        menu.name = dr["name"].ToString();
                        menu.catgoryId = (int)dr["catgoryId"];
                        menu.unit = dr["unit"].ToString();
                        menu.price = (double)dr["price"];
                        menu.technician = (bool)dr["technician"];
                        menu.waiter = ToBool(dr["waiter"]);

                        menus.Add(menu);
                    }
                }

            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return menus;
        }

        public List<CMenu> get_Menus(string state_str)
        {
            var menus = new List<CMenu>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Menu]";
                if (state_str != null && state_str != "")
                    cmd_str += " where (" + state_str + ")";

                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var menu = new CMenu();
                        menu.id = (int)dr["id"];
                        menu.name = dr["name"].ToString();
                        menu.catgoryId = (int)dr["catgoryId"];
                        menu.unit = dr["unit"].ToString();
                        menu.price = (double)dr["price"];
                        menu.technician = (bool)dr["technician"];
                        menu.waiter = ToBool(dr["waiter"]);

                        menus.Add(menu);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return menus;
        }

        /************************************************************************/
        /* 插入行
         * table_name:表名
         * pars：表列名
         * vals：表列值
         * is_str：表列是否为字符串，如果是需要加单引号
         * */
        /************************************************************************/
        public bool insert_table_row(string table_name, List<string> pars, List<string> vals)
        {
            bool return_val = false;
            SqlConnection sqlCn = null;
            string cmd_str = "";
            SqlTransaction myTran;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                return return_val;
            }

            myTran = sqlCn.BeginTransaction();

            try
            {
                cmd_str = @"insert into [" + table_name + "](";
                cmd_str += string.Join(",", pars.ToArray()) + ") values(";

                int count = vals.Count;
                for (int i = 0; i < count; i++)
                {
                    cmd_str += "'" + vals[i].ToString() + "'";
                    if (i != count - 1)
                        cmd_str += ",";
                }
                cmd_str += ")";

                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                cmdSelect.Transaction = myTran;
                cmdSelect.ExecuteNonQuery();
                return_val = true;
                myTran.Commit();
            }
            catch (System.Exception e)
            {
                myTran.Rollback();
                //IOUtil.insert_file(cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n\r");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }

        public bool execute_command(string cmd_str)
        {
            bool return_val = false;
            SqlConnection sqlCn = open_connection();
            if (sqlCn == null || sqlCn.State != ConnectionState.Open)
                return false;
            
            SqlTransaction myTran = sqlCn.BeginTransaction();

            try
            {
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                cmdSelect.Transaction = myTran;
                cmdSelect.ExecuteNonQuery();
                return_val = true;
                myTran.Commit();
            }
            catch (System.Exception e)
            {
                myTran.Rollback();
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                //BathClass.printErrorMsg(cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }

        public COrders get_order(string key, object key_val)
        {
            COrders order = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Orders] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        order = new COrders();

                        order.id = (int)dr["id"];
                        order.menu = dr["menu"].ToString();
                        order.text = dr["text"].ToString();
                        order.systemId = dr["systemId"].ToString();
                        order.number = (double)dr["number"];
                        order.priceType = dr["priceType"].ToString();
                        order.money = (double)dr["money"];
                        order.technician = dr["technician"].ToString();
                        order.techType = dr["techType"].ToString();
                        order.inputTime = (DateTime)dr["inputTime"];
                        order.inputEmployee = dr["inputEmployee"].ToString();
                        order.deleteEmployee = dr["deleteEmployee"].ToString();
                        order.comboId = ToInt(dr["comboId"]);
                        order.paid = (bool)dr["paid"];
                        order.accountId = ToInt(dr["accountId"]);
                        order.billId = dr["billId"].ToString();
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return order;
        }

        public COrders get_order(List<string> key, List<string> key_val, string logic)
        {
            COrders order = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Seat] where (";
                int count = key.Count;
                for (int i = 0; i < count; i++)
                {
                    cmd_str += key + "='" + key_val + "'";
                    if (i != count - 1)
                        cmd_str += ", " + logic + " ";
                }
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        order = new COrders();

                        order.id = (int)dr["id"];
                        order.menu = dr["menu"].ToString();
                        order.text = dr["text"].ToString();
                        order.systemId = dr["systemId"].ToString();
                        order.number = (double)dr["number"];
                        order.priceType = dr["priceType"].ToString();
                        order.money = (double)dr["money"];
                        order.technician = dr["technician"].ToString();
                        order.techType = dr["techType"].ToString();
                        order.inputTime = (DateTime)dr["inputTime"];
                        order.inputEmployee = dr["inputEmployee"].ToString();
                        order.deleteEmployee = dr["deleteEmployee"].ToString();
                        order.comboId = ToInt(dr["comboId"]);
                        order.paid = (bool)dr["paid"];
                        order.accountId = ToInt(dr["accountId"]);
                        order.billId = dr["billId"].ToString();
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return order;
        }

        public List<COrders> get_orders(List<string> key, List<string> key_val, string logic)
        {
            List<COrders> orders = new List<COrders>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Orders] where (";
                int count = key.Count;
                for (int i = 0; i < count; i++)
                {
                    cmd_str += key[i] + "='" + key_val[i] + "' ";
                    if (i != count - 1)
                        cmd_str += logic + " ";
                }
                cmd_str += ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var order = new COrders();

                        order.id = (int)dr["id"];
                        order.menu = dr["menu"].ToString();
                        order.text = dr["text"].ToString();
                        order.systemId = dr["systemId"].ToString();
                        order.number = (double)dr["number"];
                        order.priceType = dr["priceType"].ToString();
                        order.money = (double)dr["money"];
                        order.technician = dr["technician"].ToString();
                        order.techType = dr["techType"].ToString();
                        order.inputTime = (DateTime)dr["inputTime"];
                        order.inputEmployee = dr["inputEmployee"].ToString();
                        order.deleteEmployee = dr["deleteEmployee"].ToString();
                        order.comboId = ToInt(dr["comboId"]);
                        order.paid = (bool)dr["paid"];
                        order.accountId = ToInt(dr["accountId"]);
                        order.billId = dr["billId"].ToString();

                        orders.Add(order);
                    }
                }

            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return orders;
        }

        public List<COrders> get_orders(string cmd_str)
        {
            List<COrders> orders = new List<COrders>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Orders] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var order = new COrders();

                        order.id = (int)dr["id"];
                        order.menu = dr["menu"].ToString();
                        order.text = dr["text"].ToString();
                        order.systemId = dr["systemId"].ToString();
                        order.number = (double)dr["number"];
                        order.priceType = dr["priceType"].ToString();
                        order.money = (double)dr["money"];
                        order.technician = dr["technician"].ToString();
                        order.techType = dr["techType"].ToString();
                        order.inputTime = (DateTime)dr["inputTime"];
                        order.inputEmployee = dr["inputEmployee"].ToString();
                        order.deleteEmployee = dr["deleteEmployee"].ToString();
                        order.comboId = ToInt(dr["comboId"]);
                        order.paid = (bool)dr["paid"];
                        order.accountId = ToInt(dr["accountId"]);
                        order.billId = dr["billId"].ToString();

                        orders.Add(order);
                    }
                }

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return orders;
        }

        public int get_orders_count(string state_str)
        {
            List<COrders> orders = new List<COrders>();
            SqlConnection sqlCn = null;
            string cmd_str = "";
            int count = 0;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select count(*) from [Orders] where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                object obj = cmdSelect.ExecuteScalar();
                count = (int)obj;

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return count;
        }

        public CCombo get_Combo(string key, object key_val)
        {
            CCombo combo = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Combo] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        combo = new CCombo();

                        combo.id = (int)dr["id"];
                        combo.originPrice = (double)dr["originPrice"];
                        combo.priceType = dr["priceType"].ToString();
                        combo.price = ToDouble(dr["price"]);
                        combo.freePrice = ToDouble(dr["freePrice"]);
                        combo.expenseUpTo = ToDouble(dr["expenseUpTo"]);
                        combo.menuIds = dr["menuIds"].ToString();
                        combo.freeMenuIds = dr["freeMenuIds"].ToString();
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return combo;
        }

        public List<CCombo> get_Combos(string state_str, string order_str)
        {
            List<CCombo> combos = new List<CCombo>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Combo]";
                if (state_str != null && state_str != "")
                    cmd_str += " where " + state_str;

                if (order_str != null && order_str != "")
                    cmd_str += " " + order_str;
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var combo = new CCombo();

                        combo.id = (int)dr["id"];
                        combo.originPrice = (double)dr["originPrice"];
                        combo.priceType = dr["priceType"].ToString();
                        combo.price = ToDouble(dr["price"]);
                        combo.freePrice = ToDouble(dr["freePrice"]);
                        combo.expenseUpTo = ToDouble(dr["expenseUpTo"]);
                        combo.menuIds = dr["menuIds"].ToString();
                        combo.freeMenuIds = dr["freeMenuIds"].ToString();

                        combos.Add(combo);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return combos;
        }

        public CMemberSetting get_MemberSetting()
        {
            CMemberSetting memberSetting = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [MemberSetting]";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        memberSetting = new CMemberSetting();

                        memberSetting.id = (int)dr["id"];
                        memberSetting.money = ToInt(dr["money"]);
                        memberSetting.cardType = dr["cardType"].ToString();

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return memberSetting;
        }

        public CCardInfo get_CardInfo(string cmd_str)
        {
            CCardInfo cardInfo = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [CardInfo] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cardInfo = new CCardInfo();

                        cardInfo.CI_CardNo = dr["CI_CardNo"].ToString();
                        cardInfo.CI_CardTypeNo = ToInt(dr["CI_CardTypeNo"]);
                        cardInfo.CI_Name = dr["CI_Name"].ToString();
                        cardInfo.CI_Sexno = dr["CI_Sexno"].ToString();
                        cardInfo.CI_Address = dr["CI_Address"].ToString();
                        cardInfo.CI_Telephone = dr["CI_Telephone"].ToString();
                        cardInfo.CI_Remark = dr["CI_Remark"].ToString();
                        cardInfo.CI_SendCardDate = ToDateTime(dr["CI_SendCardDate"]);
                        cardInfo.CI_SendCardOperator = dr["CI_SendCardOperator"].ToString();
                        cardInfo.CI_CreditsUsed = ToInt(dr["CI_CreditsUsed"]);
                        cardInfo.birthday = ToDateTime(dr["birthday"]);
                        cardInfo.state = ToString(dr["state"]);
                        cardInfo.CI_Special1 = dr["CI_Special1"].ToString();
                        cardInfo.CI_SpecialDate1 = ToDateTime(dr["CI_SpecialDate1"]);
                        cardInfo.CI_Password = ToString(dr["CI_Password"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file("method=get_CardInfo,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return cardInfo;
        }

        public List<CCardInfo> get_CardInfos(string state_str)
        {
            List<CCardInfo> cardInfos = new List<CCardInfo>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [CardInfo]";
                if (state_str != null && state_str != "")
                    cmd_str += "where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var cardInfo = new CCardInfo();

                        cardInfo.CI_CardNo = dr["CI_CardNo"].ToString();
                        cardInfo.CI_CardTypeNo = ToInt(dr["CI_CardTypeNo"]);
                        cardInfo.CI_Name = dr["CI_Name"].ToString();
                        cardInfo.CI_Sexno = dr["CI_Sexno"].ToString();
                        cardInfo.CI_Address = dr["CI_Address"].ToString();
                        cardInfo.CI_Telephone = dr["CI_Telephone"].ToString();
                        cardInfo.CI_Remark = dr["CI_Remark"].ToString();
                        cardInfo.CI_SendCardDate = ToDateTime(dr["CI_SendCardDate"]);
                        cardInfo.CI_SendCardOperator = dr["CI_SendCardOperator"].ToString();
                        cardInfo.CI_CreditsUsed = ToInt(dr["CI_CreditsUsed"]);
                        cardInfo.birthday = ToDateTime(dr["birthday"]);
                        cardInfo.state = dr["state"].ToString();

                        cardInfos.Add(cardInfo);
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return cardInfos;
        }

        public CMemberType get_MemberType(string cmd_str)
        {
            CMemberType memberType = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [MemberType] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        memberType = new CMemberType();

                        memberType.id = (int)dr["id"];
                        memberType.name = dr["name"].ToString();
                        memberType.timSpan = dr["timSpan"].ToString();
                        memberType.times = ToInt(dr["times"]);
                        memberType.money = ToDouble(dr["money"]);
                        memberType.maxOpenMoney = ToDouble(dr["maxOpenMoney"]);
                        memberType.expireDate = ToDateTime(dr["expireDate"]);
                        memberType.offerId = ToInt(dr["offerId"]);
                        memberType.credits = (bool)dr["credits"];
                        memberType.smsAfterUsing = ToBool(dr["smsAfterUsing"]);
                        memberType.userOneTimeOneDay = ToBool(dr["userOneTimeOneDay"]);
                        memberType.LimitedTimesPerMonth = ToBool(dr["LimitedTimesPerMonth"]);
                        memberType.TimesPerMonth = ToInt(dr["TimesPerMonth"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return memberType;
        }

        public List<CCardCharge> get_CardCharges(string cmd_str)
        {
            List<CCardCharge> cardCharges = new List<CCardCharge>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [CardCharge] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var cardCharge = new CCardCharge();
                        cardCharge.CC_CardNo = dr["CC_CardNo"].ToString();
                        cardCharge.CC_ItemExplain = dr["CC_ItemExplain"].ToString();
                        cardCharge.CC_DebitSum = ToDouble(dr["CC_DebitSum"]);
                        cardCharge.CC_LenderSum = ToDouble(dr["CC_LenderSum"]);
                        cardCharge.CC_InputOperator = dr["CC_InputOperator"].ToString();
                        cardCharge.CC_InputDate = ToDateTime(dr["CC_InputDate"]);
                        cardCharge.CC_Station = dr["CC_Station"].ToString();
                        cardCharge.expense = ToDouble(dr["expense"]);
                        cardCharge.systemId = dr["systemId"].ToString();
                        cardCharge.expense = ToDouble(dr["expense"]);

                        cardCharges.Add(cardCharge);
                    }
                }

            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return cardCharges;
        }

        public CPromotion get_Promotion(string cmd_str)
        {
            CPromotion promotion = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Promotion] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        promotion = new CPromotion();

                        promotion.id = (int)dr["id"];
                        promotion.name = dr["name"].ToString();
                        promotion.status = (bool)dr["status"];
                        promotion.menuIds = dr["menuIds"].ToString();

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return promotion;
        }

        public CCatgory get_Catgory(string cmd_str)
        {
            CCatgory catgory = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Catgory] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        catgory = new CCatgory();

                        catgory.id = (int)dr["id"];
                        catgory.name = dr["name"].ToString();
                        catgory.kitchPrinterName = dr["kitchPrinterName"].ToString();

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return catgory;
        }

        public int get_member_balance(string card_no)
        {
            int balance = 0;
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select sum(cc_debitsum)-sum(cc_lendersum) from [CardCharge] where CC_CardNo='" + card_no + "'";
                SqlCommand cmd = new SqlCommand(cmd_str, sqlCn);
                object b = cmd.ExecuteScalar();

                if (b != DBNull.Value && b != null && b.ToString() != "")
                    balance = Convert.ToInt32(b);
            }
            catch (System.Exception e)
            {
                //BathClass.printErrorMsg(cmd_str);
                //IOUtil.insert_file(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return balance;
        }

        public CEmployee get_Employee(string state_str)
        {
            CEmployee user = null;
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Employee] where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        user = new CEmployee();
                        user.id = dr["id"].ToString();
                        user.name = dr["name"].ToString();
                        user.cardId = ToString(dr["cardId"]);
                        user.gender = dr["gender"].ToString();
                        user.birthday = (DateTime)dr["birthday"];
                        user.jobId = (int)dr["jobId"];
                        user.password = dr["password"].ToString();
                        user.phone = dr["phone"].ToString();
                        user.address = dr["address"].ToString();
                        user.email = dr["email"].ToString();
                        user.status = ToString(dr["status"]);
                        user.techStatus = ToString(dr["techStatus"]);
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                        //user = dr[""].ToString();
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return user;
        }

        public List<CCardPopSale> get_CardPopSales(string state_str)
        {
            List<CCardPopSale> cardPopSales = new List<CCardPopSale>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [CardPopSale]";
                if (state_str != null && state_str != "")
                    cmd_str += "where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var cardPopSale = new CCardPopSale();

                        cardPopSale.id = (int)dr["id"];
                        cardPopSale.mimMoney = ToInt(dr["mimMoney"]);
                        cardPopSale.saleMoney = ToInt(dr["saleMoney"]);

                        cardPopSales.Add(cardPopSale);
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return cardPopSales;
        }

        public CAccount get_account(string state_str)
        {
            CAccount account = null;
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Account] where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        account = new CAccount();

                        account.id = (int)dr["id"];
                        account.text = dr["text"].ToString();
                        account.systemId = dr["systemId"].ToString();
                        account.openTime = dr["openTime"].ToString();
                        account.openEmployee = dr["openEmployee"].ToString();
                        account.payTime = (DateTime)dr["payTime"];
                        account.payEmployee = dr["payEmployee"].ToString();
                        account.name = dr["name"].ToString();
                        account.promotionMemberId = dr["promotionMemberId"].ToString();
                        account.promotionAmount = ToDouble(dr["promotionAmount"]);
                        account.memberId = dr["memberId"].ToString();
                        account.serverEmployee = dr["serverEmployee"].ToString();
                        account.cash = ToDouble(dr["cash"]);
                        account.bankUnion = ToDouble(dr["bankUnion"]);
                        account.creditCard = ToDouble(dr["creditCard"]);
                        account.coupon = ToDouble(dr["coupon"]);
                        account.groupBuy = ToDouble(dr["groupBuy"]);
                        account.zero = ToDouble(dr["zero"]);
                        account.server = ToDouble(dr["server"]);
                        account.changes = ToDouble(dr["changes"]);
                        account.wipeZero = ToDouble(dr["wipeZero"]);
                        account.macAddress = dr["macAddress"].ToString();
                        account.abandon = dr["abandon"].ToString();
                        account.departmentId = ToInt(dr["departmentId"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return account;
        }

        public List<string> get_customers(string state_str)
        {
            List<string> customers = new List<string>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Customer]";
                if (state_str != null && state_str != "")
                    cmd_str += "where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        customers.Add(dr["name"].ToString());
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return customers;
        }

        public CCustomer get_customer(string state_str)
        {
            CCustomer customer = null;
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Customer] where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        customer = new CCustomer();

                        customer.id = (int)dr["id"];
                        customer.name = dr["name"].ToString();
                        customer.contact = dr["contact"].ToString();
                        customer.address = dr["address"].ToString();
                        customer.phone = dr["phone"].ToString();
                        customer.mobile = dr["mobile"].ToString();
                        customer.money = ToDouble(dr["money"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return customer;
        }

        public bool reset_seat(string state_str)
        {
            string cmd_str = reset_seat_string() + state_str + ")";
            return execute_command(cmd_str);
        }

        public string reset_seat_string()
        {
            return "update [Seat] set systemId=null,"
                + "name=null,population=null,openTime=null,openEmployee=null,"
                + "payTime=null,payEmployee=null,phone=null,discount=null,"
                + "discountEmployee=null,memberDiscount=null,memberPromotionId=null,"
                + "freeEmployee=null,chainId=null,status=1,ordering=null,paying=null,"
                + "note=null,unwarn=null,roomstatus=null,deposit=null,duetime=null where (";
        }

        //获取服务器当前时间
        public DateTime Now()
        {
            String nowtime = "";
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "select getdate() as systemtimes";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        nowtime = Convert.ToString(dr.GetDateTime(0));
                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return Convert.ToDateTime(nowtime);
        }

        //获取联台账号
        public string chainId()
        {
            string _chainId = "";
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = @"declare @maxId nvarchar(MAX)"
                        + @" declare @daystr nvarchar(MAX)"
                        + @" declare @headstr nvarchar(MAX)"
                        + @" declare @n nvarchar(MAX)"
                        + @" declare @nt int"
                        + @" select @maxid=max(chainId) from [Seat]"
                        + @" set @daystr=CONVERT(nvarchar(MAX), GETDATE(), 112)"
                        + @" set @headstr=substring(@maxId,0,10)"
                        + @" set @n=substring(@maxId, 10, 4)"
                        + @" set @nt=convert(int, @n)+1"
                        + @" set @maxId=@headstr+REPLICATE('0',4-DATALENGTH(CONVERT(VARCHAR,@nt)))+CONVERT(VARCHAR,@nt)"
                        + @" if(@maxId like 'L'+@daystr+'%')"
                        + @" select @maxId as chainId"
                        + @" else"
                        + @" select 'L'+@daystr+'0001' as chainId";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                object obj = cmdSelect.ExecuteScalar();
                _chainId = obj.ToString();

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return _chainId;
        }

        //获取系统账号
        public string systemId()
        {
            string _systemId = "";
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = @"declare @maxId nvarchar(MAX)"
                        + @" declare @daystr nvarchar(MAX)"
                        + @" declare @headstr nvarchar(MAX)"
                        + @" declare @nstr nvarchar(MAX)"
                        + @" declare @nint int"
                        + @" select @maxid=max(systemId) from [SystemIds]"
                        + @" set @daystr=CONVERT(nvarchar(MAX), GETDATE(), 112)"
                        + @" set @headstr=substring(@maxId,0,9)"
                        + @" set @nstr=substring(@maxId, 9, 5)"
                        + @" set @nint=convert(int, @nstr)+1"
                        + @" set @maxId=@headstr+REPLICATE('0',5-DATALENGTH(CONVERT(VARCHAR,@nint)))+CONVERT(VARCHAR,@nint)"
                        + @" if(@maxId like @daystr+'%')"
                        + @" select @maxId"
                        + @" else"
                        + @" select @daystr+'00001'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                object obj = cmdSelect.ExecuteScalar();
                _systemId = obj.ToString();
                //using (SqlDataReader dr = cmdSelect.ExecuteReader())
                //{
                //    while (dr.Read())
                //    {
                //        _systemId = dr["systemId"].ToString();
                //        break;
                //    }
                //}

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return _systemId;
        }

        //获取手牌绑定的项目
        public CMenu get_seat_menu(string text)
        {
            CMenu menu = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "select * from [Menu] where id=(select menuId from [seattype] where id=(select typeId from [seat] where text='"+text + "'))";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        menu = new CMenu();
                        menu.id = (int)dr["id"];
                        menu.name = dr["name"].ToString();
                        menu.catgoryId = (int)dr["catgoryId"];
                        menu.unit = dr["unit"].ToString();
                        menu.price = (double)dr["price"];
                        menu.technician = (bool)dr["technician"];
                        menu.waiter = ToBool(dr["waiter"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return menu;
        }

        public List<string> get_catgories(string state_str)
        {
            List<string> catgories = new List<string>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Catgory]";
                if (state_str != null && state_str != "")
                    cmd_str += "where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        catgories.Add(dr["name"].ToString());
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return catgories;
        }

        public List<string> get_cat_menus(string catgory)
        {
            List<string> menus = new List<string>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Menu]";
                if (catgory != null && catgory != "")
                    cmd_str += "where catgoryId=(select id from [Catgory] where name='" + catgory + "')";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        menus.Add(dr["name"].ToString());
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return menus;
        }

        public CBarMsg get_barMsg(string state_str)
        {
            CBarMsg barMsg = null;
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [BarMsg] where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        barMsg = new CBarMsg();

                        barMsg.id = (int)dr["id"];
                        barMsg.roomId = dr["roomId"].ToString();
                        barMsg.msg = dr["msg"].ToString();
                        barMsg.time = (DateTime)dr["time"];
                        barMsg.read = ToBool(dr["read"]);
                        barMsg.seatId = dr["seatId"].ToString();

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return barMsg;
        }

        public List<CRoom> get_rooms(string state_str)
        {
            List<CRoom> rooms = new List<CRoom>();
            SqlConnection sqlCn = null;
            string cmd_str = "";

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                cmd_str = "Select * from [Room]";
                if (state_str != null && state_str != "")
                    cmd_str += "where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var room = new CRoom();

                        room.id = (int)dr["id"];
                        room.name = dr["name"].ToString();
                        room.population = (int)dr["population"];
                        room.openTime = dr["openTime"].ToString();
                        room.seat = dr["seat"].ToString();
                        room.systemId = dr["systemId"].ToString();
                        room.orderTime = dr["orderTime"].ToString();
                        room.menu = dr["menu"].ToString();
                        room.orderTechId = dr["orderTechId"].ToString();
                        room.techId = dr["techId"].ToString();
                        room.startTime = dr["startTime"].ToString();
                        room.serverTime = dr["serverTime"].ToString();
                        room.status = dr["status"].ToString();
                        room.note = dr["note"].ToString();
                        room.hintPlayed = dr["hintPlayed"].ToString();
                        room.reserveId = dr["reserveId"].ToString();
                        room.reserveTime = dr["reserveTime"].ToString();
                        room.selectId = dr["selectId"].ToString();
                        room.seatIds = dr["seatIds"].ToString();

                        rooms.Add(room);
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return rooms;
        }

        public List<string> get_combo_menus(List<int> freeIds)
        {
            List<string> combo_menus = new List<string>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "select * from [Menu]";
                string state_str = "";
                int count = freeIds.Count;
                for (int i = 0; i < count; i++ )
                {
                    state_str += "id=" + freeIds[i];
                    if (i != count - 1)
                        state_str += " or ";
                }
                if (state_str != "")
                    cmd_str += " where " + state_str;

                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        combo_menus.Add(dr["name"].ToString());
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return combo_menus;
        }

        public List<string> get_order_menus(string state_str)
        {
            List<string> order_menus = new List<string>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select * from [Orders] where " + state_str;
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        order_menus.Add(dr["menu"].ToString());
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return order_menus;
        }

        public List<string> get_unDiscount_seat_systemIds(string state_str)
        {
            List<string> seat_ids = new List<string>();
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                string cmd_str = "Select systemId from [Seat] where (memberDiscount is null or memberDiscount=0) and (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        seat_ids.Add(dr["systemId"].ToString());
                    }
                }
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return seat_ids;
        }

        public bool insert_account(string cmd_str, ref int newAccountId)
        {
            bool return_val = false;
            SqlConnection sqlCn = null;
            SqlTransaction myTran;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
                return return_val;
            }

            myTran = sqlCn.BeginTransaction();

            try
            {
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                cmdSelect.Transaction = myTran;
                //cmdSelect.ExecuteNonQuery();
                object obj = cmdSelect.ExecuteScalar();
                newAccountId = (int)obj;
                return_val = true;
                myTran.Commit();
            }
            catch (System.Exception e)
            {
                myTran.Rollback();
                //IOUtil.insert_file(e.Message);
                //IOUtil.insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }

        public CTechMsg get_techMsg(string cmd_str)
        {
            CTechMsg techMsg = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        techMsg = new CTechMsg();
                        techMsg.id = (int)dr["id"];
                        techMsg.room = dr["room"].ToString();
                        techMsg.type = ToString(dr["type"]);
                        techMsg.techType = ToString(dr["techType"]);
                        techMsg.number = ToInt(dr["number"]);
                        techMsg.techId = ToString(dr["techId"]);
                        techMsg.time = (DateTime)dr["time"];
                        techMsg.printed = ToBool(dr["printed"]);
                        techMsg.read = (bool)dr["read"];
                        techMsg.seat = ToString(dr["seat"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file(e.Message);
                //IOUtil.insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return techMsg;
        }

        public int get_entities_count(string cmd_str)
        {
            SqlConnection sqlCn = null;
            int count = 0;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                //cmd_str = "Select count(*) from [Orders] where (" + state_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                object obj = cmdSelect.ExecuteScalar();
                count = (int)obj;

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file(DateTime.Now.ToString() + "=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return count;
        }

        public CTechIndex get_techIndex(string cmd_str)
        {
            CTechIndex techIndex = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        techIndex = new CTechIndex();
                        techIndex.id = (int)dr["id"];
                        techIndex.dutyid = ToInt(dr["dutyid"]);
                        techIndex.ids = dr["ids"].ToString();

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file(e.Message);
                //IOUtil.insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return techIndex;
        }

        /// <summary>
        /// 获取房间号
        /// </summary>
        /// <param name="cmd_str"></param>
        /// <returns></returns>
        public CRoom get_Room(string cmd_str)
        {
            CRoom room = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        room = new CRoom();
                        room.id = (int)dr["id"];
                        room.name = dr["name"].ToString();
                        room.seatIds = ToString(dr["seatIds"]);

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file(e.Message);
                //IOUtil.insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                //BathClass.printErrorMsg(cmd_str);
                IOUtil.insert_file(e.Message);
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return room;
        }

        public CStock get_Stock(string cmd_str)
        {
            CStock stock = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                //cmd_str = "Select * from [Stock] where (" + cmd_str + ")";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        stock = new CStock();
                        stock.id = (int)dr["id"];

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return stock;
        }

        //获取手牌所在房间
        public string get_seat_room(string seatId)
        {
            Room room = null;
            var db = new BathDBDataContext(_con_str);
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

        public string get_sms_msg(DateTime st, DateTime et)
        {
            StringBuilder sb_msg = new StringBuilder();
            sb_msg.Append("尊敬的各位领导，").Append(et.AddDays(-1).ToString("yyyy-MM-dd"));
            sb_msg.Append("营业信息如下：\n");
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                StringBuilder sb = new StringBuilder();
                sb.Append("declare @NewAct TABLE (cardCash float,cardBank float,saleCash float,");
                sb.Append("saleBank float,saleCredit float,saleCoupon float,saleGroup float,");
                sb.Append("saleZero float,saleServer float,saleChanges float,wipeZero float)");
                sb.Append(" declare @cardCash float");
                sb.Append(" declare @cardBank float");
                sb.Append(" declare @saleCash float");
                sb.Append(" declare @saleBank float");
                sb.Append(" declare @saleCredit float");
                sb.Append(" declare @saleCoupon float");
                sb.Append(" declare @saleGroup float");
                sb.Append(" declare @saleZero float");
                sb.Append(" declare @saleServer float");
                sb.Append(" declare @saleChanges float");
                sb.Append(" declare @wipeZero float");
                sb.Append(" select @cardCash=sum(isnull(cash,0)),@cardBank=sum(isnull(bankUnion,0)) from CardSale");
                sb.Append(" where payTime>='"+st.ToString("yyyy-MM-dd HH:mm:ss")+"' and payTime<='" +
                    et.ToString("yyyy-MM-dd HH:mm:ss")+"'");
                sb.Append(" select @saleCash=sum(isnull(cash,0)),@saleBank=sum(isnull(bankUnion,0)),@saleCredit=sum(isnull(creditCard,0)),");
                sb.Append(" @saleCoupon=sum(isnull(coupon,0)),@saleGroup=sum(isnull(groupBuy,0)),@saleZero=sum(isnull(zero,0)),");
                sb.Append(" @saleServer=sum(isnull(server,0)),@saleChanges=sum(isnull(changes,0)),@wipeZero=sum(isnull(wipeZero,0)) from account");
                sb.Append(" where payTime>='"+st.ToString("yyyy-MM-dd HH:mm:ss")+"' and payTime<='" +
                    et.ToString("yyyy-MM-dd HH:mm:ss")+"'");
                sb.Append(" insert into @NewAct(cardCash,cardBank,saleCash,");
                sb.Append("saleBank,saleCredit,saleCoupon,saleGroup,");
                sb.Append("saleZero,saleServer,saleChanges,wipeZero)");
                sb.Append(" values(@cardCash,@cardBank,@saleCash,");
                sb.Append("@saleBank,@saleCredit,@saleCoupon,@saleGroup,");
                sb.Append("@saleZero,@saleServer,@saleChanges,@wipeZero)");
                sb.Append(" select * from @NewAct");


                SqlCommand cmdSelect = new SqlCommand(sb.ToString(), sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        double saleCash = MConvert<double>.ToTypeOrDefault(dr["saleCash"], 0);
                        double saleBank = MConvert<double>.ToTypeOrDefault(dr["saleBank"], 0);
                        double saleCredit = MConvert<double>.ToTypeOrDefault(dr["saleCredit"], 0);
                        double saleCoupon = MConvert<double>.ToTypeOrDefault(dr["saleCoupon"], 0);
                        double saleGroup = MConvert<double>.ToTypeOrDefault(dr["saleGroup"], 0);
                        double saleZero = MConvert<double>.ToTypeOrDefault(dr["saleZero"], 0);
                        double saleServer = MConvert<double>.ToTypeOrDefault(dr["saleServer"], 0);
                        double saleChanges = MConvert<double>.ToTypeOrDefault(dr["saleChanges"], 0);
                        double wipeZero = MConvert<double>.ToTypeOrDefault((double)dr["wipeZero"], 0);
                        double cardCash = MConvert<double>.ToTypeOrDefault(dr["cardCash"], 0);
                        double cardBank = MConvert<double>.ToTypeOrDefault(dr["cardBank"], 0);

                        double total=saleCash+saleBank+saleCredit+saleCoupon+saleGroup+
                            saleZero+saleServer-saleChanges+wipeZero+cardBank+cardCash;
                        double actual = saleCash+saleBank-saleChanges+cardBank+cardCash;
                        sb_msg.Append("现金：").Append(saleCash-saleChanges).Append("\n");
                        sb_msg.Append("银联：").Append(saleBank).Append("\n");
                        sb_msg.Append("储值卡：").Append(saleCredit).Append("\n");
                        sb_msg.Append("优惠券：").Append(saleCoupon).Append("\n");
                        sb_msg.Append("团购：").Append(saleGroup).Append("\n");
                        sb_msg.Append("挂账：").Append(saleZero).Append("\n");
                        sb_msg.Append("招待：").Append(saleServer).Append("\n");
                        sb_msg.Append("抹零：").Append(wipeZero).Append("\n");
                        sb_msg.Append("现金售卡：").Append(cardCash).Append("\n");
                        sb_msg.Append("银联售卡：").Append(cardBank).Append("\n");
                        sb_msg.Append("总营业额：").Append(total).Append("\n");
                        sb_msg.Append("实际收入：").Append(actual).Append("\n");

                        break;
                    }
                }

            }
            catch (System.Exception e)
            {
                //IOUtil.insert_file("time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
                IOUtil.insert_file("\n");
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return sb_msg.ToString();
        }

        public CCardPopSale get_CardPopSale(string cmd_str)
        {
            CCardPopSale cardPopSale = null;
            SqlConnection sqlCn = null;

            try
            {
                sqlCn = new SqlConnection(_con_str);
                sqlCn.Open();

                //string cmd_str = "Select * from [Orders] where " + key + "='" + key_val.ToString() + "'";
                SqlCommand cmdSelect = new SqlCommand(cmd_str, sqlCn);
                using (SqlDataReader dr = cmdSelect.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        cardPopSale = new CCardPopSale();

                        cardPopSale.id = (int)dr["id"];
                        cardPopSale.mimMoney = ToInt(dr["mimMoney"]);
                        cardPopSale.saleMoney = ToInt(dr["saleMoney"]);
                    }
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return cardPopSale;
        }
    }
}
