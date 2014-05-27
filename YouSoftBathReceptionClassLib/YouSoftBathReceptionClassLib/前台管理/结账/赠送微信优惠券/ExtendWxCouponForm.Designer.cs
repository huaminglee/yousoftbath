namespace YouSoftBathReception
{
    partial class ExtendWxCouponForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtendWxCouponForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SP = new System.Windows.Forms.SplitContainer();
            this.BTFind = new System.Windows.Forms.Button();
            this.TextNick = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.BTCancel = new System.Windows.Forms.Button();
            this.BTExtend = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvExtend = new System.Windows.Forms.DataGridView();
            this.BTDel = new System.Windows.Forms.Button();
            this.BTAdd = new System.Windows.Forms.Button();
            this.TextNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SP.Panel1.SuspendLayout();
            this.SP.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtend)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Orange;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(715, 790);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 0;
            // 
            // SP
            // 
            this.SP.BackColor = System.Drawing.Color.Orange;
            this.SP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SP.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SP.IsSplitterFixed = true;
            this.SP.Location = new System.Drawing.Point(0, 0);
            this.SP.Name = "SP";
            this.SP.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SP.Panel1
            // 
            this.SP.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.SP.Panel1.Controls.Add(this.BTFind);
            this.SP.Panel1.Controls.Add(this.TextNick);
            // 
            // SP.Panel2
            // 
            this.SP.Panel2.AutoScroll = true;
            this.SP.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.SP.Size = new System.Drawing.Size(290, 790);
            this.SP.SplitterDistance = 60;
            this.SP.TabIndex = 1;
            // 
            // BTFind
            // 
            this.BTFind.AutoSize = true;
            this.BTFind.BackColor = System.Drawing.Color.Orange;
            this.BTFind.Location = new System.Drawing.Point(202, 12);
            this.BTFind.Name = "BTFind";
            this.BTFind.Size = new System.Drawing.Size(75, 32);
            this.BTFind.TabIndex = 3;
            this.BTFind.TabStop = false;
            this.BTFind.Text = "查询";
            this.BTFind.UseVisualStyleBackColor = false;
            this.BTFind.Click += new System.EventHandler(this.BTFind_Click);
            // 
            // TextNick
            // 
            this.TextNick.BackColor = System.Drawing.Color.Orange;
            this.TextNick.Location = new System.Drawing.Point(10, 12);
            this.TextNick.Name = "TextNick";
            this.TextNick.Size = new System.Drawing.Size(184, 32);
            this.TextNick.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Orange;
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
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer2.Panel1.Controls.Add(this.BTDel);
            this.splitContainer2.Panel1.Controls.Add(this.BTAdd);
            this.splitContainer2.Panel1.Controls.Add(this.TextNumber);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.dgvExtend);
            this.splitContainer2.Panel2.Controls.Add(this.panel1);
            this.splitContainer2.Panel2.Controls.Add(this.dgv);
            this.splitContainer2.Size = new System.Drawing.Size(421, 790);
            this.splitContainer2.SplitterDistance = 60;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // BTCancel
            // 
            this.BTCancel.AutoSize = true;
            this.BTCancel.BackColor = System.Drawing.Color.Orange;
            this.BTCancel.Location = new System.Drawing.Point(230, 13);
            this.BTCancel.Name = "BTCancel";
            this.BTCancel.Size = new System.Drawing.Size(133, 41);
            this.BTCancel.TabIndex = 4;
            this.BTCancel.TabStop = false;
            this.BTCancel.Text = "取消";
            this.BTCancel.UseVisualStyleBackColor = false;
            this.BTCancel.Click += new System.EventHandler(this.BTCancel_Click);
            // 
            // BTExtend
            // 
            this.BTExtend.AutoSize = true;
            this.BTExtend.BackColor = System.Drawing.Color.Orange;
            this.BTExtend.Location = new System.Drawing.Point(49, 13);
            this.BTExtend.Name = "BTExtend";
            this.BTExtend.Size = new System.Drawing.Size(133, 41);
            this.BTExtend.TabIndex = 4;
            this.BTExtend.TabStop = false;
            this.BTExtend.Text = "赠送";
            this.BTExtend.UseVisualStyleBackColor = false;
            this.BTExtend.Click += new System.EventHandler(this.BTExtend_Click);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.Column3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(417, 333);
            this.dgv.TabIndex = 0;
            this.dgv.TabStop = false;
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
            // Column3
            // 
            this.Column3.HeaderText = "金额";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.BTCancel);
            this.panel1.Controls.Add(this.BTExtend);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 333);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 71);
            this.panel1.TabIndex = 1;
            // 
            // dgvExtend
            // 
            this.dgvExtend.AllowUserToAddRows = false;
            this.dgvExtend.AllowUserToDeleteRows = false;
            this.dgvExtend.AllowUserToResizeColumns = false;
            this.dgvExtend.AllowUserToResizeRows = false;
            this.dgvExtend.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvExtend.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.dgvExtend.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvExtend.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvExtend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExtend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.dataGridViewTextBoxColumn1,
            this.Column5,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvExtend.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExtend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExtend.Location = new System.Drawing.Point(0, 404);
            this.dgvExtend.Name = "dgvExtend";
            this.dgvExtend.ReadOnly = true;
            this.dgvExtend.RowHeadersVisible = false;
            this.dgvExtend.RowTemplate.Height = 23;
            this.dgvExtend.Size = new System.Drawing.Size(417, 318);
            this.dgvExtend.TabIndex = 2;
            this.dgvExtend.TabStop = false;
            // 
            // BTDel
            // 
            this.BTDel.AutoSize = true;
            this.BTDel.BackColor = System.Drawing.Color.Orange;
            this.BTDel.Location = new System.Drawing.Point(316, 12);
            this.BTDel.Name = "BTDel";
            this.BTDel.Size = new System.Drawing.Size(75, 32);
            this.BTDel.TabIndex = 8;
            this.BTDel.TabStop = false;
            this.BTDel.Text = "删除";
            this.BTDel.UseVisualStyleBackColor = false;
            this.BTDel.Click += new System.EventHandler(this.BTDel_Click);
            // 
            // BTAdd
            // 
            this.BTAdd.AutoSize = true;
            this.BTAdd.BackColor = System.Drawing.Color.Orange;
            this.BTAdd.Location = new System.Drawing.Point(235, 12);
            this.BTAdd.Name = "BTAdd";
            this.BTAdd.Size = new System.Drawing.Size(75, 32);
            this.BTAdd.TabIndex = 9;
            this.BTAdd.TabStop = false;
            this.BTAdd.Text = "添加";
            this.BTAdd.UseVisualStyleBackColor = false;
            this.BTAdd.Click += new System.EventHandler(this.BTAdd_Click);
            // 
            // TextNumber
            // 
            this.TextNumber.BackColor = System.Drawing.Color.Orange;
            this.TextNumber.Location = new System.Drawing.Point(129, 12);
            this.TextNumber.Name = "TextNumber";
            this.TextNumber.Size = new System.Drawing.Size(99, 32);
            this.TextNumber.TabIndex = 7;
            this.TextNumber.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "赠送数量";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "openid";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "对象昵称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "couponid";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "数量";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // ExtendWxCouponForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(715, 790);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendWxCouponForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "赠送微信优惠券";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ExtendWxCouponForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.SP.Panel1.ResumeLayout(false);
            this.SP.Panel1.PerformLayout();
            this.SP.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExtend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer SP;
        private System.Windows.Forms.Button BTFind;
        private System.Windows.Forms.TextBox TextNick;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button BTExtend;
        private System.Windows.Forms.Button BTCancel;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvExtend;
        private System.Windows.Forms.Button BTDel;
        private System.Windows.Forms.Button BTAdd;
        private System.Windows.Forms.TextBox TextNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;






    }
}

