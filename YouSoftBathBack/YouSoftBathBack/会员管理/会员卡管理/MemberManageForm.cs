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
    public partial class MemberManageForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> typeList;
        private Thread m_thread;
        private string m_typeSelName;
        private double balance_money;
        private IQueryable<CardInfo> cards;
        private double lend_money;
        private double db_money = 0;//入库金额
        private int db_number = 0;//入库数量
        private double on_money = 0;//在用金额
        private int on_number = 0;//在用数量
        private int lost_number = 0;//挂失数量
        private double lost_money = 0;//挂失金额

        //构造函数
        public MemberManageForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
            
        }

        //对话框载入
        private void MemberManageForm_Load(object sender, EventArgs e)
        {
            createTree();
        }

        //创建树
        private void createTree()
        {
            typeTree.Nodes.Clear();

            typeList = db.MemberType.Select(x => x.name).ToList();
            List<TreeNode> childNodes = new List<TreeNode>();
            foreach (string membertype in typeList)
            {
                TreeNode node1 = new TreeNode(membertype);
                node1.Name = membertype;
                node1.Text = membertype;
                node1.ImageIndex = 1;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有卡类型", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            typeTree.Nodes.AddRange(new TreeNode[] { rootNode });
            typeTree.ExpandAll();
            //typeTree.SelectedNode = rootNode;
        }

        //显示清单
        public void do_dgv_show()
        {
            try
            {
                var dc_new = new BathDBDataContext(LogIn.connectionString);

                cards = db.CardInfo;
                if (m_typeSelName != "所有卡类型")
                {
                    int typeSelId = dc_new.MemberType.FirstOrDefault(x => x.name == m_typeSelName).id;
                    cards = dc_new.CardInfo.Where(x => x.CI_CardTypeNo == typeSelId);
                }

                if (card.Text != "")
                {
                    cards = cards.Where(x => x.CI_CardNo == card.Text);
                }
                if (name.Text != "")
                {
                    cards = cards.Where(x => x.CI_Name == name.Text);
                }
                if (phone.Text != "")
                {
                    cards = cards.Where(x => x.CI_Telephone == phone.Text);
                }

                balance_money = 0;
                lend_money = 0;
                foreach (CardInfo x in cards)
                {
                    string[] row = new string[11];
                    row[0] = x.CI_CardNo;
                    row[1] = x.CI_Name;

                    row[2] = "";
                    row[3] = "";
                    var t = dc_new.MemberType.FirstOrDefault(y => y.id == x.CI_CardTypeNo);
                    if (t != null)
                    {
                        row[2] = t.name;
                        row[3] = t.offerId.ToString();
                    }
                    row[4] = x.state;

                    var cc = dc_new.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo);
                    var debit = cc.Sum(y => y.CC_DebitSum);
                    var lend = cc.Sum(y => y.CC_LenderSum);
                    var cu = dc_new.MemberSetting.FirstOrDefault().money;
                    row[5] = (debit - lend).ToString();

                    balance_money += Convert.ToDouble(debit - lend);
                    lend_money += Convert.ToDouble(lend);

                    if (x.state == "入库")
                    {
                        db_number++;
                        db_money += Convert.ToDouble(debit - lend);
                    }
                    else if (x.state == "在用")
                    {
                        on_money += Convert.ToDouble(debit - lend);
                        on_number++;
                    }
                    else if (x.state == "挂失")
                    {
                        lost_money += Convert.ToDouble(debit - lend);
                        lost_number++;
                    }

                    if (t.credits)
                    {
                        var cexpense = dc_new.CardCharge.Where(y => y.CC_CardNo == x.CI_CardNo);
                        var cvs = cexpense.Sum(y => y.expense);
                        if (cvs.HasValue)
                        {
                            double cds = cvs.Value;
                            if (x.CI_CreditsUsed == null)
                                row[6] = (cds / cu).ToString();
                            else
                                row[6] = (cds / cu - x.CI_CreditsUsed).ToString();
                        }
                    }

                    row[7] = lend.ToString();
                    row[8] = x.CI_SendCardOperator;
                    row[9] = x.CI_SendCardDate.ToString();
                    row[10] = cc.Max(y => y.CC_InputDate).ToString();

                    //dgv.Rows.Add(row);
                    this.Invoke(new delegate_add_row(add_row), (Object)row);
                }
                //BathClass.set_dgv_fit(dgv);
                this.Invoke(new delegate_show_info(show_info), null);
            }
            catch (System.Exception ex)
            {
                //BathClass.printErrorMsg(ex.Message);
            }
        }
        private delegate void delegate_add_row(string[] vals);
        private delegate void delegate_show_info();

        private void add_row(string[] vals)
        {
            dgv.Rows.Add(vals);
        }

        private void show_info()
        {
            balance.Text = balance_money.ToString();
            number.Text = cards.Count().ToString();
            expense.Text = lend_money.ToString();

            onMoney.Text = on_money.ToString();
            onNumber.Text = on_number.ToString();

            dbMoney.Text = db_money.ToString();
            dbNumber.Text = db_number.ToString();

            lostMoney.Text = lost_money.ToString();
            lostNumber.Text = lost_number.ToString();

            balance.Visible = true;
            number.Visible = true;
            expense.Visible = true;

            dbMoney.Visible = true;
            dbNumber.Visible = true;

            onMoney.Visible = true;
            onNumber.Visible = true;

            lostMoney.Visible = true;
            lostNumber.Visible = true;
        }

        //显示清单
        public void dgv_show()
        {
            dgv.Rows.Clear();
            m_typeSelName = typeTree.SelectedNode.Text;
            //if (m_thread != null)
            //    m_thread = null;
            if (m_thread != null && m_thread.IsAlive)
            {
                m_thread.Abort();
                m_thread = null;
            }

            //if (m_thread == null)
            m_thread = new Thread(new ThreadStart(do_dgv_show));

            m_thread.Start();
        }

        //新增类型
        private void addType_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            MemberTypeForm addType = new MemberTypeForm(null);
            if (addType.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //删除类型
        private void delType_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            if (typeTree.SelectedNode == null ||
                db.MemberType.FirstOrDefault(x => x.name == typeTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择节点!");
                return;
            }

            string typeSelName = typeTree.SelectedNode.Text;
            int typeSelId = db.MemberType.FirstOrDefault(x => x.name == typeSelName).id;
            if (GeneralClass.printAskMsg("确认删除类型: " + typeSelName + " ?") != DialogResult.Yes)
                return;

            if (db.CardInfo.FirstOrDefault(x => x.CI_CardTypeNo == typeSelId) != null)
            {
                GeneralClass.printErrorMsg("所选择的类型包含会员卡，不能删除!");
                return;
            }

            db.MemberType.DeleteOnSubmit(db.MemberType.FirstOrDefault(x => x.name == typeSelName));
            db.SubmitChanges();
            createTree();
        }

        //编辑类型
        private void editType_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            if (typeTree.SelectedNode == null ||
                db.MemberType.FirstOrDefault(x => x.name == typeTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择类别!");
                return;
            }

            MemberType memberType = db.MemberType.FirstOrDefault(x => x.name == typeTree.SelectedNode.Text);
            MemberTypeForm editType = new MemberTypeForm(memberType);
            if (editType.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //导出清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                BathClass.printErrorMsg("数据正在加载，请稍后！");
                return;
            }
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                BathClass.printErrorMsg("数据正在加载，请稍后！");
                return;
            }
            PrintDGV.Print_DataGridView(dgv, "会员管理", false, "");
        }

        //选择类型节点
        private void typeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv_show();
        }

        //退出
        private void toolExit_Click(object sender, EventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
            this.Close();
        }

        //消费详情
        private void DetailsTool_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            if (dgv.CurrentRow == null)
                return;
            string no = dgv.CurrentRow.Cells[0].Value.ToString();
            MemberDetailsForm memberDetailsForm = new MemberDetailsForm(db, no);
            memberDetailsForm.ShowDialog();
        }

        //编辑会员卡
        private void editTool_Click(object sender, EventArgs e)
        {
            //CardInfo member = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == );
            //OpenCardForm openCardForm = new OpenCardForm(db, member);
            //if (openCardForm.ShowDialog() == DialogResult.OK)
                //dgv_show();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F1:
                    addType_Click(null, null);
                    break;
                case Keys.F2:
                    delType_Click(null, null);
                    break;
                case Keys.F3:
                    editType_Click(null, null);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "会员管理", false, "");
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F8:
                    findTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void findTool_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentRow == null)
                return;
            string no = dgv.CurrentRow.Cells[0].Value.ToString();
            MemberDetailsForm memberDetailsForm = new MemberDetailsForm(db, no);
            memberDetailsForm.ShowDialog();
        }

        //编辑会员信息
        private void toolEditMember_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            if (dgv.CurrentRow == null)
                return;

            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            string no = dgv.CurrentRow.Cells[0].Value.ToString();
            var ci = db.CardInfo.FirstOrDefault(x=>x.CI_CardNo == no);

            EditMemberForm editMemberForm = new EditMemberForm(db, ci);
            if (editMemberForm.ShowDialog() != DialogResult.OK)
                return;

            //dgv_show();
            dgv.CurrentCell = dgv[col, row];
        }

        //扣卡
        private void toolDeduct_Click(object sender, EventArgs e)
        {
            //if (m_thread != null && m_thread.IsAlive)
            //    m_thread.Abort();

            var dc = new BathDBDataContext(LogIn.connectionString);
            DeductedCardForm form = new DeductedCardForm();
            if (BathClass.getAuthority(dc, LogIn.m_User, "扣卡"))
                form.ShowDialog();
            else
            {
                InputEmployeeByPwd inputEmployee = new InputEmployeeByPwd();
                if (inputEmployee.ShowDialog() != DialogResult.OK)
                    return;

                if (!BathClass.getAuthority(dc, inputEmployee.employee, "扣卡"))
                {
                    BathClass.printErrorMsg(inputEmployee.employee.id + "不具有扣卡权限!");
                    return;
                }
                form.ShowDialog();
            }
        }

        private void card_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void MemberManageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }

        //删除会员
        private void tool_del_card_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
                return;

            int row = dgv.CurrentCell.RowIndex;
            int col = dgv.CurrentCell.ColumnIndex;
            string no = dgv.CurrentRow.Cells[0].Value.ToString();
            var ci = db.CardInfo.FirstOrDefault(x => x.CI_CardNo == no);
            if (ci == null)
                return;

            if (ci.state !="入库")
            {
                BathClass.printErrorMsg("不是入库的会员卡，不能删除!");
                return;
            }
            db.CardCharge.DeleteAllOnSubmit(db.CardCharge.Where(x => x.CC_CardNo == no));
            db.CardInfo.DeleteOnSubmit(ci);
            db.SubmitChanges();

            dgv_show();
        }
    }
}
