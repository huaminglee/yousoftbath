namespace YouSoftBathBack
{
    partial class EmployeeManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeManagementForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.employeePanel = new System.Windows.Forms.SplitContainer();
            this.jobTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dgv = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addOfficer = new System.Windows.Forms.ToolStripButton();
            this.delOfficer = new System.Windows.Forms.ToolStripButton();
            this.editOfficer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.addTool = new System.Windows.Forms.ToolStripButton();
            this.delTool = new System.Windows.Forms.ToolStripButton();
            this.editTool = new System.Windows.Forms.ToolStripButton();
            this.authoTool = new System.Windows.Forms.ToolStripButton();
            this.modifyPwdTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTool = new System.Windows.Forms.ToolStripButton();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.employeePanel.Panel1.SuspendLayout();
            this.employeePanel.Panel2.SuspendLayout();
            this.employeePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // employeePanel
            // 
            this.employeePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.employeePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeePanel.Location = new System.Drawing.Point(0, 95);
            this.employeePanel.Name = "employeePanel";
            // 
            // employeePanel.Panel1
            // 
            this.employeePanel.Panel1.Controls.Add(this.jobTree);
            // 
            // employeePanel.Panel2
            // 
            this.employeePanel.Panel2.Controls.Add(this.dgv);
            this.employeePanel.Size = new System.Drawing.Size(1370, 425);
            this.employeePanel.SplitterDistance = 314;
            this.employeePanel.SplitterWidth = 5;
            this.employeePanel.TabIndex = 3;
            // 
            // jobTree
            // 
            this.jobTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.jobTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.jobTree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.jobTree.ImageIndex = 0;
            this.jobTree.ImageList = this.imageList1;
            this.jobTree.Location = new System.Drawing.Point(0, 0);
            this.jobTree.Name = "jobTree";
            this.jobTree.SelectedImageIndex = 0;
            this.jobTree.Size = new System.Drawing.Size(310, 421);
            this.jobTree.TabIndex = 0;
            this.jobTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.jobTree_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.png");
            this.imageList1.Images.SetKeyName(1, "Office-Client-Male-Light-icon.png");
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1047, 421);
            this.dgv.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addOfficer,
            this.delOfficer,
            this.editOfficer,
            this.toolStripSeparator3,
            this.addTool,
            this.delTool,
            this.editTool,
            this.authoTool,
            this.modifyPwdTool,
            this.toolStripSeparator2,
            this.exportTool,
            this.printTool,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 95);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addOfficer
            // 
            this.addOfficer.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.addOfficer.Image = ((System.Drawing.Image)(resources.GetObject("addOfficer.Image")));
            this.addOfficer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addOfficer.Name = "addOfficer";
            this.addOfficer.Size = new System.Drawing.Size(119, 92);
            this.addOfficer.Text = "新增职位(F1)";
            this.addOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addOfficer.Click += new System.EventHandler(this.addOfficer_Click);
            // 
            // delOfficer
            // 
            this.delOfficer.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delOfficer.Image = ((System.Drawing.Image)(resources.GetObject("delOfficer.Image")));
            this.delOfficer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delOfficer.Name = "delOfficer";
            this.delOfficer.Size = new System.Drawing.Size(119, 92);
            this.delOfficer.Text = "删除职位(F2)";
            this.delOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delOfficer.Click += new System.EventHandler(this.delOfficer_Click);
            // 
            // editOfficer
            // 
            this.editOfficer.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.editOfficer.Image = ((System.Drawing.Image)(resources.GetObject("editOfficer.Image")));
            this.editOfficer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editOfficer.Name = "editOfficer";
            this.editOfficer.Size = new System.Drawing.Size(119, 92);
            this.editOfficer.Text = "编辑职位(F3)";
            this.editOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editOfficer.Click += new System.EventHandler(this.editOfficer_Click);
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
            this.addTool.Text = "新增员工(F6)";
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
            this.delTool.Text = "删除员工(F7)";
            this.delTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delTool.Click += new System.EventHandler(this.delTool_Click);
            // 
            // editTool
            // 
            this.editTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.editTool.Image = ((System.Drawing.Image)(resources.GetObject("editTool.Image")));
            this.editTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTool.Name = "editTool";
            this.editTool.Size = new System.Drawing.Size(119, 92);
            this.editTool.Text = "编辑员工(F8)";
            this.editTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editTool.Click += new System.EventHandler(this.editTool_Click);
            // 
            // authoTool
            // 
            this.authoTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.authoTool.Image = ((System.Drawing.Image)(resources.GetObject("authoTool.Image")));
            this.authoTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.authoTool.Name = "authoTool";
            this.authoTool.Size = new System.Drawing.Size(119, 92);
            this.authoTool.Text = "员工权限(F9)";
            this.authoTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.authoTool.Click += new System.EventHandler(this.authoTool_Click);
            // 
            // modifyPwdTool
            // 
            this.modifyPwdTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.modifyPwdTool.Image = ((System.Drawing.Image)(resources.GetObject("modifyPwdTool.Image")));
            this.modifyPwdTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modifyPwdTool.Name = "modifyPwdTool";
            this.modifyPwdTool.Size = new System.Drawing.Size(130, 92);
            this.modifyPwdTool.Text = "修改密码(F10)";
            this.modifyPwdTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.modifyPwdTool.Click += new System.EventHandler(this.modifyPwdTool_Click);
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
            // EmployeeManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 520);
            this.Controls.Add(this.employeePanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "EmployeeManagementForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "员工管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EmployeeManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.employeePanel.Panel1.ResumeLayout(false);
            this.employeePanel.Panel2.ResumeLayout(false);
            this.employeePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer employeePanel;
        private System.Windows.Forms.TreeView jobTree;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addOfficer;
        private System.Windows.Forms.ToolStripButton delOfficer;
        private System.Windows.Forms.ToolStripButton editOfficer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton addTool;
        private System.Windows.Forms.ToolStripButton delTool;
        private System.Windows.Forms.ToolStripButton editTool;
        private System.Windows.Forms.ToolStripButton authoTool;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportTool;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton modifyPwdTool;
        private System.Windows.Forms.ToolStripButton toolExit;
    }
}