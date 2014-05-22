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

namespace YouSoftBathBack
{
    public partial class MemberSettingForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public MemberSettingForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MemberSettingForm_Load(object sender, EventArgs e)
        {
            cardType.SelectedIndex = 0;

            var ms = db.MemberSetting.FirstOrDefault();
            if (ms != null)
            {
                var ct = ms.cardType;
                if (ct != null)
                    cardType.Text = ct;
                creditMoney.Text = ms.money.ToString();
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
                case Keys.Enter:
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            MemberSetting ms;
            if (db.MemberSetting.Count() == 0)
                ms = new MemberSetting();
            else
                ms = db.MemberSetting.FirstOrDefault();

            ms.id = 0;
            ms.money = Convert.ToInt32(creditMoney.Text);
            ms.cardType = cardType.Text;

            if (db.MemberSetting.Count() == 0)
                db.MemberSetting.InsertOnSubmit(ms);

            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
        }

        private void creditMoney_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
