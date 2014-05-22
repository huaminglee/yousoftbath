namespace YouSoftBathBack
{
    partial class ProviderPaysForm
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
            this.bank = new System.Windows.Forms.TextBox();
            this.confirmer = new System.Windows.Forms.TextBox();
            this.receiver = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.payer = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cash = new System.Windows.Forms.TextBox();
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
            this.panel1.Location = new System.Drawing.Point(0, 389);
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
            this.panel2.Controls.Add(this.bank);
            this.panel2.Controls.Add(this.confirmer);
            this.panel2.Controls.Add(this.receiver);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.payer);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cash);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.name);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(560, 389);
            this.panel2.TabIndex = 14;
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(163, 318);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(338, 27);
            this.note.TabIndex = 6;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(113, 322);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 26;
            this.label5.Text = "备注";
            // 
            // bank
            // 
            this.bank.Location = new System.Drawing.Point(163, 122);
            this.bank.Name = "bank";
            this.bank.Size = new System.Drawing.Size(338, 27);
            this.bank.TabIndex = 3;
            this.bank.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // confirmer
            // 
            this.confirmer.Location = new System.Drawing.Point(163, 269);
            this.confirmer.Name = "confirmer";
            this.confirmer.Size = new System.Drawing.Size(338, 27);
            this.confirmer.TabIndex = 5;
            this.confirmer.Enter += new System.EventHandler(this.name_Enter);
            // 
            // receiver
            // 
            this.receiver.Location = new System.Drawing.Point(163, 220);
            this.receiver.Name = "receiver";
            this.receiver.Size = new System.Drawing.Size(338, 27);
            this.receiver.TabIndex = 5;
            this.receiver.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 25;
            this.label1.Text = "确认人";
            // 
            // payer
            // 
            this.payer.Location = new System.Drawing.Point(163, 171);
            this.payer.Name = "payer";
            this.payer.Size = new System.Drawing.Size(338, 27);
            this.payer.TabIndex = 4;
            this.payer.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(95, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 25;
            this.label8.Text = "签收人";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(113, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 18);
            this.label7.TabIndex = 25;
            this.label7.Text = "银联";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 25;
            this.label3.Text = "支付人";
            // 
            // cash
            // 
            this.cash.Location = new System.Drawing.Point(163, 73);
            this.cash.Name = "cash";
            this.cash.Size = new System.Drawing.Size(338, 27);
            this.cash.TabIndex = 2;
            this.cash.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 21;
            this.label6.Text = "现金";
            // 
            // name
            // 
            this.name.Enabled = false;
            this.name.Location = new System.Drawing.Point(163, 19);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(338, 27);
            this.name.TabIndex = 1;
            this.name.TabStop = false;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "供应商名称";
            // 
            // ProviderPaysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(560, 440);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProviderPaysForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "供应商付款信息";
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
        private System.Windows.Forms.TextBox payer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bank;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox cash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox receiver;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox confirmer;
        private System.Windows.Forms.Label label1;
    }
}