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

namespace YouSoftBathTechnician
{
    public partial class TechListForm : Form
    {
        //成员变量
        private string m_con_str;

        //构造函数
        public TechListForm(string _con_str)
        {
            m_con_str = _con_str;
            InitializeComponent();
        }

        //对话框载入
        private void SeatTypeManagementForm_Load(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(m_con_str);
            createTree(db);
            dgv_show(db);
        }

        //创建树
        private void createTree(BathDBDataContext db)
        {
            var techTypes = db.Job.Where(x => x.name.Contains("技师")).Select(x => x.name).ToArray();
            seatTypeTree.Nodes.Clear();

            List<TreeNode> childNodes = new List<TreeNode>();
            foreach (string seattype in techTypes)
            {
                TreeNode node1 = new TreeNode(seattype);
                node1.Name = seattype;
                node1.Text = seattype;
                node1.ImageIndex = 1;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有技师", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            seatTypeTree.Nodes.AddRange(new TreeNode[] { rootNode });
            seatTypeTree.ExpandAll();
            seatTypeTree.SelectedNode = rootNode;
        }

        //显示清单
        public void dgv_show(BathDBDataContext db)
        {
            DgvFemale.Rows.Clear();
            DgvMale.Rows.Clear();
            if (!db.TechIndex.Any())
            {
                if (BathClass.printAskMsg("没有检测到排钟表，是否初始化？") != DialogResult.Yes)
                    return;

                toolReArrange_Click(null, null);
            }
            string typeSelName = seatTypeTree.SelectedNode.Text;

            if (typeSelName == "所有技师")
                return;

            var jobId = db.Job.FirstOrDefault(x => x.name == typeSelName).id;

            var techIdex_Female = db.TechIndex.FirstOrDefault(x => x.dutyid == jobId && x.gender == "女");
            if (techIdex_Female != null)
            {
                var techIndex_Female_ids = techIdex_Female.ids;
                var tech_ids = techIndex_Female_ids.Split('%');
                foreach (var tech_id_index in tech_ids)
                {
                    var tech_id = tech_id_index.Split('=')[0];
                    var user = db.Employee.FirstOrDefault(x => x.id == tech_id);
                    if (user == null) continue;
                    DgvFemale.Rows.Add(tech_id, user.name);
                }
            }

            var techIdex_Male = db.TechIndex.FirstOrDefault(x => x.dutyid == jobId && x.gender == "男");
            if (techIdex_Male != null)
            {
                var techIndex_Male_ids = techIdex_Male.ids;
                var tech_ids = techIndex_Male_ids.Split('%');
                foreach (var tech_id_index in tech_ids)
                {
                    var tech_id = tech_id_index.Split('=')[0];
                    var user = db.Employee.FirstOrDefault(x => x.id == tech_id);
                    if (user == null) continue;
                    DgvMale.Rows.Add(tech_id, user.name);
                }
            }
            
        }


        //导出手牌清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(DgvFemale);
        }

