namespace YouSoftBathFormClass
{
    partial class ReserveOverDueForm
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.days = new System.Windows.Forms.DateTimePicker();
            this.phone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.roomNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_reserve = new System.Windows.Forms.Button();
            this.btn_cancel_reserve = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.days);
            this.panel3.Controls.Add(this.phone);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.name);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.roomNumber);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(418, 209);
            this.panel3.TabIndex = 28;
            // 
            // days
            // 
            this.days.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.days.Location = new System.Drawing.Point(152, 58);
            this.days.Name = "days";
            this.days.Size = new System.Drawing.Size(200, 27);
            this.days.TabIndex = 3;
            this.days.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(152, 150);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(200, 27);
            this.phone.TabIndex = 4;
            this.phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatBox_KeyPress);
            this.phone.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(102, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "电话";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(152, 104);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(200, 27);
            this.name.TabIndex = 2;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "姓名";
            // 
            // roomNumber
            // 
            this.roomNumber.Location = new System.Drawing.Point(152, 12);
            this.roomNumber.Name = "roomNumber";
            this.roomNumber.ReadOnly = true;
            this.roomNumber.Size = new System.Drawing.Size(200, 27);
            this.roomNumber.TabIndex = 1;
            this.roomNumber.TabStop = false;
            this.roomNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatBox_KeyPress);
            this.roomNumber.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(84, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "房间号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "截止时间";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(304, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 28);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 2);
            this.label1.TabIndex = 28;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btn_cancel_reserve);
            this.panel1.Controls.Add(this.btn_reserve);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 209);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 45);
            this.panel1.TabIndex = 26;
            // 
            // btn_reserve
            // 
            this.btn_reserve.AutoSize = true;
            this.btn_reserve.Location = new System.Drawing.Point(34, 8);
            this.btn_reserve.Name = "btn_reserve";
            this.btn_reserve.Size = new System.Drawing.Size(81, 28);
            this.btn_reserve.TabIndex = 26;
            this.btn_reserve.TabStop = false;
            this.btn_reserve.Text = "续订";
            this.btn_reserve.UseVisualStyleBackColor = true;
            this.btn_reserve.Click += new System.EventHandler(this.btn_reserve_Click);
            // 
            // btn_cancel_reserve
            // 
            this.btn_cancel_reserve.AutoSize = true;
            this.btn_cancel_reserve.Location = new System.Drawing.Point(169, 8);
            this.btn_cancel_reserve.Name = "btn_cancel_reserve";
            this.btn_cancel_reserve.Size = new System.Drawing.Size(81, 28);
            this.btn_cancel_reserve.TabIndex = 26;
            this.btn_cancel_reserve.TabStop = false;
            this.btn_cancel_reserve.Text = "退订";
            this.btn_cancel_reserve.UseVisualStyleBackColor = true;
            this.btn_cancel_reserve.Click += new System.EventHandler(this.btn_cancel_reserve_Click);
            // 
            // ReserveOverDueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(418, 254);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReserveOverDueForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "房间续订";
            this.Load += new System.EventHandler(this.OpenSeatForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenSeatForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpenSeatForm_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox roomNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker days;
        private System.Windows.Forms.Button btn_reserve;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_cancel_reserve;

    }
}