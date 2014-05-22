namespace YouSoftBathBack
{
    partial class MemberTypeForm
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
            this.label9 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cExpireDate = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.times = new System.Windows.Forms.TextBox();
            this.timespan = new System.Windows.Forms.TextBox();
            this.money = new System.Windows.Forms.TextBox();
            this.maxMoney = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.offer = new System.Windows.Forms.ComboBox();
            this.credits = new System.Windows.Forms.CheckBox();
            this.sms = new System.Windows.Forms.CheckBox();
            this.userOneTime = new System.Windows.Forms.CheckBox();
            this.LimitedTimesPerMonth = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TimesPerMonth = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 305);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(552, 48);
            this.panel1.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Dock = System.Windows.Forms.DockStyle.Top;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(552, 2);
            this.label9.TabIndex = 22;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(364, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 28);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(96, 9);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(90, 28);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // cExpireDate
            // 
            this.cExpireDate.AutoSize = true;
            this.cExpireDate.Location = new System.Drawing.Point(35, 166);
            this.cExpireDate.Name = "cExpireDate";
            this.cExpireDate.Size = new System.Drawing.Size(171, 22);
            this.cExpireDate.TabIndex = 28;
            this.cExpireDate.Text = "是否使用截止日期";
            this.cExpireDate.UseVisualStyleBackColor = true;
            this.cExpireDate.CheckedChanged += new System.EventHandler(this.cExpireDate_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 18);
            this.label4.TabIndex = 21;
            this.label4.Text = "售卡金额";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(255, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 18);
            this.label3.TabIndex = 32;
            this.label3.Text = "月";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "使用期限";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 18);
            this.label5.TabIndex = 30;
            this.label5.Text = "最大开户金额";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(304, 168);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 18);
            this.label18.TabIndex = 19;
            this.label18.Text = "使用次数";
            // 
            // times
            // 
            this.times.Location = new System.Drawing.Point(385, 164);
            this.times.Name = "times";
            this.times.Size = new System.Drawing.Size(137, 27);
            this.times.TabIndex = 25;
            this.times.Enter += new System.EventHandler(this.id_Enter);
            // 
            // timespan
            // 
            this.timespan.Location = new System.Drawing.Point(112, 70);
            this.timespan.Name = "timespan";
            this.timespan.Size = new System.Drawing.Size(137, 27);
            this.timespan.TabIndex = 24;
            this.timespan.Enter += new System.EventHandler(this.id_Enter);
            // 
            // money
            // 
            this.money.Location = new System.Drawing.Point(112, 116);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(137, 27);
            this.money.TabIndex = 26;
            this.money.Enter += new System.EventHandler(this.id_Enter);
            // 
            // maxMoney
            // 
            this.maxMoney.Location = new System.Drawing.Point(385, 212);
            this.maxMoney.Name = "maxMoney";
            this.maxMoney.Size = new System.Drawing.Size(137, 27);
            this.maxMoney.TabIndex = 27;
            this.maxMoney.Enter += new System.EventHandler(this.id_Enter);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(112, 24);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(137, 27);
            this.name.TabIndex = 23;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "类型名称";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 18);
            this.label7.TabIndex = 21;
            this.label7.Text = "优惠方案";
            // 
            // offer
            // 
            this.offer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.offer.FormattingEnabled = true;
            this.offer.Items.AddRange(new object[] {
            ""});
            this.offer.Location = new System.Drawing.Point(112, 213);
            this.offer.Name = "offer";
            this.offer.Size = new System.Drawing.Size(137, 25);
            this.offer.TabIndex = 33;
            // 
            // credits
            // 
            this.credits.AutoSize = true;
            this.credits.Location = new System.Drawing.Point(311, 26);
            this.credits.Name = "credits";
            this.credits.Size = new System.Drawing.Size(99, 22);
            this.credits.TabIndex = 28;
            this.credits.Text = "是否积分";
            this.credits.UseVisualStyleBackColor = true;
            this.credits.CheckedChanged += new System.EventHandler(this.cExpireDate_CheckedChanged);
            // 
            // sms
            // 
            this.sms.AutoSize = true;
            this.sms.Location = new System.Drawing.Point(311, 72);
            this.sms.Name = "sms";
            this.sms.Size = new System.Drawing.Size(225, 22);
            this.sms.TabIndex = 28;
            this.sms.Text = "是否每次消费后发送短信";
            this.sms.UseVisualStyleBackColor = true;
            this.sms.CheckedChanged += new System.EventHandler(this.cExpireDate_CheckedChanged);
            // 
            // userOneTime
            // 
            this.userOneTime.AutoSize = true;
            this.userOneTime.Location = new System.Drawing.Point(311, 118);
            this.userOneTime.Name = "userOneTime";
            this.userOneTime.Size = new System.Drawing.Size(135, 22);
            this.userOneTime.TabIndex = 28;
            this.userOneTime.Text = "一天限用一次";
            this.userOneTime.UseVisualStyleBackColor = true;
            this.userOneTime.CheckedChanged += new System.EventHandler(this.cExpireDate_CheckedChanged);
            // 
            // LimitedTimesPerMonth
            // 
            this.LimitedTimesPerMonth.AutoSize = true;
            this.LimitedTimesPerMonth.Location = new System.Drawing.Point(35, 256);
            this.LimitedTimesPerMonth.Name = "LimitedTimesPerMonth";
            this.LimitedTimesPerMonth.Size = new System.Drawing.Size(171, 22);
            this.LimitedTimesPerMonth.TabIndex = 36;
            this.LimitedTimesPerMonth.Text = "是否每月限用次数";
            this.LimitedTimesPerMonth.UseVisualStyleBackColor = true;
            this.LimitedTimesPerMonth.CheckedChanged += new System.EventHandler(this.LimitedTimesPerMonth_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(268, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 34;
            this.label1.Text = "每月限用次数";
            // 
            // TimesPerMonth
            // 
            this.TimesPerMonth.Enabled = false;
            this.TimesPerMonth.Location = new System.Drawing.Point(385, 254);
            this.TimesPerMonth.Name = "TimesPerMonth";
            this.TimesPerMonth.Size = new System.Drawing.Size(137, 27);
            this.TimesPerMonth.TabIndex = 35;
            // 
            // MemberTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 353);
            this.Controls.Add(this.LimitedTimesPerMonth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TimesPerMonth);
            this.Controls.Add(this.offer);
            this.Controls.Add(this.userOneTime);
            this.Controls.Add(this.sms);
            this.Controls.Add(this.credits);
            this.Controls.Add(this.cExpireDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.times);
            this.Controls.Add(this.timespan);
            this.Controls.Add(this.money);
            this.Controls.Add(this.maxMoney);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemberTypeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会员卡类型";
            this.Load += new System.EventHandler(this.MemberTypeForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cExpireDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox times;
        private System.Windows.Forms.TextBox timespan;
        private System.Windows.Forms.TextBox money;
        private System.Windows.Forms.TextBox maxMoney;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox offer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox credits;
        private System.Windows.Forms.CheckBox sms;
        private System.Windows.Forms.CheckBox userOneTime;
        private System.Windows.Forms.CheckBox LimitedTimesPerMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TimesPerMonth;

    }
}