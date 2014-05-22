namespace YouSoftBathFormClass
{
    partial class RoomViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomViewForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolAvi = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolIn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolFull = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolRoomNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolAviNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolInNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolFullNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.seatId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetSeatIdByRoomNo = new System.Windows.Forms.Button();
            this.txtBoxRoomId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.sp.Panel1.SuspendLayout();
            this.sp.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "20130329080804234_easyicon_net_128.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAvi,
            this.toolIn,
            this.toolFull});
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1362, 35);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolAvi
            // 
            this.toolAvi.Name = "toolAvi";
            this.toolAvi.Size = new System.Drawing.Size(113, 30);
            this.toolAvi.Text = "    空房    ";
            // 
            // toolIn
            // 
            this.toolIn.Name = "toolIn";
            this.toolIn.Size = new System.Drawing.Size(113, 30);
            this.toolIn.Text = "    入住    ";
            // 
            // toolFull
            // 
            this.toolFull.Name = "toolFull";
            this.toolFull.Size = new System.Drawing.Size(113, 30);
            this.toolFull.Text = "    满住    ";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolRoomNumber,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolAviNumber,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel4,
            this.toolInNumber,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel7,
            this.toolFullNumber,
            this.toolStripStatusLabel11});
            this.statusStrip2.Location = new System.Drawing.Point(0, 678);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1362, 29);
            this.statusStrip2.TabIndex = 7;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 24);
            this.toolStripStatusLabel1.Text = "总房间数:";
            // 
            // toolRoomNumber
            // 
            this.toolRoomNumber.Name = "toolRoomNumber";
            this.toolRoomNumber.Size = new System.Drawing.Size(21, 24);
            this.toolRoomNumber.Text = "0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(25, 24);
            this.toolStripStatusLabel2.Text = "   ";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(64, 24);
            this.toolStripStatusLabel3.Text = "空闲：";
            // 
            // toolAviNumber
            // 
            this.toolAviNumber.Name = "toolAviNumber";
            this.toolAviNumber.Size = new System.Drawing.Size(21, 24);
            this.toolAviNumber.Text = "0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(25, 24);
            this.toolStripStatusLabel5.Text = "   ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(64, 24);
            this.toolStripStatusLabel4.Text = "已住：";
            // 
            // toolInNumber
            // 
            this.toolInNumber.Name = "toolInNumber";
            this.toolInNumber.Size = new System.Drawing.Size(21, 24);
            this.toolInNumber.Text = "0";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(25, 24);
            this.toolStripStatusLabel8.Text = "   ";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(64, 24);
            this.toolStripStatusLabel7.Text = "满住：";
            // 
            // toolFullNumber
            // 
            this.toolFullNumber.Name = "toolFullNumber";
            this.toolFullNumber.Size = new System.Drawing.Size(21, 24);
            this.toolFullNumber.Text = "0";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(25, 24);
            this.toolStripStatusLabel11.Text = "   ";
            // 
            // sp
            // 
            this.sp.BackColor = System.Drawing.Color.LightSalmon;
            this.sp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            this.sp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.sp.Panel1.Controls.Add(this.btnGetSeatIdByRoomNo);
            this.sp.Panel1.Controls.Add(this.txtBoxRoomId);
            this.sp.Panel1.Controls.Add(this.label3);
            this.sp.Panel1.Controls.Add(this.btnExit);
            this.sp.Panel1.Controls.Add(this.btnFind);
            this.sp.Panel1.Controls.Add(this.seatId);
            this.sp.Panel1.Controls.Add(this.label2);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.AutoScroll = true;
            this.sp.Panel2.BackColor = System.Drawing.Color.White;
            this.sp.Size = new System.Drawing.Size(1362, 678);
            this.sp.SplitterDistance = 80;
            this.sp.SplitterWidth = 6;
            this.sp.TabIndex = 8;
            this.sp.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = true;
            this.btnExit.Location = new System.Drawing.Point(809, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(85, 51);
            this.btnExit.TabIndex = 6;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.Location = new System.Drawing.Point(222, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(85, 51);
            this.btnFind.TabIndex = 7;
            this.btnFind.TabStop = false;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // seatId
            // 
            this.seatId.Location = new System.Drawing.Point(81, 26);
            this.seatId.Name = "seatId";
            this.seatId.Size = new System.Drawing.Size(122, 26);
            this.seatId.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "手牌号";
            // 
            // btnGetSeatIdByRoomNo
            // 
            this.btnGetSeatIdByRoomNo.AutoSize = true;
            this.btnGetSeatIdByRoomNo.Location = new System.Drawing.Point(601, 14);
            this.btnGetSeatIdByRoomNo.Name = "btnGetSeatIdByRoomNo";
            this.btnGetSeatIdByRoomNo.Size = new System.Drawing.Size(85, 51);
            this.btnGetSeatIdByRoomNo.TabIndex = 10;
            this.btnGetSeatIdByRoomNo.TabStop = false;
            this.btnGetSeatIdByRoomNo.Text = "查询";
            this.btnGetSeatIdByRoomNo.UseVisualStyleBackColor = true;
            this.btnGetSeatIdByRoomNo.Click += new System.EventHandler(this.btnGetSeatIdByRoomNo_Click);
            // 
            // txtBoxRoomId
            // 
            this.txtBoxRoomId.Location = new System.Drawing.Point(460, 26);
            this.txtBoxRoomId.Name = "txtBoxRoomId";
            this.txtBoxRoomId.Size = new System.Drawing.Size(122, 26);
            this.txtBoxRoomId.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(398, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "房间号";
            // 
            // RoomViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1362, 742);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "RoomViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "客房管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RoomManagementForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RoomViewForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomViewForm_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel1.PerformLayout();
            this.sp.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolAvi;
        private System.Windows.Forms.ToolStripStatusLabel toolIn;
        private System.Windows.Forms.ToolStripStatusLabel toolFull;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolRoomNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolAviNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolInNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolFullNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox seatId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetSeatIdByRoomNo;
        private System.Windows.Forms.TextBox txtBoxRoomId;
        private System.Windows.Forms.Label label3;
    }
}