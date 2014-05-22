namespace YouSoftBathOrder
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.BtnExpense = new System.Windows.Forms.Button();
            this.ExpenseOrder = new System.Windows.Forms.Button();
            this.BtnInputOrders = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExpense
            // 
            this.BtnExpense.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnExpense.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnExpense.Location = new System.Drawing.Point(171, 50);
            this.BtnExpense.Name = "BtnExpense";
            this.BtnExpense.Size = new System.Drawing.Size(443, 92);
            this.BtnExpense.TabIndex = 0;
            this.BtnExpense.Text = "消费查看";
            this.BtnExpense.UseVisualStyleBackColor = true;
            this.BtnExpense.Click += new System.EventHandler(this.BtnExpense_Click);
            // 
            // ExpenseOrder
            // 
            this.ExpenseOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExpenseOrder.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ExpenseOrder.Location = new System.Drawing.Point(171, 170);
            this.ExpenseOrder.Name = "ExpenseOrder";
            this.ExpenseOrder.Size = new System.Drawing.Size(443, 92);
            this.ExpenseOrder.TabIndex = 0;
            this.ExpenseOrder.Text = "消费录入";
            this.ExpenseOrder.UseVisualStyleBackColor = true;
            this.ExpenseOrder.Click += new System.EventHandler(this.ExpenseOrder_Click);
            // 
            // BtnInputOrders
            // 
            this.BtnInputOrders.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnInputOrders.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnInputOrders.Location = new System.Drawing.Point(171, 290);
            this.BtnInputOrders.Name = "BtnInputOrders";
            this.BtnInputOrders.Size = new System.Drawing.Size(443, 92);
            this.BtnInputOrders.TabIndex = 0;
            this.BtnInputOrders.Text = "录单汇总";
            this.BtnInputOrders.UseVisualStyleBackColor = true;
            this.BtnInputOrders.Click += new System.EventHandler(this.BtnInputOrders_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("宋体", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(171, 410);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(443, 92);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "退    出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(785, 553);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.BtnInputOrders);
            this.Controls.Add(this.ExpenseOrder);
            this.Controls.Add(this.BtnExpense);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.Text = "自动点单";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.OrderMainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExpense;
        private System.Windows.Forms.Button ExpenseOrder;
        private System.Windows.Forms.Button BtnInputOrders;
        private System.Windows.Forms.Button btnExit;
    }
}

