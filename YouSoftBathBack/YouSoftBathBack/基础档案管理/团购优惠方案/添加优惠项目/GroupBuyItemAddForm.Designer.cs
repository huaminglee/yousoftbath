namespace YouSoftBathBack
{
    partial class GroupBuyItemAddForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.discount = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.type = new System.Windows.Forms.ComboBox();
            this.menu = new System.Windows.Forms.ComboBox();
            this.lt = new System.Windows.Forms.Label();
            this.discountType = new System.Windows.Forms.ComboBox();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "项目类别";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "项目名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "项目单价";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(136, 102);
            this.price.Name = "price";
            this.price.ReadOnly = true;
            this.price.Size = new System.Drawing.Size(240, 27);
            this.price.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "折扣形式";
            // 
            // discount
            // 
            this.discount.ImeMode = System.Windows.Forms.ImeMode.On;
            this.discount.Location = new System.Drawing.Point(136, 180);
            this.discount.Name = "discount";
            this.discount.Size = new System.Drawing.Size(240, 27);
            this.discount.TabIndex = 1;
            this.discount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.discount_KeyPress);
            this.discount.Enter += new System.EventHandler(this.discount_Enter);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnOk);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 225);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(426, 48);
            this.panel3.TabIndex = 33;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(426, 2);
            this.label5.TabIndex = 8;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(267, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(59, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // type
            // 
            this.type.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.type.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            ""});
            this.type.Location = new System.Drawing.Point(136, 23);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(240, 25);
            this.type.TabIndex = 35;
            this.type.TextChanged += new System.EventHandler(this.type_TextChanged);
            // 
            // menu
            // 
            this.menu.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.menu.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.menu.FormattingEnabled = true;
            this.menu.Location = new System.Drawing.Point(136, 64);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(240, 25);
            this.menu.TabIndex = 35;
            this.menu.TextChanged += new System.EventHandler(this.menu_TextChanged);
            // 
            // lt
            // 
            this.lt.AutoSize = true;
            this.lt.Location = new System.Drawing.Point(50, 184);
            this.lt.Name = "lt";
            this.lt.Size = new System.Drawing.Size(80, 18);
            this.lt.TabIndex = 0;
            this.lt.Text = "项目折扣";
            // 
            // discountType
            // 
            this.discountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.discountType.FormattingEnabled = true;
            this.discountType.Items.AddRange(new object[] {
            "折扣",
            "折后价"});
            this.discountType.Location = new System.Drawing.Point(136, 142);
            this.discountType.Name = "discountType";
            this.discountType.Size = new System.Drawing.Size(240, 25);
            this.discountType.TabIndex = 35;
            this.discountType.TextChanged += new System.EventHandler(this.discountType_TextChanged);
            // 
            // GroupBuyItemAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(426, 273);
            this.Controls.Add(this.discountType);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.type);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.lt);
            this.Controls.Add(this.discount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.price);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupBuyItemAddForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加优惠项目";
            this.Load += new System.EventHandler(this.PromotionItemAddForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox discount;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox type;
        private System.Windows.Forms.ComboBox menu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lt;
        private System.Windows.Forms.ComboBox discountType;
    }
}