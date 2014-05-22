namespace YouSoftBathReception
{
    partial class TransferSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransferSelectForm));
            this.btnOrderClock = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOnClock = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOrderClock
            // 
            this.btnOrderClock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOrderClock.AutoSize = true;
            this.btnOrderClock.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOrderClock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnOrderClock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnOrderClock.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOrderClock.ImageIndex = 1;
            this.btnOrderClock.ImageList = this.imageList1;
            this.btnOrderClock.Location = new System.Drawing.Point(281, 24);
            this.btnOrderClock.Margin = new System.Windows.Forms.Padding(6);
            this.btnOrderClock.Name = "btnOrderClock";
            this.btnOrderClock.Size = new System.Drawing.Size(191, 178);
            this.btnOrderClock.TabIndex = 2;
            this.btnOrderClock.Text = "继续消费";
            this.btnOrderClock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOrderClock.UseVisualStyleBackColor = true;
            this.btnOrderClock.Click += new System.EventHandler(this.btnOrderClock_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "stop1.png");
            this.imageList1.Images.SetKeyName(1, "play.png");
            this.imageList1.Images.SetKeyName(2, "20130329080804234_easyicon_net_128.png");
            this.imageList1.Images.SetKeyName(3, "undo.png");
            // 
            // btnOnClock
            // 
            this.btnOnClock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOnClock.AutoSize = true;
            this.btnOnClock.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOnClock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnOnClock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnOnClock.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOnClock.ImageIndex = 0;
            this.btnOnClock.ImageList = this.imageList1;
            this.btnOnClock.Location = new System.Drawing.Point(60, 24);
            this.btnOnClock.Margin = new System.Windows.Forms.Padding(6);
            this.btnOnClock.Name = "btnOnClock";
            this.btnOnClock.Size = new System.Drawing.Size(191, 178);
            this.btnOnClock.TabIndex = 1;
            this.btnOnClock.Text = "停止消费";
            this.btnOnClock.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOnClock.UseVisualStyleBackColor = true;
            this.btnOnClock.Click += new System.EventHandler(this.btnOnClock_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ImageIndex = 2;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(281, 214);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(191, 178);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退出";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRestore.AutoSize = true;
            this.btnRestore.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnRestore.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnRestore.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnRestore.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRestore.ImageIndex = 3;
            this.btnRestore.ImageList = this.imageList1;
            this.btnRestore.Location = new System.Drawing.Point(60, 214);
            this.btnRestore.Margin = new System.Windows.Forms.Padding(6);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(191, 178);
            this.btnRestore.TabIndex = 2;
            this.btnRestore.Text = "恢复";
            this.btnRestore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // TransferSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(532, 416);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOrderClock);
            this.Controls.Add(this.btnOnClock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferSelectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "转账类型选择";
            this.Load += new System.EventHandler(this.TransferSelectForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransferSelectForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOrderClock;
        private System.Windows.Forms.Button btnOnClock;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRestore;
    }
}