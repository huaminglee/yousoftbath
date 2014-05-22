namespace YouSoftBathBack
{
    partial class StockInForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.unit = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.goodsCat = new System.Windows.Forms.ComboBox();
            this.name = new System.Windows.Forms.ComboBox();
            this.checker = new System.Windows.Forms.ComboBox();
            this.provider = new System.Windows.Forms.ComboBox();
            this.transactor = new System.Windows.Forms.ComboBox();
            this.stock = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.money = new System.Windows.Forms.TextBox();
            this.cost = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.amount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 344);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(543, 48);
            this.panel1.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(543, 2);
            this.label13.TabIndex = 19;
            this.label13.Text = "额外说明";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(360, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 26);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(92, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 26);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.unit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.goodsCat);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.checker);
            this.groupBox1.Controls.Add(this.provider);
            this.groupBox1.Controls.Add(this.transactor);
            this.groupBox1.Controls.Add(this.stock);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.note);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.money);
            this.groupBox1.Controls.Add(this.cost);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.amount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 325);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "进货单";
            // 
            // unit
            // 
            this.unit.FormattingEnabled = true;
            this.unit.Location = new System.Drawing.Point(330, 77);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(146, 24);
            this.unit.TabIndex = 14;
            this.unit.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(256, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "单    位";
            // 
            // goodsCat
            // 
            this.goodsCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.goodsCat.FormattingEnabled = true;
            this.goodsCat.Location = new System.Drawing.Point(57, 26);
            this.goodsCat.Name = "goodsCat";
            this.goodsCat.Size = new System.Drawing.Size(146, 24);
            this.goodsCat.TabIndex = 12;
            this.goodsCat.SelectedIndexChanged += new System.EventHandler(this.goodsCat_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.name.FormattingEnabled = true;
            this.name.Location = new System.Drawing.Point(330, 26);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(146, 24);
            this.name.TabIndex = 12;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // checker
            // 
            this.checker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.checker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.checker.FormattingEnabled = true;
            this.checker.Location = new System.Drawing.Point(57, 230);
            this.checker.Name = "checker";
            this.checker.Size = new System.Drawing.Size(146, 24);
            this.checker.TabIndex = 11;
            this.checker.Enter += new System.EventHandler(this.name_Enter);
            // 
            // provider
            // 
            this.provider.FormattingEnabled = true;
            this.provider.Location = new System.Drawing.Point(330, 128);
            this.provider.Name = "provider";
            this.provider.Size = new System.Drawing.Size(146, 24);
            this.provider.TabIndex = 11;
            this.provider.Enter += new System.EventHandler(this.name_Enter);
            // 
            // transactor
            // 
            this.transactor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.transactor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.transactor.FormattingEnabled = true;
            this.transactor.Location = new System.Drawing.Point(330, 230);
            this.transactor.Name = "transactor";
            this.transactor.Size = new System.Drawing.Size(146, 24);
            this.transactor.TabIndex = 11;
            this.transactor.Enter += new System.EventHandler(this.name_Enter);
            // 
            // stock
            // 
            this.stock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stock.FormattingEnabled = true;
            this.stock.Location = new System.Drawing.Point(57, 77);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(146, 24);
            this.stock.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "供 应 商";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(256, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 9;
            this.label6.Text = "金    额";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "进价";
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(57, 280);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(419, 26);
            this.note.TabIndex = 4;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 132);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "备注";
            // 
            // money
            // 
            this.money.Location = new System.Drawing.Point(330, 178);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(146, 26);
            this.money.TabIndex = 2;
            this.money.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cost_KeyPress);
            this.money.Enter += new System.EventHandler(this.amount_Enter);
            // 
            // cost
            // 
            this.cost.Location = new System.Drawing.Point(57, 178);
            this.cost.Name = "cost";
            this.cost.Size = new System.Drawing.Size(146, 26);
            this.cost.TabIndex = 2;
            this.cost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cost_KeyPress);
            this.cost.Enter += new System.EventHandler(this.amount_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 0;
            this.label7.Text = "类别";
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(57, 127);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(146, 26);
            this.amount.TabIndex = 3;
            this.amount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.amount_KeyPress);
            this.amount.Enter += new System.EventHandler(this.amount_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "名    称";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 234);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 0;
            this.label15.Text = "审核";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(256, 234);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "经 办 人";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "仓库";
            // 
            // StockInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(543, 392);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockInForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录入进货单";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
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
        private System.Windows.Forms.ComboBox stock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox transactor;
        private System.Windows.Forms.ComboBox checker;
        private System.Windows.Forms.TextBox cost;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox provider;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox money;
        private System.Windows.Forms.ComboBox name;
        private System.Windows.Forms.ComboBox goodsCat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox unit;
        private System.Windows.Forms.Label label8;
    }
}