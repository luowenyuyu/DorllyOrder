using System;
namespace project.Entity.Base
{
    /// <summary>服务类型资料</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityServiceType
    {
        private string _SRVTypeNo;
        private string _SRVTypeName;
        private string _ParentTypeNo;
        private string _SRVSPNo;
        private string _SRVSPName;
        private string _Remark;
        private bool _SRVStatus;


        /// <summary>缺省构造函数</summary>
        public EntityServiceType() { }

        /// <summary>服务类型编号【主键】</summary>
        public string SRVTypeNo
        {
            get { return _SRVTypeNo; }
            set { _SRVTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：服务类型名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVTypeName
        {
            get { return _SRVTypeName; }
            set { _SRVTypeName = value; }
        }

        /// <summary>
        /// 功能描述：父项编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ParentTypeNo
        {
            get { return _ParentTypeNo; }
            set { _ParentTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：父项名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ParentTypeName
        {
            get {
                string _ParentTypeName = "";
                try
                {
                    if (_ParentTypeNo != "")
                    {
                        Business.Base.BusinessServiceType bc = new Business.Base.BusinessServiceType();
                        bc.load(_ParentTypeNo);
                        _ParentTypeName = bc.Entity.SRVTypeName;
                    }
                }
                catch { }
                return _ParentTypeName;
            }
        }

        /// <summary>
        /// 功能描述：服务商编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVSPNo
        {
            get { return _SRVSPNo; }
            set { _SRVSPNo = value; }
        }

        /// <summary>
        /// 功能描述：服务商名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVSPName
        {
            get { return _SRVSPName; }
            set { _SRVSPName = value; }
        }

        /// <summary>
        /// 功能描述：备注描述
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        /// <summary>
        /// 功能描述：是否生效
        /// 不能为空：否
        /// </summary>
        public bool SRVStatus
        {
            get { return _SRVStatus; }
            set { _SRVStatus = value; }
        }
    }
}
