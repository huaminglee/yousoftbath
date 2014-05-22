namespace YouSoftBathBack
{
    partial class CustomerForm
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
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.address = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.mobile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.qq = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.note = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.contact = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 18);
            this.label5.TabIndex = 35;
            this.label5.Text = "联系人";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 209);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 33;
            this.label3.Text = "电话";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(63, 161);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 18);
            this.label18.TabIndex = 32;
            this.label18.Text = "地址";
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(110, 205);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(192, 27);
            this.phone.TabIndex = 29;
            this.phone.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(110, 157);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(192, 27);
            this.address.TabIndex = 28;
            this.address.Enter += new System.EventHandler(this.name_Enter);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(110, 61);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(192, 27);
            this.name.TabIndex = 27;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(63, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = "名称";
            // 
            // id
            // 
            this.id.Location = new System.Drawing.Point(110, 13);
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Size = new System.Drawing.Size(192, 27);
            this.id.TabIndex = 26;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 34;
            this.label1.Text = "编号";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 271);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 51);
            this.panel1.TabIndex = 30;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(379, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(145, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(117, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定(Enter)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // mobile
            // 
            this.mobile.Location = new System.Drawing.Point(390, 13);
            this.mobile.Name = "mobile";
            this.mobile.Size = new System.Drawing.Size(192, 27);
            this.mobile.TabIndex = 29;
            this.mobile.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(343, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 33;
            this.label4.Text = "手机";
            // 
            // fax
            // 
            this.fax.Location = new System.Drawing.Point(390, 61);
            this.fax.Name = "fax";
            this.fax.Size = new System.Drawing.Size(192, 27);
            this.fax.TabIndex = 29;
            this.fax.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(343, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 18);
            this.label6.TabIndex = 33;
            this.label6.Text = "传真";
            // 
            // qq
            // 
            this.qq.Location = new System.Drawing.Point(390, 109);
            this.qq.Name = "qq";
            this.qq.Size = new System.Drawing.Size(192, 27);
            this.qq.TabIndex = 29;
            this.qq.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(361, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 18);
            this.label7.TabIndex = 33;
            this.label7.Text = "QQ";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(390, 157);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(192, 27);
            this.email.TabIndex = 29;
            this.email.Enter += new System.EventHandler(this.mobile_Enter);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(343, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 33;
            this.label8.Text = "邮件";
            // 
            // note
            // 
            this.note.Location = new System.Drawing.Point(390, 205);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(192, 27);
            this.note.TabIndex = 29;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(343, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 18);
            this.label9.TabIndex = 33;
            this.label9.Text = "备注";
            // 
            // contact
            // 
            this.contact.Location = new System.Drawing.Point(110, 109);
            this.contact.Name = "contact";
            this.contact.Size = new System.Drawing.Size(192, 27);
            this.contact.TabIndex = 27;
            this.contact.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(626, 2);
            this.label10.TabIndex = 8;
            this.label10.Text = "label10";
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(626, 322);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.note);
            this.Controls.Add(this.email);
            this.Controls.Add(this.qq);
            this.Controls.Add(this.fax);
            this.Controls.Add(this.mobile);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.address);
            this.Controls.Add(this.contact);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客户";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox mobile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox fax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox qq;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox contact;
        private System.Windows.Forms.Label label10;
    }
}