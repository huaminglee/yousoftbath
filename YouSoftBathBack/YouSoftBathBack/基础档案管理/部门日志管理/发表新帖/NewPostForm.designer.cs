namespace YouSoftBathBack
{
    partial class NewPostForm
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
            this.PanelControl = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.PanelMsg = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.TextImgPath = new System.Windows.Forms.TextBox();
            this.BtnUploadImg = new System.Windows.Forms.Button();
            this.TextMsg = new System.Windows.Forms.TextBox();
            this.CheckAnonymous = new System.Windows.Forms.CheckBox();
            this.ComboDeparts = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PanelPicture = new System.Windows.Forms.Panel();
            this.Picture3 = new System.Windows.Forms.PictureBox();
            this.Picture2 = new System.Windows.Forms.PictureBox();
            this.Picture1 = new System.Windows.Forms.PictureBox();
            this.PanelControl.SuspendLayout();
            this.PanelMsg.SuspendLayout();
            this.PanelPicture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelControl
            // 
            this.PanelControl.Controls.Add(this.label13);
            this.PanelControl.Controls.Add(this.canBtn);
            this.PanelControl.Controls.Add(this.okBtn);
            this.PanelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelControl.Location = new System.Drawing.Point(0, 605);
            this.PanelControl.Name = "PanelControl";
            this.PanelControl.Size = new System.Drawing.Size(813, 51);
            this.PanelControl.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(813, 2);
            this.label13.TabIndex = 19;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(523, 11);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(101, 28);
            this.canBtn.TabIndex = 18;
            this.canBtn.TabStop = false;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(188, 11);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(101, 28);
            this.okBtn.TabIndex = 17;
            this.okBtn.TabStop = false;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // PanelMsg
            // 
            this.PanelMsg.Controls.Add(this.label2);
            this.PanelMsg.Controls.Add(this.TextImgPath);
            this.PanelMsg.Controls.Add(this.BtnUploadImg);
            this.PanelMsg.Controls.Add(this.TextMsg);
            this.PanelMsg.Controls.Add(this.CheckAnonymous);
            this.PanelMsg.Controls.Add(this.ComboDeparts);
            this.PanelMsg.Controls.Add(this.label1);
            this.PanelMsg.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelMsg.Location = new System.Drawing.Point(0, 0);
            this.PanelMsg.Name = "PanelMsg";
            this.PanelMsg.Size = new System.Drawing.Size(813, 258);
            this.PanelMsg.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(813, 2);
            this.label2.TabIndex = 23;
            // 
            // TextImgPath
            // 
            this.TextImgPath.BackColor = System.Drawing.Color.White;
            this.TextImgPath.Location = new System.Drawing.Point(185, 209);
            this.TextImgPath.Name = "TextImgPath";
            this.TextImgPath.ReadOnly = true;
            this.TextImgPath.Size = new System.Drawing.Size(596, 27);
            this.TextImgPath.TabIndex = 22;
            // 
            // BtnUploadImg
            // 
            this.BtnUploadImg.AutoSize = true;
            this.BtnUploadImg.Location = new System.Drawing.Point(35, 208);
            this.BtnUploadImg.Name = "BtnUploadImg";
            this.BtnUploadImg.Size = new System.Drawing.Size(144, 28);
            this.BtnUploadImg.TabIndex = 21;
            this.BtnUploadImg.Text = "上传第一幅图片";
            this.BtnUploadImg.UseVisualStyleBackColor = true;
            this.BtnUploadImg.Click += new System.EventHandler(this.BtnUploadImg_Click);
            // 
            // TextMsg
            // 
            this.TextMsg.Location = new System.Drawing.Point(32, 64);
            this.TextMsg.Multiline = true;
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.Size = new System.Drawing.Size(749, 123);
            this.TextMsg.TabIndex = 20;
            this.TextMsg.Enter += new System.EventHandler(this.name_Enter);
            // 
            // CheckAnonymous
            // 
            this.CheckAnonymous.AutoSize = true;
            this.CheckAnonymous.Location = new System.Drawing.Point(330, 24);
            this.CheckAnonymous.Name = "CheckAnonymous";
            this.CheckAnonymous.Size = new System.Drawing.Size(63, 22);
            this.CheckAnonymous.TabIndex = 19;
            this.CheckAnonymous.Text = "匿名";
            this.CheckAnonymous.UseVisualStyleBackColor = true;
            // 
            // ComboDeparts
            // 
            this.ComboDeparts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboDeparts.FormattingEnabled = true;
            this.ComboDeparts.Location = new System.Drawing.Point(118, 21);
            this.ComboDeparts.Name = "ComboDeparts";
            this.ComboDeparts.Size = new System.Drawing.Size(187, 25);
            this.ComboDeparts.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 17;
            this.label1.Text = "呈交部门";
            // 
            // PanelPicture
            // 
            this.PanelPicture.Controls.Add(this.Picture3);
            this.PanelPicture.Controls.Add(this.Picture2);
            this.PanelPicture.Controls.Add(this.Picture1);
            this.PanelPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelPicture.Location = new System.Drawing.Point(0, 258);
            this.PanelPicture.Name = "PanelPicture";
            this.PanelPicture.Size = new System.Drawing.Size(813, 347);
            this.PanelPicture.TabIndex = 22;
            // 
            // Picture3
            // 
            this.Picture3.BackColor = System.Drawing.Color.White;
            this.Picture3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture3.Location = new System.Drawing.Point(538, 0);
            this.Picture3.Name = "Picture3";
            this.Picture3.Size = new System.Drawing.Size(243, 339);
            this.Picture3.TabIndex = 18;
            this.Picture3.TabStop = false;
            this.Picture3.DoubleClick += new System.EventHandler(this.Picture3_DoubleClick);
            // 
            // Picture2
            // 
            this.Picture2.BackColor = System.Drawing.Color.White;
            this.Picture2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture2.Location = new System.Drawing.Point(285, 0);
            this.Picture2.Name = "Picture2";
            this.Picture2.Size = new System.Drawing.Size(243, 339);
            this.Picture2.TabIndex = 18;
            this.Picture2.TabStop = false;
            this.Picture2.DoubleClick += new System.EventHandler(this.Picture2_DoubleClick);
            // 
            // Picture1
            // 
            this.Picture1.BackColor = System.Drawing.Color.White;
            this.Picture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture1.Location = new System.Drawing.Point(32, 0);
            this.Picture1.Name = "Picture1";
            this.Picture1.Size = new System.Drawing.Size(243, 339);
            this.Picture1.TabIndex = 18;
            this.Picture1.TabStop = false;
            this.Picture1.DoubleClick += new System.EventHandler(this.Picture1_DoubleClick);
            // 
            // NewPostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 656);
            this.Controls.Add(this.PanelPicture);
            this.Controls.Add(this.PanelMsg);
            this.Controls.Add(this.PanelControl);
            this.Font = new System.Drawing.Font("宋体", 13F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPostForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "发表新帖";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.PanelControl.ResumeLayout(false);
            this.PanelControl.PerformLayout();
            this.PanelMsg.ResumeLayout(false);
            this.PanelMsg.PerformLayout();
            this.PanelPicture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelControl;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel PanelMsg;
        private System.Windows.Forms.TextBox TextImgPath;
        private System.Windows.Forms.Button BtnUploadImg;
        private System.Windows.Forms.TextBox TextMsg;
        private System.Windows.Forms.CheckBox CheckAnonymous;
        private System.Windows.Forms.ComboBox ComboDeparts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel PanelPicture;
        private System.Windows.Forms.PictureBox Picture3;
        private System.Windows.Forms.PictureBox Picture2;
        private System.Windows.Forms.PictureBox Picture1;
    }
}