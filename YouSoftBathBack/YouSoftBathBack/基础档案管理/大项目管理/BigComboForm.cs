using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathConstants;

namespace YouSoftBathBack
{
    public partial class BigComboForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private BigCombo m_BigCombo = new BigCombo();
        private bool newBigCombo = true;
        private List<int> BigComboIDList=new List<int>();
        private List<int> SubstIDList=new List<int>();

        //构造函数
        public BigComboForm(BathDBDataContext dc, BigCombo bigcombo)
        {
            db = dc;
            if (bigcombo != null)
            {
                newBigCombo = false;
                m_BigCombo = bigcombo;
            }

            InitializeComponent();
        }

        //对话框载入
        private void ComboForm_Load(object sender, EventArgs e)
        {
            var catArray = db.Catgory.Select(x => x.name).ToArray();
            TextMenuCat.Items.AddRange(catArray);
            TextBigMenuName.Items.AddRange(db.Menu.Select(x=>x.name).ToArray());
            cboxCat.Items.AddRange(catArray);
            if (!newBigCombo)
            {
                var bigMenu = db.Menu.FirstOrDefault(x => x.id == m_BigCombo.menuid);
                TextBigMenuName.Text = bigMenu.name;
                TextMenuCat.Text = db.Catgory.FirstOrDefault(x => x.id == bigMenu.catgoryId).name;
                TextBigMenuName.Enabled = false;
                txtBoxPrice.Text = bigMenu.price.ToString();
                txtBoxPrice.Enabled = false;
                SubstIDList = BathClass.disAssemble(m_BigCombo.substmenuid, Constants.SplitChar);
            }
            dgvMenus_show();
            dgvsubstItems_show();
        }

        //dgvMenus显示所有项目
        private void dgvMenus_show()
        {
            dgvMenus.Rows.Clear();
            int catID=0;
            string goodsCatName=cboxCat.Text;
            var menus = db.Menu.Where(x=>!BigComboIDList.Contains(x.id)).AsQueryable();
            if (goodsCatName != "")
                catID = db.Catgory.FirstOrDefault(x => x.name == goodsCatName).id;
            if (catID!=0)
                menus = menus.Where(x => x.catgoryId == catID);
            foreach (var m in menus)
            {
                dgvMenus.Rows.Add(m.name, m.price);

            }
            
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (TextBigMenuName.Text == "")
            {
                BathClass.printErrorMsg("请选择一个要替换的项目!");
                return;
            }
            if (SubstIDList.Count==0)
            {
                BathClass.printErrorMsg("请选择要替换的项目!");
                return;
            }
            if (newBigCombo)
            {
                string substIDs="";
                var menu = db.Menu.FirstOrDefault(x => x.name == TextBigMenuName.Text);
                if (db.BigCombo.Where(x => x.menuid == menu.id).Any())
                {
                    MessageBox.Show("该项目已存在，不能继续添加!");
                    return;

                }
                
                m_BigCombo.menuid = menu.id;
                m_BigCombo.price = menu.price;


                foreach (var s in SubstIDList)
                {
                    substIDs += s;
                    substIDs += Constants.SplitChar;
                }

                m_BigCombo.substmenuid = substIDs.Remove(substIDs.Length-1);
                db.BigCombo.InsertOnSubmit(m_BigCombo);
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {

                string substIDs = "";

                foreach (var s in SubstIDList)
                {
                    substIDs += s;
                    substIDs += Constants.SplitChar;
                }
                m_BigCombo.substmenuid = substIDs.Remove(substIDs.Length - 1);
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();

            }

        }

        //选择替换项目组合时候，类别名称发生改变
        private void cboxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMenus_show();
        }

        private void btnRight3_Click(object sender, EventArgs e)
        {
            if (dgvMenus.CurrentCell==null)
            {
                BathClass.printErrorMsg("请选择一行!");
                return;
            }
            var id = db.Menu.FirstOrDefault(x => x.name == dgvMenus.CurrentRow.Cells[0].Value.ToString()).id;
            if (!BigComboIDList.Contains(id))
                BigComboIDList.Add(id);
            if (!SubstIDList.Contains(id))
                SubstIDList.Add(id);
            dgvMenus_show();
            dgvsubstItems_show();

            
        }

        private void dgvsubstItems_show()
        {
            dgvsubstItems.Rows.Clear();
            if (SubstIDList.Count == 0)
                return;
            foreach (var substid in SubstIDList)
            {
                var substmenu = db.Menu.FirstOrDefault(x => x.id == substid);
                dgvsubstItems.Rows.Add(substmenu.name,substmenu.price);

            }

        }

        private void cboxMenuName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var menuname = TextBigMenuName.Text;
            BigComboIDList.Add(db.Menu.FirstOrDefault(x => x.name == menuname).id);
            txtBoxPrice.Text = db.Menu.FirstOrDefault(x => x.name == menuname).price.ToString();
            dgvMenus_show();
            dgvsubstItems_show();

        }

        private void benLeft3_Click(object sender, EventArgs e)
        {
            if (dgvsubstItems.CurrentCell == null)
            {
                BathClass.printErrorMsg("请选择一行!");
                return;
            }
            var id = db.Menu.FirstOrDefault(x => x.name == dgvsubstItems.CurrentRow.Cells[0].Value.ToString()).id;
            BigComboIDList.Remove(id);
            SubstIDList.Remove(id);
            dgvMenus_show();
            dgvsubstItems_show();
            
        }

        private void TextMenuCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBigMenuName.Items.Clear();
            TextBigMenuName.Items.AddRange(db.Menu.Where(x => x.catgoryId == db.Catgory.FirstOrDefault(y => y.name == TextMenuCat.Text).id).Select(x=>x.name).ToArray());
        }

        private void TextBigMenuName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var bigMenu = db.Menu.FirstOrDefault(x=>x.name==TextBigMenuName.Text);
            if (bigMenu != null)
                txtBoxPrice.Text = bigMenu.price.ToString();
        }

        private void TextMenuCat_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.change_input_ch();
        }

    }
}
