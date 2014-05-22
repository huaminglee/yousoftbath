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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolAvi = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolIn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolWait = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolReserve = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolServing = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolFini = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pr = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pr.SuspendLayout();
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
            this.toolWait,
            this.toolReserve,
            this.toolServing,
            this.toolFini});
            this.statusStrip1.Location = new System.Drawing.Point(0, 311);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(799, 35);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolAvi
            // 
            this.toolAvi.Name = "toolAvi";
            this.toolAvi.Size = new System.Drawing.Size(113, 30);
            this.toolAvi.Text = "    空闲    ";
            // 
            // toolIn
            // 
            this.toolIn.Name = "toolIn";
            this.toolIn.Size = new System.Drawing.Size(113, 30);
            this.toolIn.Text = "    入住    ";
            // 
            // toolWait
            // 
            this.toolWait.Name = "toolWait";
            this.toolWait.Size = new System.Drawing.Size(101, 30);
            this.toolWait.Text = "等待服务";
            // 
            // toolReserve
            // 
            this.toolReserve.Name = "toolReserve";
            this.toolReserve.Size = new System.Drawing.Size(101, 30);
            this.toolReserve.Text = "预约服务";
            // 
            // toolServing
            // 
            this.toolServing.Name = "toolServing";
            this.toolServing.Size = new System.Drawing.Size(107, 30);
            this.toolServing.Text = "  服务中  ";
            // 
            // toolFini
            // 
            this.toolFini.Name = "toolFini";
            this.toolFini.Size = new System.Drawing.Size(101, 30);
            this.toolFini.Text = "等待清洁";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 65);
            this.panel1.TabIndex = 9;
            // 
            // pr
            // 
            this.pr.AutoScroll = true;
            this.pr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(233)))), ((int)(((byte)(251)))));
            this.pr.Controls.Add(this.label1);
            this.pr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pr.Location = new System.Drawing.Point(0, 65);
            this.pr.Name = "pr";
            this.pr.Size = new System.Drawing.Size(799, 246);
            this.pr.TabIndex = 11;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Orange;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(317, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(202, 48);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "退   出";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(799, 2);
            this.label1.TabIndex = 0;
            // 
            // InformRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(799, 346);
            this.Controls.Add(this.pr);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "InformRoomForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "催钟";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RoomManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomViewForm_KeyDown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pr.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolAvi;
        private System.Windows.Forms.ToolStripStatusLabel toolIn;
        private System.Windows.Forms.ToolStripStatusLabel toolWait;
        private System.Windows.Forms.ToolStripStatusLabel toolReserve;
        private System.Windows.Forms.ToolStripStatusLabel toolServing;
        private System.Windows.Forms.ToolStripStatusLabel toolFini;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pr;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
    }
}