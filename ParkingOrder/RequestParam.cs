using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingOrder
{
    public class RequestParam
    {
        /// <summary>
        /// 停车场编码(接口方提供)
        /// </summary>
        public string parkingCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 请求时间
        /// </summary>
        public string requestTime { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 一次获取数据大小
        /// </summary>
        public string retLimit { get; set; }
    }
}
