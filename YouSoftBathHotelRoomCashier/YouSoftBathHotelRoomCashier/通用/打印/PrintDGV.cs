using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Text;
using System.Linq;
using YouSoftBathGeneralClass;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using YouSoftBathFormClass;

namespace YouSoftBathReception
{
    public class PrintDGV
    {
        private static StringFormat StrFormat;  // Holds content of a TextBox Cell to write by DrawString
        private static StringFormat StrFormatComboBox; // Holds content of a Boolean Cell to write by DrawImage
        private static Button CellButton;       // Holds the Contents of Button Cell
        private static CheckBox CellCheckBox;   // Holds the Contents of CheckBox Cell 
        private static ComboBox CellComboBox;   // Holds the Contents of ComboBox Cell

        private static int TotalWidth;          // Summation of Columns widths
        private static int RowPos;              // Position of currently printing row 
        private static bool NewPage;            // Indicates if a new page reached
        private static int PageNo;              // Number of pages to print
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns
        private static ArrayList ColumnWidths = new ArrayList(); // Width of Columns
        private static ArrayList ColumnTypes = new ArrayList();  // DataType of Columns
        private static int CellHeight;          // Height of DataGrid Cell
        private static int RowsPerPage;         // Number of Rows per Page
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static string PrintTitle = "";  // Header of pages
        private static DataGridView dgv;        // Holds DataGridView Object to print its contents
        private static List<string> SelectedColumns = new List<string>();   // The Columns Selected by user to print.
        private static List<string> AvailableColumns = new List<string>();  // All Columns avaiable in DataGrid 
        private static bool PrintAllRows = true;   // True = print all rows,  False = print selected rows    
        private static bool FitToPageWidth = true; // True = Fits selected columns to page width ,  False = Print columns as showed    
        private static int HeaderHeight = 0;

