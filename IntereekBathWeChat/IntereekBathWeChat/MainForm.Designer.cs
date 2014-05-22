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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BTTechs = new System.Windows.Forms.Button();
            this.BTPromotion = new System.Windows.Forms.Button();
            this.BTCombo = new System.Windows.Forms.Button();
            this.BTMenu = new System.Windows.Forms.Button();
            this.BTFans = new System.Windows.Forms.Button();
            this.BTShop = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.BTCouponUseRecord = new System.Windows.Forms.Button();
            this.BTCouponMang = new System.Windows.Forms.Button();
            this.BTCoupons = new System.Windows.Forms.Button();
            this.BTFeedBack = new System.Windows.Forms.Button();
            this.BTYeJi = new System.Windows.Forms.Button();
            this.BTChainStore = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer1.Panel1.Controls.Add(this.BTTechs);
            this.splitContainer1.Panel1.Controls.Add(this.BTPromotion);
            this.splitContainer1.Panel1.Controls.Add(this.BTCombo);
            this.splitContainer1.Panel1.Controls.Add(this.BTMenu);
            this.splitContainer1.Panel1.Controls.Add(this.BTFans);
            this.splitContainer1.Panel1.Controls.Add(this.BTShop);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(888, 402);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.TabStop = false;
            // 
            // BTTechs
            // 
            this.BTTechs.AutoSize = true;
            this.BTTechs.BackColor = System.Drawing.Color.Orange;
            this.BTTechs.Location = new System.Drawing.Point(157, 269);
            this.BTTechs.Name = "BTTechs";
            this.BTTechs.Size = new System.Drawing.Size(108, 70);
            this.BTTechs.TabIndex = 4;
            this.BTTechs.TabStop = false;
            this.BTTechs.Text = "店铺技师";
            this.BTTechs.UseVisualStyleBackColor = false;
            this.BTTechs.Click += new System.EventHandler(this.BTTechs_Click);
            // 
            // BTPromotion
            // 
            this.BTPromotion.AutoSize = true;
            this.BTPromotion.BackColor = System.Drawing.Color.Orange;
            this.BTPromotion.Location = new System.Drawing.Point(22, 269);
            this.BTPromotion.Name = "BTPromotion";
            this.BTPromotion.Size = new System.Drawing.Size(108, 70);
            this.BTPromotion.TabIndex = 5;
            this.BTPromotion.TabStop = false;
            this.BTPromotion.Text = "活动介绍";
            this.BTPromotion.UseVisualStyleBackColor = false;
            this.BTPromotion.Click += new System.EventHandler(this.BTPromotion_Click);
            // 
            // BTCombo
            // 
            this.BTCombo.AutoSize = true;
            this.BTCombo.BackColor = System.Drawing.Color.Orange;
            this.BTCombo.Location = new System.Drawing.Point(22, 162);
            this.BTCombo.Name = "BTCombo";
            this.BTCombo.Size = new System.Drawing.Size(108, 70);
            this.BTCombo.TabIndex = 3;
            this.BTCombo.TabStop = false;
            this.BTCombo.Text = "套餐介绍";
            this.BTCombo.UseVisualStyleBackColor = false;
            this.BTCombo.Click += new System.EventHandler(this.BTCombo_Click);
            // 
            // BTMenu
            // 
            this.BTMenu.AutoSize = true;
            this.BTMenu.BackColor = System.Drawing.Color.Orange;
            this.BTMenu.Location = new System.Drawing.Point(157, 162);
            this.BTMenu.Name = "BTMenu";
            this.BTMenu.Size = new System.Drawing.Size(108, 70);
            this.BTMenu.TabIndex = 1;
            this.BTMenu.TabStop = false;
            this.BTMenu.Text = "同步项目";
            this.BTMenu.UseVisualStyleBackColor = false;
            this.BTMenu.Click += new System.EventHandler(this.BTMenu_Click);
            // 
            // BTFans
            // 
            this.BTFans.AutoSize = true;
            this.BTFans.BackColor = System.Drawing.Color.Orange;
            this.BTFans.Location = new System.Drawing.Point(22, 59);
            this.BTFans.Name = "BTFans";
            this.BTFans.Size = new System.Drawing.Size(108, 70);
            this.BTFans.TabIndex = 2;
            this.BTFans.TabStop = false;
            this.BTFans.Text = "微信粉丝";
            this.BTFans.UseVisualStyleBackColor = false;
            this.BTFans.Click += new System.EventHandler(this.BTFans_Click);
            // 
            // BTShop
            // 
            this.BTShop.AutoSize = true;
            this.BTShop.BackColor = System.Drawing.Color.Orange;
            this.BTShop.Location = new System.Drawing.Point(157, 59);
            this.BTShop.Name = "BTShop";
            this.BTShop.Size = new System.Drawing.Size(108, 70);
            this.BTShop.TabIndex = 2;
            this.BTShop.TabStop = false;
            this.BTShop.Text = "店铺介绍";
            this.BTShop.UseVisualStyleBackColor = false;
            this.BTShop.Click += new System.EventHandler(this.BTShop_Click);
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
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer2.Panel1.Controls.Add(this.BTCouponUseRecord);
            this.splitContainer2.Panel1.Controls.Add(this.BTCouponMang);
            this.splitContainer2.Panel1.Controls.Add(this.BTCoupons);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.splitContainer2.Panel2.Controls.Add(this.BTChainStore);
            this.splitContainer2.Panel2.Controls.Add(this.BTYeJi);
            this.splitContainer2.Panel2.Controls.Add(this.BTFeedBack);
            this.splitContainer2.Size = new System.Drawing.Size(594, 402);
            this.splitContainer2.SplitterDistance = 290;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // BTCouponUseRecord
            // 
            this.BTCouponUseRecord.AutoSize = true;
            this.BTCouponUseRecord.BackColor = System.Drawing.Color.Orange;
            this.BTCouponUseRecord.Location = new System.Drawing.Point(56, 269);
            this.BTCouponUseRecord.Name = "BTCouponUseRecord";
            this.BTCouponUseRecord.Size = new System.Drawing.Size(174, 70);
            this.BTCouponUseRecord.TabIndex = 3;
            this.BTCouponUseRecord.TabStop = false;
            this.BTCouponUseRecord.Text = "优惠券使用记录";
            this.BTCouponUseRecord.UseVisualStyleBackColor = false;
            this.BTCouponUseRecord.Click += new System.EventHandler(this.BTCouponUseRecord_Click);
            // 
            // BTCouponMang
            // 
            this.BTCouponMang.AutoSize = true;
            this.BTCouponMang.BackColor = System.Drawing.Color.Orange;
            this.BTCouponMang.Location = new System.Drawing.Point(56, 162);
            this.BTCouponMang.Name = "BTCouponMang";
            this.BTCouponMang.Size = new System.Drawing.Size(174, 70);
            this.BTCouponMang.TabIndex = 3;
            this.BTCouponMang.TabStop = false;
            this.BTCouponMang.Text = "所有优惠券";
            this.BTCouponMang.UseVisualStyleBackColor = false;
            this.BTCouponMang.Click += new System.EventHandler(this.BTCouponMang_Click);
            // 
            // BTCoupons
            // 
            this.BTCoupons.AutoSize = true;
            this.BTCoupons.BackColor = System.Drawing.Color.Orange;
            this.BTCoupons.Location = new System.Drawing.Point(56, 59);
            this.BTCoupons.Name = "BTCoupons";
            this.BTCoupons.Size = new System.Drawing.Size(174, 70);
            this.BTCoupons.TabIndex = 3;
            this.BTCoupons.TabStop = false;
            this.BTCoupons.Text = "优惠券管理";
            this.BTCoupons.UseVisualStyleBackColor = false;
            this.BTCoupons.Click += new System.EventHandler(this.BTCoupons_Click);
            // 
            // BTFeedBack
            // 
            this.BTFeedBack.AutoSize = true;
            this.BTFeedBack.BackColor = System.Drawing.Color.Orange;
            this.BTFeedBack.Location = new System.Drawing.Point(94, 59);
            this.BTFeedBack.Name = "BTFeedBack";
            this.BTFeedBack.Size = new System.Drawing.Size(108, 70);
            this.BTFeedBack.TabIndex = 6;
            this.BTFeedBack.TabStop = false;
            this.BTFeedBack.Text = "查看反馈";
            this.BTFeedBack.UseVisualStyleBackColor = false;
            this.BTFeedBack.Click += new System.EventHandler(this.BTFeedBack_Click);
            // 
            // BTYeJi
            // 
            this.BTYeJi.AutoSize = true;
            this.BTYeJi.BackColor = System.Drawing.Color.Orange;
            this.BTYeJi.Location = new System.Drawing.Point(94, 269);
            this.BTYeJi.Name = "BTYeJi";
            this.BTYeJi.Size = new System.Drawing.Size(108, 70);
            this.BTYeJi.TabIndex = 6;
            this.BTYeJi.TabStop = false;
            this.BTYeJi.Text = "业绩查询";
            this.BTYeJi.UseVisualStyleBackColor = false;
            this.BTYeJi.Click += new System.EventHandler(this.BTYeJi_Click);
            // 
            // BTChainStore
            // 
            this.BTChainStore.AutoSize = true;
            this.BTChainStore.BackColor = System.Drawing.Color.Orange;
            this.BTChainStore.Location = new System.Drawing.Point(94, 162);
            this.BTChainStore.Name = "BTChainStore";
            this.BTChainStore.Size = new System.Drawing.Size(108, 70);
            this.BTChainStore.TabIndex = 6;
            this.BTChainStore.TabStop = false;
            this.BTChainStore.Text = "连锁店铺";
            this.BTChainStore.UseVisualStyleBackColor = false;
            this.BTChainStore.Click += new System.EventHandler(this.BTChainStore_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 402);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连客科技微信平台";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BTTechs;
        private System.Windows.Forms.Button BTPromotion;
        private System.Windows.Forms.Button BTCombo;
        private System.Windows.Forms.Button BTMenu;
        private System.Windows.Forms.Button BTShop;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button BTCoupons;
        private System.Windows.Forms.Button BTCouponMang;
        private System.Windows.Forms.Button BTCouponUseRecord;
        private System.Windows.Forms.Button BTFeedBack;
        private System.Windows.Forms.Button BTFans;
        private System.Windows.Forms.Button BTYeJi;
        private System.Windows.Forms.Button BTChainStore;



    }
}

