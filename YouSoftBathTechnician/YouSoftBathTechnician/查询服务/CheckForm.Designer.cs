namespace YouSoftBathTechnician
{
    partial class CheckForm
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
            this.btnDayOn = new System.Windows.Forms.Button();
            this.btnDayOrder = new System.Windows.Forms.Button();
            this.btnMonthOn = new System.Windows.Forms.Button();
            this.btnMonthOrder = new System.Windows.Forms.Button();
            this.btnPwd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDayOn
            // 
            this.btnDayOn.BackColor = System.Drawing.SystemColors.Control;
            this.btnDayOn.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDayOn.Location = new System.Drawing.Point(45, 27);
            this.btnDayOn.Name = "btnDayOn";
            this.btnDayOn.Size = new System.Drawing.Size(102, 102);
            this.btnDayOn.TabIndex = 16;
            this.btnDayOn.Text = "日轮钟";
            this.btnDayOn.UseVisualStyleBackColor = false;
            this.btnDayOn.Click += new System.EventHandler(this.btnDayOn_Click);
            // 
            // btnDayOrder
            // 
            this.btnDayOrder.BackColor = System.Drawing.SystemColors.Control;
            this.btnDayOrder.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDayOrder.Location = new System.Drawing.Point(153, 27);
            this.btnDayOrder.Name = "btnDayOrder";
            this.btnDayOrder.Size = new System.Drawing.Size(102, 102);
            this.btnDayOrder.TabIndex = 16;
            this.btnDayOrder.Text = "日点钟";
            this.btnDayOrder.UseVisualStyleBackColor = false;
            this.btnDayOrder.Click += new System.EventHandler(this.btnDayOrder_Click);
            // 
            // btnMonthOn
            // 
            this.btnMonthOn.BackColor = System.Drawing.SystemColors.Control;
            this.btnMonthOn.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMonthOn.Location = new System.Drawing.Point(45, 135);
            this.btnMonthOn.Name = "btnMonthOn";
            this.btnMonthOn.Size = new System.Drawing.Size(102, 102);
            this.btnMonthOn.TabIndex = 16;
            this.btnMonthOn.Text = "月轮钟";
            this.btnMonthOn.UseVisualStyleBackColor = false;
            this.btnMonthOn.Click += new System.EventHandler(this.btnMonthOn_Click);
            // 
            // btnMonthOrder
            // 
            this.btnMonthOrder.BackColor = System.Drawing.SystemColors.Control;
            this.btnMonthOrder.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMonthOrder.Location = new System.Drawing.Point(153, 135);
            this.btnMonthOrder.Name = "btnMonthOrder";
            this.btnMonthOrder.Size = new System.Drawing.Size(102, 102);
            this.btnMonthOrder.TabIndex = 16;
            this.btnMonthOrder.Text = "月点钟";
            this.btnMonthOrder.UseVisualStyleBackColor = false;
            this.btnMonthOrder.Click += new System.EventHandler(this.btnMonthOrder_Click);
            // 
            // btnPwd
            // 
            this.btnPwd.BackColor = System.Drawing.SystemColors.Control;
            this.btnPwd.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPwd.Location = new System.Drawing.Point(261, 27);
            this.btnPwd.Name = "btnPwd";
            this.btnPwd.Size = new System.Drawing.Size(102, 102);
            this.btnPwd.TabIndex = 16;
            this.btnPwd.Text = "修改密码";
            this.btnPwd.UseVisualStyleBackColor = false;
            this.btnPwd.Click += new System.EventHandler(this.btnPwd_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Font = new System.Drawing.Font("宋体", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(261, 135);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(102, 102);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // CheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(409, 265);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMonthOrder);
            this.Controls.Add(this.btnPwd);
            this.Controls.Add(this.btnMonthOn);
            this.Controls.Add(this.btnDayOrder);
            this.Controls.Add(this.btnDayOn);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "技师查询";
            this.Load += new System.EventHandler(this.SeatForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDayOn;
        private System.Windows.Forms.Button btnDayOrder;
        private System.Windows.Forms.Button btnMonthOn;
        private System.Windows.Forms.Button btnMonthOrder;
        private System.Windows.Forms.Button btnPwd;
        private System.Windows.Forms.Button btnExit;
    }
}