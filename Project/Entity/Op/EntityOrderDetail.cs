using System;
namespace project.Entity.Op
{
    /// <summary>订单明细</summary>
    /// <author>tianz</author>
    /// <date>2017-08-01</date>
    [System.Serializable]
    public class EntityOrderDetail
    {
        private string _RowPointer;
        private string _RefRP;
        private string _ODSRVTypeNo1;
        private string _ODSRVTypeNo2;
        private string _ODSRVNo;
        private string _ODSRVName;
        private string _ODSRVRemark;
        private string _ODSRVCalType;
        private string _ODContractSPNo;
        private string _ODContractSPName;
        private string _ODContractNo;
        private string _ODContractNoManual;
        private string _ResourceNo;
        private string _ResourceName;
        private DateTime _ODFeeStartDate;
        private DateTime _ODFeeEndDate;
        private int _BillingDays;
        private decimal _ODQTY;
        private string _ODUnit;
        private decimal _ODUnitPrice;
        private decimal _ODARAmount;
        private decimal _ODPaidAmount;
        private string _ODCANo;
        private string _ODCAName;
        private string _ODCreator;
        private DateTime _ODCreateDate;
        private string _ODLastReviser;
        private DateTime _ODLastRevisedDate;
        private decimal _ODTaxRate;
        private decimal _ODTaxAmount;
        private decimal _ReduceAmount;

        /// <summary>缺省构造函数</summary>
        public EntityOrderDetail() { }

        /// <summary>主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：订单外键
        /// 长度：36
        /// 不能为空：否
        /// </summary>
        public string RefRP
        {
            get { return _RefRP; }
            set { _RefRP = value; }
        }

        /// <summary>
        /// 功能描述：收费项目大类
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODSRVTypeNo1
        {
            get { return _ODSRVTypeNo1; }
            set { _ODSRVTypeNo1 = value; }
        }

        /// <summary>
        /// 功能描述：收费项目小类
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODSRVTypeNo2
        {
            get { return _ODSRVTypeNo2; }
            set { _ODSRVTypeNo2 = value; }
        }

        /// <summary>
        /// 功能描述：收费项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODSRVNo
        {
            get { return _ODSRVNo; }
            set { _ODSRVNo = value; }
        }

        /// <summary>
        /// 功能描述：收费项目名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ODSRVName
        {
            get { return _ODSRVName; }
            set { _ODSRVName = value; }
        }

        /// <summary>
        /// 功能描述：费用项说明
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string ODSRVRemark
        {
            get { return _ODSRVRemark; }
            set { _ODSRVRemark = value; }
        }

        /// <summary>
        /// 功能描述：收费方式 [ 1.按出租面积 2.按使用量 3.按天数 4.按次数 5.滞纳 ]
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODSRVCalType
        {
            get { return _ODSRVCalType; }
            set { _ODSRVCalType = value; }
        }

        /// <summary>
        /// 功能描述：合同甲方编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODContractSPNo
        {
            get { return _ODContractSPNo; }
            set { _ODContractSPNo = value; }
        }

        /// <summary>
        /// 功能描述：合同甲方名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODContractSPName
        {
            get { return _ODContractSPName; }
            set { _ODContractSPName = value; }
        }

        /// <summary>
        /// 功能描述：合同编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODContractNo
        {
            get { return _ODContractNo; }
            set { _ODContractNo = value; }
        }

        /// <summary>
        /// 功能描述：手工合同编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODContractNoManual
        {
            get { return _ODContractNoManual; }
            set { _ODContractNoManual = value; }
        }

        /// <summary>
        /// 功能描述：资源编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ResourceNo
        {
            get { return _ResourceNo; }
            set { _ResourceNo = value; }
        }

        /// <summary>
        /// 功能描述：资源名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ResourceName
        {
            get { return _ResourceName; }
            set { _ResourceName = value; }
        }

        /// <summary>
        /// 功能描述：计费起始日期
        /// </summary>
        public DateTime ODFeeStartDate
        {
            get { return _ODFeeStartDate; }
            set { _ODFeeStartDate = value; }
        }

        /// <summary>
        /// 功能描述：计费结束日期
        /// </summary>
        public DateTime ODFeeEndDate
        {
            get { return _ODFeeEndDate; }
            set { _ODFeeEndDate = value; }
        }

        /// <summary>
        /// 功能描述：当期计费天数
        /// </summary>
        public int BillingDays
        {
            get { return _BillingDays; }
            set { _BillingDays = value; }
        }

        /// <summary>
        /// 功能描述：数量
        /// </summary>
        public decimal ODQTY
        {
            get { return _ODQTY; }
            set { _ODQTY = value; }
        }

