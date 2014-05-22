namespace YouSoftBathFormClass
{
    partial class DianLunForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DianLunForm));
            this.btnUndoDiscount = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnDiscount = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnUndoDiscount
            // 
            this.btnUndoDiscount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUndoDiscount.AutoSize = true;
            this.btnUndoDiscount.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnUndoDiscount.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnUndoDiscount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnUndoDiscount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnUndoDiscount.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUndoDiscount.ImageIndex = 1;
            this.btnUndoDiscount.ImageList = this.imageList1;
            this.btnUndoDiscount.Location = new System.Drawing.Point(294, 70);
            this.btnUndoDiscount.Margin = new System.Windows.Forms.Padding(6);
            this.btnUndoDiscount.Name = "btnUndoDiscount";
            this.btnUndoDiscount.Size = new System.Drawing.Size(191, 178);
            this.btnUndoDiscount.TabIndex = 2;
            this.btnUndoDiscount.Text = "取消打折";
            this.btnUndoDiscount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnUndoDiscount.UseVisualStyleBackColor = true;
            this.btnUndoDiscount.Click += new System.EventHandler(this.btnUndoDiscount_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "20130330034045714_easyicon_net_128.png");
            this.imageList1.Images.SetKeyName(1, "undo.png");
            this.imageList1.Images.SetKeyName(2, "20130329080804234_easyicon_net_128.png");
            // 
            // btnDiscount
            // 
            this.btnDiscount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDiscount.AutoSize = true;
            this.btnDiscount.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDiscount.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDiscount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnDiscount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnDiscount.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDiscount.ImageIndex = 0;
            this.btnDiscount.ImageList = this.imageList1;
            this.btnDiscount.Location = new System.Drawing.Point(70, 70);
            this.btnDiscount.Margin = new System.Windows.Forms.Padding(6);
            this.btnDiscount.Name = "btnDiscount";
            this.btnDiscount.Size = new System.Drawing.Size(191, 178);
            this.btnDiscount.TabIndex = 1;
            this.btnDiscount.Text = "会员打折";
            this.btnDiscount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDiscount.UseVisualStyleBackColor = true;
            this.btnDiscount.Click += new System.EventHandler(this.btnDiscount_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ImageIndex = 2;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(518, 70);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(191, 178);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退出";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DianLunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(778, 319);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUndoDiscount);
            this.Controls.Add(this.btnDiscount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DianLunForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "点钟轮钟选择";
            this.Load += new System.EventHandler(this.TransferSelectForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TransferSelectForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUndoDiscount;
        private System.Windows.Forms.Button btnDiscount;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnCancel;
    }
}