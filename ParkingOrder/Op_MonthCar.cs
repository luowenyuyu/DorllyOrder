using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ParkingOrder
{
    [Serializable]
    /// <summary>
    /// 月租车充值记录
    /// </summary>
    public class Op_MonthCar
    {
        [Key]
        [StringLength(36)]
        /// <summary>
        /// 记录编号，主键
        /// </summary>
        public string CarNo { get; set; }
        [StringLength(15)]
        /// <summary>
        /// 车牌号码
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 缴费金额，单位：分
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 缴费时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 本次缴费使用截止日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 缴费类型
        /// 1.现金充值 
        /// 2.充正 
        /// 3.电子支付充值
        /// </summary>
        public int? ChargeType { get; set; }

    }
}
