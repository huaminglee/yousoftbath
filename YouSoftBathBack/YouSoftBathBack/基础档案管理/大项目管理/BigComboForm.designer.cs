namespace YouSoftBathBack
{
    partial class BigComboForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvMenus = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgvsubstItems = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.benLeft3 = new System.Windows.Forms.Button();
            this.cboxCat = new System.Windows.Forms.ComboBox();
            this.btnRight3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBigMenuName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextMenuCat = new System.Windows.Forms.ComboBox();
            this.txtBoxPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenus)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsubstItems)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(602, 49);
            this.panel1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(602, 2);
            this.label4.TabIndex = 14;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.canBtn.Location = new System.Drawing.Point(367, 8);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(119, 32);
            this.canBtn.TabIndex = 10;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.okBtn.Location = new System.Drawing.Point(141, 8);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(119, 32);
            this.okBtn.TabIndex = 9;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(602, 597);
            this.panel2.TabIndex = 21;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.benLeft3);
            this.groupBox4.Controls.Add(this.cboxCat);
            this.groupBox4.Controls.Add(this.btnRight3);
            this.groupBox4.Location = new System.Drawing.Point(13, 153);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(577, 422);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "选择替换项目组合";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "项目类别";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvMenus);
            this.groupBox5.Location = new System.Drawing.Point(21, 74);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(222, 321);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "项目列表";
            // 
            // dgvMenus
            // 
            this.dgvMenus.AllowUserToAddRows = false;
            this.dgvMenus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenus.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMenus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMenus.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvMenus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMenus.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvMenus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMenus.Location = new System.Drawing.Point(3, 22);
            this.dgvMenus.Name = "dgvMenus";
            this.dgvMenus.ReadOnly = true;
            this.dgvMenus.RowHeadersVisible = false;
            this.dgvMenus.RowTemplate.Height = 23;
            this.dgvMenus.Size = new System.Drawing.Size(216, 296);
            this.dgvMenus.TabIndex = 0;
            this.dgvMenus.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "单价";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgvsubstItems);
            this.groupBox6.Location = new System.Drawing.Point(326, 31);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(229, 367);
            this.groupBox6.TabIndex = 8;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "替换项目组合列表";
            // 
            // dgvsubstItems
            // 
            this.dgvsubstItems.AllowUserToAddRows = false;
            this.dgvsubstItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvsubstItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvsubstItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvsubstItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvsubstItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsubstItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvsubstItems.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvsubstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvsubstItems.Location = new System.Drawing.Point(3, 22);
            this.dgvsubstItems.Name = "dgvsubstItems";
            this.dgvsubstItems.ReadOnly = true;
            this.dgvsubstItems.RowHeadersVisible = false;
            this.dgvsubstItems.RowTemplate.Height = 23;
            this.dgvsubstItems.Size = new System.Drawing.Size(223, 342);
            this.dgvsubstItems.TabIndex = 0;
            this.dgvsubstItems.TabStop = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "名称";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "单价";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // benLeft3
            // 
            this.benLeft3.AutoSize = true;
            this.benLeft3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.benLeft3.Location = new System.Drawing.Point(251, 243);
            this.benLeft3.Name = "benLeft3";
            this.benLeft3.Size = new System.Drawing.Size(56, 34);
            this.benLeft3.TabIndex = 17;
            this.benLeft3.Text = "<=";
            this.benLeft3.UseVisualStyleBackColor = true;
            this.benLeft3.Click += new System.EventHandler(this.benLeft3_Click);
            // 
            // cboxCat
            // 
            this.cboxCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxCat.FormattingEnabled = true;
            this.cboxCat.Location = new System.Drawing.Point(92, 31);
            this.cboxCat.Name = "cboxCat";
            this.cboxCat.Size = new System.Drawing.Size(151, 24);
            this.cboxCat.TabIndex = 15;
            this.cboxCat.SelectedIndexChanged += new System.EventHandler(this.cboxCat_SelectedIndexChanged);
            // 
            // btnRight3
            // 
            this.btnRight3.AutoSize = true;
            this.btnRight3.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRight3.Location = new System.Drawing.Point(251, 173);
            this.btnRight3.Name = "btnRight3";
            this.btnRight3.Size = new System.Drawing.Size(56, 34);
            this.btnRight3.TabIndex = 17;
            this.btnRight3.Text = "=>";
            this.btnRight3.UseVisualStyleBackColor = true;
            this.btnRight3.Click += new System.EventHandler(this.btnRight3_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxPrice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextBigMenuName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TextMenuCat);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(569, 125);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择待拆分大项";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(323, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "大项名称";
            // 
            // TextBigMenuName
            // 
            this.TextBigMenuName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TextBigMenuName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TextBigMenuName.FormattingEnabled = true;
            this.TextBigMenuName.Location = new System.Drawing.Point(397, 30);
            this.TextBigMenuName.Name = "TextBigMenuName";
            this.TextBigMenuName.Size = new System.Drawing.Size(151, 24);
            this.TextBigMenuName.TabIndex = 24;
            this.TextBigMenuName.SelectedIndexChanged += new System.EventHandler(this.TextBigMenuName_SelectedIndexChanged);
            this.TextBigMenuName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextMenuCat_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "大项类别";
            // 
            // TextMenuCat
            // 
            this.TextMenuCat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TextMenuCat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TextMenuCat.FormattingEnabled = true;
            this.TextMenuCat.Location = new System.Drawing.Point(94, 30);
            this.TextMenuCat.Name = "TextMenuCat";
            this.TextMenuCat.Size = new System.Drawing.Size(151, 24);
            this.TextMenuCat.TabIndex = 23;
            this.TextMenuCat.SelectedIndexChanged += new System.EventHandler(this.TextMenuCat_SelectedIndexChanged);
            this.TextMenuCat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextMenuCat_KeyPress);
            // 
            // txtBoxPrice
            // 
            this.txtBoxPrice.Enabled = false;
            this.txtBoxPrice.Location = new System.Drawing.Point(93, 80);
            this.txtBoxPrice.Name = "txtBoxPrice";
            this.txtBoxPrice.Size = new System.Drawing.Size(151, 26);
            this.txtBoxPrice.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 27;
            this.label3.Text = "项目单价";
            // 
            // BigComboForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.canBtn;
            this.ClientSize = new System.Drawing.Size(602, 646);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BigComboForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增套餐";
            this.Load += new System.EventHandler(this.ComboForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenus)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsubstItems)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvMenus;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvsubstItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button benLeft3;
        private System.Windows.Forms.ComboBox cboxCat;
        private System.Windows.Forms.Button btnRight3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TextBigMenuName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TextMenuCat;
        private System.Windows.Forms.TextBox txtBoxPrice;
        private System.Windows.Forms.Label label3;
    }
}