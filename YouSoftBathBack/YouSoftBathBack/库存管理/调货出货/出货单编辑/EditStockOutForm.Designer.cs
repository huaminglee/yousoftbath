namespace YouSoftBathBack
{
    partial class EditStockOutForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtPickerIntoStock = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.ComboUnit = new System.Windows.Forms.ComboBox();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtPickerIntoStock);
            this.groupBox1.Controls.Add(this.label9);
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
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 287);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "编辑出货单";
            // 
            // dtPickerIntoStock
            // 
            this.dtPickerIntoStock.Location = new System.Drawing.Point(341, 106);
            this.dtPickerIntoStock.Name = "dtPickerIntoStock";
            this.dtPickerIntoStock.Size = new System.Drawing.Size(147, 26);
            this.dtPickerIntoStock.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(295, 113);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 39;
            this.label9.Text = "时间";
            // 
            // ComboUnit
            // 
            this.ComboUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboUnit.FormattingEnabled = true;
            this.ComboUnit.Location = new System.Drawing.Point(342, 65);
            this.ComboUnit.Name = "ComboUnit";
            this.ComboUnit.Size = new System.Drawing.Size(146, 24);
            this.ComboUnit.TabIndex = 3;
            // 
            // name
            // 
            this.name.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.name.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.name.FormattingEnabled = true;
            this.name.Location = new System.Drawing.Point(342, 24);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(146, 24);
            this.name.TabIndex = 26;
            // 
            // goodsCat
            // 
            this.goodsCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.goodsCat.FormattingEnabled = true;
            this.goodsCat.Location = new System.Drawing.Point(69, 24);
            this.goodsCat.Name = "goodsCat";
            this.goodsCat.Size = new System.Drawing.Size(146, 24);
            this.goodsCat.TabIndex = 25;
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(69, 106);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(146, 26);
            this.amount.TabIndex = 4;
            this.amount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numberText_KeyPress);
            // 
            // receiver
            // 
            this.receiver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.receiver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.receiver.FormattingEnabled = true;
            this.receiver.Location = new System.Drawing.Point(69, 142);
            this.receiver.Name = "receiver";
            this.receiver.Size = new System.Drawing.Size(146, 24);
            this.receiver.TabIndex = 8;
            // 
            // checker
            // 
            this.checker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.checker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.checker.FormattingEnabled = true;
            this.checker.Location = new System.Drawing.Point(69, 188);
            this.checker.Name = "checker";
            this.checker.Size = new System.Drawing.Size(146, 24);
            this.checker.TabIndex = 10;
            // 
            // transactor
            // 
            this.transactor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.transactor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.transactor.FormattingEnabled = true;
            this.transactor.Location = new System.Drawing.Point(342, 147);
            this.transactor.Name = "transactor";
            this.transactor.Size = new System.Drawing.Size(146, 24);
            this.transactor.TabIndex = 9;
            // 
            // stock
            // 
            this.stock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stock.FormattingEnabled = true;
            this.stock.Location = new System.Drawing.Point(69, 65);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(146, 24);
            this.stock.TabIndex = 2;
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(69, 236);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(419, 26);
            this.note.TabIndex = 11;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(23, 111);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(40, 16);
            this.label18.TabIndex = 16;
            this.label18.Text = "数量";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 241);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "备注";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "类别";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(296, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "单位";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "领料";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(296, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 23;
            this.label2.Text = "名称";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 192);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 16);
            this.label15.TabIndex = 21;
            this.label15.Text = "审核";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(279, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 16);
            this.label14.TabIndex = 22;
            this.label14.Text = "经办人";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 24;
            this.label4.Text = "仓库";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("宋体", 12F);
            this.panel1.Location = new System.Drawing.Point(0, 283);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 48);
            this.panel1.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(534, 2);
            this.label13.TabIndex = 19;
            this.label13.Text = "额外说明";
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(356, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 26);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(88, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 26);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // EditStockOutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(534, 331);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditStockOutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditStockOutForm";
            this.Load += new System.EventHandler(this.EditStockOutForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.DateTimePicker dtPickerIntoStock;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ComboUnit;
        private System.Windows.Forms.ComboBox name;
        private System.Windows.Forms.ComboBox goodsCat;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.ComboBox receiver;
        private System.Windows.Forms.ComboBox checker;
        private System.Windows.Forms.ComboBox transactor;
        private System.Windows.Forms.ComboBox stock;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
    }
}