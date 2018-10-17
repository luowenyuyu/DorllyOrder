using System;
namespace project.Entity.Sys
{
    /// <summary>用户权限分配</summary>
    /// <author>tz</author>
    /// <date>2016-2-18</date>
    [System.Serializable]
    public class EntityUserOrderTypeRights
    {
        private string _entityOID;
        private string _orderType;
        private string _orderTypeName;
        private bool _right;

        /// <summary>缺省构造函数</summary>
        public EntityUserOrderTypeRights() { }

        /// <summary>主键，只读属性</summary>
        public System.Guid EntityOID
        {
            get { return new System.Guid(_entityOID); }
        }
        
        /// <summary>
        /// 功能描述：订单类型
        /// 长度：100
        /// 不能为空：否
        /// </summary>
        public string OrderType
        {
            get { return _orderType; }
            set { _orderType = value; }
        }

        /// <summary>
        /// 功能描述：订单类型名称
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OrderTypeName
        {
            get { return _orderTypeName; }
            set { _orderTypeName = value; }
        }

        /// <summary>
        /// 功能描述：是否有权限
        /// </summary>
        public bool Right
        {
            get { return _right; }
            set { _right = value; }
        }
    }
}
