using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParkingOrder
{
    public class ResultList
    {
        /// <summary>
        /// 错误编码：0，成功；其他，错误
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMsg { get; set; }
        public List<Op_MonthCar> MonthCarResult { get; set; }
        public List<Op_PassCar> PassCarResult { get; set; }

    }
}
