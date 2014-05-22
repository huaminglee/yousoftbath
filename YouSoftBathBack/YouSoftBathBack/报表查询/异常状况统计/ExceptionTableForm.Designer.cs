namespace YouSoftBathBack
{
    partial class ExceptionTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionTableForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.findTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.exportTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rMemberSameDay = new System.Windows.Forms.RadioButton();
            this.rCancelOpen = new System.Windows.Forms.RadioButton();
            this.rTransfer = new System.Windows.Forms.RadioButton();
            this.rUnlock = new System.Windows.Forms.RadioButton();
            this.rRepay = new System.Windows.Forms.RadioButton();
            this.rMember = new System.Windows.Forms.RadioButton();
            this.pn = new System.Windows.Forms.Panel();
            this.oper = new System.Windows.Forms.TextBox();
            this.cOper = new System.Windows.Forms.CheckBox();
            this.end = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.pTimes = new System.Windows.Forms.Panel();
            this.times = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.dgvAct = new System.Windows.Forms.DataGridView();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pn.SuspendLayout();
            this.pTimes.SuspendLayout();
            this.sp.Panel1.SuspendLayout();
            this.sp.Panel2.SuspendLayout();
            this.sp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAct)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findTool,
            this.toolStripLabel1,
            this.exportTool,
            this.toolStripLabel3,
            this.printTool,
            this.toolStripLabel2,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1078, 95);
            this.toolStrip1.TabIndex = 5;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rMemberSameDay);
            this.panel1.Controls.Add(this.rCancelOpen);
            this.panel1.Controls.Add(this.rTransfer);
            this.panel1.Controls.Add(this.rUnlock);
            this.panel1.Controls.Add(this.rRepay);
            this.panel1.Controls.Add(this.rMember);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 68);
            this.panel1.TabIndex = 7;
            // 
            // rMemberSameDay
            // 
            this.rMemberSameDay.AutoSize = true;
            this.rMemberSameDay.Location = new System.Drawing.Point(11, 31);
            this.rMemberSameDay.Name = "rMemberSameDay";
            this.rMemberSameDay.Size = new System.Drawing.Size(278, 22);
            this.rMemberSameDay.TabIndex = 0;
            this.rMemberSameDay.Text = "每天刷卡次数超过一次的会员卡";
            this.rMemberSameDay.UseVisualStyleBackColor = true;
            this.rMemberSameDay.CheckedChanged += new System.EventHandler(this.rMemberSameDay_CheckedChanged);
            // 
            // rCancelOpen
            // 
            this.rCancelOpen.AutoSize = true;
            this.rCancelOpen.Location = new System.Drawing.Point(385, 31);
            this.rCancelOpen.Name = "rCancelOpen";
            this.rCancelOpen.Size = new System.Drawing.Size(134, 22);
            this.rCancelOpen.TabIndex = 0;
            this.rCancelOpen.Text = "取消开台统计";
            this.rCancelOpen.UseVisualStyleBackColor = true;
            this.rCancelOpen.CheckedChanged += new System.EventHandler(this.rCancelOpen_CheckedChanged);
            // 
            // rTransfer
            // 
            this.rTransfer.AutoSize = true;
            this.rTransfer.Location = new System.Drawing.Point(536, 31);
            this.rTransfer.Name = "rTransfer";
            this.rTransfer.Size = new System.Drawing.Size(98, 22);
            this.rTransfer.TabIndex = 0;
            this.rTransfer.Text = "转账统计";
            this.rTransfer.UseVisualStyleBackColor = true;
            this.rTransfer.CheckedChanged += new System.EventHandler(this.rUnlock_CheckedChanged);
            // 
            // rUnlock
            // 
            this.rUnlock.AutoSize = true;
            this.rUnlock.Location = new System.Drawing.Point(536, 3);
            this.rUnlock.Name = "rUnlock";
            this.rUnlock.Size = new System.Drawing.Size(98, 22);
            this.rUnlock.TabIndex = 0;
            this.rUnlock.Text = "解锁统计";
            this.rUnlock.UseVisualStyleBackColor = true;
            this.rUnlock.CheckedChanged += new System.EventHandler(this.rUnlock_CheckedChanged);
            // 
            // rRepay
            // 
            this.rRepay.AutoSize = true;
            this.rRepay.Location = new System.Drawing.Point(385, 3);
            this.rRepay.Name = "rRepay";
            this.rRepay.Size = new System.Drawing.Size(134, 22);
            this.rRepay.TabIndex = 0;
            this.rRepay.Text = "重新结账统计";
            this.rRepay.UseVisualStyleBackColor = true;
            this.rRepay.CheckedChanged += new System.EventHandler(this.rRepay_CheckedChanged);
            // 
            // rMember
            // 
            this.rMember.AutoSize = true;
            this.rMember.Checked = true;
            this.rMember.Location = new System.Drawing.Point(11, 3);
            this.rMember.Name = "rMember";
            this.rMember.Size = new System.Drawing.Size(368, 22);
            this.rMember.TabIndex = 0;
            this.rMember.TabStop = true;
            this.rMember.Text = "第一次到最后一次刷会员卡，显示同一工号";
            this.rMember.UseVisualStyleBackColor = true;
            this.rMember.CheckedChanged += new System.EventHandler(this.rMember_CheckedChanged);
            // 
            // pn
            // 
            this.pn.BackColor = System.Drawing.SystemColors.Control;
            this.pn.Controls.Add(this.oper);
            this.pn.Controls.Add(this.cOper);
            this.pn.Controls.Add(this.end);
            this.pn.Controls.Add(this.label2);
            this.pn.Controls.Add(this.start);
            this.pn.Controls.Add(this.label1);
            this.pn.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn.Location = new System.Drawing.Point(0, 163);
            this.pn.Name = "pn";
            this.pn.Size = new System.Drawing.Size(1078, 42);
            this.pn.TabIndex = 8;
            this.pn.Visible = false;
            // 
            // oper
            // 
            this.oper.Enabled = false;
            this.oper.Location = new System.Drawing.Point(609, 8);
            this.oper.Name = "oper";
            this.oper.Size = new System.Drawing.Size(146, 27);
            this.oper.TabIndex = 3;
            this.oper.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.oper_KeyPress);
            this.oper.Enter += new System.EventHandler(this.oper_Enter);
            // 
            // cOper
            // 
            this.cOper.AutoSize = true;
            this.cOper.Location = new System.Drawing.Point(516, 10);
            this.cOper.Name = "cOper";
            this.cOper.Size = new System.Drawing.Size(99, 22);
            this.cOper.TabIndex = 2;
            this.cOper.Text = "操作员工";
            this.cOper.UseVisualStyleBackColor = true;
            this.cOper.CheckedChanged += new System.EventHandler(this.cOper_CheckedChanged);
            // 
            // end
            // 
            this.end.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.end.Location = new System.Drawing.Point(320, 8);
            this.end.Name = "end";
            this.end.Size = new System.Drawing.Size(129, 27);
            this.end.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(239, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束时间";
            // 
            // start
            // 
            this.start.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.start.Location = new System.Drawing.Point(93, 8);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(129, 27);
            this.start.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间";
            // 
            // pTimes
            // 
            this.pTimes.Controls.Add(this.times);
            this.pTimes.Controls.Add(this.label4);
            this.pTimes.Controls.Add(this.label3);
            this.pTimes.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTimes.Location = new System.Drawing.Point(0, 205);
            this.pTimes.Name = "pTimes";
            this.pTimes.Size = new System.Drawing.Size(1078, 42);
            this.pTimes.TabIndex = 9;
            // 
            // times
            // 
            this.times.Location = new System.Drawing.Point(143, 8);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(79, 27);
            this.times.TabIndex = 1;
            this.times.Text = "2";
            this.times.Enter += new System.EventHandler(this.oper_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "次";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "刷卡总次数大于";
            // 
            // sp
            // 
            this.sp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 247);
            this.sp.Name = "sp";
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.dgv);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.Controls.Add(this.dgvAct);
            this.sp.Size = new System.Drawing.Size(1078, 222);
            this.sp.SplitterDistance = 640;
            this.sp.TabIndex = 11;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(636, 218);
            this.dgv.TabIndex = 10;
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            // 
            // dgvAct
            // 
            this.dgvAct.AllowUserToAddRows = false;
            this.dgvAct.AllowUserToDeleteRows = false;
            this.dgvAct.AllowUserToOrderColumns = true;
            this.dgvAct.AllowUserToResizeRows = false;
            this.dgvAct.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAct.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvAct.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAct.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAct.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAct.Location = new System.Drawing.Point(0, 0);
            this.dgvAct.Name = "dgvAct";
            this.dgvAct.ReadOnly = true;
            this.dgvAct.RowHeadersWidth = 20;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvAct.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAct.RowTemplate.Height = 23;
            this.dgvAct.Size = new System.Drawing.Size(430, 218);
            this.dgvAct.TabIndex = 11;
            // 
            // ExceptionTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 469);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.pTimes);
            this.Controls.Add(this.pn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "ExceptionTableForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "异常状况统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ExceptionTableForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExceptionTableForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pn.ResumeLayout(false);
            this.pn.PerformLayout();
            this.pTimes.ResumeLayout(false);
            this.pTimes.PerformLayout();
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel2.ResumeLayout(false);
            this.sp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAct)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rMember;
        private System.Windows.Forms.RadioButton rMemberSameDay;
        private System.Windows.Forms.RadioButton rCancelOpen;
        private System.Windows.Forms.RadioButton rRepay;
        private System.Windows.Forms.RadioButton rUnlock;
        private System.Windows.Forms.Panel pn;
        private System.Windows.Forms.DateTimePicker start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker end;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox oper;
        private System.Windows.Forms.CheckBox cOper;
        private System.Windows.Forms.Panel pTimes;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridView dgvAct;
        private System.Windows.Forms.TextBox times;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rTransfer;
    }
}