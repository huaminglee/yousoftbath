using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Runtime.InteropServices;
using System.IO;
using YouSoftBath;
using System.Timers;
using System.Net;
using System.Threading;
using YouSoftUtil;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using YouSoftBathReception;
using YouSoftBathConstants;

namespace YouSoftBathTechnician
{
    public partial class MainForm : Form
    {
        //成员变量
        private static string connectionIP = "";
        private int msgMaximum = 5;//消息上限，应该在设置里面进行设置
        private TechMsg m_msg = null;
        private System.Timers.Timer clock_timer;//时钟
        private string m_company;
        private SoundPlayer m_player;
        private string m_ip;
        private Thread m_thread;
        private Thread m_thread_tech;//技师面板线程
        private Thread m_thread_clearMemory;//清理内存
        private DAO dao;
        private static COptions _options;
        private bool user_card;//是否使用员工卡
        private bool print_tech_msg;//打印技师派遣单

        public static COptions options
        {
            get { return _options; }
            set { _options = value; }
        }

        private static Color off_color = Color.FromArgb(1, 175, 3);//下班颜色
        private static Color avi_color = Color.FromArgb(255,255, 255);//空闲颜色
        private static Color wait_color = Color.FromArgb(85, 213, 206);//待钟颜色
        private static Color on_color = Color.FromArgb(198, 0, 0);//上钟颜色
        private static Color order_color = Color.FromArgb(255, 115, 39);//点钟颜色
        private static Color plus_color = Color.FromArgb(91, 213, 206);//加钟颜色

        //构造函数
        public MainForm()
        {
            InitializeComponent();
            label2.Text = "店小二桑拿会所管理系统技师面板" + Constants.version;
        }

        //对话框载入
        private void Form1_Load(object sender, EventArgs e)
        {
            connectionIP = IOUtil.get_config_by_key(ConfigKeys.KEY_CONNECTION_IP);
            if (connectionIP == "")
            {
                PCListForm pCListForm = new PCListForm();
                if (pCListForm.ShowDialog() != DialogResult.OK)
                {
                    this.Close();
                    return;
                }
                connectionIP = pCListForm.ip;
                IOUtil.set_config_by_key(ConfigKeys.KEY_CONNECTION_IP, connectionIP);
            }
            dao = new DAO(connectionString);
            if (!dao.check_net())
            {
                BathClass.printErrorMsg("连接IP不对或者网络不通，请重试!");
                this.Close();
                return;
            }

            _options = dao.get_options();
            m_company = _options.companyName;
            print_tech_msg = MConvert<bool>.ToTypeOrDefault(options.打印技师派遣单, false);
            user_card = MConvert<bool>.ToTypeOrDefault(options.启用员工服务卡, false);
            IPHostEntry ipe = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in ipe.AddressList)
            {
                m_ip = ip.ToString();
                if (System.Text.RegularExpressions.Regex.IsMatch(m_ip, "[0-9]{1,3}//.[0-9]{1,3}//.[0-9]{1,3}//.[0-9]{1,3}"))
                    break;
            }

            setMax();
            //dgv.ColumnHeadersDefaultCellStyle.Font = new Font("宋体", 20);
            //dgv.RowsDefaultCellStyle.Font = new Font("宋体", 20);
            //dgv_show();

            m_thread = new Thread(new ThreadStart(detect_msg));
            m_thread.IsBackground = true;
            m_thread.Start();

            m_thread_tech = new Thread(new ThreadStart(detect_tech_index));
            m_thread_tech.IsBackground = true;
            m_thread_tech.Start();

            clock_timer = new System.Timers.Timer();
            clock_timer.Interval = 1000;
            clock_timer.Elapsed += new System.Timers.ElapsedEventHandler(clock_timer_Elapsed);
            clock_timer.Enabled = true;

            m_player = new SoundPlayer();

            techId.Focus();

            m_thread_clearMemory = new Thread(new ThreadStart(clear_Memory));
            m_thread_clearMemory.IsBackground = true;
            m_thread_clearMemory.Start();

            SplitGender.SplitterDistance = SplitGender.Width*2 / 3;
            create_tech_panel();
        }

