using System;
namespace project.Entity.Op
{
    /// <summary>订单信息</summary>
    /// <author>tianz</author>
    /// <date>2017-08-01</date>
    [System.Serializable]
    public class EntityOrderHeader
    {
        private string _RowPointer;
        private string _OrderNo;
        private string _OrderType;
        private string _OrderTypeName;
        private string _CustNo;
        private string _CustName;
        private string _CustShortName;
        private DateTime _OrderTime;
        private int _DaysofMonth;
        private DateTime _ARDate;
        private decimal _ARAmount;
        private decimal _ReduceAmount;
        private decimal _PaidinAmount;
        private string _OrderAuditor;
        private DateTime _OrderAuditDate;
        private string _OrderAuditReason;
        private string _OrderRAuditor;
        private DateTime _OrderRAuditDate;
        private string _OrderRAuditReason;
        private string _Remark;
        private string _OrderStatus;
        private string _OrderCreator;
        private DateTime _OrderCreateDate;
        private string _OrderLastReviser;
        private DateTime _OrderLastRevisedDate;
        
        /// <summary>缺省构造函数</summary>
        public EntityOrderHeader() { }

        /// <summary>主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：订单编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        /// <summary>
        /// 功能描述：订单类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }

        /// <summary>
        /// 功能描述：订单类型名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string OrderTypeName
        {
            get { return _OrderTypeName; }
            set { _OrderTypeName = value; }
        }

        /// <summary>
        /// 功能描述：客户编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustNo
        {
            get { return _CustNo; }
            set { _CustNo = value; }
        }

        /// <summary>
        /// 功能描述：客户名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }

        /// <summary>
        /// 功能描述：客户简称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CustShortName
        {
            get { return _CustShortName; }
            set { _CustShortName = value; }
        }

        /// <summary>
        /// 功能描述：所属年月
        /// </summary>
        public DateTime OrderTime
        {
            get { return _OrderTime; }
            set { _OrderTime = value; }
        }

        /// <summary>
        /// 功能描述：当月天数
        /// </summary>
        public int DaysofMonth
        {
            get { return _DaysofMonth; }
            set { _DaysofMonth = value; }
        }

        /// <summary>
        /// 功能描述：应收日期
        /// </summary>
        public DateTime ARDate
        {
            get { return _ARDate; }
            set { _ARDate = value; }
        }

        /// <summary>
        /// 功能描述：应收金额
        /// </summary>
        public decimal ARAmount
        {
            get { return _ARAmount; }
            set { _ARAmount = value; }
        }

        /// <summary>
        /// 功能描述：减免金额
        /// </summary>
        public decimal ReduceAmount
        {
            get { return _ReduceAmount; }
            set { _ReduceAmount = value; }
        }

        /// <summary>
        /// 功能描述：实收金额
        /// </summary>
        public decimal PaidinAmount
        {
            get { return _PaidinAmount; }
            set { _PaidinAmount = value; }
        }

        /// <summary>
        /// 功能描述：审核人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderAuditor
        {
            get { return _OrderAuditor; }
            set { _OrderAuditor = value; }
        }

        /// <summary>
        /// 功能描述：审核日期
        /// </summary>
        public DateTime OrderAuditDate
        {
            get { return _OrderAuditDate; }
            set { _OrderAuditDate = value; }
        }

        /// <summary>
        /// 功能描述：审核不通过原因
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string OrderAuditReason
        {
            get { return _OrderAuditReason; }
            set { _OrderAuditReason = value; }
        }

        /// <summary>
        /// 功能描述：反审核人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderRAuditor
        {
            get { return _OrderRAuditor; }
            set { _OrderRAuditor = value; }
        }

        /// <summary>
        /// 功能描述：反审核日期
        /// </summary>
        public DateTime OrderRAuditDate
        {
            get { return _OrderRAuditDate; }
            set { _OrderRAuditDate = value; }
        }

        /// <summary>
        /// 功能描述：反审核原因
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string OrderRAuditReason
        {
            get { return _OrderRAuditReason; }
            set { _OrderRAuditReason = value; }
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
        /// 功能描述：状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderStatus
        {
            get { return _OrderStatus; }
            set { _OrderStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string OrderStatusName
        {
            get
            {
                string _OrderStatusName = "";
                switch (_OrderStatus)
                {
                    case "0":
                        _OrderStatusName = "待审核";
                        break;
                    case "1":
                        _OrderStatusName = "审核通过";
                        break;
                    case "2":
                        _OrderStatusName = "部分收款";
                        break;
                    case "3":
                        _OrderStatusName = "完成收款";
                        break;
                    case "-1":
                        _OrderStatusName = "审核不通过";
                        break;
                }
                return _OrderStatusName;
            }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderCreator
        {
            get { return _OrderCreator; }
            set { _OrderCreator = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime OrderCreateDate
        {
            get { return _OrderCreateDate; }
            set { _OrderCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：最后修改人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderLastReviser
        {
            get { return _OrderLastReviser; }
            set { _OrderLastReviser = value; }
        }

        /// <summary>
        /// 功能描述：最后修改日期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime OrderLastRevisedDate
        {
            get { return _OrderLastRevisedDate; }
            set { _OrderLastRevisedDate = value; }
        }
    }
}
