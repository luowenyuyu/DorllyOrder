using System;
namespace project.Entity.Sys
{
    /// <summary>用户类型表</summary>
    /// <author>tianz</author>
    /// <date>2017-05-19</date>
    [System.Serializable]
    public class EntityUserType
    {
        private string _UserTypeNo;
        private string _UserTypeName;

        /// <summary>缺省构造函数</summary>
        public EntityUserType() { }

        /// <summary>用户类型编号</summary>
        public string UserTypeNo
        {
            get { return _UserTypeNo; }
            set { _UserTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：用户类型名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string UserTypeName
        {
            get { return _UserTypeName; }
            set { _UserTypeName = value; }
        }
    }
}
