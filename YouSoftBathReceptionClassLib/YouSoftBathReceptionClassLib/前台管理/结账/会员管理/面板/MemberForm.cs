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
        private DAO dao;
        private CSeat m_Seat = null;
        private static CMemberSetting _memberSetting;

        public static CMemberSetting memberSetting
        {
            get { return _memberSetting; }
            set { _memberSetting = value; }
        }

        //构造函数
        public MemberForm(CSeat seat)
        {
            m_Seat = seat;
            InitializeComponent();
        }

        //构造函数
        public MemberForm()
        {
            InitializeComponent();
        }

        //对话框载入
        private void MemberForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(LogIn.connectionString);
            memberSetting = dao.get_MemberSetting();
        }

        //售卡
        private void btnSale_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "售卡"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            //if (!BathClass.getAuthority(db, LogIn.m_User, "售卡"))
            //{
            //    BathClass.printErrorMsg("权限不够!");
            //    return;
            //}

            OpenCardForm openCardForm = new OpenCardForm(m_Seat);
            if (openCardForm.ShowDialog() == DialogResult.OK)
                this.Close();
        }

        //充值
        private void btnPop_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "充值"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            //if (!BathClass.getAuthority(db, LogIn.m_User, "充值"))
            //{
            //    BathClass.printErrorMsg("权限不够!");
            //    return;
            //}

            MemberPopForm memberPopForm = new MemberPopForm(m_Seat);
            memberPopForm.ShowDialog();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //挂失
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "挂失"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            //if (!BathClass.getAuthority(db, LogIn.m_User, "挂失"))
            //{
            //    BathClass.printErrorMsg("权限不够!");
            //    return;
            //}

            MemberStopForm memberStopForm = new MemberStopForm();
            memberStopForm.ShowDialog();
        }

        //补卡
        private void btnResume_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "补卡"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            //if (!BathClass.getAuthority(db, LogIn.m_User, "补卡"))
            //{
            //    BathClass.printErrorMsg("权限不够!");
            //    return;
            //}

            MemberResumForm memberResumForm = new MemberResumForm();
            memberResumForm.ShowDialog();
        }

        //读卡
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (!dao.get_authority(LogIn.m_User, "读卡"))
            {
                BathClass.printErrorMsg("权限不够!");
                return;
            }
            //if (!BathClass.getAuthority(db, LogIn.m_User, "读卡"))
            //{
            //    BathClass.printErrorMsg("权限不够!");
            //    return;
            //}
            MemberShowForm memberReadForm = new MemberShowForm();
            memberReadForm.ShowDialog();
        }

        //绑定快捷键
        private void MemberForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.NumPad0:
                case Keys.Escape:
                    btn_cancel_Click(null, null);
                    break;
                case Keys.NumPad1:
                    btn_activate_Click(null, null);
                    break;
                case Keys.NumPad2:
                    btnSale_Click(null, null);
                    break;
                case Keys.NumPad3:
                    btnPop_Click(null, null);
                    break;
                case Keys.NumPad4:
                    btnStop_Click(null, null);
                    break;
                case Keys.NumPad5:
                    btnResume_Click(null, null);
                    break;
                case Keys.NumPad6:
                    btnRead_Click(null, null);
                    break;
                case Keys.NumPad7:
                    btnSet_Click(null, null);
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

        //激活同时售卡
        private void btn_activate_Click(object sender, EventArgs e)
        {
            string seat_text = null;
            if (m_Seat != null)
                seat_text = m_Seat.text;
            var form = new MemberActivateForm(seat_text);
            form.ShowDialog();
        }

        //取消退出
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
