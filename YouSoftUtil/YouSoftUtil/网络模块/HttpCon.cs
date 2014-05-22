using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using YouSoftBathConstants;
using System.Web.Script.Serialization;

namespace YouSoftUtil
{
    public class HttpCon<T>
    {
        public static T run_json(string ip, string json, out string errorDesc)
        {
            string webAdd = @"http://" + ip + ":" + Constants.AliPort + @"/pangu/pangu.do?requestData=";

            string result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAdd + json);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            try
            {
                errorDesc = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                var jsresut = new JavaScriptSerializer().Deserialize<JSONResult<T>>(result);

                if (jsresut == null)
                {
                    errorDesc = "网络连接失败";
                    return default(T);
                }

                if (!jsresut.success)
                {
                    errorDesc = jsresut.errorDesc;
                    return default(T);
                }

                return jsresut.result;
            }
            catch (System.Exception ex)
            {
                errorDesc = ex.Message;
                IOUtil.insert_file(Constants.ErrorFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message);
            }

            return default(T);
        }

        public static bool run_json_for_succes(string ip, string json, out string errorDesc)
        {
            string webAdd = @"http://" + ip + ":" + Constants.AliPort + @"/pangu/pangu.do?requestData=";

            string result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAdd + json);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            try
            {
                errorDesc = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
                var jsresut = new JavaScriptSerializer().Deserialize<JSONResult<T>>(result);

                if (jsresut == null)
                {
                    errorDesc = "网络连接失败";
                    return false;
                }

                if (!jsresut.success)
                {
                    errorDesc = jsresut.errorDesc;
                    return false;
                }

                return jsresut.success;
            }
            catch (System.Exception ex)
            {
                errorDesc = ex.Message;
                IOUtil.insert_file(Constants.ErrorFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message);
            }

            return false;
        }
    }
}
