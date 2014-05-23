namespace IntereekBathWeChat
{
    partial class CouponManagementForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponManagementForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ToolAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.ToolDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.ToolEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.ToolPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.ToolExit = new System.Windows.Forms.ToolStripButton();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolAdd,
            this.toolStripLabel3,
            this.ToolDel,
            this.toolStripLabel4,
            this.ToolEdit,
            this.toolStripSeparator2,
            this.ToolExport,
            this.toolStripLabel5,
            this.ToolPrint,
            this.toolStripLabel6,
            this.ToolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1187, 95);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ToolAdd
            // 
            this.ToolAdd.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ToolAdd.Image = ((System.Drawing.Image)(resources.GetObject("ToolAdd.Image")));
            this.ToolAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolAdd.Name = "ToolAdd";
            this.ToolAdd.Size = new System.Drawing.Size(68, 92);
            this.ToolAdd.Text = "新增";
            this.ToolAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolAdd.Click += new System.EventHandler(this.ToolAdd_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel3.Text = "  ";
            // 
            // ToolDel
            // 
            this.ToolDel.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ToolDel.Image = ((System.Drawing.Image)(resources.GetObject("ToolDel.Image")));
            this.ToolDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolDel.Name = "ToolDel";
            this.ToolDel.Size = new System.Drawing.Size(68, 92);
            this.ToolDel.Text = "停用";
            this.ToolDel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolDel.Click += new System.EventHandler(this.ToolDel_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel4.Text = "  ";
            // 
            // ToolEdit
            // 
            this.ToolEdit.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ToolEdit.Image = ((System.Drawing.Image)(resources.GetObject("ToolEdit.Image")));
            this.ToolEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolEdit.Name = "ToolEdit";
            this.ToolEdit.Size = new System.Drawing.Size(68, 92);
            this.ToolEdit.Text = "编辑";
            this.ToolEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolEdit.Click += new System.EventHandler(this.ToolEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 95);
            // 
            // ToolExport
            // 
            this.ToolExport.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ToolExport.Image = ((System.Drawing.Image)(resources.GetObject("ToolExport.Image")));
            this.ToolExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExport.Name = "ToolExport";
            this.ToolExport.Size = new System.Drawing.Size(86, 92);
            this.ToolExport.Text = "导出清单";
            this.ToolExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolExport.Click += new System.EventHandler(this.ToolExport_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel5.Text = "  ";
            // 
            // ToolPrint
            // 
            this.ToolPrint.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ToolPrint.Image = ((System.Drawing.Image)(resources.GetObject("ToolPrint.Image")));
            this.ToolPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolPrint.Name = "ToolPrint";
            this.ToolPrint.Size = new System.Drawing.Size(86, 92);
            this.ToolPrint.Text = "打印清单";
            this.ToolPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolPrint.Click += new System.EventHandler(this.ToolPrint_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel6.Text = "  ";
            // 
            // ToolExit
            // 
            this.ToolExit.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.ToolExit.Image = ((System.Drawing.Image)(resources.GetObject("ToolExit.Image")));
            this.ToolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolExit.Name = "ToolExit";
            this.ToolExit.Size = new System.Drawing.Size(68, 92);
            this.ToolExit.Text = "退出";
            this.ToolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ToolExit.Click += new System.EventHandler(this.ToolExit_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column3,
            this.Column5});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 95);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1187, 538);
            this.dgv.TabIndex = 5;
            this.dgv.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellMouseUp);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "金额";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "描述";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "是否停用";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CouponManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(1187, 633);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponManagementForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连客科技微信平台";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ToolAdd;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton ToolDel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripButton ToolEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ToolExport;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton ToolPrint;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton ToolExit;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;







    }
}

