namespace YouSoftBathFormClass
{
    partial class TechnicianSeclectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechnicianSeclectForm));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ctx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.下班ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTechNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOffNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolAviNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOnNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOrderNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel14 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel13 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOverNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolOff = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolAvi = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOrder = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolOver = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.techTypes = new System.Windows.Forms.ComboBox();
            this.tPanel = new System.Windows.Forms.Panel();
            this.btnClock = new System.Windows.Forms.Button();
            this.ctx.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "sel_tick2.png");
            // 
            // ctx
            // 
            this.ctx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下班ToolStripMenuItem});
            this.ctx.Name = "ctx";
            this.ctx.Size = new System.Drawing.Size(113, 26);
            // 
            // 下班ToolStripMenuItem
            // 
            this.下班ToolStripMenuItem.Name = "下班ToolStripMenuItem";
            this.下班ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.下班ToolStripMenuItem.Text = "上下班";
            this.下班ToolStripMenuItem.Click += new System.EventHandler(this.下班ToolStripMenuItem_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolTechNumber,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolOffNumber,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel4,
            this.toolAviNumber,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel7,
            this.toolOnNumber,
            this.toolStripStatusLabel11,
            this.toolStripStatusLabel10,
            this.toolOrderNumber,
            this.toolStripStatusLabel14,
            this.toolStripStatusLabel13,
            this.toolOverNumber});
            this.statusStrip2.Location = new System.Drawing.Point(0, 466);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(883, 29);
            this.statusStrip2.TabIndex = 9;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(86, 24);
            this.toolStripStatusLabel1.Text = "总技师数:";
            // 
            // toolTechNumber
            // 
            this.toolTechNumber.Name = "toolTechNumber";
            this.toolTechNumber.Size = new System.Drawing.Size(21, 24);
            this.toolTechNumber.Text = "0";
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
            this.toolStripStatusLabel3.Text = "下班：";
            // 
            // toolOffNumber
            // 
            this.toolOffNumber.Name = "toolOffNumber";
            this.toolOffNumber.Size = new System.Drawing.Size(21, 24);
            this.toolOffNumber.Text = "0";
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
            this.toolStripStatusLabel4.Text = "空闲：";
            // 
            // toolAviNumber
            // 
            this.toolAviNumber.Name = "toolAviNumber";
            this.toolAviNumber.Size = new System.Drawing.Size(21, 24);
            this.toolAviNumber.Text = "0";
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
            this.toolStripStatusLabel7.Text = "上钟：";
            // 
            // toolOnNumber
            // 
            this.toolOnNumber.Name = "toolOnNumber";
            this.toolOnNumber.Size = new System.Drawing.Size(21, 24);
            this.toolOnNumber.Text = "0";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(25, 24);
            this.toolStripStatusLabel11.Text = "   ";
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(64, 24);
            this.toolStripStatusLabel10.Text = "点钟：";
            // 
            // toolOrderNumber
            // 
            this.toolOrderNumber.Name = "toolOrderNumber";
            this.toolOrderNumber.Size = new System.Drawing.Size(21, 24);
            this.toolOrderNumber.Text = "0";
            // 
            // toolStripStatusLabel14
            // 
            this.toolStripStatusLabel14.Name = "toolStripStatusLabel14";
            this.toolStripStatusLabel14.Size = new System.Drawing.Size(25, 24);
            this.toolStripStatusLabel14.Text = "   ";
            // 
            // toolStripStatusLabel13
            // 
            this.toolStripStatusLabel13.Name = "toolStripStatusLabel13";
            this.toolStripStatusLabel13.Size = new System.Drawing.Size(64, 24);
            this.toolStripStatusLabel13.Text = "加钟：";
            // 
            // toolOverNumber
            // 
            this.toolOverNumber.Name = "toolOverNumber";
            this.toolOverNumber.Size = new System.Drawing.Size(21, 24);
            this.toolOverNumber.Text = "0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolOff,
            this.toolAvi,
            this.toolOn,
            this.toolOrder,
            this.toolOver});
            this.statusStrip1.Location = new System.Drawing.Point(0, 495);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(883, 35);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolOff
            // 
            this.toolOff.Name = "toolOff";
            this.toolOff.Size = new System.Drawing.Size(113, 30);
            this.toolOff.Text = "    下班    ";
            // 
            // toolAvi
            // 
            this.toolAvi.Name = "toolAvi";
            this.toolAvi.Size = new System.Drawing.Size(113, 30);
            this.toolAvi.Text = "    空闲    ";
            // 
            // toolOn
            // 
            this.toolOn.Name = "toolOn";
            this.toolOn.Size = new System.Drawing.Size(113, 30);
            this.toolOn.Text = "    上钟    ";
            // 
            // toolOrder
            // 
            this.toolOrder.Name = "toolOrder";
            this.toolOrder.Size = new System.Drawing.Size(113, 30);
            this.toolOrder.Text = "    点钟    ";
            // 
            // toolOver
            // 
            this.toolOver.Name = "toolOver";
            this.toolOver.Size = new System.Drawing.Size(113, 30);
            this.toolOver.Text = "    加钟    ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnClock);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.techTypes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 75);
            this.panel1.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(0, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(883, 2);
            this.label2.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Orange;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(679, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(164, 49);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "退出";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.BackColor = System.Drawing.Color.Orange;
            this.btnFind.Font = new System.Drawing.Font("宋体", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFind.Location = new System.Drawing.Point(313, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(164, 49);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "技师类别";
            // 
            // techTypes
            // 
            this.techTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.techTypes.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.techTypes.FormattingEnabled = true;
            this.techTypes.Location = new System.Drawing.Point(125, 21);
            this.techTypes.Name = "techTypes";
            this.techTypes.Size = new System.Drawing.Size(158, 32);
            this.techTypes.TabIndex = 0;
            // 
            // tPanel
            // 
            this.tPanel.AutoScroll = true;
            this.tPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tPanel.Location = new System.Drawing.Point(0, 75);
            this.tPanel.Name = "tPanel";
            this.tPanel.Size = new System.Drawing.Size(883, 391);
            this.tPanel.TabIndex = 11;
            // 
            // btnClock
            // 
            this.btnClock.AutoSize = true;
            this.btnClock.BackColor = System.Drawing.Color.Orange;
            this.btnClock.Font = new System.Drawing.Font("宋体", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClock.Location = new System.Drawing.Point(496, 13);
            this.btnClock.Name = "btnClock";
            this.btnClock.Size = new System.Drawing.Size(164, 49);
            this.btnClock.TabIndex = 2;
            this.btnClock.Text = "上班";
            this.btnClock.UseVisualStyleBackColor = false;
            this.btnClock.Click += new System.EventHandler(this.btnClock_Click);
            // 
            // TechnicianSeclectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(883, 530);
            this.Controls.Add(this.tPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "TechnicianSeclectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "技师管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.TechnicianSeclectForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TechnicianSeclectForm_FormClosing);
            this.ctx.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip ctx;
        private System.Windows.Forms.ToolStripMenuItem 下班ToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolTechNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolOffNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolAviNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolOnNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel toolOrderNumber;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel14;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel13;
        private System.Windows.Forms.ToolStripStatusLabel toolOverNumber;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolOff;
        private System.Windows.Forms.ToolStripStatusLabel toolAvi;
        private System.Windows.Forms.ToolStripStatusLabel toolOn;
        private System.Windows.Forms.ToolStripStatusLabel toolOrder;
        private System.Windows.Forms.ToolStripStatusLabel toolOver;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel tPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox techTypes;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClock;







    }
}