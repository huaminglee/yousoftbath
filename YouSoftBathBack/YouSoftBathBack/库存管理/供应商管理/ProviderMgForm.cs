using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using YouSoftBathGeneralClass;
using YouSoftBathFormClass;

namespace YouSoftBathBack
{
    public partial class ProviderMgForm : Form
    {
        //成员变量
        private BathDBDataContext db = null;

        //构造函数
        public ProviderMgForm()
        {
            db = new BathDBDataContext(LogIn.connectionString);
            InitializeComponent();
        }

        //对话框载入
        private void CouponManagement_Load(object sender, EventArgs e)
        {
            dgvProviderList_show();
        }

        //显示清单
        private void dgvProviderList_show()
        {
            dgvCustomerList.Rows.Clear();

            foreach (var p in db.Provider)
            {
                double ts = 0;
                var sis = db.StockIn.Where(x => x.providerId == p.id).Where(x => x.money != null);
                if (sis.Any())
                {
                    ts = sis.Sum(x => x.money).Value;
                }

                double ps = 0;
                var pps_cash = db.ProviderPays.Where(x => x.providerId == p.id).Where(x => x.cash != null);
                var pps_bank = db.ProviderPays.Where(x => x.providerId == p.id).Where(x => x.bank != null);
                if (pps_cash.Any())
                {
                    ps += pps_cash.Sum(x => x.cash).Value;
                }
                if (pps_bank.Any())
                {
                    ps += pps_bank.Sum(x => x.bank).Value;
                }
                dgvCustomerList.Rows.Add(p.name, ts, ps, ts-ps, p.contactor, p.mobile, p.tel, p.address, p.note);
            }
            BathClass.set_dgv_fit(dgvCustomerList);
        }

        //导出
        private void exportTool_Click(object sender, EventArgs e)
        {
            BathClass.exportDgvToExcel(dgvCustomerList);
        }

        //打印
        private void printTool_Click(object sender, EventArgs e)
        {
            PrintDGV.Print_DataGridView(dgvCustomerList, "供货商应付账款", false, "");
        }

        private void toolExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //绑定快捷键
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F4:
                    BathClass.exportDgvToExcel(dgvCustomerList);
                    break;
                case Keys.F5:
                    PrintDGV.Print_DataGridView(dgvCustomerList, "补货单统计", false, "");
                    break;
                default:
                    break;
            }
        }

        //供应商付款
        private void ProviderPayTool_Click(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell==null)
            {
                BathClass.printErrorMsg("为选择供应商!");
                return;
            }
            var p = db.Provider.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            var form = new ProviderPaysForm(db, p);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            int r = dgvCustomerList.CurrentCell.RowIndex;
            int c = dgvCustomerList.CurrentCell.ColumnIndex;
            dgvProviderList_show();
            dgvCustomerList.CurrentCell = dgvCustomerList[c, r];
            dgvProviderPays_show(p);
        }

        //新增供应商
        private void addProviderTool_Click(object sender, EventArgs e)
        {
            int rindex = -1;
            int cindex = -1;
            if (dgvCustomerList.CurrentCell != null)
            {
                rindex = dgvCustomerList.CurrentCell.RowIndex;
                cindex = dgvCustomerList.CurrentCell.ColumnIndex;
            }
            
            var form = new ProviderForm(db, null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvProviderList_show();
                if (rindex != -1 && cindex != -1)
                    dgvCustomerList.CurrentCell = dgvCustomerList[cindex, rindex];
            }
        }

        //删除供应商
        private void delProviderTool_Click(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell==null)
            {
                BathClass.printErrorMsg("需要选择行!");
                return;
            }

            var p = db.Provider.FirstOrDefault(x=>x.name==dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            if (db.StockIn.Any(x=>x.providerId==p.id) || db.ProviderPays.Any(x=>x.providerId==p.id))
            {
                BathClass.printErrorMsg("与供应商已有账务往来，不能删除!");
                return;
            }

            if (BathClass.printAskMsg("确认删除供应商" + p.name) != DialogResult.Yes)
                return;

            db.Provider.DeleteOnSubmit(p);
            db.SubmitChanges();
            dgvProviderList_show();
        }

        //编辑供应商
        private void editProviderTool_Click(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell == null)
            {
                BathClass.printErrorMsg("需要选择行!");
                return;
            }

            int rindex = dgvCustomerList.CurrentCell.RowIndex;
            int cindex = dgvCustomerList.CurrentCell.ColumnIndex;

            var p = db.Provider.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            var form = new ProviderForm(db, p);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dgvProviderList_show();
                dgvCustomerList.CurrentCell = dgvCustomerList[cindex, rindex];
            }
        }

        //当前供应商改变
        private void dgvProviderList_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentCell == null)
                return;

            var p = db.Provider.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            dgvStockIn_show(p);
            dgvProviderPays_show(p);
        }

        private void dgvStockIn_show(Provider p)
        {
            dgvProviderStockIn.Rows.Clear();
            var sis = db.StockIn.Where(x => x.providerId == p.id);
            foreach (var si in sis)
            {
                dgvProviderStockIn.Rows.Add(si.name, si.cost, si.amount, si.money, si.unit, si.date, si.transactor, si.checker);
            }
            BathClass.set_dgv_fit(dgvProviderStockIn);
        }

        private void dgvProviderPays_show(Provider p)
        {
            dgvProviderPays.Rows.Clear();
            var pps = db.ProviderPays.Where(x => x.providerId == p.id);
            foreach (var pp in pps)
            {
                dgvProviderPays.Rows.Add(pp.id, pp.cash, pp.bank, pp.date, pp.payer, pp.receiver, pp.confirmer, pp.note);
            }
            BathClass.set_dgv_fit(dgvProviderPays);
        }

        //删除付款信息
        private void delPayTool_Click(object sender, EventArgs e)
        {
            if (dgvProviderPays.CurrentCell==null)
            {
                BathClass.printErrorMsg("未选择供应商付款信息!");
                return;
            }

            if (BathClass.printAskMsg("确认删除供应商付款信息?") != DialogResult.Yes)
                return;

            var p = db.Provider.FirstOrDefault(x => x.name == dgvCustomerList.CurrentRow.Cells[0].Value.ToString());
            var pp = db.ProviderPays.FirstOrDefault(x=>x.id.ToString() == dgvProviderPays.CurrentRow.Cells[0].Value.ToString());
            db.ProviderPays.DeleteOnSubmit(pp);
            db.SubmitChanges();

            int r = dgvCustomerList.CurrentCell.RowIndex;
            int c = dgvCustomerList.CurrentCell.ColumnIndex;
            dgvProviderList_show();
            dgvCustomerList.CurrentCell = dgvCustomerList[c, r];
            dgvProviderPays_show(p);

        }
    }
}
