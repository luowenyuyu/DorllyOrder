using System;
namespace project.Entity.Base
{
    /// <summary>订单类型</summary>
    /// <author>tianz</author>
    /// <date>2017-07-13</date>
    [System.Serializable]
    public class EntityOrderType
    {
        private string _OrderTypeNo;
        private string _OrderTypeName;


        /// <summary>缺省构造函数</summary>
        public EntityOrderType() { }

        /// <summary>订单类型编号【主键】</summary>
        public string OrderTypeNo
        {
            get { return _OrderTypeNo; }
            set { _OrderTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：订单类型名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string OrderTypeName
        {
            get { return _OrderTypeName; }
            set { _OrderTypeName = value; }
        }
    }
}
