namespace IntereekBathWeChat
{
    partial class CouponForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CouponForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTCancel = new System.Windows.Forms.Button();
            this.BTOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TextDescp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextValue = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.panel1.Controls.Add(this.BTCancel);
            this.panel1.Controls.Add(this.BTOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 288);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 87);
            this.panel1.TabIndex = 0;
            // 
            // BTCancel
            // 
            this.BTCancel.AutoSize = true;
            this.BTCancel.BackColor = System.Drawing.Color.Orange;
            this.BTCancel.Location = new System.Drawing.Point(298, 17);
            this.BTCancel.Name = "BTCancel";
            this.BTCancel.Size = new System.Drawing.Size(178, 53);
            this.BTCancel.TabIndex = 1;
            this.BTCancel.TabStop = false;
            this.BTCancel.Text = "取  消";
            this.BTCancel.UseVisualStyleBackColor = false;
            this.BTCancel.Click += new System.EventHandler(this.BTCancel_Click);
            // 
            // BTOk
            // 
            this.BTOk.AutoSize = true;
            this.BTOk.BackColor = System.Drawing.Color.Orange;
            this.BTOk.Location = new System.Drawing.Point(57, 17);
            this.BTOk.Name = "BTOk";
            this.BTOk.Size = new System.Drawing.Size(178, 53);
            this.BTOk.TabIndex = 1;
            this.BTOk.TabStop = false;
            this.BTOk.Text = "确  定";
            this.BTOk.UseVisualStyleBackColor = false;
            this.BTOk.Click += new System.EventHandler(this.BTOk_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(529, 2);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.panel2.Controls.Add(this.TextDescp);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.TextValue);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.TextTitle);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(529, 288);
            this.panel2.TabIndex = 1;
            // 
            // TextDescp
            // 
            this.TextDescp.BackColor = System.Drawing.Color.Orange;
            this.TextDescp.Location = new System.Drawing.Point(166, 155);
            this.TextDescp.Multiline = true;
            this.TextDescp.Name = "TextDescp";
            this.TextDescp.Size = new System.Drawing.Size(310, 112);
            this.TextDescp.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "优惠券描述";
            // 
            // TextValue
            // 
            this.TextValue.BackColor = System.Drawing.Color.Orange;
            this.TextValue.Location = new System.Drawing.Point(166, 103);
            this.TextValue.Name = "TextValue";
            this.TextValue.Size = new System.Drawing.Size(310, 32);
            this.TextValue.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "优惠券价值";
            // 
            // TextTitle
            // 
            this.TextTitle.BackColor = System.Drawing.Color.Orange;
            this.TextTitle.Location = new System.Drawing.Point(166, 50);
            this.TextTitle.Name = "TextTitle";
            this.TextTitle.Size = new System.Drawing.Size(310, 32);
            this.TextTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "优惠券名称";
            // 
            // CouponForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 375);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "优惠券";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTOk;
        private System.Windows.Forms.Button BTCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextTitle;
        private System.Windows.Forms.TextBox TextDescp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextValue;
        private System.Windows.Forms.Label label3;





    }
}

