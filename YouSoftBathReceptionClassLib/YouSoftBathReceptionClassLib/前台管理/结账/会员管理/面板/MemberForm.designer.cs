namespace YouSoftBathReception
{
    partial class MemberForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberForm));
            this.btnSale = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnPop = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnResume = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btn_activate = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSale
            // 
            this.btnSale.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSale.AutoSize = true;
            this.btnSale.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSale.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnSale.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnSale.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSale.ImageIndex = 0;
            this.btnSale.ImageList = this.imageList1;
            this.btnSale.Location = new System.Drawing.Point(229, 13);
            this.btnSale.Name = "btnSale";
            this.btnSale.Size = new System.Drawing.Size(161, 164);
            this.btnSale.TabIndex = 23;
            this.btnSale.TabStop = false;
            this.btnSale.Text = "资料(2)";
            this.btnSale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSale.UseVisualStyleBackColor = true;
            this.btnSale.Click += new System.EventHandler(this.btnSale_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user-add.png");
            this.imageList1.Images.SetKeyName(1, "user-up.png");
            this.imageList1.Images.SetKeyName(2, "user-cross.png");
            this.imageList1.Images.SetKeyName(3, "user-edit.png");
            this.imageList1.Images.SetKeyName(4, "card.png");
            this.imageList1.Images.SetKeyName(5, "settings-icon.png");
            this.imageList1.Images.SetKeyName(6, "add_user.png");
            this.imageList1.Images.SetKeyName(7, "20130329080804234_easyicon_net_128.png");
            // 
            // btnPop
            // 
            this.btnPop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnPop.AutoSize = true;
            this.btnPop.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnPop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnPop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnPop.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPop.ImageIndex = 1;
            this.btnPop.ImageList = this.imageList1;
            this.btnPop.Location = new System.Drawing.Point(417, 13);
            this.btnPop.Name = "btnPop";
            this.btnPop.Size = new System.Drawing.Size(161, 164);
            this.btnPop.TabIndex = 23;
            this.btnPop.TabStop = false;
            this.btnPop.Text = "充值(3)";
            this.btnPop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPop.UseVisualStyleBackColor = true;
            this.btnPop.Click += new System.EventHandler(this.btnPop_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStop.AutoSize = true;
            this.btnStop.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnStop.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStop.ImageIndex = 2;
            this.btnStop.ImageList = this.imageList1;
            this.btnStop.Location = new System.Drawing.Point(605, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(161, 164);
            this.btnStop.TabIndex = 23;
            this.btnStop.TabStop = false;
            this.btnStop.Text = "挂失启用(4)";
            this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnResume
            // 
            this.btnResume.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnResume.AutoSize = true;
            this.btnResume.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnResume.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnResume.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnResume.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnResume.ImageIndex = 3;
            this.btnResume.ImageList = this.imageList1;
            this.btnResume.Location = new System.Drawing.Point(41, 223);
            this.btnResume.Name = "btnResume";
            this.btnResume.Size = new System.Drawing.Size(161, 164);
            this.btnResume.TabIndex = 23;
            this.btnResume.TabStop = false;
            this.btnResume.Text = "补卡(5)";
            this.btnResume.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnResume.UseVisualStyleBackColor = true;
            this.btnResume.Click += new System.EventHandler(this.btnResume_Click);
            // 
            // btnSet
            // 
            this.btnSet.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSet.AutoSize = true;
            this.btnSet.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSet.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnSet.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSet.ImageIndex = 5;
            this.btnSet.ImageList = this.imageList1;
            this.btnSet.Location = new System.Drawing.Point(417, 223);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(161, 164);
            this.btnSet.TabIndex = 23;
            this.btnSet.TabStop = false;
            this.btnSet.Text = "  设置(7)";
            this.btnSet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnRead
            // 
            this.btnRead.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRead.AutoSize = true;
            this.btnRead.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRead.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnRead.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnRead.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRead.ImageIndex = 4;
            this.btnRead.ImageList = this.imageList1;
            this.btnRead.Location = new System.Drawing.Point(229, 223);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(161, 164);
            this.btnRead.TabIndex = 23;
            this.btnRead.TabStop = false;
            this.btnRead.Text = "读卡(6)";
            this.btnRead.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btn_activate
            // 
            this.btn_activate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_activate.AutoSize = true;
            this.btn_activate.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_activate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btn_activate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btn_activate.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_activate.ImageIndex = 6;
            this.btn_activate.ImageList = this.imageList1;
            this.btn_activate.Location = new System.Drawing.Point(41, 12);
            this.btn_activate.Name = "btn_activate";
            this.btn_activate.Size = new System.Drawing.Size(161, 164);
            this.btn_activate.TabIndex = 23;
            this.btn_activate.TabStop = false;
            this.btn_activate.Text = "售卡退卡(1)";
            this.btn_activate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_activate.UseVisualStyleBackColor = true;
            this.btn_activate.Click += new System.EventHandler(this.btn_activate_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_cancel.AutoSize = true;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btn_cancel.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_cancel.ImageIndex = 7;
            this.btn_cancel.ImageList = this.imageList1;
            this.btn_cancel.Location = new System.Drawing.Point(605, 223);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(161, 164);
            this.btn_cancel.TabIndex = 23;
            this.btn_cancel.TabStop = false;
            this.btn_cancel.Text = "退出(0)";
            this.btn_cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // MemberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(806, 398);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnResume);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPop);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btn_activate);
            this.Controls.Add(this.btnSale);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemberForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会员卡操作";
            this.Load += new System.EventHandler(this.MemberForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MemberForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSale;
        private System.Windows.Forms.Button btnPop;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnResume;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btn_activate;
        private System.Windows.Forms.Button btn_cancel;
    }
}