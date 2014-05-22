using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using YouSoftBathFormClass;
using YouSoftBathGeneralClass;

namespace YouSoftBathBack
{
    public partial class MemberItemChart : Form
    {
        private Dictionary<string, int> memberInfo = new Dictionary<string, int>();
        private BathDBDataContext db = null;
        private string memberId;
        private bool member_found = false;
        private string catgory_txt;

        //构造函数
        public MemberItemChart(string _memberId)
        {
            memberId = _memberId;
            InitializeComponent();
        }

        //对话框载入
        private void MemberSettingForm_Load(object sender, EventArgs e)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            ComboCatgory.Items.AddRange(db.Catgory.Select(x => x.name).ToArray());
            
            btnOk.Location = new Point((this.Size.Width-btnOk.Size.Width)/2, btnOk.Location.Y);
            PBarVal.Location = new Point((panel2.Width - PBarVal.Width) / 2, (panel2.Height - PBarVal.Height) / 2);
            LabelInfo.Location = new Point(PBarVal.Location.X, PBarVal.Location.Y - LabelInfo.Size.Height);
            
            BtnFind_Click(null, null);
        }

        private void set_pbar_val(int val)
        {
            PBarVal.Value = val;
        }
        private delegate void delegate_set_pbar_val(int val);
        private void update_pbar()
        {
            try
            {
                int i = 0;
                while (true)
                {
                    if (member_found) break;
                    if (PBarVal.Value == 100)
                    {
                        this.Invoke(new delegate_set_pbar_val(set_pbar_val), new object[] { 0 });
                    }
                    else
                    {
                        this.Invoke(new delegate_set_pbar_val(set_pbar_val), new object[] { PBarVal.Value + 1 });
                    }

                    Thread.Sleep(50);
                }
            }
            catch (System.Exception ex)
            {
            	
            }
        }

        private void find_member_menu_number()
        {
            try
            {
                Catgory catgory = null;
                if (catgory_txt != "")
                {
                    catgory = db.Catgory.FirstOrDefault(x => x.name == catgory_txt);
                }
                memberInfo.Clear();
                var account_ids = db.CardCharge.Where(x => x.CC_CardNo == memberId && x.CC_AccountNo != null).Select(x => x.CC_AccountNo);
                var orders = db.HisOrders.Where(x => account_ids.Contains(x.accountId.ToString()));

                var order_menus = orders.Select(x => x.menu).Distinct();
                foreach (var order_menu in order_menus)
                {
                    if (order_menu.Contains("浴资")) continue;
                    if (catgory != null)
                    {
                        var order_menu_obj = db.Menu.FirstOrDefault(x => x.name == order_menu);
                        if (order_menu_obj != null && order_menu_obj.catgoryId != catgory.id) continue;
                    }
                    memberInfo.Add(order_menu, orders.Where(x => x.menu == order_menu).Count());
                }

                member_found = true;
                this.Invoke(new delegate_BindGrid(BindGrid));
            }
            catch (System.Exception ex)
            {
                BathClass.printErrorMsg(ex.Message);
            }
            finally
            {
                member_found = true;
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
                default:
                    break;
            }
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private delegate void delegate_BindGrid();
        private void BindGrid()
        {
            chart1.Visible = true;
            LabelInfo.Visible = false;
            PBarVal.Visible = false;

            chart1.Width = 800;
            chart1.Height = 600;

            //作图区的显示属性设置
            chart1.ChartAreas["ChartArea1"].AxisX.IsMarginVisible = false;
            chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            //背景色设置
            chart1.ChartAreas["ChartArea1"].ShadowColor = Color.Transparent;
            chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(209, 237, 254);//该处设置了有天蓝到白色的逐渐变化
            chart1.ChartAreas["ChartArea1"].BackGradientStyle = GradientStyle.TopBottom;
            chart1.ChartAreas["ChartArea1"].BackSecondaryColor = Color.White;
            
            //X,Y坐标线颜色和大小
            chart1.ChartAreas["ChartArea1"].AxisX.LineColor = Color.FromArgb(64, 64, 64, 64);
            chart1.ChartAreas["ChartArea1"].AxisY.LineColor = Color.FromArgb(64, 64, 64, 64);
            chart1.ChartAreas["ChartArea1"].AxisX.LineWidth = 2;
            chart1.ChartAreas["ChartArea1"].AxisY.LineWidth = 2;
            chart1.ChartAreas["ChartArea1"].AxisX.Title = "项目名称";
            chart1.ChartAreas["ChartArea1"].AxisY.Title = "数量";

            //中间X,Y线条颜色设置
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.FromArgb(64, 64, 64, 64);

            //X,Y轴数据显示间隔
            //chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1.0;
            //chart1.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Days;
            //chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffset = 0.0;
            //chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffsetType = DateTimeIntervalType.Days;
            //chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "M-d";
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart1.ChartAreas["ChartArea1"].AxisX.IntervalOffset = 1;  //设置X轴坐标偏移为1
            chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.IsStaggered = true;   //设置是否交错显示,比如数据多的时间分成两行来显示 
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 20;

            chart1.Palette = ChartColorPalette.Pastel;
            chart1.Dock = DockStyle.Fill;

            /////////////////////Series属性设置///////////////////////////
            //设置显示类型-线型
            //chart1.Series["随机数"].ChartType = SeriesChartType.Line;//折线图
            //chart1.Series["随机数"].ChartType = SeriesChartType.Bar;
            chart1.Series["项目数量"].ChartType = SeriesChartType.Column;
            //设置坐标轴Value显示类型
            chart1.Series["项目数量"].XValueType = ChartValueType.Time;
            //是否显示标签的数值
            chart1.Series["项目数量"].IsValueShownAsLabel = true;

            //设置标记图案
            chart1.Series["项目数量"].MarkerStyle = MarkerStyle.Circle;
            //设置图案颜色
            chart1.Series["项目数量"].Color = Color.Green;
            //设置图案的宽度
            chart1.Series["项目数量"].BorderWidth = 3;

            //添加随机数
            chart1.Series["项目数量"].Points.Clear();
            foreach (var key in memberInfo.Keys)
            {
                chart1.Series["项目数量"].Points.AddXY(key, memberInfo[key]);
            }
            //for (int i = 1; i < 20; i++)
            //{
            //    chart1.Series["项目数量"].Points.AddXY(i, rd.Next(100));
            //}
        }

        //查询
        private void BtnFind_Click(object sender, EventArgs e)
        {
            catgory_txt = ComboCatgory.Text.Trim();
            chart1.Visible = false;
            PBarVal.Visible = true;
            LabelInfo.Visible = true;
            PBarVal.Maximum = 100;
            PBarVal.Value = 0;

            var td1 = new Thread(new ThreadStart(find_member_menu_number));
            td1.IsBackground = true;
            td1.Start();

            var td2 = new Thread(new ThreadStart(update_pbar));
            td2.IsBackground = true;
            td2.Start();
        }

    }
}
