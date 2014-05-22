namespace YouSoftBathBack
{
    partial class payAccountForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cus_name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cus_tel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cash = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bank = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 288);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 48);
            this.panel1.TabIndex = 34;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(253, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(99, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 28);
            this.btnOk.TabIndex = 4;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(451, 2);
            this.label1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 35;
            this.label3.Text = "客户名称";
            // 
            // cus_name
            // 
            this.cus_name.Enabled = false;
            this.cus_name.Location = new System.Drawing.Point(139, 29);
            this.cus_name.Name = "cus_name";
            this.cus_name.Size = new System.Drawing.Size(255, 27);
            this.cus_name.TabIndex = 36;
            this.cus_name.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 35;
            this.label4.Text = "客户电话";
            // 
            // cus_tel
            // 
            this.cus_tel.Enabled = false;
            this.cus_tel.Location = new System.Drawing.Point(139, 82);
            this.cus_tel.Name = "cus_tel";
            this.cus_tel.Size = new System.Drawing.Size(255, 27);
            this.cus_tel.TabIndex = 36;
            this.cus_tel.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 35;
            this.label6.Text = "现金销账";
            // 
            // cash
            // 
            this.cash.Location = new System.Drawing.Point(139, 135);
            this.cash.Name = "cash";
            this.cash.Size = new System.Drawing.Size(255, 27);
            this.cash.TabIndex = 1;
            this.cash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cus_pay_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 35;
            this.label7.Text = "银联销账";
            // 
            // bank
            // 
            this.bank.Location = new System.Drawing.Point(139, 188);
            this.bank.Name = "bank";
            this.bank.Size = new System.Drawing.Size(255, 27);
            this.bank.TabIndex = 2;
            this.bank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cus_pay_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 35;
            this.label5.Text = "备    注";
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(139, 234);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(255, 27);
            this.note.TabIndex = 3;
            this.note.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cus_pay_KeyPress);
            // 
            // payAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 336);
            this.Controls.Add(this.note);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bank);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cash);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cus_tel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cus_name);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "payAccountForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "协约客户销账";
            this.Load += new System.EventHandler(this.MemberSettingForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cus_name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cus_tel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cash;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox note;
    }
}