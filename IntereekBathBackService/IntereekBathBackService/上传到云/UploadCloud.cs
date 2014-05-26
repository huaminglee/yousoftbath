using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using YouSoftUtil;
using YouSoftBathConstants;

namespace IntereekBathBackService
{
    public class UploadCloud
    {
        private Thread thread_account = null;
        private Thread thread_clearTable = null;
        private Thread thread_cardsale = null;

        private string connectionString;
        private string company_code;
        
        public UploadCloud(string _connectionString)
        {
            connectionString = _connectionString;
        }

        public void start()
        {
            var db = new BathDBDataContext(connectionString);
            company_code = db.Options.FirstOrDefault().companyCode;
            if (StringUtil.isEmpty(company_code))
            {
                MessageBox.Show("没有注册公司代码，请联系连客科技注册!");
                return;
            }

            if (thread_clearTable == null)
            {
                thread_clearTable = new Thread(run_clearTable);
                thread_clearTable.IsBackground = true;
            }
            thread_clearTable.Start();

            if (thread_cardsale == null)
            {
                thread_cardsale = new Thread(run_cardSale);
                thread_cardsale.IsBackground = true;
            }
            thread_cardsale.Start();

            if (thread_account == null)
            {
                thread_account = new Thread(run_account);
                thread_account.IsBackground = true;
            }
            thread_account.Start();
        }

        //上传ClearTable数据
        private void run_clearTable()
        {
            while (true)
            {
                try
                {
                    var db = new BathDBDataContext(connectionString);
                    long maxId = -1;
                    DateTime clearTime = DateTime.Now;
                    var uploadRecords = db.UploadRecords.FirstOrDefault(x => x.tableName == "ClearTable");
                    if (uploadRecords != null)
                    {
                        maxId = MConvert<long>.ToTypeOrDefault(uploadRecords.maxId, -1);
                        clearTime = MConvert<DateTime>.ToTypeOrDefault(uploadRecords.clearTime, clearTime);
                    }
                    else
                    {
                        uploadRecords = new UploadRecords();
                        uploadRecords.tableName = "ClearTable";
                        db.UploadRecords.InsertOnSubmit(uploadRecords);
                        db.SubmitChanges();
                    }

                    var clearTables = db.ClearTable.Where(x => x.id > maxId);
                    var _clearTables = db.ClearTable.Where(x => x.id == maxId && x.clearTime != clearTime);
                    clearTables = clearTables.Union(_clearTables).Distinct();

                    bool changed = false;
                    foreach (var clearTable in clearTables)
                    {
                        bool sucess = false;
                        while (!sucess)
                        {
                            sucess = upload_json(BathClass.ConvertToJsonString(clearTable), "ClearTable");
                            if (sucess)
                            {
                                changed = true;
                                if (maxId < clearTable.id)
                                {
                                    maxId = clearTable.id;
                                    uploadRecords.maxId = clearTable.id;
                                }
                                if (clearTable.clearTime != clearTime)
                                {
                                    clearTime = clearTable.clearTime;
                                    uploadRecords.clearTime = clearTable.clearTime;
                                }
                                db.SubmitChanges();
                            }
                        }
                    }
                    if (changed)
                        db.SubmitChanges();
                }
                catch (System.Exception ex)
                {

                }
            }
        }

        //上传会员卡销售
        private void run_cardSale()
        {
            while (true)
            {
                try
                {
                    var db = new BathDBDataContext(connectionString);

                    long maxId = -1;
                    var abandonIds = new List<long>();
                    var uploadRecords = db.UploadRecords.FirstOrDefault(x => x.tableName == "CardSale");
                    if (uploadRecords != null)
                    {
                        maxId = MConvert<long>.ToTypeOrDefault(uploadRecords.maxId, -1);
                        if (uploadRecords.abandonId != null)
                        {
                            var idArray = uploadRecords.abandonId.Split(Constants.SplitChar);
                            abandonIds = Array.ConvertAll<string, long>(idArray, delegate(string s) { return MConvert<long>.ToTypeOrDefault(s, -1); }).ToList();
                        }
                    }
                    else
                    {
                        uploadRecords = new UploadRecords();
                        uploadRecords.tableName = "CardSale";
                        db.UploadRecords.InsertOnSubmit(uploadRecords);
                        db.SubmitChanges();
                    }

                    #region 更新abandonIds,去除掉夜审之前的，以免abandonIds过长

                    var maxTime = DateTime.Parse("2013-01-01 00:00:00");
                    if (db.ClearTable.Any())
                        maxTime = db.ClearTable.Max(x => x.clearTime);
                    var max_passed_id = db.CardSale.Where(x => x.payTime <= maxTime).Max(x => x.id);
                    abandonIds.RemoveAll(x => x <= max_passed_id);

                    var _idArray = Array.ConvertAll<long, string>(abandonIds.ToArray(), delegate(long s) { return s.ToString(); });
                    uploadRecords.abandonId = string.Join(Constants.SplitChar.ToString(), _idArray);
                    db.SubmitChanges();

                    #endregion

                    var cardSales = db.CardSale.Where(x => x.id > maxId);
                    var _cardSales = db.CardSale.Where(x => x.payTime >= maxTime && x.abandon != null && !abandonIds.Contains(x.id));
                    cardSales = cardSales.Union(_cardSales).Distinct();

                    bool changed = false;
                    foreach (var cardSale in cardSales)
                    {
                        bool sucess = false;
                        while (!sucess)
                        {
                            sucess = upload_json(BathClass.ConvertToJsonString(cardSale), "CardSale");
                            if (sucess)
                            {
                                changed = true;
                                if (maxId < cardSale.id)
                                {
                                    maxId = cardSale.id;
                                    uploadRecords.maxId = cardSale.id;
                                }
                                if (cardSale.abandon != null && cardSale.id >= max_passed_id)
                                {
                                    uploadRecords.abandonId += Constants.SplitChar + cardSale.id.ToString();
                                }
                            }
                        }
                    }
                    if (changed)
                        db.SubmitChanges();
                }
                catch (System.Exception ex)
                {

                }
            }
        }

