namespace YouSoftBathBack
{
    partial class SeatTypeForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.depositRequired = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.seatDepart = new System.Windows.Forms.ComboBox();
            this.menuId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.depositMin = new System.Windows.Forms.TextBox();
            this.population = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 226);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 48);
            this.panel1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(644, 2);
            this.label4.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(411, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 26);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(143, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 26);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.depositRequired);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.seatDepart);
            this.groupBox1.Controls.Add(this.menuId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.depositMin);
            this.groupBox1.Controls.Add(this.population);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 185);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // depositRequired
            // 
            this.depositRequired.AutoSize = true;
            this.depositRequired.Location = new System.Drawing.Point(23, 133);
            this.depositRequired.Name = "depositRequired";
            this.depositRequired.Size = new System.Drawing.Size(107, 20);
            this.depositRequired.TabIndex = 14;
            this.depositRequired.Text = "开台交押金";
            this.depositRequired.UseVisualStyleBackColor = true;
            this.depositRequired.CheckedChanged += new System.EventHandler(this.depositRequired_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(346, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "台位部门";
            // 
            // seatDepart
            // 
            this.seatDepart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.seatDepart.FormattingEnabled = true;
            this.seatDepart.Items.AddRange(new object[] {
            "桑拿部",
            "客房部"});
            this.seatDepart.Location = new System.Drawing.Point(420, 80);
            this.seatDepart.Name = "seatDepart";
            this.seatDepart.Size = new System.Drawing.Size(167, 24);
            this.seatDepart.TabIndex = 12;
            // 
            // menuId
            // 
            this.menuId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuId.FormattingEnabled = true;
            this.menuId.Items.AddRange(new object[] {
            ""});
            this.menuId.Location = new System.Drawing.Point(97, 80);
            this.menuId.Name = "menuId";
            this.menuId.Size = new System.Drawing.Size(177, 24);
            this.menuId.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "最低押金金额";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(346, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "容纳人数";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 84);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "绑定编码";
            // 
            // depositMin
            // 
            this.depositMin.Location = new System.Drawing.Point(420, 130);
            this.depositMin.Name = "depositMin";
            this.depositMin.Size = new System.Drawing.Size(167, 26);
            this.depositMin.TabIndex = 1;
            this.depositMin.Text = "100";
            this.depositMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.population_KeyPress);
            this.depositMin.Enter += new System.EventHandler(this.population_Enter);
            // 
            // population
            // 
            this.population.Location = new System.Drawing.Point(420, 30);
            this.population.Name = "population";
            this.population.Size = new System.Drawing.Size(167, 26);
            this.population.TabIndex = 1;
            this.population.Text = "1";
            this.population.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.population_KeyPress);
            this.population.Enter += new System.EventHandler(this.population_Enter);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(97, 30);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(177, 26);
            this.name.TabIndex = 1;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "类型名称";
            // 
            // SeatTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(644, 274);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeatTypeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "台位类型";
            this.Load += new System.EventHandler(this.SeatTypeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox population;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox menuId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox seatDepart;
        private System.Windows.Forms.CheckBox depositRequired;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox depositMin;
    }
}