namespace YouSoftBathBack
{
    partial class MemberManageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberManageForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.employeePanel = new System.Windows.Forms.SplitContainer();
            this.typeTree = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.dbMoney = new System.Windows.Forms.Label();
            this.expense = new System.Windows.Forms.Label();
            this.dbNumber = new System.Windows.Forms.Label();
            this.lostMoney = new System.Windows.Forms.Label();
            this.lostNumber = new System.Windows.Forms.Label();
            this.onMoney = new System.Windows.Forms.Label();
            this.onNumber = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.Label();
            this.balance = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.name = new System.Windows.Forms.TextBox();
            this.card = new System.Windows.Forms.TextBox();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.delType = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.editType = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.findTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.DetailsTool = new System.Windows.Forms.ToolStripButton();
            this.toolEditMember = new System.Windows.Forms.ToolStripButton();
            this.toolDeduct = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.printTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.tool_del_card = new System.Windows.Forms.ToolStripButton();
            this.addType = new System.Windows.Forms.ToolStripButton();
            this.employeePanel.Panel1.SuspendLayout();
            this.employeePanel.Panel2.SuspendLayout();
            this.employeePanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // employeePanel
            // 
            this.employeePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.employeePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.employeePanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.employeePanel.IsSplitterFixed = true;
            this.employeePanel.Location = new System.Drawing.Point(0, 95);
            this.employeePanel.Margin = new System.Windows.Forms.Padding(4);
            this.employeePanel.Name = "employeePanel";
            // 
            // employeePanel.Panel1
            // 
            this.employeePanel.Panel1.Controls.Add(this.typeTree);
            // 
            // employeePanel.Panel2
            // 
            this.employeePanel.Panel2.Controls.Add(this.splitContainer1);
            this.employeePanel.Size = new System.Drawing.Size(1362, 626);
            this.employeePanel.SplitterDistance = 189;
            this.employeePanel.SplitterWidth = 8;
            this.employeePanel.TabIndex = 6;
            // 
            // typeTree
            // 
            this.typeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typeTree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.typeTree.ImageIndex = 0;
            this.typeTree.ImageList = this.imageList1;
            this.typeTree.Location = new System.Drawing.Point(0, 0);
            this.typeTree.Margin = new System.Windows.Forms.Padding(4);
            this.typeTree.Name = "typeTree";
            this.typeTree.SelectedImageIndex = 0;
            this.typeTree.Size = new System.Drawing.Size(185, 622);
            this.typeTree.TabIndex = 0;
            this.typeTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.typeTree_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "book_bookmark.png");
            this.imageList1.Images.SetKeyName(1, "book_open_bookmark.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.dbMoney);
            this.splitContainer1.Panel1.Controls.Add(this.expense);
            this.splitContainer1.Panel1.Controls.Add(this.dbNumber);
            this.splitContainer1.Panel1.Controls.Add(this.lostMoney);
            this.splitContainer1.Panel1.Controls.Add(this.lostNumber);
            this.splitContainer1.Panel1.Controls.Add(this.onMoney);
            this.splitContainer1.Panel1.Controls.Add(this.onNumber);
            this.splitContainer1.Panel1.Controls.Add(this.number);
            this.splitContainer1.Panel1.Controls.Add(this.balance);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label14);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.phone);
            this.splitContainer1.Panel1.Controls.Add(this.name);
            this.splitContainer1.Panel1.Controls.Add(this.card);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv);
            this.splitContainer1.Size = new System.Drawing.Size(1161, 622);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(1161, 2);
            this.label6.TabIndex = 21;
            // 
            // dbMoney
            // 
            this.dbMoney.AutoSize = true;
            this.dbMoney.ForeColor = System.Drawing.Color.Red;
            this.dbMoney.Location = new System.Drawing.Point(284, 47);
            this.dbMoney.Name = "dbMoney";
            this.dbMoney.Size = new System.Drawing.Size(89, 18);
            this.dbMoney.TabIndex = 20;
            this.dbMoney.Text = "888888888";
            this.dbMoney.Visible = false;
            // 
            // expense
            // 
            this.expense.AutoSize = true;
            this.expense.ForeColor = System.Drawing.Color.Red;
            this.expense.Location = new System.Drawing.Point(848, 11);
            this.expense.Name = "expense";
            this.expense.Size = new System.Drawing.Size(89, 18);
            this.expense.TabIndex = 20;
            this.expense.Text = "888888888";
            this.expense.Visible = false;
            // 
            // dbNumber
            // 
            this.dbNumber.AutoSize = true;
            this.dbNumber.ForeColor = System.Drawing.Color.Red;
            this.dbNumber.Location = new System.Drawing.Point(96, 47);
            this.dbNumber.Name = "dbNumber";
            this.dbNumber.Size = new System.Drawing.Size(89, 18);
            this.dbNumber.TabIndex = 20;
            this.dbNumber.Text = "888888888";
            this.dbNumber.Visible = false;
            // 
            // lostMoney
            // 
            this.lostMoney.AutoSize = true;
            this.lostMoney.ForeColor = System.Drawing.Color.Red;
            this.lostMoney.Location = new System.Drawing.Point(936, 47);
            this.lostMoney.Name = "lostMoney";
            this.lostMoney.Size = new System.Drawing.Size(89, 18);
            this.lostMoney.TabIndex = 20;
            this.lostMoney.Text = "888888888";
            this.lostMoney.Visible = false;
            // 
            // lostNumber
            // 
            this.lostNumber.AutoSize = true;
            this.lostNumber.ForeColor = System.Drawing.Color.Red;
            this.lostNumber.Location = new System.Drawing.Point(760, 47);
            this.lostNumber.Name = "lostNumber";
            this.lostNumber.Size = new System.Drawing.Size(89, 18);
            this.lostNumber.TabIndex = 20;
            this.lostNumber.Text = "888888888";
            this.lostNumber.Visible = false;
            // 
            // onMoney
            // 
            this.onMoney.AutoSize = true;
            this.onMoney.ForeColor = System.Drawing.Color.Red;
            this.onMoney.Location = new System.Drawing.Point(616, 47);
            this.onMoney.Name = "onMoney";
            this.onMoney.Size = new System.Drawing.Size(89, 18);
            this.onMoney.TabIndex = 20;
            this.onMoney.Text = "888888888";
            this.onMoney.Visible = false;
            // 
            // onNumber
            // 
            this.onNumber.AutoSize = true;
            this.onNumber.ForeColor = System.Drawing.Color.Red;
            this.onNumber.Location = new System.Drawing.Point(440, 47);
            this.onNumber.Name = "onNumber";
            this.onNumber.Size = new System.Drawing.Size(89, 18);
            this.onNumber.TabIndex = 20;
            this.onNumber.Text = "888888888";
            this.onNumber.Visible = false;
            // 
            // number
            // 
            this.number.AutoSize = true;
            this.number.ForeColor = System.Drawing.Color.Red;
            this.number.Location = new System.Drawing.Point(691, 11);
            this.number.Name = "number";
            this.number.Size = new System.Drawing.Size(89, 18);
            this.number.TabIndex = 20;
            this.number.Text = "888888888";
            this.number.Visible = false;
            // 
            // balance
            // 
            this.balance.AutoSize = true;
            this.balance.ForeColor = System.Drawing.Color.Red;
            this.balance.Location = new System.Drawing.Point(1004, 11);
            this.balance.Name = "balance";
            this.balance.Size = new System.Drawing.Size(89, 18);
            this.balance.TabIndex = 20;
            this.balance.Text = "888888888";
            this.balance.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(204, 47);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "入库金额";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(422, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "电话";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(780, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 20;
            this.label7.Text = "总消费:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(46, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 18);
            this.label9.TabIndex = 20;
            this.label9.Text = "入库";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(856, 47);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 18);
            this.label12.TabIndex = 20;
            this.label12.Text = "挂失金额";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "姓名";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(536, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(80, 18);
            this.label14.TabIndex = 20;
            this.label14.Text = "在用金额";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(718, 47);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 18);
            this.label11.TabIndex = 20;
            this.label11.Text = "挂失";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(610, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 18);
            this.label5.TabIndex = 20;
            this.label5.Text = "会员数量";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(398, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 18);
            this.label8.TabIndex = 20;
            this.label8.Text = "在用";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "卡号";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(936, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 18);
            this.label1.TabIndex = 20;
            this.label1.Text = "卡余额:";
            // 
            // phone
            // 
            this.phone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.phone.Location = new System.Drawing.Point(467, 7);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(127, 27);
            this.phone.TabIndex = 18;
            this.phone.Enter += new System.EventHandler(this.card_Enter);
            // 
            // name
            // 
            this.name.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.name.Location = new System.Drawing.Point(280, 7);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(127, 27);
            this.name.TabIndex = 18;
            this.name.Enter += new System.EventHandler(this.name_Enter);
            // 
            // card
            // 
            this.card.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.card.Location = new System.Drawing.Point(89, 7);
            this.card.Name = "card";
            this.card.Size = new System.Drawing.Size(127, 27);
            this.card.TabIndex = 18;
            this.card.Enter += new System.EventHandler(this.card_Enter);
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToOrderColumns = true;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column4,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(4);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 20;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(1161, 538);
            this.dgv.TabIndex = 2;
            this.dgv.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "会员卡号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 105;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "会员名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 105;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "会员类型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 105;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "优惠方案";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 105;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "卡状态";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 87;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "卡余额";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 87;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "会员积分";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 105;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "累计消费";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 105;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "销售人员";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 105;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "开户日期";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 105;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "最后使用";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 105;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addType,
            this.toolStripLabel1,
            this.delType,
            this.toolStripLabel2,
            this.editType,
            this.toolStripSeparator3,
            this.findTool,
            this.toolStripLabel4,
            this.tool_del_card,
            this.DetailsTool,
            this.toolEditMember,
            this.toolDeduct,
            this.toolStripSeparator1,
            this.exportTool,
            this.toolStripLabel5,
            this.printTool,
            this.toolStripLabel6,
            this.toolExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1362, 95);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel1.Text = "  ";
            // 
            // delType
            // 
            this.delType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.delType.Image = ((System.Drawing.Image)(resources.GetObject("delType.Image")));
            this.delType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delType.Name = "delType";
            this.delType.Size = new System.Drawing.Size(119, 92);
            this.delType.Text = "删除类型(F2)";
            this.delType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.delType.Click += new System.EventHandler(this.delType_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel2.Text = "  ";
            // 
            // editType
            // 
            this.editType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.editType.Image = ((System.Drawing.Image)(resources.GetObject("editType.Image")));
            this.editType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editType.Name = "editType";
            this.editType.Size = new System.Drawing.Size(119, 92);
            this.editType.Text = "编辑类型(F3)";
            this.editType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.editType.Click += new System.EventHandler(this.editType_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 95);
            // 
            // findTool
            // 
            this.findTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.findTool.Image = ((System.Drawing.Image)(resources.GetObject("findTool.Image")));
            this.findTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findTool.Name = "findTool";
            this.findTool.Size = new System.Drawing.Size(107, 92);
            this.findTool.Text = "查询(Enter)";
            this.findTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.findTool.Click += new System.EventHandler(this.findTool_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel4.Text = "  ";
            // 
            // DetailsTool
            // 
            this.DetailsTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.DetailsTool.Image = ((System.Drawing.Image)(resources.GetObject("DetailsTool.Image")));
            this.DetailsTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DetailsTool.Name = "DetailsTool";
            this.DetailsTool.Size = new System.Drawing.Size(119, 92);
            this.DetailsTool.Text = "消费详情(F6)";
            this.DetailsTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DetailsTool.Click += new System.EventHandler(this.DetailsTool_Click);
            // 
            // toolEditMember
            // 
            this.toolEditMember.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolEditMember.Image = ((System.Drawing.Image)(resources.GetObject("toolEditMember.Image")));
            this.toolEditMember.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEditMember.Name = "toolEditMember";
            this.toolEditMember.Size = new System.Drawing.Size(119, 92);
            this.toolEditMember.Text = "编辑会员(F3)";
            this.toolEditMember.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolEditMember.Click += new System.EventHandler(this.toolEditMember_Click);
            // 
            // toolDeduct
            // 
            this.toolDeduct.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolDeduct.Image = ((System.Drawing.Image)(resources.GetObject("toolDeduct.Image")));
            this.toolDeduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolDeduct.Name = "toolDeduct";
            this.toolDeduct.Size = new System.Drawing.Size(68, 92);
            this.toolDeduct.Text = "扣卡";
            this.toolDeduct.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolDeduct.Click += new System.EventHandler(this.toolDeduct_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 95);
            // 
            // exportTool
            // 
            this.exportTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.exportTool.Image = ((System.Drawing.Image)(resources.GetObject("exportTool.Image")));
            this.exportTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportTool.Name = "exportTool";
            this.exportTool.Size = new System.Drawing.Size(119, 92);
            this.exportTool.Text = "导出清单(F4)";
            this.exportTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.exportTool.Click += new System.EventHandler(this.exportTool_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel5.Text = "  ";
            // 
            // printTool
            // 
            this.printTool.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.printTool.Image = ((System.Drawing.Image)(resources.GetObject("printTool.Image")));
            this.printTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printTool.Name = "printTool";
            this.printTool.Size = new System.Drawing.Size(119, 92);
            this.printTool.Text = "打印清单(F5)";
            this.printTool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.printTool.Click += new System.EventHandler(this.printTool_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(16, 92);
            this.toolStripLabel6.Text = "  ";
            // 
            // toolExit
            // 
            this.toolExit.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(94, 92);
            this.toolExit.Text = "退出(ESC)";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // tool_del_card
            // 
            this.tool_del_card.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.tool_del_card.Image = ((System.Drawing.Image)(resources.GetObject("tool_del_card.Image")));
            this.tool_del_card.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tool_del_card.Name = "tool_del_card";
            this.tool_del_card.Size = new System.Drawing.Size(119, 92);
            this.tool_del_card.Text = "删除会员(F5)";
            this.tool_del_card.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tool_del_card.Click += new System.EventHandler(this.tool_del_card_Click);
            // 
            // addType
            // 
            this.addType.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.addType.Image = ((System.Drawing.Image)(resources.GetObject("addType.Image")));
            this.addType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addType.Name = "addType";
            this.addType.Size = new System.Drawing.Size(119, 92);
            this.addType.Text = "新增类型(F1)";
            this.addType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.addType.Click += new System.EventHandler(this.addType_Click);
            // 
            // MemberManageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 721);
            this.Controls.Add(this.employeePanel);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "MemberManageForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "会员卡管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MemberManageForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MemberManageForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.employeePanel.Panel1.ResumeLayout(false);
            this.employeePanel.Panel2.ResumeLayout(false);
            this.employeePanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer employeePanel;
        private System.Windows.Forms.TreeView typeTree;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton delType;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton editType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton DetailsTool;
        private System.Windows.Forms.ToolStripButton exportTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripButton printTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox card;
        private System.Windows.Forms.ToolStripButton findTool;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.ToolStripButton toolEditMember;
        private System.Windows.Forms.ToolStripButton toolDeduct;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label dbMoney;
        private System.Windows.Forms.Label expense;
        private System.Windows.Forms.Label dbNumber;
        private System.Windows.Forms.Label onNumber;
        private System.Windows.Forms.Label number;
        private System.Windows.Forms.Label balance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label onMoney;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lostMoney;
        private System.Windows.Forms.Label lostNumber;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ToolStripButton addType;
        private System.Windows.Forms.ToolStripButton tool_del_card;
    }
}