namespace YouSoftBathBack
{
    partial class StockOutForm
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
            this.name = new System.Windows.Forms.ComboBox();
            this.goodsCat = new System.Windows.Forms.ComboBox();
            this.amount = new System.Windows.Forms.TextBox();
            this.receiver = new System.Windows.Forms.ComboBox();
            this.checker = new System.Windows.Forms.ComboBox();
            this.transactor = new System.Windows.Forms.ComboBox();
            this.stock = new System.Windows.Forms.ComboBox();
            this.note = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboUnit = new System.Windows.Forms.ComboBox();
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
            this.panel1.Location = new System.Drawing.Point(0, 292);
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
            this.groupBox1.Controls.Add(this.ComboUnit);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.goodsCat);
            this.groupBox1.Controls.Add(this.amount);
            this.groupBox1.Controls.Add(this.receiver);
            this.groupBox1.Controls.Add(this.checker);
            this.groupBox1.Controls.Add(this.transactor);
            this.groupBox1.Controls.Add(this.stock);
            this.groupBox1.Controls.Add(this.note);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 269);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "出货单";
            // 
            // name
            // 
            this.name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.name.FormattingEnabled = true;
            this.name.Location = new System.Drawing.Point(333, 26);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(146, 24);
            this.name.TabIndex = 14;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // goodsCat
            // 
            this.goodsCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.goodsCat.FormattingEnabled = true;
            this.goodsCat.Location = new System.Drawing.Point(60, 26);
            this.goodsCat.Name = "goodsCat";
            this.goodsCat.Size = new System.Drawing.Size(146, 24);
            this.goodsCat.TabIndex = 13;
            this.goodsCat.SelectedIndexChanged += new System.EventHandler(this.goodsCat_SelectedIndexChanged);
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(60, 74);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(146, 26);
            this.amount.TabIndex = 12;
            this.amount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.amount_KeyPress);
            this.amount.Enter += new System.EventHandler(this.amount_Enter);
            // 
            // receiver
            // 
            this.receiver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.receiver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.receiver.FormattingEnabled = true;
            this.receiver.Location = new System.Drawing.Point(60, 173);
            this.receiver.Name = "receiver";
            this.receiver.Size = new System.Drawing.Size(146, 24);
            this.receiver.TabIndex = 11;
            this.receiver.Enter += new System.EventHandler(this.name_Enter);
            // 
            // checker
            // 
            this.checker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.checker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.checker.FormattingEnabled = true;
            this.checker.Location = new System.Drawing.Point(333, 173);
            this.checker.Name = "checker";
            this.checker.Size = new System.Drawing.Size(146, 24);
            this.checker.TabIndex = 11;
            this.checker.Enter += new System.EventHandler(this.name_Enter);
            // 
            // transactor
            // 
            this.transactor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.transactor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.transactor.FormattingEnabled = true;
            this.transactor.Location = new System.Drawing.Point(333, 124);
            this.transactor.Name = "transactor";
            this.transactor.Size = new System.Drawing.Size(146, 24);
            this.transactor.TabIndex = 11;
            this.transactor.Enter += new System.EventHandler(this.name_Enter);
            // 
            // stock
            // 
            this.stock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stock.FormattingEnabled = true;
            this.stock.Location = new System.Drawing.Point(60, 124);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(146, 24);
            this.stock.TabIndex = 11;
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(60, 221);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(419, 26);
            this.note.TabIndex = 4;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 79);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 0;
            this.label18.Text = "数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "备注";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "类别";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "领料";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(287, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "名称";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(287, 177);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 0;
            this.label15.Text = "审核";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(255, 128);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(72, 16);
            this.label14.TabIndex = 0;
            this.label14.Text = "经 办 人";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "仓库";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(287, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 0;
            this.label5.Text = "单位";
            // 
            // ComboUnit
            // 
            this.ComboUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboUnit.FormattingEnabled = true;
            this.ComboUnit.Location = new System.Drawing.Point(333, 71);
            this.ComboUnit.Name = "ComboUnit";
            this.ComboUnit.Size = new System.Drawing.Size(146, 24);
            this.ComboUnit.TabIndex = 14;
            this.ComboUnit.Enter += new System.EventHandler(this.name_Enter);
            // 
            // StockOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(543, 340);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StockOutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "录入出货单";
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
        private System.Windows.Forms.ComboBox receiver;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox name;
        private System.Windows.Forms.ComboBox goodsCat;
        private System.Windows.Forms.ComboBox ComboUnit;
        private System.Windows.Forms.Label label5;
    }
}