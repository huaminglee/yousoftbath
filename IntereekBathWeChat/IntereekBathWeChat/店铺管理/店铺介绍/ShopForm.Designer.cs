namespace IntereekBathWeChat
{
    partial class ShopForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShopForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTCancel = new System.Windows.Forms.Button();
            this.BTUpload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.panel1.Controls.Add(this.BTCancel);
            this.panel1.Controls.Add(this.BTUpload);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 655);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1329, 77);
            this.panel1.TabIndex = 0;
            // 
            // BTCancel
            // 
            this.BTCancel.AutoSize = true;
            this.BTCancel.BackColor = System.Drawing.Color.Orange;
            this.BTCancel.Location = new System.Drawing.Point(760, 18);
            this.BTCancel.Name = "BTCancel";
            this.BTCancel.Size = new System.Drawing.Size(175, 47);
            this.BTCancel.TabIndex = 1;
            this.BTCancel.Text = "取消";
            this.BTCancel.UseVisualStyleBackColor = false;
            this.BTCancel.Click += new System.EventHandler(this.BTCancel_Click);
            // 
            // BTUpload
            // 
            this.BTUpload.AutoSize = true;
            this.BTUpload.BackColor = System.Drawing.Color.Orange;
            this.BTUpload.Location = new System.Drawing.Point(394, 18);
            this.BTUpload.Name = "BTUpload";
            this.BTUpload.Size = new System.Drawing.Size(175, 47);
            this.BTUpload.TabIndex = 1;
            this.BTUpload.Text = "上传";
            this.BTUpload.UseVisualStyleBackColor = false;
            this.BTUpload.Click += new System.EventHandler(this.BTUpload_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1329, 2);
            this.label1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(1329, 655);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // ShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 732);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShopForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连客科技微信平台";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTUpload;
        private System.Windows.Forms.Button BTCancel;
        private System.Windows.Forms.RichTextBox richTextBox1;




    }
}

