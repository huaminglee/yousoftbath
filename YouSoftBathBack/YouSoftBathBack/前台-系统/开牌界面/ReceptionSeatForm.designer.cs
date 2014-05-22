namespace YouSoftBathBack
{
    partial class ReceptionSeatForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceptionSeatForm));
            this.seatContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CtxAddNote = new System.Windows.Forms.ToolStripMenuItem();
            this.unWarnTool = new System.Windows.Forms.ToolStripMenuItem();
            this.toolExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tSeat = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.LseatTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.seatTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel20 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LseatAvi = new System.Windows.Forms.ToolStripStatusLabel();
            this.seatAvi = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel21 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LmoneyTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.moneyTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel22 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LseatPaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.seatPaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel23 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LmoneyPaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.moneyPaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel24 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LseatUnpaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.seatUnpaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel25 = new System.Windows.Forms.ToolStripStatusLabel();
            this.LmoneyUnpaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.moneyUnpaid = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.aviTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.useTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.payTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.lockTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.stopTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.warnTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.depositTip = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.seatTab = new System.Windows.Forms.TabControl();
            this.seatPanel = new System.Windows.Forms.Panel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.seatContext.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // seatContext
            // 
            this.seatContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CtxAddNote,
            this.unWarnTool});
            this.seatContext.Name = "contextMenuStrip1";
            this.seatContext.Size = new System.Drawing.Size(125, 48);
            // 
            // CtxAddNote
            // 
            this.CtxAddNote.Name = "CtxAddNote";
            this.CtxAddNote.Size = new System.Drawing.Size(124, 22);
            this.CtxAddNote.Text = "添加备注";
            this.CtxAddNote.Click += new System.EventHandler(this.CtxAddNote_Click);
            // 
            // unWarnTool
            // 
            this.unWarnTool.Name = "unWarnTool";
            this.unWarnTool.Size = new System.Drawing.Size(124, 22);
            this.unWarnTool.Text = "解除警告";
            this.unWarnTool.Click += new System.EventHandler(this.unWarnTool_Click);
            // 
            // toolExit
            // 
            this.toolExit.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolExit.Image = ((System.Drawing.Image)(resources.GetObject("toolExit.Image")));
            this.toolExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolExit.Name = "toolExit";
            this.toolExit.Size = new System.Drawing.Size(114, 92);
            this.toolExit.Text = "退    出(ESC)";
            this.toolExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolExit.Click += new System.EventHandler(this.toolExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 95);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(139, 92);
            this.toolStripLabel1.Text = "查看手牌(Enter)";
            // 
            // tSeat
            // 
            this.tSeat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tSeat.Font = new System.Drawing.Font("微软雅黑", 28F);
            this.tSeat.Name = "tSeat";
            this.tSeat.Size = new System.Drawing.Size(100, 95);
            this.tSeat.Enter += new System.EventHandler(this.tSeat_Enter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolExit,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tSeat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1362, 95);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip2
            // 
            this.statusStrip2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LseatTotal,
            this.seatTotal,
            this.toolStripStatusLabel20,
            this.LseatAvi,
            this.seatAvi,
            this.toolStripStatusLabel21,
            this.LmoneyTotal,
            this.moneyTotal,
            this.toolStripStatusLabel22,
            this.LseatPaid,
            this.seatPaid,
            this.toolStripStatusLabel23,
            this.LmoneyPaid,
            this.moneyPaid,
            this.toolStripStatusLabel24,
            this.LseatUnpaid,
            this.seatUnpaid,
            this.toolStripStatusLabel25,
            this.LmoneyUnpaid,
            this.moneyUnpaid});
            this.statusStrip2.Location = new System.Drawing.Point(0, 690);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1362, 26);
            this.statusStrip2.TabIndex = 10;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // LseatTotal
            // 
            this.LseatTotal.Font = new System.Drawing.Font("宋体", 12F);
            this.LseatTotal.Name = "LseatTotal";
            this.LseatTotal.Size = new System.Drawing.Size(80, 21);
            this.LseatTotal.Text = "总台位数:";
            // 
            // seatTotal
            // 
            this.seatTotal.Font = new System.Drawing.Font("宋体", 12F);
            this.seatTotal.Name = "seatTotal";
            this.seatTotal.Size = new System.Drawing.Size(40, 21);
            this.seatTotal.Text = "7000";
            // 
            // toolStripStatusLabel20
            // 
            this.toolStripStatusLabel20.Name = "toolStripStatusLabel20";
            this.toolStripStatusLabel20.Size = new System.Drawing.Size(20, 21);
            this.toolStripStatusLabel20.Text = "  ";
            // 
            // LseatAvi
            // 
            this.LseatAvi.Font = new System.Drawing.Font("宋体", 12F);
            this.LseatAvi.Name = "LseatAvi";
            this.LseatAvi.Size = new System.Drawing.Size(96, 21);
            this.LseatAvi.Text = "空闲台位数:";
            // 
            // seatAvi
            // 
            this.seatAvi.Font = new System.Drawing.Font("宋体", 12F);
            this.seatAvi.Name = "seatAvi";
            this.seatAvi.Size = new System.Drawing.Size(32, 21);
            this.seatAvi.Text = "100";
            // 
            // toolStripStatusLabel21
            // 
            this.toolStripStatusLabel21.Name = "toolStripStatusLabel21";
            this.toolStripStatusLabel21.Size = new System.Drawing.Size(60, 21);
            this.toolStripStatusLabel21.Text = "          ";
            // 
            // LmoneyTotal
            // 
            this.LmoneyTotal.Font = new System.Drawing.Font("宋体", 12F);
            this.LmoneyTotal.Name = "LmoneyTotal";
            this.LmoneyTotal.Size = new System.Drawing.Size(80, 21);
            this.LmoneyTotal.Text = "合计消费:";
            // 
            // moneyTotal
            // 
            this.moneyTotal.Font = new System.Drawing.Font("宋体", 12F);
            this.moneyTotal.Name = "moneyTotal";
            this.moneyTotal.Size = new System.Drawing.Size(40, 21);
            this.moneyTotal.Text = "7000";
            // 
            // toolStripStatusLabel22
            // 
            this.toolStripStatusLabel22.Name = "toolStripStatusLabel22";
            this.toolStripStatusLabel22.Size = new System.Drawing.Size(25, 21);
            this.toolStripStatusLabel22.Text = "   ";
            // 
            // LseatPaid
            // 
            this.LseatPaid.Font = new System.Drawing.Font("宋体", 12F);
            this.LseatPaid.Name = "LseatPaid";
            this.LseatPaid.Size = new System.Drawing.Size(64, 21);
            this.LseatPaid.Text = "已结账:";
            // 
            // seatPaid
            // 
            this.seatPaid.Font = new System.Drawing.Font("宋体", 12F);
            this.seatPaid.Name = "seatPaid";
            this.seatPaid.Size = new System.Drawing.Size(24, 21);
            this.seatPaid.Text = "10";
            // 
            // toolStripStatusLabel23
            // 
            this.toolStripStatusLabel23.Name = "toolStripStatusLabel23";
            this.toolStripStatusLabel23.Size = new System.Drawing.Size(15, 21);
            this.toolStripStatusLabel23.Text = " ";
            // 
            // LmoneyPaid
            // 
            this.LmoneyPaid.Font = new System.Drawing.Font("宋体", 12F);
            this.LmoneyPaid.Name = "LmoneyPaid";
            this.LmoneyPaid.Size = new System.Drawing.Size(48, 21);
            this.LmoneyPaid.Text = "金额:";
            // 
            // moneyPaid
            // 
            this.moneyPaid.Font = new System.Drawing.Font("宋体", 12F);
            this.moneyPaid.Name = "moneyPaid";
            this.moneyPaid.Size = new System.Drawing.Size(40, 21);
            this.moneyPaid.Text = "7000";
            // 
            // toolStripStatusLabel24
            // 
            this.toolStripStatusLabel24.Name = "toolStripStatusLabel24";
            this.toolStripStatusLabel24.Size = new System.Drawing.Size(30, 21);
            this.toolStripStatusLabel24.Text = "    ";
            // 
            // LseatUnpaid
            // 
            this.LseatUnpaid.Font = new System.Drawing.Font("宋体", 12F);
            this.LseatUnpaid.Name = "LseatUnpaid";
            this.LseatUnpaid.Size = new System.Drawing.Size(64, 21);
            this.LseatUnpaid.Text = "未结账:";
            // 
            // seatUnpaid
            // 
            this.seatUnpaid.Font = new System.Drawing.Font("宋体", 12F);
            this.seatUnpaid.Name = "seatUnpaid";
            this.seatUnpaid.Size = new System.Drawing.Size(40, 21);
            this.seatUnpaid.Text = "7000";
            // 
            // toolStripStatusLabel25
            // 
            this.toolStripStatusLabel25.Name = "toolStripStatusLabel25";
            this.toolStripStatusLabel25.Size = new System.Drawing.Size(15, 21);
            this.toolStripStatusLabel25.Text = " ";
            // 
            // LmoneyUnpaid
            // 
            this.LmoneyUnpaid.Font = new System.Drawing.Font("宋体", 12F);
            this.LmoneyUnpaid.Name = "LmoneyUnpaid";
            this.LmoneyUnpaid.Size = new System.Drawing.Size(48, 21);
            this.LmoneyUnpaid.Text = "金额:";
            // 
            // moneyUnpaid
            // 
            this.moneyUnpaid.Font = new System.Drawing.Font("宋体", 12F);
            this.moneyUnpaid.Name = "moneyUnpaid";
            this.moneyUnpaid.Size = new System.Drawing.Size(40, 21);
            this.moneyUnpaid.Text = "7000";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.currentUser,
            this.toolStripStatusLabel3,
            this.statusTip,
            this.aviTip,
            this.useTip,
            this.payTip,
            this.lockTip,
            this.stopTip,
            this.warnTip,
            this.depositTip,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 716);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1362, 26);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.TabStop = true;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 21);
            // 
            // currentUser
            // 
            this.currentUser.Name = "currentUser";
            this.currentUser.Size = new System.Drawing.Size(171, 21);
            this.currentUser.Text = "toolStripStatusLabel2";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(30, 21);
            this.toolStripStatusLabel3.Text = "    ";
            // 
            // statusTip
            // 
            this.statusTip.Font = new System.Drawing.Font("宋体", 10F);
            this.statusTip.Name = "statusTip";
            this.statusTip.Size = new System.Drawing.Size(77, 21);
            this.statusTip.Text = "状态说明：";
            // 
            // aviTip
            // 
            this.aviTip.BackColor = System.Drawing.Color.White;
            this.aviTip.Font = new System.Drawing.Font("宋体", 10F);
            this.aviTip.Name = "aviTip";
            this.aviTip.Size = new System.Drawing.Size(91, 21);
            this.aviTip.Text = "    可用    ";
            // 
            // useTip
            // 
            this.useTip.BackColor = System.Drawing.Color.Cyan;
            this.useTip.Font = new System.Drawing.Font("宋体", 10F);
            this.useTip.Name = "useTip";
            this.useTip.Size = new System.Drawing.Size(63, 21);
            this.useTip.Text = "正在使用";
            // 
            // payTip
            // 
            this.payTip.BackColor = System.Drawing.Color.Gray;
            this.payTip.Font = new System.Drawing.Font("宋体", 10F);
            this.payTip.Name = "payTip";
            this.payTip.Size = new System.Drawing.Size(91, 21);
            this.payTip.Text = "    结账    ";
            // 
            // lockTip
            // 
            this.lockTip.BackColor = System.Drawing.Color.Orange;
            this.lockTip.Font = new System.Drawing.Font("宋体", 10F);
            this.lockTip.Name = "lockTip";
            this.lockTip.Size = new System.Drawing.Size(91, 21);
            this.lockTip.Text = "    锁定    ";
            // 
            // stopTip
            // 
            this.stopTip.BackColor = System.Drawing.Color.Red;
            this.stopTip.Name = "stopTip";
            this.stopTip.Size = new System.Drawing.Size(82, 21);
            this.stopTip.Text = "    停用    ";
            // 
            // warnTip
            // 
            this.warnTip.BackColor = System.Drawing.Color.Yellow;
            this.warnTip.Name = "warnTip";
            this.warnTip.Size = new System.Drawing.Size(94, 21);
            this.warnTip.Text = "　  警告　  ";
            // 
            // depositTip
            // 
            this.depositTip.BackColor = System.Drawing.Color.Violet;
            this.depositTip.Name = "depositTip";
            this.depositTip.Size = new System.Drawing.Size(74, 21);
            this.depositTip.Text = "押金离场";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(74, 21);
            this.toolStripStatusLabel2.Text = "重新结账";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.seatTab);
            this.panel1.Controls.Add(this.seatPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1362, 595);
            this.panel1.TabIndex = 11;
            // 
            // seatTab
            // 
            this.seatTab.Location = new System.Drawing.Point(843, 296);
            this.seatTab.Name = "seatTab";
            this.seatTab.SelectedIndex = 0;
            this.seatTab.Size = new System.Drawing.Size(200, 100);
            this.seatTab.TabIndex = 14;
            this.seatTab.TabStop = false;
            // 
            // seatPanel
            // 
            this.seatPanel.AutoScroll = true;
            this.seatPanel.BackColor = System.Drawing.Color.White;
            this.seatPanel.Location = new System.Drawing.Point(320, 198);
            this.seatPanel.Name = "seatPanel";
            this.seatPanel.Size = new System.Drawing.Size(297, 114);
            this.seatPanel.TabIndex = 13;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.Color.SpringGreen;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(74, 21);
            this.toolStripStatusLabel4.Text = "预定客房";
            // 
            // ReceptionSeatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 742);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "ReceptionSeatForm";
            this.ShowInTaskbar = false;
            this.Text = "前台";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReceptionSeatForm_Load);
            this.SizeChanged += new System.EventHandler(this.ReceptionSeatForm_SizeChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceptionSeatForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.seatContext.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip seatContext;
        private System.Windows.Forms.ToolStripMenuItem CtxAddNote;
        private System.Windows.Forms.ToolStripButton toolExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tSeat;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel LseatTotal;
        private System.Windows.Forms.ToolStripStatusLabel seatTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel20;
        private System.Windows.Forms.ToolStripStatusLabel LseatAvi;
        private System.Windows.Forms.ToolStripStatusLabel seatAvi;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel21;
        private System.Windows.Forms.ToolStripStatusLabel LmoneyTotal;
        private System.Windows.Forms.ToolStripStatusLabel moneyTotal;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel22;
        private System.Windows.Forms.ToolStripStatusLabel LseatPaid;
        private System.Windows.Forms.ToolStripStatusLabel seatPaid;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel23;
        private System.Windows.Forms.ToolStripStatusLabel LmoneyPaid;
        private System.Windows.Forms.ToolStripStatusLabel moneyPaid;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel24;
        private System.Windows.Forms.ToolStripStatusLabel LseatUnpaid;
        private System.Windows.Forms.ToolStripStatusLabel seatUnpaid;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel25;
        private System.Windows.Forms.ToolStripStatusLabel LmoneyUnpaid;
        private System.Windows.Forms.ToolStripStatusLabel moneyUnpaid;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel currentUser;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel statusTip;
        private System.Windows.Forms.ToolStripStatusLabel aviTip;
        private System.Windows.Forms.ToolStripStatusLabel useTip;
        private System.Windows.Forms.ToolStripStatusLabel payTip;
        private System.Windows.Forms.ToolStripStatusLabel lockTip;
        private System.Windows.Forms.ToolStripStatusLabel stopTip;
        private System.Windows.Forms.ToolStripStatusLabel warnTip;
        private System.Windows.Forms.ToolStripStatusLabel depositTip;
        private System.Windows.Forms.ToolStripMenuItem unWarnTool;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl seatTab;
        private System.Windows.Forms.Panel seatPanel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
    }
}

