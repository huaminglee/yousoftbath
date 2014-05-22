using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Data.SqlClient;

using System.Timers;
using System.Threading;

namespace YouSoftBathFormClass
{
    public partial class CabPopForm : Form
    {
        private Room m_room;
        private BathDBDataContext dc;

        //构造函数
        public CabPopForm(string roomId)
        {
            dc = new BathDBDataContext(LogIn.connectionString);
            m_room = dc.Room.FirstOrDefault(x => x.id.ToString() == roomId);
            InitializeComponent();
        }

        //对话框载入
        private void RoomManagementForm_Load(object sender, EventArgs e)
        {
            roomId.Text = m_room.name;
            pop.Text = m_room.population.ToString();
            curPop.Text = m_room.seatIds;
        }


        //绑定快捷键
        private void RoomViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
                this.Close();
        }

        //减少
        private void btnDec_Click(object sender, EventArgs e)
        {
            if (m_room.seatIds == null)
            {
                BathClass.printErrorMsg("已经为0!");
                return;
            }
            var ids = m_room.seatIds.Split('|').ToList();
            if (ids.Count == 0)
            {
                BathClass.printErrorMsg("已经为0!");
                return;
            }
            if (ids.Count == 1)
            {
                m_room.seatIds = null;
                dc.SubmitChanges();
                this.Close();
                return;
            }

            List<int> s = new List<int>();
            s.Add(2);
            s.Add(6);
            s.Add(7);
            InputSeatForm form = new InputSeatForm(s);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            var t = form.m_Seat.text;
            if (!ids.Contains(t))
            {
                BathClass.printErrorMsg("手牌：" + t + "不在该房间");
                return;
            }
            ids.Remove(t);
            if (ids.Count == 0)
                m_room.seatIds = null;
            else
                m_room.seatIds = string.Join("|", ids.ToArray());
            curPop.Text = m_room.seatIds;
        }

        //增加
        private void btnInc_Click(object sender, EventArgs e)
        {
            if (m_room.seatIds!=null && m_room.seatIds.Split('|').Length == m_room.population)
            {
                BathClass.printErrorMsg("已经为达到入住上限!");
                return;
            }

            List<int> s = new List<int>();
            s.Add(2);
            s.Add(7);
            InputSeatForm form = new InputSeatForm(s);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            var t = form.m_Seat.text;
            var room = dc.Room.FirstOrDefault(x => x.seatIds != null && x.seatIds.Contains(t));
            if (room != null)
            {
                BathClass.printErrorMsg("该手牌已经入住房间:" + room.name);
                return;
            }

            if (m_room.seatIds == null)
                m_room.seatIds = t;
            else
            {
                var ids = m_room.seatIds.Split('|').ToList();
                ids.Add(t);
                m_room.seatIds = string.Join("|", ids.ToArray());
            }
            curPop.Text = m_room.seatIds;
            //curPop.Text = (Convert.ToInt32(curPop.Text) + 1).ToString();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            //m_room.curPop = Convert.ToInt32(curPop.Text);
            dc.SubmitChanges();
            this.Close();
        }

        private void curPop_KeyPress(object sender, KeyPressEventArgs e)
        {
            BathClass.only_allow_int(e);
        }
    }
}
