namespace YouSoftBathTechnician
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clock = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.msg = new System.Windows.Forms.Label();
            this.SplitGender = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.techId = new System.Windows.Forms.TextBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnTech = new System.Windows.Forms.Button();
            this.btnRoom = new System.Windows.Forms.Button();
            this.btnTechList = new System.Windows.Forms.Button();
            this.btnReceived = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button18 = new System.Windows.Forms.Button();
            this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.退钟回原排ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SplitGender.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.ctx.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1370, 750);
            this.panel2.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer1.Panel1.Controls.Add(this.clock);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.sp);
            this.splitContainer1.Size = new System.Drawing.Size(1370, 750);
            this.splitContainer1.SplitterDistance = 66;
            this.splitContainer1.TabIndex = 0;
            // 
            // clock
            // 
            this.clock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clock.AutoSize = true;
            this.clock.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clock.Location = new System.Drawing.Point(1101, 21);
            this.clock.Name = "clock";
            this.clock.Size = new System.Drawing.Size(257, 24);
            this.clock.TabIndex = 0;
            this.clock.Text = "2013-01-01 10:10:10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(612, 34);
            this.label2.TabIndex = 0;
            this.label2.Text = "店小二桑拿会所管理系统技师面板V2.1";
            // 
            // sp
            // 
            this.sp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.splitContainer2);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.splitContainer3);
            this.sp.Size = new System.Drawing.Size(1370, 680);
            this.sp.SplitterDistance = 1059;
            this.sp.TabIndex = 2;
            this.sp.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer2.Panel1.Controls.Add(this.msg);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer2.Panel2.Controls.Add(this.SplitGender);
            this.splitContainer2.Size = new System.Drawing.Size(1059, 680);
            this.splitContainer2.SplitterDistance = 100;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // msg
            // 
            this.msg.AutoSize = true;
            this.msg.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.msg.ForeColor = System.Drawing.Color.Red;
            this.msg.Location = new System.Drawing.Point(20, 21);
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(191, 54);
            this.msg.TabIndex = 0;
            this.msg.Text = "label1";
            this.msg.Visible = false;
            // 
            // SplitGender
            // 
            this.SplitGender.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SplitGender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitGender.IsSplitterFixed = true;
            this.SplitGender.Location = new System.Drawing.Point(0, 0);
            this.SplitGender.Name = "SplitGender";
            // 
            // SplitGender.Panel1
            // 
            this.SplitGender.Panel1.AutoScroll = true;
            // 
            // SplitGender.Panel2
            // 
            this.SplitGender.Panel2.AutoScroll = true;
            this.SplitGender.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            this.SplitGender.Size = new System.Drawing.Size(1059, 579);
            this.SplitGender.SplitterDistance = 532;
            this.SplitGender.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer3.Panel1.Controls.Add(this.label1);
            this.splitContainer3.Panel1.Controls.Add(this.techId);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(307, 680);
            this.splitContainer3.SplitterDistance = 100;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 59);
            this.label1.TabIndex = 8;
            this.label1.Text = "技师刷卡";
            // 
            // techId
            // 
            this.techId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(102)))));
            this.techId.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.techId.Location = new System.Drawing.Point(82, 14);
            this.techId.Name = "techId";
            this.techId.Size = new System.Drawing.Size(211, 68);
            this.techId.TabIndex = 7;
            this.techId.Enter += new System.EventHandler(this.techId_Enter);
            // 
            // splitContainer4
            // 
            this.splitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.btnExit);
            this.splitContainer4.Panel1.Controls.Add(this.btnCheck);
            this.splitContainer4.Panel1.Controls.Add(this.btnTech);
            this.splitContainer4.Panel1.Controls.Add(this.btnRoom);
            this.splitContainer4.Panel1.Controls.Add(this.btnTechList);
            this.splitContainer4.Panel1.Controls.Add(this.btnReceived);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.btnOk);
            this.splitContainer4.Panel2.Controls.Add(this.button11);
            this.splitContainer4.Panel2.Controls.Add(this.button7);
            this.splitContainer4.Panel2.Controls.Add(this.button2);
            this.splitContainer4.Panel2.Controls.Add(this.button10);
            this.splitContainer4.Panel2.Controls.Add(this.button6);
            this.splitContainer4.Panel2.Controls.Add(this.button1);
            this.splitContainer4.Panel2.Controls.Add(this.button9);
            this.splitContainer4.Panel2.Controls.Add(this.button15);
            this.splitContainer4.Panel2.Controls.Add(this.button16);
            this.splitContainer4.Panel2.Controls.Add(this.button17);
            this.splitContainer4.Panel2.Controls.Add(this.button18);
            this.splitContainer4.Size = new System.Drawing.Size(307, 579);
            this.splitContainer4.SplitterDistance = 255;
            this.splitContainer4.SplitterWidth = 1;
            this.splitContainer4.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Orange;
            this.btnExit.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(157, 168);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(140, 80);
            this.btnExit.TabIndex = 15;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheck.BackColor = System.Drawing.Color.Orange;
            this.btnCheck.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCheck.Location = new System.Drawing.Point(3, 168);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(140, 80);
            this.btnCheck.TabIndex = 17;
            this.btnCheck.TabStop = false;
            this.btnCheck.Text = "查询服务";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnTech
            // 
            this.btnTech.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTech.BackColor = System.Drawing.Color.Orange;
            this.btnTech.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTech.Location = new System.Drawing.Point(157, 84);
            this.btnTech.Name = "btnTech";
            this.btnTech.Size = new System.Drawing.Size(140, 80);
            this.btnTech.TabIndex = 17;
            this.btnTech.TabStop = false;
            this.btnTech.Text = "技师状态";
            this.btnTech.UseVisualStyleBackColor = false;
            this.btnTech.Click += new System.EventHandler(this.toolTech_Click);
            // 
            // btnRoom
            // 
            this.btnRoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRoom.BackColor = System.Drawing.Color.Orange;
            this.btnRoom.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRoom.Location = new System.Drawing.Point(3, 84);
            this.btnRoom.Name = "btnRoom";
            this.btnRoom.Size = new System.Drawing.Size(140, 80);
            this.btnRoom.TabIndex = 17;
            this.btnRoom.TabStop = false;
            this.btnRoom.Text = "催钟";
            this.btnRoom.UseVisualStyleBackColor = false;
            this.btnRoom.Click += new System.EventHandler(this.btnRoom_Click);
            // 
            // btnTechList
            // 
            this.btnTechList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTechList.BackColor = System.Drawing.Color.Orange;
            this.btnTechList.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTechList.Location = new System.Drawing.Point(157, 3);
            this.btnTechList.Name = "btnTechList";
            this.btnTechList.Size = new System.Drawing.Size(140, 75);
            this.btnTechList.TabIndex = 16;
            this.btnTechList.TabStop = false;
            this.btnTechList.Text = "技师排钟";
            this.btnTechList.UseVisualStyleBackColor = false;
            this.btnTechList.Click += new System.EventHandler(this.btnTechList_Click);
            // 
            // btnReceived
            // 
            this.btnReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceived.BackColor = System.Drawing.Color.Orange;
            this.btnReceived.Enabled = false;
            this.btnReceived.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReceived.Location = new System.Drawing.Point(3, 3);
            this.btnReceived.Name = "btnReceived";
            this.btnReceived.Size = new System.Drawing.Size(140, 75);
            this.btnReceived.TabIndex = 14;
            this.btnReceived.TabStop = false;
            this.btnReceived.Text = "收到消息";
            this.btnReceived.UseVisualStyleBackColor = false;
            this.btnReceived.Click += new System.EventHandler(this.btnReceived_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoEllipsis = true;
            this.btnOk.BackColor = System.Drawing.Color.Orange;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(204, 245);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 70);
            this.btnOk.TabIndex = 113;
            this.btnOk.TabStop = false;
            this.btnOk.Tag = "";
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // button11
            // 
            this.button11.AutoEllipsis = true;
            this.button11.BackColor = System.Drawing.Color.Orange;
            this.button11.FlatAppearance.BorderSize = 0;
            this.button11.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button11.Location = new System.Drawing.Point(204, 165);
            this.button11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(90, 70);
            this.button11.TabIndex = 113;
            this.button11.TabStop = false;
            this.button11.Tag = "";
            this.button11.Text = "3";
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button7
            // 
            this.button7.AutoEllipsis = true;
            this.button7.BackColor = System.Drawing.Color.Orange;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.Location = new System.Drawing.Point(204, 85);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(90, 70);
            this.button7.TabIndex = 112;
            this.button7.TabStop = false;
            this.button7.Text = "6";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button2
            // 
            this.button2.AutoEllipsis = true;
            this.button2.BackColor = System.Drawing.Color.Orange;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(106, 245);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 70);
            this.button2.TabIndex = 114;
            this.button2.TabStop = false;
            this.button2.Text = "回删";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // button10
            // 
            this.button10.AutoEllipsis = true;
            this.button10.BackColor = System.Drawing.Color.Orange;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button10.Location = new System.Drawing.Point(106, 165);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(90, 70);
            this.button10.TabIndex = 114;
            this.button10.TabStop = false;
            this.button10.Text = "2";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button6
            // 
            this.button6.AutoEllipsis = true;
            this.button6.BackColor = System.Drawing.Color.Orange;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.Location = new System.Drawing.Point(106, 85);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(90, 70);
            this.button6.TabIndex = 115;
            this.button6.TabStop = false;
            this.button6.Text = "5";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button1
            // 
            this.button1.AutoEllipsis = true;
            this.button1.BackColor = System.Drawing.Color.Orange;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(8, 245);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 70);
            this.button1.TabIndex = 108;
            this.button1.TabStop = false;
            this.button1.Text = "0";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button9
            // 
            this.button9.AutoEllipsis = true;
            this.button9.BackColor = System.Drawing.Color.Orange;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button9.Location = new System.Drawing.Point(8, 165);
            this.button9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(90, 70);
            this.button9.TabIndex = 108;
            this.button9.TabStop = false;
            this.button9.Text = "1";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button15
            // 
            this.button15.AutoEllipsis = true;
            this.button15.BackColor = System.Drawing.Color.Orange;
            this.button15.FlatAppearance.BorderSize = 0;
            this.button15.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button15.Location = new System.Drawing.Point(204, 5);
            this.button15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(90, 70);
            this.button15.TabIndex = 107;
            this.button15.TabStop = false;
            this.button15.Text = "9";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button16
            // 
            this.button16.AutoEllipsis = true;
            this.button16.BackColor = System.Drawing.Color.Orange;
            this.button16.FlatAppearance.BorderSize = 0;
            this.button16.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button16.Location = new System.Drawing.Point(8, 85);
            this.button16.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(90, 70);
            this.button16.TabIndex = 109;
            this.button16.TabStop = false;
            this.button16.Text = "4";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button17
            // 
            this.button17.AutoEllipsis = true;
            this.button17.BackColor = System.Drawing.Color.Orange;
            this.button17.FlatAppearance.BorderSize = 0;
            this.button17.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button17.Location = new System.Drawing.Point(106, 5);
            this.button17.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(90, 70);
            this.button17.TabIndex = 111;
            this.button17.TabStop = false;
            this.button17.Text = "8";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button18
            // 
            this.button18.AutoEllipsis = true;
            this.button18.BackColor = System.Drawing.Color.Orange;
            this.button18.FlatAppearance.BorderSize = 0;
            this.button18.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button18.Location = new System.Drawing.Point(8, 5);
            this.button18.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(90, 70);
            this.button18.TabIndex = 110;
            this.button18.TabStop = false;
            this.button18.Text = "7";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // ctx
            // 
            this.ctx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退钟回原排ToolStripMenuItem});
            this.ctx.Name = "ctx";
            this.ctx.Size = new System.Drawing.Size(137, 26);
            // 
            // 退钟回原排ToolStripMenuItem
            // 
            this.退钟回原排ToolStripMenuItem.Name = "退钟回原排ToolStripMenuItem";
            this.退钟回原排ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.退钟回原排ToolStripMenuItem.Text = "退钟回原排";
            this.退钟回原排ToolStripMenuItem.Click += new System.EventHandler(this.退钟回原排ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "技师面板";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomViewForm_KeyDown);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            this.sp.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.SplitGender.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.ctx.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label msg;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox techId;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRoom;
        private System.Windows.Forms.Button btnTechList;
        private System.Windows.Forms.Button btnReceived;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label clock;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnTech;
        private System.Windows.Forms.ContextMenuStrip ctx;
        private System.Windows.Forms.ToolStripMenuItem 退钟回原排ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer SplitGender;
    }
}

