using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using System.Management;

using YouSoftBathGeneralClass;

namespace YouSoftBathFormClass
{
    public partial class PrintOptions : Form
    {
        public PrintOptions()
        {
            InitializeComponent();
        }
        public PrintOptions(List<string> availableFields)
        {
            InitializeComponent();

            foreach (string field in availableFields)
                     chklst.Items.Add(field, true);

            bool noPrinter = false;
            List<string> printList = GetPrinterList();
            foreach (string printer in printList)
            {
                if (printer.Contains("未设置默认打印机"))
                    noPrinter = true;
                listBox1.Items.Add(printer);
            }
            listBox1.Text = listBox1.Items[0].ToString();

            if (noPrinter && printList.Count == 1)
                btnOK.Enabled = false;
        }

        private void PrintOtions_Load(object sender, EventArgs e)
        {
            // Initialize some controls
            rdoAllRows.Checked = true;
            chkFitToPageWidth.Checked = true;
        }

        #region 获取打印机

        public static List<string> lstPrinterName = new List<string>();///打印机列表
        //////////////获取服务器上安装的打印机列表====Start()====////////                                                                
        public static List<string> GetPrinters()
        {
            string _classname = "SELECT * FROM Win32_Printer";

            ManagementObjectCollection printers = new ManagementObjectSearcher(_classname).Get();

            foreach (ManagementObject printer in printers)
            {
                PropertyDataCollection.PropertyDataEnumerator pde = printer.Properties.GetEnumerator();

                while (pde.MoveNext())
                {
                    lstPrinterName.Add(pde.Current.Value.ToString());
                }
            }
            return lstPrinterName;
        }

        public static List<string> GetPrintersWinSever()
        {
            PrintDocument prtdoc = new PrintDocument();
            string strdefaultprinter = prtdoc.PrinterSettings.PrinterName;//获取默认的打印机名  
            foreach (string strprinter in PrinterSettings.InstalledPrinters)
            {
                lstPrinterName.Add(strprinter);
            }
            return lstPrinterName;
        }

        public static List<string> GetPrinterList()
        {
            /// <summary>  
            /// 获取本机的打印机列表。列表中的第一项就是默认打印机。  

            List<String> fPrinters = new List<string>();

            PrintDocument prtdoc = new PrintDocument();
            fPrinters.Add(prtdoc.PrinterSettings.PrinterName);
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                    fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }
        #endregion

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public List<string> GetSelectedColumns()
        {
            List<string> lst = new List<string>();
            foreach (object item in chklst.CheckedItems)
                    lst.Add(item.ToString());
            return lst;
        }

        public string PrintTitle
        {
            get { return txtTitle.Text; }
        }

        public bool PrintAllRows
        {
            get { return rdoAllRows.Checked; }
        }

        public bool FitToPageWidth
        {
            get { return chkFitToPageWidth.Checked; }
        }

        public string printName
        {
            get { return listBox1.Text; }
        }

        private void txtTitle_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

    }
}