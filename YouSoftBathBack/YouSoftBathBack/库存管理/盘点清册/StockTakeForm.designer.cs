﻿namespace YouSoftBathBack
{
    partial class StockTakeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTakeForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.stockTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolTakeDone = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportGoods = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitTool = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 92);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.stockTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv);
            this.splitContainer1.Size = new System.Drawing.Size(1088, 419);
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
            this.stockTree.Size = new System.Drawing.Size(196, 415);
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
            this.Column9,
            this.Column2,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column1,
            this.Column3,
            this.Column8});
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
            this.dgv.Size = new System.Drawing.Size(880, 415);
            this.dgv.TabIndex = 2;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "仓库";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 61;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 61;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "上月结存";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 75;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "本月进货";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 75;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "本月出货";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 75;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "销售消耗";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 75;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "本月盘点损耗";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 89;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "现有库存";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 75;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolTakeDone,
            this.toolStripSeparator2,
            this.exportGoods,
            this.toolStripLabel1,
            this.printTool,
            this.toolStripSeparator1,
            this.exitTool});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1088, 92);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolTakeDone
            // 
            this.toolTakeDone.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolTakeDone.Image = ((System.Drawing.Image)(resources.GetObject("toolTakeDone.Image")));
            this.toolTakeDone.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolTakeDone.Name = "toolTakeDone";
            this.toolTakeDone.Size = new System.Drawing.Size(106, 89);
            this.toolTakeDone.Text = "完成盘点(F3)";
            this.toolTakeDone.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTakeDone.Click += new System.EventHandler(this.toolTakeDone_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 92);
            // 
            // exportGoods
            // 
            this.exportGoods.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.exportGoods.Image = ((System.Drawing.Image)(resources.GetObject("exportGoods.Image")));
            this.exportGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportGoods.Name = "exportGoods";
            this.exportGoods.Size = new System.Drawing.Size(106, 89);
            this.exportGoods.Text = "导出清单(F4)";
            this.exportGoods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportGoods.Click += new System.EventHandler(this.exportGoods_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel1.Text = "  ";
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(106, 89);
            this.printTool.Text = "打印清单(F5)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printTool.Click += new System.EventHandler(this.printTool_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 92);
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
            // StockTakeForm
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
            this.Name = "StockTakeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "库存信息明细";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MenuManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
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
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolTakeDone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
    }
}