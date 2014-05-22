using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class ExceptionTableForm : Form
    {
        //成员变量
        private Thread m_thread;
        private DateTime st, et;

        //构造函数
        public ExceptionTableForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void ExceptionTableForm_Load(object sender, EventArgs e)
        {
            start.Value = DateTime.Now.AddMonths(-1);
        }

        //查询
        private void findTool_Click(object sender, EventArgs e)
        {
            st = start.Value;
            et = end.Value;
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            dgv.Rows.Clear();
            if (rMember.Checked)
            {
                dgv.Columns.Clear();
                add_cols_to_dgv(dgv, "会员卡号");
                add_cols_to_dgv(dgv, "会员卡类型");
                add_cols_to_dgv(dgv, "员工工号");
                add_cols_to_dgv(dgv, "结账日期");
                add_cols_to_dgv(dgv, "结账次数");
                add_cols_to_dgv(dgv, "总金额");

                if (times.Text == "")
                {
                    BathClass.printErrorMsg("需指定刷卡总次数最低限制");
                    return;
                }

                m_thread = new Thread(new ThreadStart(find_Member));
                m_thread.Start();
            }
            else if (rMemberSameDay.Checked)
            {
                dgv.Columns.Clear();
                add_cols_to_dgv(dgv, "会员卡号");
                add_cols_to_dgv(dgv, "会员卡类型");
                add_cols_to_dgv(dgv, "员工工号");
                add_cols_to_dgv(dgv, "结账日期");
                add_cols_to_dgv(dgv, "结账次数");
                add_cols_to_dgv(dgv, "总金额");

                m_thread = new Thread(new ThreadStart(find_MemberSameDay));
                m_thread.Start();
            }
            else if (rRepay.Checked)
            {
                dgv.Columns.Clear();
                add_cols_to_dgv(dgv, "编号");
                add_cols_to_dgv(dgv, "手牌号");
                add_cols_to_dgv(dgv, "结账时间");
                add_cols_to_dgv(dgv, "结账员工");
                add_cols_to_dgv(dgv, "授权员工");
                m_thread = new Thread(new ThreadStart(find_repay));
                m_thread.Start();
            }
            else if (rCancelOpen.Checked)
            {
                dgv.Columns.Clear();
                add_cols_to_dgv(dgv, "手牌号");
                add_cols_to_dgv(dgv, "开牌员工");
                add_cols_to_dgv(dgv, "开牌时间");
                add_cols_to_dgv(dgv, "授权员工");
                add_cols_to_dgv(dgv, "操作时间");

                m_thread = new Thread(new ThreadStart(find_cancel_open));
                m_thread.Start();
            }
            else if (rUnlock.Checked)
            {
                dgv.Columns.Clear();
                add_cols_to_dgv(dgv, "手牌号");
                add_cols_to_dgv(dgv, "开牌员工");
                add_cols_to_dgv(dgv, "开牌时间");
                add_cols_to_dgv(dgv, "授权员工");
                add_cols_to_dgv(dgv, "操作时间");

                m_thread = new Thread(new ThreadStart(find_unlock));
                m_thread.Start();
            }
            else if (rTransfer.Checked)
            {
                dgv.Columns.Clear();
                add_cols_to_dgv(dgv, "转账时间");
                add_cols_to_dgv(dgv, "转出手牌");
                add_cols_to_dgv(dgv, "转入手牌");
                add_cols_to_dgv(dgv, "转账员工");

                m_thread = new Thread(new ThreadStart(find_transfer));
                m_thread.Start();
            }
        }

        //往dgv中添加列
        private void add_cols_to_dgv(DataGridView pdgv, string header)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = header;
            pdgv.Columns.Add(col);
        }

        private delegate void delegate_add_row(string[] vals);

        private void add_row(string[] vals)
        {
            dgv.Rows.Add(vals);
        }

        private delegate void delegate_set_dgv_fit(DataGridView dgv);
        private delegate void delegate_show_transfer(IQueryable<Operation> ops);

        //查找转账
        private void find_transfer()
        {
            var db = new BathDBDataContext(LogIn.connectionString);

            var ops = db.Operation.Where(x => x.explain == "转账" && x.opTime >= st && x.opTime <= et);
            this.Invoke(new delegate_show_transfer(show_transfer), (Object)ops);
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
        }

        private void show_transfer(IQueryable<Operation> ops)
        {
            foreach (var op in ops)
            {
                dgv.Rows.Add(op.opTime.Value.ToString("MM-dd HH:mm"), op.note1, op.note2, op.employee);
            }
        }

        //第一次到最后一次刷会员卡，显示同一工号
        private void find_Member()
        {
            int max_times = Convert.ToInt32(times.Text);
            var db = new BathDBDataContext(LogIn.connectionString);
            var actList = db.Account.Where(x => (x.memberId != null || x.promotionMemberId != null) && x.abandon == null);
            var memberIds = db.Account.Where(x => x.memberId != null && x.abandon == null).Select(x => x.memberId).Distinct();
            var proIds = db.Account.Where(x => x.promotionMemberId != null && x.abandon == null).Select(x => x.promotionMemberId).Distinct();
            var ids = memberIds.Union(proIds);
            foreach (string memberId in ids)
            {
                var ci = db.CardInfo.FirstOrDefault(y => y.CI_CardNo == memberId);
                if (ci == null)
                    continue;

                var mType = db.MemberType.FirstOrDefault(x => x.id == ci.CI_CardTypeNo).name;

                bool promotion = false;
                var acts = actList.Where(x => x.memberId == memberId);
                if (!actList.Any(x => x.memberId == memberId))
                {
                    promotion = true;
                    acts = actList.Where(x => x.promotionMemberId == memberId);
                }

                var payIds = acts.Select(x => x.payEmployee).Distinct().ToList();
                if (acts.Count() > max_times && payIds.Count == 1)
                {
                    string[] row = { memberId, mType, payIds[0], "", acts.Count().ToString(), acts.Sum(x => x.creditCard).ToString() };
                    if (promotion)
                        row[5] = acts.Sum(x => x.promotionAmount).ToString();

                    this.Invoke(new delegate_add_row(add_row), (Object)row);
                }
            }

            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
            //BathClass.set_dgv_fit(dgv);
        }

        //每天刷卡次数超过一次的会员卡
        private void find_MemberSameDay()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var actList = db.Account.Where(x => (x.memberId != null || x.promotionMemberId != null) && x.abandon == null);
            var memberIds = db.Account.Where(x => x.memberId != null && x.abandon == null).Select(x => x.memberId).Distinct();
            var proIds = db.Account.Where(x => x.promotionMemberId != null && x.abandon == null).Select(x => x.promotionMemberId).Distinct();
            var ids = memberIds.Union(proIds);
            foreach (string memberId in ids)
            {
                var ci = db.CardInfo.FirstOrDefault(y => y.CI_CardNo == memberId);
                if (ci == null) continue;
                var mType = db.MemberType.FirstOrDefault(x => x.id == ci.CI_CardTypeNo).name;

                bool promotion = false;
                var acts = actList.Where(x => x.memberId == memberId);
                if (!actList.Any(x => x.memberId == memberId))
                {
                    promotion = true;
                    acts = actList.Where(x=>x.promotionMemberId == memberId);
                }
                acts = acts.OrderBy(x => x.payTime);
                DateTime firstDay = Convert.ToDateTime(acts.FirstOrDefault().payTime.ToShortDateString() + " 00:00:00");
                DateTime lastDay = Convert.ToDateTime(acts.ToList().Last().payTime.AddDays(1).ToShortDateString() + " 00:00:00");

                DateTime tempDay = firstDay;
                while (tempDay < lastDay)
                {
                    var actsDate = acts.Where(x => x.payTime >= tempDay && x.payTime < tempDay.AddDays(1));
                    if (actsDate.Count() > 1)
                    {
                        string[] row = { memberId, mType, "",tempDay.ToShortDateString(), actsDate.Count().ToString(), actsDate.Sum(x => x.creditCard).ToString() };
                        if (promotion)
                            row[5] = actsDate.Sum(x=>x.promotionAmount).ToString();

                        this.Invoke(new delegate_add_row(add_row), (Object)row);
                    }
                    tempDay = tempDay.AddDays(1);
                }
            }
            //BathClass.set_dgv_fit(dgv);
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
        }

        //指定时期内超过N次的会员卡
        /*private void find_Times()
        {
            int t = 0;
            if (!int.TryParse(times.Text, out t))
            {
                times.SelectAll();
                GeneralClass.printErrorMsg("应输入整数!");
                return;
            }

            var actList = db.Account.Where(x => x.memberId != null);
            actList = actList.Where(x => x.payTime >= startTime.Value && x.payTime <= endTime.Value);
            
            var memberIds = actList.Select(x => x.memberId).Distinct();
            foreach (string memberId in memberIds)
            {
                var acts = actList.Where(x => x.memberId == memberId);
                if (acts.Count() >= t)
                {
                    string[] row = { memberId, "", "", acts.Count().ToString(), acts.Sum(x => x.creditCard).ToString() };
                    dgv.Rows.Add(row);
                }
            }
        }*/

        //仅消费浴资异常统计表
        /*private void find_BathOnly()
        {
            var actList = db.Account.Where(x => x.memberId != null);
            var st = db.SeatType.Where(x => x.menuId != null).Select(x => x.id);
            var sts = db.Seat.Where(x => st.Contains(x.typeId)).Select(x => x.text).ToList();
            List<Account> acts = actList.Where(x => sts.Contains(x.text)).ToList();

            foreach (Account act in acts)
            {
                var orders = db.Orders.Where(x => x.systemId == act.systemId && x.deleteEmployee == null);
                if (orders.Count() == 1)
                {
                    string[] row = { act.memberId, act.payEmployee, act.payTime.ToString(), "1", act.creditCard.ToString() };
                    dgv.Rows.Add(row);
                }
            }
        }*/

        //重新结账统计
        private void find_repay()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var accounts = db.Account.Where(x => x.abandon != null && x.payTime >= st && x.payTime <= et);
            accounts = accounts.OrderBy(x => x.id);
            foreach (var act in accounts)
            {
                string[] row = { act.id.ToString(), act.text, act.payTime.ToString(), act.payEmployee, act.abandon };
                this.Invoke(new delegate_add_row(add_row), (Object)row);
            }
            //BathClass.set_dgv_fit(dgv);
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
        }

        //取消开牌统计
        private void find_cancel_open()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var ops = db.Operation.Where(x => x.opTime >= st && x.opTime <= et);
            ops = ops.Where(x => x.explain == "取消开牌");

            foreach (var op in ops)
            {
                string[] row = {
                                   op.seat,
                                   op.openEmployee,
                                   op.openTime.ToString(),
                                   op.employee,
                                   op.opTime.ToString()
                               };
                this.Invoke(new delegate_add_row(add_row), (Object)row);
            }
            //BathClass.set_dgv_fit(dgv);
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
        }

        //解锁统计
        private void find_unlock()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            var ops = db.Operation.Where(x => x.opTime >= st && x.opTime <= et);
            ops = ops.Where(x => x.explain == "解锁手牌");

            foreach (var op in ops)
            {
                string[] row = {
                                   op.seat,
                                   op.openEmployee,
                                   op.openTime.ToString(),
                                   op.employee,
                                   op.opTime.ToString()
                               };
                this.Invoke(new delegate_add_row(add_row), (Object)row);
            }
            //BathClass.set_dgv_fit(dgv);
            this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgv);
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
            PrintDGV.Print_DataGridView(dgv, "异常状况统计", false, "");
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            this.Close();
        }

        //选择 第一次到最后一次刷会员卡，显示同一工号
        private void rMember_CheckedChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            pn.Visible = false;
            pTimes.Visible = true;
            dgv.Rows.Clear();
            sp.SplitterDistance = this.Width / 2;
        }

        //选择 每天刷卡次数超过一次的会员卡
        private void rMemberSameDay_CheckedChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            pTimes.Visible = false;
            pn.Visible = false;
            dgv.Rows.Clear();
            sp.SplitterDistance = this.Width / 2;
        }

        //重新结账 
        private void rRepay_CheckedChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            pTimes.Visible = false;
            pn.Visible = true;
            dgv.Rows.Clear();
            sp.SplitterDistance = this.Width / 2;
        }

        //取消开牌统计
        private void rCancelOpen_CheckedChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            pTimes.Visible = false;
            pn.Visible = true;
            dgv.Rows.Clear();
            sp.SplitterDistance = this.Width;
        }

        //解锁统计
        private void rUnlock_CheckedChanged(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            pTimes.Visible = false;
            pn.Visible = true;
            dgv.Rows.Clear();
            sp.SplitterDistance = this.Width;
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
                    PrintDGV.Print_DataGridView(dgv, "异常状况统计", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }

        //结账详情
        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            dgvAct.Columns.Clear();
            dgvAct.Rows.Clear();
            if (rMember.Checked || rMemberSameDay.Checked)
                member_details();
            else if (rRepay.Checked)
                repay_details();
        }
        
        //选择 第一次到最后一次刷会员卡，显示同一工号，结账详情
        private void member_details()
        {
            var dc = new BathDBDataContext(LogIn.connectionString);

            if (dgv.CurrentCell == null)
                return;

            string memberId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();
            string payId = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[2].Value.ToString();

            bool promition = false;
            var actList = dc.Account.Where(x => x.memberId == memberId);
            if (!dc.Account.Any(x => x.memberId == memberId))
            {
                actList = dc.Account.Where(x => x.promotionMemberId == memberId);
                promition = true;
            }
            if (payId != "")
                actList = actList.Where(x => x.payEmployee == payId);

            actList = actList.OrderBy(x => x.payTime);
            add_cols_to_dgv(dgvAct, "手牌号");
            add_cols_to_dgv(dgvAct, "系统账号");
            add_cols_to_dgv(dgvAct, "结账时间");
            add_cols_to_dgv(dgvAct, "结账员工");
            add_cols_to_dgv(dgvAct, "金额");
            foreach (var x in actList)
            {
                string[] row = {
                                   x.text,
                                   x.systemId,
                                   x.payTime.ToString(),
                                   x.payEmployee,
                                   x.promotionAmount.ToString()
                               };
                if (!promition)
                    row[4] = x.creditCard.ToString();
                dgvAct.Rows.Add(row);
            }
            BathClass.set_dgv_fit(dgvAct);
        }

        //重新结账详情
        private void repay_details()
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            add_cols_to_dgv(dgvAct, "说明");
            add_cols_to_dgv(dgvAct, "手牌");
            add_cols_to_dgv(dgvAct, "结账时间");
            add_cols_to_dgv(dgvAct, "结账员工");
            add_cols_to_dgv(dgvAct, "打折卡");
            add_cols_to_dgv(dgvAct, "打折金额");
            add_cols_to_dgv(dgvAct, "储值卡号");
            add_cols_to_dgv(dgvAct, "手工打折");
            add_cols_to_dgv(dgvAct, "折扣率");
            add_cols_to_dgv(dgvAct, "招待员工");
            add_cols_to_dgv(dgvAct, "现金");
            add_cols_to_dgv(dgvAct, "银联");
            add_cols_to_dgv(dgvAct, "储值卡");
            add_cols_to_dgv(dgvAct, "优惠券");
            add_cols_to_dgv(dgvAct, "团购优惠");
            add_cols_to_dgv(dgvAct, "挂账");
            add_cols_to_dgv(dgvAct, "招待");
            add_cols_to_dgv(dgvAct, "扣卡");
            add_cols_to_dgv(dgvAct, "找零");
            add_cols_to_dgv(dgvAct, "抹零");

            if (dgv.CurrentCell == null)
                return;

            int abandon_id = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var act_a = db.Account.FirstOrDefault(x => x.id == abandon_id);
            string s_ids = act_a.systemId;
            var system_ids = s_ids.Split('|');
            string[] row1 = {"前",
                            act_a.text,
                            act_a.payTime.ToString(),
                            act_a.payEmployee,
                            act_a.promotionMemberId,
                            act_a.promotionAmount.ToString(),
                            act_a.memberId,
                            act_a.discountEmployee,
                            act_a.discount.ToString(),
                            act_a.serverEmployee,
                            act_a.cash.ToString(),
                            act_a.bankUnion.ToString(),
                            act_a.creditCard.ToString(),
                            act_a.coupon.ToString(),
                            act_a.groupBuy.ToString(),
                            act_a.zero.ToString(),
                            act_a.server.ToString(),
                            act_a.deducted.ToString(),
                            act_a.changes.ToString(),
                            act_a.wipeZero.ToString()};
            dgvAct.Rows.Add(row1);

            var act_bs = db.Account.Where(x => (x.systemId == s_ids || system_ids.Contains(x.systemId)) && x.abandon == null);
            foreach (var act_b in act_bs)
            {
                string[] row = {"后",
                            act_b.text,
                            act_b.payTime.ToString(),
                            act_b.payEmployee,
                            act_b.promotionMemberId,
                            act_b.promotionAmount.ToString(),
                            act_b.memberId,
                            act_b.discountEmployee,
                            act_b.discount.ToString(),
                            act_b.serverEmployee,
                            act_b.cash.ToString(),
                            act_b.bankUnion.ToString(),
                            act_b.creditCard.ToString(),
                            act_b.coupon.ToString(),
                            act_b.groupBuy.ToString(),
                            act_b.zero.ToString(),
                            act_b.server.ToString(),
                            act_b.deducted.ToString(),
                            act_b.changes.ToString(),
                            act_b.wipeZero.ToString()};
                dgvAct.Rows.Add(row);
            }
            BathClass.set_dgv_fit(dgvAct);
        }

        private void oper_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void cOper_CheckedChanged(object sender, EventArgs e)
        {
            oper.Enabled = cOper.Checked;
        }

        private void oper_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void ExceptionTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }
    }
}
