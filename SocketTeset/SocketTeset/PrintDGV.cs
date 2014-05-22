using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Data;
using System.Text;

namespace YouSoftBathWatcher
{


    class PrintKitchen
    {
        #region 成员变量
        private static string m_printer = ""; //the name of the printer
        private static string m_seat;//手牌号
        private static string m_menu;//项目名称
        private static string m_inputer;//录单员
        private static string m_number;//数量
        private static string m_time;//录单时间
        private static string m_room;//包厢号

        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static int tmpTop;
        private static Pen DashPen = new Pen(Color.Black, (float)0.8);
        #endregion


        public static void Print_DataGridView(string seat, string menu, string printer, string inputer, 
            string number, string time, string room)
        {
            PrintPreviewDialog ppvw;
            try
            {
                // Getting DataGridView object to print
                m_seat = seat;
                m_menu = menu;
                m_printer = printer;
                m_inputer = inputer;
                m_number = number;
                m_time = time;
                m_room = room;

                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                printDoc.PrinterSettings.PrinterName = m_printer;
                printDoc.OriginAtMargins = true;
                printDoc.DefaultPageSettings.Margins.Left = 3;
                printDoc.DefaultPageSettings.Margins.Right = 3;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 100;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                //printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                //printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            tmpTop = e.MarginBounds.Top;

            try
            {
                print_title(e, "厨房出品");
                print_information(e);
                tmpTop += 20;

                print_dash_line(e, tmpTop + 40);
                tmpTop += 50;
                print_footer(e);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //打印标题
        public static void print_title(System.Drawing.Printing.PrintPageEventArgs e, string title)
        {
            //打印副标题
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            int cLeft = (e.MarginBounds.Width - fsize) / 2 - e.MarginBounds.Left;
            print_str(e, subTitle, 15F, cLeft, tmpTop);
            tmpTop += str_h(e, 15F, subTitle);
            e.Graphics.DrawLine(DashPen, cLeft - fsize / 2, tmpTop + 10, cLeft + fsize * 3 / 2, tmpTop + 10);
            tmpTop += 30;
        }

        //打印虚线
        public static void print_dash_line(System.Drawing.Printing.PrintPageEventArgs e, int y)
        {
            e.Graphics.DrawLine(DashPen, e.MarginBounds.Left, y, e.MarginBounds.Right, y);
        }

        //打印页脚
        public static void print_footer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string footer = "杭州连客科技技术支持";
            int foolLeft = (e.MarginBounds.Width - str_w(e, 12F, footer)) / 2 - e.MarginBounds.Left;
            print_str(e, footer, 12F, foolLeft, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            footer = "TEL:186-7006-8930  188-5719-1220";
            foolLeft = (e.MarginBounds.Width - str_w(e, 12F, footer)) / 2 - e.MarginBounds.Left;
            print_str(e, footer, 12F, foolLeft, tmpTop);
            tmpTop += str_h(e, 20F, footer);

            footer = "";
            print_str(e, footer, 20F, foolLeft, tmpTop);
        }

        //字符串宽度
        public static int str_w(System.Drawing.Printing.PrintPageEventArgs e, Single s, string str)
        {
            Font f = new Font("SimSun", s, FontStyle.Regular);
            return (int)e.Graphics.MeasureString(str, f, e.MarginBounds.Width).Width;
        }

        //字符串高度
        public static int str_h(System.Drawing.Printing.PrintPageEventArgs e, Single s, string str)
        {
            Font f = new Font("SimSun", s, FontStyle.Regular);
            return (int)e.Graphics.MeasureString(str, f, e.MarginBounds.Width).Height;
        }

        //打印台位基本信息
        public static void print_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "手牌: " + m_seat;
            int h = str_h(e, 13F, str);
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "包厢: " + m_room;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "项目: " + m_menu;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "数量: " + m_number;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "时间: " + m_time;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "录单: " + m_inputer;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }
    }
}
