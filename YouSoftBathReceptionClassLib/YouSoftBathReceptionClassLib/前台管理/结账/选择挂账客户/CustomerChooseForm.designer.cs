namespace YouSoftBathReception
{
    partial class CustomerChooseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerChooseForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cPanel = new System.Windows.Forms.Panel();
            this.sp = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.contact = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.Label();
            this.money = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.cPanel.SuspendLayout();
            this.sp.Panel1.SuspendLayout();
            this.sp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 422);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 170);
            this.panel1.TabIndex = 10;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ImageIndex = 1;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(569, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(148, 156);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOk.AutoSize = true;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightBlue;
            this.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightBlue;
            this.btnOk.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ImageIndex = 0;
            this.btnOk.ImageList = this.imageList1;
            this.btnOk.Location = new System.Drawing.Point(291, 7);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(148, 156);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "保存";
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "save.png");
            this.imageList1.Images.SetKeyName(1, "20130329080804234_easyicon_net_128.png");
            // 
            // cPanel
            // 
            this.cPanel.BackColor = System.Drawing.Color.White;
            this.cPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cPanel.Controls.Add(this.sp);
            this.cPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cPanel.Location = new System.Drawing.Point(0, 0);
            this.cPanel.Name = "cPanel";
            this.cPanel.Size = new System.Drawing.Size(1008, 422);
            this.cPanel.TabIndex = 9;
            // 
            // sp
            // 
            this.sp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.sp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.sp.IsSplitterFixed = true;
            this.sp.Location = new System.Drawing.Point(0, 0);
            this.sp.Name = "sp";
            // 
            // sp.Panel1
            // 
            this.sp.Panel1.Controls.Add(this.money);
            this.sp.Panel1.Controls.Add(this.phone);
            this.sp.Panel1.Controls.Add(this.contact);
            this.sp.Panel1.Controls.Add(this.label4);
            this.sp.Panel1.Controls.Add(this.label3);
            this.sp.Panel1.Controls.Add(this.label1);
            // 
            // sp.Panel2
            // 
            this.sp.Panel2.AutoScroll = true;
            this.sp.Size = new System.Drawing.Size(1004, 418);
            this.sp.SplitterDistance = 330;
            this.sp.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(30, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "联系人";
            // 
            // contact
            // 
            this.contact.AutoSize = true;
            this.contact.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contact.Location = new System.Drawing.Point(153, 18);
            this.contact.Name = "contact";
            this.contact.Size = new System.Drawing.Size(110, 27);
            this.contact.TabIndex = 0;
            this.contact.Text = "contact";
            this.contact.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(43, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "电话";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "已挂账金额";
            // 
            // phone
            // 
            this.phone.AutoSize = true;
            this.phone.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.phone.Location = new System.Drawing.Point(153, 73);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(82, 27);
            this.phone.TabIndex = 0;
            this.phone.Text = "phone";
            this.phone.Visible = false;
            // 
            // money
            // 
            this.money.AutoSize = true;
            this.money.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.money.Location = new System.Drawing.Point(153, 128);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(82, 27);
            this.money.TabIndex = 0;
            this.money.Text = "money";
            this.money.Visible = false;
            // 
            // CustomerChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1008, 592);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cPanel);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomerChooseForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "选择挂账客户";
            this.Load += new System.EventHandler(this.CustomerChooseForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.cPanel.ResumeLayout(false);
            this.sp.Panel1.ResumeLayout(false);
            this.sp.Panel1.PerformLayout();
            this.sp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel cPanel;
        private System.Windows.Forms.SplitContainer sp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label contact;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label money;
        private System.Windows.Forms.Label phone;
    }
}