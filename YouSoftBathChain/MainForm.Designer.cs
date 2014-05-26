namespace IntereekBathWeChat
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BTChainStore = new System.Windows.Forms.Button();
            this.BTYeJi = new System.Windows.Forms.Button();
            this.BTFeedBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BTChainStore
            // 
            this.BTChainStore.AutoSize = true;
            this.BTChainStore.BackColor = System.Drawing.Color.Orange;
            this.BTChainStore.Location = new System.Drawing.Point(88, 150);
            this.BTChainStore.Name = "BTChainStore";
            this.BTChainStore.Size = new System.Drawing.Size(108, 70);
            this.BTChainStore.TabIndex = 9;
            this.BTChainStore.TabStop = false;
            this.BTChainStore.Text = "连锁店铺";
            this.BTChainStore.UseVisualStyleBackColor = false;
            this.BTChainStore.Click += new System.EventHandler(this.BTChainStore_Click);
            // 
            // BTYeJi
            // 
            this.BTYeJi.AutoSize = true;
            this.BTYeJi.BackColor = System.Drawing.Color.Orange;
            this.BTYeJi.Location = new System.Drawing.Point(88, 257);
            this.BTYeJi.Name = "BTYeJi";
            this.BTYeJi.Size = new System.Drawing.Size(108, 70);
            this.BTYeJi.TabIndex = 8;
            this.BTYeJi.TabStop = false;
            this.BTYeJi.Text = "业绩查询";
            this.BTYeJi.UseVisualStyleBackColor = false;
            this.BTYeJi.Click += new System.EventHandler(this.BTYeJi_Click);
            // 
            // BTFeedBack
            // 
            this.BTFeedBack.AutoSize = true;
            this.BTFeedBack.BackColor = System.Drawing.Color.Orange;
            this.BTFeedBack.Location = new System.Drawing.Point(88, 47);
            this.BTFeedBack.Name = "BTFeedBack";
            this.BTFeedBack.Size = new System.Drawing.Size(108, 70);
            this.BTFeedBack.TabIndex = 7;
            this.BTFeedBack.TabStop = false;
            this.BTFeedBack.Text = "查看反馈";
            this.BTFeedBack.UseVisualStyleBackColor = false;
            this.BTFeedBack.Click += new System.EventHandler(this.BTFeedBack_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(282, 402);
            this.Controls.Add(this.BTChainStore);
            this.Controls.Add(this.BTYeJi);
            this.Controls.Add(this.BTFeedBack);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连客科技连锁店铺管理";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTChainStore;
        private System.Windows.Forms.Button BTYeJi;
        private System.Windows.Forms.Button BTFeedBack;




    }
}

