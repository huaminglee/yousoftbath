namespace YouSoftBathBack
{
    partial class RegisterForm
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
            this.code = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serial = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // code
            // 
            this.code.Location = new System.Drawing.Point(155, 109);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(463, 27);
            this.code.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 4;
            this.label3.Text = "注册码";
            // 
            // serial
            // 
            this.serial.Location = new System.Drawing.Point(155, 67);
            this.serial.Name = "serial";
            this.serial.ReadOnly = true;
            this.serial.Size = new System.Drawing.Size(463, 27);
            this.serial.TabIndex = 7;
            this.serial.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "序列号";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(155, 24);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(463, 27);
            this.name.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "公司名称";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 159);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(693, 44);
            this.panel1.TabIndex = 9;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(386, 6);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(99, 28);
            this.canBtn.TabIndex = 0;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(208, 6);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(99, 28);
            this.okBtn.TabIndex = 0;
            this.okBtn.Text = "注册";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(693, 2);
            this.label4.TabIndex = 6;
            // 
            // RegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.canBtn;
            this.ClientSize = new System.Drawing.Size(693, 203);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.code);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.serial);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegisterForm";
            this.ShowInTaskbar = false;
            this.Text = "软件注册";
            this.Load += new System.EventHandler(this.RegisterForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serial;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label4;
    }
}