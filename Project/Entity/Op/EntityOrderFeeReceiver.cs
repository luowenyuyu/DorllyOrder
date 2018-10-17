using System;
namespace project.Entity.Op
{
    /// <summary>订单收款记录</summary>
    /// <author>tianz</author>
    /// <date>2017-08-18</date>
    [System.Serializable]
    public class EntityOrderFeeReceiver
    {
        private string _RowPointer;
        private string _RefRP;
        private decimal _ODPaidAmount;
        private DateTime _ODPaidDate;
        private string _ODFeeReceiver;
        private string _ODFeeReceiveRemark;
        private string _ODCreator;
        private DateTime _ODCreateDate;
        private string _ODLastReviser;
        private DateTime _ODLastRevisedDate;
        private string _ODPaidType;
        private string _ODPaidBank;
        
        /// <summary>缺省构造函数</summary>
        public EntityOrderFeeReceiver() { }

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
        /// 功能描述：收款金额
        /// </summary>
        public decimal ODPaidAmount
        {
            get { return _ODPaidAmount; }
            set { _ODPaidAmount = value; }
        }

        /// <summary>
        /// 功能描述：收款日期
        /// </summary>
        public DateTime ODPaidDate
        {
            get { return _ODPaidDate; }
            set { _ODPaidDate = value; }
        }

        /// <summary>
        /// 功能描述：收款人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ODFeeReceiver
        {
            get { return _ODFeeReceiver; }
            set { _ODFeeReceiver = value; }
        }

        /// <summary>
        /// 功能描述：备注
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string ODFeeReceiveRemark
        {
            get { return _ODFeeReceiveRemark; }
            set { _ODFeeReceiveRemark = value; }
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
        /// 功能描述：收款类型
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string ODPaidType
        {
            get { return _ODPaidType; }
            set { _ODPaidType = value; }
        }

        /// <summary>
        /// 功能描述：银行
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ODPaidBank
        {
            get { return _ODPaidBank; }
            set { _ODPaidBank = value; }
        }
    }
}
