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
    public partial class SeatManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private List<string> seatTypeList;

        //构造函数
        public SeatManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void SeatTypeManagementForm_Load(object sender, EventArgs e)
        {
            createTree();
            dgv_show();
        }

        //创建树
        private void createTree()
        {
            seatTypeTree.Nodes.Clear();

            seatTypeList = db.SeatType.Select(x => x.name).ToList();
            List<TreeNode> childNodes = new List<TreeNode>();
            foreach (string seattype in seatTypeList)
            {
                TreeNode node1 = new TreeNode(seattype);
                node1.Name = seattype;
                node1.Text = seattype;
                node1.ImageIndex = 1;
                node1.SelectedImageIndex = 1;
                childNodes.Add(node1);
            }

            TreeNode rootNode = new TreeNode("所有手牌", childNodes.ToArray());
            rootNode.ImageIndex = 0;
            seatTypeTree.Nodes.AddRange(new TreeNode[] { rootNode });
            seatTypeTree.ExpandAll();
            seatTypeTree.SelectedNode = rootNode;
        }

        //显示清单
        public void dgv_show()
        {
            string typeSelName = seatTypeTree.SelectedNode.Text;

            if (typeSelName == "所有手牌")
            {
                dgv.DataSource = from x in db.Seat
                                 orderby x.text
                                 select new
                                 {
                                     编号 = x.text,
                                     信息 = x.name,
                                     绑定编码 = x.oId,
                                     类别 = db.SeatType.FirstOrDefault(y => y.id == x.typeId).name,
                                     备注 = x.note
                                 };
            }
            else
            {
                int typeSelId = db.SeatType.FirstOrDefault(x => x.name == typeSelName).id;
                dgv.DataSource = from x in db.Seat
                                 where x.typeId == typeSelId
                                 orderby x.text
                                 select new
                                 {
                                     编号 = x.text,
                                     信息 = x.name,
                                     绑定编码 = x.oId,
                                     类别 = db.SeatType.FirstOrDefault(y => y.id == x.typeId).name,
                                     备注 = x.note
                                 };
            }
        }

        //新增手牌类型
        private void addSeatType_Click(object sender, EventArgs e)
        {
            SeatTypeForm addSeatType = new SeatTypeForm(db, null);
            if (addSeatType.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //删除手牌类型
        private void delSeatType_Click(object sender, EventArgs e)
        {
            if (seatTypeTree.SelectedNode == null ||
                db.SeatType.FirstOrDefault(x => x.name == seatTypeTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择节点!");
                return;
            }

            string typeSelName = seatTypeTree.SelectedNode.Text;
            int typeSelId = db.SeatType.FirstOrDefault(x => x.name == typeSelName).id;
            if (GeneralClass.printAskMsg("确认删除类型: " + typeSelName + " ?") != DialogResult.Yes)
                return;

            if (db.Seat.FirstOrDefault(x => x.typeId == typeSelId) != null)
            {
                GeneralClass.printErrorMsg("所选择的手牌类型包含手牌，不能删除!");
                return;
            }

            db.SeatType.DeleteOnSubmit(db.SeatType.FirstOrDefault(x => x.name == typeSelName));
            db.SubmitChanges();
            createTree();
        }

        //编辑手牌类型
        private void editSeatType_Click(object sender, EventArgs e)
        {
            if (seatTypeTree.SelectedNode == null ||
                db.SeatType.FirstOrDefault(x => x.name == seatTypeTree.SelectedNode.Text) == null)
            {
                GeneralClass.printErrorMsg("没有选择类别!");
                return;
            }

            SeatType seatType = db.SeatType.FirstOrDefault(x => x.name == seatTypeTree.SelectedNode.Text);
            SeatTypeForm editType = new SeatTypeForm(db, seatType);
            if (editType.ShowDialog() == DialogResult.OK)
                createTree();
        }

        //新增手牌
        private void addTool_Click(object sender, EventArgs e)
        {
            SeatForm addSeat = new SeatForm(db, null, this);
            addSeat.ShowDialog();
        }

        //删除手牌
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            db.Seat.DeleteOnSubmit(db.Seat.FirstOrDefault(s => s.text == dgv.CurrentRow.Cells[0].Value.ToString()));
            db.SubmitChanges();
            dgv_show();
        }

        //编辑手牌
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            int r = dgv.CurrentCell.RowIndex;
            int c = dgv.CurrentCell.ColumnIndex;

            var seat = db.Seat.FirstOrDefault(x => x.text == dgv.CurrentRow.Cells[0].Value.ToString());

            SeatForm editSeat = new SeatForm(db, seat, this);
            if (editSeat.ShowDialog() == DialogResult.OK)
            {
                dgv_show();
                dgv.CurrentCell = dgv[c, r];
            }
        }

        //导出手牌清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "手牌管理", false, "");
        }

        //选择手牌类型节点
        private void seatTypeTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            dgv_show();
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
                    addSeatType_Click(null, null);
                    break;
                case Keys.F2:
                    delSeatType_Click(null, null);
                    break;
                case Keys.F3:
                    editSeatType_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "手牌管理", false, "");
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
    }
}
