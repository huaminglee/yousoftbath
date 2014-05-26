using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using YouSoftBathConstants;
using YouSoftBathGeneralClass;
using YouSoftBathFormClass;
using System.Transactions;

using System.Threading;
using System.Timers;

namespace YouSoftBathBack
{
    public partial class MainWindow : Form
    {
        //成员变量
        public MainWindow()
        {
            InitializeComponent();
        }
        
        //构造函数
        public MainWindow(BathDBDataContext dc)
        {
            InitializeComponent();
        }

        //对话框载入
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(BathClass.internetTime().ToString());
            var db = new BathDBDataContext(LogIn.connectionString);
            pTable.Dock = DockStyle.Fill;
            pMember.Dock = DockStyle.Fill;
            pDocument.Dock = DockStyle.Fill;
            pReception.Dock = DockStyle.Fill;
            pStock.Dock = DockStyle.Fill;
            pData.Dock = DockStyle.Fill;

            pTable.Visible = true;
            pMember.Visible = false;
            pDocument.Visible = false;
            pReception.Visible = false;
            pStock.Visible = false;
            pData.Visible = false;

            int height = sp.Panel1.Height / 5;
            pnTable.Height = height;
            pnMember.Height = height;
            pnDocument.Height = height;
            pnStock.Height = height;
            pnData.Height = height;

            int x = picMember.Location.X;
            int y = (height - 52) / 2;
            picTable.Location = new Point(x, y);
            picMember.Location = new Point(x, y);
            picDocument.Location = new Point(x, y);
            picStock.Location = new Point(x, y);
            picData.Location = new Point(x, y);

            pnTable.BorderStyle = BorderStyle.Fixed3D;

            label4.Text = Constants.version;
            this.Text = Constants.appName + "-后台系统" + Constants.version + " 欢迎使用：" + LogIn.m_User.id;
            currentUser.Text = "当前用户:" + LogIn.m_User.id + "  " + LogIn.m_User.name;

            var td = new Thread(new ThreadStart(check_storageList));
            td.IsBackground = true;
            td.Start();
        }

        //检查库存
        private void check_storageList()
        {
            try
            {
                var db = new BathDBDataContext(LogIn.connectionString);
                string msg = "";
                var main_stock = db.Stock.FirstOrDefault(x => x.main != null && x.main.Value);
                if (main_stock == null)
                    return;

                var main_stock_id = main_stock.id;
                var stockIns = db.StockIn.Where(x => x.stockId == main_stock_id);
                var stockOuts = db.StockOut.Where(x => x.stockId == main_stock_id);
                var orderStockOuts = db.OrderStockOut.Where(x => x.stockId == main_stock_id && x.deleteEmployee == null);
                var pans = db.Pan.Where(x => x.stockId == main_stock_id);

                var msl = db.StorageList.Where(x => x.minAmount != null);
                foreach (var sl in msl)
                {
                    double number_Ins = 0;
                    double number_Outs = 0;
                    double number_OrderOuts = 0;
                    double number_pans = 0;

                    string name = sl.name;
                    var name_stockIns = stockIns.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_stockIns.Any())
                        number_Ins = name_stockIns.Sum(x => x.amount).Value;

                    var name_stockOuts = stockOuts.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_stockOuts.Any())
                        number_Outs = MConvert<double>.ToTypeOrDefault(name_stockOuts.Sum(x => x.amount), 0);

                    var name_orderStockOuts = orderStockOuts.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_orderStockOuts.Any())
                        number_OrderOuts = MConvert<double>.ToTypeOrDefault(name_orderStockOuts.Sum(x => x.amount), 0);

                    var name_pans = pans.Where(x => x.name == name).Where(x => x.amount != null);
                    if (name_pans.Any())
                        number_pans = MConvert<double>.ToTypeOrDefault(name_pans.Sum(x => x.amount), 0);

