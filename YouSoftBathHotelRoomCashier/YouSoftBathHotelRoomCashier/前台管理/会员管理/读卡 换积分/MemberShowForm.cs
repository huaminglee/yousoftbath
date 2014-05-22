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

namespace YouSoftBathReception
{
    public partial class MemberShowForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public CardInfo m_member;
        private int cs = 0;

        //构造函数
        public MemberShowForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MemberReadForm_Load(object sender, EventArgs e)
        {
            get_member();
        }

        //显示所有会员信息
        private void dgv_show()
        {
            if (name.Text == "" && id.Text == "" && phone.Text =="")
                return;

            var db_new = new BathDBDataContext(LogIn.connectionString);
            dgv.Rows.Clear();
            IQueryable<CardInfo> cards = db.CardInfo;
            var cu = db.MemberSetting.FirstOrDefault().money.Value;
            foreach (CardInfo x in cards)
            {
                if ((name.Text != "" && x.CI_Name != null && GetStringSpell.GetChineseSpell(x.CI_Name).ToUpper().IndexOf(name.Text.ToUpper()) != 0)||
                    (id.Text != "" && x.CI_CardNo != id.Text) || (phone.Text != "" && x.CI_Telephone != phone.Text) ||
                    (x.CI_Name == null || x.CI_Name == ""))
                    continue;

                string[] row = new string[12];
                row[0] = x.CI_CardNo;
                row[1] = x.CI_Name;

                row[2] = "";
                row[3] = "";
                var t = db.MemberType.FirstOrDefault(y => y.id == x.CI_CardTypeNo);
                if (t != null)
                {
                    row[2] = t.name;
                    row[3] = t.offerId.ToString();
                }
                row[4] = x.state;

                var cc = db.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo);
                var debit = cc.Sum(y => y.CC_DebitSum);
                var lend = cc.Sum(y => y.CC_LenderSum);
                row[5] = (debit - lend).ToString();

                var cexpense = db.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo && y.CC_ItemExplain.Contains("打折"));
                if (cexpense.Any())
                {
                    double cds = 0;
                    var ces = cexpense.Sum(y => y.expense);
                    if (ces.HasValue)
                        cds = ces.Value;
                    if (x.CI_CreditsUsed == null)
                        row[6] = (cds / cu).ToString();
                    else
                        row[6] = (cds / cu - x.CI_CreditsUsed).ToString();
                }
                //if (x.CI_CreditsUsed == null)
                //    row[6] = (lend / cu).ToString();
                //else
                //    row[6] = (lend / cu - x.CI_CreditsUsed).ToString();

                row[7] = lend.ToString();
                row[8] = x.CI_SendCardOperator;
                row[9] = x.CI_SendCardDate.ToString();
                row[10] = cc.Max(y => y.CC_InputDate).ToString();
                row[11] = x.CI_Telephone;

                dgv.Rows.Add(row);
            }
            BathClass.set_dgv_fit(dgv);
        }

        //通过读数据库获取会员卡
        private void get_member_by_db()
        {
            dgv_show();
        }

        //通过读卡获取会员卡
        private void get_member_by_card()
        {
            string card_data = "";
            string company_code = db.Options.FirstOrDefault().companyCode;

            bool st = false;
            var cardType = db.MemberSetting.FirstOrDefault().cardType;
            if (cardType == "SLE4442")
                st = ICCard.read_data_4442(company_code, ref card_data);
            else if (cardType == "M1")
                st = ICCard.read_data_M1(company_code, ref card_data);

            if (!st)
                return;

            m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == card_data);
            if (m_member == null)
                return;

            set_member();
        }

        private void set_member()
        {
            name.Text = m_member.CI_CardNo;
            id.Text = m_member.CI_Name;
            phone.Text = m_member.CI_Telephone;
            dgv_show();

            btnOk.Text = "重置";
        }

        private void get_member()
        {
            get_member_by_card();
            if (m_member == null)
                get_member_by_db();
        }

        private void reset_form()
        {
            name.Text = "";
            id.Text = "";
            phone.Text = "";
            //type.Text = "";
            //balance.Text = "";
            //credits.Text = "";
            creditsUsing.Text = "";
            btnOk.Text = "读卡";
        }

        //读会员卡
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
            {
                m_member = null;
                get_member();
                //if (m_member == null)
                //{
                //    BathClass.printErrorMsg("输入卡不存在");
                //}
            }
            else if (btnOk.Text == "重置")
            {
                reset_form();
            }
        }

        //绑定快捷键
        private void MemberReadForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void creditsUsing_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        //换积分
        private void btnCredits_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择会员卡!");
                return;
            }
            string id = dgv.CurrentRow.Cells[0].Value.ToString();
            var member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id);
            var mt = db.MemberType.FirstOrDefault(x => x.id == member.CI_CardTypeNo);
            if (!mt.credits)
            {
                BathClass.printErrorMsg("所选择的卡类型不能积分!");
                return;
            }

            double cs = 0;
            var cu = db.MemberSetting.FirstOrDefault().money.Value;
            var cexpense = db.CardCharge.Where(y => y.CC_CardNo == id);
            var cvs = cexpense.Sum(y => y.expense);
            if (cvs.HasValue)
            {
                double cds = cvs.Value;
                if (member.CI_CreditsUsed == null)
                    cs = cds / cu;
                else
                    cs = cds / cu - member.CI_CreditsUsed.Value;
            }

            if (creditsUsing.Text == "")
            {
                BathClass.printErrorMsg("需要输入积分!");
                return;
            }
            int csu = BathClass.ToInt(creditsUsing.Text);
            if (csu > cs)
            {
                BathClass.printErrorMsg("积分不够");
                return;
            }
            //cs -= csu;

            if (member.CI_CreditsUsed == null)
                member.CI_CreditsUsed = csu;
            else
                member.CI_CreditsUsed += csu;
            db.SubmitChanges();
            dgv_show();
            //this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
