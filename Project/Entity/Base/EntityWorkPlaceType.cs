using System;
namespace project.Entity.Base
{
    /// <summary>工位类型</summary>
    /// <author>tianz</author>
    /// <date>2017-07-12</date>
    [System.Serializable]
    public class EntityWorkPlaceType
    {
        private string _WPTypeNo;
        private string _WPTypeName;
        private int _WPTypeSeat;


        /// <summary>缺省构造函数</summary>
        public EntityWorkPlaceType() { }

        /// <summary>类型编号【主键】</summary>
        public string WPTypeNo
        {
            get { return _WPTypeNo; }
            set { _WPTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：类型名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string WPTypeName
        {
            get { return _WPTypeName; }
            set { _WPTypeName = value; }
        }

        /// <summary>
        /// 功能描述：座位数
        /// </summary>
        public int WPTypeSeat
        {
            get { return _WPTypeSeat; }
            set { _WPTypeSeat = value; }
        }
    }
}
