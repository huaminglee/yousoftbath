using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Threading;

namespace YouSoftBathBack
{
    public partial class BounsTableSetForm : Form
    {
        private int FORMAT_ALL_NODIANLUN = 0;//已结未结 不区分点钟，轮钟
        private int FORMAT_ALL_DIANLUN = 1;//已结未结  区分点钟轮钟
        private int FORMAT_INPUTTIME_DIANLUN = 2;//纯粹按照输入时间 区分点钟轮钟
        private int FORMAT_INPUTTIME_NODIANLUN = 3;//纯粹按照输入时间 不区分点钟轮钟

        private BathDBDataContext db = null;

        //构造函数
        public BounsTableSetForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void BonusTableForm_Load(object sender, EventArgs e)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            var format = db.Options.FirstOrDefault().提成报表格式;
            if (format == null)
            {
                format = FORMAT_ALL_DIANLUN;
                db.SubmitChanges();
            }

            if (format == FORMAT_ALL_DIANLUN)
            {
                CheckerLunDian.Checked = true;
                CheckerPaid.Checked = true;
            }
            else if (format == FORMAT_ALL_NODIANLUN)
            {
                CheckerLunDian.Checked = false;
                CheckerPaid.Checked = true;
            }
            else if (format == FORMAT_INPUTTIME_DIANLUN)
            {
                CheckerLunDian.Checked = true;
                CheckerPaid.Checked = false;
            }
            else if (format == FORMAT_INPUTTIME_NODIANLUN)
            {
                CheckerLunDian.Checked = false;
                CheckerPaid.Checked = false;
            }
        }


        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void BonusTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            bool checkDianLun = CheckerLunDian.Checked;
            bool checkerPaid = CheckerPaid.Checked;

            var db = new BathDBDataContext(LogIn.connectionString);
            var ops = db.Options.FirstOrDefault();
            
            if (checkDianLun && checkerPaid)
                ops.提成报表格式 = FORMAT_ALL_DIANLUN;
            else if (!checkDianLun && !checkerPaid)
                ops.提成报表格式 = FORMAT_INPUTTIME_NODIANLUN;
            else if (checkDianLun && !checkerPaid)
                ops.提成报表格式 = FORMAT_INPUTTIME_DIANLUN;
            else if (!checkDianLun && checkerPaid)
                ops.提成报表格式 = FORMAT_ALL_NODIANLUN;

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
