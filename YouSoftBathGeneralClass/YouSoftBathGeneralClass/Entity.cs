using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouSoftBathConstants;

namespace YouSoftBathGeneralClass
{
    #region 表Account
    public class CAccount
    {
        private int _id;
		
		private string _text;
		
		private string _systemId;
		
		private string _openTime;
		
		private string _openEmployee;
		
		private System.DateTime _payTime;
		
		private string _payEmployee;
		
		private string _name;
		
		private string _promotionMemberId;
		
		private System.Nullable<double> _promotionAmount;
		
		private string _memberId;
		
		private string _discountEmployee;
		
		private System.Nullable<double> _discount;
		
		private string _serverEmployee;
		
		private System.Nullable<double> _cash;
		
		private System.Nullable<double> _bankUnion;
		
		private System.Nullable<double> _creditCard;
		
		private System.Nullable<double> _coupon;
		
		private System.Nullable<double> _groupBuy;
		
		private System.Nullable<double> _zero;
		
		private System.Nullable<double> _server;
		
		private System.Nullable<double> _deducted;
		
		private System.Nullable<double> _changes;
		
		private System.Nullable<double> _wipeZero;
		
		private string _macAddress;
		
		private string _abandon;
		
		private System.Nullable<int> _departmentId;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
		
		public string openTime
		{
			get
			{
				return this._openTime;
			}
			set
			{
				if ((this._openTime != value))
				{
					this._openTime = value;
				}
			}
		}
		
		public string openEmployee
		{
			get
			{
				return this._openEmployee;
			}
			set
			{
				if ((this._openEmployee != value))
				{
					this._openEmployee = value;
				}
			}
		}
		
		public System.DateTime payTime
		{
			get
			{
				return this._payTime;
			}
			set
			{
				if ((this._payTime != value))
				{
					this._payTime = value;
				}
			}
		}
		
