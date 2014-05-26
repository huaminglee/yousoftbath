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
using YouSoftBathConstants;

namespace YouSoftBathBack
{
    public partial class MenuManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> catList;

        //构造函数
        public MenuManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString) ;

            InitializeComponent();
        }

        //对话框载入
        private void MenuManagementForm_Load(object sender, EventArgs e)
        {
            createTree();
            dgv_show();
        }

        //创建树
        private void createTree()
        {
            catTree.Nodes.Clear();

            catList = db.Catgory.Select(x => x.name).ToList();
            List<TreeNode> childNodes = new List<TreeNode>();
            foreach (string catName in catList)
            {
                TreeNode node1 = new TreeNode(catName);
                node1.Name = catName;
                node1.Text = catName;
                node1.ImageIndex = 0;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有类别", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            catTree.Nodes.AddRange(new TreeNode[] { rootNode });
            catTree.ExpandAll();
            catTree.SelectedNode = rootNode;
        }

        //显示清单
        public void dgv_show()
        {
            string catSel = catTree.SelectedNode.Text;

            if (catSel == "所有类别")
            {
                dgv.DataSource = from x in db.Menu
                                 orderby x.id
                                 select new
                                 {
                                     项目编号 = x.id,
                                     名称 = x.name,
                                     类别 = db.Catgory.FirstOrDefault(s => s.id == x.catgoryId).name,
                                     单位 = x.unit,
                                     单价 = x.price,
                                     技师 = x.technician,
                                     服务员 = x.waiter == null ? false : x.waiter.Value,
                                     限时方式 = x.timeLimitType,
                                     限时 = x.timeLimitHour.ToString() + ":" + x.timeLimitMiniute.ToString(),
                                     自动超时加收 = x.addAutomatic,
                                     加收方式 = x.addType,
                                     价格价格 = x.addMoney,
                                     说明 = x.note
                                 };
            }
            else
            {
                int catSelId = db.Catgory.FirstOrDefault(x => x.name == catSel).id;
                dgv.DataSource = from x in db.Menu
                                 where x.catgoryId == catSelId
                                 orderby x.id
                                 select new
                                 {
                                     项目编号 = x.id,
                                     名称 = x.name,
                                     类别 = db.Catgory.FirstOrDefault(s => s.id == x.catgoryId).name,
                                     单位 = x.unit,
                                     单价 = x.price,
                                     技师 = x.technician,
                                     限时方式 = x.timeLimitType,
                                     限时 = x.timeLimitHour.ToString() + ":" + x.timeLimitMiniute.ToString(),
                                     自动超时加收 = x.addAutomatic,
                                     加收方式 = x.addType,
                                     价格价格 = x.addMoney,
                                     说明 = x.note
                                 };
            }
        }

        //新增类别
        private void addCat_Click(object sender, EventArgs e)
        {
            MenuCatgoryForm addCat = new MenuCatgoryForm(db, null);
            if (addCat.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //删除类别
        private void delCat_Click(object sender, EventArgs e)
        {
            if (catTree.SelectedNode == null ||
                db.Catgory.FirstOrDefault(x => x.name == catTree.SelectedNode.Text) == null)
            {
                GeneralClass.printWarningMsg("没有选择节点!");
                return;
            }

            string catSel = catTree.SelectedNode.Text;
            int catSelId = db.Catgory.FirstOrDefault(x => x.name == catSel).id;
            if (GeneralClass.printAskMsg("确认删除类别: " + catSel + " ?") != DialogResult.Yes)
                return;

            if (db.Menu.FirstOrDefault(x => x.catgoryId == catSelId) != null)
            {
                GeneralClass.printErrorMsg("所选择的职位包含菜式，不能删除!");
                return;
            }

            db.Catgory.DeleteOnSubmit(db.Catgory.FirstOrDefault(x => x.name == catSel));
            db.SubmitChanges();
            createTree();
        }

        //编辑类别
        private void editCat_Click(object sender, EventArgs e)
        {
            if (catTree.SelectedNode == null ||
                db.Catgory.FirstOrDefault(x => x.name == catTree.SelectedNode.Text) == null)
            {
                GeneralClass.printWarningMsg("没有选择类别!");
                return;
            }

            Catgory cat = db.Catgory.FirstOrDefault(x => x.name == catTree.SelectedNode.Text);
            MenuCatgoryForm editCat = new MenuCatgoryForm(db, cat);
            if (editCat.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //新增菜式
        private void addTool_Click(object sender, EventArgs e)
        {
            MenuForm addMenu = new MenuForm(db, null, catTree.SelectedNode.Text, this);
            addMenu.ShowDialog();
        }

        //删除菜式
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printWarningMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            int delId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);

            bool be_contained_in_combo = false;
            List<int> combo_ids = new List<int>();
            foreach (var combo in db.Combo)
            {
                var ids = BathClass.disAssemble(combo.menuIds);
                var freeIds = BathClass.disAssemble(combo.freeMenuIds);
                if (ids.Contains(delId) || freeIds.Contains(delId))
                {
                    be_contained_in_combo = true;
                    combo_ids.Add(combo.id);
                }
            }
            if (be_contained_in_combo &&
                BathClass.printAskMsg("套餐" + combo_ids[0] + "等中已经包含该项目，若删除项目，则套餐也会被删除，是否删除?")
                    != DialogResult.Yes)
                return;

            bool be_contained_in_bigcombo = false;
            List<int> bigcombo_ids = new List<int>();
            foreach (var combo in db.BigCombo)
            {
                var ids = BathClass.disAssemble(combo.substmenuid, Constants.SplitChar);
                if (ids.Contains(delId) || combo.menuid == delId)
                {
                    be_contained_in_bigcombo = true;
                    bigcombo_ids.Add(combo.id);
                }
            }
            if (be_contained_in_bigcombo &&
                BathClass.printAskMsg("大项套餐" + bigcombo_ids[0] + "等中已经包含该项目，若删除项目，则套餐也会被删除，是否删除?")
                    != DialogResult.Yes)
                return;

            db.Menu.DeleteOnSubmit(db.Menu.FirstOrDefault(s => s.id == delId));
            if (be_contained_in_combo)
                db.Combo.DeleteAllOnSubmit(db.Combo.Where(x => combo_ids.Contains(x.id)));
            if (be_contained_in_bigcombo)
                db.BigCombo.DeleteAllOnSubmit(db.BigCombo.Where(x => bigcombo_ids.Contains(x.id)));
            db.SubmitChanges();
            dgv_show();
        }

        //编辑菜式
        private void editGoods_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printWarningMsg("没有选择行!");
                return;
            }

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var menu = db.Menu.FirstOrDefault(x => x.id == selId);

            MenuForm editMenu = new MenuForm(db, menu, "", this);
            if (editMenu.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //导出清单
        private void exportGoods_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "项目管理", false, "");
        }

        //离开
        private void exitTool_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //选择节点
        private void catTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv_show();
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
                    addCat_Click(null, null);
                    break;
                case Keys.F2:
                    delCat_Click(null, null);
                    break;
                case Keys.F3:
                    editCat_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "项目管理", false, "");
                    break;
                case Keys.F6:
                    addTool_Click(null, null);
                    break;
                case Keys.F7:
                    delTool_Click(null, null);
                    break;
                case Keys.F8:
                    editGoods_Click(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}
