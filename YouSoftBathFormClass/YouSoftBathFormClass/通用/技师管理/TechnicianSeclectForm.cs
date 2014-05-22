using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

using System.Timers;
using System.Threading;

namespace YouSoftBathFormClass
{
    public partial class TechnicianSeclectForm : Form
    {
        //成员变量
        private string m_connectionString;
        private Thread m_thread;
        private string m_tech_id = "-1";
        private DAO dao;
        private CEmployee m_tech;

        private static Color off_color = Color.FromArgb(1, 175, 3);//下班颜色
        private static Color avi_color = Color.FromArgb(43, 76, 255);//空闲颜色
        private static Color wait_color = Color.FromArgb(85, 213, 206);//待钟颜色
        private static Color on_color = Color.FromArgb(198, 0, 0);//上钟颜色
        private static Color order_color = Color.FromArgb(255, 115, 39);//点钟颜色
        private static Color plus_color = Color.FromArgb(91, 213, 206);//加钟颜色

        //构造函数
        public TechnicianSeclectForm()
        {
            m_connectionString = LogIn.connectionString;
            InitializeComponent();
        }

        public TechnicianSeclectForm(string connectionString, bool full_screen)
        {
            m_connectionString = connectionString;
            InitializeComponent();
            if (full_screen)
                this.FormBorderStyle = FormBorderStyle.None;
        }

        //对话框载入
        private void TechnicianSeclectForm_Load(object sender, EventArgs e)
        {
            dao = new DAO(m_connectionString);
            var db = new BathDBDataContext(m_connectionString);
            techTypes.Items.Add("所有技师");
            techTypes.Items.AddRange(db.Job.Where(x => x.name.Contains("技师")).Select(x=>x.name).ToArray());
            techTypes.SelectedIndex = 0;
            createTechnicianPanel(db);
            set_status(db);

            toolOff.BackColor = Color.FromArgb(1, 175, 3);
            toolAvi.BackColor = Color.FromArgb(43, 76, 255);
            toolOn.BackColor = Color.FromArgb(198, 0, 0);
            toolOrder.BackColor = Color.FromArgb(255, 115, 39);
            toolOver.BackColor = Color.FromArgb(91, 213, 206);

            m_thread = new Thread(new ThreadStart(update_ui));
            m_thread.Start();

        }

