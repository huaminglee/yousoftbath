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

namespace YouSoftBathBack
{
    public partial class SeatTypeForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private SeatType m_SeatType = new SeatType();
        private bool newSeatType = true;

        //构造函数
        public SeatTypeForm(BathDBDataContext dc, SeatType seatType)
        {
            db = dc;
            if (seatType != null)
            {
                newSeatType = false;
                m_SeatType = seatType;
            }
            InitializeComponent();
        }

        //对话框载入
        private void SeatTypeForm_Load(object sender, EventArgs e)
        {
            menuId.Items.AddRange(db.Menu.Select(x => x.name).ToArray());
            seatDepart.SelectedIndex = 0;
            menuId.SelectedIndex = 0;
            
            if (!newSeatType)
            {
                name.Text = m_SeatType.name;
                population.Text = m_SeatType.population.ToString();
                seatDepart.Text = m_SeatType.department;
                depositMin.Text = m_SeatType.depositeAmountMin.ToString();
                depositRequired.Checked = MConvert<bool>.ToTypeOrDefault(m_SeatType.depositeRequired,false);
                depositMin.Enabled = depositRequired.Checked;

                var menu = db.Menu.FirstOrDefault(x => x.id == m_SeatType.menuId);
                if (menu != null)
                    menuId.Text = menu.name;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            m_SeatType.name = name.Text;
            m_SeatType.population = Convert.ToInt32(population.Text);
            m_SeatType.depositeRequired = depositRequired.Checked;
            m_SeatType.department = seatDepart.Text;

            if (depositRequired.Checked)
            {
                if (depositMin.Text == "")
                {
                    depositMin.Focus();
                    BathClass.printErrorMsg("需要输入最低押金金额");
                    return;
                }
                m_SeatType.depositeAmountMin = Convert.ToInt32(depositMin.Text);
            }

            if (menuId.Text != "")
                m_SeatType.menuId = db.Menu.FirstOrDefault(x => x.name == menuId.Text).id;
            else
                m_SeatType.menuId = null;

            if (newSeatType)
                db.SeatType.InsertOnSubmit(m_SeatType);
            db.SubmitChanges();
            this.DialogResult = DialogResult.OK;
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
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void population_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void population_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void depositRequired_CheckedChanged(object sender, EventArgs e)
        {
            depositMin.Enabled = depositRequired.Checked;
        }
    }
}