        //上传账单数据
        private void run_account()
        {
            while (true)
            {
                try
                {
                    var db = new BathDBDataContext(connectionString);

                    long maxId = -1;
                    var abandonIds = new List<long>();
                    var account_uploadRecords = db.UploadRecords.FirstOrDefault(x => x.tableName == "Account");
                    if (account_uploadRecords != null)
                    {
                        maxId = MConvert<long>.ToTypeOrDefault(account_uploadRecords.maxId, -1);
                        if (account_uploadRecords.abandonId != null)
                        {
                            var idArray = account_uploadRecords.abandonId.Split(Constants.SplitChar);
                            abandonIds = Array.ConvertAll<string, long>(idArray, delegate(string s) { return MConvert<long>.ToTypeOrDefault(s, -1); }).ToList();
                        }
                    }
                    else
                    {
                        account_uploadRecords = new UploadRecords();
                        account_uploadRecords.tableName = "Account";
                        db.UploadRecords.InsertOnSubmit(account_uploadRecords);
                        db.SubmitChanges();
                    }

                    #region 更新abandonIds,去除掉夜审之前的，以免abandonIds过长

                    var maxTime = DateTime.Parse("2013-01-01 00:00:00");
                    if (db.ClearTable.Any())
                        maxTime = db.ClearTable.Max(x => x.clearTime);
                    var max_passed_account_id = db.Account.Where(x => x.payTime <= maxTime).Max(x => x.id);
                    abandonIds.RemoveAll(x => x <= max_passed_account_id);

                    var _idArray = Array.ConvertAll<long, string>(abandonIds.ToArray(), delegate(long s) { return s.ToString(); });
                    account_uploadRecords.abandonId = string.Join(Constants.SplitChar.ToString(), _idArray);
                    db.SubmitChanges();

                    #endregion

                    var accounts = db.Account.Where(x => x.id > maxId);
                    var _accounts = db.Account.Where(x => x.payTime >= maxTime && x.abandon != null && !abandonIds.Contains(x.id));
                    accounts = accounts.Union(_accounts).Distinct();

                    bool changed = false;
                    foreach (var account in accounts)
                    {
                        bool sucess = false;
                        while (!sucess)
                        {
                            sucess = upload_json(BathClass.ConvertToJsonString(account), "account");
                            if (sucess)
                            {
                                changed = true;
                                if (maxId < account.id)
                                {
                                    maxId = account.id;
                                    account_uploadRecords.maxId = account.id;
                                }
                                if (account.abandon != null && account.id >= max_passed_account_id)
                                {
                                    account_uploadRecords.abandonId += Constants.SplitChar + account.id.ToString();
                                }
                            }
                        }
                    }

                    if (changed)
                        db.SubmitChanges();
                }
                catch (System.Exception ex)
                {
                    IOUtil.insert_file(BathClass.getErrorFileName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "run_account" + ex.Message);
                }
            }
        }

        private bool upload_json(string data_json, string table)
        {
            bool success = false;
            string webAdd = @"http://" + Constants.AliIP + ":" + Constants.AliPort + @"/pangu/pangu.do?requestData=";

            string json = new JavaScriptSerializer().Serialize(new
            {
                data = data_json,
                operationType = "uploadData",
                table = table,
                companyCode = company_code
            });
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAdd + json);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";

            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var jsonUploadResult = js.Deserialize<JsonUploadResult>(result);
                    if (jsonUploadResult.success)
                        success = true;
                    else
                        IOUtil.insert_file(BathClass.getErrorFileName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + json + "\r\n" + jsonUploadResult.errorCode + "\r\n" + jsonUploadResult.errorDesc + "\r\n");
                }
            }
            catch (System.Exception ex)
            {
                IOUtil.insert_file(BathClass.getErrorFileName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message);
            }

            return success;
        }

        public void stop()
        {
            thread_account.Abort();
            thread_clearTable.Abort();
            thread_cardsale.Abort();
        }

    }
}