        /// <summary>
        /// 功能描述：单位
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODUnit
        {
            get { return _ODUnit; }
            set { _ODUnit = value; }
        }

        /// <summary>
        /// 功能描述：费用单价
        /// </summary>
        public decimal ODUnitPrice
        {
            get { return _ODUnitPrice; }
            set { _ODUnitPrice = value; }
        }

        /// <summary>
        /// 功能描述：应收金额
        /// </summary>
        public decimal ODARAmount
        {
            get { return _ODARAmount; }
            set { _ODARAmount = value; }
        }

        /// <summary>
        /// 功能描述：已收金额
        /// </summary>
        public decimal ODPaidAmount
        {
            get { return _ODPaidAmount; }
            set { _ODPaidAmount = value; }
        }

        ///// <summary>
        ///// 功能描述：实收金额
        ///// </summary>
        //public decimal ODPaidinAmount
        //{
        //    get { return _ODPaidinAmount; }
        //    set { _ODPaidinAmount = value; }
        //}

        ///// <summary>
        ///// 功能描述：收款日期
        ///// </summary>
        //public DateTime ODPaidDate
        //{
        //    get { return _ODPaidDate; }
        //    set { _ODPaidDate = value; }
        //}

        ///// <summary>
        ///// 功能描述：收款人
        ///// 长度：30
        ///// 不能为空：否
        ///// </summary>
        //public string ODFeeReceiver
        //{
        //    get { return _ODFeeReceiver; }
        //    set { _ODFeeReceiver = value; }
        //}

        ///// <summary>
        ///// 功能描述：收款备注
        ///// 长度：300
        ///// 不能为空：否
        ///// </summary>
        //public string ODFeeReceiveRemark
        //{
        //    get { return _ODFeeReceiveRemark; }
        //    set { _ODFeeReceiveRemark = value; }
        //}

        ///// <summary>
        ///// 功能描述：减免金额
        ///// </summary>
        //public decimal ODReduceAmount
        //{
        //    get { return _ODReduceAmount; }
        //    set { _ODReduceAmount = value; }
        //}

        ///// <summary>
        ///// 功能描述：开始减免日期
        ///// </summary>
        //public DateTime ODReduceStartDate
        //{
        //    get { return _ODReduceStartDate; }
        //    set { _ODReduceStartDate = value; }
        //}

        ///// <summary>
        ///// 功能描述：结束减免日期
        ///// </summary>
        //public DateTime ODReduceEndDate
        //{
        //    get { return _ODReduceEndDate; }
        //    set { _ODReduceEndDate = value; }
        //}

        ///// <summary>
        ///// 功能描述：减免日期
        ///// </summary>
        //public DateTime ODReducedDate
        //{
        //    get { return _ODReducedDate; }
        //    set { _ODReducedDate = value; }
        //}

        ///// <summary>
        ///// 功能描述：减免人
        ///// 长度：30
        ///// 不能为空：否
        ///// </summary>
        //public string ODReducePerson
        //{
        //    get { return _ODReducePerson; }
        //    set { _ODReducePerson = value; }
        //}

        ///// <summary>
        ///// 功能描述：减免原因
        ///// 长度：300
        ///// 不能为空：否
        ///// </summary>
        //public string ODReduceReason
        //{
        //    get { return _ODReduceReason; }
        //    set { _ODReduceReason = value; }
        //}

        /// <summary>
        /// 功能描述：财务收费科目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODCANo
        {
            get { return _ODCANo; }
            set { _ODCANo = value; }
        }

        /// <summary>
        /// 功能描述：财务收费科目名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODCAName
        {
            get { return _ODCAName; }
            set { _ODCAName = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODCreator
        {
            get { return _ODCreator; }
            set { _ODCreator = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime ODCreateDate
        {
            get { return _ODCreateDate; }
            set { _ODCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：最后修改人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODLastReviser
        {
            get { return _ODLastReviser; }
            set { _ODLastReviser = value; }
        }

        /// <summary>
        /// 功能描述：最后修改日期
        /// </summary>
        public DateTime ODLastRevisedDate
        {
            get { return _ODLastRevisedDate; }
            set { _ODLastRevisedDate = value; }
        }

        /// <summary>
        /// 功能描述：税率
        /// </summary>
        public decimal ODTaxRate
        {
            get { return _ODTaxRate; }
            set { _ODTaxRate = value; }
        }

        /// <summary>
        /// 功能描述：税额
        /// </summary>
        public decimal ODTaxAmount
        {
            get { return _ODTaxAmount; }
            set { _ODTaxAmount = value; }
        }

        /// <summary>
        /// 功能描述：减免金额
        /// </summary>
        public decimal ReduceAmount
        {
            get { return _ReduceAmount; }
            set { _ReduceAmount = value; }
        }
    }
}
