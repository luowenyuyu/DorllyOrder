using System;
namespace project.Entity.Base
{
    /// <summary>违约金费用项目</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityLateFee
    {
        private string _RowPointer;
        private string _SRVNo;
        private string _SRVName;
        private string _LateFeeSRVNo;
        private string _LateFeeSRVName;
        private string _CreateUser;
        private DateTime _CreateDate;
        private string _UpdateUser;
        private DateTime _UpdateDate;

        /// <summary>缺省构造函数</summary>
        public EntityLateFee() { }

        /// <summary>编号【主键】</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：费用项目
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVNo
        {
            get { return _SRVNo; }
            set { _SRVNo = value; }
        }

        /// <summary>
        /// 功能描述：费用项目名称【非维护项目】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }

        /// <summary>
        /// 功能描述：违约金项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string LateFeeSRVNo
        {
            get { return _LateFeeSRVNo; }
            set { _LateFeeSRVNo = value; }
        }

        /// <summary>
        /// 功能描述：违约金项目【非维护项目】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string LateFeeSRVName
        {
            get { return _LateFeeSRVName; }
            set { _LateFeeSRVName = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// 不能为空：否
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }

        /// <summary>
        /// 功能描述：修改人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }

        /// <summary>
        /// 功能描述：修改日期
        /// 不能为空：否
        /// </summary>
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
    }
}
