namespace YouSoftBathFormClass
{
    partial class CabPopForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CabPopForm));
            this.label1 = new System.Windows.Forms.Label();
            this.roomId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pop = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.curPop = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnInc = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnDec = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "包厢号";
            // 
            // roomId
            // 
            this.roomId.Enabled = false;
            this.roomId.Location = new System.Drawing.Point(183, 23);
            this.roomId.Name = "roomId";
            this.roomId.Size = new System.Drawing.Size(166, 27);
            this.roomId.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "可住人数";
            // 
            // pop
            // 
            this.pop.Enabled = false;
            this.pop.Location = new System.Drawing.Point(183, 72);
            this.pop.Name = "pop";
            this.pop.Size = new System.Drawing.Size(166, 27);
            this.pop.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "已住人数";
            // 
            // curPop
            // 
            this.curPop.Enabled = false;
            this.curPop.Location = new System.Drawing.Point(183, 118);
            this.curPop.Name = "curPop";
            this.curPop.Size = new System.Drawing.Size(166, 27);
            this.curPop.TabIndex = 1;
            this.curPop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.curPop_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnInc);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.btnDec);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 182);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 70);
            this.panel1.TabIndex = 2;
            // 
            // btnInc
            // 
            this.btnInc.AutoSize = true;
            this.btnInc.Location = new System.Drawing.Point(296, 12);
            this.btnInc.Name = "btnInc";
            this.btnInc.Size = new System.Drawing.Size(81, 46);
            this.btnInc.TabIndex = 2;
            this.btnInc.Text = "入住";
            this.btnInc.UseVisualStyleBackColor = true;
            this.btnInc.Click += new System.EventHandler(this.btnInc_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(183, 12);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(81, 46);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnDec
            // 
            this.btnDec.AutoSize = true;
            this.btnDec.Location = new System.Drawing.Point(70, 12);
            this.btnDec.Name = "btnDec";
            this.btnDec.Size = new System.Drawing.Size(81, 46);
            this.btnDec.TabIndex = 2;
            this.btnDec.Text = "退房";
            this.btnDec.UseVisualStyleBackColor = true;
            this.btnDec.Click += new System.EventHandler(this.btnDec_Click);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(447, 2);
            this.label4.TabIndex = 1;
            // 
            // CabPopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(447, 252);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.curPop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pop);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.roomId);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CabPopForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "包厢信息";
            this.Load += new System.EventHandler(this.RoomManagementForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RoomViewForm_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox roomId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox curPop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDec;
        private System.Windows.Forms.Button btnInc;
        private System.Windows.Forms.Button btnOk;

    }
}