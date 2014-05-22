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
    public partial class MemberActivateForm : Form
    {
        //成员变量
        private DAO dao;
        private CCardInfo m_member;
        private string cardType;
        private string m_finger = null;
        private bool m_use_finder_pwd = true;

        //构造函数
        public MemberActivateForm(string seat)
        {
            InitializeComponent();
            if (seat != null)
                tb_seat.Text = seat;

            if (!MConvert<bool>.ToTypeOrDefault(LogIn.options.启用会员卡密码, false) || LogIn.options.会员卡密码类型 != "指纹")
            {
                this.Width = GbCardInfo.Width + GbCardInfo.Location.X * 2;
                m_use_finder_pwd = false;
            }
            else
            {
                init_finger_driver();
            }
        }

        private void init_finger_driver()
        {
            try
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberActivateForm));
                this.axZKFPEngX1 = new AxZKFPEngXControl.AxZKFPEngX();
                ((System.ComponentModel.ISupportInitialize)(this.axZKFPEngX1)).BeginInit();

                this.axZKFPEngX1.Enabled = true;
                this.axZKFPEngX1.Location = new System.Drawing.Point(650, 315);
                this.axZKFPEngX1.Name = "axZKFPEngX1";
                this.axZKFPEngX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axZKFPEngX1.OcxState")));
                this.axZKFPEngX1.Size = new System.Drawing.Size(75, 23);
                this.axZKFPEngX1.TabIndex = 43;
                this.axZKFPEngX1.OnImageReceived += new AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEventHandler(this.axZKFPEngX1_OnImageReceived);
                this.axZKFPEngX1.OnFeatureInfo += new AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEventHandler(this.axZKFPEngX1_OnFeatureInfo);
                this.axZKFPEngX1.OnEnroll += new AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEventHandler(this.axZKFPEngX1_OnEnroll);

                ((System.ComponentModel.ISupportInitialize)(this.axZKFPEngX1)).EndInit();
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        //对话框载入
        private void MemberPopForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            cardType = MemberForm.memberSetting.cardType;
            id.ReadOnly = !(cardType == "CT");

            if (cardType != "CT")
                get_member();
            else
                id.Focus();
        }

        //获取会员卡
        private void get_member()
        {
            string card_data = "";
            string company_code = LogIn.options.companyCode;

            bool st = false;
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

            //card_data = "103051";
            id.Text = card_data;
            m_member = dao.get_CardInfo("CI_CardNo='" + id.Text + "'");
            //m_member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == id.Text);
            if (m_member == null)
            {
                BathClass.printErrorMsg("非本公司卡!");
                return;
            }

            var t = dao.get_MemberType("id=" + m_member.CI_CardTypeNo);
            //var t = db.MemberType.FirstOrDefault(x => x.id == m_member.CI_CardTypeNo);
            if (t != null)
                type.Text = t.name;

            balance.Text = dao.get_member_balance(id.Text).ToString();
            //balance.Text = BathClass.get_member_balance(db, card_data).ToString();

            if (m_member.state == "入库")
                btnOk.Text = "激活";

            if (m_member.state == "在用")
                btnOk.Text = "退卡";
            
        }
        
        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
                get_member();
            else if (btnOk.Text == "激活")
            {
                if (m_use_finder_pwd && m_finger==null)
                {
                    if (BathClass.printAskMsg("系统指定需要使用指纹密码，是否确定放弃录入指纹?") != DialogResult.Yes)
                        return;
                }
                var m_waiter = dao.get_Employee("id='" + tb_waiter.Text + "'");
                if (m_waiter == null)
                {
                    BathClass.printErrorMsg("输入服务员工号不存在!");
                    return;
                }

                if (tb_cash.Text.Trim() == "" && tb_bank.Text.Trim() == "")
                {
                    BathClass.printErrorMsg("需要输入金额!");
                    return;
                }

                double card_val = find_card_value(MConvert<double>.ToTypeOrDefault(balance.Text, 0));
                if (card_val != -1 && MConvert<double>.ToTypeOrDefault(tb_cash.Text, 0) + MConvert<double>.ToTypeOrDefault(tb_bank.Text, 0) < card_val)
                {
                    BathClass.printErrorMsg("付款金额不足");
                    return;
                }

                string pars = "";
                string vals = "";

                pars = "memberId";
                vals = "'" + m_member.CI_CardNo + "'";

                pars += ",payEmployee";
                vals += ",'" + m_waiter.id + "'";

                pars += ",payTime";
                vals += ",getdate()";

                pars += ",macAddress";
                vals += ",'" + PCUtil.getMacAddr_Local() + "'";

                Dictionary<string, string> pay_info = new Dictionary<string, string>();
                if (tb_bank.Text.Trim() != "")
                {
                    pars += ",bankUnion";
                    vals += ",'" + tb_bank.Text + "'";
                    pay_info["银联"] = tb_bank.Text;
                }

                if (tb_cash.Text.Trim() != "")
                {
                    pars += ",cash";
                    vals += ",'" + tb_cash.Text + "'";
                    pay_info["现金"] = tb_cash.Text;
                }

                if (balance.Text != "")
                {
                    pars += ",balance";
                    vals += ",'" + balance.Text + "'";
                }

                if (tb_seat.Text != "")
                {
                    pars += ",seat";
                    vals += ",'" + tb_seat.Text + "'";
                }

                string cmd_str = @"insert into [CardSale](" + pars + ") values(" + vals + ")";

                if (m_finger == null || m_finger == "")
                    m_finger = "null";
                else
                    m_finger = "'" + m_finger + "'";
                cmd_str += @"update [CardInfo] set state='在用',CI_Password=" + m_finger + " where CI_CardNo='" + id.Text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("会员激活失败，请重试!");
                    return;
                }

                string seat = null;
                if (tb_seat.Text != "")
                    seat = tb_seat.Text;
                PrintMemberActivateMsg.Print_DataGridView(id.Text, type.Text, balance.Text, LogIn.m_User.id,
                    DateTime.Now.ToString("MM-dd HH:mm"), LogIn.options.companyName, pay_info, seat);
                this.DialogResult = DialogResult.OK;
            }
            else if (btnOk.Text == "退卡")
            {
                if (BathClass.printAskMsg("确定退还卡号：" + m_member.CI_CardNo + "?") != DialogResult.Yes)
                    return;

                if (dao.exist_instance("CardCharge", "CC_CardNo='"+m_member.CI_CardNo+"' and (CC_ItemExplain='会员刷卡' or CC_ItemExplain='会员打折')"))
                {
                    BathClass.printErrorMsg("已有消费记录，不能退卡");
                    return;
                }
                string cmd_str = @" update cardSale set abandon='" + LogIn.m_User.id + "' where id in (select top 1 id from cardsale where memberId='" + id.Text + "' order by id desc)";
                cmd_str += @"update [CardInfo] set state='入库' where CI_CardNo='" + id.Text + "'";

                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("退卡失败，请重试!");
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        //查找卡价格
        private double find_card_value(double card_balance)
        {
            double card_val = -1;
            var cardPopSales = dao.get_CardPopSales(null);
            foreach (var card_pro in cardPopSales)
            {
                if (Math.Abs(card_balance-card_pro.mimMoney.Value-card_pro.saleMoney.Value) <= 1)
                {
                    card_val = card_balance - card_pro.saleMoney.Value;
                    break;
                }
            }

            return card_val;
        }

        //绑定快捷键
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

        //赠送卡
        private void btn_free_Click(object sender, EventArgs e)
        {
            if (btnOk.Text == "读卡")
                get_member();
            else if (btnOk.Text == "激活")
            {
                if (m_use_finder_pwd && m_finger == null)
                {
                    if (BathClass.printAskMsg("系统指定需要使用指纹密码，是否确定放弃录入指纹?") != DialogResult.Yes)
                        return;
                }

                if (tb_cash.Text.Trim() != "" || tb_bank.Text.Trim() != "")
                {
                    BathClass.printErrorMsg("已经输入付款金额，请点击\"激活\"售卡激活!");
                    return;
                }

                string pars = "memberId";
                string vals = "'" + m_member.CI_CardNo + "'";

                pars += ",payEmployee";
                vals += ",'" + LogIn.m_User.id + "'";

                pars += ",payTime";
                vals += ",getdate()";

                pars += ",macAddress";
                vals += ",'" + PCUtil.getMacAddr_Local() + "'";

                if (balance.Text != "")
                {
                    pars += ",balance";
                    vals += ",'" + balance.Text + "'";
                }

                pars += ",note";
                vals += ",'赠送卡'";

                if (tb_seat.Text != "")
                {
                    pars += ",seat";
                    vals += ",'" + tb_seat.Text + "'";
                }

                string cmd_str = @"insert into [CardSale](" + pars + ") values(" + vals + ") ";

                if (m_finger == null || m_finger == "")
                    m_finger = "null";
                else
                    m_finger = "'" + m_finger + "'";
                cmd_str += @"update [CardInfo] set state='在用',CI_Password=" + m_finger + " where CI_CardNo='" + id.Text + "'";
                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("赠送卡失败，请重试!");
                    return;
                }

                string seat = null;
                if (tb_seat.Text != "")
                    seat = tb_seat.Text;
                PrintMemberActivateMsg.Print_DataGridView(id.Text, type.Text, balance.Text, LogIn.m_User.id,
                    DateTime.Now.ToString("MM-dd HH:mm"), LogIn.options.companyName, null, seat);
                this.DialogResult = DialogResult.OK;
            }
        }

        //获取指纹
        private void BtnFinger_Click(object sender, EventArgs e)
        {
            int rt = axZKFPEngX1.InitEngine();
            if (rt == 1)
            {
                BathClass.printErrorMsg("指纹识别驱动程序加载失败");
                return;
            }
            else if (rt == 2)
            {
                BathClass.printErrorMsg("没有连接指纹识别仪");
                return;
            }
            else if (rt == 3)
            {
                BathClass.printErrorMsg("属性SensorIndex指定的指纹仪不存在");
                return;
            }

            if (axZKFPEngX1.IsRegister)
            {
                axZKFPEngX1.CancelEnroll();
            }
            if (m_finger == null)
                axZKFPEngX1.BeginEnroll();
            BtnFinger.Enabled = false;
        }

        private void axZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            m_finger = Convert.ToBase64String((byte[])e.aTemplate);
            axZKFPEngX1.EndEngine();
            BtnFinger.Enabled = true;
            BtnFinger.Text = "重新获取指纹";
        }

        private void axZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
            int handle1 = g.GetHdc().ToInt32();
            axZKFPEngX1.PrintImageAt(handle1, 0, 0, bmp.Width, bmp.Height);
            g.Dispose();
            pictureBox1.Image = bmp;
        }

        private void axZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {
            string strTemp = "指纹质量";
            if (e.aQuality == 0)
            {
                strTemp = strTemp + "合格";
            }
            else
            {
                if (e.aQuality == 1)
                {
                    strTemp = strTemp + "特征点不够";
                }
                else
                    strTemp = strTemp + "不合格";
            }
            if (axZKFPEngX1.IsRegister)
                if (axZKFPEngX1.EnrollIndex != 1)
                    strTemp = strTemp + ",请再按 " + (axZKFPEngX1.EnrollIndex - 1).ToString() + "次指纹";
                else
                    strTemp = strTemp + ",登记成功";

            BtnFinger.Text = strTemp;
        }
    }
}
