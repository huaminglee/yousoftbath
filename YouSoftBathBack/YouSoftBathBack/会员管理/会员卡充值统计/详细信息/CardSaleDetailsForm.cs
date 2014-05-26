using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Threading;

namespace YouSoftBathBack
{
    public partial class CardSaleDetailsForm : Form
    {
        //成员变量
        private Thread m_thread_details;
        private DateTime m_lastTime;//起始时间
        private DateTime m_thisTime;//终止时间

        private string m_user;
        private string m_balance;

        //构造函数
        public CardSaleDetailsForm(string _user, string _balance, DateTime _lastTime, DateTime _thisTime)
        {
            m_user = _user;
            m_balance = _balance;
            m_lastTime = _lastTime;
            m_thisTime = _thisTime;
            InitializeComponent();
        }

        //对话框载入
        private void BonusTableForm_Load(object sender, EventArgs e)
        {
            dgvDetails.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 15);
            dgvDetails.RowsDefaultCellStyle.Font = new Font("宋体", 15);

            dgvDetails.Rows.Clear();
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();

            m_thread_details = new Thread(new ThreadStart(dgvDetails_show));
            m_thread_details.Start();
        }

        private delegate void delegate_set_dgv_fit(DataGridView dg);
        private delegate void delegate_add_row_details(object[] vals);
        private void add_row_details(object[] vals)
        {
            dgvDetails.Rows.Add(vals);
        }

        //显示详细订单信息
        private void dgvDetails_show()
        {
            try
            {
                var dc = new BathDBDataContext(LogIn.connectionString);
                var cardsale = dc.CardSale.Where(x => x.payTime >= m_lastTime && x.payTime <= m_thisTime && x.payEmployee == m_user);

                if (m_balance == "赠送卡")
                    cardsale = cardsale.Where(x => x.note == "赠送卡");
                else if (m_balance == "0")
                {
                    double _balance = MConvert<double>.ToTypeOrDefault(m_balance, 0);
                    cardsale = cardsale.Where(x => x.note == null && (x.balance == _balance || x.balance == null));
                }
                else
                {
                    double _balance = MConvert<double>.ToTypeOrDefault(m_balance, 0);
                    cardsale = cardsale.Where(x => x.note == null && x.balance == _balance);
                }

                foreach (var cs in cardsale)
                {
                    var ci = dc.CardInfo.FirstOrDefault(x=>x.CI_CardNo==cs.memberId);
                    string cn = "";
                    if (ci != null && ci.CI_Name != null)
                        cn = ci.CI_Name.ToString();
                    object[] row = { false, cs.memberId, cs.payEmployee, cs.balance, cn,
                               cs.payTime, cs.cash, cs.bankUnion, cs.seat, cs.note};
                    this.Invoke(new delegate_add_row_details(add_row_details), (Object)row);
                }

                this.Invoke(new delegate_set_dgv_fit(BathClass.set_dgv_fit), dgvDetails);
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void BonusTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread_details != null && m_thread_details.IsAlive)
                m_thread_details.Abort();
        }

        private void dgvDetails_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvDetails.CurrentCell == null || dgvDetails.CurrentCell.ColumnIndex != 0)
                return;

            var has_checked = (dgvDetails.CurrentCell.EditedFormattedValue.ToString() == "False");
            if (has_checked)
            {
                //dgvDetails.CurrentRow.Cells[0].Value = false;
                dgvDetails.CurrentRow.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                //dgvDetails.CurrentRow.Cells[0].Value = true;
                dgvDetails.CurrentRow.DefaultCellStyle.BackColor = Color.Gray;
            }
        }
    }
}
