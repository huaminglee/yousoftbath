namespace YouSoftBathBack
{
    partial class StorageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StorageForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.stockTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnFind = new System.Windows.Forms.Button();
            this.TextName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.addSeatType = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.delSeatType = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.editSeatType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addGoods = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.delGoods = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.editGoods = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportGoods = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitTool = new System.Windows.Forms.ToolStripButton();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 95);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.stockTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1088, 416);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 9;
            // 
            // stockTree
            // 
            this.stockTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stockTree.ImageIndex = 0;
            this.stockTree.ImageList = this.imageList1;
            this.stockTree.Location = new System.Drawing.Point(0, 0);
            this.stockTree.Name = "stockTree";
            this.stockTree.SelectedImageIndex = 0;
            this.stockTree.Size = new System.Drawing.Size(196, 412);
            this.stockTree.TabIndex = 2;
            this.stockTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.catTree_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "book_bookmark.png");
            this.imageList1.Images.SetKeyName(1, "book_open_bookmark.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 54);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(880, 358);
            this.panel2.TabIndex = 1;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2,
            this.Column5,
            this.Column4,
            this.Column6});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 20;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(880, 358);
            this.dgv.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnFind);
            this.panel1.Controls.Add(this.TextName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 54);
            this.panel1.TabIndex = 0;
            // 
            // BtnFind
            // 
            this.BtnFind.AutoSize = true;
            this.BtnFind.Location = new System.Drawing.Point(322, 14);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(119, 26);
            this.BtnFind.TabIndex = 2;
            this.BtnFind.Text = "查找";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(98, 14);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(218, 26);
            this.TextName.TabIndex = 1;
            this.TextName.Enter += new System.EventHandler(this.TextName_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "商品名称";
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addSeatType,
            this.toolStripLabel2,
            this.delSeatType,
            this.toolStripLabel3,
            this.editSeatType,
            this.toolStripSeparator3,
            this.addGoods,
            this.toolStripLabel4,
            this.delGoods,
            this.toolStripLabel5,
            this.editGoods,
            this.toolStripSeparator2,
            this.exportGoods,
            this.toolStripLabel1,
            this.printTool,
            this.toolStripSeparator1,
            this.exitTool});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1088, 95);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // addSeatType
            // 
            this.addSeatType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.addSeatType.Image = ((System.Drawing.Image)(resources.GetObject("addSeatType.Image")));
            this.addSeatType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addSeatType.Name = "addSeatType";
            this.addSeatType.Size = new System.Drawing.Size(119, 92);
            this.addSeatType.Text = "新增类别(F1)";
            this.addSeatType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addSeatType.Click += new System.EventHandler(this.addSeatType_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel2.Text = "  ";
            // 
            // delSeatType
            // 
            this.delSeatType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delSeatType.Image = ((System.Drawing.Image)(resources.GetObject("delSeatType.Image")));
            this.delSeatType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delSeatType.Name = "delSeatType";
            this.delSeatType.Size = new System.Drawing.Size(119, 92);
            this.delSeatType.Text = "删除类别(F2)";
            this.delSeatType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delSeatType.Click += new System.EventHandler(this.delSeatType_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel3.Text = "  ";
            // 
            // editSeatType
            // 
            this.editSeatType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.editSeatType.Image = ((System.Drawing.Image)(resources.GetObject("editSeatType.Image")));
            this.editSeatType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editSeatType.Name = "editSeatType";
            this.editSeatType.Size = new System.Drawing.Size(119, 92);
            this.editSeatType.Text = "编辑类别(F3)";
            this.editSeatType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editSeatType.Click += new System.EventHandler(this.editSeatType_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 95);
            // 
            // addGoods
            // 
            this.addGoods.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.addGoods.Image = ((System.Drawing.Image)(resources.GetObject("addGoods.Image")));
            this.addGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addGoods.Name = "addGoods";
            this.addGoods.Size = new System.Drawing.Size(119, 92);
            this.addGoods.Text = "新增商品(F4)";
            this.addGoods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addGoods.Click += new System.EventHandler(this.addGoods_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel4.Text = "  ";
            // 
            // delGoods
            // 
            this.delGoods.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delGoods.Image = ((System.Drawing.Image)(resources.GetObject("delGoods.Image")));
            this.delGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delGoods.Name = "delGoods";
            this.delGoods.Size = new System.Drawing.Size(119, 92);
            this.delGoods.Text = "删除商品(F5)";
            this.delGoods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delGoods.Click += new System.EventHandler(this.delGoods_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel5.Text = "  ";
            // 
            // editGoods
            // 
            this.editGoods.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.editGoods.Image = ((System.Drawing.Image)(resources.GetObject("editGoods.Image")));
            this.editGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editGoods.Name = "editGoods";
            this.editGoods.Size = new System.Drawing.Size(119, 92);
            this.editGoods.Text = "编辑商品(F6)";
            this.editGoods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editGoods.Click += new System.EventHandler(this.editGoods_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 95);
            // 
            // exportGoods
            // 
            this.exportGoods.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.exportGoods.Image = ((System.Drawing.Image)(resources.GetObject("exportGoods.Image")));
            this.exportGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportGoods.Name = "exportGoods";
            this.exportGoods.Size = new System.Drawing.Size(106, 92);
            this.exportGoods.Text = "导出清单(F7)";
            this.exportGoods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportGoods.Click += new System.EventHandler(this.exportGoods_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel1.Text = "  ";
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(106, 92);
            this.printTool.Text = "打印清单(F8)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printTool.Click += new System.EventHandler(this.printTool_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 95);
            // 
            // exitTool
            // 
            this.exitTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.exitTool.Image = ((System.Drawing.Image)(resources.GetObject("exitTool.Image")));
            this.exitTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitTool.Name = "exitTool";
            this.exitTool.Size = new System.Drawing.Size(85, 89);
            this.exitTool.Text = "退出(ESC)";
            this.exitTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exitTool.Click += new System.EventHandler(this.exitTool_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "名称";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 65;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "现有库存";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 97;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "补货标准";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 97;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "类别";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 65;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "单位";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 65;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "备注";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 65;
            // 
            // StorageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 511);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip2);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "StorageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "库存信息明细";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MenuManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton exportGoods;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton exitTool;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.TreeView stockTree;
        private System.Windows.Forms.ToolStripButton addSeatType;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton delSeatType;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton editSeatType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addGoods;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton delGoods;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton editGoods;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnFind;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}