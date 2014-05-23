using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace YouSoftUtil.WX
{
    public class WxCouponManagement
    {

        /// 查找门店优惠券
        public static List<ShopCoupon> queryCouponByCompany(string ip, string companyCode, out string errorDesc)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "queryCouponByCompany",
                companyCode = companyCode
            });

            var jsonResult = HttpCon<ShopCouponResult>.run_json(ip, json, out errorDesc);
            if (jsonResult == null)
                return null;
            return jsonResult.couponStatList;
        }

        /// 查询某店某用户的优惠券记录，包括使用和未使用的
        public static ShopUserCoupontResult queryCouponByUser(string ip, string companyCode, string openid, out string errorDesc)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "queryCouponByUser",
                openid = openid,
                companyCode = companyCode
            });

            var jsonResult = HttpCon<ShopUserCoupontResult>.run_json(ip, json, out errorDesc);
            return jsonResult;
        }

        /// 查询某店优惠券
        public static List<WxCoupon> getCoupon(string ip, string companyCode, out string errorDesc)
        {
            string json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "getCoupon",
                companyCode = companyCode
            });


            var jsonResult = HttpCon<WxCouponResult>.run_json(ip, json, out errorDesc);
            if (jsonResult == null)
                return null;
            return jsonResult.couponList;
        }

        /// 发放优惠券
        public static bool extendCoupon(string ip, string companyCode, string isBatch, int couponid, string openid, out string errorDesc)
        {
            var json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "extendCoupon",
                openid = openid,
                couponid = couponid,
                isBatch = isBatch,
                companyCode = companyCode
            });

            return HttpCon<bool>.run_json_for_succes(ip, json, out errorDesc);
        }

        /// 消费优惠券
        public static ConsumeWxCouponResult consumeCoupon(string ip, string companyCode, string couponflag, out string errorDesc)
        {
            var json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "consumeCoupon",
                companyCode = companyCode,
                couponflag = couponflag
            });

            return HttpCon<ConsumeWxCouponResult>.run_json(ip, json, out errorDesc);
        }

        //上传优惠券
        public static bool uploadCoupon(string ip, int id, string companyCode, double value, string descp, string title, string isDeleted, out string errorDesc)
        {
            var json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "upLoadCoupon",
                data = new JavaScriptSerializer().Serialize(new
                {
                    id = id,
                    value = value,
                    descp = descp,
                    title = title,
                    isDeleted = isDeleted
                }),
                companyCode = companyCode
            });

            return HttpCon<bool>.run_json_for_succes(ip, json, out errorDesc);
        }

        //查询指定时间范围内的优惠券使用记录
        public static List<userCoupon> queryCouponRecords(string ip, string companyCode, string openid, string startTime, string endTime, out string errorDesc)
        {
            var json = new JavaScriptSerializer().Serialize(new
            {
                operationType = "queryCouponRecords",
                companyCode = companyCode,
                openid=openid,
                startTime = startTime,
                endTime = endTime
            });

            return HttpCon<List<userCoupon>>.run_json(ip, json, out errorDesc);
        }

    }
}
