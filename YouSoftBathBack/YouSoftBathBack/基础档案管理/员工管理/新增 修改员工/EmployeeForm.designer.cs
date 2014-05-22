namespace YouSoftBathBack
{
    partial class EmployeeForm
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
            this.label13 = new System.Windows.Forms.Label();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.address = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.salary = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.idCard = new System.Windows.Forms.TextBox();
            this.note = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.birthday = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.entryDate = new System.Windows.Forms.DateTimePicker();
            this.job = new System.Windows.Forms.ComboBox();
            this.gender = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.onDuty = new System.Windows.Forms.CheckBox();
            this.phone = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cardId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 409);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 48);
            this.panel1.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(644, 2);
            this.label13.TabIndex = 19;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(410, 10);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(90, 26);
            this.canBtn.TabIndex = 18;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(142, 10);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(90, 26);
            this.okBtn.TabIndex = 17;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.address);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.salary);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.email);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.idCard);
            this.groupBox2.Controls.Add(this.note);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(12, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(616, 207);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其他信息";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(97, 120);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(499, 26);
            this.address.TabIndex = 10;
            this.address.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 0;
            this.label11.Text = "员工地址";
            // 
            // salary
            // 
            this.salary.Location = new System.Drawing.Point(97, 30);
            this.salary.Name = "salary";
            this.salary.Size = new System.Drawing.Size(100, 26);
            this.salary.TabIndex = 7;
            this.salary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.salary_KeyPress);
            this.salary.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "员工薪水";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(400, 75);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(196, 26);
            this.email.TabIndex = 8;
            this.email.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(324, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "员工邮件";
            // 
            // idCard
            // 
            this.idCard.Location = new System.Drawing.Point(97, 75);
            this.idCard.Name = "idCard";
            this.idCard.Size = new System.Drawing.Size(202, 26);
            this.idCard.TabIndex = 9;
            this.idCard.Enter += new System.EventHandler(this.id_Enter);
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(97, 165);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(499, 26);
            this.note.TabIndex = 11;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 0;
            this.label9.Text = "身份证号";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(23, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "额外说明";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.birthday);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.entryDate);
            this.groupBox1.Controls.Add(this.job);
            this.groupBox1.Controls.Add(this.gender);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.onDuty);
            this.groupBox1.Controls.Add(this.phone);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cardId);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.id);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 173);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "必填信息";
            // 
            // birthday
            // 
            this.birthday.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.birthday.Location = new System.Drawing.Point(294, 128);
            this.birthday.Name = "birthday";
            this.birthday.Size = new System.Drawing.Size(100, 26);
            this.birthday.TabIndex = 16;
            this.birthday.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(220, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 15;
            this.label10.Text = "员工生日";
            // 
            // entryDate
            // 
            this.entryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.entryDate.Location = new System.Drawing.Point(294, 79);
            this.entryDate.Name = "entryDate";
            this.entryDate.Size = new System.Drawing.Size(100, 26);
            this.entryDate.TabIndex = 13;
            this.entryDate.Enter += new System.EventHandler(this.id_Enter);
            // 
            // job
            // 
            this.job.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.job.FormattingEnabled = true;
            this.job.Location = new System.Drawing.Point(97, 80);
            this.job.Name = "job";
            this.job.Size = new System.Drawing.Size(113, 24);
            this.job.TabIndex = 12;
            // 
            // gender
            // 
            this.gender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gender.FormattingEnabled = true;
            this.gender.Items.AddRange(new object[] {
            "男",
            "女"});
            this.gender.Location = new System.Drawing.Point(483, 80);
            this.gender.Name = "gender";
            this.gender.Size = new System.Drawing.Size(100, 24);
            this.gender.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "员工职位";
            // 
            // onDuty
            // 
            this.onDuty.AutoSize = true;
            this.onDuty.Checked = true;
            this.onDuty.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onDuty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.onDuty.Location = new System.Drawing.Point(409, 134);
            this.onDuty.Name = "onDuty";
            this.onDuty.Size = new System.Drawing.Size(91, 20);
            this.onDuty.TabIndex = 8;
            this.onDuty.Text = "在职状态";
            this.onDuty.UseVisualStyleBackColor = true;
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(97, 128);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(113, 26);
            this.phone.TabIndex = 4;
            this.phone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phone_KeyPress);
            this.phone.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(220, 84);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "入职日期";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "员工电话";
            // 
            // cardId
            // 
            this.cardId.Location = new System.Drawing.Point(483, 30);
            this.cardId.Name = "cardId";
            this.cardId.Size = new System.Drawing.Size(100, 26);
            this.cardId.TabIndex = 1;
            this.cardId.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(409, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "员工工卡";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(294, 30);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(100, 26);
            this.name.TabIndex = 1;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "员工姓名";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(409, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "员工性别";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(97, 30);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(113, 26);
            this.id.TabIndex = 1;
            this.id.TabStop = false;
            this.id.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "员工编号";
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 457);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EmployeeForm";
            this.ShowInTaskbar = false;
            this.Text = "新增员工";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox salary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox idCard;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker entryDate;
        private System.Windows.Forms.ComboBox job;
        private System.Windows.Forms.ComboBox gender;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox onDuty;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker birthday;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox cardId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
    }
}