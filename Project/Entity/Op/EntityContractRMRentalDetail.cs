using System;
namespace project.Entity.Op
{
    /// <summary>房屋租赁明细</summary>
    /// <author>tianz</author>
    /// <date>2017-07-20</date>
    [System.Serializable]
    public class EntityContractRMRentalDetail
    {
        private string _RowPointer;
        private string _RefRP;
        private string _RMID;
        private string _SRVNo;
        private string _SRVName;
        private string _RMLoc;
        private decimal _RMArea;
        private decimal _RentalUnitPrice;
        private string _Remark;
        private string _Creator;
        private DateTime _CreateDate;
        private string _LastReviser;
        private DateTime _LastReviseDate;
        private bool _IsFixedAmt;
        private decimal _Amount;
        private string _IncreaseType;
        private DateTime _IncreaseStartDate1;
        private decimal _IncreaseRate1;
        private DateTime _IncreaseStartDate2;
        private decimal _IncreaseRate2;
        private DateTime _IncreaseStartDate3;
        private decimal _IncreaseRate3;
        private DateTime _IncreaseStartDate4;
        private decimal _IncreaseRate4;
        
        /// <summary>缺省构造函数</summary>
        public EntityContractRMRentalDetail() { }

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
        /// 功能描述：房间编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMID
        {
            get { return _RMID; }
            set { _RMID = value; }
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
        /// 功能描述：服务项目名称【非维护项目】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }

        /// <summary>
        /// 功能描述：房屋位置
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string RMLoc
        {
            get { return _RMLoc; }
            set { _RMLoc = value; }
        }
        
        /// <summary>
        /// 功能描述：出租面积
        /// </summary>
        public decimal RMArea
        {
            get { return _RMArea; }
            set { _RMArea = value; }
        }

        /// <summary>
        /// 功能描述：单价
        /// </summary>
        public decimal RentalUnitPrice
        {
            get { return _RentalUnitPrice; }
            set { _RentalUnitPrice = value; }
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

        /// <summary>
        /// 功能描述：是否固定金额
        /// </summary>
        public bool IsFixedAmt
        {
            get { return _IsFixedAmt; }
            set { _IsFixedAmt = value; }
        }

        /// <summary>
        /// 功能描述：租金金额
        /// </summary>
        public decimal Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        /// <summary>
        /// 功能描述：递增类型（1.按递增率递增（默认），2按固定金额递增）
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string IncreaseType
        {
            get { return _IncreaseType; }
            set { _IncreaseType = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间1
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate1
        {
            get { return _IncreaseStartDate1; }
            set { _IncreaseStartDate1 = value; }
        }

        /// <summary>
        /// 功能描述：递增率1
        /// </summary>
        public decimal IncreaseRate1
        {
            get { return _IncreaseRate1; }
            set { _IncreaseRate1 = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间2
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate2
        {
            get { return _IncreaseStartDate2; }
            set { _IncreaseStartDate2 = value; }
        }

        /// <summary>
        /// 功能描述：递增率2
        /// </summary>
        public decimal IncreaseRate2
        {
            get { return _IncreaseRate2; }
            set { _IncreaseRate2 = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间3
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate3
        {
            get { return _IncreaseStartDate3; }
            set { _IncreaseStartDate3 = value; }
        }

        /// <summary>
        /// 功能描述：递增率3
        /// </summary>
        public decimal IncreaseRate3
        {
            get { return _IncreaseRate3; }
            set { _IncreaseRate3 = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间4
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate4
        {
            get { return _IncreaseStartDate4; }
            set { _IncreaseStartDate4 = value; }
        }

        /// <summary>
        /// 功能描述：递增率4
        /// </summary>
        public decimal IncreaseRate4
        {
            get { return _IncreaseRate4; }
            set { _IncreaseRate4 = value; }
        }
    }
}
