namespace YouSoftBathReception
{
    partial class WXCouponVerifyForm
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
            this.TextCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BTCancel = new System.Windows.Forms.Button();
            this.BTVerify = new System.Windows.Forms.Button();
            this.BTScan = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextCode
            // 
            this.TextCode.Location = new System.Drawing.Point(230, 70);
            this.TextCode.Name = "TextCode";
            this.TextCode.Size = new System.Drawing.Size(307, 38);
            this.TextCode.TabIndex = 20;
            this.TextCode.Enter += new System.EventHandler(this.TextCode_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(75, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 27);
            this.label2.TabIndex = 16;
            this.label2.Text = "优惠券代码";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.BTCancel);
            this.panel1.Controls.Add(this.BTVerify);
            this.panel1.Controls.Add(this.BTScan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 172);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 76);
            this.panel1.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(646, 2);
            this.label1.TabIndex = 20;
            this.label1.Text = "label1";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(646, 0);
            this.label5.TabIndex = 19;
            // 
            // BTCancel
            // 
            this.BTCancel.AutoSize = true;
            this.BTCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BTCancel.Location = new System.Drawing.Point(436, 16);
            this.BTCancel.Margin = new System.Windows.Forms.Padding(0);
            this.BTCancel.Name = "BTCancel";
            this.BTCancel.Size = new System.Drawing.Size(158, 44);
            this.BTCancel.TabIndex = 6;
            this.BTCancel.TabStop = false;
            this.BTCancel.Text = "取消(ESC)";
            this.BTCancel.UseVisualStyleBackColor = true;
            this.BTCancel.Click += new System.EventHandler(this.BTCancel_Click);
            // 
            // BTVerify
            // 
            this.BTVerify.AutoSize = true;
            this.BTVerify.Location = new System.Drawing.Point(244, 16);
            this.BTVerify.Margin = new System.Windows.Forms.Padding(0);
            this.BTVerify.Name = "BTVerify";
            this.BTVerify.Size = new System.Drawing.Size(158, 44);
            this.BTVerify.TabIndex = 5;
            this.BTVerify.TabStop = false;
            this.BTVerify.Text = "验证并消费";
            this.BTVerify.UseVisualStyleBackColor = true;
            this.BTVerify.Click += new System.EventHandler(this.BTVerify_Click);
            // 
            // BTScan
            // 
            this.BTScan.AutoSize = true;
            this.BTScan.Location = new System.Drawing.Point(52, 16);
            this.BTScan.Margin = new System.Windows.Forms.Padding(0);
            this.BTScan.Name = "BTScan";
            this.BTScan.Size = new System.Drawing.Size(158, 44);
            this.BTScan.TabIndex = 5;
            this.BTScan.TabStop = false;
            this.BTScan.Text = "扫描并消费";
            this.BTScan.UseVisualStyleBackColor = true;
            this.BTScan.Click += new System.EventHandler(this.BTScan_Click);
            // 
            // WXCouponVerifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 27F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(646, 248);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TextCode);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WXCouponVerifyForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "微信优惠券验证";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BTCancel;
        private System.Windows.Forms.Button BTScan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTVerify;


    }
}