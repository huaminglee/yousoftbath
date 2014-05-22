namespace YouSoftBathTechnician
{
    partial class TechArrangeSetForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.班次内技师排序规则 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.班次内动牌模式 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.动牌时间 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.休息时间是否动牌 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.上班时插入队列模式 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.到岗未到上班时间是否插入队列 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.上钟被退回是否回到队列原位 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.派遣被退回是否回到队列原位 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.新加入对应本班次组技师插入方式 = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(931, 51);
            this.panel1.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(931, 2);
            this.label4.TabIndex = 18;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(574, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 28);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "取消(ESC)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Location = new System.Drawing.Point(196, 11);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(117, 28);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "确定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(129, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 18);
            this.label5.TabIndex = 24;
            this.label5.Text = "班次内技师排序规则";
            // 
            // 班次内技师排序规则
            // 
            this.班次内技师排序规则.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.班次内技师排序规则.FormattingEnabled = true;
            this.班次内技师排序规则.Items.AddRange(new object[] {
            "按指定顺序排序",
            "按序号初始排序",
            "按上次队列初始排序",
            "按指定顺序排序"});
            this.班次内技师排序规则.Location = new System.Drawing.Point(297, 6);
            this.班次内技师排序规则.Name = "班次内技师排序规则";
            this.班次内技师排序规则.Size = new System.Drawing.Size(207, 25);
            this.班次内技师排序规则.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(632, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "班次内动排模式";
            // 
            // 班次内动牌模式
            // 
            this.班次内动牌模式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.班次内动牌模式.FormattingEnabled = true;
            this.班次内动牌模式.Items.AddRange(new object[] {
            "按设定顺序插入",
            "甩到队尾",
            "甩到下一轮队尾"});
            this.班次内动牌模式.Location = new System.Drawing.Point(766, 6);
            this.班次内动牌模式.Name = "班次内动牌模式";
            this.班次内动牌模式.Size = new System.Drawing.Size(153, 25);
            this.班次内动牌模式.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 24;
            this.label2.Text = "动牌时间";
            // 
            // 动牌时间
            // 
            this.动牌时间.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.动牌时间.FormattingEnabled = true;
            this.动牌时间.Items.AddRange(new object[] {
            "下钟动牌",
            "上钟动牌",
            "安排活派遣时动牌"});
            this.动牌时间.Location = new System.Drawing.Point(297, 44);
            this.动牌时间.Name = "动牌时间";
            this.动牌时间.Size = new System.Drawing.Size(207, 25);
            this.动牌时间.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(614, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 18);
            this.label6.TabIndex = 24;
            this.label6.Text = "休息时间是否动牌";
            // 
            // 休息时间是否动牌
            // 
            this.休息时间是否动牌.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.休息时间是否动牌.FormattingEnabled = true;
            this.休息时间是否动牌.Items.AddRange(new object[] {
            "是",
            "否"});
            this.休息时间是否动牌.Location = new System.Drawing.Point(766, 44);
            this.休息时间是否动牌.Name = "休息时间是否动牌";
            this.休息时间是否动牌.Size = new System.Drawing.Size(153, 25);
            this.休息时间是否动牌.TabIndex = 25;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(129, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 18);
            this.label7.TabIndex = 24;
            this.label7.Text = "上班时插入队列模式";
            // 
            // 上班时插入队列模式
            // 
            this.上班时插入队列模式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.上班时插入队列模式.FormattingEnabled = true;
            this.上班时插入队列模式.Items.AddRange(new object[] {
            "上班时排在队列0轮后面",
            "上班时排在队列后面"});
            this.上班时插入队列模式.Location = new System.Drawing.Point(297, 85);
            this.上班时插入队列模式.Name = "上班时插入队列模式";
            this.上班时插入队列模式.Size = new System.Drawing.Size(207, 25);
            this.上班时插入队列模式.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(506, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(260, 18);
            this.label8.TabIndex = 24;
            this.label8.Text = "到岗未到上班时间是否插入队列";
            // 
            // 到岗未到上班时间是否插入队列
            // 
            this.到岗未到上班时间是否插入队列.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.到岗未到上班时间是否插入队列.FormattingEnabled = true;
            this.到岗未到上班时间是否插入队列.Items.AddRange(new object[] {
            "是",
            "否"});
            this.到岗未到上班时间是否插入队列.Location = new System.Drawing.Point(766, 85);
            this.到岗未到上班时间是否插入队列.Name = "到岗未到上班时间是否插入队列";
            this.到岗未到上班时间是否插入队列.Size = new System.Drawing.Size(153, 25);
            this.到岗未到上班时间是否插入队列.TabIndex = 25;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(57, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(242, 18);
            this.label9.TabIndex = 24;
            this.label9.Text = "上钟被退回是否回到队列原位";
            // 
            // 上钟被退回是否回到队列原位
            // 
            this.上钟被退回是否回到队列原位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.上钟被退回是否回到队列原位.FormattingEnabled = true;
            this.上钟被退回是否回到队列原位.Items.AddRange(new object[] {
            "是",
            "否"});
            this.上钟被退回是否回到队列原位.Location = new System.Drawing.Point(297, 126);
            this.上钟被退回是否回到队列原位.Name = "上钟被退回是否回到队列原位";
            this.上钟被退回是否回到队列原位.Size = new System.Drawing.Size(207, 25);
            this.上钟被退回是否回到队列原位.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(524, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(242, 18);
            this.label10.TabIndex = 24;
            this.label10.Text = "派遣被退回是否回到队列原位";
            // 
            // 派遣被退回是否回到队列原位
            // 
            this.派遣被退回是否回到队列原位.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.派遣被退回是否回到队列原位.FormattingEnabled = true;
            this.派遣被退回是否回到队列原位.Items.AddRange(new object[] {
            "是",
            "否"});
            this.派遣被退回是否回到队列原位.Location = new System.Drawing.Point(766, 126);
            this.派遣被退回是否回到队列原位.Name = "派遣被退回是否回到队列原位";
            this.派遣被退回是否回到队列原位.Size = new System.Drawing.Size(153, 25);
            this.派遣被退回是否回到队列原位.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 18);
            this.label3.TabIndex = 24;
            this.label3.Text = "新加入对应本班次组技师插入方式";
            // 
            // 新加入对应本班次组技师插入方式
            // 
            this.新加入对应本班次组技师插入方式.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.新加入对应本班次组技师插入方式.FormattingEnabled = true;
            this.新加入对应本班次组技师插入方式.Items.AddRange(new object[] {
            "打头牌",
            "打尾牌",
            "本班次尾轮",
            "本班次0轮"});
            this.新加入对应本班次组技师插入方式.Location = new System.Drawing.Point(297, 170);
            this.新加入对应本班次组技师插入方式.Name = "新加入对应本班次组技师插入方式";
            this.新加入对应本班次组技师插入方式.Size = new System.Drawing.Size(207, 25);
            this.新加入对应本班次组技师插入方式.TabIndex = 25;
            // 
            // TechArrangeSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(931, 275);
            this.Controls.Add(this.派遣被退回是否回到队列原位);
            this.Controls.Add(this.到岗未到上班时间是否插入队列);
            this.Controls.Add(this.休息时间是否动牌);
            this.Controls.Add(this.班次内动牌模式);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.新加入对应本班次组技师插入方式);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.上钟被退回是否回到队列原位);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.上班时插入队列模式);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.动牌时间);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.班次内技师排序规则);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TechArrangeSetForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "排钟设置";
            this.Load += new System.EventHandler(this.SeatForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox 班次内技师排序规则;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox 班次内动牌模式;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox 动牌时间;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox 休息时间是否动牌;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox 上班时插入队列模式;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox 到岗未到上班时间是否插入队列;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox 上钟被退回是否回到队列原位;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox 派遣被退回是否回到队列原位;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox 新加入对应本班次组技师插入方式;
    }
}