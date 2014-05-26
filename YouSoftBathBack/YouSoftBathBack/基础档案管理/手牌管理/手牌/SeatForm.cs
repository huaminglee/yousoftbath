using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using YouSoftUtil;

namespace YouSoftBathBack
{
    public partial class SeatForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        private Seat m_Seat = new Seat();
        private bool newSeat = true;
        private SeatManagementForm m_mForm = null;
        private string lock_type;
        private Thread m_td;
        private bool _close = false;

        //构造函数
        public SeatForm(BathDBDataContext dc, Seat seat, SeatManagementForm smf)
        {
            db = dc;
            m_mForm = smf;
            if (seat != null)
            {
                newSeat = false;
                m_Seat = seat;
            }

            InitializeComponent();
        }

        //对话框载入
        private void SeatForm_Load(object sender, EventArgs e)
        {
            typeName.Items.AddRange(db.SeatType.Select(x => x.name).ToArray());
            if (typeName.Items.Count != 0)
                typeName.SelectedIndex = 0;
            if (!newSeat)
            {
                id.Text = m_Seat.text;
                //if (m_Seat.name != null)
                name.Text = m_Seat.name;
                typeName.Text = db.SeatType.FirstOrDefault(x => x.id == m_Seat.typeId).name;
                oId.Text = m_Seat.oId;
                note.Text = m_Seat.note;
            }

            lock_type = LogIn.options.手牌锁类型;
            m_td = new Thread(new ThreadStart(seat_card_thread));
            if (MConvert<bool>.ToTypeOrDefault(LogIn.options.启用手牌锁, false))
                m_td.Start();
        }

        //手牌线程
        private void seat_card_thread()
        {
            while (true)
            {
                try
                {
                    if (_close) break;
                    if (lock_type == "欧亿达")
                    {
                        Thread.Sleep(500);
                        if (OYD.FKOPEN() != 1)
                            continue;

                        OYD.CH375SetTimeout(0, 5000, 5000);
                    }

                    string seat_text = "";
                    byte[] buff = new byte[200];

                    int rt = -1;
                    if (lock_type == "欧亿达")
                    {
                        Thread.Sleep(500);
                        rt = OYD.OYEDA_id(buff);
                    }
                    else if (lock_type == "锦衣卫")
                        rt = JYW.ReadID(buff);
                    else if (lock_type == "RF")
                        rt = RF.RF_RFID(ref seat_text);

                    if (rt != 0)
                        continue;

                    if (lock_type == "欧亿达")
                    {
                        seat_text = Encoding.Default.GetString(buff, 0, 20).Trim();
                        oId.Text = seat_text.Substring(0, 16);
                    }
                    else if (lock_type == "锦衣卫")
                    {
                        seat_text = BathClass.byteToHexStr(buff);
                        oId.Text = seat_text.Substring(0, 16);
                    }
                    else if (lock_type == "RF")
                    {
                        oId.Text = seat_text;
                    }

                }
                catch
                {
                    //this.Invoke(new delegate_print_msg(BathClass.printErrorMsg), new object[]{ex.Message});
                }
            }
        }

        private delegate void delegate_print_msg(string msg);

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (newSeat && db.Seat.FirstOrDefault(x => x.text == id.Text) != null)
            {
                id.SelectAll();
                id.Focus();
                GeneralClass.printErrorMsg("该台位已经存在!");
                return;
            }

            m_Seat.text = id.Text;
            m_Seat.name = name.Text;
            m_Seat.typeId = db.SeatType.FirstOrDefault(x => x.name == typeName.Text).id;
            m_Seat.oId = oId.Text;
            m_Seat.note = note.Text;

            if (newSeat)
            {
                m_Seat.status = 1;
                db.Seat.InsertOnSubmit(m_Seat);

                db.SubmitChanges();

                id.Text = "";
                name.Text = "";
                oId.Text = "";
                note.Text = "";
                m_mForm.dgv_show();
                m_Seat = new Seat();
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
                    btnOk_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void id_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        private void name_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_ch();
        }

        private void SeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _close = true;
        }

    }
}
