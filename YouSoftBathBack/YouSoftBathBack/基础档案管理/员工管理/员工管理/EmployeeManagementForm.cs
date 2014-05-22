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

namespace YouSoftBathBack
{
    public partial class EmployeeManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public EmployeeManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);

            InitializeComponent();
        }

        //对话框载入
        private void EmployeeManagementForm_Load(object sender, EventArgs e)
        {
            createTree();
            dgv_show();
        }

        //创建职位树
        private void createTree()
        {
            jobTree.Nodes.Clear();

            TreeNode rootNode = new TreeNode("所有职位");
            rootNode.ImageIndex = 0;

            var jobs = new List<int>();
            create_one_node(db.Job.FirstOrDefault(), jobs, rootNode);

            
            jobTree.Nodes.AddRange(new TreeNode[] { rootNode });
            jobTree.ExpandAll();
            jobTree.SelectedNode = rootNode;
        }

        private void create_one_node(Job job, List<int> jobs, TreeNode pNode)
        {
            jobs.Add(job.id);
            var jobName = job.name;
            var node1 = new TreeNode(jobName);
            node1.Name = jobName;
            node1.Text = jobName;
            node1.ImageIndex = 1;
            node1.SelectedImageIndex = 1;

            if (pNode != null)
                pNode.Nodes.Add(node1);

            var childs = db.Job.Where(x => x.leaderId == job.id);
            if (childs.Any())
            {
                create_one_node(childs.FirstOrDefault(), jobs, node1);
            }

            var job_next = db.Job.FirstOrDefault(x => !jobs.Contains(x.id) && 
                ((job.leaderId==null && x.leaderId==null) || (job.leaderId != null && x.leaderId==job.leaderId)));
            if (job_next == null)
                return;

            create_one_node(job_next, jobs, pNode);
        }

        //显示清单
        public void dgv_show()
        {
            string jobSelName = jobTree.SelectedNode.Text;

            if (jobSelName == "所有职位")
            {
                dgv.DataSource = from x in db.Employee
                                 where x.id != "1000"
                                 orderby x.id
                                 select new
                                 {
                                     工号 = x.id,
                                     姓名 = x.name,
                                     工卡 = x.cardId,
                                     性别 = x.gender,
                                     生日 = x.birthday,
                                     职位 = db.Job.FirstOrDefault(s=>s.id == x.jobId).name,
                                     薪水 = x.salary,
                                     在职 = x.onDuty,
                                     电话 = x.phone,
                                     地址 = x.address,
                                     邮件 = x.email,
                                     入职日期 = x.entryDate,
                                     身份证号 = x.idCard,
                                     备注 = x.note
                                 };
            }
            else
            {
                int jobSelId = db.Job.FirstOrDefault(x => x.name == jobSelName).id;
                dgv.DataSource = from x in db.Employee
                                 where x.jobId == jobSelId && x.id!="1000"
                                 orderby x.id
                                 select new
                                 {
                                     工号 = x.id,
                                     姓名 = x.name,
                                     性别 = x.gender,
                                     生日 = x.birthday,
                                     职位 = db.Job.FirstOrDefault(s => s.id == x.jobId).name,
                                     薪水 = x.salary,
                                     在职 = x.onDuty,
                                     电话 = x.phone,
                                     地址 = x.address,
                                     邮件 = x.email,
                                     入职日期 = x.entryDate,
                                     身份证号 = x.idCard,
                                     备注 = x.note
                                 };
            }
        }

        //选择工作节点
        private void jobTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv_show();
        }

        //新增职位
        private void addOfficer_Click(object sender, EventArgs e)
        {
            JobForm addJob = new JobForm(db, null);
            if (addJob.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //删除职位
        private void delOfficer_Click(object sender, EventArgs e)
        {
            if (jobTree.SelectedNode == null ||
                db.Job.FirstOrDefault(x => x.name == jobTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择节点!");
                return;
            }

            string jobSelName = jobTree.SelectedNode.Text;
            int jobSelId = db.Job.FirstOrDefault(x => x.name == jobSelName).id;
            if (GeneralClass.printAskMsg("确认删除职位: " + jobSelName + " ?") != DialogResult.Yes)
                return;

            if (db.Employee.FirstOrDefault(x => x.jobId == jobSelId) != null)
            {
                GeneralClass.printErrorMsg("所选择的职位包含员工，不能删除!");
                return;
            }

            db.Job.DeleteOnSubmit(db.Job.FirstOrDefault(x => x.name == jobSelName));
            db.Authority.DeleteOnSubmit(db.Authority.FirstOrDefault(x => x.jobId == jobSelId));
            db.SubmitChanges();
            createTree();
        }

        //编辑职位
        private void editOfficer_Click(object sender, EventArgs e)
        {
            if (jobTree.SelectedNode == null ||
                db.Job.FirstOrDefault(x => x.name == jobTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择类别!");
                return;
            }

            Job job = db.Job.FirstOrDefault(x => x.name == jobTree.SelectedNode.Text);
            JobForm editJob = new JobForm(db, job);
            if (editJob.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //新增员工
        private void addTool_Click(object sender, EventArgs e)
        {
            EmployeeForm addEmployee = new EmployeeForm(db, null, this);
            if (addEmployee.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //删除员工
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            string delId = dgv.CurrentRow.Cells[0].Value.ToString();
            db.Employee.DeleteOnSubmit(db.Employee.FirstOrDefault(s => s.id == delId));
            
            var authEmployee = db.Authority.FirstOrDefault(x => x.emplyeeId == delId);
            if (authEmployee != null)
                db.Authority.DeleteOnSubmit(authEmployee);

            db.SubmitChanges();
            dgv_show();
        }

        //编辑员工
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            string selId = dgv.CurrentRow.Cells[0].Value.ToString();
            var employee = db.Employee.FirstOrDefault(x => x.id == selId);

            EmployeeForm editEmployee = new EmployeeForm(db, employee, this);
            if (editEmployee.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //员工权限
        private void authoTool_Click(object sender, EventArgs e)
        {
            if (!BathClass.getAuthority(db, LogIn.m_User, "权限管理"))
            {
                GeneralClass.printErrorMsg("没有权限！");
                return;
            }

            if (dgv.CurrentCell == null)
            {
                GeneralClass.printWarningMsg("没有选择行!");
                return;
            }

            string selId = dgv.CurrentRow.Cells[0].Value.ToString();
            var employee = db.Employee.FirstOrDefault(x => x.id == selId);

            EmployeeAuthorityForm eauthoForm = new EmployeeAuthorityForm(db, employee);
            eauthoForm.ShowDialog();
        }

        //修改密码
        private void modifyPwdTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printWarningMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("恢复初始密码?") != DialogResult.Yes)
                return;

            string selId = dgv.CurrentRow.Cells[0].Value.ToString();
            var employee = db.Employee.FirstOrDefault(x => x.id == selId);

            employee.password = IOUtil.GetMD5("12345678");
            db.SubmitChanges();
        }

        //导出清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "员工管理", false, "");
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
                    addOfficer_Click(null, null);
                    break;
                case Keys.F2:
                    delOfficer_Click(null, null);
                    break;
                case Keys.F3:
                    editOfficer_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "员工管理", false, "");
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
                case Keys.F9:
                    authoTool_Click(null, null);
                    break;
                case Keys.F10:
                    modifyPwdTool_Click(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}
