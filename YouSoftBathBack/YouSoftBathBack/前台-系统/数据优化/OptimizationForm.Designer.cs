namespace YouSoftBathBack
{
    partial class OptimizationForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cl = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.endTime = new System.Windows.Forms.DateTimePicker();
            this.startTime = new System.Windows.Forms.DateTimePicker();
            this.month = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rmonth = new System.Windows.Forms.RadioButton();
            this.rtime = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.month)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.endTime);
            this.groupBox1.Controls.Add(this.startTime);
            this.groupBox1.Controls.Add(this.month);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rmonth);
            this.groupBox1.Controls.Add(this.rtime);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 452);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "优化条件";
            // 
            // cl
            // 
            this.cl.CheckOnClick = true;
            this.cl.FormattingEnabled = true;
            this.cl.Items.AddRange(new object[] {
            "账单记录",
            "吧台消息记录",
            "售卡充卡记录",
            "前台打印时间记录",
            "优惠券设置记录",
            "团购记录",
            "支出记录",
            "订单记录",
            "异常操作记录",
            "摄像头录像消息记录",
            "催钟记录",
            "房间预警记录",
            "鞋吧消息记录",
            "技师消息记录",
            "技师预约记录",
            "技师退钟记录"});
            this.cl.Location = new System.Drawing.Point(9, 125);
            this.cl.Name = "cl";
            this.cl.Size = new System.Drawing.Size(411, 312);
            this.cl.TabIndex = 7;
            this.cl.ThreeDCheckBoxes = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "选择优化日志";
            // 
            // endTime
            // 
            this.endTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.endTime.Location = new System.Drawing.Point(232, 59);
            this.endTime.Name = "endTime";
            this.endTime.Size = new System.Drawing.Size(120, 27);
            this.endTime.TabIndex = 4;
            this.endTime.Enter += new System.EventHandler(this.month_Enter);
            // 
            // startTime
            // 
            this.startTime.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.startTime.Location = new System.Drawing.Point(74, 59);
            this.startTime.Name = "startTime";
            this.startTime.Size = new System.Drawing.Size(120, 27);
            this.startTime.TabIndex = 4;
            this.startTime.Enter += new System.EventHandler(this.month_Enter);
            // 
            // month
            // 
            this.month.Location = new System.Drawing.Point(74, 24);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(120, 27);
            this.month.TabIndex = 3;
            this.month.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.month.Enter += new System.EventHandler(this.month_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(358, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "的数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "至";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "个月前的数据";
            // 
            // rmonth
            // 
            this.rmonth.AutoSize = true;
            this.rmonth.Checked = true;
            this.rmonth.Location = new System.Drawing.Point(6, 26);
            this.rmonth.Name = "rmonth";
            this.rmonth.Size = new System.Drawing.Size(62, 22);
            this.rmonth.TabIndex = 0;
            this.rmonth.TabStop = true;
            this.rmonth.Text = "优化";
            this.rmonth.UseVisualStyleBackColor = true;
            // 
            // rtime
            // 
            this.rtime.AutoSize = true;
            this.rtime.Location = new System.Drawing.Point(6, 63);
            this.rtime.Name = "rtime";
            this.rtime.Size = new System.Drawing.Size(62, 22);
            this.rtime.TabIndex = 0;
            this.rtime.Text = "优化";
            this.rtime.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 483);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(507, 48);
            this.panel1.TabIndex = 35;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(507, 2);
            this.label5.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(334, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(73, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 28);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // OptimizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(507, 531);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptimizationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据优化";
            this.Load += new System.EventHandler(this.OptimizationForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OptimizationForm_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.month)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rmonth;
        private System.Windows.Forms.RadioButton rtime;
        private System.Windows.Forms.NumericUpDown month;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox cl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label5;
    }
}