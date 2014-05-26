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
using YouSoftBathConstants;

namespace YouSoftBathBack
{
    public partial class BigComboManagementForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public BigComboManagementForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);

            InitializeComponent();
            dgvCombo_show();
            dgvItems_show(0);
        }

        //对话框载入
        private void ComboManagementForm_Load(object sender, EventArgs e)
        {
            dgvCombo_show();
        }

        //显示套餐清单
        private void dgvCombo_show()
        {
            dgvCombo.DataSource = from x in db.BigCombo
                               orderby x.id
                               select new
                               {
                                   套餐号 = x.id,
                                   名称 = db.Menu.FirstOrDefault(y=>y.id==x.menuid).name,
                                   单价 = x.price,
                                   
                               };
            BathClass.set_dgv_fit(dgvCombo);
        }

        //显示套餐详情清单
        private void dgvItems_show(int rowIndex)
        {
            if (dgvCombo.Rows.Count == 0 || rowIndex == -1)
                return;

            int selId = Convert.ToInt32(dgvCombo.Rows[rowIndex].Cells[0].Value);
            List<int> menuIdList = BathClass.disAssemble(db.BigCombo.FirstOrDefault(x => x.id == selId).substmenuid,Constants.SplitChar);
            dgvItems.DataSource = from x in db.Menu
                                  where menuIdList.Contains(x.id)
                                  orderby x.id
                                  select new
                                  {
                                      编号 = x.id,
                                      名称 = x.name,
                                      单价 = x.price
                                  };
            BathClass.set_dgv_fit(dgvItems);
        }

        public  List<int> disAssemble(string menuIds)
        {
            List<int> menuIdList = new List<int>();

            if (menuIds == null || menuIds == "")
                return menuIdList;

            string[] ids = menuIds.Split(Constants.SplitChar);
            foreach (string menuId in ids)
            {
                if (menuId == "")
                    continue;
                menuIdList.Add(Convert.ToInt32(menuId));
            }

            return menuIdList;
        }

        //新增套餐
        private void addTool_Click(object sender, EventArgs e)
        {
            BigComboForm addCombo = new BigComboForm(db, null);
            if (addCombo.ShowDialog() == DialogResult.OK)
                dgvCombo_show();
        }

        //删除套餐
        private void delTool_Click(object sender, EventArgs e)
        {
            if (dgvCombo.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            if (GeneralClass.printAskMsg("确认删除?") != DialogResult.Yes)
                return;

            int bigcomboID = Convert.ToInt32(dgvCombo.CurrentRow.Cells[0].Value.ToString());
            db.BigCombo.DeleteOnSubmit(db.BigCombo.FirstOrDefault(x => x.id == bigcomboID));
            db.SubmitChanges();
            dgvCombo_show();
        }

        //编辑套餐
        private void editTool_Click(object sender, EventArgs e)
        {
            if (dgvCombo.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            int bigcomboId = MConvert<int>.ToTypeOrDefault(dgvCombo.CurrentRow.Cells[0].Value.ToString(),0);
            BigComboForm addCombo = new BigComboForm(db, db.BigCombo.FirstOrDefault(x => x.id == bigcomboId));
            if (addCombo.ShowDialog() == DialogResult.OK)
                dgvCombo_show();

        }

        //导出清单
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgvCombo);
        }

        //打印清单
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgvCombo, "套餐管理", false, "");
        }

        //获取套餐所对应的菜式
        public static List<int> disAssembleCombo(Combo combo)
        {
            List<int> menuIdList = new List<int>();

            if (combo.menuIds == null)
                return menuIdList;

            string[] menuIds = combo.menuIds.Split(';');
            foreach (string menuId in menuIds)
            {
                if (menuId == "")
                    continue;
                menuIdList.Add(Convert.ToInt32(menuId));
            }

            return menuIdList;
        }

        //根据对应的菜式生成套餐
        public static string assembleCombo(List<int> menuIdList)
        {
            string menuIds = "";
            foreach (int menuId in menuIdList)
                menuIds += ";" + menuId.ToString();
            return menuIds;
        }

        //获取套餐原始价格
        public static double getComboOriginPrice(BathDBDataContext db, List<int> menuIdList)
        {
            double price = 0;
            foreach (int menuId in menuIdList)
            {
                var m = db.Menu.FirstOrDefault(x => x.id == menuId);
                if (m == null)
                    continue;
                price += m.price;
            }

            return price;
        }

        //选中某行套餐
        private void dgvCombo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            dgvItems_show(e.RowIndex);
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
                    addTool_Click(null, null);
                    break;
                case Keys.F2:
                    delTool_Click(null, null);
                    break;
                case Keys.F3:
                    editTool_Click(null, null);
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgvCombo);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgvCombo, "套餐管理", false, "");
                    break;
                default:
                    break;
            }
        }

        private void dgvCombo_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            dgvItems_show(e.RowIndex);
        }

    }
}
