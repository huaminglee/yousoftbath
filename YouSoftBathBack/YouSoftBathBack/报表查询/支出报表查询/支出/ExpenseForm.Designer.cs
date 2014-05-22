namespace YouSoftBathBack
{
    partial class ExpenseForm
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
            this.label11 = new System.Windows.Forms.Label();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.expenseDate = new System.Windows.Forms.DateTimePicker();
            this.checker = new System.Windows.Forms.ComboBox();
            this.tableMaker = new System.Windows.Forms.ComboBox();
            this.transactor = new System.Windows.Forms.ComboBox();
            this.payType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.type = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.money = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toWhom = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 297);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(603, 52);
            this.panel1.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(603, 2);
            this.label11.TabIndex = 16;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(370, 11);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(99, 28);
            this.canBtn.TabIndex = 3;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(132, 11);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(99, 28);
            this.okBtn.TabIndex = 2;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.expenseDate);
            this.groupBox1.Controls.Add(this.checker);
            this.groupBox1.Controls.Add(this.tableMaker);
            this.groupBox1.Controls.Add(this.transactor);
            this.groupBox1.Controls.Add(this.payType);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.type);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.money);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.note);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.toWhom);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 265);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // expenseDate
            // 
            this.expenseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.expenseDate.Location = new System.Drawing.Point(376, 188);
            this.expenseDate.Name = "expenseDate";
            this.expenseDate.Size = new System.Drawing.Size(190, 27);
            this.expenseDate.TabIndex = 38;
            // 
            // checker
            // 
            this.checker.FormattingEnabled = true;
            this.checker.Location = new System.Drawing.Point(86, 189);
            this.checker.Name = "checker";
            this.checker.Size = new System.Drawing.Size(190, 25);
            this.checker.TabIndex = 33;
            this.checker.Enter += new System.EventHandler(this.money_Enter);
            // 
            // tableMaker
            // 
            this.tableMaker.FormattingEnabled = true;
            this.tableMaker.Location = new System.Drawing.Point(376, 149);
            this.tableMaker.Name = "tableMaker";
            this.tableMaker.Size = new System.Drawing.Size(190, 25);
            this.tableMaker.TabIndex = 34;
            this.tableMaker.Enter += new System.EventHandler(this.money_Enter);
            // 
            // transactor
            // 
            this.transactor.FormattingEnabled = true;
            this.transactor.Location = new System.Drawing.Point(86, 149);
            this.transactor.Name = "transactor";
            this.transactor.Size = new System.Drawing.Size(190, 25);
            this.transactor.TabIndex = 37;
            this.transactor.Enter += new System.EventHandler(this.money_Enter);
            // 
            // payType
            // 
            this.payType.FormattingEnabled = true;
            this.payType.Items.AddRange(new object[] {
            "现金",
            "银行转账",
            "支票"});
            this.payType.Location = new System.Drawing.Point(86, 109);
            this.payType.Name = "payType";
            this.payType.Size = new System.Drawing.Size(190, 25);
            this.payType.TabIndex = 36;
            this.payType.Enter += new System.EventHandler(this.money_Enter);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(293, 192);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 26;
            this.label12.Text = "支出日期";
            // 
            // type
            // 
            this.type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type.FormattingEnabled = true;
            this.type.Location = new System.Drawing.Point(86, 69);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(190, 25);
            this.type.TabIndex = 35;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(293, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 18);
            this.label10.TabIndex = 26;
            this.label10.Text = "制    表";
            // 
            // money
            // 
            this.money.Location = new System.Drawing.Point(376, 68);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(190, 27);
            this.money.TabIndex = 31;
            this.money.Enter += new System.EventHandler(this.money_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 25;
            this.label4.Text = "金    额";
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(86, 228);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(480, 27);
            this.note.TabIndex = 32;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "备    注";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 19;
            this.label6.Text = "经 手 人";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "类    别";
            // 
            // toWhom
            // 
            this.toWhom.Location = new System.Drawing.Point(376, 108);
            this.toWhom.Name = "toWhom";
            this.toWhom.Size = new System.Drawing.Size(190, 27);
            this.toWhom.TabIndex = 30;
            this.toWhom.Enter += new System.EventHandler(this.name_Enter);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(86, 28);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(190, 27);
            this.name.TabIndex = 28;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(293, 112);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 21;
            this.label9.Text = "付款对象";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "审    核";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 23;
            this.label2.Text = "名    称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 22;
            this.label5.Text = "付款方式";
            // 
            // ExpenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.canBtn;
            this.ClientSize = new System.Drawing.Size(603, 349);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExpenseForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "支出";
            this.Load += new System.EventHandler(this.ExpenseForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox checker;
        private System.Windows.Forms.ComboBox tableMaker;
        private System.Windows.Forms.ComboBox transactor;
        private System.Windows.Forms.ComboBox payType;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox money;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox toWhom;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker expenseDate;
    }
}