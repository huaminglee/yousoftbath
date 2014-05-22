namespace YouSoftBathBack
{
    partial class ExpenseTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExpenseTableForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addType = new System.Windows.Forms.ToolStripButton();
            this.delType = new System.Windows.Forms.ToolStripButton();
            this.editType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addTool = new System.Windows.Forms.ToolStripButton();
            this.delTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTool = new System.Windows.Forms.ToolStripButton();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.employeePanel = new System.Windows.Forms.SplitContainer();
            this.typeTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.monthBox = new System.Windows.Forms.ComboBox();
            this.yearBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.transactor = new System.Windows.Forms.ComboBox();
            this.cTransactor = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.employeePanel.Panel1.SuspendLayout();
            this.employeePanel.Panel2.SuspendLayout();
            this.employeePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addType,
            this.delType,
            this.editType,
            this.toolStripSeparator3,
            this.addTool,
            this.delTool,
            this.toolStripSeparator2,
            this.exportTool,
            this.printTool,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1362, 95);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addType
            // 
            this.addType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.addType.Image = ((System.Drawing.Image)(resources.GetObject("addType.Image")));
            this.addType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addType.Name = "addType";
            this.addType.Size = new System.Drawing.Size(119, 92);
            this.addType.Text = "新增类别(F1)";
            this.addType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addType.Click += new System.EventHandler(this.addType_Click);
            // 
            // delType
            // 
            this.delType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delType.Image = ((System.Drawing.Image)(resources.GetObject("delType.Image")));
            this.delType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delType.Name = "delType";
            this.delType.Size = new System.Drawing.Size(119, 92);
            this.delType.Text = "删除类别(F2)";
            this.delType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delType.Click += new System.EventHandler(this.delType_Click);
            // 
            // editType
            // 
            this.editType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.editType.Image = ((System.Drawing.Image)(resources.GetObject("editType.Image")));
            this.editType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editType.Name = "editType";
            this.editType.Size = new System.Drawing.Size(119, 92);
            this.editType.Text = "编辑类别(F3)";
            this.editType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editType.Click += new System.EventHandler(this.editType_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 95);
            // 
            // addTool
            // 
            this.addTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.addTool.Image = ((System.Drawing.Image)(resources.GetObject("addTool.Image")));
            this.addTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTool.Name = "addTool";
            this.addTool.Size = new System.Drawing.Size(119, 92);
            this.addTool.Text = "新建支出(F6)";
            this.addTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addTool.Click += new System.EventHandler(this.addTool_Click);
            // 
            // delTool
            // 
            this.delTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delTool.Image = ((System.Drawing.Image)(resources.GetObject("delTool.Image")));
            this.delTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delTool.Name = "delTool";
            this.delTool.Size = new System.Drawing.Size(119, 92);
            this.delTool.Text = "删除支出(F7)";
            this.delTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delTool.Click += new System.EventHandler(this.delTool_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 95);
            // 
            // exportTool
            // 
            this.exportTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.exportTool.Image = ((System.Drawing.Image)(resources.GetObject("exportTool.Image")));
            this.exportTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportTool.Name = "exportTool";
            this.exportTool.Size = new System.Drawing.Size(83, 92);
            this.exportTool.Text = "导出(F4)";
            this.exportTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportTool.Click += new System.EventHandler(this.exportTool_Click);
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(83, 92);
            this.printTool.Text = "打印(F5)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printTool.Click += new System.EventHandler(this.printTool_Click);
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
            this.employeePanel.Panel1.Controls.Add(this.typeTree);
            // 
            // employeePanel.Panel2
            // 
            this.employeePanel.Panel2.Controls.Add(this.dgv);
            this.employeePanel.Panel2.Controls.Add(this.panel2);
            this.employeePanel.Panel2.Controls.Add(this.panel1);
            this.employeePanel.Size = new System.Drawing.Size(1362, 647);
            this.employeePanel.SplitterDistance = 249;
            this.employeePanel.SplitterWidth = 5;
            this.employeePanel.TabIndex = 4;
            // 
            // typeTree
            // 
            this.typeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeTree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.typeTree.ImageIndex = 0;
            this.typeTree.ImageList = this.imageList1;
            this.typeTree.Location = new System.Drawing.Point(0, 0);
            this.typeTree.Name = "typeTree";
            this.typeTree.SelectedImageIndex = 0;
            this.typeTree.Size = new System.Drawing.Size(245, 643);
            this.typeTree.TabIndex = 0;
            this.typeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.typeTree_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "book_bookmark.png");
            this.imageList1.Images.SetKeyName(1, "book_open_bookmark.png");
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 43);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 20;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1104, 543);
            this.dgv.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tMoney);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 586);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1104, 57);
            this.panel2.TabIndex = 14;
            // 
            // tMoney
            // 
            this.tMoney.AutoSize = true;
            this.tMoney.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tMoney.ForeColor = System.Drawing.Color.Red;
            this.tMoney.Location = new System.Drawing.Point(149, 15);
            this.tMoney.Name = "tMoney";
            this.tMoney.Size = new System.Drawing.Size(27, 27);
            this.tMoney.TabIndex = 0;
            this.tMoney.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(15, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "支出共计:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.monthBox);
            this.panel1.Controls.Add(this.yearBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.transactor);
            this.panel1.Controls.Add(this.cTransactor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1104, 43);
            this.panel1.TabIndex = 8;
            // 
            // monthBox
            // 
            this.monthBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthBox.FormattingEnabled = true;
            this.monthBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.monthBox.Location = new System.Drawing.Point(334, 10);
            this.monthBox.Name = "monthBox";
            this.monthBox.Size = new System.Drawing.Size(121, 25);
            this.monthBox.TabIndex = 16;
            this.monthBox.SelectedIndexChanged += new System.EventHandler(this.timeBox_SelectedIndexChanged);
            // 
            // yearBox
            // 
            this.yearBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearBox.FormattingEnabled = true;
            this.yearBox.Location = new System.Drawing.Point(104, 10);
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(121, 25);
            this.yearBox.TabIndex = 15;
            this.yearBox.SelectedIndexChanged += new System.EventHandler(this.timeBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "选择月份";
            // 
            // transactor
            // 
            this.transactor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transactor.Enabled = false;
            this.transactor.FormattingEnabled = true;
            this.transactor.Location = new System.Drawing.Point(633, 9);
            this.transactor.Name = "transactor";
            this.transactor.Size = new System.Drawing.Size(127, 25);
            this.transactor.TabIndex = 12;
            // 
            // cTransactor
            // 
            this.cTransactor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cTransactor.AutoSize = true;
            this.cTransactor.Location = new System.Drawing.Point(552, 10);
            this.cTransactor.Name = "cTransactor";
            this.cTransactor.Size = new System.Drawing.Size(81, 22);
            this.cTransactor.TabIndex = 8;
            this.cTransactor.Text = "经手人";
            this.cTransactor.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择年份";
            // 
            // ExpenseTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 742);
            this.Controls.Add(this.employeePanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "ExpenseTableForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "支出统计";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ExpenseTableForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.employeePanel.Panel1.ResumeLayout(false);
            this.employeePanel.Panel2.ResumeLayout(false);
            this.employeePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addType;
        private System.Windows.Forms.ToolStripButton delType;
        private System.Windows.Forms.ToolStripButton editType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addTool;
        private System.Windows.Forms.ToolStripButton delTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportTool;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.SplitContainer employeePanel;
        private System.Windows.Forms.TreeView typeTree;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox transactor;
        private System.Windows.Forms.CheckBox cTransactor;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label tMoney;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox monthBox;
        private System.Windows.Forms.ComboBox yearBox;
        private System.Windows.Forms.Label label2;
    }
}