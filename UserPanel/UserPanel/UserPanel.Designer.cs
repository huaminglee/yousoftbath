namespace UserPanel
{
    partial class UserPanel
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

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.userIcon = new System.Windows.Forms.PictureBox();
            this.LBNick = new System.Windows.Forms.Label();
            this.LBContent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.userIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // userIcon
            // 
            this.userIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userIcon.Location = new System.Drawing.Point(17, 12);
            this.userIcon.Name = "userIcon";
            this.userIcon.Size = new System.Drawing.Size(50, 55);
            this.userIcon.TabIndex = 0;
            this.userIcon.TabStop = false;
            this.userIcon.Click += new System.EventHandler(this.UserPanel_Click);
            // 
            // LBNick
            // 
            this.LBNick.AutoSize = true;
            this.LBNick.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LBNick.ForeColor = System.Drawing.Color.Red;
            this.LBNick.Location = new System.Drawing.Point(74, 11);
            this.LBNick.Name = "LBNick";
            this.LBNick.Size = new System.Drawing.Size(68, 18);
            this.LBNick.TabIndex = 1;
            this.LBNick.Text = "label1";
            this.LBNick.Click += new System.EventHandler(this.UserPanel_Click);
            // 
            // LBContent
            // 
            this.LBContent.AutoSize = true;
            this.LBContent.BackColor = System.Drawing.Color.Transparent;
            this.LBContent.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LBContent.ForeColor = System.Drawing.Color.Gray;
            this.LBContent.Location = new System.Drawing.Point(74, 29);
            this.LBContent.Name = "LBContent";
            this.LBContent.Size = new System.Drawing.Size(55, 15);
            this.LBContent.TabIndex = 2;
            this.LBContent.Text = "label2";
            this.LBContent.Click += new System.EventHandler(this.UserPanel_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 2);
            this.label1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 2);
            this.label2.TabIndex = 4;
            // 
            // UserPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LBContent);
            this.Controls.Add(this.LBNick);
            this.Controls.Add(this.userIcon);
            this.Name = "UserPanel";
            this.Size = new System.Drawing.Size(160, 72);
            this.Click += new System.EventHandler(this.UserPanel_Click);
            ((System.ComponentModel.ISupportInitialize)(this.userIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox userIcon;
        private System.Windows.Forms.Label LBNick;
        private System.Windows.Forms.Label LBContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
