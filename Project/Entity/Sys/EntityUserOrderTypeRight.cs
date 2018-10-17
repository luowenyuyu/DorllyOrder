using System;
namespace project.Entity.Sys
{
    /// <summary>用户订单类型权限</summary>
    /// <author>tianz</author>
    /// <date>2017-05-19</date>
    [System.Serializable]
    public class EntityUserOrderTypeRight
    {
        private string _entityOID;
        private string _orderType;
        private string _userType;

        /// <summary>缺省构造函数</summary>
        public EntityUserOrderTypeRight() { }

        /// <summary>主键，只读属性</summary>
        public System.Guid EntityOID
        {
            get { return new System.Guid(_entityOID); }
        }

        /// <summary>内部映射主键</summary>
        public string InnerEntityOID
        {
            get { return _entityOID; }
            set { _entityOID = value; }
        }

        /// <summary>
        /// 功能描述：订单类型
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string OrderType
        {
            get { return _orderType; }
            set { _orderType = value; }
        }

        /// <summary>
        /// 功能描述：用户类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
    }
}
