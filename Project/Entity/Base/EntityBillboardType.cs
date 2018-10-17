using System;
namespace project.Entity.Base
{
    /// <summary>广告位类型</summary>
    /// <author>tianz</author>
    /// <date>2017-07-12</date>
    [System.Serializable]
    public class EntityBillboardType
    {
        private string _BBTypeNo;
        private string _BBTypeName;


        /// <summary>缺省构造函数</summary>
        public EntityBillboardType() { }

        /// <summary>广告位类型编号【主键】</summary>
        public string BBTypeNo
        {
            get { return _BBTypeNo; }
            set { _BBTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：广告位类型名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string BBTypeName
        {
            get { return _BBTypeName; }
            set { _BBTypeName = value; }
        }
    }
}
