using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class MemberDetailsForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private string m_No;

        //构造函数
        public MemberDetailsForm(BathDBDataContext dc, string card_no)
        {
            db = dc;
            m_No = card_no;
            InitializeComponent();
        }

        //对话框载入
        private void MemberManageForm_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        //显示清单
        public void dgv_show()
        {
            var cards = db.CardCharge.Where(x => x.CC_CardNo == m_No);
            dgv.DataSource = from x in cards
                             orderby x.CC_InputDate
                             select new
                             {
                                 会员卡号 = x.CC_CardNo,
                                 详情 = x.CC_ItemExplain,
                                 充值金额 = x.CC_DebitSum,
                                 消费金额 = x.CC_LenderSum,
                                 操作日期 = x.CC_InputDate,
                                 操作员工 = x.CC_InputOperator
                             };
            BathClass.set_dgv_fit(dgv);
        }

        //导出清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "会员消费详情", false, "");
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
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "会员消费详情", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                default:
                    break;
            }
        }
    }
}
