namespace IntereekBathWeChat
{
    partial class CouponRecordsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponRecordsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.DPStart = new System.Windows.Forms.DateTimePicker();
            this.BTFind = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DPEnd = new System.Windows.Forms.DateTimePicker();
            this.BTExport = new System.Windows.Forms.Button();
            this.BTPrint = new System.Windows.Forms.Button();
            this.BTExit = new System.Windows.Forms.Button();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.BTExit);
            this.panel1.Controls.Add(this.BTPrint);
            this.panel1.Controls.Add(this.BTExport);
            this.panel1.Controls.Add(this.BTFind);
            this.panel1.Controls.Add(this.DPEnd);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DPStart);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1251, 66);
            this.panel1.TabIndex = 5;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column6,
            this.Column4,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 66);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1251, 567);
            this.dgv.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始时间";
            // 
            // DPStart
            // 
            this.DPStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DPStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DPStart.Location = new System.Drawing.Point(104, 15);
            this.DPStart.Name = "DPStart";
            this.DPStart.Size = new System.Drawing.Size(236, 32);
            this.DPStart.TabIndex = 1;
            // 
            // BTFind
            // 
            this.BTFind.AutoSize = true;
            this.BTFind.BackColor = System.Drawing.Color.Orange;
            this.BTFind.Location = new System.Drawing.Point(691, 9);
            this.BTFind.Name = "BTFind";
            this.BTFind.Size = new System.Drawing.Size(108, 44);
            this.BTFind.TabIndex = 2;
            this.BTFind.Text = "查询";
            this.BTFind.UseVisualStyleBackColor = false;
            this.BTFind.Click += new System.EventHandler(this.BTFind_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(352, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束时间";
            // 
            // DPEnd
            // 
            this.DPEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.DPEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DPEnd.Location = new System.Drawing.Point(446, 15);
            this.DPEnd.Name = "DPEnd";
            this.DPEnd.Size = new System.Drawing.Size(236, 32);
            this.DPEnd.TabIndex = 1;
            // 
            // BTExport
            // 
            this.BTExport.AutoSize = true;
            this.BTExport.BackColor = System.Drawing.Color.Orange;
            this.BTExport.Location = new System.Drawing.Point(843, 9);
            this.BTExport.Name = "BTExport";
            this.BTExport.Size = new System.Drawing.Size(108, 44);
            this.BTExport.TabIndex = 2;
            this.BTExport.Text = "导出清单";
            this.BTExport.UseVisualStyleBackColor = false;
            this.BTExport.Click += new System.EventHandler(this.BTExport_Click);
            // 
            // BTPrint
            // 
            this.BTPrint.AutoSize = true;
            this.BTPrint.BackColor = System.Drawing.Color.Orange;
            this.BTPrint.Location = new System.Drawing.Point(957, 9);
            this.BTPrint.Name = "BTPrint";
            this.BTPrint.Size = new System.Drawing.Size(108, 44);
            this.BTPrint.TabIndex = 2;
            this.BTPrint.Text = "打印";
            this.BTPrint.UseVisualStyleBackColor = false;
            this.BTPrint.Click += new System.EventHandler(this.BTPrint_Click);
            // 
            // BTExit
            // 
            this.BTExit.AutoSize = true;
            this.BTExit.BackColor = System.Drawing.Color.Orange;
            this.BTExit.Location = new System.Drawing.Point(1071, 9);
            this.BTExit.Name = "BTExit";
            this.BTExit.Size = new System.Drawing.Size(108, 44);
            this.BTExit.TabIndex = 2;
            this.BTExit.Text = "退出";
            this.BTExit.UseVisualStyleBackColor = false;
            this.BTExit.Click += new System.EventHandler(this.BTExit_Click);
            // 
            // Column5
            // 
            this.Column5.HeaderText = "编号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 208;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "优惠券编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 209;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 208;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "金额";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 208;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "使用人";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 209;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "使用时间";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 208;
            // 
            // CouponRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(1251, 633);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponRecordsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "优惠券使用记录";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button BTFind;
        private System.Windows.Forms.DateTimePicker DPStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DPEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BTExit;
        private System.Windows.Forms.Button BTPrint;
        private System.Windows.Forms.Button BTExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;







    }
}

