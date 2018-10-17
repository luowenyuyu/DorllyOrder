using System;
namespace project.Entity.Op
{
    /// <summary>广告位明细</summary>
    /// <author>tianz</author>
    /// <date>2017-07-20</date>
    [System.Serializable]
    public class EntityContractBBRentalDetail
    {
        private string _RowPointer;
        private string _RefRP;
        private string _SRVNo;
        private string _SRVName;
        private string _BBNo;
        private string _BBName;
        private string _BBSize;
        private string _BBAddr;
        private DateTime _BBStartDate;
        private DateTime _BBEndDate;
        private string _TimeUnit;
        private int _BBRentalMonths;
        private decimal _RentalUnitPrice;
        private decimal _RentalAmount;
        private string _Remark;
        private string _Creator;
        private DateTime _CreateDate;
        private string _LastReviser;
        private DateTime _LastReviseDate;
        

        /// <summary>缺省构造函数</summary>
        public EntityContractBBRentalDetail() { }

        /// <summary>主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：合同外键
        /// 长度：36
        /// 不能为空：否
        /// </summary>
        public string RefRP
        {
            get { return _RefRP; }
            set { _RefRP = value; }
        }

        /// <summary>
        /// 功能描述：服务项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVNo
        {
            get { return _SRVNo; }
            set { _SRVNo = value; }
        }

        /// <summary>
        /// 功能描述：服务项目名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }

        /// <summary>
        /// 功能描述：广告位编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBNo
        {
            get { return _BBNo; }
            set { _BBNo = value; }
        }

        /// <summary>
        /// 功能描述：广告位名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string BBName
        {
            get { return _BBName; }
            set { _BBName = value; }
        }

        /// <summary>
        /// 功能描述：广告位尺寸大小
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BBSize
        {
            get { return _BBSize; }
            set { _BBSize = value; }
        }

        /// <summary>
        /// 功能描述：广告位位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string BBAddr
        {
            get { return _BBAddr; }
            set { _BBAddr = value; }
        }

        /// <summary>
        /// 功能描述：开始投放日期
        /// </summary>
        public DateTime BBStartDate
        {
            get { return _BBStartDate; }
            set { _BBStartDate = value; }
        }

        /// <summary>
        /// 功能描述：截止投放日期
        /// </summary>
        public DateTime BBEndDate
        {
            get { return _BBEndDate; }
            set { _BBEndDate = value; }
        }

        /// <summary>
        /// 功能描述：时间单位（Day天 Month月 Quarter季度 Year年）
        /// </summary>
        public string TimeUnit
        {
            get { return _TimeUnit; }
            set { _TimeUnit = value; }
        }

        /// <summary>
        /// 功能描述：时间单位【非维护字段】（Day天 Month月 Quarter季度 Year年）
        /// </summary>
        public string TimeUnitName
        {
            get
            {
                string _TimeUnitName = "";
                switch (_TimeUnit)
                {
                    case "Day":
                        _TimeUnitName = "按天";
                        break;
                    case "Month":
                        _TimeUnitName = "按月";
                        break;
                    case "Quarter":
                        _TimeUnitName = "按季度";
                        break;
                    case "Year":
                        _TimeUnitName = "按年";
                        break;
                }
                return _TimeUnitName;
            }
        }

        /// <summary>
        /// 功能描述：广告位租用月数
        /// </summary>
        public int BBRentalMonths
        {
            get { return _BBRentalMonths; }
            set { _BBRentalMonths = value; }
        }

        /// <summary>
        /// 功能描述：单价（元/月）
        /// </summary>
        public decimal RentalUnitPrice
        {
            get { return _RentalUnitPrice; }
            set { _RentalUnitPrice = value; }
        }

        /// <summary>
        /// 功能描述：单价（元/月）
        /// </summary>
        public decimal RentalAmount
        {
            get { return _RentalAmount; }
            set { _RentalAmount = value; }
        }

        /// <summary>
        /// 功能描述：备注
        /// 长度：500
        /// 不能为空：否
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// 功能描述：最后修改人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string LastReviser
        {
            get { return _LastReviser; }
            set { _LastReviser = value; }
        }

        /// <summary>
        /// 功能描述：最后修改日期
        /// </summary>
        public DateTime LastReviseDate
        {
            get { return _LastReviseDate; }
            set { _LastReviseDate = value; }
        }
    }
}
