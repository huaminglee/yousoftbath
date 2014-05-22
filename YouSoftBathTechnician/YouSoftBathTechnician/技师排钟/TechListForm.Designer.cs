namespace YouSoftBathTechnician
{
    partial class TechListForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechListForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolReArrange = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.employeePanel = new System.Windows.Forms.SplitContainer();
            this.seatTypeTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DgvFemale = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnUpFemale = new System.Windows.Forms.Button();
            this.BtnDownFemale = new System.Windows.Forms.Button();
            this.BtnRearrangeFemale = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.BtnDownMale = new System.Windows.Forms.Button();
            this.BtnRearrangeMale = new System.Windows.Forms.Button();
            this.BtnUpMale = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DgvMale = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            this.employeePanel.Panel1.SuspendLayout();
            this.employeePanel.Panel2.SuspendLayout();
            this.employeePanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvFemale)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMale)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripLabel1,
            this.toolReArrange,
            this.toolStripLabel4,
            this.printTool,
            this.toolStripLabel6,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1200, 95);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(119, 92);
            this.toolStripButton3.Text = "排钟设置(F4)";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel1.Text = "  ";
            // 
            // toolReArrange
            // 
            this.toolReArrange.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolReArrange.Image = ((System.Drawing.Image)(resources.GetObject("toolReArrange.Image")));
            this.toolReArrange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolReArrange.Name = "toolReArrange";
            this.toolReArrange.Size = new System.Drawing.Size(119, 92);
            this.toolReArrange.Text = "全部重排(F4)";
            this.toolReArrange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolReArrange.Click += new System.EventHandler(this.toolReArrange_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel4.Text = "  ";
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(119, 92);
            this.printTool.Text = "打印清单(F5)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel6.Text = "  ";
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
            // employeePanel
            // 
            this.employeePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.employeePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeePanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.employeePanel.IsSplitterFixed = true;
            this.employeePanel.Location = new System.Drawing.Point(0, 95);
            this.employeePanel.Name = "employeePanel";
            // 
            // employeePanel.Panel1
            // 
            this.employeePanel.Panel1.Controls.Add(this.seatTypeTree);
            // 
            // employeePanel.Panel2
            // 
            this.employeePanel.Panel2.Controls.Add(this.splitContainer1);
            this.employeePanel.Size = new System.Drawing.Size(1200, 398);
            this.employeePanel.SplitterDistance = 200;
            this.employeePanel.SplitterWidth = 5;
            this.employeePanel.TabIndex = 4;
            // 
            // seatTypeTree
            // 
            this.seatTypeTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seatTypeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.seatTypeTree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.seatTypeTree.ImageIndex = 0;
            this.seatTypeTree.ImageList = this.imageList1;
            this.seatTypeTree.Location = new System.Drawing.Point(0, 0);
            this.seatTypeTree.Name = "seatTypeTree";
            this.seatTypeTree.SelectedImageIndex = 0;
            this.seatTypeTree.Size = new System.Drawing.Size(196, 394);
            this.seatTypeTree.TabIndex = 0;
            this.seatTypeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.seatTypeTree_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "book_bookmark.png");
            this.imageList1.Images.SetKeyName(1, "book_open_bookmark.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(995, 398);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.BtnDownFemale);
            this.splitContainer2.Panel1.Controls.Add(this.BtnRearrangeFemale);
            this.splitContainer2.Panel1.Controls.Add(this.BtnUpFemale);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(491, 398);
            this.splitContainer2.SplitterDistance = 80;
            this.splitContainer2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DgvFemale);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 310);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "女技师";
            // 
            // DgvFemale
            // 
            this.DgvFemale.AllowUserToAddRows = false;
            this.DgvFemale.AllowUserToDeleteRows = false;
            this.DgvFemale.AllowUserToResizeColumns = false;
            this.DgvFemale.AllowUserToResizeRows = false;
            this.DgvFemale.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvFemale.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DgvFemale.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvFemale.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DgvFemale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvFemale.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvFemale.DefaultCellStyle = dataGridViewCellStyle2;
            this.DgvFemale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvFemale.Location = new System.Drawing.Point(3, 22);
            this.DgvFemale.Name = "DgvFemale";
            this.DgvFemale.ReadOnly = true;
            this.DgvFemale.RowHeadersWidth = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvFemale.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.DgvFemale.RowTemplate.Height = 23;
            this.DgvFemale.Size = new System.Drawing.Size(481, 285);
            this.DgvFemale.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "技师号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "姓名";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // BtnUpFemale
            // 
            this.BtnUpFemale.AutoSize = true;
            this.BtnUpFemale.Location = new System.Drawing.Point(192, 15);
            this.BtnUpFemale.Name = "BtnUpFemale";
            this.BtnUpFemale.Size = new System.Drawing.Size(107, 50);
            this.BtnUpFemale.TabIndex = 0;
            this.BtnUpFemale.Text = "向上";
            this.BtnUpFemale.UseVisualStyleBackColor = true;
            this.BtnUpFemale.Click += new System.EventHandler(this.BtnUpFemale_Click);
            // 
            // BtnDownFemale
            // 
            this.BtnDownFemale.AutoSize = true;
            this.BtnDownFemale.Location = new System.Drawing.Point(305, 15);
            this.BtnDownFemale.Name = "BtnDownFemale";
            this.BtnDownFemale.Size = new System.Drawing.Size(107, 50);
            this.BtnDownFemale.TabIndex = 0;
            this.BtnDownFemale.Text = "向下";
            this.BtnDownFemale.UseVisualStyleBackColor = true;
            this.BtnDownFemale.Click += new System.EventHandler(this.BtnDownFemale_Click);
            // 
            // BtnRearrangeFemale
            // 
            this.BtnRearrangeFemale.AutoSize = true;
            this.BtnRearrangeFemale.Location = new System.Drawing.Point(79, 15);
            this.BtnRearrangeFemale.Name = "BtnRearrangeFemale";
            this.BtnRearrangeFemale.Size = new System.Drawing.Size(107, 50);
            this.BtnRearrangeFemale.TabIndex = 0;
            this.BtnRearrangeFemale.Text = "重排";
            this.BtnRearrangeFemale.UseVisualStyleBackColor = true;
            this.BtnRearrangeFemale.Click += new System.EventHandler(this.BtnRearrangeFemale_Click);
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
            this.splitContainer3.Panel1.Controls.Add(this.BtnDownMale);
            this.splitContainer3.Panel1.Controls.Add(this.BtnRearrangeMale);
            this.splitContainer3.Panel1.Controls.Add(this.BtnUpMale);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer3.Size = new System.Drawing.Size(500, 398);
            this.splitContainer3.SplitterDistance = 80;
            this.splitContainer3.TabIndex = 1;
            // 
            // BtnDownMale
            // 
            this.BtnDownMale.AutoSize = true;
            this.BtnDownMale.Location = new System.Drawing.Point(308, 15);
            this.BtnDownMale.Name = "BtnDownMale";
            this.BtnDownMale.Size = new System.Drawing.Size(107, 50);
            this.BtnDownMale.TabIndex = 0;
            this.BtnDownMale.Text = "向下";
            this.BtnDownMale.UseVisualStyleBackColor = true;
            this.BtnDownMale.Click += new System.EventHandler(this.BtnDownMale_Click);
            // 
            // BtnRearrangeMale
            // 
            this.BtnRearrangeMale.AutoSize = true;
            this.BtnRearrangeMale.Location = new System.Drawing.Point(82, 15);
            this.BtnRearrangeMale.Name = "BtnRearrangeMale";
            this.BtnRearrangeMale.Size = new System.Drawing.Size(107, 50);
            this.BtnRearrangeMale.TabIndex = 0;
            this.BtnRearrangeMale.Text = "重排";
            this.BtnRearrangeMale.UseVisualStyleBackColor = true;
            this.BtnRearrangeMale.Click += new System.EventHandler(this.BtnRearrangeMale_Click);
            // 
            // BtnUpMale
            // 
            this.BtnUpMale.AutoSize = true;
            this.BtnUpMale.Location = new System.Drawing.Point(195, 15);
            this.BtnUpMale.Name = "BtnUpMale";
            this.BtnUpMale.Size = new System.Drawing.Size(107, 50);
            this.BtnUpMale.TabIndex = 0;
            this.BtnUpMale.Text = "向上";
            this.BtnUpMale.UseVisualStyleBackColor = true;
            this.BtnUpMale.Click += new System.EventHandler(this.BtnUpMale_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DgvMale);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(496, 310);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "男技师";
            // 
            // DgvMale
            // 
            this.DgvMale.AllowUserToAddRows = false;
            this.DgvMale.AllowUserToDeleteRows = false;
            this.DgvMale.AllowUserToResizeColumns = false;
            this.DgvMale.AllowUserToResizeRows = false;
            this.DgvMale.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DgvMale.BackgroundColor = System.Drawing.SystemColors.Window;
            this.DgvMale.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DgvMale.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DgvMale.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMale.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DgvMale.DefaultCellStyle = dataGridViewCellStyle5;
            this.DgvMale.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DgvMale.Location = new System.Drawing.Point(3, 22);
            this.DgvMale.Name = "DgvMale";
            this.DgvMale.ReadOnly = true;
            this.DgvMale.RowHeadersWidth = 20;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DgvMale.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DgvMale.RowTemplate.Height = 23;
            this.DgvMale.Size = new System.Drawing.Size(490, 285);
            this.DgvMale.TabIndex = 4;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "技师号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "姓名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // TechListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 493);
            this.Controls.Add(this.employeePanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "TechListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "技师排钟";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SeatTypeManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.employeePanel.Panel1.ResumeLayout(false);
            this.employeePanel.Panel2.ResumeLayout(false);
            this.employeePanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvFemale)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgvMale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.SplitContainer employeePanel;
        private System.Windows.Forms.TreeView seatTypeTree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton toolReArrange;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DgvFemale;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button BtnDownFemale;
        private System.Windows.Forms.Button BtnRearrangeFemale;
        private System.Windows.Forms.Button BtnUpFemale;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button BtnDownMale;
        private System.Windows.Forms.Button BtnRearrangeMale;
        private System.Windows.Forms.Button BtnUpMale;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView DgvMale;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    }
}