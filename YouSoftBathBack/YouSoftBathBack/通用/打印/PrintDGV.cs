using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Linq;
using System.Text;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    class PrintDGV
    {
        private static StringFormat StrFormat;  // Holds content of a TextBox Cell to write by DrawString
        private static StringFormat StrFormatComboBox; // Holds content of a Boolean Cell to write by DrawImage
        private static CheckBox CellCheckBox;   // Holds the Contents of CheckBox Cell 

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
        private static List<string> AvailableColumns = new List<string>();  // All Columns avaiable in DataGrid 
        //private static bool PrintAllRows = true;   // True = print all rows,  False = print selected rows    
        private static int HeaderHeight = 0;
        private static int tmpTop;
        private static Single font_size = 10F;
        private static string m_subtitle;

        public static void Print_DataGridView(DataGridView dgv1, string title, bool Landscape, string subTitle)
        {
            PrintPreviewDialog ppvw;
            try 
	        {	
                // Getting DataGridView object to print
                dgv = dgv1;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

                PrintTitle = title;
                m_subtitle = subTitle;
                //PrintAllRows = dlg.PrintAllRows;
                //SelectedColumns = dlg.GetSelectedColumns();
                printDoc.PrinterSettings.PrinterName = dlg.printName;

                printDoc.DefaultPageSettings.Margins.Left = 40;
                printDoc.DefaultPageSettings.Margins.Right = 40;
                printDoc.DefaultPageSettings.Margins.Top = 40;
                printDoc.DefaultPageSettings.Margins.Bottom = 80;

                TotalWidth = 0;
                foreach (DataGridViewColumn GridCol in dgv.Columns)
                {
                    if (!GridCol.Visible) continue;
                    //if (!PrintDGV.SelectedColumns.Contains(GridCol.HeaderText)) continue;
                    TotalWidth += GridCol.Width;
                }
                if (TotalWidth >= printDoc.DefaultPageSettings.Bounds.Width)
                    printDoc.DefaultPageSettings.Landscape = true;
                else
                    printDoc.DefaultPageSettings.Landscape = false;

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
                BathClass.set_dgv_fit(dgv);
            }
        }

        private static void PrintDoc_BeginPrint(object sender, 
                    System.Drawing.Printing.PrintEventArgs e) 
        {
            try
	        {
                // Formatting the Content of Text Cell to print
                StrFormat = new StringFormat();
                StrFormat.Alignment = StringAlignment.Center;
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
                //CellButton = new Button();
                CellCheckBox = new CheckBox();
                //CellComboBox = new ComboBox();

                // Calculating Total Widths
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
            tmpTop = e.MarginBounds.Top;
            int tmpLeft = e.MarginBounds.Left;

            try 
	        {	        
                // Before starting first page, it saves Width & Height of Headers and CoulmnType
                if (PageNo == 1) 
                {
                    int extra_width = (e.MarginBounds.Width - TotalWidth) / AvailableColumns.Count;
                    foreach (DataGridViewColumn GridCol in dgv.Columns)
                    {
                        if (!GridCol.Visible) continue;
                        // Skip if the current column not selected
                        //if (!PrintDGV.SelectedColumns.Contains(GridCol.HeaderText)) continue;

                        tmpWidth = GridCol.Width + extra_width;
                        HeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font, tmpWidth).Height) + 12;
                        
                        // Save width & height of headers and ColumnType
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
                    if (GridRow.IsNewRow)
                    {
                        RowPos++;
                        continue;
                    }

                    CellHeight = GridRow.Height;

                    if (tmpTop + CellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        print_footer(e, RowsPerPage);
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
                            print_title(e);

                            // Draw Columns
                            print_column_header(e);
                            NewPage = false;
                            tmpTop += HeaderHeight;
                        }

                        // Draw Columns Contents
                        i = 0;
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (!Cel.OwningColumn.Visible) continue;
                            //if (!SelectedColumns.Contains(Cel.OwningColumn.HeaderText))
                                //continue;

                            // For the TextBox Column
                            if (((Type) ColumnTypes[i]).Name == "DataGridViewTextBoxColumn" || 
                                ((Type) ColumnTypes[i]).Name == "DataGridViewLinkColumn")
                            {
                                string str = Cel.Value==null?"":Cel.Value.ToString();
                                e.Graphics.DrawString(str, new Font("SimSun", font_size), 
                                        new SolidBrush(Cel.InheritedStyle.ForeColor),
                                        new RectangleF((int)ColumnLefts[i], (float)tmpTop,
                                        (int)ColumnWidths[i], (float)HeaderHeight), StrFormat);
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
                print_footer(e, RowsPerPage);

                e.HasMorePages = false;
	        }
	        catch (Exception ex)
	        {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);        		
	        }
        }

        private static void print_footer(System.Drawing.Printing.PrintPageEventArgs e, 
                    int RowsPerPage)
        {
            double cnt = 0; 

            // Detemining rows number to print
            if (dgv.Rows[dgv.Rows.Count - 1].IsNewRow)
                cnt = dgv.Rows.Count - 2; // When the DataGridView doesn't allow adding rows
            else
                cnt = dgv.Rows.Count - 1; // When the DataGridView allows adding rows

            int footer_top = e.MarginBounds.Top + e.MarginBounds.Height;
            string footer = "杭州连客科技技术支持";
            int foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, foolLeft, footer_top);
            footer_top += str_h(e, 12F, footer);
            
            footer = "TEL:186-7006-8930  188-5719-1220";
            foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, foolLeft, footer_top);
            footer_top += str_h(e, 12F, footer);
            
            // Writing the Page Number on the Bottom of Page
            footer = PageNo.ToString() + " of " + 
                Math.Ceiling((double)(cnt / RowsPerPage)).ToString();
            foolLeft = (e.PageBounds.Width - str_w(e, 12F, footer)) / 2;
            print_str(e, footer, 12F, foolLeft, footer_top);
        }

        //打印字符串
        public static void print_str(System.Drawing.Printing.PrintPageEventArgs e, string str, Single s, int x, int y)
        {
            e.Graphics.DrawString(str, new Font("SimSun", s), Brushes.Black, x, y);
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

        //打印标题
        public static void print_title(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //打印标题
            var db = new BathDBDataContext(LogIn.connectionString);
            string companyName = db.Options.FirstOrDefault().companyName;

            int cLeft = (e.PageBounds.Width - str_w(e, 13F, companyName)) / 2;
            print_str(e, companyName, 13F, cLeft, tmpTop);
            tmpTop += str_h(e, 13F, companyName);

            cLeft = (e.PageBounds.Width - str_w(e, 18F, PrintTitle)) / 2;
            e.Graphics.DrawString(PrintTitle, new Font("SimSun", 18F, FontStyle.Bold),
                Brushes.Black, cLeft, tmpTop);
            tmpTop += str_h(e, 18F, PrintTitle);

            //打印副标题
            string subTitle = BathClass.Now(LogIn.connectionString).ToString();
            print_str(e, "打印时间：" + subTitle, 10F, e.MarginBounds.Left, tmpTop);

            cLeft = (e.PageBounds.Width - str_w(e, 10F, m_subtitle)) / 2;
            print_str(e, m_subtitle, 10F, cLeft, tmpTop);

            subTitle = "打印员工：" + LogIn.m_User.id + "  " + LogIn.m_User.name;
            int fsize = str_w(e, 10F, subTitle);
            print_str(e, subTitle, 10F, e.MarginBounds.Right - fsize, tmpTop);
            tmpTop += str_h(e, 10F, subTitle);
        }

        //打印表头
        private static void print_column_header(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int i = 0;
            foreach (DataGridViewColumn GridCol in dgv.Columns)
            {
                if (!GridCol.Visible) continue;
                //if (!PrintDGV.SelectedColumns.Contains(GridCol.HeaderText))
                    //continue;

                HeaderHeight = (int)(str_h(e, font_size, "测试") * 1.5);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                    new Rectangle((int)ColumnLefts[i], tmpTop,
                    (int)ColumnWidths[i], HeaderHeight));

                e.Graphics.DrawRectangle(Pens.Black,
                    new Rectangle((int)ColumnLefts[i], tmpTop,
                    (int)ColumnWidths[i], HeaderHeight));

                e.Graphics.DrawString(GridCol.HeaderText, new Font("SimSun", font_size),
                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                    new RectangleF((int)ColumnLefts[i], tmpTop,
                    (int)ColumnWidths[i], HeaderHeight), StrFormat);
                i++;
            }
        }

    }
}
