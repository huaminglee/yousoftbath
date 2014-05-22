namespace YouSoftBathBack
{
    partial class ProviderForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.note = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tel = new System.Windows.Forms.TextBox();
            this.address = new System.Windows.Forms.TextBox();
            this.mobile = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contactor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 334);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 51);
            this.panel1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(560, 2);
            this.label4.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(317, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 28);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(143, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(99, 28);
            this.btnOk.TabIndex = 17;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.note);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.tel);
            this.panel2.Controls.Add(this.address);
            this.panel2.Controls.Add(this.mobile);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.contactor);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.name);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 334);
            this.panel2.TabIndex = 14;
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(172, 259);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(338, 27);
            this.note.TabIndex = 6;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 263);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "备注";
            // 
            // tel
            // 
            this.tel.Location = new System.Drawing.Point(172, 118);
            this.tel.Name = "tel";
            this.tel.Size = new System.Drawing.Size(338, 27);
            this.tel.TabIndex = 3;
            this.tel.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(172, 212);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(338, 27);
            this.address.TabIndex = 5;
            this.address.Enter += new System.EventHandler(this.name_Enter);
            // 
            // mobile
            // 
            this.mobile.Location = new System.Drawing.Point(172, 165);
            this.mobile.Name = "mobile";
            this.mobile.Size = new System.Drawing.Size(338, 27);
            this.mobile.TabIndex = 4;
            this.mobile.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(68, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 18);
            this.label8.TabIndex = 25;
            this.label8.Text = "供应商地址";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(68, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 18);
            this.label7.TabIndex = 25;
            this.label7.Text = "供应商电话";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "供应商手机";
            // 
            // contactor
            // 
            this.contactor.Location = new System.Drawing.Point(172, 71);
            this.contactor.Name = "contactor";
            this.contactor.Size = new System.Drawing.Size(338, 27);
            this.contactor.TabIndex = 2;
            this.contactor.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 18);
            this.label6.TabIndex = 21;
            this.label6.Text = "供应商联系人";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(172, 24);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(338, 27);
            this.name.TabIndex = 1;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(68, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "供应商名称";
            // 
            // ProviderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 385);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProviderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供应商信息";
            this.Load += new System.EventHandler(this.SeatTypeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mobile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox contactor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label label8;
    }
}