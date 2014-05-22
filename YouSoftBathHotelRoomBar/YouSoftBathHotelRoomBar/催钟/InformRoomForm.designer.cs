namespace YouSoftBathReception
{
    partial class InformRoomForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformRoomForm));
            this.pr = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAvi = new System.Windows.Forms.Button();
            this.btnFini = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnReserve = new System.Windows.Forms.Button();
            this.btnOn = new System.Windows.Forms.Button();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnWait = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pr
            // 
            this.pr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pr.Location = new System.Drawing.Point(0, 100);
            this.pr.Name = "pr";
            this.pr.Size = new System.Drawing.Size(1370, 650);
            this.pr.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnAvi);
            this.panel1.Controls.Add(this.btnFini);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnReserve);
            this.panel1.Controls.Add(this.btnOn);
            this.panel1.Controls.Add(this.btnIn);
            this.panel1.Controls.Add(this.btnWait);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 100);
            this.panel1.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.AutoSize = true;
            this.btnExit.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnExit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.ImageIndex = 0;
            this.btnExit.ImageList = this.imageList1;
            this.btnExit.Location = new System.Drawing.Point(637, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(94, 90);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "退出";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.toolLogout_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "20130329080804234_easyicon_net_128.png");
            // 
            // btnAvi
            // 
            this.btnAvi.Enabled = false;
            this.btnAvi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAvi.Location = new System.Drawing.Point(11, 15);
            this.btnAvi.Name = "btnAvi";
            this.btnAvi.Size = new System.Drawing.Size(50, 50);
            this.btnAvi.TabIndex = 30;
            this.btnAvi.UseVisualStyleBackColor = true;
            // 
            // btnFini
            // 
            this.btnFini.Enabled = false;
            this.btnFini.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFini.Location = new System.Drawing.Point(351, 15);
            this.btnFini.Name = "btnFini";
            this.btnFini.Size = new System.Drawing.Size(50, 50);
            this.btnFini.TabIndex = 33;
            this.btnFini.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "空闲";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "等待清洁";
            // 
            // btnReserve
            // 
            this.btnReserve.Enabled = false;
            this.btnReserve.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReserve.Location = new System.Drawing.Point(215, 15);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(50, 50);
            this.btnReserve.TabIndex = 32;
            this.btnReserve.UseVisualStyleBackColor = true;
            // 
            // btnOn
            // 
            this.btnOn.Enabled = false;
            this.btnOn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOn.Location = new System.Drawing.Point(283, 15);
            this.btnOn.Name = "btnOn";
            this.btnOn.Size = new System.Drawing.Size(50, 50);
            this.btnOn.TabIndex = 35;
            this.btnOn.UseVisualStyleBackColor = true;
            // 
            // btnIn
            // 
            this.btnIn.Enabled = false;
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIn.Location = new System.Drawing.Point(79, 15);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(50, 50);
            this.btnIn.TabIndex = 34;
            this.btnIn.UseVisualStyleBackColor = true;
            // 
            // btnWait
            // 
            this.btnWait.Enabled = false;
            this.btnWait.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWait.Location = new System.Drawing.Point(147, 15);
            this.btnWait.Name = "btnWait";
            this.btnWait.Size = new System.Drawing.Size(50, 50);
            this.btnWait.TabIndex = 31;
            this.btnWait.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(84, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "入住";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(204, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 25;
            this.label4.Text = "预约服务";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "等待服务";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "服务中";
            // 
            // RoomViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 750);
            this.Controls.Add(this.pr);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "客房管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RoomManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomViewForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnAvi;
        private System.Windows.Forms.Button btnFini;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnReserve;
        private System.Windows.Forms.Button btnOn;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnWait;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}