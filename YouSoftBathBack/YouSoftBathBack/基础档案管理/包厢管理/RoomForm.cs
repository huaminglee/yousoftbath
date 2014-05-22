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
    public partial class RoomForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Room m_Room = new Room();
        private bool newRoom = true;
        private RoomManagementForm m_rmForm = null;

        //构造函数
        public RoomForm(BathDBDataContext dc, Room room, RoomManagementForm rm)
        {
            db = dc;
            m_rmForm = rm;
            if (room != null)
            {
                newRoom = false;
                m_Room = room;
            }
            InitializeComponent();
        }

        //对话框载入
        private void RoomForm_Load(object sender, EventArgs e)
        {
            if (!newRoom)
            {
                name.Text = m_Room.name;
                population.Text = m_Room.population.ToString();
                note.Text = m_Room.note;
            }
        }

        //点击确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (name.Text == "")
            {
                name.Focus();
                GeneralClass.printErrorMsg("请输入房号");
                return;
            }
            m_Room.name = name.Text;
            m_Room.population = Convert.ToInt32(population.Text);
            m_Room.note = note.Text;

            if (newRoom)
            {
                if (db.Room.FirstOrDefault(x => x.name == name.Text) != null)
                {
                    name.SelectAll();
                    name.Focus();
                    GeneralClass.printErrorMsg("已存在" + name.Text + "房间，不能重复添加");
                    return;
                }
                m_Room.status = "空闲";
                db.Room.InsertOnSubmit(m_Room);
                db.SubmitChanges();
                m_rmForm.dgv_show();

                m_Room = new Room();
                name.Text = "";
                population.Text = "1";
                note.Text = "";
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
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void note_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }
    }
}
