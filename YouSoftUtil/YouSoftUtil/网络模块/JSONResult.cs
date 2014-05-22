using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouSoftUtil
{
    public class JSONResult<T>
    {
        private bool _success;
        private string _type;
        private int _errorCode;
        private string _errorDesc;
        private T _result;

        public T result
        {
            get { return _result; }
            set { _result = value; }
        }

        public bool success
        {
            get { return _success; }
            set { _success = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int errorCode
        {
            get { return _errorCode; }
            set { _errorCode = value; }
        }

        public string errorDesc
        {
            get { return _errorDesc; }
            set { _errorDesc = value; }
        }
    }

    //微信用户查找结果
    public class WxCouponResult
    {
        private List<WxCoupon> _couponList;

        public List<WxCoupon> couponList
        {
            get { return _couponList; }
            set { _couponList = value; }
        }
    }

    //微信用户查找结果
    public class WxUserResult
    {
        private List<WxUser> _wxUserList;

        public List<WxUser> wxUserList
        {
            get { return _wxUserList; }
            set { _wxUserList = value; }
        }
    }

    //门店优惠券查找结果
    public class ShopCouponResult
    {
        private List<ShopCoupon> _couponStatList;

        public List<ShopCoupon> couponStatList
        {
            get { return _couponStatList; }
            set { _couponStatList = value; }
        }
    }

    public class ShopUserCoupontResult
    {
        private List<ShopUserUnUsedCoupon> _unUseList;
        private List<ShopUserUsedCoupon> _usedList;

        public List<ShopUserUnUsedCoupon> unUseList
        {
            get { return _unUseList; }
            set { _unUseList = value; }
        }

        public List<ShopUserUsedCoupon> usedList
        {
            get { return _usedList; }
            set { _usedList = value; }
        }
    }

    //微信优惠券
    public class WxCoupon
    {
        private int _id;
        private string _type;
        private double _value;

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string type
        {
            get { return _type; }
            set { _type = value; }
        }

        public double value
        {
            get { return _value; }
            set { _value = value; }
        }
    }

    //门店某用户未使用优惠券
    public class ShopUserUnUsedCoupon
    {
        private int _count;//优惠券数量
        private int _id;//优惠券id
        private string _title;//优惠券名称

        public int count
        {
            get { return _count; }
            set { _count = value; }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
    }

    //门店某用户使用过的优惠券记录
    public class ShopUserUsedCoupon
    {
        private DateTime _consumeTime;//优惠券消费时间
        private int _id;//优惠券id
        private string _title;//优惠券名称

        public DateTime consumeTime
        {
            get { return _consumeTime; }
            set { _consumeTime = value; }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
    }

    //门店优惠券
    public class ShopCoupon
    {
        private int _count;//门店某种优惠券数量
        private int _id;//门店某种优惠券ID号
        private string _isConsume;//门店某种优惠券是否消费
        private string _title;//门店某种优惠券名称

        public int count
        {
            get { return _count; }
            set { _count = value; }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string isConsume
        {
            get { return _isConsume; }
            set { _isConsume = value; }
        }

        public string title
        {
            get { return _title; }
            set { _title = value; }
        }
    }

    //门店业绩
    public class ShopYeJi
    {
        private string _companyName;

        private double _accountbankUnion;
        private double _accountCash;
        private double _cardSaleBankUnion;
        private double _cardSaleCash;
        private double _coupon;
        private double _creditCard;
        private double _deducted;
        private double _groupBuy;
        private double _server;
        private double _totalBankUnion;
        private double _totalCash;
        private double _totalRevenue;
        private double _wipeZero;
        private double _zero;

        public string companyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        public double accountbankUnion
        {
            get { return _accountbankUnion; }
            set { _accountbankUnion = value; }
        }

        public double accountCash
        {
            get { return _accountCash; }
            set { _accountCash = value; }
        }

        public double cardSaleBankUnion
        {
            get { return _cardSaleBankUnion; }
            set { _cardSaleBankUnion = value; }
        }

        public double cardSaleCash
        {
            get { return _cardSaleCash; }
            set { _cardSaleCash = value; }
        }

        public double coupon
        {
            get { return _coupon; }
            set { _coupon = value; }
        }

        public double creditCard
        {
            get { return _creditCard; }
            set { _creditCard = value; }
        }

        public double deducted
        {
            get { return _deducted; }
            set { _deducted = value; }
        }

        public double groupBuy
        {
            get { return _groupBuy; }
            set { _groupBuy = value; }
        }

        public double server
        {
            get { return _server; }
            set { _server = value; }
        }

        public double totalBankUnion
        {
            get { return _totalBankUnion; }
            set { _totalBankUnion = value; }
        }

        public double totalCash
        {
            get { return _totalCash; }
            set { _totalCash = value; }
        }

        public double totalRevenue
        {
            get { return _totalRevenue; }
            set { _totalRevenue = value; }
        }

        public double wipeZero
        {
            get { return _wipeZero; }
            set { _wipeZero = value; }
        }

        public double zero
        {
            get { return _zero; }
            set { _zero = value; }
        }
    }

    //微信用户
    public class WxUser
    {
        private string _companyCode;
        private string _headimgurl;
        private string _nickname;
        private string _openid;

        public string companyCode
        {
            get { return _companyCode; }
            set { _companyCode = value; }
        }

        public string headimgurl
        {
            get { return _headimgurl; }
            set { _headimgurl = value; }
        }

        public string nickname
        {
            get { return _nickname; }
            set { _nickname = value; }
        }

        public string openid
        {
            get { return _openid; }
            set { _openid = value; }
        }
    }
}
