using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using YouSoftUtil;
using YouSoftBathConstants;

namespace YouSoftBathBack
{
    public partial class MemberAnalysisForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Thread m_thread, m_ThreadMsg;
        private string month_Text, averageMax_Text, averageMin_Text,
            totalMax_Text, totalMin_Text, timesMax_Text, timesMin_Text;

        private bool stop_flag = false;

        //构造函数
        public MemberAnalysisForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MemberAnalysisForm_Load(object sender, EventArgs e)
        {
            //dgv_show();
        }

        private string smsPort;
        private string smsBaud;
        //发送短信
        private void btnSmsSend_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            stop_flag = false;

            smsPort = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
            smsBaud = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
            if (smsPort == "" || smsBaud == "")
            {
                SMmsForm smsForm = new SMmsForm();
                if (smsForm.ShowDialog() != DialogResult.OK)
                    return;

                smsPort = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
                smsBaud = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
            }

            if (smsPort == "" || smsBaud == "" || msg.Text == "")
                return;

            if (m_ThreadMsg != null && m_ThreadMsg.IsAlive)
                m_ThreadMsg.Abort();

            m_ThreadMsg = new Thread(new ThreadStart(sendMsg_thread));
            m_ThreadMsg.Start();

        }

        private SmsMsgForm smsMsgForm = null;
        private delegate void delegate_no_para();
        private void show_smsMsgForm()
        {
            if (smsMsgForm == null)
                smsMsgForm = new SmsMsgForm(this);
            smsMsgForm.Show();
        }

        private void close_smsMsgForm()
        {
            smsMsgForm.Close();
        }

        private delegate void delegate_msgForm_addMsg(string msg);
        private void msgForm_addMsg(string msg)
        {
            smsMsgForm.ListMsg_Add_Msg(msg);
        }

        private delegate void delegate_change_row_color(DataGridViewRow r, Color color);
        private void change_row_color(DataGridViewRow r, Color color)
        {
            r.DefaultCellStyle.BackColor = color;
        }

        private delegate void delegate_change_current_cell(int r);
        private void change_current_cell(int r)
        {
            dgv.CurrentCell = dgv[0, r];
        }

        //发送短信线程
        private void sendMsg_thread()
        {
            try
            {
                String TypeStr = "";
                String CopyRightToCOM = "";
                String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";

                this.Invoke(new delegate_no_para(show_smsMsgForm));
                this.Invoke(new delegate_msgForm_addMsg(msgForm_addMsg), new object[] { "正在连接短信猫......." });
                if (SmsClass.Sms_Connection(CopyRightStr, uint.Parse(smsPort[3].ToString()), uint.Parse(smsBaud), out TypeStr, out CopyRightToCOM) != 1)
                {
                    this.Invoke(new delegate_no_para(close_smsMsgForm));
                    BathClass.printErrorMsg("短信猫连接失败，请重试!");
                    return;
                }

                List<string> phones = new List<string>();
                string phone = "";
                string fileName = "短信发送-" + DateTime.Now.ToString("yyMMddHHmm") + ".txt";
                IOUtil.insert_file(" 卡号   姓名    电话          短信", fileName);
                int i = 0;

                string txtStart = TextMsgStart.Text.Trim();
                if (txtStart != "")
                {
                    i = MConvert<int>.ToTypeOrDefault(txtStart, 1) - 1;
                }
                string sent_flag = "成功";
                while (!stop_flag)
                {
                    if (i >= dgv.Rows.Count)
                        break;

                    if (smsMsgForm.IsDisposed)
                    {
                        SmsClass.Sms_Disconnection();
                        break;
                    }
                    sent_flag = "成功";
                    DataGridViewRow r = dgv.Rows[i];
                    this.Invoke(new delegate_change_current_cell(change_current_cell), new object[] { i });
                    this.Invoke(new delegate_change_row_color(change_row_color), new object[] { r, Color.Cyan });
                    phone = MConvert<string>.ToTypeOrDefault(r.Cells[2].Value, "");
                    if (phone.Length != 11 || phones.Contains(phone))
                    {
                        i++;
                        this.Invoke(new delegate_change_row_color(change_row_color), new object[] { r, Color.OrangeRed });
                        continue;
                    }
                    if (SmsClass.Sms_Send(phone, msg.Text) == 0)
                    {
                        sent_flag = "失败";
                        this.Invoke(new delegate_change_row_color(change_row_color), new object[] { r, Color.OrangeRed });
                    }
                    else
                    {
                        phones.Add(phone);
                        this.Invoke(new delegate_change_row_color(change_row_color), new object[] { r, Color.LightGreen });
                    }

                    this.Invoke(new delegate_msgForm_addMsg(msgForm_addMsg),
                        new object[] { "卡号：" + MConvert<string>.ToTypeOrDefault(r.Cells[0].Value, "") +
                                        "，姓名：" + MConvert<string>.ToTypeOrDefault(r.Cells[1].Value, "") +
                                        "，电话：" + phone +
                                        "，发送：" + sent_flag });
                    IOUtil.insert_file(MConvert<string>.ToTypeOrDefault(r.Cells[0].Value, "") + "   " +
                        MConvert<string>.ToTypeOrDefault(r.Cells[1].Value, "") + "   " + phone + "   " + sent_flag, fileName);
                    i++;
                }

                IOUtil.insert_file("一共发送：" + phones.Count.ToString() + "条短信", fileName);
                this.Invoke(new delegate_msgForm_addMsg(msgForm_addMsg), new object[] { "发送结束，正在关闭短信猫!" });
                SmsClass.Sms_Disconnection();
                this.Invoke(new delegate_msgForm_addMsg(msgForm_addMsg), new object[] { "短信发送结束，短信猫关闭成功!" });
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        private void do_dgv_show()
        {
            try
            {
                IQueryable<CardInfo> ci = db.CardInfo;
                if (month_Text != "")
                {
                    DateTime lastTime = DateTime.Now.AddMonths(-Convert.ToInt32(month.Text));
                    ci = ci.Where(x => !db.CardCharge.Any(y => y.CC_CardNo == x.CI_CardNo && y.CC_InputDate > lastTime));
                }

                if (averageMax_Text != "")
                {
                    int averageMaxMoney = Convert.ToInt32(averageMax.Text);
                    var cc = db.CardCharge.Where(x => x.CC_LenderSum != null);
                    var ccs = cc.Select(x => x.CC_CardNo);
                    ci = ci.Where(x => ccs.Contains(x.CI_CardNo));
                    ci = ci.Where(x => cc.Where(y => y.CC_CardNo == x.CI_CardNo).Sum(y => y.CC_LenderSum) /
                        cc.Where(y => y.CC_CardNo == x.CI_CardNo).Count() >= averageMaxMoney);
                }

                if (averageMin_Text != "")
                {
                    int averageMinMoney = Convert.ToInt32(averageMin.Text);
                    var cc = db.CardCharge.Where(x => x.CC_LenderSum != null);
                    var ccs = cc.Select(x => x.CC_CardNo);
                    ci = ci.Where(x => ccs.Contains(x.CI_CardNo));
                    ci = ci.Where(x => cc.Where(y => y.CC_CardNo == x.CI_CardNo).Sum(y => y.CC_LenderSum) /
                        cc.Where(y => y.CC_CardNo == x.CI_CardNo).Count() <= averageMinMoney);
                }

                if (totalMax_Text != "")
                {
                    int totalMaxMoney = Convert.ToInt32(totalMax.Text);
                    var cc = db.CardCharge.Where(x => x.CC_LenderSum != null);
                    var ccs = cc.Select(x => x.CC_CardNo);
                    ci = ci.Where(x => ccs.Contains(x.CI_CardNo));
                    ci = ci.Where(x => cc.Where(y => y.CC_CardNo == x.CI_CardNo).Sum(y => y.CC_LenderSum) >= totalMaxMoney);
                }

                if (totalMin_Text != "")
                {
                    int totalMinMoney = Convert.ToInt32(totalMin.Text);
                    var cc = db.CardCharge.Where(x => x.CC_LenderSum != null);
                    var ccs = cc.Select(x => x.CC_CardNo);
                    ci = ci.Where(x => ccs.Contains(x.CI_CardNo));
                    ci = ci.Where(x => cc.Where(y => y.CC_CardNo == x.CI_CardNo).Sum(y => y.CC_LenderSum) <= totalMinMoney);
                }

                if (timesMax_Text != "")
                {
                    int maxTimes = Convert.ToInt32(timesMax.Text);
                    ci = ci.Where(x => db.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo).Count() >= maxTimes);
                }

                if (timesMin_Text != "")
                {
                    int minTimes = Convert.ToInt32(timesMin.Text);
                    ci = ci.Where(x => db.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo).Count() <= minTimes);
                }

                ci = ci.OrderByDescending(x => db.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo).Max(y => y.CC_InputDate));
                foreach (var x in ci)
                {
                    string[] row = new string[11];
                    row[0] = x.CI_CardNo;//卡号
                    row[1] = x.CI_Name;//姓名

                    row[2] = x.CI_Telephone;//电话

                    row[3] = row[4] = "";
                    var t = db.MemberType.FirstOrDefault(y => y.id == x.CI_CardTypeNo);
                    if (t != null)
                    {
                        row[3] = t.name;
                        row[4] = t.offerId.ToString();
                    }

                    row[5] = x.state;

                    var cc = db.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo);
                    var debit = cc.Sum(y => y.CC_DebitSum);
                    var lend = cc.Sum(y => y.CC_LenderSum);

                    row[6] = (debit - lend).ToString();
                    row[7] = lend.ToString();
                    row[8] = x.CI_SendCardOperator;
                    row[9] = x.CI_SendCardDate.ToString();
                    row[10] = cc.Max(y => y.CC_InputDate).ToString();

                    this.Invoke(new delegate_add_row(add_row), (Object)row);
                }
            }
            catch
            {
            	
            }
        }

        private delegate void delegate_add_row(string[] vals);

        private void add_row(string[] vals)
        {
            dgv.Rows.Add(vals);
        }

        //显示信息
        private void dgv_show()
        {
            dgv.Rows.Clear();
            if (month.Text == "" && averageMax.Text == "" && averageMin.Text == "" &&
                totalMax.Text == "" && totalMin.Text == "" && timesMax.Text == "" && timesMin.Text == "")
            {
                BathClass.printErrorMsg("需要输入至少一项条件!");
                return;
            }

            month_Text = month.Text;
            averageMax_Text = averageMax.Text;
            averageMin_Text = averageMin.Text;
            totalMax_Text = totalMax.Text;
            totalMin_Text = totalMin.Text;
            timesMax_Text = timesMax.Text;
            timesMin_Text = timesMin.Text;

            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            m_thread = new Thread(new ThreadStart(do_dgv_show));

            m_thread.Start();
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            PrintDGV.Print_DataGridView(dgv, "会员分析", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            this.Close();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F3:
                    findTool_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "会员分析", false, "");
                    break;
                default:
                    break;
            }
        }

        private void txts_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        //短信设置
        private void toolSms_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();
            SMmsForm smsForm = new SMmsForm();
            smsForm.ShowDialog();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void month_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void msg_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void MemberAnalysisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();

            if (m_ThreadMsg != null && m_ThreadMsg.IsAlive)
                m_ThreadMsg.Abort();
        }

        private void TextMsgStart_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //定位
        private void BtnPos_Click(object sender, EventArgs e)
        {
            int i = MConvert<int>.ToTypeOrDefault(TextMsgStart.Text.Trim(), 0);

            if (i >= dgv.Rows.Count)
            {
                BathClass.printErrorMsg("会员正在加载中，请稍等!");
                return;
            }

            dgv.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
        }

        //发送单条短信
        private void BtnSendOneMsg_Click(object sender, EventArgs e)
        {
            stop_flag = true;
            if (dgv.CurrentCell == null)
                return;

            String TypeStr = "";
            String CopyRightToCOM = "";
            String CopyRightStr = "//上海迅赛信息技术有限公司,网址www.xunsai.com//";

            smsPort = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
            smsBaud = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
            if (smsPort == "" || smsBaud == "")
            {
                SMmsForm smsForm = new SMmsForm();
                if (smsForm.ShowDialog() != DialogResult.OK)
                    return;

                smsPort = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSPORT);
                smsBaud = IOUtil.get_config_by_key(ConfigKeys.KEY_SMSBAUD);
            }

            if (smsPort == "" || smsBaud == "" || msg.Text == "")
                return;

            if (SmsClass.Sms_Connection(CopyRightStr, uint.Parse(smsPort[3].ToString()), uint.Parse(smsBaud), out TypeStr, out CopyRightToCOM) != 1)
            {
                this.Invoke(new delegate_no_para(close_smsMsgForm));
                BathClass.printErrorMsg("短信猫连接失败，请重试!");
                return;
            }

            DataGridViewRow r = dgv.CurrentRow;
            change_row_color(r, Color.Cyan);
            string phone = MConvert<string>.ToTypeOrDefault(r.Cells[2].Value, "");
            if (phone.Length != 11)
            {
                BathClass.printErrorMsg("电话号码格式不正确，需要11位电话号码");
                change_row_color(r, Color.OrangeRed);
            }

            if (SmsClass.Sms_Send(phone, msg.Text) == 0)
            {
                BathClass.printErrorMsg("发送失败!");
                change_row_color(r, Color.OrangeRed);
            }
            else
            {
                BathClass.printInformation("发送成功!");
                change_row_color(r, Color.LightGreen);
            }

            SmsClass.Sms_Disconnection();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("未选择行");
                return;
            }

            var memberId = dgv.CurrentRow.Cells[0].Value.ToString();
            var form = new MemberItemChart(memberId);
            form.ShowDialog();
        }
    }
}