		public string payEmployee
		{
			get
			{
				return this._payEmployee;
			}
			set
			{
				if ((this._payEmployee != value))
				{
					this._payEmployee = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string promotionMemberId
		{
			get
			{
				return this._promotionMemberId;
			}
			set
			{
				if ((this._promotionMemberId != value))
				{
					this._promotionMemberId = value;
				}
			}
		}
		
		public System.Nullable<double> promotionAmount
		{
			get
			{
				return this._promotionAmount;
			}
			set
			{
				if ((this._promotionAmount != value))
				{
					this._promotionAmount = value;
				}
			}
		}
		
		public string memberId
		{
			get
			{
				return this._memberId;
			}
			set
			{
				if ((this._memberId != value))
				{
					this._memberId = value;
				}
			}
		}
		
		public string discountEmployee
		{
			get
			{
				return this._discountEmployee;
			}
			set
			{
				if ((this._discountEmployee != value))
				{
					this._discountEmployee = value;
				}
			}
		}
		
		public System.Nullable<double> discount
		{
			get
			{
				return this._discount;
			}
			set
			{
				if ((this._discount != value))
				{
					this._discount = value;
				}
			}
		}
		
		public string serverEmployee
		{
			get
			{
				return this._serverEmployee;
			}
			set
			{
				if ((this._serverEmployee != value))
				{
					this._serverEmployee = value;
				}
			}
		}
		
		public System.Nullable<double> cash
		{
			get
			{
				return this._cash;
			}
			set
			{
				if ((this._cash != value))
				{
					this._cash = value;
				}
			}
		}
		
		public System.Nullable<double> bankUnion
		{
			get
			{
				return this._bankUnion;
			}
			set
			{
				if ((this._bankUnion != value))
				{
					this._bankUnion = value;
				}
			}
		}
		
		public System.Nullable<double> creditCard
		{
			get
			{
				return this._creditCard;
			}
			set
			{
				if ((this._creditCard != value))
				{
					this._creditCard = value;
				}
			}
		}
		
		public System.Nullable<double> coupon
		{
			get
			{
				return this._coupon;
			}
			set
			{
				if ((this._coupon != value))
				{
					this._coupon = value;
				}
			}
		}
		
		public System.Nullable<double> groupBuy
		{
			get
			{
				return this._groupBuy;
			}
			set
			{
				if ((this._groupBuy != value))
				{
					this._groupBuy = value;
				}
			}
		}
		
		public System.Nullable<double> zero
		{
			get
			{
				return this._zero;
			}
			set
			{
				if ((this._zero != value))
				{
					this._zero = value;
				}
			}
		}
		
		public System.Nullable<double> server
		{
			get
			{
				return this._server;
			}
			set
			{
				if ((this._server != value))
				{
					this._server = value;
				}
			}
		}
		
		public System.Nullable<double> deducted
		{
			get
			{
				return this._deducted;
			}
			set
			{
				if ((this._deducted != value))
				{
					this._deducted = value;
				}
			}
		}
		
		public System.Nullable<double> changes
		{
			get
			{
				return this._changes;
			}
			set
			{
				if ((this._changes != value))
				{
					this._changes = value;
				}
			}
		}
		
		public System.Nullable<double> wipeZero
		{
			get
			{
				return this._wipeZero;
			}
			set
			{
				if ((this._wipeZero != value))
				{
					this._wipeZero = value;
				}
			}
		}
		
		public string macAddress
		{
			get
			{
				return this._macAddress;
			}
			set
			{
				if ((this._macAddress != value))
				{
					this._macAddress = value;
				}
			}
		}
		
		public string abandon
		{
			get
			{
				return this._abandon;
			}
			set
			{
				if ((this._abandon != value))
				{
					this._abandon = value;
				}
			}
		}
		
		public System.Nullable<int> departmentId
		{
			get
			{
				return this._departmentId;
			}
			set
			{
				if ((this._departmentId != value))
				{
					this._departmentId = value;
				}
			}
		}
		
    }
    #endregion

    #region 表Authority
    public class CAuthority
    {
        private int _id;
		
		private string _emplyeeId;
		
		private System.Nullable<int> _jobId;
		
		private System.Nullable<bool> _开牌;
		
		private System.Nullable<bool> _取消开牌;
		
		private System.Nullable<bool> _更换手牌;
		
		private System.Nullable<bool> _锁定解锁;
		
		private System.Nullable<bool> _解除警告;
		
		private System.Nullable<bool> _停用启用;
		
		private System.Nullable<bool> _添加备注;
		
		private System.Nullable<bool> _挂失手牌;
		
		private System.Nullable<bool> _完整点单;
		
		private System.Nullable<bool> _可见本人点单;
		
		private System.Nullable<bool> _退单;
		
		private System.Nullable<bool> _手工打折;
		
		private System.Nullable<bool> _签字免单;
		
		private System.Nullable<bool> _转账;
		
		private System.Nullable<bool> _重新结账;
		
		private System.Nullable<bool> _结账;
		
		private System.Nullable<bool> _技师管理;
		
		private System.Nullable<bool> _收银汇总统计;
		
		private System.Nullable<bool> _包房管理;
		
		private System.Nullable<bool> _收银单据查询;
		
		private System.Nullable<bool> _录单汇总;
		
		private System.Nullable<bool> _营业信息查看;
		
		private System.Nullable<bool> _售卡;
		
		private System.Nullable<bool> _充值;
		
		private System.Nullable<bool> _挂失;
		
		private System.Nullable<bool> _补卡;
		
		private System.Nullable<bool> _读卡;
		
		private System.Nullable<bool> _扣卡;
		
		private System.Nullable<bool> _卡入库;
		
		private System.Nullable<bool> _异常状况统计;
		
		private System.Nullable<bool> _提成统计;
		
		private System.Nullable<bool> _手工打折汇总;
		
		private System.Nullable<bool> _项目报表;
		
		private System.Nullable<bool> _信用卡统计;
		
		private System.Nullable<bool> _营业报表;
		
		private System.Nullable<bool> _退免单汇总;
		
		private System.Nullable<bool> _支出统计;
		
		private System.Nullable<bool> _收银员收款统计;
		
		private System.Nullable<bool> _月报表;
		
		private System.Nullable<bool> _往来单位账目;
		
		private System.Nullable<bool> _会员积分设置;
		
		private System.Nullable<bool> _优惠方案;
		
		private System.Nullable<bool> _会员分析;
		
		private System.Nullable<bool> _会员管理;
		
		private System.Nullable<bool> _会员消费统计;
		
		private System.Nullable<bool> _会员售卡及充值统计;
		
		private System.Nullable<bool> _手牌管理;
		
		private System.Nullable<bool> _券类管理;
		
		private System.Nullable<bool> _客户管理;
		
		private System.Nullable<bool> _项目档案管理;
		
		private System.Nullable<bool> _客房管理;
		
		private System.Nullable<bool> _员工管理;
		
		private System.Nullable<bool> _权限管理;
		
		private System.Nullable<bool> _套餐管理;
		
		private System.Nullable<bool> _库存参数;
		
		private System.Nullable<bool> _仓库设定;
		
		private System.Nullable<bool> _补货标准;
		
		private System.Nullable<bool> _进货入库;
		
		private System.Nullable<bool> _现有库存;
		
		private System.Nullable<bool> _调货补货;
		
		private System.Nullable<bool> _盘点清册;
		
		private System.Nullable<bool> _盘点调整;
		
		private System.Nullable<bool> _应付账款;
		
		private System.Nullable<bool> _系统设置;
		
		private System.Nullable<bool> _数据优化;
		
		private System.Nullable<bool> _收银报表;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string emplyeeId
		{
			get
			{
				return this._emplyeeId;
			}
			set
			{
				if ((this._emplyeeId != value))
				{
					this._emplyeeId = value;
				}
			}
		}
		
		public System.Nullable<int> jobId
		{
			get
			{
				return this._jobId;
			}
			set
			{
				if ((this._jobId != value))
				{
					this._jobId = value;
				}
			}
		}
		
		public System.Nullable<bool> 开牌
		{
			get
			{
				return this._开牌;
			}
			set
			{
				if ((this._开牌 != value))
				{
					this._开牌 = value;
				}
			}
		}
		
		public System.Nullable<bool> 取消开牌
		{
			get
			{
				return this._取消开牌;
			}
			set
			{
				if ((this._取消开牌 != value))
				{
					this._取消开牌 = value;
				}
			}
		}
		
		public System.Nullable<bool> 更换手牌
		{
			get
			{
				return this._更换手牌;
			}
			set
			{
				if ((this._更换手牌 != value))
				{
					this._更换手牌 = value;
				}
			}
		}
		
		public System.Nullable<bool> 锁定解锁
		{
			get
			{
				return this._锁定解锁;
			}
			set
			{
				if ((this._锁定解锁 != value))
				{
					this._锁定解锁 = value;
				}
			}
		}
		
		public System.Nullable<bool> 解除警告
		{
			get
			{
				return this._解除警告;
			}
			set
			{
				if ((this._解除警告 != value))
				{
					this._解除警告 = value;
				}
			}
		}
		
		public System.Nullable<bool> 停用启用
		{
			get
			{
				return this._停用启用;
			}
			set
			{
				if ((this._停用启用 != value))
				{
					this._停用启用 = value;
				}
			}
		}
		
		public System.Nullable<bool> 添加备注
		{
			get
			{
				return this._添加备注;
			}
			set
			{
				if ((this._添加备注 != value))
				{
					this._添加备注 = value;
				}
			}
		}
		
		public System.Nullable<bool> 挂失手牌
		{
			get
			{
				return this._挂失手牌;
			}
			set
			{
				if ((this._挂失手牌 != value))
				{
					this._挂失手牌 = value;
				}
			}
		}
		
		public System.Nullable<bool> 完整点单
		{
			get
			{
				return this._完整点单;
			}
			set
			{
				if ((this._完整点单 != value))
				{
					this._完整点单 = value;
				}
			}
		}
		
		public System.Nullable<bool> 可见本人点单
		{
			get
			{
				return this._可见本人点单;
			}
			set
			{
				if ((this._可见本人点单 != value))
				{
					this._可见本人点单 = value;
				}
			}
		}
		
		public System.Nullable<bool> 退单
		{
			get
			{
				return this._退单;
			}
			set
			{
				if ((this._退单 != value))
				{
					this._退单 = value;
				}
			}
		}
		
		public System.Nullable<bool> 手工打折
		{
			get
			{
				return this._手工打折;
			}
			set
			{
				if ((this._手工打折 != value))
				{
					this._手工打折 = value;
				}
			}
		}
		
		public System.Nullable<bool> 签字免单
		{
			get
			{
				return this._签字免单;
			}
			set
			{
				if ((this._签字免单 != value))
				{
					this._签字免单 = value;
				}
			}
		}
		
		public System.Nullable<bool> 转账
		{
			get
			{
				return this._转账;
			}
			set
			{
				if ((this._转账 != value))
				{
					this._转账 = value;
				}
			}
		}
		
		public System.Nullable<bool> 重新结账
		{
			get
			{
				return this._重新结账;
			}
			set
			{
				if ((this._重新结账 != value))
				{
					this._重新结账 = value;
				}
			}
		}
		
		public System.Nullable<bool> 结账
		{
			get
			{
				return this._结账;
			}
			set
			{
				if ((this._结账 != value))
				{
					this._结账 = value;
				}
			}
		}
		
		public System.Nullable<bool> 技师管理
		{
			get
			{
				return this._技师管理;
			}
			set
			{
				if ((this._技师管理 != value))
				{
					this._技师管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 收银汇总统计
		{
			get
			{
				return this._收银汇总统计;
			}
			set
			{
				if ((this._收银汇总统计 != value))
				{
					this._收银汇总统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 包房管理
		{
			get
			{
				return this._包房管理;
			}
			set
			{
				if ((this._包房管理 != value))
				{
					this._包房管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 收银单据查询
		{
			get
			{
				return this._收银单据查询;
			}
			set
			{
				if ((this._收银单据查询 != value))
				{
					this._收银单据查询 = value;
				}
			}
		}
		
		public System.Nullable<bool> 录单汇总
		{
			get
			{
				return this._录单汇总;
			}
			set
			{
				if ((this._录单汇总 != value))
				{
					this._录单汇总 = value;
				}
			}
		}
		
		public System.Nullable<bool> 营业信息查看
		{
			get
			{
				return this._营业信息查看;
			}
			set
			{
				if ((this._营业信息查看 != value))
				{
					this._营业信息查看 = value;
				}
			}
		}
		
		public System.Nullable<bool> 售卡
		{
			get
			{
				return this._售卡;
			}
			set
			{
				if ((this._售卡 != value))
				{
					this._售卡 = value;
				}
			}
		}
		
		public System.Nullable<bool> 充值
		{
			get
			{
				return this._充值;
			}
			set
			{
				if ((this._充值 != value))
				{
					this._充值 = value;
				}
			}
		}
		
		public System.Nullable<bool> 挂失
		{
			get
			{
				return this._挂失;
			}
			set
			{
				if ((this._挂失 != value))
				{
					this._挂失 = value;
				}
			}
		}
		
		public System.Nullable<bool> 补卡
		{
			get
			{
				return this._补卡;
			}
			set
			{
				if ((this._补卡 != value))
				{
					this._补卡 = value;
				}
			}
		}
		
		public System.Nullable<bool> 读卡
		{
			get
			{
				return this._读卡;
			}
			set
			{
				if ((this._读卡 != value))
				{
					this._读卡 = value;
				}
			}
		}
		
		public System.Nullable<bool> 扣卡
		{
			get
			{
				return this._扣卡;
			}
			set
			{
				if ((this._扣卡 != value))
				{
					this._扣卡 = value;
				}
			}
		}
		
		public System.Nullable<bool> 卡入库
		{
			get
			{
				return this._卡入库;
			}
			set
			{
				if ((this._卡入库 != value))
				{
					this._卡入库 = value;
				}
			}
		}
		
		public System.Nullable<bool> 异常状况统计
		{
			get
			{
				return this._异常状况统计;
			}
			set
			{
				if ((this._异常状况统计 != value))
				{
					this._异常状况统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 提成统计
		{
			get
			{
				return this._提成统计;
			}
			set
			{
				if ((this._提成统计 != value))
				{
					this._提成统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 手工打折汇总
		{
			get
			{
				return this._手工打折汇总;
			}
			set
			{
				if ((this._手工打折汇总 != value))
				{
					this._手工打折汇总 = value;
				}
			}
		}
		
		public System.Nullable<bool> 项目报表
		{
			get
			{
				return this._项目报表;
			}
			set
			{
				if ((this._项目报表 != value))
				{
					this._项目报表 = value;
				}
			}
		}
		
		public System.Nullable<bool> 信用卡统计
		{
			get
			{
				return this._信用卡统计;
			}
			set
			{
				if ((this._信用卡统计 != value))
				{
					this._信用卡统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 营业报表
		{
			get
			{
				return this._营业报表;
			}
			set
			{
				if ((this._营业报表 != value))
				{
					this._营业报表 = value;
				}
			}
		}
		
		public System.Nullable<bool> 退免单汇总
		{
			get
			{
				return this._退免单汇总;
			}
			set
			{
				if ((this._退免单汇总 != value))
				{
					this._退免单汇总 = value;
				}
			}
		}
		
		public System.Nullable<bool> 支出统计
		{
			get
			{
				return this._支出统计;
			}
			set
			{
				if ((this._支出统计 != value))
				{
					this._支出统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 收银员收款统计
		{
			get
			{
				return this._收银员收款统计;
			}
			set
			{
				if ((this._收银员收款统计 != value))
				{
					this._收银员收款统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 月报表
		{
			get
			{
				return this._月报表;
			}
			set
			{
				if ((this._月报表 != value))
				{
					this._月报表 = value;
				}
			}
		}
		
		public System.Nullable<bool> 往来单位账目
		{
			get
			{
				return this._往来单位账目;
			}
			set
			{
				if ((this._往来单位账目 != value))
				{
					this._往来单位账目 = value;
				}
			}
		}
		
		public System.Nullable<bool> 会员积分设置
		{
			get
			{
				return this._会员积分设置;
			}
			set
			{
				if ((this._会员积分设置 != value))
				{
					this._会员积分设置 = value;
				}
			}
		}
		
		public System.Nullable<bool> 优惠方案
		{
			get
			{
				return this._优惠方案;
			}
			set
			{
				if ((this._优惠方案 != value))
				{
					this._优惠方案 = value;
				}
			}
		}
		
		public System.Nullable<bool> 会员分析
		{
			get
			{
				return this._会员分析;
			}
			set
			{
				if ((this._会员分析 != value))
				{
					this._会员分析 = value;
				}
			}
		}
		
		public System.Nullable<bool> 会员管理
		{
			get
			{
				return this._会员管理;
			}
			set
			{
				if ((this._会员管理 != value))
				{
					this._会员管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 会员消费统计
		{
			get
			{
				return this._会员消费统计;
			}
			set
			{
				if ((this._会员消费统计 != value))
				{
					this._会员消费统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 会员售卡及充值统计
		{
			get
			{
				return this._会员售卡及充值统计;
			}
			set
			{
				if ((this._会员售卡及充值统计 != value))
				{
					this._会员售卡及充值统计 = value;
				}
			}
		}
		
		public System.Nullable<bool> 手牌管理
		{
			get
			{
				return this._手牌管理;
			}
			set
			{
				if ((this._手牌管理 != value))
				{
					this._手牌管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 券类管理
		{
			get
			{
				return this._券类管理;
			}
			set
			{
				if ((this._券类管理 != value))
				{
					this._券类管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 客户管理
		{
			get
			{
				return this._客户管理;
			}
			set
			{
				if ((this._客户管理 != value))
				{
					this._客户管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 项目档案管理
		{
			get
			{
				return this._项目档案管理;
			}
			set
			{
				if ((this._项目档案管理 != value))
				{
					this._项目档案管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 客房管理
		{
			get
			{
				return this._客房管理;
			}
			set
			{
				if ((this._客房管理 != value))
				{
					this._客房管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 员工管理
		{
			get
			{
				return this._员工管理;
			}
			set
			{
				if ((this._员工管理 != value))
				{
					this._员工管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 权限管理
		{
			get
			{
				return this._权限管理;
			}
			set
			{
				if ((this._权限管理 != value))
				{
					this._权限管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 套餐管理
		{
			get
			{
				return this._套餐管理;
			}
			set
			{
				if ((this._套餐管理 != value))
				{
					this._套餐管理 = value;
				}
			}
		}
		
		public System.Nullable<bool> 库存参数
		{
			get
			{
				return this._库存参数;
			}
			set
			{
				if ((this._库存参数 != value))
				{
					this._库存参数 = value;
				}
			}
		}
		
		public System.Nullable<bool> 仓库设定
		{
			get
			{
				return this._仓库设定;
			}
			set
			{
				if ((this._仓库设定 != value))
				{
					this._仓库设定 = value;
				}
			}
		}
		
		public System.Nullable<bool> 补货标准
		{
			get
			{
				return this._补货标准;
			}
			set
			{
				if ((this._补货标准 != value))
				{
					this._补货标准 = value;
				}
			}
		}
		
		public System.Nullable<bool> 进货入库
		{
			get
			{
				return this._进货入库;
			}
			set
			{
				if ((this._进货入库 != value))
				{
					this._进货入库 = value;
				}
			}
		}
		
		public System.Nullable<bool> 现有库存
		{
			get
			{
				return this._现有库存;
			}
			set
			{
				if ((this._现有库存 != value))
				{
					this._现有库存 = value;
				}
			}
		}
		
		public System.Nullable<bool> 调货补货
		{
			get
			{
				return this._调货补货;
			}
			set
			{
				if ((this._调货补货 != value))
				{
					this._调货补货 = value;
				}
			}
		}
		
		public System.Nullable<bool> 盘点清册
		{
			get
			{
				return this._盘点清册;
			}
			set
			{
				if ((this._盘点清册 != value))
				{
					this._盘点清册 = value;
				}
			}
		}
		
		public System.Nullable<bool> 盘点调整
		{
			get
			{
				return this._盘点调整;
			}
			set
			{
				if ((this._盘点调整 != value))
				{
					this._盘点调整 = value;
				}
			}
		}
		
		public System.Nullable<bool> 应付账款
		{
			get
			{
				return this._应付账款;
			}
			set
			{
				if ((this._应付账款 != value))
				{
					this._应付账款 = value;
				}
			}
		}
		
		public System.Nullable<bool> 系统设置
		{
			get
			{
				return this._系统设置;
			}
			set
			{
				if ((this._系统设置 != value))
				{
					this._系统设置 = value;
				}
			}
		}
		
		public System.Nullable<bool> 数据优化
		{
			get
			{
				return this._数据优化;
			}
			set
			{
				if ((this._数据优化 != value))
				{
					this._数据优化 = value;
				}
			}
		}
		
		public System.Nullable<bool> 收银报表
		{
			get
			{
				return this._收银报表;
			}
			set
			{
				if ((this._收银报表 != value))
				{
					this._收银报表 = value;
				}
			}
		}
		
    }

    #endregion

    #region 表BarMsg

    public class CBarMsg
    {
        private int _id;
		
		private string _roomId;
		
		private string _msg;
		
		private System.DateTime _time;
		
		private System.Nullable<bool> _read;
		
		private string _seatId;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				if ((this._roomId != value))
				{
					this._roomId = value;
				}
			}
		}
		
		public string msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				if ((this._msg != value))
				{
					this._msg = value;
				}
			}
		}
		
		public System.DateTime time
		{
			get
			{
				return this._time;
			}
			set
			{
				if ((this._time != value))
				{
					this._time = value;
				}
			}
		}
		
		public System.Nullable<bool> read
		{
			get
			{
				return this._read;
			}
			set
			{
				if ((this._read != value))
				{
					this._read = value;
				}
			}
		}
		
		public string seatId
		{
			get
			{
				return this._seatId;
			}
			set
			{
				if ((this._seatId != value))
				{
					this._seatId = value;
				}
			}
		}
    }

    #endregion

    #region 表CardCharge

    public class CCardCharge
    {
        private int _CC_Id;
		
		private string _CC_CardNo;
		
		private string _CC_ItemExplain;
		
		private System.Nullable<double> _CC_DebitSum;
		
		private System.Nullable<double> _CC_LenderSum;
		
		private string _CC_InputOperator;
		
		private System.Nullable<System.DateTime> _CC_InputDate;
		
		private string _CC_Station;
		
		private System.Nullable<double> _expense;
		
		private string _systemId;
		
		public int CC_Id
		{
			get
			{
				return this._CC_Id;
			}
			set
			{
				if ((this._CC_Id != value))
				{
					this._CC_Id = value;
				}
			}
		}
		
		public string CC_CardNo
		{
			get
			{
				return this._CC_CardNo;
			}
			set
			{
				if ((this._CC_CardNo != value))
				{
					this._CC_CardNo = value;
				}
			}
		}
		
		
		public string CC_ItemExplain
		{
			get
			{
				return this._CC_ItemExplain;
			}
			set
			{
				if ((this._CC_ItemExplain != value))
				{
					this._CC_ItemExplain = value;
				}
			}
		}
		
		public System.Nullable<double> CC_DebitSum
		{
			get
			{
				return this._CC_DebitSum;
			}
			set
			{
				if ((this._CC_DebitSum != value))
				{
					this._CC_DebitSum = value;
				}
			}
		}
		
		public System.Nullable<double> CC_LenderSum
		{
			get
			{
				return this._CC_LenderSum;
			}
			set
			{
				if ((this._CC_LenderSum != value))
				{
					this._CC_LenderSum = value;
				}
			}
		}
		
		public string CC_InputOperator
		{
			get
			{
				return this._CC_InputOperator;
			}
			set
			{
				if ((this._CC_InputOperator != value))
				{
					this._CC_InputOperator = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> CC_InputDate
		{
			get
			{
				return this._CC_InputDate;
			}
			set
			{
				if ((this._CC_InputDate != value))
				{
					this._CC_InputDate = value;
				}
			}
		}
		
		
		public string CC_Station
		{
			get
			{
				return this._CC_Station;
			}
			set
			{
				if ((this._CC_Station != value))
				{
					this._CC_Station = value;
				}
			}
		}
		
		public System.Nullable<double> expense
		{
			get
			{
				return this._expense;
			}
			set
			{
				if ((this._expense != value))
				{
					this._expense = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
    }

    #endregion

    #region 表CardInfo

    public class CCardInfo
    {
        private string _CI_CardNo;
		
		private string _CI_SystemICNo;
		
		private System.Nullable<int> _CI_CardTypeNo;
		
		private string _CI_Name;
		
		private string _CI_Sexno;
		
		private string _CI_Address;
		
		private string _CI_Telephone;
		
		private string _CI_Remark;
		
		private System.Nullable<System.DateTime> _CI_SendCardDate;
		
		private string _CI_SendCardOperator;
		
		private System.Nullable<decimal> _CI_DiscountRate;
		
		private System.Nullable<decimal> _CI_DiscountRatepos;
		
		private System.Nullable<decimal> _CI_Limitation;
		
		private System.Nullable<char> _CI_CardStateNo;
		
		private System.Nullable<System.DateTime> _CI_UsefulLife;
		
		private string _CI_SalesNo;
		
		private string _CI_Company;
		
		private string _CI_CardExplain;
		
		private bool _CI_IsLock;
		
		private string _CI_PaperNo;
		
		private string _CI_AccountNo;
		
		private string _CI_Password;
		
		private System.Nullable<decimal> _CI_Integral;
		
		private System.Data.Linq.Binary _CI_Photo;
		
		private string _CI_Station;
		
		private string _CI_ConsumeType;
		
		private System.Nullable<char> _CI_State;
		
		private System.Nullable<System.DateTime> _CI_CheckDate;
		
		private string _CI_CheckOperator;
		
		private bool _CI_SendFlag;
		
		private bool _CI_CheckFlag;
		
		private string _CI_CardAccountNo;
		
		private string _CI_EName;
		
		private System.Nullable<System.DateTime> _CI_Birthday;
		
		private string _CI_City;
		
		private string _CI_Professional;
		
		private string _CI_Religions;
		
		private string _CI_Special1;
		
		private System.Nullable<System.DateTime> _CI_SpecialDate1;
		
		private string _CI_VipNo;
		
		private string _CI_CardCode;
		
		private System.Nullable<int> _CI_CreditsUsed;
		
		private System.Nullable<System.DateTime> _birthday;
		
		private string _state;
		
		public string CI_CardNo
		{
			get
			{
				return this._CI_CardNo;
			}
			set
			{
				if ((this._CI_CardNo != value))
				{
					this._CI_CardNo = value;
				}
			}
		}
		
		public string CI_SystemICNo
		{
			get
			{
				return this._CI_SystemICNo;
			}
			set
			{
				if ((this._CI_SystemICNo != value))
				{
					this._CI_SystemICNo = value;
				}
			}
		}
		
		public System.Nullable<int> CI_CardTypeNo
		{
			get
			{
				return this._CI_CardTypeNo;
			}
			set
			{
				if ((this._CI_CardTypeNo != value))
				{
					this._CI_CardTypeNo = value;
				}
			}
		}
		
		public string CI_Name
		{
			get
			{
				return this._CI_Name;
			}
			set
			{
				if ((this._CI_Name != value))
				{
					this._CI_Name = value;
				}
			}
		}
		
		public string CI_Sexno
		{
			get
			{
				return this._CI_Sexno;
			}
			set
			{
				if ((this._CI_Sexno != value))
				{
					this._CI_Sexno = value;
				}
			}
		}
		
		public string CI_Address
		{
			get
			{
				return this._CI_Address;
			}
			set
			{
				if ((this._CI_Address != value))
				{
					this._CI_Address = value;
				}
			}
		}
		
		public string CI_Telephone
		{
			get
			{
				return this._CI_Telephone;
			}
			set
			{
				if ((this._CI_Telephone != value))
				{
					this._CI_Telephone = value;
				}
			}
		}
		
		public string CI_Remark
		{
			get
			{
				return this._CI_Remark;
			}
			set
			{
				if ((this._CI_Remark != value))
				{
					this._CI_Remark = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> CI_SendCardDate
		{
			get
			{
				return this._CI_SendCardDate;
			}
			set
			{
				if ((this._CI_SendCardDate != value))
				{
					this._CI_SendCardDate = value;
				}
			}
		}
		
		public string CI_SendCardOperator
		{
			get
			{
				return this._CI_SendCardOperator;
			}
			set
			{
				if ((this._CI_SendCardOperator != value))
				{
					this._CI_SendCardOperator = value;
				}
			}
		}
		
		public System.Nullable<decimal> CI_DiscountRate
		{
			get
			{
				return this._CI_DiscountRate;
			}
			set
			{
				if ((this._CI_DiscountRate != value))
				{
					this._CI_DiscountRate = value;
				}
			}
		}
		
		public System.Nullable<decimal> CI_DiscountRatepos
		{
			get
			{
				return this._CI_DiscountRatepos;
			}
			set
			{
				if ((this._CI_DiscountRatepos != value))
				{
					this._CI_DiscountRatepos = value;
				}
			}
		}
		
		public System.Nullable<decimal> CI_Limitation
		{
			get
			{
				return this._CI_Limitation;
			}
			set
			{
				if ((this._CI_Limitation != value))
				{
					this._CI_Limitation = value;
				}
			}
		}
		
		public System.Nullable<char> CI_CardStateNo
		{
			get
			{
				return this._CI_CardStateNo;
			}
			set
			{
				if ((this._CI_CardStateNo != value))
				{
					this._CI_CardStateNo = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> CI_UsefulLife
		{
			get
			{
				return this._CI_UsefulLife;
			}
			set
			{
				if ((this._CI_UsefulLife != value))
				{
					this._CI_UsefulLife = value;
				}
			}
		}
		
		public string CI_SalesNo
		{
			get
			{
				return this._CI_SalesNo;
			}
			set
			{
				if ((this._CI_SalesNo != value))
				{
					this._CI_SalesNo = value;
				}
			}
		}
		
		public string CI_Company
		{
			get
			{
				return this._CI_Company;
			}
			set
			{
				if ((this._CI_Company != value))
				{
					this._CI_Company = value;
				}
			}
		}
		
		public string CI_CardExplain
		{
			get
			{
				return this._CI_CardExplain;
			}
			set
			{
				if ((this._CI_CardExplain != value))
				{
					this._CI_CardExplain = value;
				}
			}
		}
		
		public bool CI_IsLock
		{
			get
			{
				return this._CI_IsLock;
			}
			set
			{
				if ((this._CI_IsLock != value))
				{
					this._CI_IsLock = value;
				}
			}
		}
		
		public string CI_PaperNo
		{
			get
			{
				return this._CI_PaperNo;
			}
			set
			{
				if ((this._CI_PaperNo != value))
				{
					this._CI_PaperNo = value;
				}
			}
		}
		
		public string CI_AccountNo
		{
			get
			{
				return this._CI_AccountNo;
			}
			set
			{
				if ((this._CI_AccountNo != value))
				{
					this._CI_AccountNo = value;
				}
			}
		}
		
		public string CI_Password
		{
			get
			{
				return this._CI_Password;
			}
			set
			{
				if ((this._CI_Password != value))
				{
					this._CI_Password = value;
				}
			}
		}
		
		public System.Nullable<decimal> CI_Integral
		{
			get
			{
				return this._CI_Integral;
			}
			set
			{
				if ((this._CI_Integral != value))
				{
					this._CI_Integral = value;
				}
			}
		}
		
		public System.Data.Linq.Binary CI_Photo
		{
			get
			{
				return this._CI_Photo;
			}
			set
			{
				if ((this._CI_Photo != value))
				{
					this._CI_Photo = value;
				}
			}
		}
		
		public string CI_Station
		{
			get
			{
				return this._CI_Station;
			}
			set
			{
				if ((this._CI_Station != value))
				{
					this._CI_Station = value;
				}
			}
		}
		
		public string CI_ConsumeType
		{
			get
			{
				return this._CI_ConsumeType;
			}
			set
			{
				if ((this._CI_ConsumeType != value))
				{
					this._CI_ConsumeType = value;
				}
			}
		}
		
		public System.Nullable<char> CI_State
		{
			get
			{
				return this._CI_State;
			}
			set
			{
				if ((this._CI_State != value))
				{
					this._CI_State = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> CI_CheckDate
		{
			get
			{
				return this._CI_CheckDate;
			}
			set
			{
				if ((this._CI_CheckDate != value))
				{
					this._CI_CheckDate = value;
				}
			}
		}
		
		public string CI_CheckOperator
		{
			get
			{
				return this._CI_CheckOperator;
			}
			set
			{
				if ((this._CI_CheckOperator != value))
				{
					this._CI_CheckOperator = value;
				}
			}
		}
		
		public bool CI_SendFlag
		{
			get
			{
				return this._CI_SendFlag;
			}
			set
			{
				if ((this._CI_SendFlag != value))
				{
					this._CI_SendFlag = value;
				}
			}
		}
		
		public bool CI_CheckFlag
		{
			get
			{
				return this._CI_CheckFlag;
			}
			set
			{
				if ((this._CI_CheckFlag != value))
				{
					this._CI_CheckFlag = value;
				}
			}
		}
		
		public string CI_CardAccountNo
		{
			get
			{
				return this._CI_CardAccountNo;
			}
			set
			{
				if ((this._CI_CardAccountNo != value))
				{
					this._CI_CardAccountNo = value;
				}
			}
		}
		
		public string CI_EName
		{
			get
			{
				return this._CI_EName;
			}
			set
			{
				if ((this._CI_EName != value))
				{
					this._CI_EName = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> CI_Birthday
		{
			get
			{
				return this._CI_Birthday;
			}
			set
			{
				if ((this._CI_Birthday != value))
				{
					this._CI_Birthday = value;
				}
			}
		}
		
		public string CI_City
		{
			get
			{
				return this._CI_City;
			}
			set
			{
				if ((this._CI_City != value))
				{
					this._CI_City = value;
				}
			}
		}
		
		public string CI_Professional
		{
			get
			{
				return this._CI_Professional;
			}
			set
			{
				if ((this._CI_Professional != value))
				{
					this._CI_Professional = value;
				}
			}
		}
		
		public string CI_Religions
		{
			get
			{
				return this._CI_Religions;
			}
			set
			{
				if ((this._CI_Religions != value))
				{
					this._CI_Religions = value;
				}
			}
		}
		
		public string CI_Special1
		{
			get
			{
				return this._CI_Special1;
			}
			set
			{
				if ((this._CI_Special1 != value))
				{
					this._CI_Special1 = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> CI_SpecialDate1
		{
			get
			{
				return this._CI_SpecialDate1;
			}
			set
			{
				if ((this._CI_SpecialDate1 != value))
				{
					this._CI_SpecialDate1 = value;
				}
			}
		}
		
		public string CI_VipNo
		{
			get
			{
				return this._CI_VipNo;
			}
			set
			{
				if ((this._CI_VipNo != value))
				{
					this._CI_VipNo = value;
				}
			}
		}
		
		public string CI_CardCode
		{
			get
			{
				return this._CI_CardCode;
			}
			set
			{
				if ((this._CI_CardCode != value))
				{
					this._CI_CardCode = value;
				}
			}
		}
		
		public System.Nullable<int> CI_CreditsUsed
		{
			get
			{
				return this._CI_CreditsUsed;
			}
			set
			{
				if ((this._CI_CreditsUsed != value))
				{
					this._CI_CreditsUsed = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> birthday
		{
			get
			{
				return this._birthday;
			}
			set
			{
				if ((this._birthday != value))
				{
					this._birthday = value;
				}
			}
		}
		
		public string state
		{
			get
			{
				return this._state;
			}
			set
			{
				if ((this._state != value))
				{
					this._state = value;
				}
			}
		}
    }

    #endregion

    #region 表CardPopSale

    public class CCardPopSale
    {
        private int _id;
		
		private System.Nullable<int> _mimMoney;
		
		private System.Nullable<int> _saleMoney;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public System.Nullable<int> mimMoney
		{
			get
			{
				return this._mimMoney;
			}
			set
			{
				if ((this._mimMoney != value))
				{
					this._mimMoney = value;
				}
			}
		}
		
		public System.Nullable<int> saleMoney
		{
			get
			{
				return this._saleMoney;
			}
			set
			{
				if ((this._saleMoney != value))
				{
					this._saleMoney = value;
				}
			}
		}
    }

    #endregion

    #region 表CardSale

    public class CCardSale
    {
        private int _id;
		
		private string _memberId;
		
		private System.Nullable<double> _balance;
		
		private System.Nullable<double> _cash;
		
		private System.Nullable<double> _bankUnion;
		
		private System.Nullable<System.DateTime> _payTime;
		
		private string _payEmployee;
		
		private string _note;
		
		private string _macAddress;
		
		private string _seat;
		
		private string _explain;
		
		private System.Nullable<double> _server;
		
		private string _serverEmployee;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string memberId
		{
			get
			{
				return this._memberId;
			}
			set
			{
				if ((this._memberId != value))
				{
					this._memberId = value;
				}
			}
		}
		
		public System.Nullable<double> balance
		{
			get
			{
				return this._balance;
			}
			set
			{
				if ((this._balance != value))
				{
					this._balance = value;
				}
			}
		}
		
		public System.Nullable<double> cash
		{
			get
			{
				return this._cash;
			}
			set
			{
				if ((this._cash != value))
				{
					this._cash = value;
				}
			}
		}
		
		public System.Nullable<double> bankUnion
		{
			get
			{
				return this._bankUnion;
			}
			set
			{
				if ((this._bankUnion != value))
				{
					this._bankUnion = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> payTime
		{
			get
			{
				return this._payTime;
			}
			set
			{
				if ((this._payTime != value))
				{
					this._payTime = value;
				}
			}
		}
		
		public string payEmployee
		{
			get
			{
				return this._payEmployee;
			}
			set
			{
				if ((this._payEmployee != value))
				{
					this._payEmployee = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string macAddress
		{
			get
			{
				return this._macAddress;
			}
			set
			{
				if ((this._macAddress != value))
				{
					this._macAddress = value;
				}
			}
		}
		
		public string seat
		{
			get
			{
				return this._seat;
			}
			set
			{
				if ((this._seat != value))
				{
					this._seat = value;
				}
			}
		}
		
		public string explain
		{
			get
			{
				return this._explain;
			}
			set
			{
				if ((this._explain != value))
				{
					this._explain = value;
				}
			}
		}
		
		public System.Nullable<double> server
		{
			get
			{
				return this._server;
			}
			set
			{
				if ((this._server != value))
				{
					this._server = value;
				}
			}
		}
		
		public string serverEmployee
		{
			get
			{
				return this._serverEmployee;
			}
			set
			{
				if ((this._serverEmployee != value))
				{
					this._serverEmployee = value;
				}
			}
		}
    }

    #endregion

    #region 表CashPrintTime

    public class CCashPrintTime
    {
        private int _id;
		
		private string _macAdd;
		
		private System.DateTime _time;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string macAdd
		{
			get
			{
				return this._macAdd;
			}
			set
			{
				if ((this._macAdd != value))
				{
					this._macAdd = value;
				}
			}
		}
		
		public System.DateTime time
		{
			get
			{
				return this._time;
			}
			set
			{
				if ((this._time != value))
				{
					this._time = value;
				}
			}
		}
    }

    #endregion

    #region 表Catgory

    public class CCatgory
    {
        private int _id;
		
		private string _name;
		
		private string _kitchPrinterName;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string kitchPrinterName
		{
			get
			{
				return this._kitchPrinterName;
			}
			set
			{
				if ((this._kitchPrinterName != value))
				{
					this._kitchPrinterName = value;
				}
			}
		}
    }

    #endregion

    #region 表ChainStore
    #endregion 

    #region 表ClearTable

    public class CClearTable
    {
        private int _id;
		
		private System.DateTime _clearTime;
		
		private System.Nullable<bool> _proceeded;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public System.DateTime clearTime
		{
			get
			{
				return this._clearTime;
			}
			set
			{
				if ((this._clearTime != value))
				{
					this._clearTime = value;
				}
			}
		}
		
		public System.Nullable<bool> proceeded
		{
			get
			{
				return this._proceeded;
			}
			set
			{
				if ((this._proceeded != value))
				{
					this._proceeded = value;
				}
			}
		}
    }

    #endregion

    #region 表Combo

    public class CCombo
    {
        private int _id;
		
		private double _originPrice;
		
		private string _priceType;
		
		private System.Nullable<double> _price;
		
		private System.Nullable<double> _freePrice;
		
		private System.Nullable<double> _expenseUpTo;
		
		private string _menuIds;
		
		private string _freeMenuIds;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public double originPrice
		{
			get
			{
				return this._originPrice;
			}
			set
			{
				if ((this._originPrice != value))
				{
					this._originPrice = value;
				}
			}
		}
		
		public string priceType
		{
			get
			{
				return this._priceType;
			}
			set
			{
				if ((this._priceType != value))
				{
					this._priceType = value;
				}
			}
		}
		
		public System.Nullable<double> price
		{
			get
			{
				return this._price;
			}
			set
			{
				if ((this._price != value))
				{
					this._price = value;
				}
			}
		}
		
		public System.Nullable<double> freePrice
		{
			get
			{
				return this._freePrice;
			}
			set
			{
				if ((this._freePrice != value))
				{
					this._freePrice = value;
				}
			}
		}
		
		public System.Nullable<double> expenseUpTo
		{
			get
			{
				return this._expenseUpTo;
			}
			set
			{
				if ((this._expenseUpTo != value))
				{
					this._expenseUpTo = value;
				}
			}
		}
		
		public string menuIds
		{
			get
			{
				return this._menuIds;
			}
			set
			{
				if ((this._menuIds != value))
				{
					this._menuIds = value;
				}
			}
		}
		
		public string freeMenuIds
		{
			get
			{
				return this._freeMenuIds;
			}
			set
			{
				if ((this._freeMenuIds != value))
				{
					this._freeMenuIds = value;
				}
			}
		}

        private List<int> disAssemble(string str)
        {
            List<int> menuIdList = new List<int>();

            if (str == null || str == "")
                return menuIdList;

            string[] ids = str.Split(';');
            foreach (string menuId in ids)
            {
                if (menuId == "")
                    continue;
                menuIdList.Add(Convert.ToInt32(menuId));
            }

            return menuIdList;
        }

        //拆分套餐
        public List<int> disAssemble_freeIds()
        {
            return disAssemble(this._freeMenuIds);
        }

        //拆分套餐
        public List<int> disAssemble_menuIds()
        {
            return disAssemble(this._menuIds);
        }

        public double get_combo_price(DAO dao)
        {
            double combo_price = 0;
            if (this._priceType == "免项目" || this._priceType == "消费满免项目")
            {
                var freeIds = disAssemble_freeIds();

                var pars = new List<string>();
                var vals = new List<string>();
                int count = freeIds.Count;
                for (int i = 0; i < count; i++)
                {
                    pars.Add("id");
                    vals.Add(freeIds[i].ToString());
                }
                var freeMenus = dao.get_Menus(pars, vals, "or");
                //var freeMenus = db.Menu.Where(x => freeIds.Contains(x.id));
                var freeMoney = freeMenus.Sum(x => x.price);
                combo_price = freeMoney;
            }
            else if (this._priceType == "减金额")
                combo_price = this._originPrice - this._price.Value;

            return combo_price;

        }
    }

    #endregion

    #region 表Customer

    public class CCustomer
    {
        private int _id;
		
		private string _name;
		
		private string _contact;
		
		private string _address;
		
		private string _phone;
		
		private string _mobile;
		
		private string _fax;
		
		private string _qq;
		
		private string _email;
		
		private System.Nullable<double> _money;
		
		private System.DateTime _registerDate;
		
		private string _mainBusiness;
		
		private string _note;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string contact
		{
			get
			{
				return this._contact;
			}
			set
			{
				if ((this._contact != value))
				{
					this._contact = value;
				}
			}
		}
		
		public string address
		{
			get
			{
				return this._address;
			}
			set
			{
				if ((this._address != value))
				{
					this._address = value;
				}
			}
		}
		
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		public string mobile
		{
			get
			{
				return this._mobile;
			}
			set
			{
				if ((this._mobile != value))
				{
					this._mobile = value;
				}
			}
		}
		
		public string fax
		{
			get
			{
				return this._fax;
			}
			set
			{
				if ((this._fax != value))
				{
					this._fax = value;
				}
			}
		}
		
		public string qq
		{
			get
			{
				return this._qq;
			}
			set
			{
				if ((this._qq != value))
				{
					this._qq = value;
				}
			}
		}
		
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this._email = value;
				}
			}
		}
		
		public System.Nullable<double> money
		{
			get
			{
				return this._money;
			}
			set
			{
				if ((this._money != value))
				{
					this._money = value;
				}
			}
		}
		
		public System.DateTime registerDate
		{
			get
			{
				return this._registerDate;
			}
			set
			{
				if ((this._registerDate != value))
				{
					this._registerDate = value;
				}
			}
		}
		
		public string mainBusiness
		{
			get
			{
				return this._mainBusiness;
			}
			set
			{
				if ((this._mainBusiness != value))
				{
					this._mainBusiness = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
    }

    #endregion

    #region 表Employee

    public class CEmployee
    {
        private string _id;
		
		private string _name;
		
		private string _cardId;
		
		private string _gender;
		
		private System.DateTime _birthday;
		
		private int _jobId;
		
		private bool _onDuty;
		
		private string _salary;
		
		private string _password;
		
		private string _phone;
		
		private string _address;
		
		private string _email;
		
		private System.DateTime _entryDate;
		
		private string _idCard;
		
		private string _note;
		
		private string _status;
		
		private System.Nullable<bool> _OrderClock;
		
		private System.Nullable<System.DateTime> _startTime;
		
		private System.Nullable<int> _serverTime;
		
		private string _techStatus;
		
		private string _room;
		
		private string _seat;
		
		public string id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string cardId
		{
			get
			{
				return this._cardId;
			}
			set
			{
				if ((this._cardId != value))
				{
					this._cardId = value;
				}
			}
		}
		
		public string gender
		{
			get
			{
				return this._gender;
			}
			set
			{
				if ((this._gender != value))
				{
					this._gender = value;
				}
			}
		}
		
		public System.DateTime birthday
		{
			get
			{
				return this._birthday;
			}
			set
			{
				if ((this._birthday != value))
				{
					this._birthday = value;
				}
			}
		}
		
		public int jobId
		{
			get
			{
				return this._jobId;
			}
			set
			{
				if ((this._jobId != value))
				{
					this._jobId = value;
				}
			}
		}
		
		public bool onDuty
		{
			get
			{
				return this._onDuty;
			}
			set
			{
				if ((this._onDuty != value))
				{
					this._onDuty = value;
				}
			}
		}
		
		public string salary
		{
			get
			{
				return this._salary;
			}
			set
			{
				if ((this._salary != value))
				{
					this._salary = value;
				}
			}
		}
		
		public string password
		{
			get
			{
				return this._password;
			}
			set
			{
				if ((this._password != value))
				{
					this._password = value;
				}
			}
		}
		
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		public string address
		{
			get
			{
				return this._address;
			}
			set
			{
				if ((this._address != value))
				{
					this._address = value;
				}
			}
		}
		
		public string email
		{
			get
			{
				return this._email;
			}
			set
			{
				if ((this._email != value))
				{
					this._email = value;
				}
			}
		}
		
		public System.DateTime entryDate
		{
			get
			{
				return this._entryDate;
			}
			set
			{
				if ((this._entryDate != value))
				{
					this._entryDate = value;
				}
			}
		}
		
		public string idCard
		{
			get
			{
				return this._idCard;
			}
			set
			{
				if ((this._idCard != value))
				{
					this._idCard = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this._status = value;
				}
			}
		}
		
		public System.Nullable<bool> OrderClock
		{
			get
			{
				return this._OrderClock;
			}
			set
			{
				if ((this._OrderClock != value))
				{
					this._OrderClock = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				if ((this._startTime != value))
				{
					this._startTime = value;
				}
			}
		}
		
		public System.Nullable<int> serverTime
		{
			get
			{
				return this._serverTime;
			}
			set
			{
				if ((this._serverTime != value))
				{
					this._serverTime = value;
				}
			}
		}
		
		public string techStatus
		{
			get
			{
				return this._techStatus;
			}
			set
			{
				if ((this._techStatus != value))
				{
					this._techStatus = value;
				}
			}
		}
		
		public string room
		{
			get
			{
				return this._room;
			}
			set
			{
				if ((this._room != value))
				{
					this._room = value;
				}
			}
		}
		
		public string seat
		{
			get
			{
				return this._seat;
			}
			set
			{
				if ((this._seat != value))
				{
					this._seat = value;
				}
			}
		}
    }

    #endregion

    #region 表HisOrders

    public class CHisOrders
    {
        private int _id;
		
		private string _menu;
		
		private string _text;
		
		private string _systemId;
		
		private double _number;
		
		private string _priceType;
		
		private double _money;
		
		private string _technician;
		
		private string _techType;
		
		private System.Nullable<System.DateTime> _startTime;
		
		private System.DateTime _inputTime;
		
		private string _inputEmployee;
		
		private string _deleteEmployee;
		
		private string _donorEmployee;
		
		private System.Nullable<int> _comboId;
		
		private bool _paid;
		
		private System.Nullable<int> _accountId;
		
		private string _billId;
		
		private System.Nullable<int> _departmentId;

        private string _deleteExplain;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				if ((this._menu != value))
				{
					this._menu = value;
				}
			}
		}
		
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
		
		public double number
		{
			get
			{
				return this._number;
			}
			set
			{
				if ((this._number != value))
				{
					this._number = value;
				}
			}
		}
		
		public string priceType
		{
			get
			{
				return this._priceType;
			}
			set
			{
				if ((this._priceType != value))
				{
					this._priceType = value;
				}
			}
		}
		
		public double money
		{
			get
			{
				return this._money;
			}
			set
			{
				if ((this._money != value))
				{
					this._money = value;
				}
			}
		}
		
		public string technician
		{
			get
			{
				return this._technician;
			}
			set
			{
				if ((this._technician != value))
				{
					this._technician = value;
				}
			}
		}
		
		public string techType
		{
			get
			{
				return this._techType;
			}
			set
			{
				if ((this._techType != value))
				{
					this._techType = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				if ((this._startTime != value))
				{
					this._startTime = value;
				}
			}
		}
		
		public System.DateTime inputTime
		{
			get
			{
				return this._inputTime;
			}
			set
			{
				if ((this._inputTime != value))
				{
					this._inputTime = value;
				}
			}
		}
		
		public string inputEmployee
		{
			get
			{
				return this._inputEmployee;
			}
			set
			{
				if ((this._inputEmployee != value))
				{
					this._inputEmployee = value;
				}
			}
		}
		
		public string deleteEmployee
		{
			get
			{
				return this._deleteEmployee;
			}
			set
			{
				if ((this._deleteEmployee != value))
				{
					this._deleteEmployee = value;
				}
			}
		}
		
		public string donorEmployee
		{
			get
			{
				return this._donorEmployee;
			}
			set
			{
				if ((this._donorEmployee != value))
				{
					this._donorEmployee = value;
				}
			}
		}
		
		public System.Nullable<int> comboId
		{
			get
			{
				return this._comboId;
			}
			set
			{
				if ((this._comboId != value))
				{
					this._comboId = value;
				}
			}
		}
		
		public bool paid
		{
			get
			{
				return this._paid;
			}
			set
			{
				if ((this._paid != value))
				{
					this._paid = value;
				}
			}
		}
		
		public System.Nullable<int> accountId
		{
			get
			{
				return this._accountId;
			}
			set
			{
				if ((this._accountId != value))
				{
					this._accountId = value;
				}
			}
		}
		
		public string billId
		{
			get
			{
				return this._billId;
			}
			set
			{
				if ((this._billId != value))
				{
					this._billId = value;
				}
			}
		}
		
		public System.Nullable<int> departmentId
		{
			get
			{
				return this._departmentId;
			}
			set
			{
				if ((this._departmentId != value))
				{
					this._departmentId = value;
				}
			}
		}

        public string deleteExplain
        {
            get
            {
                return this._deleteExplain;
            }
            set
            {
                if ((this._deleteExplain != value))
                {
                    this._deleteExplain = value;
                }
            }
        }
    }

    #endregion

    #region 表Job

    public class CJob
    {
        private int _id;
		
		private string _name;
		
		private string _note;
		
		private string _ip;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string ip
		{
			get
			{
				return this._ip;
			}
			set
			{
				if ((this._ip != value))
				{
					this._ip = value;
				}
			}
		}
    }

    #endregion

    #region 表MemberSetting

    public class CMemberSetting
    {
        private int _id;
		
		private System.Nullable<int> _money;
		
		private string _cardType;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public System.Nullable<int> money
		{
			get
			{
				return this._money;
			}
			set
			{
				if ((this._money != value))
				{
					this._money = value;
				}
			}
		}
		
		public string cardType
		{
			get
			{
				return this._cardType;
			}
			set
			{
				if ((this._cardType != value))
				{
					this._cardType = value;
				}
			}
		}
    }

    #endregion

    #region 表MemberType

    public class CMemberType
    {
        private int _id;
		
		private string _name;
		
		private string _timSpan;
		
		private System.Nullable<int> _times;
		
		private System.Nullable<double> _money;
		
		private System.Nullable<double> _maxOpenMoney;
		
		private System.Nullable<System.DateTime> _expireDate;
		
		private System.Nullable<int> _offerId;
		
		private bool _credits;
		
		private System.Nullable<bool> _smsAfterUsing;

        private System.Nullable<bool> _userOneTimeOneDay;

        private System.Nullable<bool> _LimitedTimesPerMonth;

        private System.Nullable<int> _TimesPerMonth;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string timSpan
		{
			get
			{
				return this._timSpan;
			}
			set
			{
				if ((this._timSpan != value))
				{
					this._timSpan = value;
				}
			}
		}
		
		public System.Nullable<int> times
		{
			get
			{
				return this._times;
			}
			set
			{
				if ((this._times != value))
				{
					this._times = value;
				}
			}
		}
		
		public System.Nullable<double> money
		{
			get
			{
				return this._money;
			}
			set
			{
				if ((this._money != value))
				{
					this._money = value;
				}
			}
		}
		
		public System.Nullable<double> maxOpenMoney
		{
			get
			{
				return this._maxOpenMoney;
			}
			set
			{
				if ((this._maxOpenMoney != value))
				{
					this._maxOpenMoney = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> expireDate
		{
			get
			{
				return this._expireDate;
			}
			set
			{
				if ((this._expireDate != value))
				{
					this._expireDate = value;
				}
			}
		}
		
		public System.Nullable<int> offerId
		{
			get
			{
				return this._offerId;
			}
			set
			{
				if ((this._offerId != value))
				{
					this._offerId = value;
				}
			}
		}
		
		public bool credits
		{
			get
			{
				return this._credits;
			}
			set
			{
				if ((this._credits != value))
				{
					this._credits = value;
				}
			}
		}
		
		public System.Nullable<bool> smsAfterUsing
		{
			get
			{
				return this._smsAfterUsing;
			}
			set
			{
				if ((this._smsAfterUsing != value))
				{
					this._smsAfterUsing = value;
				}
			}
		}

        public System.Nullable<bool> userOneTimeOneDay
        {
            get
            {
                return this._userOneTimeOneDay;
            }
            set
            {
                if ((this._userOneTimeOneDay != value))
                {
                    this._userOneTimeOneDay = value;
                }
            }
        }

        public System.Nullable<bool> LimitedTimesPerMonth
        {
            get
            {
                return this._LimitedTimesPerMonth;
            }
            set
            {
                if ((this._LimitedTimesPerMonth != value))
                {
                    this._LimitedTimesPerMonth = value;
                }
            }
        }

        public System.Nullable<int> TimesPerMonth
        {
            get
            {
                return this._TimesPerMonth;
            }
            set
            {
                if ((this._TimesPerMonth != value))
                {
                    this._TimesPerMonth = value;
                }
            }
        }
    }


    #endregion

    #region 表Menu

    public class CMenu
    {
        private int _id;
		
		private string _name;
		
		private int _catgoryId;
		
		private string _unit;
		
		private double _price;
		
		private bool _technician;
		
		private string _techRatioType;
		
		private System.Nullable<double> _onRatio;
		
		private System.Nullable<double> _orderRatio;
		
		private System.Nullable<int> _timeLimitHour;
		
		private System.Nullable<int> _timeLimitMiniute;
		
		private bool _addAutomatic;
		
		private string _addType;
		
		private System.Nullable<double> _addMoney;
		
		private string _note;
		
		private string _timeLimitType;
		
		private System.Nullable<bool> _waiter;
		
		private System.Nullable<double> _waiterRatio;
		
		private System.Nullable<int> _waiterRatioType;
		
		private string _techRatioCat;

        private string _ResourceExpense;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public int catgoryId
		{
			get
			{
				return this._catgoryId;
			}
			set
			{
				if ((this._catgoryId != value))
				{
					this._catgoryId = value;
				}
			}
		}
		
		public string unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if ((this._unit != value))
				{
					this._unit = value;
				}
			}
		}
		
		public double price
		{
			get
			{
				return this._price;
			}
			set
			{
				if ((this._price != value))
				{
					this._price = value;
				}
			}
		}
		
		public bool technician
		{
			get
			{
				return this._technician;
			}
			set
			{
				if ((this._technician != value))
				{
					this._technician = value;
				}
			}
		}
		
		public string techRatioType
		{
			get
			{
				return this._techRatioType;
			}
			set
			{
				if ((this._techRatioType != value))
				{
					this._techRatioType = value;
				}
			}
		}
		
		public System.Nullable<double> onRatio
		{
			get
			{
				return this._onRatio;
			}
			set
			{
				if ((this._onRatio != value))
				{
					this._onRatio = value;
				}
			}
		}
		
		public System.Nullable<double> orderRatio
		{
			get
			{
				return this._orderRatio;
			}
			set
			{
				if ((this._orderRatio != value))
				{
					this._orderRatio = value;
				}
			}
		}
		
		public System.Nullable<int> timeLimitHour
		{
			get
			{
				return this._timeLimitHour;
			}
			set
			{
				if ((this._timeLimitHour != value))
				{
					this._timeLimitHour = value;
				}
			}
		}
		
		public System.Nullable<int> timeLimitMiniute
		{
			get
			{
				return this._timeLimitMiniute;
			}
			set
			{
				if ((this._timeLimitMiniute != value))
				{
					this._timeLimitMiniute = value;
				}
			}
		}
		
		public bool addAutomatic
		{
			get
			{
				return this._addAutomatic;
			}
			set
			{
				if ((this._addAutomatic != value))
				{
					this._addAutomatic = value;
				}
			}
		}
		
		public string addType
		{
			get
			{
				return this._addType;
			}
			set
			{
				if ((this._addType != value))
				{
					this._addType = value;
				}
			}
		}
		
		public System.Nullable<double> addMoney
		{
			get
			{
				return this._addMoney;
			}
			set
			{
				if ((this._addMoney != value))
				{
					this._addMoney = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string timeLimitType
		{
			get
			{
				return this._timeLimitType;
			}
			set
			{
				if ((this._timeLimitType != value))
				{
					this._timeLimitType = value;
				}
			}
		}
		
		public System.Nullable<bool> waiter
		{
			get
			{
				return this._waiter;
			}
			set
			{
				if ((this._waiter != value))
				{
					this._waiter = value;
				}
			}
		}
		
		public System.Nullable<double> waiterRatio
		{
			get
			{
				return this._waiterRatio;
			}
			set
			{
				if ((this._waiterRatio != value))
				{
					this._waiterRatio = value;
				}
			}
		}
		
		public System.Nullable<int> waiterRatioType
		{
			get
			{
				return this._waiterRatioType;
			}
			set
			{
				if ((this._waiterRatioType != value))
				{
					this._waiterRatioType = value;
				}
			}
		}
		
		public string techRatioCat
		{
			get
			{
				return this._techRatioCat;
			}
			set
			{
				if ((this._techRatioCat != value))
				{
					this._techRatioCat = value;
				}
			}
		}

        public string ResourceExpense
        {
            get
            {
                return this._ResourceExpense;
            }
            set
            {
                if ((this._ResourceExpense != value))
                {
                    this._ResourceExpense = value;
                }
            }
        }

        /// <summary>
        /// 拆分项目的资源消耗
        /// </summary>
        /// <param name="resourceExpense">项目的资源消耗字段</param>
        /// <returns></returns>
        public Dictionary<string, double> disAssemble_Menu_resourceExpense()
        {
            Dictionary<string, double> menuIdList = new Dictionary<string, double>();

            if (_ResourceExpense == null)
                return menuIdList;

            string[] menuIds = _ResourceExpense.Split('$');
            foreach (string menuId in menuIds)
            {
                if (menuId == "")
                    continue;

                bool trans = true;
                string[] tps = menuId.Split('=');
                double expense = -1;
                try
                {
                    expense = Convert.ToDouble(tps[1]);
                }
                catch
                {
                    trans = false;
                }
                if (!trans) continue;
                menuIdList.Add(tps[0], expense);
            }

            return menuIdList;
        }

        /// <summary>
        /// 合并项目的资源消耗
        /// </summary>
        /// <param name="resourceExpense">项目的资源消耗字段</param>
        /// <returns></returns>
        public string assemble_Menu_resourceExpense(Dictionary<string, double> resourceExpense)
        {
            StringBuilder sb = new StringBuilder();
            if (resourceExpense == null)
                return "";

            foreach (KeyValuePair<string, double> a in resourceExpense)
            {
                sb.Append(a.Key).Append("=").Append(a.Value.ToString()).Append("$");
            }

            return sb.ToString();
        }
    }

    #endregion

    #region 表Operation

    public class COperation
    {
        private int _id;
		
		private string _seat;
		
		private string _openEmployee;
		
		private System.Nullable<System.DateTime> _openTime;
		
		private string _employee;
		
		private string _explain;
		
		private System.Nullable<System.DateTime> _opTime;
		
		private string _note1;
		
		private string _note2;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string seat
		{
			get
			{
				return this._seat;
			}
			set
			{
				if ((this._seat != value))
				{
					this._seat = value;
				}
			}
		}
		
		public string openEmployee
		{
			get
			{
				return this._openEmployee;
			}
			set
			{
				if ((this._openEmployee != value))
				{
					this._openEmployee = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> openTime
		{
			get
			{
				return this._openTime;
			}
			set
			{
				if ((this._openTime != value))
				{
					this._openTime = value;
				}
			}
		}
		
		public string employee
		{
			get
			{
				return this._employee;
			}
			set
			{
				if ((this._employee != value))
				{
					this._employee = value;
				}
			}
		}
		
		public string explain
		{
			get
			{
				return this._explain;
			}
			set
			{
				if ((this._explain != value))
				{
					this._explain = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> opTime
		{
			get
			{
				return this._opTime;
			}
			set
			{
				if ((this._opTime != value))
				{
					this._opTime = value;
				}
			}
		}
		
		public string note1
		{
			get
			{
				return this._note1;
			}
			set
			{
				if ((this._note1 != value))
				{
					this._note1 = value;
				}
			}
		}
		
		public string note2
		{
			get
			{
				return this._note2;
			}
			set
			{
				if ((this._note2 != value))
				{
					this._note2 = value;
				}
			}
		}
    }


    #endregion

    #region 表 Options

    public class COptions
    {

        private int _id;

        private string _companyName;

        private string _companyCode;

        private string _companyPhone;

        private string _companyAddress;

        private System.Nullable<int> _取消开牌时限;

        private System.Nullable<int> _取消开房时限;

        private System.Nullable<int> _删除支出时限;

        private System.Nullable<int> _退钟时限;

        private System.Nullable<int> _技师条数;

        private System.Nullable<bool> _启用鞋部;

        private System.Nullable<int> _鞋部条数;

        private System.Nullable<bool> _启用会员卡密码;

        private System.Nullable<bool> _启用结账监控;

        private string _结账视频长度;

        private System.Nullable<bool> _启用手牌锁;

        private System.Nullable<int> _开业时间;

        private System.Nullable<bool> _启用客房面板;

        private System.Nullable<int> _包房等待时限;

        private System.Nullable<int> _下钟提醒;

        private System.Nullable<bool> _启用ID手牌锁;

        private System.Nullable<bool> _允许手工输入手牌号开牌;

        private System.Nullable<bool> _允许手工输入手牌号结账;

        private System.Nullable<bool> _录单输入单据编号;

        private System.Nullable<bool> _结账未打单锁定手牌;

        private System.Nullable<int> _营业报表格式;

        private System.Nullable<bool> _结账打印结账单;

        private System.Nullable<bool> _结账打印存根单;

        private System.Nullable<bool> _结账打印取鞋小票;

        private System.Nullable<int> _抹零限制;

        private string _手牌锁类型;

        private System.Nullable<bool> _自动加收过夜费;

        private string _过夜费起点;

        private string _过夜费终点;

        private System.Nullable<bool> _启用分单结账;

        private System.Nullable<bool> _启用员工服务卡;

        private System.Nullable<bool> _台位类型分页显示;

        private System.Nullable<int> _提成报表格式;

        private System.Nullable<bool> _自动感应手牌;

        private System.Nullable<bool> _录单区分点钟轮钟;

        private System.Nullable<bool> _打印技师派遣单;

        private string _会员卡密码类型;

        private string _company_Code;

        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                if ((this._id != value))
                {
                    this._id = value;
                }
            }
        }

        public string companyName
        {
            get
            {
                return this._companyName;
            }
            set
            {
                if ((this._companyName != value))
                {
                    this._companyName = value;
                }
            }
        }

        public string companyCode
        {
            get
            {
                return this._companyCode;
            }
            set
            {
                if ((this._companyCode != value))
                {
                    this._companyCode = value;
                }
            }
        }

        public string companyPhone
        {
            get
            {
                return this._companyPhone;
            }
            set
            {
                if ((this._companyPhone != value))
                {
                    this._companyPhone = value;
                }
            }
        }

        public string companyAddress
        {
            get
            {
                return this._companyAddress;
            }
            set
            {
                if ((this._companyAddress != value))
                {
                    this._companyAddress = value;
                }
            }
        }

        public System.Nullable<int> 取消开牌时限
        {
            get
            {
                return this._取消开牌时限;
            }
            set
            {
                if ((this._取消开牌时限 != value))
                {
                    this._取消开牌时限 = value;
                }
            }
        }

        public System.Nullable<int> 取消开房时限
        {
            get
            {
                return this._取消开房时限;
            }
            set
            {
                if ((this._取消开房时限 != value))
                {
                    this._取消开房时限 = value;
                }
            }
        }

        public System.Nullable<int> 删除支出时限
        {
            get
            {
                return this._删除支出时限;
            }
            set
            {
                if ((this._删除支出时限 != value))
                {
                    this._删除支出时限 = value;
                }
            }
        }

        public System.Nullable<int> 退钟时限
        {
            get
            {
                return this._退钟时限;
            }
            set
            {
                if ((this._退钟时限 != value))
                {
                    this._退钟时限 = value;
                }
            }
        }

        public System.Nullable<int> 技师条数
        {
            get
            {
                return this._技师条数;
            }
            set
            {
                if ((this._技师条数 != value))
                {
                    this._技师条数 = value;
                }
            }
        }

        public System.Nullable<bool> 启用鞋部
        {
            get
            {
                return this._启用鞋部;
            }
            set
            {
                if ((this._启用鞋部 != value))
                {
                    this._启用鞋部 = value;
                }
            }
        }

        public System.Nullable<int> 鞋部条数
        {
            get
            {
                return this._鞋部条数;
            }
            set
            {
                if ((this._鞋部条数 != value))
                {
                    this._鞋部条数 = value;
                }
            }
        }

        public System.Nullable<bool> 启用会员卡密码
        {
            get
            {
                return this._启用会员卡密码;
            }
            set
            {
                if ((this._启用会员卡密码 != value))
                {
                    this._启用会员卡密码 = value;
                }
            }
        }

        public System.Nullable<bool> 启用结账监控
        {
            get
            {
                return this._启用结账监控;
            }
            set
            {
                if ((this._启用结账监控 != value))
                {
                    this._启用结账监控 = value;
                }
            }
        }

        public string 结账视频长度
        {
            get
            {
                return this._结账视频长度;
            }
            set
            {
                if ((this._结账视频长度 != value))
                {
                    this._结账视频长度 = value;
                }
            }
        }

        public System.Nullable<bool> 启用手牌锁
        {
            get
            {
                return this._启用手牌锁;
            }
            set
            {
                if ((this._启用手牌锁 != value))
                {
                    this._启用手牌锁 = value;
                }
            }
        }

        public System.Nullable<int> 开业时间
        {
            get
            {
                return this._开业时间;
            }
            set
            {
                if ((this._开业时间 != value))
                {
                    this._开业时间 = value;
                }
            }
        }

        public System.Nullable<bool> 启用客房面板
        {
            get
            {
                return this._启用客房面板;
            }
            set
            {
                if ((this._启用客房面板 != value))
                {
                    this._启用客房面板 = value;
                }
            }
        }

        public System.Nullable<int> 包房等待时限
        {
            get
            {
                return this._包房等待时限;
            }
            set
            {
                if ((this._包房等待时限 != value))
                {
                    this._包房等待时限 = value;
                }
            }
        }

        public System.Nullable<int> 下钟提醒
        {
            get
            {
                return this._下钟提醒;
            }
            set
            {
                if ((this._下钟提醒 != value))
                {
                    this._下钟提醒 = value;
                }
            }
        }

        public System.Nullable<bool> 启用ID手牌锁
        {
            get
            {
                return this._启用ID手牌锁;
            }
            set
            {
                if ((this._启用ID手牌锁 != value))
                {
                    this._启用ID手牌锁 = value;
                }
            }
        }

        public System.Nullable<bool> 允许手工输入手牌号开牌
        {
            get
            {
                return this._允许手工输入手牌号开牌;
            }
            set
            {
                if ((this._允许手工输入手牌号开牌 != value))
                {
                    this._允许手工输入手牌号开牌 = value;
                }
            }
        }

        public System.Nullable<bool> 允许手工输入手牌号结账
        {
            get
            {
                return this._允许手工输入手牌号结账;
            }
            set
            {
                if ((this._允许手工输入手牌号结账 != value))
                {
                    this._允许手工输入手牌号结账 = value;
                }
            }
        }

        public System.Nullable<bool> 录单输入单据编号
        {
            get
            {
                return this._录单输入单据编号;
            }
            set
            {
                if ((this._录单输入单据编号 != value))
                {
                    this._录单输入单据编号 = value;
                }
            }
        }

        public System.Nullable<bool> 结账未打单锁定手牌
        {
            get
            {
                return this._结账未打单锁定手牌;
            }
            set
            {
                if ((this._结账未打单锁定手牌 != value))
                {
                    this._结账未打单锁定手牌 = value;
                }
            }
        }

        public System.Nullable<int> 营业报表格式
        {
            get
            {
                return this._营业报表格式;
            }
            set
            {
                if ((this._营业报表格式 != value))
                {
                    this._营业报表格式 = value;
                }
            }
        }

        public System.Nullable<bool> 结账打印结账单
        {
            get
            {
                return this._结账打印结账单;
            }
            set
            {
                if ((this._结账打印结账单 != value))
                {
                    this._结账打印结账单 = value;
                }
            }
        }

        public System.Nullable<bool> 结账打印存根单
        {
            get
            {
                return this._结账打印存根单;
            }
            set
            {
                if ((this._结账打印存根单 != value))
                {
                    this._结账打印存根单 = value;
                }
            }
        }

        public System.Nullable<bool> 结账打印取鞋小票
        {
            get
            {
                return this._结账打印取鞋小票;
            }
            set
            {
                if ((this._结账打印取鞋小票 != value))
                {
                    this._结账打印取鞋小票 = value;
                }
            }
        }

        public System.Nullable<int> 抹零限制
        {
            get
            {
                return this._抹零限制;
            }
            set
            {
                if ((this._抹零限制 != value))
                {
                    this._抹零限制 = value;
                }
            }
        }

        public string 手牌锁类型
        {
            get
            {
                return this._手牌锁类型;
            }
            set
            {
                if ((this._手牌锁类型 != value))
                {
                    this._手牌锁类型 = value;
                }
            }
        }

        public System.Nullable<bool> 自动加收过夜费
        {
            get
            {
                return this._自动加收过夜费;
            }
            set
            {
                if ((this._自动加收过夜费 != value))
                {
                    this._自动加收过夜费 = value;
                }
            }
        }

        public string 过夜费起点
        {
            get
            {
                return this._过夜费起点;
            }
            set
            {
                if ((this._过夜费起点 != value))
                {
                    this._过夜费起点 = value;
                }
            }
        }

        public string 过夜费终点
        {
            get
            {
                return this._过夜费终点;
            }
            set
            {
                if ((this._过夜费终点 != value))
                {
                    this._过夜费终点 = value;
                }
            }
        }

        public System.Nullable<bool> 启用分单结账
        {
            get
            {
                return this._启用分单结账;
            }
            set
            {
                if ((this._启用分单结账 != value))
                {
                    this._启用分单结账 = value;
                }
            }
        }

        public System.Nullable<bool> 启用员工服务卡
        {
            get
            {
                return this._启用员工服务卡;
            }
            set
            {
                if ((this._启用员工服务卡 != value))
                {
                    this._启用员工服务卡 = value;
                }
            }
        }

        public System.Nullable<bool> 台位类型分页显示
        {
            get
            {
                return this._台位类型分页显示;
            }
            set
            {
                if ((this._台位类型分页显示 != value))
                {
                    this._台位类型分页显示 = value;
                }
            }
        }

        public System.Nullable<int> 提成报表格式
        {
            get
            {
                return this._提成报表格式;
            }
            set
            {
                if ((this._提成报表格式 != value))
                {
                    this._提成报表格式 = value;
                }
            }
        }

        public System.Nullable<bool> 自动感应手牌
        {
            get
            {
                return this._自动感应手牌;
            }
            set
            {
                if ((this._自动感应手牌 != value))
                {
                    this._自动感应手牌 = value;
                }
            }
        }

        public System.Nullable<bool> 录单区分点钟轮钟
        {
            get
            {
                return this._录单区分点钟轮钟;
            }
            set
            {
                if ((this._录单区分点钟轮钟 != value))
                {
                    this._录单区分点钟轮钟 = value;
                }
            }
        }

        public System.Nullable<bool> 打印技师派遣单
        {
            get
            {
                return this._打印技师派遣单;
            }
            set
            {
                if ((this._打印技师派遣单 != value))
                {
                    this._打印技师派遣单 = value;
                }
            }
        }

        public string 会员卡密码类型
        {
            get
            {
                return this._会员卡密码类型;
            }
            set
            {
                if ((this._会员卡密码类型 != value))
                {
                    this._会员卡密码类型 = value;
                }
            }
        }

        public string company_Code
        {
            get
            {
                return this._company_Code;
            }
            set
            {
                if ((this._company_Code != value))
                {
                    this._company_Code = value;
                }
            }
        }

    }

    #endregion

    #region 表 Orders

    public class COrders
    {
        private int _id;
		
		private string _menu;
		
		private string _text;
		
		private string _systemId;
		
		private double _number;
		
		private string _priceType;
		
		private double _money;
		
		private string _technician;
		
		private string _techType;
		
		private System.Nullable<System.DateTime> _startTime;
		
		private System.DateTime _inputTime;
		
		private string _inputEmployee;
		
		private string _deleteEmployee;
		
		private string _donorEmployee;

        private string _donorExplain;

        private System.Nullable<System.DateTime> _donorTime;
		
		private System.Nullable<int> _comboId;
		
		private bool _paid;
		
		private System.Nullable<int> _accountId;
		
		private string _billId;
		
		private System.Nullable<bool> _stopTiming;
		
		private System.Nullable<int> _departmentId;

        private string _deleteExplain;

        public System.Nullable<System.DateTime> donorTime
        {
            get
            {
                return this._donorTime;
            }
            set
            {
                if ((this._donorTime != value))
                {
                    this._donorTime = value;
                }
            }
        }

        public string donorExplain
        {
            get
            {
                return this._donorExplain;
            }
            set
            {
                if ((this._donorExplain != value))
                {
                    this._donorExplain = value;
                }
            }
        }

		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				if ((this._menu != value))
				{
					this._menu = value;
				}
			}
		}
		
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
		
		public double number
		{
			get
			{
				return this._number;
			}
			set
			{
				if ((this._number != value))
				{
					this._number = value;
				}
			}
		}
		
		public string priceType
		{
			get
			{
				return this._priceType;
			}
			set
			{
				if ((this._priceType != value))
				{
					this._priceType = value;
				}
			}
		}
		
		public double money
		{
			get
			{
				return this._money;
			}
			set
			{
				if ((this._money != value))
				{
					this._money = value;
				}
			}
		}
		
		public string technician
		{
			get
			{
				return this._technician;
			}
			set
			{
				if ((this._technician != value))
				{
					this._technician = value;
				}
			}
		}
		
		public string techType
		{
			get
			{
				return this._techType;
			}
			set
			{
				if ((this._techType != value))
				{
					this._techType = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				if ((this._startTime != value))
				{
					this._startTime = value;
				}
			}
		}
		
		public System.DateTime inputTime
		{
			get
			{
				return this._inputTime;
			}
			set
			{
				if ((this._inputTime != value))
				{
					this._inputTime = value;
				}
			}
		}
		
		public string inputEmployee
		{
			get
			{
				return this._inputEmployee;
			}
			set
			{
				if ((this._inputEmployee != value))
				{
					this._inputEmployee = value;
				}
			}
		}
		
		public string deleteEmployee
		{
			get
			{
				return this._deleteEmployee;
			}
			set
			{
				if ((this._deleteEmployee != value))
				{
					this._deleteEmployee = value;
				}
			}
		}
		
		public string donorEmployee
		{
			get
			{
				return this._donorEmployee;
			}
			set
			{
				if ((this._donorEmployee != value))
				{
					this._donorEmployee = value;
				}
			}
		}
		
		public System.Nullable<int> comboId
		{
			get
			{
				return this._comboId;
			}
			set
			{
				if ((this._comboId != value))
				{
					this._comboId = value;
				}
			}
		}
		
		public bool paid
		{
			get
			{
				return this._paid;
			}
			set
			{
				if ((this._paid != value))
				{
					this._paid = value;
				}
			}
		}
		
		public System.Nullable<int> accountId
		{
			get
			{
				return this._accountId;
			}
			set
			{
				if ((this._accountId != value))
				{
					this._accountId = value;
				}
			}
		}
		
		public string billId
		{
			get
			{
				return this._billId;
			}
			set
			{
				if ((this._billId != value))
				{
					this._billId = value;
				}
			}
		}
		
		public System.Nullable<bool> stopTiming
		{
			get
			{
				return this._stopTiming;
			}
			set
			{
				if ((this._stopTiming != value))
				{
					this._stopTiming = value;
				}
			}
		}
		
		public System.Nullable<int> departmentId
		{
			get
			{
				return this._departmentId;
			}
			set
			{
				if ((this._departmentId != value))
				{
					this._departmentId = value;
				}
			}
		}

        public string deleteExplain
        {
            get
            {
                return this._deleteExplain;
            }
            set
            {
                if ((this._deleteExplain != value))
                {
                    this._deleteExplain = value;
                }
            }
        }
    }

    #endregion 

    #region 表PayMsg

    public class CPayMsg
    {
        private int _id;
		
		private string _systemId;
		
		private string _ip;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
		
		public string ip
		{
			get
			{
				return this._ip;
			}
			set
			{
				if ((this._ip != value))
				{
					this._ip = value;
				}
			}
		}
    }

    #endregion

    #region 表Promotion

    public class CPromotion
    {
        private int _id;
		
		private string _name;
		
		private bool _status;
		
		private string _menuIds;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public bool status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this._status = value;
				}
			}
		}
		
		public string menuIds
		{
			get
			{
				return this._menuIds;
			}
			set
			{
				if ((this._menuIds != value))
				{
					this._menuIds = value;
				}
			}
		}

        //拆分优惠方案
        public Dictionary<string, string> disAssemble()
        {
            Dictionary<string, string> menuIdList = new Dictionary<string, string>();

            if (this._menuIds == null)
                return menuIdList;

            string[] menuIds = this._menuIds.Split(';');
            foreach (string menuId in menuIds)
            {
                if (menuId == "")
                    continue;

                string[] tps = menuId.Split('=');
                menuIdList.Add(tps[0], tps[1]);
            }

            return menuIdList;
        }

    }

    #endregion

    #region 表Room

    public class CRoom
    {
        private int _id;
		
		private string _name;
		
		private int _population;
		
		private string _openTime;
		
		private string _seat;
		
		private string _systemId;
		
		private string _orderTime;
		
		private string _menu;
		
		private string _orderTechId;
		
		private string _techId;
		
		private string _startTime;
		
		private string _serverTime;
		
		private string _status;
		
		private string _note;
		
		private string _hintPlayed;
		
		private string _reserveId;
		
		private string _reserveTime;
		
		private string _selectId;
		
		private string _seatIds;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public int population
		{
			get
			{
				return this._population;
			}
			set
			{
				if ((this._population != value))
				{
					this._population = value;
				}
			}
		}
		
		public string openTime
		{
			get
			{
				return this._openTime;
			}
			set
			{
				if ((this._openTime != value))
				{
					this._openTime = value;
				}
			}
		}
		
		public string seat
		{
			get
			{
				return this._seat;
			}
			set
			{
				if ((this._seat != value))
				{
					this._seat = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
		
		public string orderTime
		{
			get
			{
				return this._orderTime;
			}
			set
			{
				if ((this._orderTime != value))
				{
					this._orderTime = value;
				}
			}
		}
		
		public string menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				if ((this._menu != value))
				{
					this._menu = value;
				}
			}
		}
		
		public string orderTechId
		{
			get
			{
				return this._orderTechId;
			}
			set
			{
				if ((this._orderTechId != value))
				{
					this._orderTechId = value;
				}
			}
		}
		
		public string techId
		{
			get
			{
				return this._techId;
			}
			set
			{
				if ((this._techId != value))
				{
					this._techId = value;
				}
			}
		}
		
		public string startTime
		{
			get
			{
				return this._startTime;
			}
			set
			{
				if ((this._startTime != value))
				{
					this._startTime = value;
				}
			}
		}
		
		public string serverTime
		{
			get
			{
				return this._serverTime;
			}
			set
			{
				if ((this._serverTime != value))
				{
					this._serverTime = value;
				}
			}
		}
		
		public string status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this._status = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string hintPlayed
		{
			get
			{
				return this._hintPlayed;
			}
			set
			{
				if ((this._hintPlayed != value))
				{
					this._hintPlayed = value;
				}
			}
		}
		
		public string reserveId
		{
			get
			{
				return this._reserveId;
			}
			set
			{
				if ((this._reserveId != value))
				{
					this._reserveId = value;
				}
			}
		}
		
		public string reserveTime
		{
			get
			{
				return this._reserveTime;
			}
			set
			{
				if ((this._reserveTime != value))
				{
					this._reserveTime = value;
				}
			}
		}
		
		public string selectId
		{
			get
			{
				return this._selectId;
			}
			set
			{
				if ((this._selectId != value))
				{
					this._selectId = value;
				}
			}
		}
		
		public string seatIds
		{
			get
			{
				return this._seatIds;
			}
			set
			{
				if ((this._seatIds != value))
				{
					this._seatIds = value;
				}
			}
		}
    }

    #endregion

    #region 表RoomCall

    public class CRoomCall
    {
        private int _id;
		
		private string _roomId;
		
		private string _msg;
		
		private bool _read;
		
		private string _seatId;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				if ((this._roomId != value))
				{
					this._roomId = value;
				}
			}
		}
		
		public string msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				if ((this._msg != value))
				{
					this._msg = value;
				}
			}
		}
		
		public bool read
		{
			get
			{
				return this._read;
			}
			set
			{
				if ((this._read != value))
				{
					this._read = value;
				}
			}
		}
		
		public string seatId
		{
			get
			{
				return this._seatId;
			}
			set
			{
				if ((this._seatId != value))
				{
					this._seatId = value;
				}
			}
        }
    }

    #endregion

    #region 表RoomWarn

    public class CRoomWarn
    {
        private int _id;
		
		private string _msg;
		
		private string _room;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string msg
		{
			get
			{
				return this._msg;
			}
			set
			{
				if ((this._msg != value))
				{
					this._msg = value;
				}
			}
		}
		
		public string room
		{
			get
			{
				return this._room;
			}
			set
			{
				if ((this._room != value))
				{
					this._room = value;
				}
			}
		}
    }

    #endregion

    #region 表Seat

    public class CSeat
    {
        private int _id;
		
		private string _oId;
		
		private string _text;
		
		private int _typeId;
		
		private string _systemId;
		
		private string _name;
		
		private System.Nullable<int> _population;
		
		private System.Nullable<System.DateTime> _openTime;
		
		private string _openEmployee;
		
		private System.Nullable<System.DateTime> _payTime;
		
		private string _payEmployee;
		
		private string _phone;
		
		private string _discountEmployee;
		
		private System.Nullable<double> _discount;
		
		private System.Nullable<bool> _memberDiscount;
		
		private string _memberPromotionId;
		
		private string _freeEmployee;
		
		private string _chainId;
		
		private SeatStatus _status;
		
		private System.Nullable<bool> _ordering;
		
		private System.Nullable<bool> _paying;
		
		private string _note;
		
		private string _unwarn;
		
		private string _roomStatus;
		
		private System.Nullable<int> _deposit;

        private System.Nullable<int> _depositBank;
		
		private System.Nullable<System.DateTime> _dueTime;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string oId
		{
			get
			{
				return this._oId;
			}
			set
			{
				if ((this._oId != value))
				{
					this._oId = value;
				}
			}
		}
		
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}
		
		public int typeId
		{
			get
			{
				return this._typeId;
			}
			set
			{
				if ((this._typeId != value))
				{
					this._typeId = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public System.Nullable<int> population
		{
			get
			{
				return this._population;
			}
			set
			{
				if ((this._population != value))
				{
					this._population = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> openTime
		{
			get
			{
				return this._openTime;
			}
			set
			{
				if ((this._openTime != value))
				{
					this._openTime = value;
				}
			}
		}
		
		public string openEmployee
		{
			get
			{
				return this._openEmployee;
			}
			set
			{
				if ((this._openEmployee != value))
				{
					this._openEmployee = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> payTime
		{
			get
			{
				return this._payTime;
			}
			set
			{
				if ((this._payTime != value))
				{
					this._payTime = value;
				}
			}
		}
		
		public string payEmployee
		{
			get
			{
				return this._payEmployee;
			}
			set
			{
				if ((this._payEmployee != value))
				{
					this._payEmployee = value;
				}
			}
		}
		
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		public string discountEmployee
		{
			get
			{
				return this._discountEmployee;
			}
			set
			{
				if ((this._discountEmployee != value))
				{
					this._discountEmployee = value;
				}
			}
		}
		
		public System.Nullable<double> discount
		{
			get
			{
				return this._discount;
			}
			set
			{
				if ((this._discount != value))
				{
					this._discount = value;
				}
			}
		}
		
		public System.Nullable<bool> memberDiscount
		{
			get
			{
				return this._memberDiscount;
			}
			set
			{
				if ((this._memberDiscount != value))
				{
					this._memberDiscount = value;
				}
			}
		}
		
		public string memberPromotionId
		{
			get
			{
				return this._memberPromotionId;
			}
			set
			{
				if ((this._memberPromotionId != value))
				{
					this._memberPromotionId = value;
				}
			}
		}
		
		public string freeEmployee
		{
			get
			{
				return this._freeEmployee;
			}
			set
			{
				if ((this._freeEmployee != value))
				{
					this._freeEmployee = value;
				}
			}
		}
		
		public string chainId
		{
			get
			{
				return this._chainId;
			}
			set
			{
				if ((this._chainId != value))
				{
					this._chainId = value;
				}
			}
		}
		
		public SeatStatus status
		{
			get
			{
				return this._status;
			}
			set
			{
				if ((this._status != value))
				{
					this._status = value;
				}
			}
		}
		
		public System.Nullable<bool> ordering
		{
			get
			{
				return this._ordering;
			}
			set
			{
				if ((this._ordering != value))
				{
					this._ordering = value;
				}
			}
		}
		
		public System.Nullable<bool> paying
		{
			get
			{
				return this._paying;
			}
			set
			{
				if ((this._paying != value))
				{
					this._paying = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string unwarn
		{
			get
			{
				return this._unwarn;
			}
			set
			{
				if ((this._unwarn != value))
				{
					this._unwarn = value;
				}
			}
		}
		
		public string roomStatus
		{
			get
			{
				return this._roomStatus;
			}
			set
			{
				if ((this._roomStatus != value))
				{
					this._roomStatus = value;
				}
			}
		}
		
		public System.Nullable<int> deposit
		{
			get
			{
				return this._deposit;
			}
			set
			{
				if ((this._deposit != value))
				{
					this._deposit = value;
				}
			}
		}

        public System.Nullable<int> depositBank
        {
            get
            {
                return this._depositBank;
            }
            set
            {
                if ((this._depositBank != value))
                {
                    this._depositBank = value;
                }
            }
        }
		
		public System.Nullable<System.DateTime> dueTime
		{
			get
			{
				return this._dueTime;
			}
			set
			{
				if ((this._dueTime != value))
				{
					this._dueTime = value;
				}
			}
		}
    }

    #endregion

    #region 表SeatType

    public class CSeatType
    {
        private int _id;
		
		private string _name;
		
		private int _population;
		
		private System.Nullable<int> _menuId;
		
		private string _department;
		
		private System.Nullable<bool> _depositeRequired;
		
		private System.Nullable<int> _depositeAmountMin;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public int population
		{
			get
			{
				return this._population;
			}
			set
			{
				if ((this._population != value))
				{
					this._population = value;
				}
			}
		}
		
		public System.Nullable<int> menuId
		{
			get
			{
				return this._menuId;
			}
			set
			{
				if ((this._menuId != value))
				{
					this._menuId = value;
				}
			}
		}
		
		public string department
		{
			get
			{
				return this._department;
			}
			set
			{
				if ((this._department != value))
				{
					this._department = value;
				}
			}
		}
		
		public System.Nullable<bool> depositeRequired
		{
			get
			{
				return this._depositeRequired;
			}
			set
			{
				if ((this._depositeRequired != value))
				{
					this._depositeRequired = value;
				}
			}
		}
		
		public System.Nullable<int> depositeAmountMin
		{
			get
			{
				return this._depositeAmountMin;
			}
			set
			{
				if ((this._depositeAmountMin != value))
				{
					this._depositeAmountMin = value;
				}
			}
		}
    }

    #endregion

    #region 表ShoeMsg

    public class CShoeMsg
    {
        private int _id;
		
		private string _text;
		
		private string _payEmployee;
		
		private System.DateTime _payTime;
		
		private bool _processed;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string text
		{
			get
			{
				return this._text;
			}
			set
			{
				if ((this._text != value))
				{
					this._text = value;
				}
			}
		}
		
		public string payEmployee
		{
			get
			{
				return this._payEmployee;
			}
			set
			{
				if ((this._payEmployee != value))
				{
					this._payEmployee = value;
				}
			}
		}
		
		public System.DateTime payTime
		{
			get
			{
				return this._payTime;
			}
			set
			{
				if ((this._payTime != value))
				{
					this._payTime = value;
				}
			}
		}
		
		public bool processed
		{
			get
			{
				return this._processed;
			}
			set
			{
				if ((this._processed != value))
				{
					this._processed = value;
				}
			}
		}
    }

    #endregion

    #region 表Stock

    public class CStock
    {
        private int _id;
		
		private string _name;
		
		private string _note;
		
		private string _phone;
		
		private string _ips;
		
		private System.Nullable<bool> _main;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public string phone
		{
			get
			{
				return this._phone;
			}
			set
			{
				if ((this._phone != value))
				{
					this._phone = value;
				}
			}
		}
		
		public string ips
		{
			get
			{
				return this._ips;
			}
			set
			{
				if ((this._ips != value))
				{
					this._ips = value;
				}
			}
		}
		
		public System.Nullable<bool> main
		{
			get
			{
				return this._main;
			}
			set
			{
				if ((this._main != value))
				{
					this._main = value;
				}
			}
		}
    }

    #endregion

    #region 表StockIn

    public class CStockIn
    {
        private int _id;
		
		private string _name;
		
		private System.Nullable<double> _cost;
		
		private int _amount;
		
		private string _unit;
		
		private int _stockId;
		
		private string _note;
		
		private System.DateTime _date;
		
		private string _transactor;
		
		private string _checker;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public System.Nullable<double> cost
		{
			get
			{
				return this._cost;
			}
			set
			{
				if ((this._cost != value))
				{
					this._cost = value;
				}
			}
		}
		
		public int amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if ((this._amount != value))
				{
					this._amount = value;
				}
			}
		}
		
		public string unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if ((this._unit != value))
				{
					this._unit = value;
				}
			}
		}
		
		public int stockId
		{
			get
			{
				return this._stockId;
			}
			set
			{
				if ((this._stockId != value))
				{
					this._stockId = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
		
		public System.DateTime date
		{
			get
			{
				return this._date;
			}
			set
			{
				if ((this._date != value))
				{
					this._date = value;
				}
			}
		}
		
		public string transactor
		{
			get
			{
				return this._transactor;
			}
			set
			{
				if ((this._transactor != value))
				{
					this._transactor = value;
				}
			}
		}
		
		public string checker
		{
			get
			{
				return this._checker;
			}
			set
			{
				if ((this._checker != value))
				{
					this._checker = value;
				}
			}
		}
    }

    #endregion

    #region 表StockOut

    public class CStockOut
    {
        private int _id;
		
		private string _name;
		
		private System.Nullable<int> _amount;
		
		private string _unit;
		
		private System.Nullable<int> _stockId;
		
		private System.Nullable<int> _toStockId;
		
		private System.DateTime _date;
		
		private string _receiver;
		
		private string _transactor;
		
		private string _checker;
		
		private string _note;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public System.Nullable<int> amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if ((this._amount != value))
				{
					this._amount = value;
				}
			}
		}
		
		public string unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if ((this._unit != value))
				{
					this._unit = value;
				}
			}
		}
		
		public System.Nullable<int> stockId
		{
			get
			{
				return this._stockId;
			}
			set
			{
				if ((this._stockId != value))
				{
					this._stockId = value;
				}
			}
		}
		
		public System.Nullable<int> toStockId
		{
			get
			{
				return this._toStockId;
			}
			set
			{
				if ((this._toStockId != value))
				{
					this._toStockId = value;
				}
			}
		}
		
		public System.DateTime date
		{
			get
			{
				return this._date;
			}
			set
			{
				if ((this._date != value))
				{
					this._date = value;
				}
			}
		}
		
		public string receiver
		{
			get
			{
				return this._receiver;
			}
			set
			{
				if ((this._receiver != value))
				{
					this._receiver = value;
				}
			}
		}
		
		public string transactor
		{
			get
			{
				return this._transactor;
			}
			set
			{
				if ((this._transactor != value))
				{
					this._transactor = value;
				}
			}
		}
		
		public string checker
		{
			get
			{
				return this._checker;
			}
			set
			{
				if ((this._checker != value))
				{
					this._checker = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
    }

    #endregion

    #region 表StorageList

    public class CStorageList
    {
        private int _id;
		
		private string _name;
		
		private System.Nullable<double> _cost;
		
		private System.Nullable<int> _amountLastMonth;
		
		private System.Nullable<int> _amountThisMonth;
		
		private System.Nullable<int> _minAmount;
		
		private string _unit;
		
		private System.Nullable<int> _stockId;
		
		private string _note;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public System.Nullable<double> cost
		{
			get
			{
				return this._cost;
			}
			set
			{
				if ((this._cost != value))
				{
					this._cost = value;
				}
			}
		}
		
		public System.Nullable<int> amountLastMonth
		{
			get
			{
				return this._amountLastMonth;
			}
			set
			{
				if ((this._amountLastMonth != value))
				{
					this._amountLastMonth = value;
				}
			}
		}
		
		public System.Nullable<int> amountThisMonth
		{
			get
			{
				return this._amountThisMonth;
			}
			set
			{
				if ((this._amountThisMonth != value))
				{
					this._amountThisMonth = value;
				}
			}
		}
		
		public System.Nullable<int> minAmount
		{
			get
			{
				return this._minAmount;
			}
			set
			{
				if ((this._minAmount != value))
				{
					this._minAmount = value;
				}
			}
		}
		
		public string unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if ((this._unit != value))
				{
					this._unit = value;
				}
			}
		}
		
		public System.Nullable<int> stockId
		{
			get
			{
				return this._stockId;
			}
			set
			{
				if ((this._stockId != value))
				{
					this._stockId = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
    }

    #endregion

    #region 表SystemIds

    public class CSystemIds
    {
        private int _id;
		
		private string _systemId;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string systemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				if ((this._systemId != value))
				{
					this._systemId = value;
				}
			}
		}
    }

    #endregion

    #region 表TechIndex

    public class CTechIndex
    {
        private int _id;
		
		private System.Nullable<int> _dutyid;
		
		private string _ids;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public System.Nullable<int> dutyid
		{
			get
			{
				return this._dutyid;
			}
			set
			{
				if ((this._dutyid != value))
				{
					this._dutyid = value;
				}
			}
		}
		
		public string ids
		{
			get
			{
				return this._ids;
			}
			set
			{
				if ((this._ids != value))
				{
					this._ids = value;
				}
			}
		}
    }

    #endregion

    #region 表TechMsg

    public class CTechMsg
    {
        private int _id;
		
		private string _room;
		
		private string _type;
		
		private string _techType;
		
		private System.Nullable<int> _number;
		
		private string _techId;
		
		private System.DateTime _time;
		
		private System.Nullable<bool> _printed;
		
		private bool _read;
		
		private string _seat;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string room
		{
			get
			{
				return this._room;
			}
			set
			{
				if ((this._room != value))
				{
					this._room = value;
				}
			}
		}
		
		public string type
		{
			get
			{
				return this._type;
			}
			set
			{
				if ((this._type != value))
				{
					this._type = value;
				}
			}
		}
		
		public string techType
		{
			get
			{
				return this._techType;
			}
			set
			{
				if ((this._techType != value))
				{
					this._techType = value;
				}
			}
		}
		
		public System.Nullable<int> number
		{
			get
			{
				return this._number;
			}
			set
			{
				if ((this._number != value))
				{
					this._number = value;
				}
			}
		}
		
		public string techId
		{
			get
			{
				return this._techId;
			}
			set
			{
				if ((this._techId != value))
				{
					this._techId = value;
				}
			}
		}
		
		public System.DateTime time
		{
			get
			{
				return this._time;
			}
			set
			{
				if ((this._time != value))
				{
					this._time = value;
				}
			}
		}
		
		public System.Nullable<bool> printed
		{
			get
			{
				return this._printed;
			}
			set
			{
				if ((this._printed != value))
				{
					this._printed = value;
				}
			}
		}
		
		public bool read
		{
			get
			{
				return this._read;
			}
			set
			{
				if ((this._read != value))
				{
					this._read = value;
				}
			}
		}
		
		public string seat
		{
			get
			{
				return this._seat;
			}
			set
			{
				if ((this._seat != value))
				{
					this._seat = value;
				}
			}
		}
    }

    #endregion

    #region 表TechReturn

    public class CTechReturn
    {
        private int _id;
		
		private string _techId;
		
		private System.Nullable<System.DateTime> _inputTime;
		
		private string _seatId;
		
		private string _roomId;
		
		private string _menu;
		
		private string _note;
		
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string techId
		{
			get
			{
				return this._techId;
			}
			set
			{
				if ((this._techId != value))
				{
					this._techId = value;
				}
			}
		}
		
		public System.Nullable<System.DateTime> inputTime
		{
			get
			{
				return this._inputTime;
			}
			set
			{
				if ((this._inputTime != value))
				{
					this._inputTime = value;
				}
			}
		}
		
		public string seatId
		{
			get
			{
				return this._seatId;
			}
			set
			{
				if ((this._seatId != value))
				{
					this._seatId = value;
				}
			}
		}
		
		public string roomId
		{
			get
			{
				return this._roomId;
			}
			set
			{
				if ((this._roomId != value))
				{
					this._roomId = value;
				}
			}
		}
		
		public string menu
		{
			get
			{
				return this._menu;
			}
			set
			{
				if ((this._menu != value))
				{
					this._menu = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
    }

    #endregion

    #region 表Unit

    public class CUnit
    {
        private int _id;
		
		private string _name;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
    }

    #endregion

    #region 表WaiterItem

    public class CWaiterItem
    {
        private int _id;
		
		private string _name;
		
		private string _note;
		
		public int id
		{
			get
			{
				return this._id;
			}
			set
			{
				if ((this._id != value))
				{
					this._id = value;
				}
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				if ((this._name != value))
				{
					this._name = value;
				}
			}
		}
		
		public string note
		{
			get
			{
				return this._note;
			}
			set
			{
				if ((this._note != value))
				{
					this._note = value;
				}
			}
		}
    }

    #endregion
}
