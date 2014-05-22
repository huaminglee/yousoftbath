using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using YouSoftBathGeneralClass;
using YouSoftUtil;
using YouSoftBathConstants;

namespace YouSoftBathFormClass
{
    public partial class PrinterChooseForm : Form
    {
        public string printer = "";

        public PrinterChooseForm()
        {
            InitializeComponent();
        }

        private void PrinterChooseForm_Load(object sender, EventArgs e)
        {
            List<string> printList = GetPrinterList();
            ptList.DataSource = printList;
        }

        // 获取本机的打印机列表。列表中的第一项就是默认打印机。 
        public static List<string> GetPrinterList()
        {
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

        //////////////获取服务器上安装的打印机列表====Start()====////////                                                                
        public static List<string> GetPrinters()
        {
            List<string> lstPrinterName = new List<string>();///打印机列表
            string _classname = "SELECT * FROM Win32_Printer";

            ManagementObjectCollection printers = new ManagementObjectSearcher(_classname).Get();

            foreach (ManagementObject printer in printers)
            {
                PropertyDataCollection.PropertyDataEnumerator pde = printer.Properties.GetEnumerator();

                while (pde.MoveNext())
                {
                    if (pde.Current.Value == null)
                        continue;

                    lstPrinterName.Add(pde.Current.Value.ToString());
                }
            }
            return lstPrinterName;
        }


        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (ptList.SelectedIndex == -1)
            {
                BathClass.printErrorMsg("需要选择打印机 ");
                return;
            }
            printer = ptList.SelectedItem.ToString();
            //write_printer();
            IOUtil.set_config_by_key(ConfigKeys.KEY_PRINTER, printer);
            this.DialogResult = DialogResult.OK;
        }
    }
}
