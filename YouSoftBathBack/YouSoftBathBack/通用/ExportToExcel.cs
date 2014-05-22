using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace YouSoftBathGeneralClass
{
    public class ExportToExcel
    {
        /// <summary>
        /// DataGridView导出Excel
        /// </summary>
        /// <param name="strCaption">Excel文件中的标题</param>
        /// <param name="myDGV">DataGridView 控件</param>
        /// <returns>0:成功;1:DataGridView中无记录;2:Excel无法启动;9999:异常错误</returns>
        public static int ExportExcel(string strCaption, DataGridView myDGV)
        {
            int result = 9999;
            //保存
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = strCaption;
            //saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "Export Excel File";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName == "")
                {
                    MessageBox.Show("请输入保存文件名！");
                    saveFileDialog.ShowDialog();
                }
                // 列索引，行索引，总列数，总行数
                int ColIndex = 0;
                int RowIndex = 0;
                int ColCount = 0;
                foreach (DataGridViewColumn c in myDGV.Columns)
                {
                    if (c.Visible)
                        ColCount++;
                }
                int RowCount = myDGV.RowCount + 1;
                if (myDGV.RowCount == 0)
                {
                    result = 1;
                }
                // 创建Excel对象
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                if (xlApp == null)
                {
                    result = 2;
                }
                try
                {
                    // 创建Excel工作薄
                    Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);
                    Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                    // 设置标题
                    Microsoft.Office.Interop.Excel.Range range = xlSheet.get_Range(xlApp.Cells[2, 2], xlApp.Cells[2, ColCount+1]); //标题所占的单元格数与DataGridView中的列数相同
                    range.MergeCells = true;
                    range.Borders.LineStyle = 1;

                    range.FormulaR1C1 = strCaption;
                    range.Font.Size = 20;
                    range.Font.Bold = true;
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.Constants.xlCenter;

                    // 创建缓存数据
                    object[,] objData = new object[RowCount + 1, ColCount];
                    //获取列标题
                    foreach (DataGridViewColumn col in myDGV.Columns)
                    {
                        if (!col.Visible)
                            continue;
                        objData[RowIndex, ColIndex++] = col.HeaderText;
                    }

                    // 获取数据
                    for (RowIndex = 1; RowIndex < RowCount; RowIndex++)
                    {
                        for (ColIndex = 0; ColIndex < myDGV.Columns.Count; ColIndex++)
                        {
                            if (!myDGV.Columns[ColIndex].Visible)
                                continue;
                          /*内容修正
                            if (myDGV[ColIndex, RowIndex - 1].ValueType == typeof(string)
                                || myDGV[ColIndex, RowIndex - 1].ValueType == typeof(DateTime))//这里就是验证DataGridView单元格中的类型,如果是string或是DataTime类型,则在放入缓存时在该内容前加入" ";
                            {
                                objData[RowIndex, ColIndex] = " " + myDGV[ColIndex, RowIndex - 1].Value;
                            }
                            else
                            {
                                objData[RowIndex, ColIndex] = myDGV[ColIndex, RowIndex - 1].Value;
                            }*/

                            objData[RowIndex, ColIndex] = "'" + myDGV[ColIndex, RowIndex - 1].Value;
                            //在项目中，通过Excel表格特性，在单元格内容前加上单引号之后，不管是数字，和其或是时间都会以字符串形式展现。
                            //if (myDGV[ColIndex, RowIndex - 1].ValueType == typeof(string))//这里就是验证DataGridView单元格中的类型,如果是string类型,则在放入缓存时在该内容前加入" ";
                            //{
                            //    objData[RowIndex, ColIndex] = "'" + myDGV[ColIndex, RowIndex - 1].Value;
                            //}
                            //else if (myDGV[ColIndex, RowIndex - 1].ValueType == typeof(DateTime))//这里就是验证DataGridView单元格中的类型,如果是DataTime类型,则在放入缓存时在该内容前加入" ";
                            //{
                            //    objData[RowIndex, ColIndex] =  "'"+ myDGV[ColIndex, RowIndex - 1].Value;
                            //}
                            //else
                            //{
                            //    objData[RowIndex, ColIndex] = myDGV[ColIndex, RowIndex - 1].Value;
                            //}
                        }
                        System.Windows.Forms.Application.DoEvents();
                    }
                    // 写入Excel
                    range = xlSheet.get_Range(xlApp.Cells[3, 2], xlApp.Cells[RowCount+2, ColCount+1]);
                    range.Borders.LineStyle = 1;
                    range.Value2 = objData;
                    range.Font.Size = 11;
                    range.EntireColumn.AutoFit();

                    range = xlSheet.get_Range(xlApp.Cells[3, 2], xlApp.Cells[3, ColCount + 1]);
                    range.Font.Bold = true;
                    range.EntireColumn.AutoFit();

                    xlBook.Saved = true;
                    xlBook.SaveCopyAs(saveFileDialog.FileName);
                }
                catch (Exception err)
                {
                    result = 9999;
                }
                finally
                {
                    xlApp.Quit();
                    GC.Collect(); //强制回收
                }
                //返回值
                result = 0;
            }
            return result;
        }
    }
}
