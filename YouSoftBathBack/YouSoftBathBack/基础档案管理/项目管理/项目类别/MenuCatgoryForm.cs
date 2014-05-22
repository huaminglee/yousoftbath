using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class MenuCatgoryForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Catgory m_cat = new Catgory();
        private bool newCat = true;

        //构造函数
        public MenuCatgoryForm(BathDBDataContext dc, Catgory curCat)
        {
            db = dc;
            if (curCat != null)
            {
                newCat = false;
                m_cat = curCat;
            }

            InitializeComponent();
        }

        //对话框载入
        private void MenuCatgoryForm_Load(object sender, EventArgs e)
        {
            kitchenPrinter.Items.Add("");
            kitchenPrinter.Items.AddRange(GetPrinterList().ToArray());
            if (!newCat)
            {
                name.Text = m_cat.name;
                if (m_cat.kitchPrinterName!= null && kitchenPrinter.Items.Contains(m_cat.kitchPrinterName))
                    kitchenPrinter.Text = m_cat.kitchPrinterName;
                this.Text = "编辑项目类别";
            }
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

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                GeneralClass.printErrorMsg("需要填入信息!");
                name.Focus();
                return;
            }
            if (db.Catgory.FirstOrDefault(x=>x.name==name.Text) != null)
            {
                BathClass.printErrorMsg("已经存在类别:" + name.Text);
                name.SelectAll();
                name.Focus();
                return;
            }

            m_cat.name = name.Text;
            if (kitchenPrinter.Text == "")
                m_cat.kitchPrinterName = null;
            else
                m_cat.kitchPrinterName = kitchenPrinter.Text;

            if (newCat)
                db.Catgory.InsertOnSubmit(m_cat);
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
