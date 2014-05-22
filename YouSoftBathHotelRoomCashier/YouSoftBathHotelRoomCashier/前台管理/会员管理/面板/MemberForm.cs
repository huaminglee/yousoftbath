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

namespace YouSoftBathReception
{
    public partial class MemberForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private HotelRoom m_Seat = null;

        //构造函数
        public MemberForm(HotelRoom seat)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            m_Seat = seat;
            InitializeComponent();
        }

        //构造函数
        public MemberForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void MemberForm_Load(object sender, EventArgs e)
        {
            if (m_Seat == null)
            {
                //btnSale.Enabled = false;//前台售卡
                btnPop.Enabled = false;
            }
        }

        //售卡
        private void btnSale_Click(object sender, EventArgs e)
        {
            if (!BathClass.getAuthority(db, LogIn.m_User, "售卡"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            OpenCardForm openCardForm = new OpenCardForm(m_Seat);
            if (openCardForm.ShowDialog() == DialogResult.OK)
                this.Close();
        }

        //充值
        private void btnPop_Click(object sender, EventArgs e)
        {
            if (!BathClass.getAuthority(db, LogIn.m_User, "充值"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            MemberPopForm memberPopForm = new MemberPopForm(m_Seat);
            memberPopForm.ShowDialog();
        }

        //挂失
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!BathClass.getAuthority(db, LogIn.m_User, "挂失"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            MemberStopForm memberStopForm = new MemberStopForm();
            memberStopForm.ShowDialog();
        }

        //补卡
        private void btnResume_Click(object sender, EventArgs e)
        {
            if (!BathClass.getAuthority(db, LogIn.m_User, "补卡"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }

            MemberResumForm memberResumForm = new MemberResumForm();
            memberResumForm.ShowDialog();
        }

        //读卡
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!BathClass.getAuthority(db, LogIn.m_User, "读卡"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            MemberShowForm memberReadForm = new MemberShowForm();
            memberReadForm.ShowDialog();
        }

        //绑定快捷键
        private void MemberForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.F1:
                    btnSale_Click(null, null);
                    break;
                case Keys.F2:
                    btnPop_Click(null, null);
                    break;
                case Keys.F3:
                    btnStop_Click(null, null);
                    break;
                case Keys.F4:
                    btnResume_Click(null, null);
                    break;
                case Keys.F5:
                    btnRead_Click(null, null);
                    break;
                case Keys.Escape:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                default:
                    break;
            }
        }

        //设置
        private void btnSet_Click(object sender, EventArgs e)
        {
            CardPortBaudForm cardPortBaudForm = new CardPortBaudForm();
            if (cardPortBaudForm.ShowDialog() != DialogResult.OK)
                return;

            string card_port = cardPortBaudForm.card_port.ToString();
            string card_baud = cardPortBaudForm.card_baud.ToString();

            Int16 port = Convert.ToInt16(card_port);
            int baud = Convert.ToInt32(card_baud);

            int icdev = IC.ic_init(port, baud);
            if (icdev < 0)
            {
                BathClass.printErrorMsg("设置读卡机失败，请重试!");
            }
            else
            {
                BathClass.printInformation("读卡机设置成功!");
                int st = IC.ic_exit(icdev);
            }
        }
    }
}
