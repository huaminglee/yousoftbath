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
    public partial class CustomerChooseForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;
        public string customerId = "";

        //构造函数
        public CustomerChooseForm(BathDBDataContext dc)
        {
            db = dc;
            InitializeComponent();
        }

        //对话框载入
        private void CustomerChooseForm_Load(object sender, EventArgs e)
        {
            createCustomerPanel();
        }

        //创建界面
        private void createCustomerPanel()
        {
            sp.Panel2.Controls.Clear();
            int row = 0;
            int col = 0;
            int index = 0;
            Size clientSize = sp.Panel2.Size;

            List<string> catList = db.Customer.Select(z => z.name).ToList();
            int count = catList.Count;
            while (index < count)
            {
                while ((col + 1) * 80 < clientSize.Width && index < count)
                {
                    int x = col * 70 + 10 * (col + 1);
                    int y = row * 70 + 10 * (row + 1);

                    createButton(x, y, catList[index], sp.Panel2, 2, Color.Gray);
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

        //选择客户
        private void btn_Click(object sender, EventArgs e)
        {
            foreach (Control c in sp.Panel2.Controls)
                c.BackColor = Color.Gray;

            Button btn = sender as Button;
            btn.BackColor = Color.Red;

            Customer customer = db.Customer.FirstOrDefault(x => x.name == btn.Text);
            contact.Text = customer.contact;
            phone.Text = customer.mobile;
            money.Text = customer.money.ToString();

            contact.Visible = true;
            phone.Visible = true;
            money.Visible = true;

            customerId = customer.id.ToString();
        }

        //确定
        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