        //检测技师排钟变化
        private void detect_tech_index()
        {
            while(true)
            {
                try
                {
                    var dc = new BathDBDataContext(connectionString);
                    var job = dc.Job.FirstOrDefault(x => x.ip == m_ip);
                    
                    var new_index_male = dc.TechIndex.FirstOrDefault(x => x.dutyid == job.id && x.gender=="男").ids;
                    string old_index_male = IOUtil.get_config_by_key(ConfigKeys.KEY_TECH_INDEX_MALE);
                    if (old_index_male != new_index_male)
                    {
                        IOUtil.set_config_by_key(ConfigKeys.KEY_TECH_INDEX_MALE, new_index_male);
                        this.Invoke(new delegate_create_tech_panel_gender(create_tech_panel_gender),
                                            new object[] { dc, "男", SplitGender.Panel2 });
                        //this.Invoke(new delegate_no_param(create_tech_panel));
                    }

                    var new_index_female = dc.TechIndex.FirstOrDefault(x => x.dutyid == job.id && x.gender=="女").ids;
                    string old_index_female = IOUtil.get_config_by_key(ConfigKeys.KEY_TECH_INDEX_FEMALE);
                    if (old_index_female != new_index_female)
                    {
                        IOUtil.set_config_by_key(ConfigKeys.KEY_TECH_INDEX_FEMALE, new_index_female);
                        this.Invoke(new delegate_create_tech_panel_gender(create_tech_panel_gender),
                                            new object[] { dc, "女", SplitGender.Panel1 });
                        //this.Invoke(new delegate_no_param(create_tech_panel));
                    }
                }
                catch (System.Exception e)
                {
                	
                }
            }
        }

        //创建单个台位按钮
        private void createButton(int x, int y, Employee tech, Control sp)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 14F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = tech.id;
            btn_status(btn, tech);
            btn.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            btn.Size = new System.Drawing.Size(140, 110);
            btn.FlatStyle = FlatStyle.Popup;
            btn.UseVisualStyleBackColor = true;
            btn.ContextMenuStrip = ctx;
            //btn.ImageList = imageList1;
            btn.ImageAlign = ContentAlignment.TopRight;
            //btn.Click += btn_Click;

