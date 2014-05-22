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
using System.Threading;

namespace YouSoftBathBack
{
    public partial class OptimizationForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private ProgressForm m_form = null;
        private DateTime st;
        private DateTime et;

        //构造函数
        public OptimizationForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void OptimizationForm_Load(object sender, EventArgs e)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            int count = cl.Items.Count;
            for (int i = 0; i < count; i++ )
            {
                cl.SetItemChecked(i, true);
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            st = DateTime.Parse("2000-1-1 00:00:00");
            et = BathClass.Now(LogIn.connectionString);
            if (rmonth.Checked)
                et = DateTime.Today.AddMonths(-Convert.ToInt32(month.Value));
            else
            {
                st = startTime.Value;
                et = endTime.Value;
            }
            if (BathClass.printAskMsg("确定消除截止到" + et.ToString("yyyy年MM月dd日HH:mm") + "的记录?")
                != DialogResult.Yes)
                return;

            Thread td = new Thread(new ThreadStart(do_work));
            td.Start();

            m_form = new ProgressForm("工作进行中...", "正在删除记录，请稍等......");
            m_form.ShowDialog();

            this.DialogResult = DialogResult.OK;
        }

        private void do_work()
        {
            try
            {
                if (cl.GetItemChecked(0))//账单记录
                    db.Account.DeleteAllOnSubmit(db.Account.Where(x => x.payTime > st && x.payTime < et));
                if (cl.GetItemChecked(1))//吧台消息记录
                    db.BarMsg.DeleteAllOnSubmit(db.BarMsg.Where(x => x.time > st && x.time < et));
                if (cl.GetItemChecked(2))//售卡充值记录
                    db.CardSale.DeleteAllOnSubmit(db.CardSale.Where(x => x.payTime > st && x.payTime < et));
                if (cl.GetItemChecked(3))//前台打印时间记录
                    db.CashPrintTime.DeleteAllOnSubmit(db.CashPrintTime.Where(x => x.time > st && x.time < et));
                if (cl.GetItemChecked(4))//优惠券设置记录
                    db.Coupon.DeleteAllOnSubmit(db.Coupon.Where(x => x.issueDate > st && x.issueDate < et));
                if (cl.GetItemChecked(5))//团购记录
                    db.GroupBuy.DeleteAllOnSubmit(db.GroupBuy.Where(x => x.issueDate > st && x.issueDate < et));
                if (cl.GetItemChecked(6))//支出记录
                    db.Expense.DeleteAllOnSubmit(db.Expense.Where(x => x.inputDate > st && x.inputDate < et));
                if (cl.GetItemChecked(7))//订单记录
                    db.HisOrders.DeleteAllOnSubmit(db.HisOrders.Where(x => x.inputTime > st && x.inputTime < et));
                if (cl.GetItemChecked(8))//异常操作记录
                    db.Operation.DeleteAllOnSubmit(db.Operation.Where(x => x.opTime > st && x.opTime < et));
                if (cl.GetItemChecked(9))//摄像头录像消息记录
                    db.ExecuteCommand("truncate table paymsg");
                if (cl.GetItemChecked(10))//催钟记录
                    db.ExecuteCommand("truncate table roomcall");
                if (cl.GetItemChecked(11))//房间预警记录
                    db.ExecuteCommand("truncate table roomwarn");
                if (cl.GetItemChecked(12))//鞋吧消息记录
                    db.ExecuteCommand("truncate table shoemsg");
                db.SubmitChanges();
            }
            catch (System.Exception e)
            {
                this.Invoke(new print_msg_delegate(BathClass.printErrorMsg), new object[] { e.ToString() });
            }
            finally
            {
                if (m_form != null)
                    this.Invoke(new no_par_delegate(close_form));
            }
        }

        private void close_form()
        {
            m_form.Close();
        }

        private delegate void no_par_delegate();

        private delegate void print_msg_delegate(string msg);

        //绑定快捷键
        private void OptimizationForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }

        private void month_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

    }
}
