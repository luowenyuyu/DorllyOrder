using System;
namespace project.Entity.Base
{
    /// <summary>房间资料</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityConferenceRoom
    {
        private string _CRNo;
        private string _CRName;
        private string _CRCapacity;
        private decimal _CRINPriceHour;
        private decimal _CRINPriceHalfDay;
        private decimal _CRINPriceDay;
        private decimal _CROUTPriceHour;
        private decimal _CROUTPriceHalfDay;
        private decimal _CROUTPriceDay;
        private decimal _CRDeposit;
        private string _CRAddr;
        private bool _CRISEnable;
        private DateTime _CRReservedDate;
        private DateTime _CRBegReservedDate;
        private DateTime _CREndReservedDate;
        private string _CRCurrentCustNo;
        private string _CRCurrentCustName;
        private string _CRStatus;
        private DateTime _CRCreateDate;
        private string _CRCreator;
        
        
        /// <summary>缺省构造函数</summary>
        public EntityConferenceRoom() { }

        /// <summary>会议室编号【主键】</summary>
        public string CRNo
        {
            get { return _CRNo; }
            set { _CRNo = value; }
        }

        /// <summary>
        /// 功能描述：会议室名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CRName
        {
            get { return _CRName; }
            set { _CRName = value; }
        }

        /// <summary>
        /// 功能描述：容纳人数
        /// </summary>
        public string CRCapacity
        {
            get { return _CRCapacity; }
            set { _CRCapacity = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/小时
        /// </summary>
        public decimal CRINPriceHour
        {
            get { return _CRINPriceHour; }
            set { _CRINPriceHour = value; }
        }

        /// <summary>
        /// 功能描述：对内价格/半天
        /// </summary>
        public decimal CRINPriceHalfDay
        {
            get { return _CRINPriceHalfDay; }
            set { _CRINPriceHalfDay = value; }
        }
        
        /// <summary>
        /// 功能描述：对内价格/全天
        /// </summary>
        public decimal CRINPriceDay
        {
            get { return _CRINPriceDay; }
            set { _CRINPriceDay = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/小时
        /// </summary>
        public decimal CROUTPriceHour
        {
            get { return _CROUTPriceHour; }
            set { _CROUTPriceHour = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/半天
        /// </summary>
        public decimal CROUTPriceHalfDay
        {
            get { return _CROUTPriceHalfDay; }
            set { _CROUTPriceHalfDay = value; }
        }

        /// <summary>
        /// 功能描述：对外价格/全天
        /// </summary>
        public decimal CROUTPriceDay
        {
            get { return _CROUTPriceDay; }
            set { _CROUTPriceDay = value; }
        }

        /// <summary>
        /// 功能描述：押金
        /// </summary>
        public decimal CRDeposit
        {
            get { return _CRDeposit; }
            set { _CRDeposit = value; }
        }

        /// <summary>
        /// 功能描述：位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string CRAddr
        {
            get { return _CRAddr; }
            set { _CRAddr = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// </summary>
        public bool CRISEnable
        {
            get { return _CRISEnable; }
            set { _CRISEnable = value; }
        }
        
        /// <summary>
        /// 功能描述：状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRStatus
        {
            get { return _CRStatus; }
            set { _CRStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态描述【非维护字段】
        /// </summary>
        public string CRStatusName
        {
            get
            {
                string _CRStatusName = "";
                switch (_CRStatus)
                {
                    case "free":
                        _CRStatusName = "空闲";
                        break;
                    case "use":
                        _CRStatusName = "占用";
                        break;
                    case "reserve":
                        _CRStatusName = "预定";
                        break;
                }
                return _CRStatusName;
            }
        }

        /// <summary>
        /// 功能描述：预定日期
        /// </summary>
        public DateTime CRReservedDate
        {
            get { return _CRReservedDate; }
            set { _CRReservedDate = value; }
        }

        /// <summary>
        /// 功能描述：预定开始日期
        /// </summary>
        public DateTime CRBegReservedDate
        {
            get { return _CRBegReservedDate; }
            set { _CRBegReservedDate = value; }
        }

        /// <summary>
        /// 功能描述：预定截止日期
        /// </summary>
        public DateTime CREndReservedDate
        {
            get { return _CREndReservedDate; }
            set { _CREndReservedDate = value; }
        }

        /// <summary>
        /// 功能描述：当前客户编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRCurrentCustNo
        {
            get { return _CRCurrentCustNo; }
            set { _CRCurrentCustNo = value; }
        }

        /// <summary>
        /// 功能描述：当前客户名称【非维护字段】
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string CRCurrentCustName
        {
            get { return _CRCurrentCustName; }
            set { _CRCurrentCustName = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime CRCreateDate
        {
            get { return _CRCreateDate; }
            set { _CRCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRCreator
        {
            get { return _CRCreator; }
            set { _CRCreator = value; }
        }
        /// <summary>
        /// 是否纳入统计：0：否；1：是
        /// bit类型
        /// </summary>
        public bool IsStatistics { get; set; }
    }
}
