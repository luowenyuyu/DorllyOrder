using System;
namespace project.Entity.Base
{
    /// <summary>收费科目资料</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityChargeAccount
    {
        private string _CANo;
        private string _CAName;
        private string _CASPNo;
        private string _CASPName;
        private string _APNo;

        /// <summary>缺省构造函数</summary>
        public EntityChargeAccount() { }

        /// <summary>收费科目编号【主键】</summary>
        public string CANo
        {
            get { return _CANo; }
            set { _CANo = value; }
        }

        /// <summary>
        /// 功能描述：收费科目名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CAName
        {
            get { return _CAName; }
            set { _CAName = value; }
        }

        /// <summary>
        /// 功能描述：服务商编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CASPNo
        {
            get { return _CASPNo; }
            set { _CASPNo = value; }
        }

        /// <summary>
        /// 功能描述：服务商名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CASPName
        {
            get { return _CASPName; }
            set { _CASPName = value; }
        }

        /// <summary>
        /// 功能描述：应收账款科目编码
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string APNo
        {
            get { return _APNo; }
            set { _APNo = value; }
        }
    }
}
