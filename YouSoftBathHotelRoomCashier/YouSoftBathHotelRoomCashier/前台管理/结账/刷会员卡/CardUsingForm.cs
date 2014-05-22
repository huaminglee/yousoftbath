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
    public partial class CardUsingForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private double m_money = 0;
        public CardInfo m_member;
        public Dictionary<string, double> m_memberList = new Dictionary<string, double>();

        //构造函数
        public CardUsingForm(double m)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_money = m;
            InitializeComponent();
        }

        //对话框载入
        private void MemberCardUsingForm_Load(object sender, EventArgs e)
        {
            //if (MainWindow.ic_dev <= 0)
            //{
            //    BathClass.printErrorMsg("会员卡读卡器设置不正确，请重新设置!");
            //    this.Close();
            //    return;
            //}

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
                m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text);
            }
            else
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
            if (m_memberList.Keys.Contains(m_member.CI_CardNo))
            {
                BathClass.printErrorMsg("已经添加卡号为" + m_member.CI_CardNo + "的会员卡，不能重复添加！");
                return;
            }

            string memberType = "";
            var t = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            if (t != null)
                memberType = t.name;

            var cc = db.CardCharge.Where(x => x.CC_CardNo == m_member.CI_CardNo);
            double debit = BathClass.ToInt(cc.Sum(x => x.CC_DebitSum));
            double lend = BathClass.ToInt(cc.Sum(x => x.CC_LenderSum));
            double balance = debit - lend;

            var c = BathClass.ToInt(m_member.CI_CreditsUsed);
            var cu = BathClass.ToInt(db.MemberSetting.FirstOrDefault().money);
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
            if (BathClass.getAuthority(db, LogIn.m_User, "扣卡"))
                id.ReadOnly = false;
            else
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;
                
                if (!BathClass.getAuthority(db, inputEmployee.employee, "扣卡"))
                {
                    BathClass.printErrorMsg(inputEmployee.employee.id + "不具有扣卡权限!");
                    return;
                }
                id.ReadOnly = false;
            }
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
