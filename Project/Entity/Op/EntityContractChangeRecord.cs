using System;
namespace project.Entity.Op
{
    /// <summary>合同变更记录</summary>
    /// <author>tianz</author>
    /// <date>2017-07-25</date>
    [System.Serializable]
    public class EntityContractChangeRecord
    {
        private string _RowPointer;
        private string _RefRP;
        private string _ChangeItem;
        private string _BeforeChangeContent;
        private string _AfterChangeContent;
        private string _ChangeUser;
        private DateTime _ChangeDate;
        private string _ChangeReason;
        private string _Remark;
        private string _Creator;
        private DateTime _CreateDate;
        private string _LastReviser;
        private DateTime _LastReviseDate;
        

        /// <summary>缺省构造函数</summary>
        public EntityContractChangeRecord() { }

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
        /// 功能描述：变更项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ChangeItem
        {
            get { return _ChangeItem; }
            set { _ChangeItem = value; }
        }

        /// <summary>
        /// 功能描述：变更前内容
        /// 长度：500
        /// 不能为空：否
        /// </summary>
        public string BeforeChangeContent
        {
            get { return _BeforeChangeContent; }
            set { _BeforeChangeContent = value; }
        }

        /// <summary>
        /// 功能描述：变更后内容
        /// 长度：500
        /// 不能为空：否
        /// </summary>
        public string AfterChangeContent
        {
            get { return _AfterChangeContent; }
            set { _AfterChangeContent = value; }
        }

        /// <summary>
        /// 功能描述：变更人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ChangeUser
        {
            get { return _ChangeUser; }
            set { _ChangeUser = value; }
        }

        /// <summary>
        /// 功能描述：变更时间
        /// </summary>
        public DateTime ChangeDate
        {
            get { return _ChangeDate; }
            set { _ChangeDate = value; }
        }

        /// <summary>
        /// 功能描述：变更原因
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string ChangeReason
        {
            get { return _ChangeReason; }
            set { _ChangeReason = value; }
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
