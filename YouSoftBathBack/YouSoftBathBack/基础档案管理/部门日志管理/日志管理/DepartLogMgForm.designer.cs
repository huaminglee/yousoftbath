namespace YouSoftBathBack
{
    partial class DepartLogMgForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepartLogMgForm));
            this.employeePanel = new System.Windows.Forms.SplitContainer();
            this.jobTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addDepartTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.delOfficer = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.editOfficer = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.ToolNewPost = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.PanelLogs = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnFind = new System.Windows.Forms.Button();
            this.DtpEt = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.DtpSt = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.employeePanel.Panel1.SuspendLayout();
            this.employeePanel.Panel2.SuspendLayout();
            this.employeePanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // employeePanel
            // 
            this.employeePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.employeePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeePanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.employeePanel.IsSplitterFixed = true;
            this.employeePanel.Location = new System.Drawing.Point(0, 92);
            this.employeePanel.Name = "employeePanel";
            // 
            // employeePanel.Panel1
            // 
            this.employeePanel.Panel1.Controls.Add(this.jobTree);
            // 
            // employeePanel.Panel2
            // 
            this.employeePanel.Panel2.AutoScroll = true;
            this.employeePanel.Panel2.BackColor = System.Drawing.Color.White;
            this.employeePanel.Panel2.Controls.Add(this.PanelLogs);
            this.employeePanel.Panel2.Controls.Add(this.panel1);
            this.employeePanel.Size = new System.Drawing.Size(1370, 428);
            this.employeePanel.SplitterDistance = 200;
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
            this.jobTree.Size = new System.Drawing.Size(196, 424);
            this.jobTree.TabIndex = 0;
            this.jobTree.TabStop = false;
            this.jobTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.jobTree_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "group.png");
            this.imageList1.Images.SetKeyName(1, "Office-Client-Male-Light-icon.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDepartTool,
            this.toolStripLabel1,
            this.delOfficer,
            this.toolStripLabel2,
            this.editOfficer,
            this.toolStripSeparator2,
            this.ToolNewPost,
            this.toolStripLabel5,
            this.exportTool,
            this.toolStripLabel3,
            this.printTool,
            this.toolStripLabel4,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1370, 92);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addDepartTool
            // 
            this.addDepartTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.addDepartTool.Image = ((System.Drawing.Image)(resources.GetObject("addDepartTool.Image")));
            this.addDepartTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addDepartTool.Name = "addDepartTool";
            this.addDepartTool.Size = new System.Drawing.Size(106, 89);
            this.addDepartTool.Text = "新增部门(F1)";
            this.addDepartTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addDepartTool.Click += new System.EventHandler(this.addOfficer_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel1.Text = "  ";
            // 
            // delOfficer
            // 
            this.delOfficer.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.delOfficer.Image = ((System.Drawing.Image)(resources.GetObject("delOfficer.Image")));
            this.delOfficer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delOfficer.Name = "delOfficer";
            this.delOfficer.Size = new System.Drawing.Size(106, 89);
            this.delOfficer.Text = "删除部门(F2)";
            this.delOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delOfficer.Click += new System.EventHandler(this.delOfficer_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel2.Text = "  ";
            // 
            // editOfficer
            // 
            this.editOfficer.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.editOfficer.Image = ((System.Drawing.Image)(resources.GetObject("editOfficer.Image")));
            this.editOfficer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editOfficer.Name = "editOfficer";
            this.editOfficer.Size = new System.Drawing.Size(106, 89);
            this.editOfficer.Text = "编辑部门(F3)";
            this.editOfficer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editOfficer.Click += new System.EventHandler(this.editOfficer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 92);
            // 
            // exportTool
            // 
            this.exportTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.exportTool.Image = ((System.Drawing.Image)(resources.GetObject("exportTool.Image")));
            this.exportTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportTool.Name = "exportTool";
            this.exportTool.Size = new System.Drawing.Size(74, 89);
            this.exportTool.Text = "导出(F4)";
            this.exportTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportTool.Click += new System.EventHandler(this.exportTool_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel3.Text = "  ";
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(74, 89);
            this.printTool.Text = "打印(F5)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printTool.Click += new System.EventHandler(this.printTool_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel4.Text = "  ";
            // 
            // toolExit
            // 
            this.toolExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(85, 89);
            this.toolExit.Text = "退出(ESC)";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // ToolNewPost
            // 
            this.ToolNewPost.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.ToolNewPost.Image = ((System.Drawing.Image)(resources.GetObject("ToolNewPost.Image")));
            this.ToolNewPost.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolNewPost.Name = "ToolNewPost";
            this.ToolNewPost.Size = new System.Drawing.Size(106, 89);
            this.ToolNewPost.Text = "发表新帖(F1)";
            this.ToolNewPost.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolNewPost.Click += new System.EventHandler(this.ToolNewPost_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(16, 89);
            this.toolStripLabel5.Text = "  ";
            // 
            // PanelLogs
            // 
            this.PanelLogs.AutoScroll = true;
            this.PanelLogs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PanelLogs.BackgroundImage")));
            this.PanelLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelLogs.Location = new System.Drawing.Point(0, 47);
            this.PanelLogs.Name = "PanelLogs";
            this.PanelLogs.Size = new System.Drawing.Size(1161, 377);
            this.PanelLogs.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.BtnFind);
            this.panel1.Controls.Add(this.DtpEt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DtpSt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1161, 47);
            this.panel1.TabIndex = 2;
            // 
            // BtnFind
            // 
            this.BtnFind.AutoSize = true;
            this.BtnFind.Location = new System.Drawing.Point(488, 10);
            this.BtnFind.Name = "BtnFind";
            this.BtnFind.Size = new System.Drawing.Size(75, 26);
            this.BtnFind.TabIndex = 2;
            this.BtnFind.Text = "查询";
            this.BtnFind.UseVisualStyleBackColor = true;
            this.BtnFind.Click += new System.EventHandler(this.BtnFind_Click);
            // 
            // DtpEt
            // 
            this.DtpEt.Location = new System.Drawing.Point(345, 10);
            this.DtpEt.Name = "DtpEt";
            this.DtpEt.Size = new System.Drawing.Size(125, 26);
            this.DtpEt.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(273, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "截止时间";
            // 
            // DtpSt
            // 
            this.DtpSt.Location = new System.Drawing.Point(103, 10);
            this.DtpSt.Name = "DtpSt";
            this.DtpSt.Size = new System.Drawing.Size(125, 26);
            this.DtpSt.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始时间";
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1161, 2);
            this.label3.TabIndex = 3;
            // 
            // DepartLogMgForm
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
            this.Name = "DepartLogMgForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "部门日志管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EmployeeManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.employeePanel.Panel1.ResumeLayout(false);
            this.employeePanel.Panel2.ResumeLayout(false);
            this.employeePanel.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer employeePanel;
        private System.Windows.Forms.TreeView jobTree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addDepartTool;
        private System.Windows.Forms.ToolStripButton delOfficer;
        private System.Windows.Forms.ToolStripButton editOfficer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton exportTool;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton ToolNewPost;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.Panel PanelLogs;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnFind;
        private System.Windows.Forms.DateTimePicker DtpEt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker DtpSt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}