        //刷新线程
        private void update_ui()
        {
            while (true)
            {
                try
                {
                    var db_new = new BathDBDataContext(m_connectionString);
                    var Techs = db_new.Employee.Where(x => db_new.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
                    if (techTypes.Text != "所有技师")
                        Techs = db_new.Employee.Where(x => db_new.Job.FirstOrDefault(y => y.id == x.jobId).name == techTypes.Text);
                    var tech_ids = Techs.Select(x => x.id).ToList();
                    var tech_status = Techs.Select(x => x.techStatus).ToList();

                    for (int i = 0; i < tech_ids.Count; i++)
                    {
                        var tech = db_new.Employee.FirstOrDefault(x => x.id == tech_ids[i]);
                        var btn = (Button)tPanel.Controls.Find(tech_ids[i], false).FirstOrDefault();
                        if (btn == null || tech == null) continue;
                        var ts = tech_status[i];
                        if ((ts == null || ts == "空闲") && btn.BackColor != avi_color)
                        {
                            this.Invoke(new delegate_change_btn_status(change_btn_status),
                                    new object[] { tech, btn, avi_color });
                        }
                        else if (ts == "下班" && btn.BackColor != off_color)
                        {
                            this.Invoke(new delegate_change_btn_status(change_btn_status),
                                    new object[] { tech, btn, off_color });
                        }
                        else if (ts == "点钟" || ts == "上钟" || ts == "加钟")
                        {
                            Color color = off_color;
                            if (ts == "点钟")
                                color = order_color;
                            else if (ts == "上钟")
                                color = on_color;
                            else if (ts == "加钟")
                                color = plus_color;
                            this.Invoke(new delegate_change_btn_status(change_btn_status),
                                    new object[] { tech, btn, color });
                        }
                    }
                    this.Invoke(new delegate_set_status(set_status), new object[] { db_new });
                }
                catch
                {
                }
            }
        }

        private delegate void delegate_set_status(BathDBDataContext dc);
        private delegate void delegate_change_btn_status(Employee tech, Button btn, Color color);
        private void change_btn_status(Employee tech, Button btn, Color color)
        {
            btn.BackColor = color;
            btn.Text = getBtnString(tech);
        }

        private void set_status(BathDBDataContext dc)
        {
            var Techs = dc.Employee.Where(x => dc.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师"));
            if (techTypes.Text != "所有技师")
                Techs = dc.Employee.Where(x => dc.Job.FirstOrDefault(y => y.id == x.jobId).name == techTypes.Text);
            toolTechNumber.Text = Techs.Count().ToString();
            toolOffNumber.Text = Techs.Where(x => x.techStatus == "下班").Count().ToString();
            toolAviNumber.Text = Techs.Where(x => x.techStatus == "空闲" || x.techStatus == null).Count().ToString();
            toolOnNumber.Text = Techs.Where(x => x.techStatus == "上钟").Count().ToString();
            toolOrderNumber.Text = Techs.Where(x => x.techStatus == "点钟").Count().ToString();
            toolOverNumber.Text = Techs.Where(x => x.techStatus == "加钟").Count().ToString();
        }

        //创建单个台位按钮
        private void createButton(int x, int y, Employee tech, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = tech.id;
            btn.Text = getBtnString(tech);
            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 110);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn.ContextMenuStrip = ctx;
            btn.ImageList = imageList1;
            btn.ImageAlign = ContentAlignment.TopRight;
            btn.Click += btn_Click;
            btn_status(btn, tech);

            sp.Controls.Add(btn);
        }

        //获取button显示字符串
        //房间号、技师号、手牌、服务项目、服务时间
        private string getBtnString(Employee tech)
        {
            string techId = tech.id;
            string str = "技师:" + techId;

            try
            {
                if (tech.techStatus == null || tech.techStatus == "空闲" || tech.techStatus == "下班")
                    return str;

                if (tech.room != null)
                    str += "\n房间:" + tech.room;

                if (tech.seat != null)
                    str += "\n手牌:" + tech.seat;

                if (tech.startTime != null)
                {
                    str += "\n起钟:" + tech.startTime.Value.ToString("HH:mm");

                    if (tech.serverTime != null)
                    {
                        int m = (int)(tech.serverTime - (DateTime.Now - tech.startTime.Value).TotalMinutes);
                        str += "\n剩:" + m.ToString() + "分钟";
                    }
                }
                
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg("获取技师字符串出错:" + e.Message);
            }

            return str;
            
        }

        //点击台位按钮
        private void btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Name == m_tech_id)
            {
                btn.ImageIndex = -1;
                m_tech_id = "-1";
            }
            else
            {
                if (m_tech_id != "-1")
                {
                    var old_btn = (Button)(tPanel.Controls.Find(m_tech_id, false).FirstOrDefault());
                    if (old_btn != null)
                        old_btn.ImageIndex = -1;
                }
                btn.ImageIndex = 0;
                m_tech_id = btn.Name;
                m_tech = dao.get_Employee("id='" + m_tech_id + "'");
                if (m_tech.techStatus != null && m_tech.techStatus == "下班")
                    btnClock.Text = "上班";
                else
                    btnClock.Text = "下班";
            }
        }

        //生成按钮状态
        private void btn_status(Button btn, Employee tech)
        {
            if (tech.techStatus == "下班")
                btn.BackColor = off_color;
            else if (tech.techStatus == "空闲" || tech.techStatus == null)
                btn.BackColor = avi_color;
            else if (tech.techStatus == "上钟" || tech.techStatus.Trim() == "轮钟")
                btn.BackColor = on_color;
            else if (tech.techStatus == "点钟")
                btn.BackColor = order_color;
            else if (tech.techStatus == "加钟")
                btn.BackColor = plus_color;
            else if (tech.techStatus == "待钟")
                btn.BackColor = wait_color;
        }

