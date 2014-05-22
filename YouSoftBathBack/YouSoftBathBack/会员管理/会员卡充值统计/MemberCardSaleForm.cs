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
    public partial class MemberCardSaleForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private DateTime lastTime;//起始时间
        private DateTime thisTime;//终止时间

        //构造函数
        public MemberCardSaleForm()
        {
            InitializeComponent();
        }

        //获取夜审时间
        private bool get_clear_table_time()
        {
            var lct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == startTime.Value.Date);
            if (lct == null)
            {
                lct = db.ClearTable.Where(x => x.clearTime < startTime.Value).OrderByDescending(x => x.clearTime).FirstOrDefault();
                if (lct == null)
                    lastTime = DateTime.Parse("2013-01-01");
                else
                    lastTime = lct.clearTime;
            }
            else
                lastTime = lct.clearTime;

            var ct = db.ClearTable.FirstOrDefault(x => x.clearTime.Date == endTime.Value.AddDays(1).Date);
            if (ct == null)
            {
                GeneralClass.printErrorMsg("没有夜审，不能查询");
                return false;
            }

            thisTime = ct.clearTime;

            return true;
        }

        //对话框载入
        private void MemberCardSaleForm_Load(object sender, EventArgs e)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            mType.Items.AddRange(db.MemberType.Select(x => x.name).ToArray());
            startTime.Value = DateTime.Now.AddDays(-1);
            endTime.Value = DateTime.Now.AddDays(-1);
            checkType.SelectedIndex = 0;
        }

        //显示信息
        private void dgv_show()
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            if (!get_clear_table_time())
                return;

            if (checkType.SelectedIndex == 0)//售卡统计
            {
                var cardLst = db.CardSale.Where(x => x.payTime >= lastTime &&
                    x.payTime <= thisTime && x.explain == null && x.abandon == null);
                if (cId.Checked)
                    cardLst = cardLst.Where(x => x.memberId == id.Text);
                if (cType.Checked)
                {
                    var mt = db.MemberType.FirstOrDefault(x => x.name == mType.Text);
                    if (mt != null)
                    {
                        int typeId = mt.id;
                        cardLst = cardLst.Where(x => db.CardInfo.FirstOrDefault(y => y.CI_CardNo == x.memberId).CI_CardTypeNo == typeId);
                    }
                }

                dgv.DataSource = from x in cardLst
                                 orderby x.payTime
                                 select new
                                 {
                                     卡号 = x.memberId,
                                     卡类型 = db.MemberType.FirstOrDefault(y => y.id == db.CardInfo.FirstOrDefault(z => z.CI_CardNo == x.memberId).CI_CardTypeNo).name,
                                     持卡人 = db.CardInfo.FirstOrDefault(z => z.CI_CardNo == x.memberId).CI_Name,
                                     售卡日期 = x.payTime,
                                     售卡金额 = x.balance,
                                     收现金 = x.cash,
                                     收银联 = x.bankUnion,
                                     手牌 = x.seat,
                                     备注 = x.note,
                                     售卡工号 = x.payEmployee
                                 };
            }
            else if (checkType.SelectedIndex == 1)//售卡汇总
            {
                dgv.DataSource = null;
                var cardLst = db.CardSale.Where(x => x.payTime >= lastTime &&
                    x.payTime <= thisTime && x.explain == null && x.abandon == null);
                if (cId.Checked)
                    cardLst = cardLst.Where(x => x.memberId == id.Text);
                if (cType.Checked)
                {
                    var mt = db.MemberType.FirstOrDefault(x => x.name == mType.Text);
                    if (mt != null)
                    {
                        int typeId = mt.id;
                        cardLst = cardLst.Where(x => db.CardInfo.FirstOrDefault(y => y.CI_CardNo == x.memberId).CI_CardTypeNo == typeId);
                    }
                }

                BathClass.add_cols_to_dgv(dgv, "服务员");
                BathClass.add_cols_to_dgv(dgv, "卡余额");
                BathClass.add_cols_to_dgv(dgv, "数量");
                BathClass.add_cols_to_dgv(dgv, "现金");
                BathClass.add_cols_to_dgv(dgv, "银联");

                var users = cardLst.Select(x => x.payEmployee).Distinct();
                foreach (var user in users)
                {
                    double user_cash=0, user_bank=0;
                    var cards_users = cardLst.Where(x => x.payEmployee == user);
                    //首先考虑赠送卡

                    var cards_users_free = cards_users.Where(x => x.note == "赠送卡");
                    if (cards_users_free.Any())
                        dgv.Rows.Add(user, "赠送卡", cards_users_free.Count());

                    int count = cards_users.Count();
                    cards_users = cards_users.Where(x => x.note == null);
                    var card_types = cards_users.Select(x => x.balance).Distinct();
                    foreach (var card_type in card_types)
                    {
                        var cards_users_type = cards_users.Where(x => x.balance == card_type);
                        if (cards_users_type.Any())
                        {
                            double cash = 0, bank = 0;
                            var card_cash = cards_users_type.Where(x=>x.cash != null);
                            var card_bank = cards_users_type.Where(x=>x.bankUnion != null);

                            if (card_cash.Any())
                                cash = card_cash.Sum(x => x.cash).Value;
                            if (card_bank.Any())
                                bank = card_bank.Sum(x => x.bankUnion).Value;

                            user_bank += bank;
                            user_cash += cash;
                            dgv.Rows.Add(user, card_type, cards_users_type.Count(), cash, bank);
                        }
                    }
                    dgv.Rows.Add("", "小计", count, user_cash, user_bank);
                    dgv.Rows[dgv.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }

            }
            else if (checkType.SelectedIndex == 2)//充值统计
            {
                var cardLst = db.CardCharge.Where(x => x.CC_InputDate >= lastTime && x.CC_InputDate <= thisTime);
                cardLst = cardLst.Where(x => x.CC_ItemExplain == "会员卡充值-收");
                if (cId.Checked)
                    cardLst = cardLst.Where(x => x.CC_OldCardNo == id.Text);
                if (cType.Checked)
                {
                    var mt = db.MemberType.FirstOrDefault(x => x.name == mType.Text);
                    if (mt != null)
                    {
                        int typeId = mt.id;
                        cardLst = cardLst.Where(x => db.CardInfo.FirstOrDefault(y => y.CI_CardNo == x.CC_CardNo).CI_CardTypeNo == typeId);
                    }
                }

                dgv.DataSource = from x in cardLst
                                 orderby x.CC_InputDate
                                 select new
                                 {
                                     卡号 = x.CC_CardNo,
                                     卡类型 = db.MemberType.FirstOrDefault(y => y.id == db.CardInfo.FirstOrDefault(z => z.CI_CardNo == x.CC_CardNo).CI_CardTypeNo).name,
                                     持卡人 = db.CardInfo.FirstOrDefault(z => z.CI_CardNo == x.CC_CardNo).CI_Name,
                                     充值日期 = x.CC_InputDate,
                                     充值金额 = x.CC_DebitSum,
                                     充值工号 = x.CC_InputOperator
                                 };
            }
        }

        private void cId_CheckedChanged(object sender, EventArgs e)
        {
            id.Enabled = cId.Checked;
        }

        private void cType_CheckedChanged(object sender, EventArgs e)
        {
            mType.Enabled = cType.Checked;
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "会员卡售卡统计", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
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
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "会员卡充值统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void checkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_show();
        }

        //双击显示汇总详细信息
        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell == null)
                return;

            string m_user = dgv.CurrentRow.Cells[0].Value.ToString();
            string balance = dgv.CurrentRow.Cells[1].Value.ToString();
            var form = new CardSaleDetailsForm(m_user, balance, lastTime, thisTime);
            form.ShowDialog();
        }
    }
}
