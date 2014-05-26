namespace IntereekBathBackService
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnCalcel = new System.Windows.Forms.Button();
            this.BtnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtBoxBaudRate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.CheckCloud = new System.Windows.Forms.CheckBox();
            this.CheckServer = new System.Windows.Forms.CheckBox();
            this.CheckSMS = new System.Windows.Forms.CheckBox();
            this.CheckAuto = new System.Windows.Forms.CheckBox();
            this.CheckDetect = new System.Windows.Forms.CheckBox();
            this.GroupDetect = new System.Windows.Forms.GroupBox();
            this.TextMoneyLimit = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TextTimeLimit = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NotifyServer = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.GroupDetect.SuspendLayout();
            this.ContextNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnCalcel);
            this.panel1.Controls.Add(this.BtnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 321);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 68);
            this.panel1.TabIndex = 0;
            // 
            // BtnCalcel
            // 
            this.BtnCalcel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCalcel.Location = new System.Drawing.Point(268, 18);
            this.BtnCalcel.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCalcel.Name = "BtnCalcel";
            this.BtnCalcel.Size = new System.Drawing.Size(172, 33);
            this.BtnCalcel.TabIndex = 2;
            this.BtnCalcel.TabStop = false;
            this.BtnCalcel.Text = "退出";
            this.BtnCalcel.UseVisualStyleBackColor = true;
            this.BtnCalcel.Click += new System.EventHandler(this.BtnCalcel_Click);
            // 
            // BtnOk
            // 
            this.BtnOk.Location = new System.Drawing.Point(27, 18);
            this.BtnOk.Margin = new System.Windows.Forms.Padding(4);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(172, 33);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "最小化到系统托盘";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(467, 2);
            this.label1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtBoxBaudRate);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbComPort);
            this.panel2.Controls.Add(this.CheckCloud);
            this.panel2.Controls.Add(this.CheckServer);
            this.panel2.Controls.Add(this.CheckSMS);
            this.panel2.Controls.Add(this.CheckAuto);
            this.panel2.Controls.Add(this.CheckDetect);
            this.panel2.Controls.Add(this.GroupDetect);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(467, 321);
            this.panel2.TabIndex = 1;
            // 
            // txtBoxBaudRate
            // 
            this.txtBoxBaudRate.Enabled = false;
            this.txtBoxBaudRate.Location = new System.Drawing.Point(300, 272);
            this.txtBoxBaudRate.Name = "txtBoxBaudRate";
            this.txtBoxBaudRate.Size = new System.Drawing.Size(126, 27);
            this.txtBoxBaudRate.TabIndex = 11;
            this.txtBoxBaudRate.Text = "9600";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "波特率";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 276);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "端口号 ";
            // 
            // cmbComPort
            // 
            this.cmbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPort.Enabled = false;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.cmbComPort.Location = new System.Drawing.Point(97, 273);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(121, 25);
            this.cmbComPort.TabIndex = 9;
            // 
            // CheckCloud
            // 
            this.CheckCloud.AutoSize = true;
            this.CheckCloud.Checked = true;
            this.CheckCloud.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckCloud.Enabled = false;
            this.CheckCloud.Location = new System.Drawing.Point(234, 189);
            this.CheckCloud.Name = "CheckCloud";
            this.CheckCloud.Size = new System.Drawing.Size(207, 22);
            this.CheckCloud.TabIndex = 7;
            this.CheckCloud.Text = "开启连客科技后台服务";
            this.CheckCloud.UseVisualStyleBackColor = true;
            // 
            // CheckServer
            // 
            this.CheckServer.AutoSize = true;
            this.CheckServer.Location = new System.Drawing.Point(234, 23);
            this.CheckServer.Name = "CheckServer";
            this.CheckServer.Size = new System.Drawing.Size(225, 22);
            this.CheckServer.TabIndex = 8;
            this.CheckServer.Text = "开启连客科技服务器服务";
            this.CheckServer.UseVisualStyleBackColor = true;
            // 
            // CheckSMS
            // 
            this.CheckSMS.AutoSize = true;
            this.CheckSMS.Location = new System.Drawing.Point(25, 231);
            this.CheckSMS.Name = "CheckSMS";
            this.CheckSMS.Size = new System.Drawing.Size(135, 22);
            this.CheckSMS.TabIndex = 8;
            this.CheckSMS.Text = "开启短信控制";
            this.CheckSMS.UseVisualStyleBackColor = true;
            this.CheckSMS.CheckedChanged += new System.EventHandler(this.CheckSMS_CheckedChanged);
            // 
            // CheckAuto
            // 
            this.CheckAuto.AutoSize = true;
            this.CheckAuto.Location = new System.Drawing.Point(27, 189);
            this.CheckAuto.Name = "CheckAuto";
            this.CheckAuto.Size = new System.Drawing.Size(135, 22);
            this.CheckAuto.TabIndex = 8;
            this.CheckAuto.Text = "开启自动续费";
            this.CheckAuto.UseVisualStyleBackColor = true;
            // 
            // CheckDetect
            // 
            this.CheckDetect.AutoSize = true;
            this.CheckDetect.Location = new System.Drawing.Point(12, 23);
            this.CheckDetect.Name = "CheckDetect";
            this.CheckDetect.Size = new System.Drawing.Size(207, 22);
            this.CheckDetect.TabIndex = 6;
            this.CheckDetect.Text = "自动检测异常消费手牌";
            this.CheckDetect.UseVisualStyleBackColor = true;
            this.CheckDetect.CheckedChanged += new System.EventHandler(this.CheckDetect_CheckedChanged);
            // 
            // GroupDetect
            // 
            this.GroupDetect.Controls.Add(this.TextMoneyLimit);
            this.GroupDetect.Controls.Add(this.label9);
            this.GroupDetect.Controls.Add(this.TextTimeLimit);
            this.GroupDetect.Controls.Add(this.label19);
            this.GroupDetect.Controls.Add(this.label18);
            this.GroupDetect.Controls.Add(this.label17);
            this.GroupDetect.Controls.Add(this.label8);
            this.GroupDetect.Location = new System.Drawing.Point(7, 66);
            this.GroupDetect.Name = "GroupDetect";
            this.GroupDetect.Size = new System.Drawing.Size(434, 107);
            this.GroupDetect.TabIndex = 5;
            this.GroupDetect.TabStop = false;
            this.GroupDetect.Text = "异常条件设置";
            // 
            // TextMoneyLimit
            // 
            this.TextMoneyLimit.Location = new System.Drawing.Point(211, 61);
            this.TextMoneyLimit.Name = "TextMoneyLimit";
            this.TextMoneyLimit.Size = new System.Drawing.Size(116, 27);
            this.TextMoneyLimit.TabIndex = 16;
            this.TextMoneyLimit.TabStop = false;
            this.TextMoneyLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextTimeLimit_KeyPress);
            this.TextMoneyLimit.Enter += new System.EventHandler(this.TextTimeLimit_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(95, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 18);
            this.label9.TabIndex = 15;
            this.label9.Text = "消费金额超过";
            // 
            // TextTimeLimit
            // 
            this.TextTimeLimit.Location = new System.Drawing.Point(211, 26);
            this.TextTimeLimit.Name = "TextTimeLimit";
            this.TextTimeLimit.Size = new System.Drawing.Size(116, 27);
            this.TextTimeLimit.TabIndex = 17;
            this.TextTimeLimit.TabStop = false;
            this.TextTimeLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextTimeLimit_KeyPress);
            this.TextTimeLimit.Enter += new System.EventHandler(this.TextTimeLimit_Enter);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(330, 66);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(26, 18);
            this.label19.TabIndex = 14;
            this.label19.Text = "元";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(330, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 18);
            this.label18.TabIndex = 12;
            this.label18.Text = "小时";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(95, 31);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(116, 18);
            this.label17.TabIndex = 13;
            this.label17.Text = "在场时间超过";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 18);
            this.label8.TabIndex = 11;
            this.label8.Text = "异常条件：";
            // 
            // NotifyServer
            // 
            this.NotifyServer.ContextMenuStrip = this.ContextNotify;
            this.NotifyServer.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyServer.Icon")));
            this.NotifyServer.Text = "连客科技后台服务";
            this.NotifyServer.Visible = true;
            this.NotifyServer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyServer_MouseDoubleClick);
            // 
            // ContextNotify
            // 
            this.ContextNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.ContextNotify.Name = "ContextNotify";
            this.ContextNotify.Size = new System.Drawing.Size(101, 48);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCalcel;
            this.ClientSize = new System.Drawing.Size(467, 389);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连客科技后台服务";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.GroupDetect.ResumeLayout(false);
            this.GroupDetect.PerformLayout();
            this.ContextNotify.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox CheckCloud;
        private System.Windows.Forms.CheckBox CheckAuto;
        private System.Windows.Forms.CheckBox CheckDetect;
        private System.Windows.Forms.GroupBox GroupDetect;
        private System.Windows.Forms.TextBox TextMoneyLimit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TextTimeLimit;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCalcel;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.CheckBox CheckServer;
        private System.Windows.Forms.NotifyIcon NotifyServer;
        private System.Windows.Forms.ContextMenuStrip ContextNotify;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.CheckBox CheckSMS;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.TextBox txtBoxBaudRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;

    }
}

