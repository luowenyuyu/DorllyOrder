using System;
namespace project.Entity.Base
{
    /// <summary>园区/建设期/楼栋/楼层资料</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityLocation
    {
        private string _LOCNo;
        private string _LOCName;
        private string _ParentLOCNo;
        private int _LOCLevel;
        
        /// <summary>缺省构造函数</summary>
        public EntityLocation() { }

        /// <summary>编号【主键】</summary>
        public string LOCNo
        {
            get { return _LOCNo; }
            set { _LOCNo = value; }
        }

        /// <summary>
        /// 功能描述：名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string LOCName
        {
            get { return _LOCName; }
            set { _LOCName = value; }
        }

        /// <summary>
        /// 功能描述：上级编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ParentLOCNo
        {
            get { return _ParentLOCNo; }
            set { _ParentLOCNo = value; }
        }

        /// <summary>
        /// 功能描述：上级名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ParentLOCName
        {
            get { 
                string _ParentLOCName ="";
                try
                {
                    if (_ParentLOCNo != "")
                    {
                        Business.Base.BusinessLocation bc = new Business.Base.BusinessLocation();
                        bc.load(_ParentLOCNo);
                        _ParentLOCName = bc.Entity.LOCName;
                    }
                }
                catch { }
                return _ParentLOCName; 
            }
        }

        /// <summary>
        /// 功能描述：层级
        /// 不能为空：否
        /// </summary>
        public int LOCLevel
        {
            get { return _LOCLevel; }
            set { _LOCLevel = value; }
        }
    }
}
