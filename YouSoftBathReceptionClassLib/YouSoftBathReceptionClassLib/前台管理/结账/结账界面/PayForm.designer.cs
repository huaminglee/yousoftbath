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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnOk = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonEx7 = new CSharpWin.ButtonEx();
            this.buttonEx6 = new CSharpWin.ButtonEx();
            this.couponTool = new CSharpWin.ButtonEx();
            this.zeroTool = new CSharpWin.ButtonEx();
            this.signTool = new CSharpWin.ButtonEx();
            this.creditCardTool = new CSharpWin.ButtonEx();
            this.bankUnionTool = new CSharpWin.ButtonEx();
            this.label3 = new System.Windows.Forms.Label();
            this.bankUnion = new System.Windows.Forms.TextBox();
            this.wipeZero = new System.Windows.Forms.TextBox();
            this.groupBuy = new System.Windows.Forms.TextBox();
            this.coupon = new System.Windows.Forms.TextBox();
            this.sign = new System.Windows.Forms.TextBox();
            this.zero = new System.Windows.Forms.TextBox();
            this.creditCard = new System.Windows.Forms.TextBox();
            this.cash = new System.Windows.Forms.TextBox();
            this.changes = new System.Windows.Forms.Label();
            this.moneyPayable = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            this.btnCancel.Location = new System.Drawing.Point(561, 1);
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
            this.btnOk.Location = new System.Drawing.Point(315, 1);
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
            this.panel2.Controls.Add(this.buttonEx7);
            this.panel2.Controls.Add(this.buttonEx6);
            this.panel2.Controls.Add(this.couponTool);
            this.panel2.Controls.Add(this.zeroTool);
            this.panel2.Controls.Add(this.signTool);
            this.panel2.Controls.Add(this.creditCardTool);
            this.panel2.Controls.Add(this.bankUnionTool);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.bankUnion);
            this.panel2.Controls.Add(this.wipeZero);
            this.panel2.Controls.Add(this.groupBuy);
            this.panel2.Controls.Add(this.coupon);
            this.panel2.Controls.Add(this.sign);
            this.panel2.Controls.Add(this.zero);
            this.panel2.Controls.Add(this.creditCard);
            this.panel2.Controls.Add(this.cash);
            this.panel2.Controls.Add(this.changes);
            this.panel2.Controls.Add(this.moneyPayable);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(994, 374);
            this.panel2.TabIndex = 6;
            // 
            // buttonEx7
            // 
            this.buttonEx7.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEx7.Image = ((System.Drawing.Image)(resources.GetObject("buttonEx7.Image")));
            this.buttonEx7.ImageWidth = 88;
            this.buttonEx7.Location = new System.Drawing.Point(873, 5);
            this.buttonEx7.Name = "buttonEx7";
            this.buttonEx7.Size = new System.Drawing.Size(103, 108);
            this.buttonEx7.TabIndex = 25;
            this.buttonEx7.Text = "抹零";
            this.buttonEx7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonEx7.UseVisualStyleBackColor = true;
            // 
            // buttonEx6
            // 
            this.buttonEx6.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEx6.Image = ((System.Drawing.Image)(resources.GetObject("buttonEx6.Image")));
            this.buttonEx6.ImageWidth = 88;
            this.buttonEx6.Location = new System.Drawing.Point(732, 5);
            this.buttonEx6.Name = "buttonEx6";
            this.buttonEx6.Size = new System.Drawing.Size(103, 108);
            this.buttonEx6.TabIndex = 25;
            this.buttonEx6.Text = "团购优惠";
            this.buttonEx6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonEx6.UseVisualStyleBackColor = true;
            // 
            // couponTool
            // 
            this.couponTool.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.couponTool.Image = ((System.Drawing.Image)(resources.GetObject("couponTool.Image")));
            this.couponTool.ImageWidth = 88;
            this.couponTool.Location = new System.Drawing.Point(591, 5);
            this.couponTool.Name = "couponTool";
            this.couponTool.Size = new System.Drawing.Size(103, 108);
            this.couponTool.TabIndex = 25;
            this.couponTool.Text = "券类";
            this.couponTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.couponTool.UseVisualStyleBackColor = true;
            this.couponTool.Click += new System.EventHandler(this.couponTool_Click);
            // 
            // zeroTool
            // 
            this.zeroTool.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.zeroTool.Image = ((System.Drawing.Image)(resources.GetObject("zeroTool.Image")));
            this.zeroTool.ImageWidth = 88;
            this.zeroTool.Location = new System.Drawing.Point(450, 5);
            this.zeroTool.Name = "zeroTool";
            this.zeroTool.Size = new System.Drawing.Size(103, 108);
            this.zeroTool.TabIndex = 25;
            this.zeroTool.Text = "挂账";
            this.zeroTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.zeroTool.UseVisualStyleBackColor = true;
            this.zeroTool.Click += new System.EventHandler(this.zeroTool_Click);
            // 
            // signTool
            // 
            this.signTool.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.signTool.Image = ((System.Drawing.Image)(resources.GetObject("signTool.Image")));
            this.signTool.ImageWidth = 88;
            this.signTool.Location = new System.Drawing.Point(309, 5);
            this.signTool.Name = "signTool";
            this.signTool.Size = new System.Drawing.Size(103, 108);
            this.signTool.TabIndex = 25;
            this.signTool.Text = "签字(.)";
            this.signTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.signTool.UseVisualStyleBackColor = true;
            this.signTool.Click += new System.EventHandler(this.signTool_Click);
            // 
            // creditCardTool
            // 
            this.creditCardTool.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.creditCardTool.Image = ((System.Drawing.Image)(resources.GetObject("creditCardTool.Image")));
            this.creditCardTool.ImageWidth = 88;
            this.creditCardTool.Location = new System.Drawing.Point(168, 5);
            this.creditCardTool.Name = "creditCardTool";
            this.creditCardTool.Size = new System.Drawing.Size(103, 108);
            this.creditCardTool.TabIndex = 25;
            this.creditCardTool.Text = "储值卡(-)";
            this.creditCardTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.creditCardTool.UseVisualStyleBackColor = true;
            this.creditCardTool.Click += new System.EventHandler(this.creditCardTool_Click);
            // 
            // bankUnionTool
            // 
            this.bankUnionTool.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bankUnionTool.Image = ((System.Drawing.Image)(resources.GetObject("bankUnionTool.Image")));
            this.bankUnionTool.ImageWidth = 88;
            this.bankUnionTool.Location = new System.Drawing.Point(27, 5);
            this.bankUnionTool.Name = "bankUnionTool";
            this.bankUnionTool.Size = new System.Drawing.Size(103, 108);
            this.bankUnionTool.TabIndex = 25;
            this.bankUnionTool.Text = "银联(+)";
            this.bankUnionTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.bankUnionTool.UseVisualStyleBackColor = true;
            this.bankUnionTool.Click += new System.EventHandler(this.bankUnionTool_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(994, 2);
            this.label3.TabIndex = 24;
            // 
            // bankUnion
            // 
            this.bankUnion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bankUnion.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bankUnion.ImeMode = System.Windows.Forms.ImeMode.On;
            this.bankUnion.Location = new System.Drawing.Point(12, 129);
            this.bankUnion.Name = "bankUnion";
            this.bankUnion.Size = new System.Drawing.Size(132, 35);
            this.bankUnion.TabIndex = 13;
            this.bankUnion.TabStop = false;
            this.bankUnion.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.bankUnion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.bankUnion.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // wipeZero
            // 
            this.wipeZero.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wipeZero.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wipeZero.ImeMode = System.Windows.Forms.ImeMode.On;
            this.wipeZero.Location = new System.Drawing.Point(858, 129);
            this.wipeZero.Name = "wipeZero";
            this.wipeZero.Size = new System.Drawing.Size(132, 35);
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
            this.groupBuy.Location = new System.Drawing.Point(717, 129);
            this.groupBuy.Name = "groupBuy";
            this.groupBuy.Size = new System.Drawing.Size(132, 35);
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
            this.coupon.Location = new System.Drawing.Point(576, 129);
            this.coupon.Name = "coupon";
            this.coupon.Size = new System.Drawing.Size(132, 35);
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
            this.sign.Location = new System.Drawing.Point(294, 129);
            this.sign.Name = "sign";
            this.sign.Size = new System.Drawing.Size(132, 35);
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
            this.zero.Location = new System.Drawing.Point(435, 129);
            this.zero.Name = "zero";
            this.zero.Size = new System.Drawing.Size(132, 35);
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
            this.creditCard.Location = new System.Drawing.Point(153, 129);
            this.creditCard.Name = "creditCard";
            this.creditCard.Size = new System.Drawing.Size(132, 35);
            this.creditCard.TabIndex = 16;
            this.creditCard.TabStop = false;
            this.creditCard.TextChanged += new System.EventHandler(this.money_TextChanged);
            this.creditCard.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.money_KeyPress);
            this.creditCard.Enter += new System.EventHandler(this.bankUnion_Enter);
            // 
            // cash
            // 
            this.cash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cash.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cash.ImeMode = System.Windows.Forms.ImeMode.On;
            this.cash.Location = new System.Drawing.Point(340, 267);
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
            this.changes.Location = new System.Drawing.Point(641, 273);
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
            this.moneyPayable.Location = new System.Drawing.Point(540, 198);
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
            this.label2.Location = new System.Drawing.Point(338, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 40);
            this.label2.TabIndex = 8;
            this.label2.Text = "应收金额：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(545, 273);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 34);
            this.label6.TabIndex = 10;
            this.label6.Text = "找零:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(248, 273);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PayForm_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.TextBox groupBuy;
        private System.Windows.Forms.TextBox coupon;
        private System.Windows.Forms.TextBox wipeZero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sign;
        private System.Windows.Forms.Label label3;
        private CSharpWin.ButtonEx bankUnionTool;
        private CSharpWin.ButtonEx creditCardTool;
        private CSharpWin.ButtonEx signTool;
        private CSharpWin.ButtonEx zeroTool;
        private CSharpWin.ButtonEx couponTool;
        private CSharpWin.ButtonEx buttonEx6;
        private CSharpWin.ButtonEx buttonEx7;

    }
}