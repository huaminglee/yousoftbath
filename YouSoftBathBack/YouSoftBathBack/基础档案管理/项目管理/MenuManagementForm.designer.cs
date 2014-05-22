namespace YouSoftBathBack
{
    partial class MenuManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuManagementForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.catTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.addCat = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.delCat = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.editCat = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.delTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.editGoods = new System.Windows.Forms.ToolStripButton();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 92);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.catTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv);
            this.splitContainer1.Size = new System.Drawing.Size(1088, 419);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 9;
            // 
            // catTree
            // 
            this.catTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.catTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.catTree.ImageIndex = 0;
            this.catTree.ImageList = this.imageList1;
            this.catTree.Location = new System.Drawing.Point(0, 0);
            this.catTree.Name = "catTree";
            this.catTree.SelectedImageIndex = 0;
            this.catTree.Size = new System.Drawing.Size(231, 415);
            this.catTree.TabIndex = 0;
            this.catTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.catTree_AfterSelect);
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
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersWidth = 20;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(845, 415);
            this.dgv.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addCat,
            this.toolStripLabel2,
            this.delCat,
            this.toolStripLabel3,
            this.editCat,
            this.toolStripSeparator3,
            this.addTool,
            this.toolStripLabel4,
            this.delTool,
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
            this.toolStrip2.Size = new System.Drawing.Size(1088, 92);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // addCat
            // 
            this.addCat.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.addCat.Image = ((System.Drawing.Image)(resources.GetObject("addCat.Image")));
            this.addCat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addCat.Name = "addCat";
            this.addCat.Size = new System.Drawing.Size(106, 89);
            this.addCat.Text = "新增类别(F1)";
            this.addCat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addCat.Click += new System.EventHandler(this.addCat_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel2.Text = "  ";
            // 
            // delCat
            // 
            this.delCat.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.delCat.Image = ((System.Drawing.Image)(resources.GetObject("delCat.Image")));
            this.delCat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delCat.Name = "delCat";
            this.delCat.Size = new System.Drawing.Size(106, 89);
            this.delCat.Text = "删除类别(F2)";
            this.delCat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delCat.Click += new System.EventHandler(this.delCat_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel3.Text = "  ";
            // 
            // editCat
            // 
            this.editCat.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.editCat.Image = ((System.Drawing.Image)(resources.GetObject("editCat.Image")));
            this.editCat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editCat.Name = "editCat";
            this.editCat.Size = new System.Drawing.Size(106, 89);
            this.editCat.Text = "编辑类别(F3)";
            this.editCat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editCat.Click += new System.EventHandler(this.editCat_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 92);
            // 
            // addTool
            // 
            this.addTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.addTool.Image = ((System.Drawing.Image)(resources.GetObject("addTool.Image")));
            this.addTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addTool.Name = "addTool";
            this.addTool.Size = new System.Drawing.Size(106, 89);
            this.addTool.Text = "新增项目(F6)";
            this.addTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addTool.Click += new System.EventHandler(this.addTool_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel4.Text = "  ";
            // 
            // delTool
            // 
            this.delTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.delTool.Image = ((System.Drawing.Image)(resources.GetObject("delTool.Image")));
            this.delTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delTool.Name = "delTool";
            this.delTool.Size = new System.Drawing.Size(106, 89);
            this.delTool.Text = "删除项目(F7)";
            this.delTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delTool.Click += new System.EventHandler(this.delTool_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel5.Text = "  ";
            // 
            // editGoods
            // 
            this.editGoods.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.editGoods.Image = ((System.Drawing.Image)(resources.GetObject("editGoods.Image")));
            this.editGoods.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editGoods.Name = "editGoods";
            this.editGoods.Size = new System.Drawing.Size(106, 89);
            this.editGoods.Text = "编辑项目(F8)";
            this.editGoods.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editGoods.Click += new System.EventHandler(this.editGoods_Click);
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
            // MenuManagementForm
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
            this.Name = "MenuManagementForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "菜谱管理";
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
        private System.Windows.Forms.TreeView catTree;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton addCat;
        private System.Windows.Forms.ToolStripButton delCat;
        private System.Windows.Forms.ToolStripButton editCat;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addTool;
        private System.Windows.Forms.ToolStripButton delTool;
        private System.Windows.Forms.ToolStripButton editGoods;
        private System.Windows.Forms.ToolStripButton exportGoods;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton exitTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    }
}