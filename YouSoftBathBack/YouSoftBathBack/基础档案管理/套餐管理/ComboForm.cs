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
    public partial class ComboForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Combo m_Combo = new Combo();
        private bool newCombo = true;
        private List<int> menuIdList;
        private List<int> freeIdList;

        //构造函数
        public ComboForm(BathDBDataContext dc, Combo combo)
        {
            db = dc;
            if (combo != null)
            {
                newCombo = false;
                m_Combo = combo;
            }

            InitializeComponent();
        }

        //对话框载入
        private void ComboForm_Load(object sender, EventArgs e)
        {
            catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            cboxCat3.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            priceType.SelectedIndex = 0;
            menuIdList = BathClass.disAssemble(m_Combo.menuIds);
            freeIdList = BathClass.disAssemble(m_Combo.freeMenuIds);
            dgv_show();
            dgvMenu_show();
            dgvFreeItems_show();

            dgvExcept_show();
            dgvItems3_show();
            dgvFreeItems3_show();
            if (!newCombo)
            {
                priceType.Text = m_Combo.priceType;
                if (m_Combo.priceType == "免项目")
                {
                    price.Enabled = false;
                }
                else if (m_Combo.priceType == "减金额")
                {
                    price.Text = m_Combo.price.ToString();
                }
                else if (m_Combo.priceType == "消费满免项目")
                {
                    upTo.Text = m_Combo.expenseUpTo.ToString();
                }
                
                this.Text = "编辑套餐";
            }
        }

        //显示套餐
        private void dgv_show()
        {
            dgvItems.Rows.Clear();
            var ms = db.Menu.Where(x => menuIdList.Contains(x.id));
            foreach (var m in ms)
            {
                dgvItems.Rows.Add(m.name, m.price);
            }
        }

        private void dgvMenu_show()
        {
            dgvMenu.Rows.Clear();
            var ms = db.Menu.Where(x => !menuIdList.Contains(x.id));
            if (catgory.Text != "")
            {
                var c = db.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
                ms = ms.Where(x => x.catgoryId == c);
            }
            foreach (var m in ms)
            {
                dgvMenu.Rows.Add(m.name, m.price);
            }
        }

        private void dgvItems3_show()
        {
            dgvItems3.Rows.Clear();
            var ms = db.Menu.Where(x => !freeIdList.Contains(x.id) && !menuIdList.Contains(x.id));
            if (cboxCat3.Text != "")
            {
                var c = db.Catgory.FirstOrDefault(x => x.name == cboxCat3.Text).id;
                ms = ms.Where(x => x.catgoryId == c);
            }
            foreach (var m in ms)
            {
                dgvItems3.Rows.Add(m.name, m.price);
            }
        }

        private void dgvFreeItems_show()
        {
            dgvFreeItems.Rows.Clear();
            var ms = db.Menu.Where(x => freeIdList.Contains(x.id));
            foreach (var m in ms)
            {
                dgvFreeItems.Rows.Add(m.name, m.price);
            }
        }

        private void dgvFreeItems3_show()
        {
            dgvFreeItems3.Rows.Clear();
            var ms = db.Menu.Where(x => freeIdList.Contains(x.id));
            foreach (var m in ms)
            {
                dgvFreeItems3.Rows.Add(m.name, m.price);
            }
        }

        //排除列表显示
        private void dgvExcept_show()
        {
            dgvExcept.Rows.Clear();
            var ms = db.Menu.Where(x => menuIdList.Contains(x.id));
            foreach (var m in ms)
            {
                dgvExcept.Rows.Add(m.name, m.price);
            }
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (priceType.SelectedIndex == 1 && price.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入价格");
                return;
            }

            if (priceType.SelectedIndex == 0 && dgvFreeItems.Rows.Count == 0)
            {
                GeneralClass.printErrorMsg("需要输入减免项目");
                return;
            }

            if (priceType.SelectedIndex == 2 && upTo.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入价格");
                return;
            }

            if (priceType.SelectedIndex == 2 && dgvFreeItems3.Rows.Count == 0)
            {
                GeneralClass.printErrorMsg("需要输入减免项目");
                return;
            }

            if (priceType.SelectedIndex == 0 || priceType.SelectedIndex == 1)
            {
                m_Combo.menuIds = ComboManagementForm.assembleCombo(menuIdList);
                m_Combo.originPrice = ComboManagementForm.getComboOriginPrice(db, menuIdList);
            }

            if (priceType.SelectedIndex == 0)
            {
                m_Combo.priceType = "免项目";
                m_Combo.freeMenuIds = ComboManagementForm.assembleCombo(freeIdList);
                m_Combo.price = m_Combo.originPrice - BathClass.get_combo_price(db, m_Combo);
            }
            else if (priceType.SelectedIndex == 1)
            {
                m_Combo.priceType = "减金额";
                m_Combo.freeMenuIds = null;
                m_Combo.price = Convert.ToDouble(price.Text);
            }
            else if (priceType.SelectedIndex == 2)
            {
                m_Combo.priceType = "消费满免项目";
                m_Combo.menuIds = ComboManagementForm.assembleCombo(menuIdList);
                m_Combo.freeMenuIds = ComboManagementForm.assembleCombo(freeIdList);
                m_Combo.expenseUpTo = Convert.ToDouble(upTo.Text);
            }
            m_Combo.freePrice = BathClass.get_combo_price(db, m_Combo);


            if (newCombo)
                db.Combo.InsertOnSubmit(m_Combo);
            db.SubmitChanges();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //取消
        private void canBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        //根据首拼或者编号寻找菜式
        private void searchMenu_TextChanged(object sender, EventArgs e)
        {
            dgv_show();
            if (searchMenu.Text == "")
            {
                dgv_show();
                return;
            }

            for (int i = 0; i < dgvMenu.Rows.Count; i++)
            {
                DataGridViewRow dgvRow = dgvMenu.Rows[i];
                string menuId = dgvRow.Cells[0].Value.ToString();
                string menuName = dgvRow.Cells[1].Value.ToString();
                if (menuId.IndexOf(searchMenu.Text) != 0 &&
                    GetStringSpell.GetChineseSpell(menuName).IndexOf(searchMenu.Text.ToUpper()) != 0)
                {
                    dgvMenu.Rows.Remove(dgvRow);
                    i--;
                }
            }
        }

        //找到之后回车添加第一项
        private void searchMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvMenu.Rows.Count == 0)
                    return;

                var menuId = db.Menu.FirstOrDefault(x => x.name == dgvItems.CurrentRow.Cells[0].Value.ToString()).id.ToString();
                m_Combo.menuIds += ";" + menuId;
                dgv_show();
                searchMenu.SelectAll();
                searchMenu.Focus();
            }
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(price, e);
        }

        private void catgory_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMenu_show();
        }

        //套餐形式发生变化
        private void priceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (priceType.SelectedIndex == 0)
            {
                groupBox3.Visible = true;
                groupBox3.Dock = DockStyle.Fill;
                groupBox4.Visible = false;

                price.Enabled = false;
                freeBox.Visible = true;
                btnFreeAdd.Enabled = true;
                btnFreeDel.Enabled = true;
            }
            else if (priceType.SelectedIndex == 1)
            {
                groupBox3.Visible = true;
                groupBox3.Dock = DockStyle.Fill;
                groupBox4.Visible = false;

                price.Enabled = true;
                freeBox.Visible = false;
                btnFreeAdd.Enabled = false;
                btnFreeDel.Enabled = false;
            }
            else if (priceType.SelectedIndex == 2)
            {
                btnFreeAdd.Enabled = true;
                btnFreeDel.Enabled = true;

                groupBox3.Visible = false;
                groupBox4.Visible = true;
                groupBox4.Dock = DockStyle.Fill;
            }
        }

        //增加
        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            if (dgvMenu.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("未选择行");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvMenu.CurrentRow.Cells[0].Value.ToString()).id;
            menuIdList.Add(menuId);
            dgv_show();
            dgvMenu_show();
            searchMenu.SelectAll();
            searchMenu.Focus();
        }

        //删除
        private void btnItemDel_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvItems.CurrentRow.Cells[0].Value.ToString()).id;
            menuIdList.Remove(menuId);
            dgv_show();
            dgvMenu_show();
            dgvFreeItems_show();
        }

        private void btnFreeAdd_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("未选择行");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvItems.CurrentRow.Cells[0].Value.ToString()).id;
            freeIdList.Add(menuId);
            dgvFreeItems_show();
        }

        private void btnFreeDel_Click(object sender, EventArgs e)
        {
            if (dgvFreeItems.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvFreeItems.CurrentRow.Cells[0].Value.ToString()).id;
            freeIdList.Remove(menuId);
            dgv_show();
            dgvMenu_show();
            dgvFreeItems_show();
        }

        private void price_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void btnRight3_Click(object sender, EventArgs e)
        {
            if (dgvItems3.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("未选择行");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvItems3.CurrentRow.Cells[0].Value.ToString()).id;
            freeIdList.Add(menuId);
            dgvItems3_show();
            dgvFreeItems3_show();
        }

        private void benLeft3_Click(object sender, EventArgs e)
        {
            if (dgvFreeItems3.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("没有选择行!");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvFreeItems3.CurrentRow.Cells[0].Value.ToString()).id;
            freeIdList.Remove(menuId);
            dgvItems3_show();
            dgvFreeItems3_show();
        }

        private void cboxCat3_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvItems3_show();
        }

        //删除排除项目
        private void btnExcepDel_Click(object sender, EventArgs e)
        {
            if (dgvItems3.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("未选择行");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvItems3.CurrentRow.Cells[0].Value.ToString()).id;
            menuIdList.Remove(menuId);
            dgvExcept_show();
            dgvItems3_show();
            dgvFreeItems3_show();
        }

        //增加排除项目
        private void btnExcepAdd_Click(object sender, EventArgs e)
        {
            if (dgvItems3.CurrentCell == null)
            {
                GeneralClass.printErrorMsg("未选择行");
                return;
            }

            var menuId = db.Menu.FirstOrDefault(x => x.name == dgvItems3.CurrentRow.Cells[0].Value.ToString()).id;
            menuIdList.Add(menuId);
            dgvExcept_show();
            dgvItems3_show();
            dgvFreeItems3_show();
        }

    }
}
