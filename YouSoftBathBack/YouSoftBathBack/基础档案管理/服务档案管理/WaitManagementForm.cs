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
    public partial class WaitManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public WaitManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void RoomManagementForm_Load(object sender, EventArgs e)
        {
            dgv_show();
        }

        //显示清单
        public void dgv_show()
        {
            dgv.DataSource = from x in db.WaiterItem
                             orderby x.id
                             select new
                             {
                                 编号 = x.id,
                                 名称 = x.name,
                                 备注 = x.note
                             };
            BathClass.set_dgv_fit(dgv);
        }

        //新增
        private void addTool_Click(object sender, EventArgs e)
        {
            WaitForm addRoom = new WaitForm(db, null, this);
            addRoom.ShowDialog();
            dgv_show();
        }

        //删除
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            db.WaiterItem.DeleteOnSubmit(db.WaiterItem.FirstOrDefault(s => s.id == selId));
            db.SubmitChanges();
            dgv_show();
        }

        //编辑
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            int selId = Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
            var room = db.WaiterItem.FirstOrDefault(x => x.id == selId);

            WaitForm editRoom = new WaitForm(db, room, this);
            if (editRoom.ShowDialog() == DialogResult.OK)
                dgv_show();
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgv);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgv, "服务项目管理", false, "");
        }

        //退出
        private void exitTool_Click(object sender, EventArgs e)
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
                    addTool_Click(null, null);
                    break;
                case Keys.F2:
                    delTool_Click(null, null);
                    break;
                case Keys.F3:
                    editTool_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgv);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgv, "服务项目管理", false, "");
                    break;
                default:
                    break;
            }
        }
    }
}
