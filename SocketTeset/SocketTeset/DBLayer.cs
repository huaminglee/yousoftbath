using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace SocketTeset
{
    public class DBLayer
    {
        private string _con_str;

        //构造函数
        public DBLayer(string con_str)
        {
            _con_str = con_str;
        }

        public string connectionString
        {
            get { return _con_str; }
            set { _con_str = value; }
        }

        //关闭网络连接
        public void close_connection(SqlConnection sqlCn)
        {
            if (sqlCn != null && sqlCn.State == System.Data.ConnectionState.Open)
                    sqlCn.Close();
        }

        public bool execute_command(string cmd_str)
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
                //BathClass.printErrorMsg(e.ToString());
                //return return_val;
            }
            if (sqlCn == null || sqlCn.State != System.Data.ConnectionState.Open)
                return return_val;

            myTran = sqlCn.BeginTransaction();

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
                insert_file(e.ToString());
                insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return return_val;
        }

        //写入文件
        public static void insert_file(string msg)
        {
            //if (File.Exists(@".\Log\error.log"))
            //File.Delete(@".\Log\error.log");

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

        private int? ToInt(object obj)
        {
            if (obj == DBNull.Value || obj == null)
                return null;
            else
                return Convert.ToInt32(obj);
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
                insert_file(e.ToString());
                insert_file("method=execute_command,time=" + DateTime.Now.ToString() + ",cmd_str=" + cmd_str);
            }
            finally
            {
                close_connection(sqlCn);
            }

            return techIndex;
        }
    }

    #region 表TechIndex

    public class CTechIndex
    {
        private int _id;

        private System.Nullable<int> _dutyid;

        private string _ids;

        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((this._id != value))
                {
                    this._id = value;
                }
            }
        }

        public System.Nullable<int> dutyid
        {
            get
            {
                return this._dutyid;
            }
            set
            {
                if ((this._dutyid != value))
                {
                    this._dutyid = value;
                }
            }
        }

        public string ids
        {
            get
            {
                return this._ids;
            }
            set
            {
                if ((this._ids != value))
                {
                    this._ids = value;
                }
            }
        }
    }

    #endregion
}
