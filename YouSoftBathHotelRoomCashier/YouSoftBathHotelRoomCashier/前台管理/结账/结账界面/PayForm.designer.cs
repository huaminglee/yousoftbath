namespace YouSoftBathReception
{
    partial class PayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PayForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.bankUnionTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.creditCardTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.signTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.zeroTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.couponTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.wipeZero = new System.Windows.Forms.TextBox();
            this.groupBuy = new System.Windows.Forms.TextBox();
            this.coupon = new System.Windows.Forms.TextBox();
            this.sign = new System.Windows.Forms.TextBox();
            this.zero = new System.Windows.Forms.TextBox();
            this.creditCard = new System.Windows.Forms.TextBox();
            this.bankUnion = new System.Windows.Forms.TextBox();
            this.cash = new System.Windows.Forms.TextBox();
            this.changes = new System.Windows.Forms.Label();
            this.moneyPayable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(88, 88);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.bankUnionTool,
            this.toolStripLabel2,
            this.creditCardTool,
            this.toolStripLabel3,
            this.signTool,
            this.toolStripLabel7,
            this.zeroTool,
            this.toolStripLabel4,
            this.couponTool,
            this.toolStripLabel5,
            this.toolStripButton1,
            this.toolStripLabel6,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(994, 119);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(24, 116);
            this.toolStripLabel1.Text = "    ";
            // 
            // bankUnionTool
            // 
            this.bankUnionTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.bankUnionTool.Image = ((System.Drawing.Image)(resources.GetObject("bankUnionTool.Image")));
            this.bankUnionTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bankUnionTool.Name = "bankUnionTool";
            this.bankUnionTool.Size = new System.Drawing.Size(95, 116);
            this.bankUnionTool.Text = "银    联(+)";
            this.bankUnionTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bankUnionTool.Click += new System.EventHandler(this.bankUnionTool_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(32, 116);
            this.toolStripLabel2.Text = "      ";
            // 
            // creditCardTool
            // 
            this.creditCardTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.creditCardTool.Image = ((System.Drawing.Image)(resources.GetObject("creditCardTool.Image")));
            this.creditCardTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.creditCardTool.Name = "creditCardTool";
            this.creditCardTool.Size = new System.Drawing.Size(98, 116);
            this.creditCardTool.Text = "储 值 卡(-)";
            this.creditCardTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.creditCardTool.Click += new System.EventHandler(this.creditCardTool_Click);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(36, 116);
            this.toolStripLabel3.Text = "       ";
            // 
            // signTool
            // 
            this.signTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.signTool.Image = ((System.Drawing.Image)(resources.GetObject("signTool.Image")));
            this.signTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.signTool.Name = "signTool";
            this.signTool.Size = new System.Drawing.Size(92, 116);
            this.signTool.Text = "签字(.)";
            this.signTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.signTool.Click += new System.EventHandler(this.signTool_Click);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(28, 116);
            this.toolStripLabel7.Text = "     ";
            // 
            // zeroTool
            // 
            this.zeroTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.zeroTool.Image = ((System.Drawing.Image)(resources.GetObject("zeroTool.Image")));
            this.zeroTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zeroTool.Name = "zeroTool";
            this.zeroTool.Size = new System.Drawing.Size(92, 116);
            this.zeroTool.Text = "挂    账";
            this.zeroTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.zeroTool.Click += new System.EventHandler(this.zeroTool_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(36, 116);
            this.toolStripLabel4.Text = "       ";
            // 
            // couponTool
            // 
            this.couponTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.couponTool.Image = ((System.Drawing.Image)(resources.GetObject("couponTool.Image")));
            this.couponTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.couponTool.Name = "couponTool";
            this.couponTool.Size = new System.Drawing.Size(92, 116);
            this.couponTool.Text = "券    类";
            this.couponTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(56, 116);
            this.toolStripLabel5.Text = "            ";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(92, 116);
            this.toolStripButton1.Text = "团购优惠";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(36, 116);
            this.toolStripLabel6.Text = "       ";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(92, 116);
            this.toolStripButton2.Text = "抹零";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 374);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 95);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(994, 2);
            this.label1.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.ImageIndex = 1;
            this.btnCancel.ImageList = this.imageList1;
            this.btnCancel.Location = new System.Drawing.Point(534, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(118, 92);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "save.png");
            this.imageList1.Images.SetKeyName(1, "20130329080804234_easyicon_net_128.png");
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOk.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnOk.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.ImageIndex = 0;
            this.btnOk.ImageList = this.imageList1;
            this.btnOk.Location = new System.Drawing.Point(288, 1);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(126, 92);
            this.btnOk.TabIndex = 8;
            this.btnOk.TabStop = false;
            this.btnOk.Text = "确定(Enter)";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.wipeZero);
            this.panel2.Controls.Add(this.groupBuy);
            this.panel2.Controls.Add(this.coupon);
            this.panel2.Controls.Add(this.sign);
            this.panel2.Controls.Add(this.zero);
            this.panel2.Controls.Add(this.creditCard);
            this.panel2.Controls.Add(this.bankUnion);
            this.panel2.Controls.Add(this.cash);
            this.panel2.Controls.Add(this.changes);
            this.panel2.Controls.Add(this.moneyPayable);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(994, 255);
            this.panel2.TabIndex = 6;
            // 
            // wipeZero
            // 
            this.wipeZero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wipeZero.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wipeZero.ImeMode = System.Windows.Forms.ImeMode.On;
            this.wipeZero.Location = new System.Drawing.Point(807, 5);
            this.wipeZero.Name = "wipeZero";
            this.wipeZero.Size = new System.Drawing.Size(121, 35);
            this.wipeZero.TabIndex = 23;
            this.wipeZero.TabStop = false;
            this.wipeZero.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.wipeZero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.wipeZero.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // groupBuy
            // 
            this.groupBuy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBuy.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBuy.ImeMode = System.Windows.Forms.ImeMode.On;
            this.groupBuy.Location = new System.Drawing.Point(680, 5);
            this.groupBuy.Name = "groupBuy";
            this.groupBuy.Size = new System.Drawing.Size(121, 35);
            this.groupBuy.TabIndex = 23;
            this.groupBuy.TabStop = false;
            this.groupBuy.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.groupBuy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.groupBuy.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // coupon
            // 
            this.coupon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.coupon.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.coupon.ImeMode = System.Windows.Forms.ImeMode.On;
            this.coupon.Location = new System.Drawing.Point(538, 5);
            this.coupon.Name = "coupon";
            this.coupon.Size = new System.Drawing.Size(127, 35);
            this.coupon.TabIndex = 22;
            this.coupon.TabStop = false;
            this.coupon.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.coupon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.coupon.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // sign
            // 
            this.sign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sign.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sign.ImeMode = System.Windows.Forms.ImeMode.On;
            this.sign.Location = new System.Drawing.Point(284, 5);
            this.sign.Name = "sign";
            this.sign.Size = new System.Drawing.Size(121, 35);
            this.sign.TabIndex = 19;
            this.sign.TabStop = false;
            this.sign.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.sign.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.sign.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // zero
            // 
            this.zero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zero.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zero.ImeMode = System.Windows.Forms.ImeMode.On;
            this.zero.Location = new System.Drawing.Point(411, 5);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(121, 35);
            this.zero.TabIndex = 19;
            this.zero.TabStop = false;
            this.zero.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.zero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.zero.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // creditCard
            // 
            this.creditCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.creditCard.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.creditCard.ImeMode = System.Windows.Forms.ImeMode.On;
            this.creditCard.Location = new System.Drawing.Point(146, 5);
            this.creditCard.Name = "creditCard";
            this.creditCard.Size = new System.Drawing.Size(132, 35);
            this.creditCard.TabIndex = 16;
            this.creditCard.TabStop = false;
            this.creditCard.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.creditCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.creditCard.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // bankUnion
            // 
            this.bankUnion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bankUnion.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bankUnion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.bankUnion.Location = new System.Drawing.Point(8, 5);
            this.bankUnion.Name = "bankUnion";
            this.bankUnion.Size = new System.Drawing.Size(132, 35);
            this.bankUnion.TabIndex = 13;
            this.bankUnion.TabStop = false;
            this.bankUnion.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.bankUnion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.bankUnion.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // cash
            // 
            this.cash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cash.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cash.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cash.Location = new System.Drawing.Point(313, 151);
            this.cash.Name = "cash";
            this.cash.Size = new System.Drawing.Size(163, 46);
            this.cash.TabIndex = 1;
            this.cash.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.cash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.cash.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // changes
            // 
            this.changes.AutoSize = true;
            this.changes.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.changes.Location = new System.Drawing.Point(614, 157);
            this.changes.Name = "changes";
            this.changes.Size = new System.Drawing.Size(105, 34);
            this.changes.TabIndex = 17;
            this.changes.Text = "300.0";
            // 
            // moneyPayable
            // 
            this.moneyPayable.AutoSize = true;
            this.moneyPayable.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.moneyPayable.ForeColor = System.Drawing.Color.Red;
            this.moneyPayable.Location = new System.Drawing.Point(513, 82);
            this.moneyPayable.Name = "moneyPayable";
            this.moneyPayable.Size = new System.Drawing.Size(117, 40);
            this.moneyPayable.TabIndex = 15;
            this.moneyPayable.Text = "300.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(311, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 40);
            this.label2.TabIndex = 8;
            this.label2.Text = "应收金额：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(518, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 34);
            this.label6.TabIndex = 10;
            this.label6.Text = "找零:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(221, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 34);
            this.label5.TabIndex = 12;
            this.label5.Text = "现金：";
            // 
            // PayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(994, 469);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PayForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "宾客付款";
            this.Load += new System.EventHandler(this.PayForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton bankUnionTool;
        private System.Windows.Forms.ToolStripButton creditCardTool;
        private System.Windows.Forms.ToolStripButton couponTool;
        private System.Windows.Forms.ToolStripButton zeroTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox zero;
        private System.Windows.Forms.TextBox creditCard;
        private System.Windows.Forms.TextBox bankUnion;
        private System.Windows.Forms.TextBox cash;
        private System.Windows.Forms.Label changes;
        private System.Windows.Forms.Label moneyPayable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TextBox groupBuy;
        private System.Windows.Forms.TextBox coupon;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.TextBox wipeZero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton signTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.TextBox sign;

    }
}