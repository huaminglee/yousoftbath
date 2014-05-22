namespace YouSoftBathBack
{
    partial class DaysLimitForm
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
            this.PanelControl = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DTPLimit = new System.Windows.Forms.DateTimePicker();
            this.PanelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelControl
            // 
            this.PanelControl.Controls.Add(this.label13);
            this.PanelControl.Controls.Add(this.canBtn);
            this.PanelControl.Controls.Add(this.okBtn);
            this.PanelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelControl.Location = new System.Drawing.Point(0, 144);
            this.PanelControl.Name = "PanelControl";
            this.PanelControl.Size = new System.Drawing.Size(477, 51);
            this.PanelControl.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(477, 2);
            this.label13.TabIndex = 19;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(303, 11);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(101, 28);
            this.canBtn.TabIndex = 18;
            this.canBtn.TabStop = false;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(72, 11);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(101, 28);
            this.okBtn.TabIndex = 17;
            this.okBtn.TabStop = false;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "事件截止日期";
            // 
            // DTPLimit
            // 
            this.DTPLimit.Location = new System.Drawing.Point(231, 59);
            this.DTPLimit.Name = "DTPLimit";
            this.DTPLimit.Size = new System.Drawing.Size(137, 27);
            this.DTPLimit.TabIndex = 12;
            // 
            // DaysLimitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 195);
            this.Controls.Add(this.DTPLimit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PanelControl);
            this.Font = new System.Drawing.Font("宋体", 13F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaysLimitForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "限定时间";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.PanelControl.ResumeLayout(false);
            this.PanelControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel PanelControl;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DTPLimit;
    }
}