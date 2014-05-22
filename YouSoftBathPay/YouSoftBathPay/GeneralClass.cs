using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace YouSoftBathPay
{
    class GeneralClass
    {
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

        //获取MD5加密字符串
        public static string GetMD5(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Unicode.GetBytes(input));
            return Encoding.Default.GetString(res);
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
    }

    class GetStringSpell
    {
        /// <summary>   
        /// 提取汉字首字母   
        /// </summary>   
        /// <param name="strText">需要转换的字</param>   
        /// <returns>转换结果</returns>   
        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }
        /// <summary>   
        /// 把提取的字母变成大写   
        /// </summary>   
        /// <param name="strText">需要转换的字符串</param>   
        /// <returns>转换结果</returns>   
        public static string GetLowerChineseSpell(string strText)
        {
            return GetChineseSpell(strText).ToLower();
        }
        /// <summary>   
        /// 把提取的字母变成大写   
        /// </summary>   
        /// <param name="myChar">需要转换的字符串</param>   
        /// <returns>转换结果</returns>   
        public static string GetUpperChineseSpell(string strText)
        {
            return GetChineseSpell(strText).ToUpper();
        }
        /// <summary>   
        /// 获取单个汉字的首拼音   
        /// </summary>   
        /// <param name="myChar">需要转换的字符</param>   
        /// <returns>转换结果</returns>   
        public static string getSpell(string myChar)
        {
            byte[] arrCN = System.Text.Encoding.Default.GetBytes(myChar);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                    }
                }
                return "_";
            }
            else return myChar;
        }
    }
}
