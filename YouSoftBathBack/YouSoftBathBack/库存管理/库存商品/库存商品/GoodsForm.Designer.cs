namespace YouSoftBathBack
{
    partial class GoodsForm
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
            this.GroupStock = new System.Windows.Forms.GroupBox();
            this.stockNote = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.provider = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.money = new System.Windows.Forms.TextBox();
            this.cost = new System.Windows.Forms.TextBox();
            this.amount = new System.Windows.Forms.TextBox();
            this.stock = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.note = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.minAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.catId = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ComboUnit = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GroupStock.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 412);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 51);
            this.panel1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(707, 2);
            this.label4.TabIndex = 19;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 12);
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
            this.btnOk.Location = new System.Drawing.Point(216, 12);
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
            this.panel2.Controls.Add(this.GroupStock);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(707, 412);
            this.panel2.TabIndex = 14;
            // 
            // GroupStock
            // 
            this.GroupStock.Controls.Add(this.ComboUnit);
            this.GroupStock.Controls.Add(this.stockNote);
            this.GroupStock.Controls.Add(this.label10);
            this.GroupStock.Controls.Add(this.provider);
            this.GroupStock.Controls.Add(this.label7);
            this.GroupStock.Controls.Add(this.label11);
            this.GroupStock.Controls.Add(this.label8);
            this.GroupStock.Controls.Add(this.label9);
            this.GroupStock.Controls.Add(this.label18);
            this.GroupStock.Controls.Add(this.money);
            this.GroupStock.Controls.Add(this.cost);
            this.GroupStock.Controls.Add(this.amount);
            this.GroupStock.Controls.Add(this.stock);
            this.GroupStock.Controls.Add(this.label6);
            this.GroupStock.Location = new System.Drawing.Point(12, 176);
            this.GroupStock.Name = "GroupStock";
            this.GroupStock.Size = new System.Drawing.Size(683, 217);
            this.GroupStock.TabIndex = 1;
            this.GroupStock.TabStop = false;
            this.GroupStock.Text = "商品初始库存";
            // 
            // stockNote
            // 
            this.stockNote.Location = new System.Drawing.Point(76, 167);
            this.stockNote.Name = "stockNote";
            this.stockNote.Size = new System.Drawing.Size(564, 27);
            this.stockNote.TabIndex = 23;
            this.stockNote.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 171);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 18);
            this.label10.TabIndex = 22;
            this.label10.Text = "备注";
            // 
            // provider
            // 
            this.provider.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.provider.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.provider.FormattingEnabled = true;
            this.provider.Location = new System.Drawing.Point(424, 42);
            this.provider.Name = "provider";
            this.provider.Size = new System.Drawing.Size(216, 25);
            this.provider.TabIndex = 21;
            this.provider.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(338, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 18;
            this.label7.Text = "供 应 商";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 129);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 19;
            this.label8.Text = "金额";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(370, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 18);
            this.label9.TabIndex = 20;
            this.label9.Text = "进价";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(26, 87);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 18);
            this.label18.TabIndex = 14;
            this.label18.Text = "数量";
            // 
            // money
            // 
            this.money.Location = new System.Drawing.Point(76, 125);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(173, 27);
            this.money.TabIndex = 15;
            this.money.Enter += new System.EventHandler(this.minAmount_Enter);
            // 
            // cost
            // 
            this.cost.Location = new System.Drawing.Point(424, 83);
            this.cost.Name = "cost";
            this.cost.Size = new System.Drawing.Size(216, 27);
            this.cost.TabIndex = 16;
            this.cost.Enter += new System.EventHandler(this.minAmount_Enter);
            // 
            // amount
            // 
            this.amount.Location = new System.Drawing.Point(76, 83);
            this.amount.Name = "amount";
            this.amount.Size = new System.Drawing.Size(173, 27);
            this.amount.TabIndex = 17;
            this.amount.Enter += new System.EventHandler(this.minAmount_Enter);
            // 
            // stock
            // 
            this.stock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stock.FormattingEnabled = true;
            this.stock.Location = new System.Drawing.Point(76, 42);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(173, 25);
            this.stock.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "仓库";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.note);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.minAmount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.catId);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(683, 144);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "商品信息";
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(467, 89);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(173, 27);
            this.note.TabIndex = 32;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(417, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 34;
            this.label5.Text = "备注";
            // 
            // minAmount
            // 
            this.minAmount.Location = new System.Drawing.Point(119, 89);
            this.minAmount.Name = "minAmount";
            this.minAmount.Size = new System.Drawing.Size(173, 27);
            this.minAmount.TabIndex = 31;
            this.minAmount.Enter += new System.EventHandler(this.minAmount_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 33;
            this.label3.Text = "最低库存";
            // 
            // catId
            // 
            this.catId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.catId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.catId.FormattingEnabled = true;
            this.catId.Location = new System.Drawing.Point(467, 40);
            this.catId.Name = "catId";
            this.catId.Size = new System.Drawing.Size(173, 25);
            this.catId.TabIndex = 30;
            this.catId.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(381, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 28;
            this.label1.Text = "商品类别";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(119, 39);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(173, 27);
            this.name.TabIndex = 27;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 29;
            this.label2.Text = "商品名称";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(374, 129);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 18);
            this.label11.TabIndex = 19;
            this.label11.Text = "单位";
            // 
            // ComboUnit
            // 
            this.ComboUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ComboUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ComboUnit.FormattingEnabled = true;
            this.ComboUnit.Location = new System.Drawing.Point(424, 126);
            this.ComboUnit.Name = "ComboUnit";
            this.ComboUnit.Size = new System.Drawing.Size(216, 25);
            this.ComboUnit.TabIndex = 31;
            // 
            // GoodsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(707, 463);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GoodsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "库存商品信息";
            this.Load += new System.EventHandler(this.SeatTypeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.GroupStock.ResumeLayout(false);
            this.GroupStock.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox minAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox catId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GroupStock;
        private System.Windows.Forms.ComboBox stock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox provider;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox money;
        private System.Windows.Forms.TextBox cost;
        private System.Windows.Forms.TextBox amount;
        private System.Windows.Forms.TextBox stockNote;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox ComboUnit;
    }
}