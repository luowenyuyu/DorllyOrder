using System;
namespace project.Entity.Op
{
    /// <summary>合同管理费明细</summary>
    /// <author>tianz</author>
    /// <date>2017-07-31</date>
    [System.Serializable]
    public class EntityContractRMRentList
    {
        private string _RowPointer;
        private string _RefRP;
        private string _RMID;
        private string _WPNo;
        private string _SRVNo;
        private string _SRVName;
        private DateTime _FeeStartDate;
        private DateTime _FeeEndDate;
        private decimal _FeeAmount;
        private string _FeeStatus;
        private string _Creator;
        private DateTime _CreateDate;

        /// <summary>缺省构造函数</summary>
        public EntityContractRMRentList() { }

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
        /// 功能描述：工位编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string WPNo
        {
            get { return _WPNo; }
            set { _WPNo = value; }
        }

        /// <summary>
        /// 功能描述：费用项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVNo
        {
            get { return _SRVNo; }
            set { _SRVNo = value; }
        }

        /// <summary>
        /// 功能描述：费用项目名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }

        /// <summary>
        /// 功能描述：开始日期
        /// </summary>
        public DateTime FeeStartDate
        {
            get { return _FeeStartDate; }
            set { _FeeStartDate = value; }
        }

        /// <summary>
        /// 功能描述：截止日期
        /// </summary>
        public DateTime FeeEndDate
        {
            get { return _FeeEndDate; }
            set { _FeeEndDate = value; }
        }

        /// <summary>
        /// 功能描述：金额
        /// </summary>
        public decimal FeeAmount
        {
            get { return _FeeAmount; }
            set { _FeeAmount = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string FeeStatus
        {
            get { return _FeeStatus; }
            set { _FeeStatus = value; }
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
    }
}
