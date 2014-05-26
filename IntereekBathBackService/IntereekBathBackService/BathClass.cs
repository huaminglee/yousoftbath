using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace IntereekBathBackService
{
    public class BathClass
    {
        public static string ConvertToJsonString(Account account)
        {
            return new JavaScriptSerializer().Serialize(new
            {
                id = account.id,
                text = account.text,
                systemId = account.systemdId,
                openTime = account.openTime,
                openEmployee = account.openEmployee,
                payTime = account.payTime.ToString("yyyy-MM-dd HH:mm:ss"),
                payEmployee = account.payEmployee,
                name = account.name,
                promotionMemberId = account.promotionMemberId,
                promotionAmount = account.promotionAmount,
                memberId = account.memberId,
                discountEmployee = account.discountEmployee,
                discount = account.discount,
                serverEmployee = account.serverEmployee,
                cash = account.cash,
                bankUnion = account.bankUnion,
                creditCard = account.creditCard,
                coupon = account.coupon,
                groupBuy = account.groupBuy,
                zero = account.zero,
                server = account.server,
                deducted = account.deducted,
                changes = account.changes,
                wipeZero = account.wipeZero,
                abandon = account.abandon,
                departmentId = account.departmentId
            });
        }

        public static string ConvertToJsonString(ClearTable clearTable)
        {
            return new JavaScriptSerializer().Serialize(new
            {
                id = clearTable.id,
                clearTime = clearTable.clearTime.ToString("yyyy-MM-dd HH:mm:ss")
            });
        }

        public static string ConvertToJsonString(Employee employee)
        {
            return new JavaScriptSerializer().Serialize(new
            {
                id = employee.id,
                name = employee.name,
                gender = employee.gender,
                birthday = employee.birthday.ToString("yyyy-MM-dd HH:mm:ss"),
                jobId = employee.jobId,
                salary = employee.salary,
                password = employee.password,
                phone = employee.phone,
                note = employee.note
            });
        }

        public static string ConvertToJsonString(CardSale cardSale)
        {
            return new JavaScriptSerializer().Serialize(new
            {
                id = cardSale.id,
                memberId = cardSale.memberId,
                balance = cardSale.balance,
                cash = cardSale.cash,
                bankUnion = cardSale.bankUnion,
                payTime = cardSale.payTime.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                //explain = cardSale.explain,
                abandon = cardSale.abandon,
                server = cardSale.server
            });
        }

        #region 配置文件
        //根据key值获取配置
        public static string get_config_by_key(string key)
        {
            if (!Directory.Exists(@".\config"))
                return "";

            if (!File.Exists(@".\config\config.ini"))
                return "";

            string val = "";
            using (StreamReader sr = new StreamReader(@".\config\config.ini"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] vals = line.Split('=');
                    if (vals.Length == 2 && vals[0] == key)
                        return vals[1];
                }
            }
            return val;
        }

        //根据key值、val值更改或者写入配置文件
        public static void set_config_by_key(string key, string val)
        {
            try
            {
                if (!Directory.Exists(@".\config"))
                    Directory.CreateDirectory(@".\config");
                if (!File.Exists(@".\config\config.ini"))
                {
                    FileStream fs = new FileStream(@".\config\config.ini", FileMode.Create);
                    fs.Close();
                }

                string infor = "";
                infor = key + "=" + val + "\r\n";
                using (StreamReader sr = new StreamReader(@".\config\config.ini"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] vals = line.Split('=');
                        if (vals.Length != 0 && vals[0] == key)
                            continue;
                        else
                            infor += line + "\r\n";
                    }
                }

                using (StreamWriter sw = new StreamWriter(@".\config\config.ini", false))
                    sw.Write(infor);
            }
            catch
            {}
        }

        #endregion

        public static string getErrorFileName()
        {
            return DateTime.Now.ToString("yyMMdd") + ".log";
        }



    }

}
