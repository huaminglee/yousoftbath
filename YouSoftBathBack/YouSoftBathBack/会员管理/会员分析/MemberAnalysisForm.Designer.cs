namespace YouSoftBathBack
{
    partial class MemberAnalysisForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberAnalysisForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.findTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolSms = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.exportTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnSendOneMsg = new System.Windows.Forms.Button();
            this.BtnPos = new System.Windows.Forms.Button();
            this.TextMsgStart = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.month = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timesMin = new System.Windows.Forms.TextBox();
            this.totalMin = new System.Windows.Forms.TextBox();
            this.averageMin = new System.Windows.Forms.TextBox();
            this.timesMax = new System.Windows.Forms.TextBox();
            this.totalMax = new System.Windows.Forms.TextBox();
            this.averageMax = new System.Windows.Forms.TextBox();
            this.btnSmsSend = new System.Windows.Forms.Button();
            this.msg = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findTool,
            this.toolStripLabel4,
            this.toolSms,
            this.toolStripLabel1,
            this.exportTool,
            this.toolStripLabel3,
            this.printTool,
            this.toolStripLabel2,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1370, 95);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // findTool
            // 
            this.findTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.findTool.Image = ((System.Drawing.Image)(resources.GetObject("findTool.Image")));
            this.findTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findTool.Name = "findTool";
            this.findTool.Size = new System.Drawing.Size(93, 92);
            this.findTool.Text = "查  询(F3)";
            this.findTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.findTool.Click += new System.EventHandler(this.findTool_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel4.Text = "  ";
            // 
            // toolSms
            // 
            this.toolSms.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolSms.Image = ((System.Drawing.Image)(resources.GetObject("toolSms.Image")));
            this.toolSms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolSms.Name = "toolSms";
            this.toolSms.Size = new System.Drawing.Size(119, 92);
            this.toolSms.Text = "短信设置(F6)";
            this.toolSms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSms.Click += new System.EventHandler(this.toolSms_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel1.Text = "  ";
            // 
            // exportTool
            // 
            this.exportTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.exportTool.Image = ((System.Drawing.Image)(resources.GetObject("exportTool.Image")));
            this.exportTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportTool.Name = "exportTool";
            this.exportTool.Size = new System.Drawing.Size(93, 92);
            this.exportTool.Text = "导  出(F4)";
            this.exportTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportTool.Click += new System.EventHandler(this.exportTool_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel3.Text = "  ";
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(93, 92);
            this.printTool.Text = "打  印(F5)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printTool.Click += new System.EventHandler(this.printTool_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel2.Text = "  ";
            // 
            // toolExit
            // 
            this.toolExit.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(104, 92);
            this.toolExit.Text = "退  出(ESC)";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column11,
            this.Column3,
            this.Column5,
            this.Column4,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 258);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1370, 394);
            this.dgv.TabIndex = 18;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "会员卡号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 105;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "会员名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 105;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "电话号码";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 105;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "会员类型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 105;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "优惠方案";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 105;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "卡状态";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 87;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "卡余额";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 87;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "累计消费";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 105;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "销售人员";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 105;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "开户日期";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 105;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "最后使用";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 105;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.BtnSendOneMsg);
            this.panel1.Controls.Add(this.BtnPos);
            this.panel1.Controls.Add(this.TextMsgStart);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnSmsSend);
            this.panel1.Controls.Add(this.msg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 163);
            this.panel1.TabIndex = 17;
            // 
            // BtnSendOneMsg
            // 
            this.BtnSendOneMsg.AutoSize = true;
            this.BtnSendOneMsg.Location = new System.Drawing.Point(1232, 129);
            this.BtnSendOneMsg.Name = "BtnSendOneMsg";
            this.BtnSendOneMsg.Size = new System.Drawing.Size(126, 28);
            this.BtnSendOneMsg.TabIndex = 20;
            this.BtnSendOneMsg.Text = "发送单条短信";
            this.BtnSendOneMsg.UseVisualStyleBackColor = true;
            this.BtnSendOneMsg.Click += new System.EventHandler(this.BtnSendOneMsg_Click);
            // 
            // BtnPos
            // 
            this.BtnPos.AutoSize = true;
            this.BtnPos.Location = new System.Drawing.Point(1125, 129);
            this.BtnPos.Name = "BtnPos";
            this.BtnPos.Size = new System.Drawing.Size(101, 28);
            this.BtnPos.TabIndex = 20;
            this.BtnPos.Text = "定位";
            this.BtnPos.UseVisualStyleBackColor = true;
            this.BtnPos.Click += new System.EventHandler(this.BtnPos_Click);
            // 
            // TextMsgStart
            // 
            this.TextMsgStart.Location = new System.Drawing.Point(976, 130);
            this.TextMsgStart.Name = "TextMsgStart";
            this.TextMsgStart.Size = new System.Drawing.Size(143, 27);
            this.TextMsgStart.TabIndex = 19;
            this.TextMsgStart.Text = "1";
            this.TextMsgStart.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txts_KeyPress);
            this.TextMsgStart.Enter += new System.EventHandler(this.TextMsgStart_Enter);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(854, 134);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 18);
            this.label17.TabIndex = 18;
            this.label17.Text = "指定起始位置";
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Dock = System.Windows.Forms.DockStyle.Top;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(1370, 2);
            this.label16.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(770, 149);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.month);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(764, 46);
            this.panel3.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "个月无消费会员";
            // 
            // month
            // 
            this.month.ImeMode = System.Windows.Forms.ImeMode.On;
            this.month.Location = new System.Drawing.Point(59, 10);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(95, 27);
            this.month.TabIndex = 16;
            this.month.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.month.Enter += new System.EventHandler(this.month_Enter);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 15;
            this.label1.Text = "最近";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.timesMin);
            this.panel2.Controls.Add(this.totalMin);
            this.panel2.Controls.Add(this.averageMin);
            this.panel2.Controls.Add(this.timesMax);
            this.panel2.Controls.Add(this.totalMax);
            this.panel2.Controls.Add(this.averageMax);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(764, 77);
            this.panel2.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(764, 2);
            this.label11.TabIndex = 30;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(582, 48);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(44, 18);
            this.label15.TabIndex = 27;
            this.label15.Text = "小于";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(328, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 18);
            this.label10.TabIndex = 27;
            this.label10.Text = "小于";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(86, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 26;
            this.label6.Text = "小于";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(510, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 18);
            this.label14.TabIndex = 29;
            this.label14.Text = "消费次数大于";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(274, 11);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 18);
            this.label9.TabIndex = 29;
            this.label9.Text = "总消费大于";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 18);
            this.label3.TabIndex = 28;
            this.label3.Text = "平均消费大于";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(723, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 18);
            this.label13.TabIndex = 23;
            this.label13.Text = "次";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(469, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 18);
            this.label8.TabIndex = 23;
            this.label8.Text = "元";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(237, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "元";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(723, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 18);
            this.label12.TabIndex = 21;
            this.label12.Text = "次";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(469, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "元";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "元";
            // 
            // timesMin
            // 
            this.timesMin.ImeMode = System.Windows.Forms.ImeMode.On;
            this.timesMin.Location = new System.Drawing.Point(627, 44);
            this.timesMin.Name = "timesMin";
            this.timesMin.Size = new System.Drawing.Size(95, 27);
            this.timesMin.TabIndex = 17;
            this.timesMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.timesMin.Enter += new System.EventHandler(this.month_Enter);
            // 
            // totalMin
            // 
            this.totalMin.ImeMode = System.Windows.Forms.ImeMode.On;
            this.totalMin.Location = new System.Drawing.Point(373, 44);
            this.totalMin.Name = "totalMin";
            this.totalMin.Size = new System.Drawing.Size(95, 27);
            this.totalMin.TabIndex = 17;
            this.totalMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.totalMin.Enter += new System.EventHandler(this.month_Enter);
            // 
            // averageMin
            // 
            this.averageMin.ImeMode = System.Windows.Forms.ImeMode.On;
            this.averageMin.Location = new System.Drawing.Point(136, 44);
            this.averageMin.Name = "averageMin";
            this.averageMin.Size = new System.Drawing.Size(95, 27);
            this.averageMin.TabIndex = 16;
            this.averageMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.averageMin.Enter += new System.EventHandler(this.month_Enter);
            // 
            // timesMax
            // 
            this.timesMax.ImeMode = System.Windows.Forms.ImeMode.On;
            this.timesMax.Location = new System.Drawing.Point(627, 7);
            this.timesMax.Name = "timesMax";
            this.timesMax.Size = new System.Drawing.Size(95, 27);
            this.timesMax.TabIndex = 19;
            this.timesMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.timesMax.Enter += new System.EventHandler(this.month_Enter);
            // 
            // totalMax
            // 
            this.totalMax.ImeMode = System.Windows.Forms.ImeMode.On;
            this.totalMax.Location = new System.Drawing.Point(373, 7);
            this.totalMax.Name = "totalMax";
            this.totalMax.Size = new System.Drawing.Size(95, 27);
            this.totalMax.TabIndex = 19;
            this.totalMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.totalMax.Enter += new System.EventHandler(this.month_Enter);
            // 
            // averageMax
            // 
            this.averageMax.ImeMode = System.Windows.Forms.ImeMode.On;
            this.averageMax.Location = new System.Drawing.Point(136, 7);
            this.averageMax.Name = "averageMax";
            this.averageMax.Size = new System.Drawing.Size(95, 27);
            this.averageMax.TabIndex = 18;
            this.averageMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            this.averageMax.Enter += new System.EventHandler(this.month_Enter);
            // 
            // btnSmsSend
            // 
            this.btnSmsSend.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSmsSend.Location = new System.Drawing.Point(812, 8);
            this.btnSmsSend.Name = "btnSmsSend";
            this.btnSmsSend.Size = new System.Drawing.Size(36, 146);
            this.btnSmsSend.TabIndex = 9;
            this.btnSmsSend.Text = "发送短信";
            this.btnSmsSend.UseVisualStyleBackColor = true;
            this.btnSmsSend.Click += new System.EventHandler(this.btnSmsSend_Click);
            // 
            // msg
            // 
            this.msg.Location = new System.Drawing.Point(854, 8);
            this.msg.Multiline = true;
            this.msg.Name = "msg";
            this.msg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.msg.Size = new System.Drawing.Size(504, 111);
            this.msg.TabIndex = 8;
            this.msg.Enter += new System.EventHandler(this.msg_Enter);
            // 
            // MemberAnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 652);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "MemberAnalysisForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "会员分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MemberAnalysisForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemberAnalysisForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton findTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton exportTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox msg;
        private System.Windows.Forms.Button btnSmsSend;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton toolSms;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox totalMin;
        private System.Windows.Forms.TextBox averageMin;
        private System.Windows.Forms.TextBox totalMax;
        private System.Windows.Forms.TextBox averageMax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox month;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox timesMin;
        private System.Windows.Forms.TextBox timesMax;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox TextMsgStart;
        private System.Windows.Forms.Button BtnPos;
        private System.Windows.Forms.Button BtnSendOneMsg;
    }
}