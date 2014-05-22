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
        #region ��Ա����
        private static string m_printer = ""; //the name of the printer
        private static string m_seat;//���ƺ�
        private static string m_menu;//��Ŀ����
        private static string m_inputer;//¼��Ա
        private static string m_number;//����
        private static string m_time;//¼��ʱ��
        private static string m_room;//�����

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
                print_title(e, "������Ʒ");
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

        //��ӡ����
        public static void print_title(System.Drawing.Printing.PrintPageEventArgs e, string title)
        {
            //��ӡ������
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            int cLeft = (e.MarginBounds.Width - fsize) / 2 - e.MarginBounds.Left;
            print_str(e, subTitle, 15F, cLeft, tmpTop);
            tmpTop += str_h(e, 15F, subTitle);
            e.Graphics.DrawLine(DashPen, cLeft - fsize / 2, tmpTop + 10, cLeft + fsize * 3 / 2, tmpTop + 10);
            tmpTop += 30;
        }

        //��ӡ����
        public static void print_dash_line(System.Drawing.Printing.PrintPageEventArgs e, int y)
        {
            e.Graphics.DrawLine(DashPen, e.MarginBounds.Left, y, e.MarginBounds.Right, y);
        }

        //��ӡҳ��
        public static void print_footer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string footer = "�������ͿƼ�����֧��";
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

        //�ַ������
        public static int str_w(System.Drawing.Printing.PrintPageEventArgs e, Single s, string str)
        {
            Font f = new Font("SimSun", s, FontStyle.Regular);
            return (int)e.Graphics.MeasureString(str, f, e.MarginBounds.Width).Width;
        }

        //�ַ����߶�
        public static int str_h(System.Drawing.Printing.PrintPageEventArgs e, Single s, string str)
        {
            Font f = new Font("SimSun", s, FontStyle.Regular);
            return (int)e.Graphics.MeasureString(str, f, e.MarginBounds.Width).Height;
        }

        //��ӡ̨λ������Ϣ
        public static void print_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "����: " + m_seat;
            int h = str_h(e, 13F, str);
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "����: " + m_room;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "��Ŀ: " + m_menu;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "����: " + m_number;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "ʱ��: " + m_time;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "¼��: " + m_inputer;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;
        }

        //��ӡ�ַ���
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }
    }
}
