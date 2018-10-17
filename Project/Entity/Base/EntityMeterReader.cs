using System;
namespace project.Entity.Base
{
    /// <summary>抄表人资料</summary>
    /// <author>tianz</author>
    /// <date>2017-09-27</date>
    [System.Serializable]
    public class EntityMeterReader
    {
        private string _ReaderNo;
        private string _ReaderName;
        private string _Status;
        private string _CreateUser;
        private DateTime _CreateDate;

        /// <summary>缺省构造函数</summary>
        public EntityMeterReader() { }

        /// <summary>抄表人编号【主键】</summary>
        public string ReaderNo
        {
            get { return _ReaderNo; }
            set { _ReaderNo = value; }
        }

        /// <summary>
        /// 功能描述：抄表人姓名
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ReaderName
        {
            get { return _ReaderName; }
            set { _ReaderName = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        /// <summary>
        /// 功能描述：状态名称【非维护字段】
        /// </summary>
        public string StatusName
        {
            get
            {
                string _StatusName = "";
                switch (_Status)
                {
                    case "open":
                        _StatusName = "启用";
                        break;
                    case "close":
                        _StatusName = "停用";
                        break;
                }
                return _StatusName;
            }
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
        /// </summary>
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value; }
        }
    }
}
