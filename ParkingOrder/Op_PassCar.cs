using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ParkingOrder
{
    [Serializable]
    /// <summary>
    /// 过车记录
    /// 主要获取不是月租车的消费记录
    /// </summary>
    public class Op_PassCar
    {
        [Key]
        [StringLength(36)]
        /// <summary>
        ///进出场编号，唯一
        /// </summary>
        public string UniqueID { get; set; }
        [StringLength(15)]
        /// <summary>
        ///车牌号码
        /// </summary>
        public string PlateNo { get; set; }
        /// <summary>
        /// 车辆状态 
        /// 0.车辆在场内 
        /// 1.车辆已出场
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 车辆入场时间，示例：2018-01-01 00:00:00
        /// </summary>
        public DateTime? EnterTime { get; set; }
        /// <summary>
        /// 车辆出场时间，示例：2018-01-01 00:00:00
        /// </summary>
        public DateTime? OutTime { get; set; }
        [StringLength(36)]
        /// <summary>
        /// 入口通道名称
        /// </summary>
        public string InPortName { get; set; }
        [StringLength(36)]
        /// <summary>
        /// 出口通道名称
        /// </summary>
        public string OutPortName { get; set; }
        [StringLength(36)]
        /// <summary>
        ///计费类型名称，示例：临时车，免费车，特殊月租车
        /// </summary>
        public string CarType { get; set; }
        /// <summary>
        /// 该次停车应收总费用，单位：分
        /// </summary>
        public decimal NeedPay { get; set; }
        /// <summary>
        /// 电子支付金额，单位：分
        /// </summary>
        public decimal Prepay { get; set; }
        /// <summary>
        /// 实收现金，单位：分
        /// </summary>
        public decimal ActualPay { get; set; }
        /// <summary>
        /// 商圈优惠额度，单位：（现金-分）或（时间-分钟）
        /// </summary>
        public decimal DiscountVal { get; set; }
        /// <summary>
        /// 商圈类型
        /// 1-现金 
        /// 2-时间
        /// </summary>
        public int? DiscountType { get; set; }
        /// <summary>
        /// 停车时长，单位:分钟
        /// </summary>
        public int? ResidenceTime { get; set; }
        /// <summary>
        /// 是否是自动支付模式
        /// 1.是 
        /// 0.否
        /// </summary>
        public int? AutoPay { get; set; }

    }
}
