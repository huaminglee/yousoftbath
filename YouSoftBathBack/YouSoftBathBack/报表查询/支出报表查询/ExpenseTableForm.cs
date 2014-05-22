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
    public partial class ExpenseTableForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> typeList;
        private object del_time_limit;

        //构造函数
        public ExpenseTableForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void ExpenseTableForm_Load(object sender, EventArgs e)
        {
            int year = BathClass.Now(LogIn.connectionString).Year;
            yearBox.Items.Add(year);
            for (int i = 1; i < 11; i++)
            {
                yearBox.Items.Add(year - i);
            }
            yearBox.SelectedIndex = 0;
            monthBox.Text = BathClass.Now(LogIn.connectionString).Month.ToString();

            del_time_limit = get_del_time_limit;
            transactor.Items.AddRange(db.Employee.Select(x => x.name).ToArray());

            createTree();
            dgv_show();

            this.transactor.SelectedIndexChanged += new System.EventHandler(this.timeBox_SelectedIndexChanged);
            this.cTransactor.CheckedChanged += new System.EventHandler(this.cTransactor_CheckedChanged);
        }

        //创建树
        private void createTree()
        {
            typeTree.Nodes.Clear();

            typeList = db.ExpenseType.Select(x => x.name).ToList();
            List<TreeNode> childNodes = new List<TreeNode>();
            foreach (string t in typeList)
            {
                TreeNode node1 = new TreeNode(t);
                node1.Name = t;
                node1.Text = t;
                node1.ImageIndex = 1;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有类别", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            typeTree.Nodes.AddRange(new TreeNode[] { rootNode });
            typeTree.ExpandAll();
            typeTree.SelectedNode = rootNode;
        }

        //显示清单
        private void dgv_show()
        {
            tMoney.Text = "0";
            if (yearBox.Text == "" || monthBox.Text == "" || typeTree.SelectedNode == null)
                return;

            DateTime st = DateTime.Parse(yearBox.Text + "-" + monthBox.Text.PadLeft(2, '0') + "-01" + " 00:00:00");
            DateTime et = st.AddMonths(1);

            var exs = db.Expense.Where(x => x.expenseDate >= st && x.expenseDate <= et);
            if (cTransactor.Checked)
                exs = exs.Where(x => x.transactor == transactor.Text);

            string typeSelName = typeTree.SelectedNode.Text;
            if (typeSelName != "所有类别")
            {
                int typeSelId = db.ExpenseType.FirstOrDefault(x => x.name == typeSelName).id;
                exs = exs.Where(x => x.typeId == typeSelId);
            }

            if (exs.Count() != 0)
                tMoney.Text = exs.Where(x => x.money != null).Sum(x => x.money).ToString();
            dgv.DataSource = from x in exs
                             orderby x.typeId, x.inputDate
                             select new
                             {
                                 编号 = x.id,
                                 名称 = x.name,
                                 类别 = db.ExpenseType.FirstOrDefault(y => y.id == x.typeId).name,
                                 金额 = x.money,
                                 付款方式 = x.payType,
                                 支出日期= x.expenseDate,
                                 输入日期 = x.inputDate,
                                 对象 = x.toWhom,
                                 经手人 = x.transactor,
                                 制表 = x.tableMaker,
                                 审核 = x.checker,
                                 备注 = x.note
                             };
            BathClass.set_dgv_fit(dgv);
        }

        //选择工作节点
        private void typeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv_show();
        }

        //新增类别
        private void addType_Click(object sender, EventArgs e)
        {
            ExpenseTypeForm addJob = new ExpenseTypeForm(db, null);
            if (addJob.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //删除类别
        private void delType_Click(object sender, EventArgs e)
        {
            if (typeTree.SelectedNode == null ||
                db.Job.FirstOrDefault(x => x.name == typeTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择节点!");
                return;
            }

            string jobSelName = typeTree.SelectedNode.Text;
            int jobSelId = db.ExpenseType.FirstOrDefault(x => x.name == jobSelName).id;
            if (GeneralClass.printAskMsg("确认删除类别: " + jobSelName + " ?") != DialogResult.Yes)
                return;

            if (db.Expense.FirstOrDefault(x => x.typeId == jobSelId) != null)
            {
                GeneralClass.printErrorMsg("所选择的类别包含支出，不能删除!");
                return;
            }

            db.ExpenseType.DeleteOnSubmit(db.ExpenseType.FirstOrDefault(x => x.name == jobSelName));
            db.SubmitChanges();
            createTree();
        }

        //编辑类别
        private void editType_Click(object sender, EventArgs e)
        {
            if (typeTree.SelectedNode == null ||
                db.Job.FirstOrDefault(x => x.name == typeTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择类别!");
                return;
            }

            ExpenseType job = db.ExpenseType.FirstOrDefault(x => x.name == typeTree.SelectedNode.Text);
            ExpenseTypeForm editJob = new ExpenseTypeForm(db, job);
            if (editJob.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //新增支出
        private void addTool_Click(object sender, EventArgs e)
        {
            ExpenseForm addEmployee = new ExpenseForm(db, null);
            if (addEmployee.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //删除支出
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            int delId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var expense = db.Expense.FirstOrDefault(s => s.id == delId);


            if (del_time_limit != null)
            {
                if ((BathClass.Now(LogIn.connectionString) - expense.inputDate) >= TimeSpan.Parse("00:" + del_time_limit.ToString() + ":00"))
                {
                    GeneralClass.printErrorMsg("超过支出删除时限，不能删除！");
                    return;
                }
            }
            db.Expense.DeleteOnSubmit(expense);

            db.SubmitChanges();
            dgv_show();
        }

        //编辑支出
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var employee = db.Expense.FirstOrDefault(x => x.id == selId);

            ExpenseForm editEmployee = new ExpenseForm(db, employee);
            if (editEmployee.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //导出清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "支出报表", false, "");
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
                case Keys.F1:
                    addType_Click(null, null);
                    break;
                case Keys.F2:
                    delType_Click(null, null);
                    break;
                case Keys.F3:
                    editType_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "支出报表", false, "");
                    break;
                case Keys.F6:
                    addTool_Click(null, null);
                    break;
                case Keys.F7:
                    delTool_Click(null, null);
                    break;
                case Keys.F8:
                    editTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //经手人
        private void cTransactor_CheckedChanged(object sender, EventArgs e)
        {
            transactor.Enabled = cTransactor.Checked;
            dgv_show();
        }

        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            dgv_show();
        }

        //获取删除支出时限
        private object get_del_time_limit
        {
            get
            {
                if (db.Options.Count() == 0)
                    return null;
                return db.Options.FirstOrDefault().删除支出时限;
            }
        }

        private void timeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv_show();
        }
    }
}
