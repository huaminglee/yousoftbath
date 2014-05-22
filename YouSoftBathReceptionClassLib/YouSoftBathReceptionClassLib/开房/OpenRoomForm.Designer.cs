namespace YouSoftBathReception
{
    partial class OpenRoomForm
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
            this.CheckZhong = new System.Windows.Forms.CheckBox();
            this.days = new System.Windows.Forms.DateTimePicker();
            this.phone = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBank = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.deposit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.roomNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_reserve = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.TextDays = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.CheckZhong);
            this.panel3.Controls.Add(this.days);
            this.panel3.Controls.Add(this.phone);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.name);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.TextBank);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.TextDays);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.deposit);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.roomNumber);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(418, 400);
            this.panel3.TabIndex = 28;
            // 
            // CheckZhong
            // 
            this.CheckZhong.AutoSize = true;
            this.CheckZhong.Location = new System.Drawing.Point(87, 347);
            this.CheckZhong.Name = "CheckZhong";
            this.CheckZhong.Size = new System.Drawing.Size(81, 22);
            this.CheckZhong.TabIndex = 5;
            this.CheckZhong.Text = "钟点房";
            this.CheckZhong.UseVisualStyleBackColor = true;
            // 
            // days
            // 
            this.days.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.days.Location = new System.Drawing.Point(152, 103);
            this.days.Name = "days";
            this.days.Size = new System.Drawing.Size(200, 27);
            this.days.TabIndex = 3;
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(152, 295);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(200, 27);
            this.phone.TabIndex = 4;
            this.phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatBox_KeyPress);
            this.phone.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(102, 299);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "电话";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(152, 247);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(200, 27);
            this.name.TabIndex = 2;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(102, 251);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "姓名";
            // 
            // TextBank
            // 
            this.TextBank.Location = new System.Drawing.Point(152, 199);
            this.TextBank.Name = "TextBank";
            this.TextBank.Size = new System.Drawing.Size(200, 27);
            this.TextBank.TabIndex = 1;
            this.TextBank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatBox_KeyPress);
            this.TextBank.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(66, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "银联预授";
            // 
            // deposit
            // 
            this.deposit.Location = new System.Drawing.Point(152, 151);
            this.deposit.Name = "deposit";
            this.deposit.Size = new System.Drawing.Size(200, 27);
            this.deposit.TabIndex = 1;
            this.deposit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatBox_KeyPress);
            this.deposit.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 155);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "现金押金";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(210, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 18);
            this.label6.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "截止时间";
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(42, 8);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 28);
            this.btnOk.TabIndex = 26;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 8);
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
            this.panel1.Controls.Add(this.btn_reserve);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 400);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 45);
            this.panel1.TabIndex = 26;
            // 
            // btn_reserve
            // 
            this.btn_reserve.AutoSize = true;
            this.btn_reserve.Location = new System.Drawing.Point(169, 8);
            this.btn_reserve.Name = "btn_reserve";
            this.btn_reserve.Size = new System.Drawing.Size(81, 28);
            this.btn_reserve.TabIndex = 26;
            this.btn_reserve.TabStop = false;
            this.btn_reserve.Text = "预定";
            this.btn_reserve.UseVisualStyleBackColor = true;
            this.btn_reserve.Click += new System.EventHandler(this.btn_reserve_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "开房天数";
            // 
            // TextDays
            // 
            this.TextDays.Location = new System.Drawing.Point(152, 54);
            this.TextDays.Name = "TextDays";
            this.TextDays.Size = new System.Drawing.Size(124, 27);
            this.TextDays.TabIndex = 1;
            this.TextDays.Text = "1";
            this.TextDays.TextChanged += new System.EventHandler(this.TextDays_TextChanged);
            this.TextDays.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.seatBox_KeyPress);
            this.TextDays.Enter += new System.EventHandler(this.seatBox_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(282, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "天";
            // 
            // OpenRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(418, 445);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenRoomForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "开房";
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
        private System.Windows.Forms.TextBox deposit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox roomNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker days;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_reserve;
        private System.Windows.Forms.TextBox TextBank;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox CheckZhong;
        private System.Windows.Forms.TextBox TextDays;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;

    }
}