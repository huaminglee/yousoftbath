namespace MsgPanel
{
    partial class LogPanel
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelMsg = new System.Windows.Forms.Panel();
            this.TextMsg = new System.Windows.Forms.TextBox();
            this.PanelImg = new System.Windows.Forms.Panel();
            this.Picture3 = new System.Windows.Forms.PictureBox();
            this.Picture2 = new System.Windows.Forms.PictureBox();
            this.Picture1 = new System.Windows.Forms.PictureBox();
            this.PanelHead = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.TextToDepart = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TextSendDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TextSender = new System.Windows.Forms.Label();
            this.lable2 = new System.Windows.Forms.Label();
            this.PanelUser = new System.Windows.Forms.Panel();
            this.BtnDone = new System.Windows.Forms.Button();
            this.BtnUrgent = new System.Windows.Forms.Button();
            this.BtnSetTime = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelDueLabel = new System.Windows.Forms.Label();
            this.LabelDue = new System.Windows.Forms.Label();
            this.LabelUrgentLabel = new System.Windows.Forms.Label();
            this.LabelUrgent = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.PanelMsg.SuspendLayout();
            this.PanelImg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).BeginInit();
            this.PanelHead.SuspendLayout();
            this.PanelUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.PanelMsg);
            this.panel1.Controls.Add(this.PanelImg);
            this.panel1.Controls.Add(this.PanelHead);
            this.panel1.Controls.Add(this.PanelUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 590);
            this.panel1.TabIndex = 0;
            // 
            // PanelMsg
            // 
            this.PanelMsg.BackColor = System.Drawing.Color.White;
            this.PanelMsg.Controls.Add(this.TextMsg);
            this.PanelMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMsg.Location = new System.Drawing.Point(0, 389);
            this.PanelMsg.Name = "PanelMsg";
            this.PanelMsg.Size = new System.Drawing.Size(798, 152);
            this.PanelMsg.TabIndex = 3;
            // 
            // TextMsg
            // 
            this.TextMsg.BackColor = System.Drawing.Color.White;
            this.TextMsg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TextMsg.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TextMsg.Location = new System.Drawing.Point(24, 18);
            this.TextMsg.Multiline = true;
            this.TextMsg.Name = "TextMsg";
            this.TextMsg.ReadOnly = true;
            this.TextMsg.Size = new System.Drawing.Size(749, 24);
            this.TextMsg.TabIndex = 0;
            // 
            // PanelImg
            // 
            this.PanelImg.Controls.Add(this.Picture3);
            this.PanelImg.Controls.Add(this.Picture2);
            this.PanelImg.Controls.Add(this.Picture1);
            this.PanelImg.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelImg.Location = new System.Drawing.Point(0, 64);
            this.PanelImg.Name = "PanelImg";
            this.PanelImg.Size = new System.Drawing.Size(798, 325);
            this.PanelImg.TabIndex = 2;
            // 
            // Picture3
            // 
            this.Picture3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture3.Location = new System.Drawing.Point(539, 6);
            this.Picture3.Name = "Picture3";
            this.Picture3.Size = new System.Drawing.Size(260, 313);
            this.Picture3.TabIndex = 0;
            this.Picture3.TabStop = false;
            // 
            // Picture2
            // 
            this.Picture2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture2.Location = new System.Drawing.Point(271, 6);
            this.Picture2.Name = "Picture2";
            this.Picture2.Size = new System.Drawing.Size(260, 313);
            this.Picture2.TabIndex = 0;
            this.Picture2.TabStop = false;
            // 
            // Picture1
            // 
            this.Picture1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Picture1.Location = new System.Drawing.Point(3, 6);
            this.Picture1.Name = "Picture1";
            this.Picture1.Size = new System.Drawing.Size(260, 313);
            this.Picture1.TabIndex = 0;
            this.Picture1.TabStop = false;
            // 
            // PanelHead
            // 
            this.PanelHead.Controls.Add(this.label7);
            this.PanelHead.Controls.Add(this.TextToDepart);
            this.PanelHead.Controls.Add(this.label5);
            this.PanelHead.Controls.Add(this.TextSendDate);
            this.PanelHead.Controls.Add(this.label3);
            this.PanelHead.Controls.Add(this.TextSender);
            this.PanelHead.Controls.Add(this.lable2);
            this.PanelHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelHead.Location = new System.Drawing.Point(0, 0);
            this.PanelHead.Name = "PanelHead";
            this.PanelHead.Size = new System.Drawing.Size(798, 64);
            this.PanelHead.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(0, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(798, 2);
            this.label7.TabIndex = 3;
            // 
            // TextToDepart
            // 
            this.TextToDepart.AutoSize = true;
            this.TextToDepart.Location = new System.Drawing.Point(691, 27);
            this.TextToDepart.Name = "TextToDepart";
            this.TextToDepart.Size = new System.Drawing.Size(62, 18);
            this.TextToDepart.TabIndex = 2;
            this.TextToDepart.Text = "工程部";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(604, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 18);
            this.label5.TabIndex = 2;
            this.label5.Text = "呈交部门：";
            // 
            // TextSendDate
            // 
            this.TextSendDate.AutoSize = true;
            this.TextSendDate.Location = new System.Drawing.Point(369, 27);
            this.TextSendDate.Name = "TextSendDate";
            this.TextSendDate.Size = new System.Drawing.Size(152, 18);
            this.TextSendDate.TabIndex = 2;
            this.TextSendDate.Text = "2014-03-30 08:00";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(282, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "发帖日期：";
            // 
            // TextSender
            // 
            this.TextSender.AutoSize = true;
            this.TextSender.Location = new System.Drawing.Point(97, 27);
            this.TextSender.Name = "TextSender";
            this.TextSender.Size = new System.Drawing.Size(62, 18);
            this.TextSender.TabIndex = 1;
            this.TextSender.Text = "赵飞飞";
            // 
            // lable2
            // 
            this.lable2.AutoSize = true;
            this.lable2.Location = new System.Drawing.Point(21, 27);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(80, 18);
            this.lable2.TabIndex = 0;
            this.lable2.Text = "发帖人：";
            // 
            // PanelUser
            // 
            this.PanelUser.Controls.Add(this.LabelUrgent);
            this.PanelUser.Controls.Add(this.LabelUrgentLabel);
            this.PanelUser.Controls.Add(this.LabelDue);
            this.PanelUser.Controls.Add(this.LabelDueLabel);
            this.PanelUser.Controls.Add(this.BtnDone);
            this.PanelUser.Controls.Add(this.BtnUrgent);
            this.PanelUser.Controls.Add(this.BtnSetTime);
            this.PanelUser.Controls.Add(this.label1);
            this.PanelUser.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelUser.Location = new System.Drawing.Point(0, 541);
            this.PanelUser.Margin = new System.Windows.Forms.Padding(4);
            this.PanelUser.Name = "PanelUser";
            this.PanelUser.Size = new System.Drawing.Size(798, 49);
            this.PanelUser.TabIndex = 0;
            // 
            // BtnDone
            // 
            this.BtnDone.BackColor = System.Drawing.Color.LimeGreen;
            this.BtnDone.Location = new System.Drawing.Point(678, 10);
            this.BtnDone.Name = "BtnDone";
            this.BtnDone.Size = new System.Drawing.Size(95, 29);
            this.BtnDone.TabIndex = 1;
            this.BtnDone.Text = "已完成";
            this.BtnDone.UseVisualStyleBackColor = false;
            this.BtnDone.Click += new System.EventHandler(this.BtnDone_Click);
            // 
            // BtnUrgent
            // 
            this.BtnUrgent.BackColor = System.Drawing.Color.Red;
            this.BtnUrgent.Location = new System.Drawing.Point(577, 10);
            this.BtnUrgent.Name = "BtnUrgent";
            this.BtnUrgent.Size = new System.Drawing.Size(95, 29);
            this.BtnUrgent.TabIndex = 1;
            this.BtnUrgent.Text = "加急";
            this.BtnUrgent.UseVisualStyleBackColor = false;
            this.BtnUrgent.Click += new System.EventHandler(this.BtnUrgent_Click);
            // 
            // BtnSetTime
            // 
            this.BtnSetTime.BackColor = System.Drawing.Color.SkyBlue;
            this.BtnSetTime.Location = new System.Drawing.Point(24, 10);
            this.BtnSetTime.Name = "BtnSetTime";
            this.BtnSetTime.Size = new System.Drawing.Size(135, 29);
            this.BtnSetTime.TabIndex = 1;
            this.BtnSetTime.Text = "设定时间";
            this.BtnSetTime.UseVisualStyleBackColor = false;
            this.BtnSetTime.Click += new System.EventHandler(this.BtnSetTime_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(798, 2);
            this.label1.TabIndex = 0;
            // 
            // LabelDueLabel
            // 
            this.LabelDueLabel.AutoSize = true;
            this.LabelDueLabel.Location = new System.Drawing.Point(165, 16);
            this.LabelDueLabel.Name = "LabelDueLabel";
            this.LabelDueLabel.Size = new System.Drawing.Size(53, 18);
            this.LabelDueLabel.TabIndex = 2;
            this.LabelDueLabel.Text = "到期:";
            // 
            // LabelDue
            // 
            this.LabelDue.AutoSize = true;
            this.LabelDue.Location = new System.Drawing.Point(213, 16);
            this.LabelDue.Name = "LabelDue";
            this.LabelDue.Size = new System.Drawing.Size(152, 18);
            this.LabelDue.TabIndex = 3;
            this.LabelDue.Text = "2014-03-30 08:00";
            // 
            // LabelUrgentLabel
            // 
            this.LabelUrgentLabel.AutoSize = true;
            this.LabelUrgentLabel.Location = new System.Drawing.Point(371, 16);
            this.LabelUrgentLabel.Name = "LabelUrgentLabel";
            this.LabelUrgentLabel.Size = new System.Drawing.Size(53, 18);
            this.LabelUrgentLabel.TabIndex = 2;
            this.LabelUrgentLabel.Text = "加急:";
            // 
            // LabelUrgent
            // 
            this.LabelUrgent.AutoSize = true;
            this.LabelUrgent.Location = new System.Drawing.Point(419, 16);
            this.LabelUrgent.Name = "LabelUrgent";
            this.LabelUrgent.Size = new System.Drawing.Size(152, 18);
            this.LabelUrgent.TabIndex = 3;
            this.LabelUrgent.Text = "2014-03-30 08:00";
            // 
            // LogPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LogPanel";
            this.Size = new System.Drawing.Size(798, 590);
            this.panel1.ResumeLayout(false);
            this.PanelMsg.ResumeLayout(false);
            this.PanelMsg.PerformLayout();
            this.PanelImg.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Picture1)).EndInit();
            this.PanelHead.ResumeLayout(false);
            this.PanelHead.PerformLayout();
            this.PanelUser.ResumeLayout(false);
            this.PanelUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnSetTime;
        private System.Windows.Forms.Button BtnDone;
        private System.Windows.Forms.Button BtnUrgent;
        private System.Windows.Forms.Panel PanelHead;
        private System.Windows.Forms.Label lable2;
        private System.Windows.Forms.Label TextSender;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label TextSendDate;
        private System.Windows.Forms.Label TextToDepart;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel PanelMsg;
        private System.Windows.Forms.Panel PanelImg;
        private System.Windows.Forms.TextBox TextMsg;
        private System.Windows.Forms.PictureBox Picture3;
        private System.Windows.Forms.PictureBox Picture2;
        private System.Windows.Forms.PictureBox Picture1;
        private System.Windows.Forms.Label LabelDueLabel;
        private System.Windows.Forms.Label LabelDue;
        private System.Windows.Forms.Label LabelUrgent;
        private System.Windows.Forms.Label LabelUrgentLabel;
    }
}
