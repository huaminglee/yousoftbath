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
using YouSoftUtil;

namespace YouSoftBathReception
{
    public partial class MemberPopForm : Form
    {
        //成员变量
        private DAO dao;
        private CCardInfo m_member = new CCardInfo();
        private CSeat m_Seat;
        private string cardType;

        //构造函数
        public MemberPopForm(CSeat seat)
        {
            m_Seat = seat;
            InitializeComponent();
        }

        //对话框载入
        private void MemberPopForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            cardType = MemberForm.memberSetting.cardType;
            id.ReadOnly = !(cardType == "CT");
            if (cardType != "CT")
                get_member();
        }

        //获取会员卡
        private void get_member()
        {
            string card_data = "";
            string company_code = LogIn.options.companyCode;

            bool st = false;
            //var cardType = db.MemberSetting.FirstOrDefault().cardType;
            if (cardType == "SLE4442")
                st = ICCard.read_data_4442(company_code, ref card_data);
            else if (cardType == "M1")
                st = ICCard.read_data_M1(company_code, ref card_data);
            else if (cardType == "CT")
            {
                card_data = id.Text;
                st = true;
            }
            if (!st)
                return;

            //card_data = "0000117";
            id.Text = card_data;
            m_member = dao.get_CardInfo("CI_CardNo='" + id.Text + "'");
            //m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text);
            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }
            name.Text = m_member.CI_Name;
            var t = dao.get_MemberType("id=" + m_member.CI_CardTypeNo);
            //var t = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            if (t != null)
                type.Text = t.name;

            var cc = dao.get_CardCharges("CC_CardNo='" + m_member.CI_CardNo + "'");
            //var cc = db.CardCharge.Where(x => x.CC_CardNo == m_member.CI_CardNo);
            int debit = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            int lend = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);
            balance.Text = (debit - lend).ToString();

            var c = MConvert<int>.ToTypeOrDefault(m_member.CI_CreditsUsed, 0);
            var cu = MConvert<int>.ToTypeOrDefault(MemberForm.memberSetting.money, 0);
            int cs = (int)(lend / cu - c);
            credits.Text = cs.ToString();
            btnOk.Text = "充值";
        }
        
        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
                get_member();
            else if (btnOk.Text == "充值")
            {
                string bank = tb_bank.Text.Trim();
                string cash = tb_cash.Text.Trim();
                string server = tb_server.Text.Trim();
                if (cash == "" && bank == "" && server == "")
                {
                    BathClass.printErrorMsg("需要输入充值金额");
                    return;
                }

                string serverEmployee = string.Empty;
                if (tb_server.Text.Trim() != "")
                {
                    var form = new SignForFreeForm();
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        BathClass.printErrorMsg("需要输入赠送人姓名");
                        return;
                    }
                    serverEmployee = form.signature;
                }

                string pars = "";
                string vals = "";

                pars = "memberId";
                vals = "'" + id.Text + "'";
                if (balance.Text != "")
                {
                    pars += ",balance";
                    vals += ",'" + balance.Text + "'";
                }

                double money = 0;
                if (bank != "")
                {
                    double b = Convert.ToDouble(bank);
                    pars += ",bankUnion";
                    vals += ",'" + bank + "'";
                    money += b;
                }

                if (cash != "")
                {
                    double c = Convert.ToDouble(cash);
                    pars += ",cash";
                    vals += ",'" + cash + "'";
                    money += c;
                }

                if (server != "")
                {
                    double s = Convert.ToDouble(server);
                    pars += ",server";
                    vals += ",'" + server + "'";
                    money += s;

                    pars += ",serverEmployee";
                    vals += ",'" + serverEmployee + "'";
                }

                if (tb_seat.Text.Trim() != "")
                {
                    pars += ",seat";
                    vals += ",'" + tb_seat.Text + "'";
                }

                pars += ",macAddress";
                vals += ",'" + PCUtil.getMacAddr_Local() + "'";

                pars += ",explain";
                vals += ",'会员充值'";

                pars += ",payEmployee";
                vals += ",'" + LogIn.m_User.id + "'";

                pars += ",payTime";
                vals += ",getdate()";

                string cmd_str = @"insert into [CardSale](" + pars + ") values(" + vals + ") ";

                #region 会员充值
                pars = "CC_CardNo";
                vals = "'" + m_member.CI_CardNo + "'";

                pars += ",CC_DebitSum";
                vals += "," + money;

                pars += ",CC_ItemExplain";
                vals += ",'会员卡充值-收'";

                pars += ",CC_InputOperator";
                vals += ",'" + LogIn.m_User.id + "'";

                pars += ",CC_InputDate";
                vals += ",getdate()";

                if (m_Seat != null)
                {
                    pars += ",systemId";
                    vals += ",'" + m_Seat.systemId + "'";
                }

                cmd_str += @" insert into [CardCharge](" + pars + ") values(" + vals + ")";
                #endregion

                #region 会员充值送
                var sale_money = get_promotion_for_cardPop(money);
                if (sale_money != 0)
                {
                    BathClass.printInformation("会员充值送" + sale_money.ToString());
                    pars = "CC_CardNo";
                    vals = "'" + m_member.CI_CardNo + "'";

                    pars += ",CC_DebitSum";
                    vals += "," + sale_money;

                    pars += ",CC_ItemExplain";
                    vals += ",'会员卡充值-送'";

                    pars += ",CC_InputOperator";
                    vals += ",'" + LogIn.m_User.id + "'";

                    pars += ",CC_InputDate";
                    vals += ",getdate()";

                    if (m_Seat != null)
                    {
                        pars += ",systemId";
                        vals += ",'" + m_Seat.systemId + "'";
                    }

                    cmd_str += @" insert into [CardCharge](" + pars + ") values(" + vals + ")";
                }
                
                #endregion

                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("会员充值失败，请重试!");
                    return;
                }

                Dictionary<string, string> pay_info = new Dictionary<string, string>();
                if (bank != "")
                {
                    pay_info["银联"] = bank;
                }

                if (cash != "")
                {
                    pay_info["现金"] = cash;
                }

                if (server != "")
                {
                    pay_info["招待"] = server + "$" + serverEmployee;
                }

                string bl = dao.get_member_balance(id.Text).ToString();
                PrintMemberPopMsg.Print_DataGridView(id.Text, type.Text, bl, LogIn.m_User.id,
                    DateTime.Now.ToString("MM-dd HH:mm"), LogIn.options.companyName, pay_info, tb_seat.Text);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //获取卡充值优惠
        private double get_promotion_for_cardPop(double pop_money)
        {
            double sale_money = 0;
            var cardPopSale = dao.get_CardPopSale("select top 1 * from cardpopSale where mimmoney<=" + pop_money.ToString() + " order by mimmoney desc");

            if (cardPopSale != null && cardPopSale.saleMoney != null)
                sale_money = MConvert<double>.ToTypeOrDefault(cardPopSale.saleMoney, 0);

            return sale_money;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void money_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
