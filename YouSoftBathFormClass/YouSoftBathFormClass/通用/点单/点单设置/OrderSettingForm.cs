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
    public partial class OrderSettingForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public List<string> typeList;

        //构造函数
        public OrderSettingForm(List<string> tList)
        {
            db = new BathDBDataContext(LogIn.connectionString);
            typeList = tList;
            InitializeComponent();
        }

        //对话框载入
        private void OrderSettingForm_Load(object sender, EventArgs e)
        {
            createMenuTypePanel();
        }

        //保存
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        //创建菜单类别界面
        private void createMenuTypePanel()
        {
            cPanel.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;
            Size clientSize = this.Size;
            
            List<string> catList = db.Catgory.Select(z => z.name).ToList();
            int count = catList.Count;
            while (index < count)
            {
                while ((col + 1) * 80 < clientSize.Width && index < count)
                {
                    int x = col * 70 + 10 * (col + 1);
                    int y = row * 70 + 10 * (row + 1);

                    Color color = Color.Gray;
                    if (typeList.Contains(catList[index]))
                        color = Color.Red;
                    createButton(x, y, catList[index], cPanel, 2, color);
                    col++;
                    index++;
                }
                col = 0;
                row++;
            }
        }

        //创建单个台位按钮
        private void createButton(int x, int y, string txt, Control sp, int type, Color color)
        {
            Button btn = new Button();

            btn.Font = new Font("SimSun", 18F, FontStyle.Bold);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Name = txt;
            btn.Text = txt;
            btn.Size = new System.Drawing.Size(70, 70);
            btn.UseVisualStyleBackColor = true;
            btn.FlatStyle = FlatStyle.Popup;
            btn.BackColor = color;
            btn.ForeColor = Color.White;
            btn.Click += new System.EventHandler(btn_Click);
            sp.Controls.Add(btn);
        }

        //选择
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (typeList.Contains(btn.Text))
                typeList.Remove(btn.Text);
            else
                typeList.Add(btn.Text);

            foreach (Control c in cPanel.Controls)
            {
                Button b = c as Button;
                if (typeList.Contains(b.Text))
                    b.BackColor = Color.Red;
                else
                    b.BackColor = Color.Gray;
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
    }
}
