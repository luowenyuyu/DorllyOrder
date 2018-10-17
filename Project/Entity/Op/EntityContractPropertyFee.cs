using System;
namespace project.Entity.Op
{
    /// <summary>合同管理费明细</summary>
    /// <author>tianz</author>
    /// <date>2017-07-26</date>
    [System.Serializable]
    public class EntityContractPropertyFee
    {
        private string _RowPointer;
        private string _RefRP;
        private string _RMID;
        private string _WPNo;
        private decimal _RMArea;
        private decimal _UnitPrice;
        private string _Remark;
        private string _SRVNo;
        private string _SRVName;
        private string _Creator;
        private DateTime _CreateDate;
        private string _LastReviser;
        private DateTime _LastReviseDate;

        /// <summary>缺省构造函数</summary>
        public EntityContractPropertyFee() { }

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
        /// 功能描述：面积
        /// </summary>
        public decimal RMArea
        {
            get { return _RMArea; }
            set { _RMArea = value; }
        }

        /// <summary>
        /// 功能描述：单价（元/㎡/月）
        /// </summary>
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
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

        public string WPNo
        {
            get
            {
                return _WPNo;
            }

            set
            {
                _WPNo = value;
            }
        }
    }
}
