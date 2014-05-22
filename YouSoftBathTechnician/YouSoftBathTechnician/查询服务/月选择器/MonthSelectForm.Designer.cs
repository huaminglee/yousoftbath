namespace YouSoftBathTechnician
{
    partial class MonthSelectForm
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
            this.year = new System.Windows.Forms.TextBox();
            this.month = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // year
            // 
            this.year.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.year.Location = new System.Drawing.Point(86, 69);
            this.year.Name = "year";
            this.year.Size = new System.Drawing.Size(110, 53);
            this.year.TabIndex = 1;
            this.year.Text = "2013";
            this.year.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.year.Enter += new System.EventHandler(this.box_Enter);
            // 
            // month
            // 
            this.month.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.month.Location = new System.Drawing.Point(212, 69);
            this.month.Name = "month";
            this.month.Size = new System.Drawing.Size(110, 53);
            this.month.TabIndex = 2;
            this.month.Text = "07";
            this.month.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.month.Enter += new System.EventHandler(this.box_Enter);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(86, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 51);
            this.button1.TabIndex = 1;
            this.button1.TabStop = false;
            this.button1.Text = "+";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAddYear_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(86, 128);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(110, 51);
            this.button2.TabIndex = 1;
            this.button2.TabStop = false;
            this.button2.Text = "-";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnMinusYear_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(212, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(110, 51);
            this.button3.TabIndex = 1;
            this.button3.TabStop = false;
            this.button3.Text = "+";
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btnAddMonth_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.Location = new System.Drawing.Point(212, 128);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(110, 51);
            this.button5.TabIndex = 1;
            this.button5.TabStop = false;
            this.button5.Text = "-";
            this.button5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.btnMinusMonth_Click);
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.Location = new System.Drawing.Point(36, 208);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(110, 51);
            this.button7.TabIndex = 1;
            this.button7.TabStop = false;
            this.button7.Text = "1";
            this.button7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.Location = new System.Drawing.Point(152, 208);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(110, 51);
            this.button8.TabIndex = 1;
            this.button8.TabStop = false;
            this.button8.Text = "2";
            this.button8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button9.Location = new System.Drawing.Point(268, 208);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(110, 51);
            this.button9.TabIndex = 1;
            this.button9.TabStop = false;
            this.button9.Text = "3";
            this.button9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button10.Location = new System.Drawing.Point(36, 265);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(110, 51);
            this.button10.TabIndex = 1;
            this.button10.TabStop = false;
            this.button10.Text = "4";
            this.button10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button11.Location = new System.Drawing.Point(152, 265);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(110, 51);
            this.button11.TabIndex = 1;
            this.button11.TabStop = false;
            this.button11.Text = "5";
            this.button11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button12.Location = new System.Drawing.Point(268, 265);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(110, 51);
            this.button12.TabIndex = 1;
            this.button12.TabStop = false;
            this.button12.Text = "6";
            this.button12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button13.Location = new System.Drawing.Point(36, 322);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(110, 51);
            this.button13.TabIndex = 1;
            this.button13.TabStop = false;
            this.button13.Text = "7";
            this.button13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button14
            // 
            this.button14.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button14.Location = new System.Drawing.Point(152, 322);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(110, 51);
            this.button14.TabIndex = 1;
            this.button14.TabStop = false;
            this.button14.Text = "8";
            this.button14.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button15
            // 
            this.button15.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button15.Location = new System.Drawing.Point(268, 322);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(110, 51);
            this.button15.TabIndex = 1;
            this.button15.TabStop = false;
            this.button15.Text = "9";
            this.button15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button16
            // 
            this.button16.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button16.Location = new System.Drawing.Point(36, 379);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(110, 51);
            this.button16.TabIndex = 1;
            this.button16.TabStop = false;
            this.button16.Text = "0";
            this.button16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.BtnNumber_Click);
            // 
            // button17
            // 
            this.button17.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button17.Location = new System.Drawing.Point(152, 379);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(110, 51);
            this.button17.TabIndex = 1;
            this.button17.TabStop = false;
            this.button17.Text = "回删";
            this.button17.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnOk
            // 
            this.btnOk.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(268, 379);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(110, 51);
            this.btnOk.TabIndex = 1;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "确定";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // DaySelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(192)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(409, 446);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.button15);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button17);
            this.Controls.Add(this.button16);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.month);
            this.Controls.Add(this.year);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DaySelectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "技师查询";
            this.Load += new System.EventHandler(this.SeatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox year;
        private System.Windows.Forms.TextBox month;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button btnOk;

    }
}