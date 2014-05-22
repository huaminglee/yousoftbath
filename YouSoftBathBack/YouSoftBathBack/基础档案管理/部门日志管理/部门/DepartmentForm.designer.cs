namespace YouSoftBathBack
{
    partial class DepartmentForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TextName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextNote = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 51);
            this.panel1.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Dock = System.Windows.Forms.DockStyle.Top;
            this.label13.Location = new System.Drawing.Point(0, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(424, 2);
            this.label13.TabIndex = 19;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(252, 11);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(101, 28);
            this.canBtn.TabIndex = 18;
            this.canBtn.TabStop = false;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(71, 11);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(101, 28);
            this.okBtn.TabIndex = 17;
            this.okBtn.TabStop = false;
            this.okBtn.Text = "确定";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 11;
            this.label1.Text = "部门名称";
            // 
            // TextName
            // 
            this.TextName.Location = new System.Drawing.Point(150, 38);
            this.TextName.Name = "TextName";
            this.TextName.Size = new System.Drawing.Size(193, 27);
            this.TextName.TabIndex = 1;
            this.TextName.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 11;
            this.label2.Text = "部门备注";
            // 
            // TextNote
            // 
            this.TextNote.Location = new System.Drawing.Point(150, 84);
            this.TextNote.Name = "TextNote";
            this.TextNote.Size = new System.Drawing.Size(193, 27);
            this.TextNote.TabIndex = 2;
            this.TextNote.Enter += new System.EventHandler(this.name_Enter);
            // 
            // DepartmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 213);
            this.Controls.Add(this.TextNote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DepartmentForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "部门";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextNote;
    }
}