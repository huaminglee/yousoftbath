using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MsgPanel;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Threading;
using YouSoftUtil;
using YouSoftBathConstants;

namespace YouSoftBathBack
{
    public partial class DepartLogMgForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Thread m_thread;
        private string selNode;
        private string connectionIP;

        //构造函数
        public DepartLogMgForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void EmployeeManagementForm_Load(object sender, EventArgs e)
        {
            connectionIP = IOUtil.get_config_by_key(ConfigKeys.KEY_CONNECTION_IP);
            DtpSt.Value = DateTime.Now.AddMonths(-1);
            createTree();
        }

        //创建职位树
        public void createTree()
        {
            jobTree.Nodes.Clear();

            var childNodes = new List<TreeNode>();
            foreach (var d in db.Department)
            {
                var dName = d.name;
                var node1 = new TreeNode(dName);
                node1.Name = dName;
                node1.Text = dName;
                node1.ImageIndex = 1;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有部门", childNodes.ToArray());
            rootNode.ImageIndex = 0;

            jobTree.Nodes.AddRange(new TreeNode[] { rootNode });
            jobTree.ExpandAll();
            jobTree.SelectedNode = rootNode;
        }

        //显示清单
        public void dgv_show()
        {
            try
            {
                this.Invoke(new delegate_no_para(clear_all_panels));
                var logs = db.DepartmentLog.Where(x => x.date >= DtpSt.Value && x.date <= DtpEt.Value);
                if (selNode != "所有部门")
                {
                    var depart = db.Department.FirstOrDefault(x => x.name == selNode);
                    if (depart == null)
                    {
                        BathClass.printErrorMsg("所选节点异常，请确认!");
                        return;
                    }
                    logs = logs.Where(x => x.departId == depart.id);
                }

                logs = logs.OrderByDescending(x => x.date);
                int v_space = 15;
                int y = v_space;
                foreach (var log in logs)
                {
                    object[] args = new object[3];
                    args[0] = log;
                    args[1] = y;
                    args[2] = v_space;
                    this.Invoke(new create_msgPanel_delegate(create_msgPanel), args);
                    y = Convert.ToInt32(args[1]);
                }

            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        private delegate void delegate_no_para();
        private void clear_all_panels()
        {
            PanelLogs.Controls.Clear();
            PanelLogs.Focus();
        }

        private delegate void create_msgPanel_delegate(DepartmentLog log, ref int y, int v_space);
        private void create_msgPanel(DepartmentLog log, ref int y, int v_space)
        {
            int w = PanelLogs.Width;
            var p = new MsgPanel.LogPanel();
            p.Location = new Point((w - p.Width) / 2, y);
            p.setDate(log.date.Value);
            p.setSender(log.sender);
            p.setToDepart(db.Department.FirstOrDefault(x => x.id == log.departId).name);
            PanelLogs.Controls.Add(p);

            string img1Url = log.imgUrl;
            string img2Url = log.img2Url;
            string img3Url = log.img3Url;

            if (img1Url != null)
                img1Url = "ftp://" + connectionIP + "/" + img1Url;

            if (img2Url != null)
                img2Url = "ftp://" + connectionIP + "/" + img2Url;

            if (img3Url != null)
                img3Url = "ftp://" + connectionIP + "/" + img3Url;
            p.setImg(img1Url, img2Url, img3Url);

            p.id = log.id;
            p.setMsg(log.msg);

            bool done = MConvert<bool>.ToTypeOrDefault(log.done, false);
            bool urgent = MConvert<bool>.ToTypeOrDefault(log.urgent, false);
            p.set_panel_status(done, urgent, log.doneDate, log.urgentDate, log.dueTime);

            p.BtnSetTimeClicked +=new LogPanel.BtnSetTimeClickHandle(p_BtnSetTimeClicked);
            p.BtnUrgentClicked += new LogPanel.BtnUrgentClickHandle(p_BtnUrgentClicked);
            p.BtnDoneClicked += new LogPanel.BtnDoneClickHandle(p_BtnDoneClicked);
            y += p.Size.Height + v_space;
        }

        private void p_BtnSetTimeClicked(object sender, EventArgs e)
        {
            LogPanel logPanel = sender as LogPanel;
            var log = db.DepartmentLog.FirstOrDefault(x => x.id == logPanel.id);

            var form = new DaysLimitForm();
            if (form.ShowDialog() != DialogResult.OK)
                return;

            log.dueTime = form.dt;
            db.SubmitChanges();

            logPanel.set_Due_text(log.dueTime);
        }

        private void p_BtnUrgentClicked(object sender, EventArgs e)
        {
            LogPanel logPanel = sender as LogPanel;
            var log = db.DepartmentLog.FirstOrDefault(x => x.id == logPanel.id);

            if (MConvert<bool>.ToTypeOrDefault(log.done, false))
            {
                if (BathClass.printAskMsg("事件已完成，加急将会把事件改成未完成状态，确定继续?") != DialogResult.Yes)
                    return;

                log.done = false;
                logPanel.set_btnDone_text("已完成");
            }

            if (!MConvert<bool>.ToTypeOrDefault(log.urgent, false))
            {
                log.urgent = true;
                log.urgentDate = DateTime.Now;
            }
            else
            {
                log.urgent = false;
            }
            db.SubmitChanges();

            bool done = MConvert<bool>.ToTypeOrDefault(log.done, false);
            bool urgent = MConvert<bool>.ToTypeOrDefault(log.urgent, false);
            logPanel.set_panel_status(done, urgent, log.doneDate, log.urgentDate, log.dueTime);
        }

        private void p_BtnDoneClicked(object sender, EventArgs e)
        {
            LogPanel logPanel = sender as LogPanel;
            var log = db.DepartmentLog.FirstOrDefault(x => x.id == logPanel.id);

            if (MConvert<bool>.ToTypeOrDefault(log.done, false))
            {
                log.done = false;
            }
            else
            {
                log.done = true;
                log.urgent = false;
                log.doneDate = DateTime.Now;
                logPanel.set_btnUrgent_text("加急");
            }
            db.SubmitChanges();

            bool done = MConvert<bool>.ToTypeOrDefault(log.done, false);
            bool urgent = MConvert<bool>.ToTypeOrDefault(log.urgent, false);
            logPanel.set_panel_status(done, urgent, log.doneDate, log.urgentDate, log.dueTime);
        }

        //选择工作节点
        private void jobTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
            {
                m_thread.Abort();
                m_thread = null;
            }
            selNode = jobTree.SelectedNode.Text;
            m_thread = new Thread(new ThreadStart(dgv_show));
            m_thread.Start();
        }

        //新增部门
        private void addOfficer_Click(object sender, EventArgs e)
        {
            var form = new DepartmentForm(db, null, this);
            form.ShowDialog();
        }

        //删除部门
        private void delOfficer_Click(object sender, EventArgs e)
        {
            
            if (jobTree.SelectedNode == null)
            {
                GeneralClass.printErrorMsg("没有选择节点!");
                return;
            }

            string jobSelName = jobTree.SelectedNode.Text;
            var depart = db.Department.FirstOrDefault(x => x.name == jobSelName);
            if (depart == null)
            {
                BathClass.printErrorMsg("所选节点异常，请确认!");
                return;
            }

            int jobSelId = depart.id;
            if (GeneralClass.printAskMsg("确认删除部门: " + jobSelName + " ?") != DialogResult.Yes)
                return;

            if (db.Job.FirstOrDefault(x => x.departId == jobSelId) != null)
            {
                GeneralClass.printErrorMsg("所选择的部门包含职位，不能删除!");
                return;
            }

            db.Department.DeleteOnSubmit(depart);
            db.SubmitChanges();
            createTree();
        }

        //编辑部门
        private void editOfficer_Click(object sender, EventArgs e)
        {
            if (jobTree.SelectedNode == null)
            {
                GeneralClass.printErrorMsg("没有选择类别!");
                return;
            }

            var depart = db.Department.FirstOrDefault(x => x.name == jobTree.SelectedNode.Text);
            if (depart == null)
            {
                BathClass.printErrorMsg("所选节点异常，请确认!");
                return;
            }

            var form = new DepartmentForm(db, depart, this);
            if (form.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //新增员工
        private void addTool_Click(object sender, EventArgs e)
        {
        }

        //删除员工
        private void delTool_Click(object sender, EventArgs e)
        {
        }

        //编辑员工
        private void editTool_Click(object sender, EventArgs e)
        {
        }

        //员工权限
        private void authoTool_Click(object sender, EventArgs e)
        {
        }

        //修改密码
        private void modifyPwdTool_Click(object sender, EventArgs e)
        {
        }

        //导出清单
        private void exportTool_Click(object sender, EventArgs e)
        {
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
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
                    break;
                case Keys.F5:
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

        //发表新帖
        private void ToolNewPost_Click(object sender, EventArgs e)
        {
            var form = new NewPostForm(db);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            dgv_show();
        }

        //查询
        private void BtnFind_Click(object sender, EventArgs e)
        {
            dgv_show();
        }
    }
}