                    double number_now = number_Ins + number_pans - number_Outs - number_OrderOuts;
                    if (sl.minAmount >= number_now)
                    {
                        msg += main_stock.name + ":" + sl.name + "的库存量为" +
                            number_now.ToString() + "；已小于最低库存量:" + sl.minAmount + "\n";
                    }
                }
                if (msg != "")
                {
                    MessageBox.Show(msg);
                }
            }
            catch (System.Exception e)
            {
                BathClass.printErrorMsg(e.Message);
            }
        }

        //报表查询
        private void picTable_Click(object sender, EventArgs e)
        {
            pTable.Visible = true;
            pMember.Visible = false;
            pDocument.Visible = false;
            pReception.Visible = false;
            pStock.Visible = false;
            pData.Visible = false;

            pnTable.BorderStyle = BorderStyle.Fixed3D;
            pnMember.BorderStyle = BorderStyle.None;
            pnDocument.BorderStyle = BorderStyle.None;
            pnStock.BorderStyle = BorderStyle.None;
            pnData.BorderStyle = BorderStyle.None;
        }

        #region 报表查询
        //异常状况统计
        private void btnExceptionTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            ExceptionTableForm exceptionTableForm = new ExceptionTableForm();
            exceptionTableForm.ShowDialog();
        }

        //提成统计
        private void btnBonusTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "提成统计"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            BonusTableForm bonusTableForm = new BonusTableForm();
            bonusTableForm.ShowDialog();
        }

        //提前下钟统计
        private void btnAdvanceOff_Click(object sender, EventArgs e)
        {
            var form = new AdvanceOffForm();
            form.ShowDialog();
        }

        //手工打折汇总
        private void btnManulDiscount_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }

            ManulDiscountForm ManulDiscountForm = new ManulDiscountForm();
            ManulDiscountForm.ShowDialog();
        }

        //提成明细查询
        private void btnBounusDetailsTable_Click(object sender, EventArgs e)
        {

        }

        //项目报表
        private void btnItem_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            TableItemsForm tableItemsForm = new TableItemsForm();
            tableItemsForm.ShowDialog();
        }

        //信用卡统计
        private void btnCreditCardTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            CreditCardTableForm creditCardTableForm = new CreditCardTableForm();
            creditCardTableForm.ShowDialog();
        }

        //营业报表
        private void btnOverallTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "营业报表"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            DayOperationForm tableOperationForm = new DayOperationForm();
            tableOperationForm.ShowDialog();
        }

        //收银员收款统计
        private void btnCashierTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "收银员收款统计"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }

            var form = new TableCashierForm();
            form.ShowDialog();
            //CashierTableForm cashierTableForm = new CashierTableForm();
            //cashierTableForm.ShowDialog();
        }

        //结账单据查询
        private void btnBillTbale_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "收银单据查询"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            TableCashierCheckForm tableCashierCheckForm = new TableCashierCheckForm();
            tableCashierCheckForm.ShowDialog();
        }

        //月报表
        private void btnMonthTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MonthTableForm monthTableForm = new MonthTableForm();
            monthTableForm.ShowDialog();
        }

        //往来单位账目统计
        private void btnAccountWithTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "往来单位账目"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            AccountWithTableForm accountWithTableForm = new AccountWithTableForm();
            accountWithTableForm.ShowDialog();
        }

        //退单免单汇总
        private void btnReturnedBillTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            ReturnedBillTableForm returnedBillTableForm = new ReturnedBillTableForm();
            returnedBillTableForm.ShowDialog();
        }

        //支出统计
        private void btnExpense_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            ExpenseTableForm expenseTableForm = new ExpenseTableForm();
            expenseTableForm.ShowDialog();
        }
        #endregion

        //会员管理
        private void picMember_Click(object sender, EventArgs e)
        {
            pTable.Visible = false;
            pMember.Visible = true;
            pDocument.Visible = false;
            pReception.Visible = false;
            pStock.Visible = false;
            pData.Visible = false;

            pnTable.BorderStyle = BorderStyle.None;
            pnMember.BorderStyle = BorderStyle.Fixed3D;
            pnDocument.BorderStyle = BorderStyle.None;
            pnStock.BorderStyle = BorderStyle.None;
            pnData.BorderStyle = BorderStyle.None;
        }

        #region 会员管理
        //会员卡参数设置
        private void btnMemberSetting_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MemberSettingForm memberSettingForm = new MemberSettingForm();
            memberSettingForm.ShowDialog();
        }

        //优惠方案
        private void btnPromotion_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "优惠方案"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            PromotionManagementForm promotionManagementForm = new PromotionManagementForm();
            promotionManagementForm.ShowDialog();
        }

        //会员卡售卡及续卡统计
        private void btnMemberCardSale_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "会员售卡及充值统计"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MemberCardSaleForm memberCardSaleForm = new MemberCardSaleForm();
            memberCardSaleForm.ShowDialog();
        }

        //会员管理
        private void btnMemberManage_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MemberManageForm memberManageForm = new MemberManageForm();
            memberManageForm.ShowDialog();
        }

        //扣卡
        private void btnDeductCard_Click(object sender, EventArgs e)
        {
            //string pro = ((Button)sender).Text;
            //if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            //{
            //    GeneralClass.printErrorMsg("权限不够，不能访问!");
            //    return;
            //}
            //DeductedCardForm deductedCardForm = new DeductedCardForm(db, 0);
            //deductedCardForm.ShowDialog();
        }

        //会员分析
        private void btnMemberTable_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MemberAnalysisForm memberAnalysisForm = new MemberAnalysisForm();
            memberAnalysisForm.ShowDialog();
        }

        //会员开卡及续卡
        private void btnMemberOpenTable_Click(object sender, EventArgs e)
        {

        }

        //会员消费统计
        private void btnMemberExpense_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MemberExpenseTableForm memberExpenseTableForm = new MemberExpenseTableForm();
            memberExpenseTableForm.ShowDialog();
        }

        //会员积分查询
        private void btnMemberCredits_Click(object sender, EventArgs e)
        {

        }

        //会员积分操作日志
        private void btnMemberCreditsLog_Click(object sender, EventArgs e)
        {

        }

        //会员卡充值优惠
        private void btnPopSale_Click(object sender, EventArgs e)
        {
            CardPopSaleForm cardPopSaleForm = new CardPopSaleForm();
            cardPopSaleForm.ShowDialog();
        }
        #endregion

        //基础档案管理
        private void picDocument_Click(object sender, EventArgs e)
        {
            pTable.Visible = false;
            pMember.Visible = false;
            pDocument.Visible = true;
            pReception.Visible = false;
            pStock.Visible = false;
            pData.Visible = false;

            pnTable.BorderStyle = BorderStyle.None;
            pnMember.BorderStyle = BorderStyle.None;
            pnDocument.BorderStyle = BorderStyle.Fixed3D;
            pnStock.BorderStyle = BorderStyle.None;
            pnData.BorderStyle = BorderStyle.None;
        }

        #region 基础档案管理
        //手牌管理
        private void btnSeatManage_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            SeatManagementForm seatManagementForm = new SeatManagementForm();
            seatManagementForm.ShowDialog();
        }

        //券类管理
        private void btnCoupon_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            CouponManagement couponManagementForm = new CouponManagement();
            couponManagementForm.ShowDialog();
        }

        //服务档案管理
        private void btnWaiter_Click(object sender, EventArgs e)
        {
            WaitManagementForm form = new WaitManagementForm();
            form.ShowDialog();
        }

        //客户信息
        private void btnGuest1_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            AccountWithTableForm accountWithTableForm = new AccountWithTableForm();
            accountWithTableForm.ShowDialog();
        }

        //菜单管理
        private void btnMenu_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            MenuManagementForm menuForm = new MenuManagementForm();
            menuForm.ShowDialog();
        }

        //客房管理
        private void btnRoomManage_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "客房管理"))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            RoomManagementForm roomForm = new RoomManagementForm();
            roomForm.ShowDialog();
        }

        //员工管理
        private void btnEmployee_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            EmployeeManagementForm employeeManagementForm = new EmployeeManagementForm();
            employeeManagementForm.ShowDialog();
        }

        //部门日志管理
        private void btnDepartLog_Click(object sender, EventArgs e)
        {
            var form = new DepartLogMgForm();
            form.ShowDialog();
        }

        //套餐管理
        private void btnCombo_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            ComboManagementForm comboForm = new ComboManagementForm();
            comboForm.ShowDialog();
        }
        #endregion

        //仓库管理
        private void picStock_Click(object sender, EventArgs e)
        {
            pTable.Visible = false;
            pMember.Visible = false;
            pDocument.Visible = false;
            pReception.Visible = false;
            pStock.Visible = true;
            pData.Visible = false;

            pnTable.BorderStyle = BorderStyle.None;
            pnMember.BorderStyle = BorderStyle.None;
            pnDocument.BorderStyle = BorderStyle.None;
            pnStock.BorderStyle = BorderStyle.Fixed3D;
            pnData.BorderStyle = BorderStyle.None;
        }

        #region 仓库管理
        //库存参数
        private void btnStockOptions_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            StockOptionsForm stockSettingForm = new StockOptionsForm();
            stockSettingForm.ShowDialog();
        }

        //仓库设定
        private void btnStockSetting_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            StockSettingForm stockSettingForm = new StockSettingForm();
            stockSettingForm.ShowDialog();
        }
        
        //供应商管理
        private void btnProvideCretia_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }

            var form = new ProviderMgForm();
            form.ShowDialog();
        }

        //进货入库
        private void btnInStock_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            IntoStockManagementForm inStockForm = new IntoStockManagementForm();
            inStockForm.ShowDialog();
        }

        //现有库存
        private void btnStockList_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            StorageForm storageForm = new StorageForm();
            storageForm.ShowDialog();
        }

        //调货补货
        private void btnAdjust_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            TransferStockMangeForm transferStockMangeForm = new TransferStockMangeForm();
            transferStockMangeForm.ShowDialog();
        }

        //盘点清册
        private void btnCheckStock_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }

            StockTakeForm stockTakeForm = new StockTakeForm();
            stockTakeForm.ShowDialog();
        }

        //盘点调整
        private void btnAdjustStock_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
        }

        //应付账款
        private void btnPayableMoney_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }

        }
        #endregion

        //数据管理
        private void picData_Click(object sender, EventArgs e)
        {
            pTable.Visible = false;
            pMember.Visible = false;
            pDocument.Visible = false;
            pReception.Visible = false;
            pStock.Visible = false;
            pData.Visible = true;

            pnTable.BorderStyle = BorderStyle.None;
            pnMember.BorderStyle = BorderStyle.None;
            pnDocument.BorderStyle = BorderStyle.None;
            pnStock.BorderStyle = BorderStyle.None;
            pnData.BorderStyle = BorderStyle.Fixed3D;
        }

        #region 数据管理
        //系统设置
        private void btnSetting_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            SystemSettingForm regionForm = new SystemSettingForm();
            regionForm.ShowDialog();
        }

        //修改密码
        private void btnLog_Click(object sender, EventArgs e)
        {
            ModifyPwdForm modifyPwdForm = new ModifyPwdForm();
            modifyPwdForm.ShowDialog();
        }

        //数据优化
        private void btnOptimization_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            string pro = ((Button)sender).Text;
            if (!BathClass.getAuthority(db, LogIn.m_User, pro))
            {
                GeneralClass.printErrorMsg("权限不够，不能访问!");
                return;
            }
            OptimizationForm optimizationForm = new OptimizationForm();
            optimizationForm.ShowDialog();
        }

        //软件注册
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }

        //开牌登记
        private void btnSeat_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "开牌"))
            {
                GeneralClass.printErrorMsg("不具有权限");
                return;
            }
            ReceptionSeatForm receptionSeatForm = new ReceptionSeatForm();
            receptionSeatForm.ShowDialog();
        }

        //技师管理
        private void btnTechnician_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "技师管理"))
            {
                GeneralClass.printErrorMsg("不具有权限");
                return;
            }

            TechnicianSeclectForm technicianForm = new TechnicianSeclectForm();
            technicianForm.ShowDialog();
        }

        //包厢管理
        private void btnRoom_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "客房管理"))
            {
                GeneralClass.printErrorMsg("不具有权限");
                return;
            }

            if (MConvert<bool>.ToTypeOrDefault(db.Options.FirstOrDefault().启用客房面板, false))
            {
                RoomViewForm rvForm = new RoomViewForm();
                rvForm.ShowDialog();
            }
            else
            {
                var form = new CabViewForm();
                form.ShowDialog();
            }
        }

        //卡入库
        private void btnMemberPop_Click(object sender, EventArgs e)
        {
            var db = new BathDBDataContext(LogIn.connectionString);
            if (!BathClass.getAuthority(db, LogIn.m_User, "卡入库"))
            {
                GeneralClass.printErrorMsg("不具有权限");
                return;
            }

            var form = new OpenCardForm();
            form.ShowDialog();
        }


        #endregion

        //绑定快捷键
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (BathClass.printAskMsg("是否退出?") == DialogResult.Yes)
                        this.Close();
                    break;
                case Keys.F1:
                    //table_Click(null, null);
                    break;
                case Keys.F2:
                    //member_Click(null, null);
                    break;
                case Keys.F3:
                    //document_Click(null, null);
                    break;
                case Keys.F4:
                    //stock_Click(null, null);
                    break;
                case Keys.F5:
                    //reception_Click(null, null);
                    break;
                case Keys.F6:
                    //data_Click(null, null);
                    break;
                default:
                    break;
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MConvert<bool>.ToTypeOrDefault(LogIn.options.启用手牌锁, false) && LogIn.options.手牌锁类型 == "锦衣卫")
                JYW.CloseReader();
        }

        private void btnBigCombo_Click(object sender, EventArgs e)
        {
            BigComboManagementForm bigcomboform = new BigComboManagementForm();
            bigcomboform.ShowDialog();
        }

    }
}