        //创建技师列表
        private void createTechnicianPanel(BathDBDataContext db)
        {
            var tLst = db.Employee.Where(x => db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师")).ToList();
            if (techTypes.Text != "所有技师")
                tLst = db.Employee.Where(x => db.Job.FirstOrDefault(y => y.id == x.jobId).name == techTypes.Text).ToList();
            tPanel.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;
            int count = tLst.Count;
            while (index < count)
            {
                while ((col + 1) * 160 < tPanel.Size.Width && index < count)
                {
                    int x = col * 140 + 20 * (col + 1);
                    int y = row * 110 + 20 * (row + 1);
                    createButton(x, y, tLst[index], tPanel);
                    col++;
                    index++;
                }
                col = 0;
                row++;
            }
        }

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //右键下班
        private void 下班ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip cmenu = item.GetCurrentParent() as ContextMenuStrip;
            Button bt = cmenu.SourceControl as Button;

            var dc = new BathDBDataContext(m_connectionString);
            var user = dc.Employee.FirstOrDefault(x => x.id == bt.Name);
            if (user == null)
                return;

            var tech_index = dao.get_techIndex("select * from [TechIndex] where dutyid=" + user.jobId);
            List<string> old_index = null;
            if (tech_index != null)
                old_index = tech_index.ids.Split('%').ToList();

            string cmd_str = "";
            if (user.techStatus != "下班")
            {
                if (user.techStatus != null && user.techStatus != "空闲")
                {
                    if (BathClass.printAskMsg("技师正在上钟，确认下班?") != DialogResult.Yes)
                        return;
                }
                cmd_str = "update [Employee] set techStatus='下班' where id='" + user.id + "'";
                if (tech_index != null)
                {
                    old_index.Remove(user.id);
                    cmd_str += " update [techIndex] set ids='" + string.Join("%", old_index.ToArray()) + "' where id=" + tech_index.id;
                }
            }
            else
            {
                cmd_str = "update [Employee] set techStatus='空闲',startTime=null, seat=null,serverTime=null,room=null,OrderClock=null" +
                       " where id='" + user.id + "'";
                if (tech_index != null)
                {
                    old_index.Add(user.id);
                    cmd_str += " update [techIndex] set ids='" + string.Join("%", old_index.ToArray()) + "' where id=" + tech_index.id;
                }
            }

            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("技师上下班失败!");
                return;
            }
        }

        private void TechnicianSeclectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null && m_thread.IsAlive)
                m_thread.Abort();
        }

        //查询
        private void btnFind_Click(object sender, EventArgs e)
        {
            var dc = new BathDBDataContext(m_connectionString);
            createTechnicianPanel(dc);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //上下班
        private void btnClock_Click(object sender, EventArgs e)
        {
            if (m_tech_id == "-1")
            {
                BathClass.printErrorMsg("请先选择技师!");
                return;
            }

            var tech_index = dao.get_techIndex("select * from [TechIndex] where dutyid=" + m_tech.jobId);
            List<string> old_index = null;
            if (tech_index != null)
                old_index = tech_index.ids.Split('%').ToList();

            string cmd_str = "";
            if (m_tech.techStatus != null && m_tech.techStatus == "下班")
            {
                cmd_str = "update [Employee] set techStatus='空闲',startTime=null, seat=null,serverTime=null,room=null,OrderClock=null" +
                       " where id='" + m_tech_id + "'";
                if (tech_index != null)
                {
                    old_index.Add(m_tech_id);
                    cmd_str += " update [techIndex] set ids='" + string.Join("%", old_index.ToArray()) + "' where id=" + tech_index.id;
                }
            }
            else
            {
                if (m_tech.techStatus != null && m_tech.techStatus != "空闲")
                {
                    if (BathClass.printAskMsg("技师正在上钟，确认下班?") != DialogResult.Yes)
                        return;
                }
                cmd_str = "update [Employee] set techStatus='下班' where id='" + m_tech_id + "'";
                if (tech_index != null)
                {
                    old_index.Remove(m_tech_id);
                    cmd_str += " update [techIndex] set ids='" + string.Join("%", old_index.ToArray()) + "' where id=" + tech_index.id;
                }
            }

            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("技师上下班失败!");
                return;
            }
            else
            {
                var btn = (Button)(tPanel.Controls.Find(m_tech_id, false).FirstOrDefault());
                btn.ImageIndex = -1;

                m_tech_id = "-1";
                m_tech = null;
            }
        }

    }
}
