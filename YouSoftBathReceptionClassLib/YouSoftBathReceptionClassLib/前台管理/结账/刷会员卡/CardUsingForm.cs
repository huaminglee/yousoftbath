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
    public partial class CardUsingForm : Form
    {
        //成员变量
        private DAO dao;
        private double m_money = 0;
        public CCardInfo m_member;
        private CMemberSetting memberSetting;
        public Dictionary<string, double> m_memberList = new Dictionary<string, double>();
        private bool m_use_finger_pwd = true;

        //构造函数
        public CardUsingForm(double m)
        {
            m_money = m;
            InitializeComponent();
        }

        //对话框载入
        private void MemberCardUsingForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            memberSetting = dao.get_MemberSetting();
            id.Enabled = (memberSetting.cardType == "CT");
            if (!MConvert<bool>.ToTypeOrDefault(LogIn.options.启用会员卡密码, false) || LogIn.options.会员卡密码类型 != "指纹")
            {
                m_use_finger_pwd = false;
            }
            btnAdd_Click(null, null);
            money.Text = m_money.ToString();
        }

        private double get_paid_money()
        {
            double paid_money = 0;
            foreach (DataGridViewRow r in dgv.Rows)
                paid_money += Convert.ToDouble(r.Cells[6].Value);

            return paid_money;
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
                    if (id.Text != "")
                        btnAdd_Click(null, null);
                    else
                        btnCard_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void money_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        //添加储值卡
        private void btnAdd_Click(object sender, EventArgs e)
        {
            double paid_money = get_paid_money();
            if (paid_money >= m_money)
            {
                BathClass.printErrorMsg("刷卡金额已经足够！");
                return;
            }

            if (id.Text != "")
            {
                m_member = dao.get_CardInfo("CI_CardNo='" + id.Text + "'");
            }
            else
            {
                string card_data = "";
                string company_code = LogIn.options.companyCode;
                
                bool st = false;
                var cardType = memberSetting.cardType;
                if (cardType == "SLE4442")
                    st = ICCard.read_data_4442(company_code, ref card_data);
                else if (cardType == "M1")
                    st = ICCard.read_data_M1(company_code, ref card_data);
                if (!st)
                    return;

                m_member = dao.get_CardInfo("CI_CardNo='" + card_data + "'");
            }

            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }
            if (m_member.state == "挂失")
            {
                BathClass.printErrorMsg("卡已挂失，无法使用！");
                return;
            }
            if (m_member.state == "入库")
            {
                BathClass.printErrorMsg("卡已入库，但未激活，无法使用！");
                return;
            }
            if (m_member.state == "停用")
            {
                BathClass.printErrorMsg("卡已停用，无法使用！，新卡卡号为:" + m_member.CI_Special1 + "，补卡时间为：" +
                        m_member.CI_SpecialDate1.Value.ToString("yyyy-MM-dd HH:mm"));
                return;
            }

            string memberType = "";
            var t = dao.get_MemberType("id=" + m_member.CI_CardTypeNo);
            if (t != null)
                memberType = t.name;
            if (t != null && MConvert<bool>.ToTypeOrDefault(t.userOneTimeOneDay, false) &&
                    dao.exist_instance("CardCharge", "datediff(day,CC_InputDate,getdate())=0 and cc_itemExplain!='售卡收' and cc_cardno='" + m_member.CI_CardNo + "'"))
            {
                BathClass.printErrorMsg("此卡被限定一天只能使用一次，今天已经使用过!");
                return;
            }
            if (t != null && MConvert<bool>.ToTypeOrDefault(t.LimitedTimesPerMonth, false) &&
                    dao.get_memberCard_useTimes_this_month(m_member.CI_CardNo) >= t.TimesPerMonth)
            {
                BathClass.printErrorMsg("此卡被限定每月只能使用" + t.TimesPerMonth + "次，本月已达额度!");
                return;
            }
            if (m_memberList.Keys.Contains(m_member.CI_CardNo))
            {
                BathClass.printErrorMsg("已经添加卡号为" + m_member.CI_CardNo + "的会员卡，不能重复添加！");
                return;
            }

            if (m_use_finger_pwd)
            {
                var form = new MemberFingerForm(m_member);
                form.ShowDialog();

                if (!form.verified)
                {
                    BathClass.printErrorMsg("验证指纹失败!");
                    return;
                }
            }

            var cc = dao.get_CardCharges("CC_CardNo='" + m_member.CI_CardNo + "'");
            double debit = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_DebitSum), 0);
            double lend = MConvert<int>.ToTypeOrDefault(cc.Sum(x => x.CC_LenderSum), 0);
            double balance = debit - lend;

            var c = MConvert<int>.ToTypeOrDefault(m_member.CI_CreditsUsed, 0);
            var cu = memberSetting.money.Value;
            int cs = (int)(lend / cu - c);

            double hasto_money = 0;
            if (balance <= m_money - paid_money)
                hasto_money = balance;
            else
                hasto_money = m_money - paid_money;

            m_memberList[m_member.CI_CardNo] = hasto_money;
            dgv.Rows.Add(m_member.CI_CardNo, m_member.CI_Name, m_member.CI_Telephone, memberType, balance, cs, hasto_money);

            id.Text = "";
        }

        //刷卡
        private void btnCard_Click(object sender, EventArgs e)
        {
            if (get_paid_money() < m_money)
            {
                BathClass.printErrorMsg("刷卡金额不足!");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        //删除会员卡
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
                return;

            dgv.Rows.Remove(dgv.CurrentRow);
        }

        //扣卡
        private void btnDeduct_Click(object sender, EventArgs e)
        {
            
            if (dao.get_authority(LogIn.m_User, "扣卡"))
            {
                id.Enabled = true;
                id.Focus();
            }
            else
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;

                if (!dao.get_authority(inputEmployee.employee, "扣卡"))
                {
                    BathClass.printErrorMsg(inputEmployee.employee.id + "不具有扣卡权限!");
                    return;
                }
                id.Enabled = true;
                id.Focus();
            }
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

    }
}
