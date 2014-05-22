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
    public partial class MenuForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private YouSoftBathGeneralClass.Menu m_menu = new YouSoftBathGeneralClass.Menu();
        private bool newMenu = true;
        private string m_cat;
        private MenuManagementForm m_Form;
        private Dictionary<string, double> resourceExpense = new Dictionary<string, double>();

        //构造函数
        public MenuForm(BathDBDataContext dc, YouSoftBathGeneralClass.Menu menu, string cat, MenuManagementForm form)
        {
            db = dc;
            m_cat = cat;
            m_Form = form;
            if (menu != null)
            {
                newMenu = false;
                m_menu = menu;
            }

            InitializeComponent();
            catgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            catgory.Text = cat;
            techRatioType.SelectedIndex = 0;
            timeLimitType.SelectedIndex = 0;
        }
        
        //对话框载入
        private void MenuForm_Load(object sender, EventArgs e)
        {
            techRatioCat.SelectedIndex = 0;
            unit.Items.AddRange(db.Unit.Select(x => x.name).ToArray());

            if (!newMenu)
            {
                name.Text = m_menu.name;
                unit.Text = m_menu.unit;
                catgory.Text = db.Catgory.FirstOrDefault(x => x.id == m_menu.catgoryId).name;
                technician.Checked = m_menu.technician;
                waiter.Checked = MConvert<bool>.ToTypeOrDefault(m_menu.waiter, false);
                price.Text = m_menu.price.ToString();
                note.Text = m_menu.note;
                timeLimitHour.Text = m_menu.timeLimitHour.ToString();
                timeLimitMiniute.Text = m_menu.timeLimitMiniute.ToString();
                timeLimitType.Text = m_menu.timeLimitType;

                if (Convert.ToBoolean(m_menu.addAutomatic))
                {
                    addAutomatic.Checked = true;
                    addType.Text = m_menu.addType;
                    if (addType.Text == "按时间")
                        addMoney.Text = m_menu.addMoney.ToString();
                }
                if (technician.Checked)
                {
                    if (m_menu.techRatioCat != null)
                        techRatioCat.Text = m_menu.techRatioCat;
                    if (m_menu.techRatioType != null)
                        techRatioType.Text = m_menu.techRatioType;
                    if (m_menu.onRatio != null)
                        onRatio.Text = m_menu.onRatio.ToString();
                    if (orderRatio.Text != null)
                        orderRatio.Text = m_menu.orderRatio.ToString();
                }

                waiterRatio.Enabled = waiter.Checked;
                waiterRatioType.Enabled = waiter.Checked;
                if (waiter.Checked)
                {
                    waiterRatioType.SelectedIndex = MConvert<int>.ToTypeOrDefault(m_menu.waiterRatioType, 0);
                    waiterRatio.Text = m_menu.waiterRatio.ToString();
                }
                dgv_show();
            }
            timeLimitHour.Enabled = addAutomatic.Checked;
            timeLimitMiniute.Enabled = addAutomatic.Checked;
            techRatioType.Enabled = (techRatioCat.Text == "按比例");
        }

        private void dgv_show()
        {
            resourceExpense = BathClass.disAssemble_Menu_resourceExpense(m_menu.ResourceExpense);
            foreach (KeyValuePair<string, double> a in resourceExpense)
            {
                dgv.Rows.Add(a.Key, a.Value);
            }
        }

        //确定
        private void okBtn_Click(object sender, EventArgs e)
        {
            if (catgory.Text == "")
            {
                GeneralClass.printErrorMsg("需要选择类别!");
                return;
            }

            if (name.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入名称!");
                return;
            }
            if (unit.Text == "")
            {
                GeneralClass.printErrorMsg("需要输入单位!");
                return;
            }

            m_menu.name = name.Text;
            m_menu.unit = unit.Text;
            m_menu.catgoryId = db.Catgory.FirstOrDefault(x => x.name == catgory.Text).id;
            m_menu.note = note.Text;
            m_menu.addAutomatic = addAutomatic.Checked;
            m_menu.waiter = waiter.Checked;
            m_menu.technician = technician.Checked;
            if (technician.Checked)
            {
                m_menu.techRatioType = techRatioType.Text;
                if (onRatio.Text == "" || orderRatio.Text == "")
                {
                    GeneralClass.printErrorMsg("需要输入提成比例!");
                    return;
                }
                m_menu.techRatioCat = techRatioCat.Text;
                m_menu.onRatio = Convert.ToDouble(onRatio.Text);
                m_menu.orderRatio = Convert.ToDouble(orderRatio.Text);
            }

            if (waiter.Checked)
            {
                if (waiterRatio.Text == "")
                {
                    GeneralClass.printErrorMsg("需要输入提成比例!");
                    return;
                }
                m_menu.waiterRatioType = waiterRatioType.SelectedIndex;
                m_menu.waiterRatio = Convert.ToDouble(waiterRatio.Text);
            }

            if (price.Text == "")
            {
                price.Focus();
                GeneralClass.printErrorMsg("需要输入价格!");
                return;
            }
            m_menu.price = Convert.ToDouble(price.Text);

            //if (timeLimitHour.Text == "" || timeLimitMiniute.Text == "")
            //{
            //    GeneralClass.printErrorMsg("需要输入数据!");
            //    return;
            //}

            if (addAutomatic.Checked)
            {
                if (addType.Text == "")
                {
                    GeneralClass.printErrorMsg("需要选择添加类型!");
                    return;
                }
                m_menu.timeLimitType = timeLimitType.Text;
                m_menu.addType = addType.Text;
                m_menu.timeLimitHour = Convert.ToInt32(timeLimitHour.Text);
                m_menu.timeLimitMiniute = Convert.ToInt32(timeLimitMiniute.Text);

                if (addType.SelectedIndex == 1)
                {
                    if (addMoney.Text == "")
                    {
                        GeneralClass.printErrorMsg("需要输入价格!");
                        return;
                    }
                    m_menu.addMoney = Convert.ToDouble(addMoney.Text);
                }
            }

            if (dgv.Rows.Count != 0)
            {
                m_menu.ResourceExpense = BathClass.assemble_Menu_resourceExpense(resourceExpense);
            }

            if (db.Unit.FirstOrDefault(x=>x.name==unit.Text)==null)
            {
                Unit newUnit = new Unit();
                newUnit.name = unit.Text;
                db.Unit.InsertOnSubmit(newUnit);
            }

            if (newMenu)
            {
                db.Menu.InsertOnSubmit(m_menu);
                db.SubmitChanges();
                m_Form.dgv_show();

                name.Text = "";
                catgory.Text = m_cat;
                price.Text = "";
                
                technician.Checked = false;
                techRatioType.SelectedIndex = 0;
                onRatio.Text = "";
                orderRatio.Text = "";

                timeLimitHour.Text = "0";
                timeLimitMiniute.Text = "0";
                addAutomatic.Checked = false;
                addType.Text = "";
                addType.Enabled = false;
                addMoney.Text = "10";
                addMoney.Enabled = false;
                note.Text = "";
                m_menu = new YouSoftBathGeneralClass.Menu();
                name.Focus();

                dgv.Rows.Clear();
                return;
            }
            else
            {
                db.SubmitChanges();
                this.DialogResult = DialogResult.OK;
            }
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.Enter:
                    okBtn_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        //设置自动加收
        private void addAutomatic_CheckedChanged(object sender, EventArgs e)
        {
            addType.Enabled = addAutomatic.Checked;
            timeLimitType.Enabled = addAutomatic.Checked;
            addMoney.Enabled = addType.SelectedIndex == 1 && addAutomatic.Checked;

            timeLimitHour.Enabled = addAutomatic.Checked;
            timeLimitMiniute.Enabled = addAutomatic.Checked;
        }
        
        //设置加收方式
        private void addType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (addType.SelectedIndex == 1 && addAutomatic.Checked)
                addMoney.Enabled = true;
            else
                addMoney.Enabled = false;
        }

        private void price_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void orderRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(orderRatio, e);
        }

        private void onRatio_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(onRatio, e);
        }

        //是否需要技师
        private void technician_CheckedChanged(object sender, EventArgs e)
        {
            techRatioType.Enabled = technician.Checked;
            onRatio.Enabled = technician.Checked;
            orderRatio.Enabled = technician.Checked;
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void price_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void waiter_CheckedChanged(object sender, EventArgs e)
        {
            waiterRatio.Enabled = waiter.Checked;
            waiterRatioType.Enabled = waiter.Checked;
        }

        private void waiterRatioType_SelectedIndexChanged(object sender, EventArgs e)
        {
            label20.Visible = (waiterRatioType.SelectedIndex == 0);
            if (waiterRatioType.SelectedIndex == 0)
                label21.Text = "提成比例";
            else if (waiterRatioType.SelectedIndex == 1)
                label21.Text = "提成价格";
        }

        //提成类别改变
        private void techRatioCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool by_money = (techRatioCat.Text == "按金额");
            label16.Visible = !by_money;
            label17.Visible = !by_money;
            techRatioType.Enabled = !by_money;

            if (by_money)
            {
                label14.Text = "上钟提成金额";
                label15.Text = "点钟提成金额";
            }
            else
            {
                label14.Text = "上钟提成比例";
                label15.Text = "点钟提成比例";
            }
        }

        //添加消耗品
        private void re_add_Click(object sender, EventArgs e)
        {
            if (re_name.Text.Trim() == "")
            {
                re_name.Focus();
                BathClass.printErrorMsg("需要输入消耗品名称");
                return;
            }

            if (re_number.Text.Trim() == "")
            {
                re_number.Focus();
                BathClass.printErrorMsg("需要输入消耗品数量");
                return;
            }

            resourceExpense.Add(re_name.Text, Convert.ToDouble(re_number.Text));
            dgv.Rows.Add(re_name.Text, re_number.Text);
            re_number.Text = "";
            re_name.Text = "";
            re_name.Focus();
        }

        //删除消耗品
        private void re_del_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择行!");
                return;
            }

            resourceExpense.Remove(dgv.CurrentRow.Cells[0].Value.ToString());
            dgv.Rows.RemoveAt(dgv.CurrentRow.Index);
        }

        private void re_number_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_float(re_number, e);
        }
    }
}
