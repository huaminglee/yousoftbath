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

namespace YouSoftBathFormClass
{
    //输入手牌号
    public partial class InputSeatForm : Form
    {
        //成员变量
        //private BathDBDataContext db = null;
        private CSeat _m_Seat;
        private List<int> m_StatusList; //所需要的台位的状态
        private int m_Status = -1;
        private DAO dao;

        public CSeat m_Seat
        {
            get { return _m_Seat; }
            set { _m_Seat = value; }
        }


        //构造函数
        public InputSeatForm(int status)
        {
            m_Status = status;
            InitializeComponent();
        }

        //重载构造函数
        public InputSeatForm(List<int> ss)
        {
            m_StatusList = ss;

            InitializeComponent();
        }

        //对话框载入
        private void InputSeatForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
        }

        //确定
        private void BtnOK_Click(object sender, EventArgs e)
        {
            BtnOK.Enabled = false;
            BtnOK.Enabled = true;

            m_Seat = dao.get_seat("text='" + text.Text + "' or oId='" + text.Text + "'");
            //m_Seat = db.Seat.FirstOrDefault(x => x.text == text.Text || x.oId == text.Text);
            if (m_Seat == null)
            {
                BathClass.printErrorMsg("所选择台位不存在");
                return;
            }

            if (m_Status != -1)
            {
                if (m_Status != (int)m_Seat.status)
                {
                    BathClass.printErrorMsg("所选择台位不可用");
                    return;
                }
            }
            else if (!m_StatusList.Contains((int)m_Seat.status))
            {
                BathClass.printErrorMsg("所选择台位不可用");
                return;
            }


            this.DialogResult = DialogResult.OK;
        }

        //删除
        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (!text.ContainsFocus)
            {
                BtnBack.Enabled = false;
                BtnBack.Enabled = true;

                if (text.Text == "")
                    return;

                text.Text = text.Text.Substring(0, text.Text.Length - 1);
                text.SelectionStart = text.Text.Length;
            }
        }

        //输入内容
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.Enabled = false;
            btn.Enabled = true;

            text.Text += btn.Text;
            text.SelectionStart = text.Text.Length;
        }

        private void InputSeatForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                BtnOK_Click(null, null);
            else if (e.KeyCode == Keys.Back)
                BtnBack_Click(null, null);
            else if (e.KeyCode == Keys.Escape)
                this.DialogResult = DialogResult.Cancel;
        }

        private void text_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }
    }
}
