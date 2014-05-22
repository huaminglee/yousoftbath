namespace YouSoftBathBack
{
    partial class CouponForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.menuId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.money = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.issueDate = new System.Windows.Forms.DateTimePicker();
            this.expireDate = new System.Windows.Forms.DateTimePicker();
            this.transator = new System.Windows.Forms.ComboBox();
            this.img = new System.Windows.Forms.Panel();
            this.path = new System.Windows.Forms.TextBox();
            this.btnSel = new System.Windows.Forms.Button();
            this.minAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 547);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 48);
            this.panel1.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Dock = System.Windows.Forms.DockStyle.Top;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(617, 2);
            this.label8.TabIndex = 20;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(433, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 26);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(165, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 26);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // menuId
            // 
            this.menuId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuId.FormattingEnabled = true;
            this.menuId.Items.AddRange(new object[] {
            ""});
            this.menuId.Location = new System.Drawing.Point(86, 277);
            this.menuId.Name = "menuId";
            this.menuId.Size = new System.Drawing.Size(195, 24);
            this.menuId.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 27;
            this.label6.Text = "发 行 人";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 26;
            this.label3.Text = "过期日期";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "发行日期";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 28;
            this.label5.Text = "抵用金额";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(12, 281);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 16);
            this.label18.TabIndex = 18;
            this.label18.Text = "抵用项目";
            // 
            // money
            // 
            this.money.Location = new System.Drawing.Point(86, 72);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(195, 26);
            this.money.TabIndex = 2;
            this.money.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.money.Enter += new System.EventHandler(this.id_Enter);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(86, 225);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(195, 26);
            this.name.TabIndex = 1;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "券类名称";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(86, 21);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(195, 26);
            this.id.TabIndex = 0;
            this.id.TabStop = false;
            this.id.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "券类编号";
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(86, 378);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(195, 26);
            this.note.TabIndex = 7;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 383);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 27;
            this.label7.Text = "备    注";
            // 
            // issueDate
            // 
            this.issueDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.issueDate.Location = new System.Drawing.Point(86, 123);
            this.issueDate.Name = "issueDate";
            this.issueDate.Size = new System.Drawing.Size(195, 26);
            this.issueDate.TabIndex = 4;
            this.issueDate.Enter += new System.EventHandler(this.id_Enter);
            // 
            // expireDate
            // 
            this.expireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.expireDate.Location = new System.Drawing.Point(86, 327);
            this.expireDate.Name = "expireDate";
            this.expireDate.Size = new System.Drawing.Size(195, 26);
            this.expireDate.TabIndex = 5;
            this.expireDate.Enter += new System.EventHandler(this.id_Enter);
            // 
            // transator
            // 
            this.transator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.transator.FormattingEnabled = true;
            this.transator.Location = new System.Drawing.Point(86, 175);
            this.transator.Name = "transator";
            this.transator.Size = new System.Drawing.Size(195, 24);
            this.transator.TabIndex = 6;
            // 
            // img
            // 
            this.img.BackColor = System.Drawing.Color.White;
            this.img.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.img.Location = new System.Drawing.Point(287, 21);
            this.img.Name = "img";
            this.img.Size = new System.Drawing.Size(306, 488);
            this.img.TabIndex = 30;
            // 
            // path
            // 
            this.path.Enabled = false;
            this.path.Location = new System.Drawing.Point(15, 517);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(490, 26);
            this.path.TabIndex = 34;
            // 
            // btnSel
            // 
            this.btnSel.AutoSize = true;
            this.btnSel.Location = new System.Drawing.Point(511, 515);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(82, 26);
            this.btnSel.TabIndex = 33;
            this.btnSel.Text = "选择图片";
            this.btnSel.UseVisualStyleBackColor = true;
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click);
            // 
            // minAmount
            // 
            this.minAmount.Location = new System.Drawing.Point(154, 425);
            this.minAmount.Name = "minAmount";
            this.minAmount.Size = new System.Drawing.Size(127, 26);
            this.minAmount.TabIndex = 7;
            this.minAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.minAmount.Enter += new System.EventHandler(this.id_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 430);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 16);
            this.label9.TabIndex = 27;
            this.label9.Text = "赠送最低消费金额";
            // 
            // CouponForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(617, 595);
            this.Controls.Add(this.path);
            this.Controls.Add(this.btnSel);
            this.Controls.Add(this.img);
            this.Controls.Add(this.expireDate);
            this.Controls.Add(this.issueDate);
            this.Controls.Add(this.transator);
            this.Controls.Add(this.menuId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.minAmount);
            this.Controls.Add(this.note);
            this.Controls.Add(this.money);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CouponForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "优惠券";
            this.Load += new System.EventHandler(this.CouponForm_Load);
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
        private System.Windows.Forms.ComboBox menuId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox money;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker issueDate;
        private System.Windows.Forms.DateTimePicker expireDate;
        private System.Windows.Forms.ComboBox transator;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel img;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button btnSel;
        private System.Windows.Forms.TextBox minAmount;
        private System.Windows.Forms.Label label9;

    }
}