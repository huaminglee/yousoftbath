namespace YouSoftBathBack
{
    partial class ModifyPictureForm
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
            this.Picture1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnRemove = new System.Windows.Forms.Button();
            this.BtnModify = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Picture1
            // 
            this.Picture1.BackColor = System.Drawing.Color.White;
            this.Picture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture1.Location = new System.Drawing.Point(240, 12);
            this.Picture1.Name = "Picture1";
            this.Picture1.Size = new System.Drawing.Size(243, 339);
            this.Picture1.TabIndex = 19;
            this.Picture1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.BtnOk);
            this.groupBox1.Controls.Add(this.BtnModify);
            this.groupBox1.Controls.Add(this.BtnRemove);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 339);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "修改图片信息";
            // 
            // BtnRemove
            // 
            this.BtnRemove.AutoSize = true;
            this.BtnRemove.Location = new System.Drawing.Point(6, 76);
            this.BtnRemove.Name = "BtnRemove";
            this.BtnRemove.Size = new System.Drawing.Size(210, 28);
            this.BtnRemove.TabIndex = 0;
            this.BtnRemove.Text = "移除";
            this.BtnRemove.UseVisualStyleBackColor = true;
            this.BtnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // BtnModify
            // 
            this.BtnModify.AutoSize = true;
            this.BtnModify.Location = new System.Drawing.Point(6, 129);
            this.BtnModify.Name = "BtnModify";
            this.BtnModify.Size = new System.Drawing.Size(210, 28);
            this.BtnModify.TabIndex = 0;
            this.BtnModify.Text = "修改";
            this.BtnModify.UseVisualStyleBackColor = true;
            this.BtnModify.Click += new System.EventHandler(this.BtnModify_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.AutoSize = true;
            this.BtnOk.Location = new System.Drawing.Point(6, 182);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(210, 28);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "确定";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.AutoSize = true;
            this.BtnCancel.Location = new System.Drawing.Point(6, 235);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(210, 28);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "取消";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ModifyPictureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 369);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Picture1);
            this.Font = new System.Drawing.Font("宋体", 13F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyPictureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改图片信息";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Picture1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnRemove;
        private System.Windows.Forms.Button BtnModify;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;

    }
}