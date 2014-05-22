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
    public partial class DeductedCardForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public DeductedCardForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void DeductedCardForm_Load(object sender, EventArgs e)
        {
            //money.Text = m_Amount.ToString();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cardId.Text == "" || money.Text == "" || money.Text == "0")
            {
                GeneralClass.printErrorMsg("需要输入必填信息");
                return;
            }

            if (name.Text == "")
            {
                GeneralClass.printErrorMsg("不存在此会员卡");
                return;
            }

            CardCharge cc = new CardCharge();
            cc.CC_CardNo = cardId.Text;
            //cc.CC_AccountNo = account.id.ToString();
            cc.CC_ItemExplain = "会员扣卡";
            cc.CC_LenderSum = Convert.ToDouble(money.Text);
            //cc.expense = account_money;
            cc.CC_InputOperator = LogIn.m_User.id.ToString();
            cc.CC_InputDate = DateTime.Now;
            db.CardCharge.InsertOnSubmit(cc);

            //Deducted deduct = new Deducted();
            //deduct.memberId = cardId.Text;
            //deduct.money = Convert.ToDouble(money.Text);
            //deduct.date = BathClass.Now(LogIn.connectionString);
            //deduct.processed = false;

            //db.Deducted.InsertOnSubmit(deduct);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
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

        private void cardId_TextChanged(object sender, EventArgs e)
        {
            var member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == cardId.Text);
            if (member != null)
            {
                name.Text = member.CI_Name;
                memberType.Text = db.MemberType.FirstOrDefault(x => x.id == member.CI_CardTypeNo).name;

                var cc = db.CardCharge.Where(y => y.CC_CardNo == member.CI_CardNo);
                var debit = cc.Sum(y => y.CC_DebitSum);
                var lend = cc.Sum(y => y.CC_LenderSum);
                balance.Text = (debit - lend).ToString();
            }
        }

        private void cardId_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
