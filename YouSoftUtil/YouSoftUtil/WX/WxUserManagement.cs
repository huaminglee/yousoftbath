using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace YouSoftUtil.WX
{
    public class WxUserManagement
    {
        public static List<WxUser> queryWxUser(string ip, string nickName, string companyCode, out string errorDesc)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "queryWxUser",
                nickName = nickName,
                companyCode = companyCode
            });

            var jsonResult = HttpCon<WxUserResult>.run_json(ip, json, out errorDesc);
            if (jsonResult == null)
                return null;

            return jsonResult.wxUserList;
        }
    }
}
