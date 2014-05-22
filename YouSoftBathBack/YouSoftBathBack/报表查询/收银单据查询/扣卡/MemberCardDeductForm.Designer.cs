namespace YouSoftBathBack
{
    partial class MemberCardDeductForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.balance = new System.Windows.Forms.TextBox();
            this.type = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.memberId = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCard = new System.Windows.Forms.Button();
            this.phone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 46;
            this.label4.Text = "会员余额";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 48;
            this.label2.Text = "会员类型";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 47;
            this.label1.Text = "会员姓名";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 44;
            this.label5.Text = "会员卡号";
            // 
            // balance
            // 
            this.balance.Location = new System.Drawing.Point(135, 160);
            this.balance.Name = "balance";
            this.balance.ReadOnly = true;
            this.balance.Size = new System.Drawing.Size(296, 27);
            this.balance.TabIndex = 40;
            // 
            // type
            // 
            this.type.Location = new System.Drawing.Point(135, 123);
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Size = new System.Drawing.Size(296, 27);
            this.type.TabIndex = 41;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(135, 49);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(296, 27);
            this.name.TabIndex = 43;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // memberId
            // 
            this.memberId.Location = new System.Drawing.Point(135, 12);
            this.memberId.Name = "memberId";
            this.memberId.Size = new System.Drawing.Size(296, 27);
            this.memberId.TabIndex = 42;
            this.memberId.Enter += new System.EventHandler(this.memberId_Enter);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnCard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 206);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(472, 58);
            this.panel1.TabIndex = 49;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ImageIndex = 2;
            this.btnCancel.Location = new System.Drawing.Point(288, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(116, 40);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCard
            // 
            this.btnCard.AutoSize = true;
            this.btnCard.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCard.ImageIndex = 2;
            this.btnCard.Location = new System.Drawing.Point(69, 9);
            this.btnCard.Name = "btnCard";
            this.btnCard.Size = new System.Drawing.Size(116, 40);
            this.btnCard.TabIndex = 25;
            this.btnCard.Text = "保存";
            this.btnCard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCard.UseVisualStyleBackColor = true;
            this.btnCard.Click += new System.EventHandler(this.btnCard_Click);
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(135, 86);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(296, 27);
            this.phone.TabIndex = 41;
            this.phone.Enter += new System.EventHandler(this.memberId_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 48;
            this.label6.Text = "会员电话";
            // 
            // MemberCardDeductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(472, 264);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.balance);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.type);
            this.Controls.Add(this.name);
            this.Controls.Add(this.memberId);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemberCardDeductForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "会员扣卡";
            this.Load += new System.EventHandler(this.MemberCardDeductForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MemberCardDeductForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox balance;
        private System.Windows.Forms.TextBox type;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox memberId;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCard;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label label6;
    }
}