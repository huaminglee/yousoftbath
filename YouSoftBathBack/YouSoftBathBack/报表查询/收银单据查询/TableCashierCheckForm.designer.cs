namespace YouSoftBathBack
{
    partial class TableCashierCheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableCashierCheckForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolFind = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.cboxSeat = new System.Windows.Forms.CheckBox();
            this.seat = new System.Windows.Forms.TextBox();
            this.cboxSystemId = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboxPrint = new System.Windows.Forms.CheckBox();
            this.printTimeList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbBillId = new System.Windows.Forms.CheckBox();
            this.billId = new System.Windows.Forms.TextBox();
            this.systemId = new System.Windows.Forms.TextBox();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvActList = new System.Windows.Forms.DataGridView();
            this.sp2 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvAct = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActList)).BeginInit();
            this.sp2.Panel1.SuspendLayout();
            this.sp2.Panel2.SuspendLayout();
            this.sp2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAct)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolFind,
            this.toolStripLabel2,
            this.toolPrint,
            this.toolStripLabel1,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1344, 95);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolFind
            // 
            this.toolFind.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolFind.Image = ((System.Drawing.Image)(resources.GetObject("toolFind.Image")));
            this.toolFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolFind.Name = "toolFind";
            this.toolFind.Size = new System.Drawing.Size(83, 92);
            this.toolFind.Text = "查询(F3)";
            this.toolFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolFind.Click += new System.EventHandler(this.toolFind_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(24, 92);
            this.toolStripLabel2.Text = "    ";
            // 
            // toolPrint
            // 
            this.toolPrint.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolPrint.Image")));
            this.toolPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolPrint.Name = "toolPrint";
            this.toolPrint.Size = new System.Drawing.Size(83, 92);
            this.toolPrint.Text = "打印(F5)";
            this.toolPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolPrint.Click += new System.EventHandler(this.toolPrint_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(24, 92);
            this.toolStripLabel1.Text = "    ";
            // 
            // toolExit
            // 
            this.toolExit.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(94, 92);
            this.toolExit.Text = "退出(ESC)";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "收银日期";
            // 
            // startTime
            // 
            this.startTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startTime.Location = new System.Drawing.Point(114, 8);
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(138, 27);
            this.startTime.TabIndex = 7;
            this.startTime.ValueChanged += new System.EventHandler(this.startTime_ValueChanged);
            // 
            // cboxSeat
            // 
            this.cboxSeat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxSeat.AutoSize = true;
            this.cboxSeat.Location = new System.Drawing.Point(645, 11);
            this.cboxSeat.Name = "cboxSeat";
            this.cboxSeat.Size = new System.Drawing.Size(81, 22);
            this.cboxSeat.TabIndex = 9;
            this.cboxSeat.Text = "手牌号";
            this.cboxSeat.UseVisualStyleBackColor = true;
            this.cboxSeat.CheckedChanged += new System.EventHandler(this.cboxSeat_CheckedChanged);
            // 
            // seat
            // 
            this.seat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.seat.Enabled = false;
            this.seat.Location = new System.Drawing.Point(719, 8);
            this.seat.Name = "seat";
            this.seat.Size = new System.Drawing.Size(127, 27);
            this.seat.TabIndex = 11;
            this.seat.Enter += new System.EventHandler(this.seat_Enter);
            // 
            // cboxSystemId
            // 
            this.cboxSystemId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cboxSystemId.AutoSize = true;
            this.cboxSystemId.Location = new System.Drawing.Point(860, 10);
            this.cboxSystemId.Name = "cboxSystemId";
            this.cboxSystemId.Size = new System.Drawing.Size(81, 22);
            this.cboxSystemId.TabIndex = 8;
            this.cboxSystemId.Text = "账单号";
            this.cboxSystemId.UseVisualStyleBackColor = true;
            this.cboxSystemId.CheckedChanged += new System.EventHandler(this.cboxSystemId_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.cboxPrint);
            this.panel1.Controls.Add(this.printTimeList);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbBillId);
            this.panel1.Controls.Add(this.cboxSystemId);
            this.panel1.Controls.Add(this.billId);
            this.panel1.Controls.Add(this.systemId);
            this.panel1.Controls.Add(this.seat);
            this.panel1.Controls.Add(this.cboxSeat);
            this.panel1.Controls.Add(this.startTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 43);
            this.panel1.TabIndex = 7;
            // 
            // cboxPrint
            // 
            this.cboxPrint.AutoSize = true;
            this.cboxPrint.Location = new System.Drawing.Point(293, 10);
            this.cboxPrint.Name = "cboxPrint";
            this.cboxPrint.Size = new System.Drawing.Size(99, 22);
            this.cboxPrint.TabIndex = 15;
            this.cboxPrint.Text = "交班时间";
            this.cboxPrint.UseVisualStyleBackColor = true;
            this.cboxPrint.CheckedChanged += new System.EventHandler(this.cboxPrint_CheckedChanged);
            // 
            // printTimeList
            // 
            this.printTimeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.printTimeList.Enabled = false;
            this.printTimeList.FormattingEnabled = true;
            this.printTimeList.Location = new System.Drawing.Point(393, 9);
            this.printTimeList.Name = "printTimeList";
            this.printTimeList.Size = new System.Drawing.Size(225, 25);
            this.printTimeList.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1344, 2);
            this.label3.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1344, 2);
            this.label2.TabIndex = 12;
            // 
            // cbBillId
            // 
            this.cbBillId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBillId.AutoSize = true;
            this.cbBillId.Location = new System.Drawing.Point(1086, 10);
            this.cbBillId.Name = "cbBillId";
            this.cbBillId.Size = new System.Drawing.Size(81, 22);
            this.cbBillId.TabIndex = 8;
            this.cbBillId.Text = "单据号";
            this.cbBillId.UseVisualStyleBackColor = true;
            this.cbBillId.CheckedChanged += new System.EventHandler(this.cbBillId_CheckedChanged);
            // 
            // billId
            // 
            this.billId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.billId.Enabled = false;
            this.billId.Location = new System.Drawing.Point(1167, 8);
            this.billId.Name = "billId";
            this.billId.Size = new System.Drawing.Size(127, 27);
            this.billId.TabIndex = 11;
            this.billId.Enter += new System.EventHandler(this.seat_Enter);
            // 
            // systemId
            // 
            this.systemId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.systemId.Enabled = false;
            this.systemId.Location = new System.Drawing.Point(942, 8);
            this.systemId.Name = "systemId";
            this.systemId.Size = new System.Drawing.Size(127, 27);
            this.systemId.TabIndex = 11;
            this.systemId.Enter += new System.EventHandler(this.seat_Enter);
            // 
            // sp
            // 
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 138);
            this.sp.Name = "sp";
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.groupBox1);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.sp2);
            this.sp.Size = new System.Drawing.Size(1344, 392);
            this.sp.SplitterDistance = 560;
            this.sp.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvActList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 392);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "账单情况列表";
            // 
            // dgvActList
            // 
            this.dgvActList.AllowUserToAddRows = false;
            this.dgvActList.AllowUserToDeleteRows = false;
            this.dgvActList.AllowUserToOrderColumns = true;
            this.dgvActList.AllowUserToResizeRows = false;
            this.dgvActList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvActList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvActList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvActList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvActList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvActList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvActList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActList.Location = new System.Drawing.Point(3, 23);
            this.dgvActList.Name = "dgvActList";
            this.dgvActList.ReadOnly = true;
            this.dgvActList.RowHeadersWidth = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvActList.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvActList.RowTemplate.Height = 23;
            this.dgvActList.Size = new System.Drawing.Size(554, 366);
            this.dgvActList.TabIndex = 7;
            this.dgvActList.CurrentCellChanged += new System.EventHandler(this.dgvActList_CurrentCellChanged);
            // 
            // sp2
            // 
            this.sp2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp2.Location = new System.Drawing.Point(0, 0);
            this.sp2.Name = "sp2";
            this.sp2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp2.Panel1
            // 
            this.sp2.Panel1.Controls.Add(this.groupBox2);
            // 
            // sp2.Panel2
            // 
            this.sp2.Panel2.Controls.Add(this.groupBox3);
            this.sp2.Size = new System.Drawing.Size(780, 392);
            this.sp2.SplitterDistance = 197;
            this.sp2.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvOrders);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(780, 197);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "消费情况列表";
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AllowUserToOrderColumns = true;
            this.dgvOrders.AllowUserToResizeRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvOrders.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvOrders.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOrders.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(3, 23);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.RowHeadersWidth = 20;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvOrders.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvOrders.RowTemplate.Height = 23;
            this.dgvOrders.Size = new System.Drawing.Size(774, 171);
            this.dgvOrders.TabIndex = 7;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvAct);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(780, 191);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "结账情况列表";
            // 
            // dgvAct
            // 
            this.dgvAct.AllowUserToAddRows = false;
            this.dgvAct.AllowUserToDeleteRows = false;
            this.dgvAct.AllowUserToOrderColumns = true;
            this.dgvAct.AllowUserToResizeRows = false;
            this.dgvAct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAct.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAct.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAct.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column6,
            this.Column7,
            this.Column8});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAct.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAct.Location = new System.Drawing.Point(3, 23);
            this.dgvAct.Name = "dgvAct";
            this.dgvAct.ReadOnly = true;
            this.dgvAct.RowHeadersWidth = 20;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAct.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvAct.RowTemplate.Height = 23;
            this.dgvAct.Size = new System.Drawing.Size(774, 165);
            this.dgvAct.TabIndex = 7;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "付款方式";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 105;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "付款金额";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 105;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "会员卡号";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 105;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "免单原因";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 105;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "挂账单位";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 105;
            // 
            // TableCashierCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1344, 530);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "TableCashierCheckForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "收银单据查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TableCashierCheckForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            this.sp.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActList)).EndInit();
            this.sp2.Panel1.ResumeLayout(false);
            this.sp2.Panel2.ResumeLayout(false);
            this.sp2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolFind;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton toolPrint;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.CheckBox cboxSeat;
        private System.Windows.Forms.TextBox seat;
        private System.Windows.Forms.CheckBox cboxSystemId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer sp2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dgvActList;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.DataGridView dgvAct;
        private System.Windows.Forms.TextBox systemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.CheckBox cbBillId;
        private System.Windows.Forms.TextBox billId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox printTimeList;
        private System.Windows.Forms.CheckBox cboxPrint;
    }
}