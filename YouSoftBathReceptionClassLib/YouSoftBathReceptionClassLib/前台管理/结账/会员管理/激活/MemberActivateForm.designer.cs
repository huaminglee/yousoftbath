namespace YouSoftBathReception
{
    partial class MemberActivateForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.GbCardInfo = new System.Windows.Forms.GroupBox();
            this.btn_free = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tb_bank = new System.Windows.Forms.TextBox();
            this.tb_waiter = new System.Windows.Forms.TextBox();
            this.tb_cash = new System.Windows.Forms.TextBox();
            this.tb_seat = new System.Windows.Forms.TextBox();
            this.balance = new System.Windows.Forms.TextBox();
            this.type = new System.Windows.Forms.TextBox();
            this.id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnFinger = new System.Windows.Forms.Button();
            this.GbFinger = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.GbCardInfo.SuspendLayout();
            this.GbFinger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 462);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 61);
            this.panel1.TabIndex = 40;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.Orange;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(277, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(153, 40);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.BackColor = System.Drawing.Color.Orange;
            this.btnOk.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(32, 10);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(153, 40);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "读卡";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Dock = System.Windows.Forms.DockStyle.Top;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(896, 2);
            this.label7.TabIndex = 42;
            // 
            // GbCardInfo
            // 
            this.GbCardInfo.Controls.Add(this.btn_free);
            this.GbCardInfo.Controls.Add(this.button2);
            this.GbCardInfo.Controls.Add(this.button3);
            this.GbCardInfo.Controls.Add(this.button1);
            this.GbCardInfo.Controls.Add(this.label5);
            this.GbCardInfo.Controls.Add(this.label2);
            this.GbCardInfo.Controls.Add(this.label18);
            this.GbCardInfo.Controls.Add(this.tb_bank);
            this.GbCardInfo.Controls.Add(this.tb_waiter);
            this.GbCardInfo.Controls.Add(this.tb_cash);
            this.GbCardInfo.Controls.Add(this.tb_seat);
            this.GbCardInfo.Controls.Add(this.balance);
            this.GbCardInfo.Controls.Add(this.type);
            this.GbCardInfo.Controls.Add(this.id);
            this.GbCardInfo.Controls.Add(this.label1);
            this.GbCardInfo.Location = new System.Drawing.Point(12, 12);
            this.GbCardInfo.Name = "GbCardInfo";
            this.GbCardInfo.Size = new System.Drawing.Size(452, 444);
            this.GbCardInfo.TabIndex = 41;
            this.GbCardInfo.TabStop = false;
            // 
            // btn_free
            // 
            this.btn_free.AutoSize = true;
            this.btn_free.BackColor = System.Drawing.Color.Orange;
            this.btn_free.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_free.Location = new System.Drawing.Point(27, 376);
            this.btn_free.Name = "btn_free";
            this.btn_free.Size = new System.Drawing.Size(398, 40);
            this.btn_free.TabIndex = 58;
            this.btn_free.Text = "赠送卡";
            this.btn_free.UseVisualStyleBackColor = false;
            this.btn_free.Click += new System.EventHandler(this.btn_free_Click);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.Orange;
            this.button2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(27, 318);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(134, 40);
            this.button2.TabIndex = 57;
            this.button2.TabStop = false;
            this.button2.Text = "银联";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.AutoSize = true;
            this.button3.BackColor = System.Drawing.Color.Orange;
            this.button3.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(27, 203);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(134, 40);
            this.button3.TabIndex = 60;
            this.button3.TabStop = false;
            this.button3.Text = "服务工号";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Orange;
            this.button1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(27, 260);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 40);
            this.button1.TabIndex = 59;
            this.button1.TabStop = false;
            this.button1.Text = "现金";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(41, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 27);
            this.label5.TabIndex = 56;
            this.label5.Text = "会员类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(68, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 27);
            this.label2.TabIndex = 53;
            this.label2.Text = "手牌号";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(41, 120);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(120, 27);
            this.label18.TabIndex = 54;
            this.label18.Text = "会员余额";
            // 
            // tb_bank
            // 
            this.tb_bank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.tb_bank.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_bank.Location = new System.Drawing.Point(167, 319);
            this.tb_bank.Name = "tb_bank";
            this.tb_bank.Size = new System.Drawing.Size(258, 38);
            this.tb_bank.TabIndex = 48;
            // 
            // tb_waiter
            // 
            this.tb_waiter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.tb_waiter.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_waiter.Location = new System.Drawing.Point(167, 204);
            this.tb_waiter.Name = "tb_waiter";
            this.tb_waiter.Size = new System.Drawing.Size(258, 38);
            this.tb_waiter.TabIndex = 47;
            // 
            // tb_cash
            // 
            this.tb_cash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.tb_cash.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_cash.Location = new System.Drawing.Point(167, 261);
            this.tb_cash.Name = "tb_cash";
            this.tb_cash.Size = new System.Drawing.Size(258, 38);
            this.tb_cash.TabIndex = 46;
            // 
            // tb_seat
            // 
            this.tb_seat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.tb_seat.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tb_seat.Location = new System.Drawing.Point(167, 161);
            this.tb_seat.Name = "tb_seat";
            this.tb_seat.ReadOnly = true;
            this.tb_seat.Size = new System.Drawing.Size(258, 30);
            this.tb_seat.TabIndex = 51;
            this.tb_seat.TabStop = false;
            // 
            // balance
            // 
            this.balance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.balance.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.balance.Location = new System.Drawing.Point(167, 118);
            this.balance.Name = "balance";
            this.balance.ReadOnly = true;
            this.balance.Size = new System.Drawing.Size(258, 30);
            this.balance.TabIndex = 52;
            this.balance.TabStop = false;
            // 
            // type
            // 
            this.type.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.type.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.type.Location = new System.Drawing.Point(167, 73);
            this.type.Name = "type";
            this.type.ReadOnly = true;
            this.type.Size = new System.Drawing.Size(258, 30);
            this.type.TabIndex = 50;
            this.type.TabStop = false;
            // 
            // id
            // 
            this.id.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(176)))), ((int)(((byte)(28)))));
            this.id.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.id.Location = new System.Drawing.Point(167, 28);
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Size = new System.Drawing.Size(258, 30);
            this.id.TabIndex = 49;
            this.id.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(41, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 27);
            this.label1.TabIndex = 55;
            this.label1.Text = "会员卡号";
            // 
            // BtnFinger
            // 
            this.BtnFinger.AutoSize = true;
            this.BtnFinger.BackColor = System.Drawing.Color.Orange;
            this.BtnFinger.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnFinger.Location = new System.Drawing.Point(515, 416);
            this.BtnFinger.Name = "BtnFinger";
            this.BtnFinger.Size = new System.Drawing.Size(358, 40);
            this.BtnFinger.TabIndex = 42;
            this.BtnFinger.Text = "获取指纹";
            this.BtnFinger.UseVisualStyleBackColor = false;
            this.BtnFinger.Click += new System.EventHandler(this.BtnFinger_Click);
            // 
            // GbFinger
            // 
            this.GbFinger.Controls.Add(this.pictureBox1);
            this.GbFinger.Location = new System.Drawing.Point(515, 12);
            this.GbFinger.Name = "GbFinger";
            this.GbFinger.Size = new System.Drawing.Size(358, 398);
            this.GbFinger.TabIndex = 43;
            this.GbFinger.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(352, 372);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MemberActivateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(225)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(896, 523);
            this.Controls.Add(this.GbFinger);
            this.Controls.Add(this.BtnFinger);
            this.Controls.Add(this.GbCardInfo);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemberActivateForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会员售卡激活";
            this.Load += new System.EventHandler(this.MemberPopForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GbCardInfo.ResumeLayout(false);
            this.GbCardInfo.PerformLayout();
            this.GbFinger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox GbCardInfo;
        private System.Windows.Forms.Button btn_free;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tb_bank;
        private System.Windows.Forms.TextBox tb_waiter;
        private System.Windows.Forms.TextBox tb_cash;
        private System.Windows.Forms.TextBox tb_seat;
        private System.Windows.Forms.TextBox balance;
        private System.Windows.Forms.TextBox type;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnFinger;
        private System.Windows.Forms.GroupBox GbFinger;
        private System.Windows.Forms.PictureBox pictureBox1;
        private AxZKFPEngXControl.AxZKFPEngX axZKFPEngX1;
    }
}