            sp.Controls.Add(btn);
        }

        //获取技师字符串
        private void btn_status(Button btn, Employee tech)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("技师:").Append(tech.id).Append("\n");

            if (tech.techStatus == "待钟")
            {
                sb.Append("房间:").Append(tech.room).Append("\n");
                sb.Append("手牌:").Append(tech.seat).Append("\n");
                sb.Append(tech.techMenu).Append("\n");
            }
            
            btn.Text = sb.ToString();

            if (tech.techStatus == "下班")
                btn.BackColor = off_color;
            else if (tech.techStatus == "待钟")
                btn.BackColor = wait_color;
            else if (tech.techStatus == "空闲" || tech.techStatus == null)
                btn.BackColor = avi_color;
            else if (tech.techStatus == "上钟" || tech.techStatus.Trim() == "轮钟")
                btn.BackColor = on_color;
            else if (tech.techStatus == "点钟")
                btn.BackColor = order_color;
            else if (tech.techStatus == "加钟")
                btn.BackColor = plus_color;
        }

        //全部重排
        private void reArrange_all_techs(BathDBDataContext dc)
        {
            dc.ExecuteCommand("truncate table techindex");
            dc.SubmitChanges();

            var job_ids = dc.Job.Where(x => x.name.Contains("技师")).Select(x => x.id);
            foreach (var job_id in job_ids)
            {
                var techs = dc.Employee.Where(x => x.jobId == job_id);

                techs = techs.Where(x => x.techStatus == null || x.techStatus == "空闲" || x.techStatus == "待钟");

                var techs_male = techs.Where(x => x.gender == "男");
                var techIndex = new TechIndex();
                techIndex.dutyid = job_id;
                techIndex.gender = "男";
                techIndex.ids = string.Join("%", techs_male.OrderBy(x => x.id).Select(x => x.id + "=T").ToArray());
                dc.TechIndex.InsertOnSubmit(techIndex);

                var techs_female = techs.Where(x => x.gender == "女");
                techIndex = new TechIndex();
                techIndex.dutyid = job_id;
                techIndex.gender = "女";
                techIndex.ids = string.Join("%", techs_female.OrderBy(x => x.id).Select(x => x.id + "=T").ToArray());
                dc.TechIndex.InsertOnSubmit(techIndex);
            }
            dc.SubmitChanges();
        }

        private delegate void createButton_delegate(int x, int y, Employee tech, Control sp);

        //创建技师排钟
        private void create_tech_panel()
        {
            try
            {
                var db = new BathDBDataContext(connectionString);
                if (!db.TechIndex.Any())
                {
                    if (BathClass.printAskMsg("没有检测到排钟表，是否初始化？") != DialogResult.Yes)
                        return;

                    reArrange_all_techs(db);
                }

                create_tech_panel_gender(db, "男", SplitGender.Panel2);
                create_tech_panel_gender(db, "女", SplitGender.Panel1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void create_tech_panel_gender(BathDBDataContext db, string gender, Control sp)
        {
            sp.Controls.Clear();

            var job = db.Job.FirstOrDefault(x => x.ip == m_ip);
            var tLst = db.Employee.Where(x => db.Job.FirstOrDefault(y => y.id == x.jobId).name.Contains("技师")).
                Select(x => x.id).ToList();

            if (job != null)
            {
                tLst = db.TechIndex.FirstOrDefault(x => x.dutyid == job.id && x.gender == gender).ids.Split('%').ToList();
            }
            int row = 0;
            int col = 0;
            int index = 0;
            int count = tLst.Count;
            while (index < count)
            {
                while ((col + 1) * 160 < sp.Size.Width && index < count)
                {
                    var tech_index = tLst[index].Split('=');
                    if (tech_index.Length == 2 && tech_index[1] == "F")
                    {
                        index++;
                        continue;
                    }
                    var tech_id = tech_index[0];
                    var tech = db.Employee.FirstOrDefault(s => s.id == tech_id);
                    if (tech == null)
                    {
                        index++;
                        continue;
                    }

                    int x = col * 140 + 20 * (col + 1);
                    int y = row * 110 + 20 * (row + 1);

                    createButton(x, y, tech, sp);
                    col++;
                    index++;
                }
                col = 0;
                row++;
            }
        }
        private delegate void delegate_create_tech_panel_gender(BathDBDataContext db, string gender, Control sp);

        //设置信息条数
        private void setMax()
        {
            if (_options == null)
                return;

            var shoe = _options.启用鞋部;
            var max = _options.鞋部条数;
            if (shoe == null || !Convert.ToBoolean(shoe) || max == null)
                return;

            msgMaximum = Convert.ToInt32(max);
        }

        //显示信息
        /*private void dgv_show()
        {
            var dc = new BathDBDataContext(connectionString);
            dgv.DataSource = from x in dc.TechMsg
                             orderby x.time descending
                             select new
                             {
                                 房间号 = x.room,
                                 手牌号 = x.seat,
                                 时间 = x.time.ToShortTimeString(),
                                 类型 = x.type,
                                 技师号 = (x.techId == null || x.techId == "") ? x.techType : x.techId,
                                 项目 = x.menu
                             };
            foreach(DataGridViewRow r in dgv.Rows)
                r.Height = 40;

            BathClass.set_dgv_fit(dgv);
            dgv.CurrentCell = null;
        }*/

        //时钟
        private void clock_timer_Elapsed(object sender, EventArgs e)
        {
            this.Invoke(new delegate_no_parameter(show_clock), null);
        }

        private delegate void delegate_no_parameter();

        private void show_clock()
        {
            clock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        //定时轮询
        private void detect_msg()
        {
            while(true)
            {
                try
                {
                    var dc = new BathDBDataContext(connectionString);
                    var job = dc.Job.FirstOrDefault(x => x.ip == m_ip);
                    if (job == null)
                        m_msg = dc.TechMsg.FirstOrDefault(x => !x.read);
                    else
                    {
                        m_msg = dc.TechMsg.FirstOrDefault(x => !x.read && 
                            (x.techType == job.name || (x.techId!=null && x.techId!="" && 
                            dc.Employee.FirstOrDefault(y=>y.id==x.techId).jobId==job.id)));
                    }

                    if (m_msg == null)
                    {
                        this.Invoke(new delegate_no_param(unable_msg_btn), null);
                        continue;
                    }

                    var tech = dc.Employee.FirstOrDefault(x => x.msgId == m_msg.id);
                    if (tech == null && job != null)
                    {
                        tech = dc.Employee.FirstOrDefault(x => x.id == m_msg.techId);
                        if (tech == null)
                        {
                            if (m_msg.gender == null || m_msg.gender.Trim() == "" || m_msg.gender == "无"|| m_msg.gender == "女")
                            {
                                var tLst = dc.TechIndex.FirstOrDefault(x => x.dutyid == job.id && x.gender == "女").ids.Split('%').ToList();

                                foreach (var techIds in tLst)
                                {
                                    var tech_id = techIds.Split('=');
                                    if (tech_id[1] == "F") continue;

                                    var tmp_tech = dc.Employee.FirstOrDefault(x => x.id == tech_id[0]);
                                    if (tmp_tech.techStatus == "空闲" || tmp_tech.techStatus == null)
                                    {
                                        tech = tmp_tech;
                                        tech.techStatus = "待钟";
                                        tech.techMenu = m_msg.menu;
                                        tech.room = m_msg.room;
                                        tech.seat = m_msg.seat;
                                        tech.msgId = m_msg.id;

                                        m_msg.techId = tech.id;
                                        dc.SubmitChanges();

                                        this.Invoke(new delegate_create_tech_panel_gender(create_tech_panel_gender),
                                            new object[]{dc, "女", SplitGender.Panel1});
                                        //this.Invoke(new delegate_no_param(create_tech_panel));
                                        break;
                                    }
                                }
                            }
                            else if (m_msg.gender == "男")
                            {
                                var tLst = dc.TechIndex.FirstOrDefault(x => x.dutyid == job.id && x.gender == "男").ids.Split('%').ToList();

                                foreach (var techIds in tLst)
                                {
                                    var tech_id = techIds.Split('=');
                                    if (tech_id[1] == "F") continue;
                                    var tmp_tech = dc.Employee.FirstOrDefault(x => x.id == tech_id[0]);
                                    if (tmp_tech.techStatus == "空闲" || tmp_tech.techStatus == null)
                                    {
                                        tech = tmp_tech;
                                        tech.techStatus = "待钟";
                                        tech.techMenu = m_msg.menu;
                                        tech.room = m_msg.room;
                                        tech.seat = m_msg.seat;
                                        tech.msgId = m_msg.id;

                                        m_msg.techId = tech.id;
                                        dc.SubmitChanges();

                                        this.Invoke(new delegate_create_tech_panel_gender(create_tech_panel_gender),
                                            new object[]{dc, "男", SplitGender.Panel2});
                                        //this.Invoke(new delegate_no_param(create_tech_panel));
                                        break;
                                    }
                                }
                            }
                            
                        }
                        else
                        {
                            tech.techStatus = "待钟";
                            tech.techMenu = m_msg.menu;
                            tech.room = m_msg.room;
                            tech.seat = m_msg.seat;
                            tech.msgId = m_msg.id;

                            m_msg.techId = tech.id;
                            dc.SubmitChanges();

                            Control sp = SplitGender.Panel1;
                            if (tech.gender == "男")
                                sp = SplitGender.Panel2;

                            this.Invoke(new delegate_create_tech_panel_gender(create_tech_panel_gender),
                                            new object[] { dc, tech.gender, sp });
                            //this.Invoke(new delegate_no_param(create_tech_panel));

                        }
                        
                    }
                    this.Invoke(new delegate_no_param(showMessage));
                    play();
                    if (print_tech_msg)
                        print_msg(dc);
                }
                catch
                {
                }
            }
        }

        private delegate void delegate_no_param();
        private void unable_msg_btn()
        {
            msg.Visible = false;
            btnReceived.Enabled = false;
        }

        //显示消息
        private void showMessage()
        {
            btnReceived.Enabled = true;
            msg.Visible = true;
            if (m_msg.type == "上钟" || m_msg.type == "轮钟")
            {
                msg.Text = m_msg.room + "|" + m_msg.type + "|" + m_msg.techId + "|" + m_msg.menu;
            }
            else if (m_msg.type == "点钟")
            {
                msg.Text = m_msg.room + "|" + m_msg.type + "|" + m_msg.techId + "|" + m_msg.menu;
            }
            else if (m_msg.type == "挑钟")
            {
                msg.Text = m_msg.room + "|" + m_msg.type + "|" + m_msg.techId + "|" + m_msg.menu;
            }
        }

        //播放声音
        private void play()
        {
            m_player.SoundLocation = @".\wav\roomId.wav";
            m_player.Play();
            Thread.Sleep(1000);

            foreach (char c in m_msg.room)
            {
                string a = c.ToString();
                if (!char.IsDigit(c))
                    a = c.ToString().ToLower();
                m_player.SoundLocation = @".\wav\" + a + ".wav";
                m_player.Play();
                Thread.Sleep(700);
            }

            m_player.SoundLocation = @".\wav\" + m_msg.type + ".wav";
            m_player.Play();
            Thread.Sleep(900);

            m_player.SoundLocation = @".\wav\tech.wav";
            m_player.Play();
            Thread.Sleep(900);
            //m_player.SoundLocation = @".\wav\" + m_msg.number + ".wav";
            //m_player.Play();
            //Thread.Sleep(400);
            //m_player.SoundLocation = @".\wav\名.wav";
            //m_player.Play();
            //Thread.Sleep(400);

            if (m_msg.techId != null && m_msg.techId != "")
            {
                foreach (char c in m_msg.techId)
                {
                    string a = c.ToString();
                    if (!char.IsDigit(c))
                        a = c.ToString().ToLower();
                    m_player.SoundLocation = @".\wav\" + a + ".wav";
                    m_player.Play();
                    Thread.Sleep(700);
                }
            }
        }

        //收到消息
        private void btnReceived_Click(object sender, EventArgs e)
        {
            try
            {
                //var dc_new = new BathDBDataContext(connectionString);

                //var msg = dao.get_techMsg("select * from [TechMsg] where id=" + m_msg.id);
                ////var msg = dc_new.TechMsg.FirstOrDefault(x => x.id == m_msg.id);
                //msg.read = true;
                string cmd_str = @"update [TechMsg] set [read]=1 where id=" + m_msg.id;

                int count = dao.get_entities_count("select count(*) from [TechMsg]");
                if (count > msgMaximum)
                    cmd_str += @" delete top(" + (count - msgMaximum).ToString() + ") from [TechMsg]";
                    //dc_new.TechMsg.DeleteAllOnSubmit(dc_new.TechMsg.ToList().Take(dc_new.TechMsg.Count() - msgMaximum));

                if (!dao.execute_command(cmd_str))
                {
                    BathClass.printErrorMsg("记录更新失败，请重试!");
                    return;
                }
                //dc_new.SubmitChanges();
                btnReceived.Enabled = false;

                m_player.Stop();
                techId.Focus();

                //_thread_pause = false;
            }
            catch
            {
            }
        }

        //删除
        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        //退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //绑定快捷键
        private void RoomViewForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    change_tech_status();
                    break;
                case Keys.Delete:
                    btnDel_Click(null, null);
                    break;
                case Keys.Escape:
                    btnExit_Click(null, null);
                    break;
            }
        }

        //返回连接字符串
        public static string connectionString
        {
            get
            {
                return @"Data Source=" + connectionIP + @"\SQLEXPRESS;"
                + @"Initial Catalog=BathDB;Persist Security Info=True;"
                + @"User ID=sa;pwd=123";
                //return @"Data Source=" + "." + @"\SQLEXPRESS;"
                //+ @"Initial Catalog=BathDB;Persist Security Info=True;"
                //+ @"User ID=sa;pwd=123";
            }
        }

        //技师管理
        private void toolTech_Click(object sender, EventArgs e)
        {
            TechnicianSeclectForm form = new TechnicianSeclectForm(connectionString, true);
            form.ShowDialog();
        }

        //客房查看
        private void btnRoom_Click(object sender, EventArgs e)
        {
            var form = new InformRoomForm(connectionString, true);
            form.ShowDialog();
        }

        //查询服务
        private void btnCheck_Click(object sender, EventArgs e)
        {
            Employee user;
            if (user_card)
            {
                var form = new InputEmployeeByCard(connectionString);
                if (form.ShowDialog() != DialogResult.OK)
                    return;
                user = form.employee;
            }
            else
            {
                var form = new InputEmployeeByPwd(connectionString);
                if (form.ShowDialog() != DialogResult.OK)
                    return;

                user = form.employee;
            }
            
            CheckForm checkForm = new CheckForm(user);
            checkForm.ShowDialog();
            techId.Focus();
        }

        //刷卡改变技师状态
        private void change_tech_status()
        {
            if (techId.Text == "")
                return;

            var tech = dao.get_Employee("id='" + techId.Text + "'");
            if (tech == null)
            {
                BathClass.printErrorMsg("技师号不存在!");
                return;
            }

            var tech_index = dao.get_techIndex("select * from [TechIndex] where dutyid=" + tech.jobId);
            List<string> old_index = null;
            if (tech_index != null)
                old_index = tech_index.ids.Split('%').ToList();

            string cmd_str = "";
            if (tech.techStatus != null && tech.techStatus == "下班")
            {
                cmd_str = "update [Employee] set status='空闲',startTime=null, seat=null,"+
                    "serverTime=null,room=null,OrderClock=null where id='" + tech.id + "'";
                if (tech_index != null)
                {
                    old_index.Add(tech.id);
                    cmd_str += " update [techIndex] set ids='" + string.Join("%", old_index.ToArray()) + "' where id=" + tech_index.id;
                }
            }
            else
            {
                if (tech.techStatus != null && tech.techStatus != "空闲")
                {
                    if (BathClass.printAskMsg("技师正在上钟，确认下班?") != DialogResult.Yes)
                        return;
                }
                cmd_str = "update [Employee] set status='下班' where id='" + tech.id + "'";
                if (tech_index != null)
                {
                    old_index.Remove(tech.id);
                    cmd_str += " update [techIndex] set ids='" + string.Join("%", old_index.ToArray()) + "' where id=" + tech_index.id;
                }
                
            }

            techId.Text = "";
            techId.Focus();
            if (!dao.execute_command(cmd_str))
            {
                BathClass.printErrorMsg("技师上下班失败，请重试!");
            }
        }

        //输入内容
        private void BtnNumber_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            techId.Focus();

            techId.Text += btn.Text;
            techId.SelectionStart = techId.Text.Length;

        }

        //回删
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (techId.Text == "")
                return;

            techId.Text = techId.Text.Substring(0, techId.Text.Length - 1);
            techId.SelectionStart = techId.Text.Length;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            change_tech_status();
        }

        //打印派遣单
        private void print_msg(BathDBDataContext dc)
        {
            if (MConvert<bool>.ToTypeOrDefault(m_msg.printed, false))
                return;

            if (!dao.execute_command("update [TechMsg] set printed=1 where id="+m_msg.id))
            {
                BathClass.printErrorMsg("打印派遣单失败，请重试!");
                return;
            }
            PrintMsg.Print_Msg(m_msg, m_company);
            //m_msg.printed = true;
            //dc.SubmitChanges();
        }

        private void techId_Enter(object sender, EventArgs e)
        {
            BathClass.change_input_en();
        }

        //技师排钟
        private void btnTechList_Click(object sender, EventArgs e)
        {
            var form = new TechListForm(connectionString);
            form.ShowDialog();

            //create_tech_panel();
        }

        private void clear_Memory()
        {
            while (true)
            {
                try
                {
                    ClearMemory();
                    Thread.Sleep(2 * 1000);
                }
                catch
                {
                }
            }
        }

        #region 内存回收
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
        #endregion

        private void 退钟回原排ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ContextMenuStrip cmenu = item.GetCurrentParent() as ContextMenuStrip;
            Button bt = cmenu.SourceControl as Button;
            if (bt.BackColor == avi_color)
                return;

            var dc = new BathDBDataContext(connectionString);
            var tech = dc.Employee.FirstOrDefault(x => x.id == bt.Name);
            tech.techStatus = "空闲";
            tech.techMenu = null;
            tech.room = null;
            tech.seat = null;
            tech.msgId = null;
            dc.SubmitChanges();

            bt.BackColor = avi_color;
            bt.Text = "技师:" + tech.id;
        }
    }
}
