using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace YouSoftUtil.Shop
{
    public class ShopManagement
    {
        public static List<ShopYeJi> queryYeJi(string ip, List<string> companyCodes, string date, string dateType, out string errorDesc)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "queryMultiData",
                table = "account",
                companyCode = new JavaScriptSerializer().Serialize(companyCodes),
                dateType = dateType,
                date = date
            });

            var jsresut = HttpCon<List<ShopYeJi>>.run_json(ip, json, out errorDesc);
            return jsresut;
        }


        public static bool registerCompany(string ip, string companyCode, string companyName, string companyTel, string companyAddr, out string errorDesc)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                data = new JavaScriptSerializer().Serialize(new
                {
                    companyName = companyName,
                    companyTel = companyTel,
                    companyAdd = companyAddr
                    //registerDate = "2014-05-01",
                    //dueDate = "2014-05-08"
                }),
                operationType = "registerCompany",
                companyCode = companyCode
            });

            return HttpCon<bool>.run_json_for_succes(ip, json, out errorDesc); ;
        }
    }
}