        //选择手牌类型节点
        private void seatTypeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var db = new BathDBDataContext(m_con_str);
            dgv_show(db);
        }

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
                    break;
                case Keys.F2:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(DgvFemale);
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                default:
                    break;
            }
        }

        //全部重排
        private void toolReArrange_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(m_con_str);
            dc.ExecuteCommand("truncate table techindex");
            dc.SubmitChanges();

            var job_ids = dc.Job.Where(x => x.name.Contains("技师")).Select(x => x.id);
            foreach (var job_id in job_ids)
            {
                var techs = dc.Employee.Where(x => x.jobId == job_id);
                
                techs = techs.Where(x => x.techStatus == null || x.techStatus == "空闲" || x.techStatus == "待钟");

                var techs_male = techs.Where(x=>x.gender=="男");
                var techIndex = new TechIndex();
                techIndex.dutyid = job_id;
                techIndex.gender = "男";
                techIndex.ids = string.Join("%", techs_male.OrderBy(x => x.id).Select(x => x.id + "=T").ToArray());
                dc.TechIndex.InsertOnSubmit(techIndex);

                var techs_female = techs.Where(x => x.gender == "女");
                techIndex = new TechIndex();
                techIndex.dutyid = job_id;
                techIndex.gender = "女";
                techIndex.ids = string.Join("%", techs_female.OrderBy(x => x.id).Select(x => x.id + "=T").ToArray());
                dc.TechIndex.InsertOnSubmit(techIndex);
            }
            dc.SubmitChanges();
            dgv_show(dc);
        }

        //女技师重排
        private void BtnRearrangeFemale_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(m_con_str);

            string typeSelName = seatTypeTree.SelectedNode.Text;

            if (typeSelName == "所有技师")
                return;

            var job_id = dc.Job.FirstOrDefault(x => x.name == typeSelName).id;
            var techs = dc.Employee.Where(x => x.jobId == job_id);

            techs = techs.Where(x => x.techStatus == null || x.techStatus == "空闲" || x.techStatus == "待钟");

            var techs_male = techs.Where(x => x.gender == "女");

            var techIndex = dc.TechIndex.FirstOrDefault(x => x.dutyid == job_id && x.gender == "女");
            bool new_techIndex = false;
            if (techIndex == null)
            {
                new_techIndex = true;
                techIndex = new TechIndex();
            }
            techIndex.dutyid = job_id;
            techIndex.gender = "女";
            techIndex.ids = string.Join("%", techs_male.OrderBy(x => x.id).Select(x => x.id + "=T").ToArray());

            if (new_techIndex)
                dc.TechIndex.InsertOnSubmit(techIndex);

            dc.SubmitChanges();
            dgv_show(dc);
        }

        //女技师向上
        private void BtnUpFemale_Click(object sender, EventArgs e)
        {
            if (DgvFemale.CurrentCell == null)
            {
                BathClass.printErrorMsg("没有选择行!");
                return;
            }

            if (DgvFemale.CurrentCell.RowIndex == 0)
            {
                BathClass.printErrorMsg("已经排首排，不能提前");
                return;
            }

            var db = new BathDBDataContext(m_con_str);
            var tech_id = DgvFemale.CurrentRow.Cells[0].Value.ToString();
            var job_id = db.Employee.FirstOrDefault(x => x.id == tech_id).jobId;
            var tech_index = db.TechIndex.FirstOrDefault(x => x.dutyid == job_id && x.gender == "女");
            var old_index = tech_index.ids.Split('%');

            int col_index = DgvFemale.CurrentCell.ColumnIndex;
            int row_index = DgvFemale.CurrentCell.RowIndex;
            int count = DgvFemale.Rows.Count;
            var new_index = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i + 1 == row_index)
                {
                    new_index.Add(old_index[i + 1]);
                    new_index.Add(old_index[i]);
                    i++;
                }
                else
                {
                    new_index.Add(old_index[i]);
                }
            }

            tech_index.ids = string.Join("%", new_index.ToArray());
            db.SubmitChanges();
            dgv_show(db);
            DgvFemale.CurrentCell = DgvFemale[col_index, row_index - 1];
        }

        //女技师向下
        private void BtnDownFemale_Click(object sender, EventArgs e)
        {
            if (DgvFemale.CurrentCell == null)
            {
                BathClass.printErrorMsg("没有选择行!");
                return;
            }

            if (DgvFemale.CurrentCell.RowIndex == DgvFemale.Rows.Count - 1)
            {
                BathClass.printErrorMsg("已经排末排，不能退后");
                return;
            }

            var db = new BathDBDataContext(m_con_str);
            var tech_id = DgvFemale.CurrentRow.Cells[0].Value.ToString();
            var job_id = db.Employee.FirstOrDefault(x => x.id == tech_id).jobId;
            var tech_index = db.TechIndex.FirstOrDefault(x => x.dutyid == job_id && x.gender == "女");
            var old_index = tech_index.ids.Split('%');

            int col_index = DgvFemale.CurrentCell.ColumnIndex;
            int row_index = DgvFemale.CurrentCell.RowIndex;
            int count = DgvFemale.Rows.Count;
            var new_index = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i == row_index)
                {
                    new_index.Add(old_index[i + 1]);
                    new_index.Add(old_index[i]);
                    i++;
                }
                else
                {
                    new_index.Add(old_index[i]);
                }
            }

            tech_index.ids = string.Join("%", new_index.ToArray());
            db.SubmitChanges();
            dgv_show(db);
            DgvFemale.CurrentCell = DgvFemale[col_index, row_index + 1];
        }

        //男技师重排
        private void BtnRearrangeMale_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(m_con_str);

            string typeSelName = seatTypeTree.SelectedNode.Text;

            if (typeSelName == "所有技师")
                return;

            var job_id = dc.Job.FirstOrDefault(x => x.name == typeSelName).id;
            var techs = dc.Employee.Where(x => x.jobId == job_id);
            
            techs = techs.Where(x => x.techStatus == null || x.techStatus == "空闲" || x.techStatus == "待钟");

            var techs_male = techs.Where(x => x.gender == "男");

            var techIndex = dc.TechIndex.FirstOrDefault(x => x.dutyid == job_id && x.gender == "男");
            bool new_techIndex = false;
            if (techIndex == null)
            {
                new_techIndex = true;
                techIndex = new TechIndex();
            }
            techIndex.dutyid = job_id;
            techIndex.gender = "男";
            techIndex.ids = string.Join("%", techs_male.OrderBy(x => x.id).Select(x => x.id + "=T").ToArray());
            
            if (new_techIndex)
                dc.TechIndex.InsertOnSubmit(techIndex);

            dc.SubmitChanges();
            dgv_show(dc);
        }

        //男技师向上
        private void BtnUpMale_Click(object sender, EventArgs e)
        {
            if (DgvMale.CurrentCell == null)
            {
                BathClass.printErrorMsg("没有选择行!");
                return;
            }

            if (DgvMale.CurrentCell.RowIndex == 0)
            {
                BathClass.printErrorMsg("已经排首排，不能提前");
                return;
            }

            var db = new BathDBDataContext(m_con_str);
            var tech_id = DgvMale.CurrentRow.Cells[0].Value.ToString();
            var job_id = db.Employee.FirstOrDefault(x => x.id == tech_id).jobId;
            var tech_index = db.TechIndex.FirstOrDefault(x => x.dutyid == job_id && x.gender=="男");
            var old_index = tech_index.ids.Split('%');

            int col_index = DgvMale.CurrentCell.ColumnIndex;
            int row_index = DgvMale.CurrentCell.RowIndex;
            int count = DgvMale.Rows.Count;
            var new_index = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i + 1 == row_index)
                {
                    new_index.Add(old_index[i + 1]);
                    new_index.Add(old_index[i]);
                    i++;
                }
                else
                {
                    new_index.Add(old_index[i]);
                }
            }

            tech_index.ids = string.Join("%", new_index.ToArray());
            db.SubmitChanges();
            dgv_show(db);
            DgvMale.CurrentCell = DgvMale[col_index, row_index - 1];
        }

        //男技师向下
        private void BtnDownMale_Click(object sender, EventArgs e)
        {
            if (DgvMale.CurrentCell == null)
            {
                BathClass.printErrorMsg("没有选择行!");
                return;
            }

            if (DgvMale.CurrentCell.RowIndex == DgvMale.Rows.Count - 1)
            {
                BathClass.printErrorMsg("已经排末排，不能退后");
                return;
            }

            var db = new BathDBDataContext(m_con_str);
            var tech_id = DgvMale.CurrentRow.Cells[0].Value.ToString();
            var job_id = db.Employee.FirstOrDefault(x => x.id == tech_id).jobId;
            var tech_index = db.TechIndex.FirstOrDefault(x => x.dutyid == job_id && x.gender=="男");
            var old_index = tech_index.ids.Split('%');

            int col_index = DgvMale.CurrentCell.ColumnIndex;
            int row_index = DgvMale.CurrentCell.RowIndex;
            int count = DgvMale.Rows.Count;
            var new_index = new List<string>();
            for (int i = 0; i < count; i++)
            {
                if (i == row_index)
                {
                    new_index.Add(old_index[i + 1]);
                    new_index.Add(old_index[i]);
                    i++;
                }
                else
                {
                    new_index.Add(old_index[i]);
                }
            }

            tech_index.ids = string.Join("%", new_index.ToArray());
            db.SubmitChanges();
            dgv_show(db);
            DgvMale.CurrentCell = DgvMale[col_index, row_index + 1];
        }
    }
}
