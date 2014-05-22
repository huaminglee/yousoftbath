namespace YouSoftBathTechnician
{
    partial class ModifyPwdForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.pwdNew2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pwdNew = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pwdOld = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.job = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(395, 44);
            this.panel1.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(395, 2);
            this.label7.TabIndex = 10;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(228, 6);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(99, 28);
            this.canBtn.TabIndex = 5;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            this.canBtn.Click += new System.EventHandler(this.canBtn_Click);
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(63, 6);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(99, 28);
            this.okBtn.TabIndex = 4;
            this.okBtn.Text = "确认";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // pwdNew2
            // 
            this.pwdNew2.Location = new System.Drawing.Point(132, 249);
            this.pwdNew2.Name = "pwdNew2";
            this.pwdNew2.PasswordChar = '*';
            this.pwdNew2.Size = new System.Drawing.Size(211, 27);
            this.pwdNew2.TabIndex = 16;
            this.pwdNew2.Enter += new System.EventHandler(this.pwdOld_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "确认密码";
            // 
            // pwdNew
            // 
            this.pwdNew.Location = new System.Drawing.Point(132, 201);
            this.pwdNew.Name = "pwdNew";
            this.pwdNew.PasswordChar = '*';
            this.pwdNew.Size = new System.Drawing.Size(211, 27);
            this.pwdNew.TabIndex = 13;
            this.pwdNew.Enter += new System.EventHandler(this.pwdOld_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "新 密 码";
            // 
            // pwdOld
            // 
            this.pwdOld.Location = new System.Drawing.Point(132, 153);
            this.pwdOld.Name = "pwdOld";
            this.pwdOld.PasswordChar = '*';
            this.pwdOld.Size = new System.Drawing.Size(211, 27);
            this.pwdOld.TabIndex = 11;
            this.pwdOld.Enter += new System.EventHandler(this.pwdOld_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "旧 密 码";
            // 
            // job
            // 
            this.job.Location = new System.Drawing.Point(132, 105);
            this.job.Name = "job";
            this.job.ReadOnly = true;
            this.job.Size = new System.Drawing.Size(211, 27);
            this.job.TabIndex = 14;
            this.job.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "员工职位";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(132, 57);
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Size = new System.Drawing.Size(211, 27);
            this.name.TabIndex = 12;
            this.name.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "员工姓名";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(132, 10);
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Size = new System.Drawing.Size(211, 27);
            this.id.TabIndex = 10;
            this.id.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "员 工 号";
            // 
            // ModifyPwdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.canBtn;
            this.ClientSize = new System.Drawing.Size(395, 340);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pwdNew2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pwdNew);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pwdOld);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.job);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyPwdForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.Load += new System.EventHandler(this.ModifyPwdForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModifyPwdForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.TextBox pwdNew2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pwdNew;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pwdOld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox job;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
    }
}