        public static void Print_DataGridView(DataGridView dgv1)
        {
            PrintPreviewDialog ppvw;
            try 
	        {	
                // Getting DataGridView object to print
                dgv = dgv1;

                // Getting all Coulmns Names in the DataGridView
                AvailableColumns.Clear();
                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    if (!c.Visible) continue;
                    AvailableColumns.Add(c.HeaderText);
                }

                // Showing the PrintOption Form
                PrintOptions dlg = new PrintOptions(AvailableColumns);
                if (dlg.ShowDialog() != DialogResult.OK) return;

                PrintTitle = dlg.PrintTitle;
                PrintAllRows = dlg.PrintAllRows;
                FitToPageWidth = dlg.FitToPageWidth;
                SelectedColumns = dlg.GetSelectedColumns();
                printDoc.PrinterSettings.PrinterName = dlg.printName;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                RowsPerPage = 0;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint +=new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage +=new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                //if (ppvw.ShowDialog() != DialogResult.OK)
                //{
                //    printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                //    printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
                //    return;
                //}

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
	        }
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);        		
	        }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender, 
                    System.Drawing.Printing.PrintEventArgs e) 
        {
            try
	        {
                // Formatting the Content of Text Cell to print
                StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Near;
                StrFormat.LineAlignment = StringAlignment.Center;
                StrFormat.Trimming = StringTrimming.EllipsisCharacter;

                // Formatting the Content of Combo Cells to print
                StrFormatComboBox = new StringFormat();
                StrFormatComboBox.LineAlignment = StringAlignment.Center;
                StrFormatComboBox.FormatFlags = StringFormatFlags.NoWrap;
                StrFormatComboBox.Trimming = StringTrimming.EllipsisCharacter;

                ColumnLefts.Clear();
                ColumnWidths.Clear();
                ColumnTypes.Clear();
                CellHeight = 0;
                RowsPerPage = 0;

                // For various column types
                CellButton = new Button();
                CellCheckBox = new CheckBox();
                CellComboBox = new ComboBox();

                // Calculating Total Widths
                TotalWidth = 0;
                foreach (DataGridViewColumn GridCol in dgv.Columns)
                {
                    if (!GridCol.Visible) continue;
                    if (!PrintDGV.SelectedColumns.Contains(GridCol.HeaderText)) continue;
                    TotalWidth += GridCol.Width;
                }
                PageNo = 1;
                NewPage = true;
                RowPos = 0;        		
	        }
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);        		
	        }
        }

        private static void PrintDoc_PrintPage(object sender, 
                    System.Drawing.Printing.PrintPageEventArgs e) 
        {
            int tmpWidth, i;
            int tmpTop = e.MarginBounds.Top;
            int tmpLeft = e.MarginBounds.Left;

            try 
	        {	        
                // Before starting first page, it saves Width & Height of Headers and CoulmnType
                if (PageNo == 1) 
                {
                    foreach (DataGridViewColumn GridCol in dgv.Columns)
                    {
                        if (!GridCol.Visible) continue;
                        // Skip if the current column not selected
                        if (!PrintDGV.SelectedColumns.Contains(GridCol.HeaderText)) continue;

                        // Detemining whether the columns are fitted to page or not.
                        if (FitToPageWidth) 
                            tmpWidth = (int)(Math.Floor((double)((double)GridCol.Width / 
                                       (double)TotalWidth * (double)TotalWidth * 
                                       ((double)e.MarginBounds.Width / (double)TotalWidth))));
                        else
                            tmpWidth = GridCol.Width;

                        HeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, tmpWidth).Height) + 11;
                        
                        // Save width & height of headres and ColumnType
                        ColumnLefts.Add(tmpLeft);
                        ColumnWidths.Add(tmpWidth);
                        ColumnTypes.Add(GridCol.GetType());
                        tmpLeft += tmpWidth;
                    }
                }

                // Printing Current Page, Row by Row
                while (RowPos <= dgv.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dgv.Rows[RowPos];
                    if (GridRow.IsNewRow || (!PrintAllRows && !GridRow.Selected))
                    {
                        RowPos++;
                        continue;
                    }

                    CellHeight = GridRow.Height;

                    if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        DrawFooter(e, RowsPerPage);
                        NewPage = true;
                        PageNo++;
                        e.HasMorePages = true;
                        return;
                    }
                    else
                    {
                        if (NewPage)
                        {
                            // Draw Header
                            e.Graphics.DrawString(PrintTitle, new Font(dgv.Font, FontStyle.Bold), 
                                    Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top -
                            e.Graphics.MeasureString(PrintTitle, new Font(dgv.Font, 
                                    FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String s = BathClass.Now(LogIn.connectionString).ToLongDateString() + " " + BathClass.Now(LogIn.connectionString).ToShortTimeString();

                            e.Graphics.DrawString(s, new Font(dgv.Font, FontStyle.Bold), 
                                    Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - 
                                    e.Graphics.MeasureString(s, new Font(dgv.Font, 
                                    FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - 
                                    e.Graphics.MeasureString(PrintTitle, new Font(new Font(dgv.Font, 
                                    FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            // Draw Columns
                            tmpTop = e.MarginBounds.Top;
                            i = 0;
                            foreach (DataGridViewColumn GridCol in dgv.Columns)
                            {
                                if (!GridCol.Visible) continue;
                                if (!PrintDGV.SelectedColumns.Contains(GridCol.HeaderText)) 
                                    continue;

                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), 
                                    new Rectangle((int) ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black, 
                                    new Rectangle((int) ColumnLefts[i], tmpTop,
                                    (int)ColumnWidths[i], HeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font, 
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)ColumnLefts[i], tmpTop, 
                                    (int)ColumnWidths[i], HeaderHeight), StrFormat);
                                i++;
                            }
                            NewPage = false;
                            tmpTop += HeaderHeight;
                        }

                        // Draw Columns Contents
                        i = 0;
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (!Cel.OwningColumn.Visible) continue;
                            if (!SelectedColumns.Contains(Cel.OwningColumn.HeaderText))
                                continue;

                            // For the TextBox Column
                            if (((Type) ColumnTypes[i]).Name == "DataGridViewTextBoxColumn" || 
                                ((Type) ColumnTypes[i]).Name == "DataGridViewLinkColumn")
                            {
                                string str = Cel.Value==null?"":Cel.Value.ToString();
                                e.Graphics.DrawString(str, Cel.InheritedStyle.Font, 
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)ColumnLefts[i], (float)tmpTop,
                                        (int)ColumnWidths[i], (float)CellHeight), StrFormat);
                            }
                            // For the Button Column
                            else if (((Type) ColumnTypes[i]).Name == "DataGridViewButtonColumn")
                            {
                                CellButton.Text = Cel.Value.ToString();
                                CellButton.Size = new Size((int)ColumnWidths[i], CellHeight);
                                Bitmap bmp =new Bitmap(CellButton.Width, CellButton.Height);
                                CellButton.DrawToBitmap(bmp, new Rectangle(0, 0, 
                                        bmp.Width, bmp.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            }
                            // For the CheckBox Column
                            else if (((Type) ColumnTypes[i]).Name == "DataGridViewCheckBoxColumn")
                            {
                                CellCheckBox.Size = new Size(14, 14);
                                CellCheckBox.Checked = (bool)Cel.Value;
                                Bitmap bmp = new Bitmap((int)ColumnWidths[i], CellHeight);
                                Graphics tmpGraphics = Graphics.FromImage(bmp);
                                tmpGraphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 
                                        bmp.Width, bmp.Height));
                                CellCheckBox.DrawToBitmap(bmp, 
                                        new Rectangle((int)((bmp.Width - CellCheckBox.Width) / 2), 
                                        (int)((bmp.Height - CellCheckBox.Height) / 2), 
                                        CellCheckBox.Width, CellCheckBox.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                            }
                            // For the ComboBox Column
                            else if (((Type) ColumnTypes[i]).Name == "DataGridViewComboBoxColumn")
                            {
                                CellComboBox.Size = new Size((int)ColumnWidths[i], CellHeight);
                                Bitmap bmp = new Bitmap(CellComboBox.Width, CellComboBox.Height);
                                CellComboBox.DrawToBitmap(bmp, new Rectangle(0, 0, 
                                        bmp.Width, bmp.Height));
                                e.Graphics.DrawImage(bmp, new Point((int)ColumnLefts[i], tmpTop));
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, 
                                        new SolidBrush(Cel.InheritedStyle.ForeColor), 
                                        new RectangleF((int)ColumnLefts[i] + 1, tmpTop, (int)ColumnWidths[i]
                                        - 16, CellHeight), StrFormatComboBox);
                            }
                            // For the Image Column
                            else if (((Type) ColumnTypes[i]).Name == "DataGridViewImageColumn")
                            {
                                Rectangle CelSize = new Rectangle((int)ColumnLefts[i], 
                                        tmpTop, (int)ColumnWidths[i], CellHeight);
                                Size ImgSize = ((Image)(Cel.FormattedValue)).Size;
                                e.Graphics.DrawImage((Image)Cel.FormattedValue, 
                                        new Rectangle((int)ColumnLefts[i] + (int)((CelSize.Width - ImgSize.Width) / 2), 
                                        tmpTop + (int)((CelSize.Height - ImgSize.Height) / 2), 
                                        ((Image)(Cel.FormattedValue)).Width, ((Image)(Cel.FormattedValue)).Height));

                            }

                            // Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)ColumnLefts[i], 
                                    tmpTop, (int)ColumnWidths[i], CellHeight));

                            i++;

                        }
                        tmpTop += CellHeight;
                    }

                    RowPos++;
                    // For the first page it calculates Rows per Page
                    if (PageNo == 1) RowsPerPage++;
                }

                if (RowsPerPage == 0) return;

                // Write Footer (Page Number)
                DrawFooter(e, RowsPerPage);

                e.HasMorePages = false;
	        }
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);        		
	        }
        }

        private static void DrawFooter(System.Drawing.Printing.PrintPageEventArgs e, 
                    int RowsPerPage)
        {
            double cnt = 0; 

            // Detemining rows number to print
            if (PrintAllRows)
            {
                if (dgv.Rows[dgv.Rows.Count - 1].IsNewRow) 
                    cnt = dgv.Rows.Count - 2; // When the DataGridView doesn't allow adding rows
                else
                    cnt = dgv.Rows.Count - 1; // When the DataGridView allows adding rows
            }
            else
                cnt = dgv.SelectedRows.Count;

            // Writing the Page Number on the Bottom of Page
            string PageNum = PageNo.ToString() + " of " + 
                Math.Ceiling((double)(cnt / RowsPerPage)).ToString();

            e.Graphics.DrawString(PageNum, dgv.Font, Brushes.Black, 
                e.MarginBounds.Left + (e.MarginBounds.Width - 
                e.Graphics.MeasureString(PageNum, dgv.Font, 
                e.MarginBounds.Width).Width) / 2, e.MarginBounds.Top + 
                e.MarginBounds.Height + 31);
        }
    }

    public class PrintReceipt:PrintBill
    {
        private static int TotalWidth;          // Summation of Columns widths
        private static int RowPos;              // Position of currently printing row 
        private static int PageNo;              // Number of pages to print
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns
        private static ArrayList ColumnWidths = new ArrayList(); // Width of Columns
        private static ArrayList ColumnTypes = new ArrayList();  // DataType of Columns
        private static int CellHeight;          // Height of DataGrid Cell
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing
        private static string m_lasttime;
        private static string m_thistime;

        private static int HeaderHeight = 0;

        public static void Print_DataGridView(DataGridView dgv1, string lasttime, string thistime, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;

                // Getting DataGridView object to print
                m_dgv = dgv1;
                companyName = coName;
                m_lasttime = lasttime;
                m_thistime = thistime;

                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 30;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender,
                    System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                // Formatting the Content of Text Cell to print
                ColumnLefts.Clear();
                ColumnWidths.Clear();
                ColumnTypes.Clear();
                CellHeight = 0;
                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                // Calculating Total Widths
                TotalWidth = 0;
                foreach (DataGridViewColumn GridCol in m_dgv.Columns)
                {
                    TotalWidth += GridCol.Width;
                }
                PageNo = 1;
                RowPos = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_PrintPage(object sender,
                    System.Drawing.Printing.PrintPageEventArgs e)
        {
            int tmpWidth, i;
            tmpTop = e.MarginBounds.Top;
            int tmpLeft = e.MarginBounds.Left;

            try
            {
                // Before starting first page, it saves Width & Height of Headers and CoulmnType
                if (PageNo == 1)
                {
                    print_title(e, "收银交班");
                    tmpTop -= 40;
                    print_str(e, "打印员工:"+LogIn.m_User.id, 13F, e.MarginBounds.Left, tmpTop);
                    tmpTop += str_h(e, 13F, "打印时间: ");
                    
                    print_str(e, "上次打印: " + m_lasttime, 13F, e.MarginBounds.Left, tmpTop);
                    tmpTop += str_h(e, 13F, "打印时间: ");
                    
                    print_str(e, "打印时间: " + m_thistime, 13F, e.MarginBounds.Left, tmpTop);
                    tmpTop += str_h(e, 13F, "打印时间: ");

                    HeaderHeight = str_h(e, 12F, m_dgv.Columns[0].HeaderText);
                    int left_width = e.MarginBounds.Width - TotalWidth;
                    foreach (DataGridViewColumn GridCol in m_dgv.Columns)
                    {
                        // Detemining whether the columns are fitted to page or not.
                        tmpWidth = (int)((double)left_width / (double)m_dgv.Columns.Count + GridCol.Width);
                        print_str(e, GridCol.HeaderText, 9F, tmpLeft, tmpTop);
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(tmpLeft,
                                    tmpTop, tmpWidth, HeaderHeight));

                        ColumnLefts.Add(tmpLeft);
                        ColumnWidths.Add(tmpWidth);
                        tmpLeft += tmpWidth;
                    }
                    tmpTop += HeaderHeight;
                }

                // Printing Current Page, Row by Row
                while (RowPos <= m_dgv.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = m_dgv.Rows[RowPos];
                    if (GridRow.IsNewRow)
                    {
                        RowPos++;
                        continue;
                    }

                    CellHeight = GridRow.Height;

                    if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        PageNo++;
                        e.HasMorePages = true;
                        return;
                    }
                    else
                    {
                        // Draw Columns Contents
                        i = 0;
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            // For the TextBox Column
                            string str = Cel.Value == null ? "" : Cel.Value.ToString();
                            print_str(e, str, 9F, (int)ColumnLefts[i], (int)tmpTop);
                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)ColumnLefts[i],
                                    tmpTop, (int)ColumnWidths[i], CellHeight));
                            i++;
                        }
                        tmpTop += CellHeight;
                    }
                    RowPos++;
                }

                tmpTop += 10;
                print_dash_line(e, tmpTop);
                tmpTop += 10;
                print_footer(e);

                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private static string get_last_print_time()
        //{
        //    var db = new BathDBDataContext(LogIn.connectionString);
        //    string macAdd = BathClass.getMacAddr_Local();
        //    var printT = db.CashPrintTime.OrderByDescending(x => x.time).FirstOrDefault(x => x.macAdd == macAdd);

        //    string lastTime = "";
        //    if (printT != null)
        //        lastTime = printT.time.ToString();
            
        //    return lastTime;
        //}
    }

    public class PrintDepositReceipt
    {
        private static HotelRoom m_Seat;
        private static string m_Money;
        private static string printer;
        private static string companyName;
        private static string m_Expense;
        public static int tmpTop;
        public static int PageNo;              // Number of pages to print
        public static Pen DashPen = new Pen(Color.Black, (float)0.8);

        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        public static void Print_DataGridView(HotelRoom seat, string money, string expense, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;

                // Getting DataGridView object to print
                companyName = coName;
                m_Money = money;
                m_Seat = seat;
                m_Expense = expense;

                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 30;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender,
                    System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                // Calculating Total Widths
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_PrintPage(object sender,
                    System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //打印标题
                print_title(e, "押金单");
                print_seat_information(e);

                print_str(e, "已消费:  " + m_Expense, 20F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 20F, "已消费");
                
                print_str(e, "押  金:  " + m_Money, 20F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 20F, "押金") + 10;

                print_dash_line(e, tmpTop);
                tmpTop += 20;
                if (!print_footer(e))
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //打印台位基本信息
        public static void print_seat_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            List<string> infor = new List<string>();
            string str = "台    号:" + m_Seat.text;
            infor.Add(str);

            str = "进店时间:" + m_Seat.openTime.ToString();
            infor.Add(str);

            str = "打单时间:" + BathClass.Now(LogIn.connectionString).ToString();
            infor.Add(str);

            str = "作业员工:" + LogIn.m_User.name;
            infor.Add(str);

            foreach (string st in infor)
            {
                print_str(e, st, 13F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 13F, st);
            }
        }

        //打印标题
        public static void print_title(System.Drawing.Printing.PrintPageEventArgs e, string title)
        {
            //打印标题
            int cLeft = (e.PageBounds.Width - str_w(e, 15F, companyName)) / 2;
            print_str(e, companyName, 15F, cLeft, tmpTop + 50);
            tmpTop += str_h(e, 15F, companyName) + 70;

            //打印副标题
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            cLeft = (e.PageBounds.Width - fsize) / 2;
            print_str(e, subTitle, 15F, cLeft, tmpTop);
            tmpTop += str_h(e, 15F, subTitle);
            e.Graphics.DrawLine(DashPen, cLeft - fsize / 2, tmpTop + 10, cLeft + fsize * 3 / 2, tmpTop + 10);
            tmpTop += 60;
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }

        //打印页脚
        public static bool print_footer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string footer = "杭州连客科技技术支持";
            if (tmpTop + str_h(e, 12F, footer) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            int foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, foolLeft, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            if (tmpTop + str_h(e, 12F, footer) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            footer = "TEL:186-7006-8930  188-5719-1220";
            //foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, e.MarginBounds.Left, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            if (tmpTop + 20 >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            return true;
        }

        //打印虚线
        public static void print_dash_line(System.Drawing.Printing.PrintPageEventArgs e, int y)
        {
            e.Graphics.DrawLine(DashPen, e.MarginBounds.Left, y, e.MarginBounds.Right, y);
        }

        //字符串高度
        public static int str_h(System.Drawing.Printing.PrintPageEventArgs e, Single s, string str)
        {
            Font f = new Font("SimSun", s, FontStyle.Regular);
            return (int)e.Graphics.MeasureString(str, f, e.MarginBounds.Width).Height;
        }

        //字符串宽度
        public static int str_w(System.Drawing.Printing.PrintPageEventArgs e, Single s, string str)
        {
            Font f = new Font("SimSun", s, FontStyle.Regular);
            return (int)e.Graphics.MeasureString(str, f, e.MarginBounds.Width).Width;
        }
    }

    public class PrePrintBill
    {
        #region 成员变量
        public static string printer = ""; //the name of the printer
        private static string printTile = "";
        public static string m_Money;
        public static DataGridView m_dgv;
        public static List<string> m_cols;
        public static List<HotelRoom> m_seats;
        public static int PageNo;              // Number of pages to print
        public static string companyName = "";  // name of the company
        public static int TotalWidth;          // Summation of Columns widths
        public static int RowPos;              // Position of currently printing row 
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns

        public static int CellHeight;          // Height of DataGrid Cell
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing
        public static int HeaderHeight = 0;

        public static int tmpTop;
        public static Pen DashPen = new Pen(Color.Black, (float)0.8);
        #endregion

        public static void Print_DataGridView(string money, List<HotelRoom> seats, string title,
            DataGridView dgv, List<string> printCols, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;

                // Getting DataGridView object to print
                m_dgv = dgv;
                m_cols = printCols;
                m_Money = money;
                printTile = title;
                m_seats = seats;

                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                companyName = coName;
                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 30;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void PrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ColumnLefts.Clear();
            CellHeight = 0;
            PageNo = 1;
            RowPos = 0;
        }

        private static void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            tmpTop = e.MarginBounds.Top;

            try
            {
                if (PageNo == 1)
                {
                    print_title(e, printTile);
                    print_seat_information(e);
                    print_dash_line(e, tmpTop + 10);
                    tmpTop += 20;
                }

                if (!print_expense_information(e))
                    return;

                if (tmpTop + 40 >= e.MarginBounds.Height)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return;
                }
                print_dash_line(e, tmpTop + 40);
                tmpTop += 50;
                if (!print_footer(e))
                    return;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //打印标题
        public static void print_title(System.Drawing.Printing.PrintPageEventArgs e, string title)
        {
            //打印标题
            int cLeft = (e.PageBounds.Width - str_w(e, 15F, companyName)) / 2;
            print_str(e, companyName, 15F, cLeft, tmpTop + 50);
            tmpTop += str_h(e, 15F, companyName) + 70;

            //打印副标题
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            cLeft = (e.PageBounds.Width - fsize) / 2;
            print_str(e, subTitle, 15F, cLeft, tmpTop);
            tmpTop += str_h(e, 15F, subTitle);
            e.Graphics.DrawLine(DashPen, cLeft - fsize / 2, tmpTop + 10, cLeft + fsize * 3 / 2, tmpTop + 10);
            tmpTop += 60;
        }

        //打印虚线
        public static void print_dash_line(System.Drawing.Printing.PrintPageEventArgs e, int y)
        {
            e.Graphics.DrawLine(DashPen, e.MarginBounds.Left, y, e.MarginBounds.Right, y);
        }

        //打印页脚
        public static bool print_footer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string footer = "杭州连客科技技术支持";
            if (tmpTop + str_h(e, 12F, footer) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            int foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, foolLeft, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            if (tmpTop + str_h(e, 12F, footer) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            footer = "TEL:186-7006-8930  188-5719-1220";
            //foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, e.MarginBounds.Left, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            if (tmpTop + 20 >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            return true;
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
        public static void print_seat_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            List<string> infor = new List<string>();

            string str = "台    号:";
            string s_str = "\n         ";
            bool first = true;
            //var sarray = m_Act.text.Split('|').ToArray();
            var sarray = m_seats.Select(x => x.text);
            foreach (var t in sarray)
            {
                if (first)
                    str += t;
                else
                    str += s_str + t;
                first = false;
            }

            //+ string.Join("\n", );
            infor.Add(str);

            str = "进店时间:" + m_seats[0].openTime;
            infor.Add(str);

            str = "打单时间:" + BathClass.Now(LogIn.connectionString).ToString();
            infor.Add(str);

            str = "作业员工:" + LogIn.m_User.name;
            infor.Add(str);

            foreach (string st in infor)
            {
                print_str(e, st, 13F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 13F, st);
            }
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }

        //打印消费信息
        public static bool print_expense_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int tmpWidth, i;
            int tmpLeft = e.MarginBounds.Left;

            // Calculating Total Widths
            TotalWidth = 0;
            foreach (DataGridViewColumn GridCol in m_dgv.Columns)
            {
                if (!m_cols.Contains(GridCol.HeaderText))
                    continue;
                TotalWidth += str_w(e, 10F, GridCol.HeaderText);
            }

            if (PageNo == 1)
            {
                foreach (DataGridViewColumn GridCol in m_dgv.Columns)
                {
                    if (!m_cols.Contains(GridCol.HeaderText))
                        continue;

                    // Detemining whether the columns are fitted to page or not.
                    tmpWidth = (int)((double)e.MarginBounds.Width * 0.9 * str_w(e, 10F, GridCol.HeaderText) / (double)TotalWidth);
                    print_str(e, GridCol.HeaderText, 10F, tmpLeft, tmpTop);

                    ColumnLefts.Add(tmpLeft);
                    tmpLeft += tmpWidth;
                }
                HeaderHeight = str_h(e, 10F, m_dgv.Columns[0].HeaderText);
                tmpTop += HeaderHeight;
            }

            while (RowPos <= m_dgv.Rows.Count - 1)
            {
                DataGridViewRow GridRow = m_dgv.Rows[RowPos];
                CellHeight = GridRow.Height;

                CellHeight = GridRow.Height;

                if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    //NewPage = true;
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }

                i = 0;
                foreach (DataGridViewCell Cel in GridRow.Cells)
                {
                    if (!m_cols.Contains(Cel.OwningColumn.HeaderText))
                        continue;

                    string str = Cel.Value == null ? "" : Cel.Value.ToString();
                    print_str(e, str, 10F, (int)ColumnLefts[i], (int)tmpTop);
                    i++;

                }
                tmpTop += CellHeight;
                RowPos++;
            }

            if (tmpTop + str_h(e, 20F, m_Money) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            e.Graphics.DrawString("合    计:   " + m_Money, new Font("SimSun", 20F, FontStyle.Bold),
                Brushes.Black, e.MarginBounds.Left, tmpTop);
            tmpTop += str_h(e, 20F, m_Money);

            return true;
        }

    }

    public class PrintBill
    {
        #region 成员变量
        public static string printer = ""; //the name of the printer
        private static string printTile = "";
        private static Account m_Act;
        public static string m_Money;
        public static DataGridView m_dgv;
        public static List<string> m_cols;
        public static int PageNo;              // Number of pages to print
        public static string companyName = "";  // name of the company
        public static int TotalWidth;          // Summation of Columns widths
        public static int RowPos;              // Position of currently printing row 
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns

        public static int CellHeight;          // Height of DataGrid Cell
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing
        public static int HeaderHeight = 0;

        public static int tmpTop;
        public static Pen DashPen = new Pen(Color.Black, (float)0.8);
        #endregion

        public static void Print_DataGridView(Account acctount, string title,
            DataGridView dgv, List<string> printCols, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;
                if (printer == "未设置默认打印机")
                {
                    BathClass.printErrorMsg("未设置默认打印机");
                    return;
                }
                //if (printer == "")
                //{
                //    PrinterChooseForm printerChooseForm = new PrinterChooseForm();
                //    if (printerChooseForm.ShowDialog() != DialogResult.OK)
                //        return;
                //    printer = printerChooseForm.printer;
                //}

                // Getting DataGridView object to print
                //m_Seat = seat;
                m_Act = acctount;
                m_dgv = dgv;
                m_cols = printCols;
                m_Money = BathClass.get_account_money(m_Act).ToString();
                printTile = title;

                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                companyName = coName;
                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 30;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void PrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ColumnLefts.Clear();
            CellHeight = 0;
            PageNo = 1;
            RowPos = 0;
        }

        private static void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            tmpTop = e.MarginBounds.Top;

            try
            {
                if (PageNo == 1)
                {
                    print_title(e, printTile);
                    print_seat_information(e);
                    print_dash_line(e, tmpTop + 10);
                    tmpTop += 20;
                }

                if (!print_expense_information(e))
                    return;

                if (tmpTop + 10 >= e.MarginBounds.Height)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return;
                }
                print_dash_line(e, tmpTop + 10);
                tmpTop += 20;
                if (!print_account_information(e))
                    return;

                if (tmpTop + 40 >= e.MarginBounds.Height)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return;
                }
                print_dash_line(e, tmpTop + 40);
                tmpTop += 50;
                if (!print_footer(e))
                    return;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //打印标题
        public static void print_title(System.Drawing.Printing.PrintPageEventArgs e, string title)
        {
            //打印标题
            int cLeft = (e.PageBounds.Width - str_w(e, 15F, companyName)) / 2;
            print_str(e, companyName, 15F, cLeft, tmpTop + 50);
            tmpTop += str_h(e, 15F, companyName) + 70;

            //打印副标题
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            cLeft = (e.PageBounds.Width - fsize) / 2;
            print_str(e, subTitle, 15F, cLeft, tmpTop);
            tmpTop += str_h(e, 15F, subTitle);
            e.Graphics.DrawLine(DashPen, cLeft - fsize / 2, tmpTop + 10, cLeft + fsize * 3 / 2, tmpTop + 10);
            tmpTop += 60;
        }

        //打印虚线
        public static void print_dash_line(System.Drawing.Printing.PrintPageEventArgs e, int y)
        {
            e.Graphics.DrawLine(DashPen, e.MarginBounds.Left, y, e.MarginBounds.Right, y);
        }

        //打印页脚
        public static bool print_footer(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string footer = "杭州连客科技技术支持";
            if (tmpTop + str_h(e, 12F, footer) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            int foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, foolLeft, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            if (tmpTop + str_h(e, 12F, footer) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            footer = "TEL:186-7006-8930  188-5719-1220";
            //foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, e.MarginBounds.Left, tmpTop);
            tmpTop += str_h(e, 12F, footer);

            if (tmpTop + 20 >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            return true;
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
        public static void print_seat_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            List<string> infor = new List<string>();

            string str = "账 单 号:" + m_Act.id;
            infor.Add(str);

            str = "台    号:";
            string s_str = "\n         ";
            bool first = true;
            var sarray = m_Act.text.Split('|').ToArray();
            foreach (var t in sarray)
            {
                if (first)
                    str += t;
                else
                    str += s_str+t;
                first = false;
            }

            //+ string.Join("\n", );
            infor.Add(str);

            str = "进店时间:" + m_Act.openTime.Split('|')[0];
            infor.Add(str);

            str = "打单时间:" + BathClass.Now(LogIn.connectionString).ToString();
            infor.Add(str);

            str = "作业员工:" + LogIn.m_User.name;
            infor.Add(str);

            foreach (string st in infor)
            {
                print_str(e, st, 13F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 13F, st);
            }
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }

        //打印消费信息
        public static bool print_expense_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int tmpWidth, i;
            int tmpLeft = e.MarginBounds.Left;

            // Calculating Total Widths
            TotalWidth = 0;
            foreach (DataGridViewColumn GridCol in m_dgv.Columns)
            {
                if (!m_cols.Contains(GridCol.HeaderText))
                    continue;
                TotalWidth += str_w(e, 10F, GridCol.HeaderText);
            }

            if (PageNo == 1)
            {
                foreach (DataGridViewColumn GridCol in m_dgv.Columns)
                {
                    if (!m_cols.Contains(GridCol.HeaderText))
                        continue;

                    // Detemining whether the columns are fitted to page or not.
                    tmpWidth = (int)((double)e.MarginBounds.Width * 0.9 * str_w(e, 10F, GridCol.HeaderText) / (double)TotalWidth);
                    print_str(e, GridCol.HeaderText, 10F, tmpLeft, tmpTop);

                    ColumnLefts.Add(tmpLeft);
                    tmpLeft += tmpWidth;
                }
                HeaderHeight = str_h(e, 10F, m_dgv.Columns[0].HeaderText);
                tmpTop += HeaderHeight;
            }

            while (RowPos <= m_dgv.Rows.Count - 1)
            {
                DataGridViewRow GridRow = m_dgv.Rows[RowPos];
                CellHeight = GridRow.Height;

                CellHeight = GridRow.Height;

                if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    //NewPage = true;
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }

                i = 0;
                foreach (DataGridViewCell Cel in GridRow.Cells)
                {
                    if (!m_cols.Contains(Cel.OwningColumn.HeaderText))
                        continue;

                    string str = Cel.Value == null ? "" : Cel.Value.ToString();
                    print_str(e, str, 10F, (int)ColumnLefts[i], (int)tmpTop);
                    i++;

                }
                tmpTop += CellHeight;
                RowPos++;
            }

            if (tmpTop + str_h(e, 20F, m_Money) >= e.MarginBounds.Height)
            {
                PageNo++;
                e.HasMorePages = true;
                return false;
            }
            e.Graphics.DrawString("合    计:   "+m_Money, new Font("SimSun", 20F, FontStyle.Bold),
                Brushes.Black, e.MarginBounds.Left, tmpTop);
            tmpTop += str_h(e, 20F, m_Money);

            return true;
        }

        //打印结账信息
        private static bool print_account_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "";

            BathDBDataContext db = new BathDBDataContext(LogIn.connectionString);
            if (m_Act.promotionMemberId != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }

                string id = m_Act.id.ToString();
                var cc = db.CardCharge.FirstOrDefault(x => x.CC_AccountNo == id && x.CC_ItemExplain.Contains("会员打折"));
                if (cc != null)
                {
                    str = "会员卡号:" + cc.CC_CardNo + "  优惠金额:" + m_Act.promotionAmount.ToString();
                    print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
                    tmpTop += str_h(e, 13F, str);
                }
            }

            if (m_Act.memberId != null)
            {
                
                string[] cardNoList = m_Act.memberId.Split('|');
                foreach (var cardNo in cardNoList)
                {
                    if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        PageNo++;
                        e.HasMorePages = true;
                        return false;
                    }

                    string id = m_Act.id.ToString();
                    var cc = db.CardCharge.Where(x => x.CC_ItemExplain.Contains("会员刷卡") && x.CC_AccountNo == id);
                    if (cc.Any())
                    {
                        foreach (var c in cc)
                        {
                            var ccs = db.CardCharge.Where(x => x.CC_CardNo == c.CC_CardNo);
                            double debit = BathClass.ToInt(ccs.Sum(x => x.CC_DebitSum));
                            double lend = BathClass.ToInt(ccs.Sum(x => x.CC_LenderSum));

                            str = "储值卡:" + c.CC_CardNo + "  余额:" + (debit - lend).ToString();
                            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
                            tmpTop += str_h(e, 13F, str);
                        }
                    }
                }
            }

            if (m_Act.cash != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "现  金:" + m_Act.cash.ToString() + "    实收:" + (m_Act.cash - BathClass.ToInt(m_Act.changes)).ToString();
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.bankUnion != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "银  联:" + m_Act.bankUnion.ToString();
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.creditCard != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = get_member_type(m_Act.memberId)+":" + m_Act.creditCard.ToString();
                //if (m_Act.memberId != null)
                //    str += "  " + m_Act.memberId;
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.coupon != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "优惠券:" + m_Act.coupon.ToString();
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.groupBuy != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "团购优惠:" + m_Act.groupBuy.ToString();
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.zero != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "挂  账:" + m_Act.zero.ToString();
                if (m_Act.name != null)
                    str += "  " + m_Act.name;
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.server != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "签  单:" + m_Act.server.ToString();
                if (m_Act.serverEmployee != null)
                    str += "  签单人:" + m_Act.serverEmployee;
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.deducted != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "扣  卡:" + m_Act.deducted.ToString();
                if (m_Act.memberId != null)
                    str += "  " + m_Act.memberId;
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.changes != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "找  零:" + m_Act.changes.ToString();
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }

            if (m_Act.wipeZero != null)
            {
                if (tmpTop >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return false;
                }
                str = "抹  零:" + m_Act.wipeZero.ToString();
                print_str(e, str, 16F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 16F, str);
            }
            return true;
        }

        private static string get_member_type(string card_no)
        {
            string cardType = "会员卡";
            var db = new BathDBDataContext(LogIn.connectionString);
            var cardInfo = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == card_no);
            if (cardInfo == null)
                return cardType;

            var tmpType = db.MemberType.FirstOrDefault(x => x.id == cardInfo.CI_CardTypeNo);
            if (tmpType == null)
                return cardType;

            return tmpType.name;
        }
    }

    public class PrintSeatBill:PrintBill
    {
        #region 成员变量
        private static string printTile = "";
        private static List<HotelRoom> m_Seats;
        //private static int PageNo;              // Number of pages to print
        //private static int TotalWidth;          // Summation of Columns widths
        //private static int RowPos;              // Position of currently printing row 
        private static ArrayList ColumnLefts = new ArrayList();  // Left Coordinate of Columns

        //private static int CellHeight;          // Height of DataGrid Cell
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing
        //private static int HeaderHeight = 0;

        #endregion

        public static void Print_DataGridView(List<HotelRoom> seats, string title,
            DataGridView dgv, List<string> printCols, string money, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = BathClass.get_config_by_key("printer");
                if (printer == "")
                {
                    PrinterChooseForm printerChooseForm = new PrinterChooseForm();
                    if (printerChooseForm.ShowDialog() != DialogResult.OK)
                        return;
                    printer = printerChooseForm.printer;
                }

                // Getting DataGridView object to print
                m_Seats = seats;
                m_dgv = dgv;
                m_cols = printCols;
                printTile = title;
                m_Money = money;

                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                companyName = coName;
                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 30;

                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
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
                if (PageNo == 1)
                {
                    print_title(e, printTile);
                    print_seat_information(e);
                    print_dash_line(e, tmpTop + 10);
                    tmpTop += 20;
                }

                if (!print_expense_information(e))
                    return;

                if (tmpTop + 10 >= e.MarginBounds.Height)
                {
                    PageNo++;
                    e.HasMorePages = true;
                    return;
                }
                print_dash_line(e, tmpTop + 10);
                tmpTop += 20;
                if (!print_footer(e))
                    return;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //打印台位基本信息
        public static void print_seat_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            List<string> infor = new List<string>();
            string str = "台    号:";
            string s_str = "\n         ";
            bool first = true;
            foreach (var seat in m_Seats)
            {
                if (first)
                    str += seat.text;
                else
                    str += s_str + seat.text;
                first = false;
            }

            infor.Add(str);

            str = "进店时间:" + m_Seats[0].openTime.ToString();
            infor.Add(str);

            str = "打单时间:" + BathClass.Now(LogIn.connectionString).ToString();
            infor.Add(str);

            str = "作业员工:" + LogIn.m_User.name;
            infor.Add(str);

            foreach (string st in infor)
            {
                print_str(e, st, 13F, e.MarginBounds.Left, tmpTop);
                tmpTop += str_h(e, 13F, st);
            }
        }
    }

    public class PrintCoupon
    {
        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing
        private static string printer = ""; //the name of the printer
        private static Bitmap bmp;

        public static void Print_Coupon(byte[] img)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;

                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.DefaultPageSettings.Margins.Left = 0;
                printDoc.DefaultPageSettings.Margins.Right = 0;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 0;

                MemoryStream ms = new MemoryStream(img);
                bmp = (Bitmap)Image.FromStream(ms);
                if (bmp.Width > bmp.Height)
                    printDoc.DefaultPageSettings.Landscape = true;
                else
                    printDoc.DefaultPageSettings.Landscape = false;
                ms.Close();


                PrintController printController = new StandardPrintController();
                printDoc.PrintController = printController;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private static void PrintDoc_BeginPrint(object sender,
                    System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                // Formatting the Content of Combo Cells to print
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_PrintPage(object sender,
                    System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int w = bmp.Width > bmp.Height ? bmp.Height : bmp.Width;
                var resizeBmp = PicSized(bmp, e.PageBounds.Width / w, ImageFormat.Bmp);
                e.Graphics.DrawImage(bmp, new Point(0, 0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Bitmap PicSized(Bitmap originBmp, int iSize, ImageFormat format)
        {
            int w = originBmp.Width * iSize;
            int h = originBmp.Height * iSize;
            Bitmap resizedBmp = new Bitmap(w, h);
            Graphics g = Graphics.FromImage(resizedBmp);
            //设置高质量插值法   
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度   
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //消除锯齿 
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawImage(originBmp, new Rectangle(0, 0, w, h), new Rectangle(0, 0, originBmp.Width, originBmp.Height), GraphicsUnit.Pixel);
            g.Dispose();

            return resizedBmp;
        }
    }

    public class PrintShoeMsg
    {
        #region 成员变量
        private static string printer = ""; //the name of the printer
        private static List<string> m_Seats;
        private static string companyName = "";  // name of the company
        private static string m_employee;
        private static string m_Time;

        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static int tmpTop;
        private static Pen DashPen = new Pen(Color.Black, (float)0.8);
        #endregion

        public static void Print_DataGridView(List<string> seats, string payEmployee, string payTime, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;

                // Getting DataGridView object to print
                m_Seats = seats;
                m_employee = payEmployee;
                m_Time = payTime;
                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                companyName = coName;

                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.OriginAtMargins = true;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 100;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private static void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            tmpTop = e.MarginBounds.Top;

            try
            {
                print_title(e, "取鞋单");
                print_pay_information(e);
                tmpTop += 20;

                foreach (string str in m_Seats)
                {
                    e.Graphics.DrawString(str, new Font("SimSun", 18F, FontStyle.Bold), Brushes.Black,
                        e.MarginBounds.Left, tmpTop);
                    tmpTop += str_h(e, 19F, str);
                }

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
            //打印标题
            int cLeft = (e.MarginBounds.Width - str_w(e, 15F, companyName)) / 2 - e.MarginBounds.Left;
            print_str(e, companyName, 15F, cLeft, tmpTop + 20);
            tmpTop += str_h(e, 15F, companyName) + 40;

            //打印副标题
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            cLeft = (e.MarginBounds.Width - fsize) / 2 - e.MarginBounds.Left;
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
        public static void print_pay_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "收银员: " + m_employee;
            int h = str_h(e, 13F, str);
            e.Graphics.DrawRectangle(Pens.Black,
                new Rectangle(e.MarginBounds.Left, tmpTop, (int)(e.MarginBounds.Width * 0.9), h * 2));

            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;

            str = "结账时间: " + m_Time;
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += h;
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }
    }

    //打印鞋吧对鞋单
    public class PrintCheckSeatBills
    {
        #region 成员变量

        private static string printer = ""; //the name of the printer
        private static List<string> m_Seats;
        private static string companyName = "";  // name of the company

        private static System.Drawing.Printing.PrintDocument printDoc =
                       new System.Drawing.Printing.PrintDocument();  // PrintDocumnet Object used for printing

        private static int tmpTop;
        private static Pen DashPen = new Pen(Color.Black, (float)0.8);

        #endregion

        public static void Print_DataGridView(List<string> seats, string coName)
        {
            PrintPreviewDialog ppvw;
            try
            {
                printer = printDoc.PrinterSettings.PrinterName;

                // Getting DataGridView object to print
                m_Seats = seats;
                DashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                companyName = coName;

                printDoc.PrinterSettings.PrinterName = printer;
                printDoc.OriginAtMargins = true;
                printDoc.DefaultPageSettings.Margins.Left = 10;
                printDoc.DefaultPageSettings.Margins.Right = 10;
                printDoc.DefaultPageSettings.Margins.Top = 0;
                printDoc.DefaultPageSettings.Margins.Bottom = 100;

                ppvw = new PrintPreviewDialog();
                ppvw.Document = printDoc;

                // Showing the Print Preview Page
                printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);

                // Printing the Documnet
                printDoc.Print();
                printDoc.BeginPrint -= new System.Drawing.Printing.PrintEventHandler(PrintDoc_BeginPrint);
                printDoc.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void PrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private static void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            tmpTop = e.MarginBounds.Top;

            try
            {
                print_title(e, "对鞋单");
                print_pay_information(e);
                tmpTop += 20;

                foreach (string str in m_Seats)
                {
                    e.Graphics.DrawString(str, new Font("SimSun", 16F, FontStyle.Bold), Brushes.Black,
                        e.MarginBounds.Left, tmpTop);
                    tmpTop += str_h(e, 17F, str);
                }

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
            //打印标题
            int cLeft = (e.MarginBounds.Width - str_w(e, 15F, companyName)) / 2 - e.MarginBounds.Left;
            print_str(e, companyName, 15F, cLeft, tmpTop + 20);
            tmpTop += str_h(e, 15F, companyName) + 40;

            //打印副标题
            string subTitle = title;
            int fsize = str_w(e, 15F, subTitle);
            cLeft = (e.MarginBounds.Width - fsize) / 2 - e.MarginBounds.Left;
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
        public static void print_pay_information(System.Drawing.Printing.PrintPageEventArgs e)
        {
            string str = "打印时间: " + DateTime.Now.ToString();
            print_str(e, str, 13F, e.MarginBounds.Left, tmpTop);
            tmpTop += str_h(e, 13F, str);
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
        }
    }
}
