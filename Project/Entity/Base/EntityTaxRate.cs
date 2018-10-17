using System;
namespace project.Entity.Base
{
    /// <summary>费用项目税率</summary>
    /// <author>tianz</author>
    /// <date>2017-07-27</date>
    [System.Serializable]
    public class EntityTaxRate
    {
        private string _RP;
        private string _SPNo;
        private string _SPName;
        private string _SRVNo;
        private string _SRVName;
        private decimal _Rate;
        private string _CreateUser;
        private DateTime _CreateDate;
        private string _UpdateUser;
        private DateTime _UpdateDate;

        /// <summary>缺省构造函数</summary>
        public EntityTaxRate() { }

        /// <summary>主键</summary>
        public string RP
        {
            get { return _RP; }
            set { _RP = value; }
        }

        /// <summary>
        /// 功能描述：服务商编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPNo
        {
            get { return _SPNo; }
            set { _SPNo = value; }
        }

        /// <summary>
        /// 功能描述：服务商名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SPName
        {
            get { return _SPName; }
            set { _SPName = value; }
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
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }

        /// <summary>
        /// 功能描述：税率
        /// </summary>
        public decimal Rate
        {
            get { return _Rate; }
            set { _Rate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// </summary>
        public string CreateUser
        {
            get { return _CreateUser; }
            set { _CreateUser = value; }
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
        /// 功能描述：修改人
        /// </summary>
        public string UpdateUser
        {
            get { return _UpdateUser; }
            set { _UpdateUser = value; }
        }

        /// <summary>
        /// 功能描述：修改日期
        /// </summary>
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }
    }
}
