namespace IntereekBathWeChat
{
    partial class FansForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FansForm));
            this.SP = new System.Windows.Forms.SplitContainer();
            this.BTFind = new System.Windows.Forms.Button();
            this.TextNick = new System.Windows.Forms.TextBox();
            this.SP.Panel1.SuspendLayout();
            this.SP.SuspendLayout();
            this.SuspendLayout();
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
            this.SP.Size = new System.Drawing.Size(291, 790);
            this.SP.SplitterDistance = 60;
            this.SP.TabIndex = 0;
            // 
            // BTFind
            // 
            this.BTFind.AutoSize = true;
            this.BTFind.Location = new System.Drawing.Point(202, 12);
            this.BTFind.Name = "BTFind";
            this.BTFind.Size = new System.Drawing.Size(75, 32);
            this.BTFind.TabIndex = 3;
            this.BTFind.Text = "查询";
            this.BTFind.UseVisualStyleBackColor = true;
            this.BTFind.Click += new System.EventHandler(this.BTFind_Click);
            // 
            // TextNick
            // 
            this.TextNick.BackColor = System.Drawing.Color.Orange;
            this.TextNick.Location = new System.Drawing.Point(10, 12);
            this.TextNick.Name = "TextNick";
            this.TextNick.Size = new System.Drawing.Size(184, 32);
            this.TextNick.TabIndex = 2;
            // 
            // FansForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(291, 790);
            this.Controls.Add(this.SP);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FansForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连客科技微信平台";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SP.Panel1.ResumeLayout(false);
            this.SP.Panel1.PerformLayout();
            this.SP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer SP;
        private System.Windows.Forms.Button BTFind;
        private System.Windows.Forms.TextBox TextNick;





    }
}

