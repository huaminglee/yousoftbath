namespace YouSoftBathBack
{
    partial class MenuForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addAutomatic = new System.Windows.Forms.CheckBox();
            this.timeLimitType = new System.Windows.Forms.ComboBox();
            this.addType = new System.Windows.Forms.ComboBox();
            this.catgory = new System.Windows.Forms.ComboBox();
            this.unit = new System.Windows.Forms.ComboBox();
            this.timeLimitMiniute = new System.Windows.Forms.TextBox();
            this.timeLimitHour = new System.Windows.Forms.TextBox();
            this.price = new System.Windows.Forms.TextBox();
            this.addMoney = new System.Windows.Forms.TextBox();
            this.note = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.technician = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.canBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.orderRatio = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.onRatio = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.techRatioCat = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.techRatioType = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.waiterRatioType = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.waiterRatio = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.waiter = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.re_name = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.re_number = new System.Windows.Forms.TextBox();
            this.re_add = new System.Windows.Forms.Button();
            this.re_del = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addAutomatic);
            this.groupBox1.Controls.Add(this.timeLimitType);
            this.groupBox1.Controls.Add(this.addType);
            this.groupBox1.Controls.Add(this.catgory);
            this.groupBox1.Controls.Add(this.unit);
            this.groupBox1.Controls.Add(this.timeLimitMiniute);
            this.groupBox1.Controls.Add(this.timeLimitHour);
            this.groupBox1.Controls.Add(this.price);
            this.groupBox1.Controls.Add(this.addMoney);
            this.groupBox1.Controls.Add(this.note);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.name);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(604, 206);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "项目信息";
            // 
            // addAutomatic
            // 
            this.addAutomatic.AutoSize = true;
            this.addAutomatic.Location = new System.Drawing.Point(13, 117);
            this.addAutomatic.Name = "addAutomatic";
            this.addAutomatic.Size = new System.Drawing.Size(171, 22);
            this.addAutomatic.TabIndex = 4;
            this.addAutomatic.Text = "是否自动超时加收";
            this.addAutomatic.UseVisualStyleBackColor = true;
            this.addAutomatic.CheckedChanged += new System.EventHandler(this.addAutomatic_CheckedChanged);
            // 
            // timeLimitType
            // 
            this.timeLimitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeLimitType.Enabled = false;
            this.timeLimitType.FormattingEnabled = true;
            this.timeLimitType.Items.AddRange(new object[] {
            "限时长",
            "限时间"});
            this.timeLimitType.Location = new System.Drawing.Point(273, 71);
            this.timeLimitType.Name = "timeLimitType";
            this.timeLimitType.Size = new System.Drawing.Size(130, 25);
            this.timeLimitType.TabIndex = 2;
            this.timeLimitType.SelectedIndexChanged += new System.EventHandler(this.addType_SelectedIndexChanged);
            // 
            // addType
            // 
            this.addType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addType.Enabled = false;
            this.addType.FormattingEnabled = true;
            this.addType.Items.AddRange(new object[] {
            "按项目单位",
            "按时间"});
            this.addType.Location = new System.Drawing.Point(273, 116);
            this.addType.Name = "addType";
            this.addType.Size = new System.Drawing.Size(130, 25);
            this.addType.TabIndex = 2;
            this.addType.SelectedIndexChanged += new System.EventHandler(this.addType_SelectedIndexChanged);
            // 
            // catgory
            // 
            this.catgory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.catgory.FormattingEnabled = true;
            this.catgory.Location = new System.Drawing.Point(459, 28);
            this.catgory.Name = "catgory";
            this.catgory.Size = new System.Drawing.Size(130, 25);
            this.catgory.TabIndex = 2;
            // 
            // unit
            // 
            this.unit.FormattingEnabled = true;
            this.unit.Location = new System.Drawing.Point(273, 28);
            this.unit.Name = "unit";
            this.unit.Size = new System.Drawing.Size(130, 25);
            this.unit.TabIndex = 2;
            this.unit.Enter += new System.EventHandler(this.name_Enter);
            // 
            // timeLimitMiniute
            // 
            this.timeLimitMiniute.ImeMode = System.Windows.Forms.ImeMode.On;
            this.timeLimitMiniute.Location = new System.Drawing.Point(507, 70);
            this.timeLimitMiniute.Name = "timeLimitMiniute";
            this.timeLimitMiniute.Size = new System.Drawing.Size(34, 27);
            this.timeLimitMiniute.TabIndex = 3;
            this.timeLimitMiniute.Text = "0";
            this.timeLimitMiniute.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.price_KeyPress);
            this.timeLimitMiniute.Enter += new System.EventHandler(this.price_Enter);
            // 
            // timeLimitHour
            // 
            this.timeLimitHour.ImeMode = System.Windows.Forms.ImeMode.On;
            this.timeLimitHour.Location = new System.Drawing.Point(459, 70);
            this.timeLimitHour.Name = "timeLimitHour";
            this.timeLimitHour.Size = new System.Drawing.Size(34, 27);
            this.timeLimitHour.TabIndex = 3;
            this.timeLimitHour.Text = "0";
            this.timeLimitHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.price_KeyPress);
            this.timeLimitHour.Enter += new System.EventHandler(this.price_Enter);
            // 
            // price
            // 
            this.price.ImeMode = System.Windows.Forms.ImeMode.On;
            this.price.Location = new System.Drawing.Point(53, 70);
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(130, 27);
            this.price.TabIndex = 3;
            this.price.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.price_KeyPress);
            this.price.Enter += new System.EventHandler(this.price_Enter);
            // 
            // addMoney
            // 
            this.addMoney.Enabled = false;
            this.addMoney.ImeMode = System.Windows.Forms.ImeMode.On;
            this.addMoney.Location = new System.Drawing.Point(459, 115);
            this.addMoney.Name = "addMoney";
            this.addMoney.Size = new System.Drawing.Size(67, 27);
            this.addMoney.TabIndex = 3;
            this.addMoney.Text = "10";
            this.addMoney.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.price_KeyPress);
            this.addMoney.Enter += new System.EventHandler(this.price_Enter);
            // 
            // note
            // 
            this.note.ImeMode = System.Windows.Forms.ImeMode.On;
            this.note.Location = new System.Drawing.Point(78, 153);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(511, 27);
            this.note.TabIndex = 3;
            this.note.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(492, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 14);
            this.label7.TabIndex = 0;
            this.label7.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "限时方式";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(195, 119);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "加收方式";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "单价";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(532, 119);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 18);
            this.label12.TabIndex = 0;
            this.label12.Text = "元";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(416, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 18);
            this.label10.TabIndex = 0;
            this.label10.Text = "每时";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(416, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "限时";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(416, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "类别";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "说  明";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "单    位";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(53, 27);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(130, 27);
            this.name.TabIndex = 1;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称";
            // 
            // technician
            // 
            this.technician.AutoSize = true;
            this.technician.Location = new System.Drawing.Point(13, 24);
            this.technician.Name = "technician";
            this.technician.Size = new System.Drawing.Size(135, 22);
            this.technician.TabIndex = 4;
            this.technician.Text = "是否需要技师";
            this.technician.UseVisualStyleBackColor = true;
            this.technician.CheckedChanged += new System.EventHandler(this.technician_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.canBtn);
            this.panel1.Controls.Add(this.okBtn);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 457);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(954, 47);
            this.panel1.TabIndex = 6;
            // 
            // canBtn
            // 
            this.canBtn.AutoSize = true;
            this.canBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.canBtn.Location = new System.Drawing.Point(549, 9);
            this.canBtn.Name = "canBtn";
            this.canBtn.Size = new System.Drawing.Size(117, 28);
            this.canBtn.TabIndex = 9;
            this.canBtn.Text = "取消(ESC)";
            this.canBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.AutoSize = true;
            this.okBtn.Location = new System.Drawing.Point(289, 9);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(117, 28);
            this.okBtn.TabIndex = 8;
            this.okBtn.Text = "确定(ENTER)";
            this.okBtn.UseVisualStyleBackColor = true;
            this.okBtn.Click += new System.EventHandler(this.okBtn_Click);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.Dock = System.Windows.Forms.DockStyle.Top;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(954, 2);
            this.label11.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.orderRatio);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.onRatio);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.techRatioCat);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.techRatioType);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.technician);
            this.groupBox2.Location = new System.Drawing.Point(14, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(604, 143);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "技师提成";
            // 
            // orderRatio
            // 
            this.orderRatio.Enabled = false;
            this.orderRatio.ImeMode = System.Windows.Forms.ImeMode.On;
            this.orderRatio.Location = new System.Drawing.Point(274, 102);
            this.orderRatio.Name = "orderRatio";
            this.orderRatio.Size = new System.Drawing.Size(100, 27);
            this.orderRatio.TabIndex = 8;
            this.orderRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.orderRatio_KeyPress);
            this.orderRatio.Enter += new System.EventHandler(this.price_Enter);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(152, 106);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 18);
            this.label15.TabIndex = 7;
            this.label15.Text = "点钟提成比例";
            // 
            // onRatio
            // 
            this.onRatio.Enabled = false;
            this.onRatio.ImeMode = System.Windows.Forms.ImeMode.On;
            this.onRatio.Location = new System.Drawing.Point(274, 62);
            this.onRatio.Name = "onRatio";
            this.onRatio.Size = new System.Drawing.Size(100, 27);
            this.onRatio.TabIndex = 8;
            this.onRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onRatio_KeyPress);
            this.onRatio.Enter += new System.EventHandler(this.price_Enter);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(381, 103);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 24);
            this.label17.TabIndex = 7;
            this.label17.Text = "%";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(381, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 24);
            this.label16.TabIndex = 7;
            this.label16.Text = "%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(152, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 18);
            this.label14.TabIndex = 7;
            this.label14.Text = "上钟提成比例";
            // 
            // techRatioCat
            // 
            this.techRatioCat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.techRatioCat.FormattingEnabled = true;
            this.techRatioCat.Items.AddRange(new object[] {
            "按金额",
            "按比例"});
            this.techRatioCat.Location = new System.Drawing.Point(274, 23);
            this.techRatioCat.Name = "techRatioCat";
            this.techRatioCat.Size = new System.Drawing.Size(100, 25);
            this.techRatioCat.TabIndex = 6;
            this.techRatioCat.SelectedIndexChanged += new System.EventHandler(this.techRatioCat_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(408, 26);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 18);
            this.label19.TabIndex = 5;
            this.label19.Text = "提成基数";
            // 
            // techRatioType
            // 
            this.techRatioType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.techRatioType.Enabled = false;
            this.techRatioType.FormattingEnabled = true;
            this.techRatioType.Items.AddRange(new object[] {
            "按原价",
            "按实收"});
            this.techRatioType.Location = new System.Drawing.Point(489, 23);
            this.techRatioType.Name = "techRatioType";
            this.techRatioType.Size = new System.Drawing.Size(100, 25);
            this.techRatioType.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(152, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 18);
            this.label13.TabIndex = 5;
            this.label13.Text = "技师提成方式";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.waiterRatioType);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.waiterRatio);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.waiter);
            this.groupBox3.Location = new System.Drawing.Point(14, 225);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(604, 69);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "服务员提成";
            // 
            // waiterRatioType
            // 
            this.waiterRatioType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.waiterRatioType.Enabled = false;
            this.waiterRatioType.FormattingEnabled = true;
            this.waiterRatioType.Items.AddRange(new object[] {
            "按比例",
            "按价格"});
            this.waiterRatioType.Location = new System.Drawing.Point(258, 22);
            this.waiterRatioType.Name = "waiterRatioType";
            this.waiterRatioType.Size = new System.Drawing.Size(103, 25);
            this.waiterRatioType.TabIndex = 10;
            this.waiterRatioType.SelectedIndexChanged += new System.EventHandler(this.waiterRatioType_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(177, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(80, 18);
            this.label18.TabIndex = 9;
            this.label18.Text = "提成方式";
            // 
            // waiterRatio
            // 
            this.waiterRatio.Enabled = false;
            this.waiterRatio.ImeMode = System.Windows.Forms.ImeMode.On;
            this.waiterRatio.Location = new System.Drawing.Point(465, 21);
            this.waiterRatio.Name = "waiterRatio";
            this.waiterRatio.Size = new System.Drawing.Size(82, 27);
            this.waiterRatio.TabIndex = 8;
            this.waiterRatio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onRatio_KeyPress);
            this.waiterRatio.Enter += new System.EventHandler(this.price_Enter);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(552, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 24);
            this.label20.TabIndex = 7;
            this.label20.Text = "%";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(382, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(80, 18);
            this.label21.TabIndex = 7;
            this.label21.Text = "提成比例";
            // 
            // waiter
            // 
            this.waiter.AutoSize = true;
            this.waiter.Location = new System.Drawing.Point(13, 23);
            this.waiter.Name = "waiter";
            this.waiter.Size = new System.Drawing.Size(153, 22);
            this.waiter.TabIndex = 4;
            this.waiter.Text = "是否需要服务员";
            this.waiter.UseVisualStyleBackColor = true;
            this.waiter.CheckedChanged += new System.EventHandler(this.waiter_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.panel2);
            this.groupBox4.Controls.Add(this.re_del);
            this.groupBox4.Controls.Add(this.re_add);
            this.groupBox4.Controls.Add(this.re_number);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.re_name);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Location = new System.Drawing.Point(637, 13);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(290, 430);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "资源消耗信息";
            // 
            // re_name
            // 
            this.re_name.Location = new System.Drawing.Point(55, 28);
            this.re_name.Name = "re_name";
            this.re_name.Size = new System.Drawing.Size(148, 27);
            this.re_name.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 32);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(44, 18);
            this.label22.TabIndex = 2;
            this.label22.Text = "名称";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 73);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(44, 18);
            this.label23.TabIndex = 2;
            this.label23.Text = "数量";
            // 
            // re_number
            // 
            this.re_number.Location = new System.Drawing.Point(55, 69);
            this.re_number.Name = "re_number";
            this.re_number.Size = new System.Drawing.Size(148, 27);
            this.re_number.TabIndex = 3;
            this.re_number.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.re_number_KeyPress);
            // 
            // re_add
            // 
            this.re_add.Location = new System.Drawing.Point(209, 27);
            this.re_add.Name = "re_add";
            this.re_add.Size = new System.Drawing.Size(75, 28);
            this.re_add.TabIndex = 4;
            this.re_add.Text = "添加";
            this.re_add.UseVisualStyleBackColor = true;
            this.re_add.Click += new System.EventHandler(this.re_add_Click);
            // 
            // re_del
            // 
            this.re_del.Location = new System.Drawing.Point(209, 66);
            this.re_del.Name = "re_del";
            this.re_del.Size = new System.Drawing.Size(75, 28);
            this.re_del.TabIndex = 4;
            this.re_del.Text = "删除";
            this.re_del.UseVisualStyleBackColor = true;
            this.re_del.Click += new System.EventHandler(this.re_del_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(284, 325);
            this.panel2.TabIndex = 5;
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 20;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(284, 325);
            this.dgv.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "耗材名称";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "耗材数量";
            this.Column2.Name = "Column2";
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 504);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MenuForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新增项目";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox technician;
        private System.Windows.Forms.ComboBox catgory;
        private System.Windows.Forms.ComboBox unit;
        private System.Windows.Forms.TextBox timeLimitMiniute;
        private System.Windows.Forms.TextBox timeLimitHour;
        private System.Windows.Forms.TextBox note;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox price;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox addAutomatic;
        private System.Windows.Forms.ComboBox addType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox addMoney;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button canBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox techRatioType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox onRatio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox orderRatio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox timeLimitType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox waiterRatio;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox waiter;
        private System.Windows.Forms.ComboBox waiterRatioType;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox techRatioCat;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox re_number;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox re_name;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button re_add;
        private System.Windows.Forms.Button re_del;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}