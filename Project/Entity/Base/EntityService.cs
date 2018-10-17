using System;
namespace project.Entity.Base
{
    /// <summary>费用项目</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityService
    {
        private string _SRVNo;
        private string _SRVName;
        private string _SRVTypeNo1;
        private string _SRVTypeNo1Name;
        private string _SRVTypeNo2;
        private string _SRVTypeNo2Name;
        private string _SRVSPNo;
        private string _SRVSPName;
        private string _CANo;
        private string _CAName;
        private string _SRVCalType;
        private string _SRVRoundType;
        private int _SRVDecimalPoint;
        private decimal _SRVRate;
        private decimal _SRVTaxRate;
        private bool _SRVStatus;
        private string _SRVRemark;

        /// <summary>缺省构造函数</summary>
        public EntityService() { }

        /// <summary>收费项目编号【主键】</summary>
        public string SRVNo
        {
            get { return _SRVNo; }
            set { _SRVNo = value; }
        }

        /// <summary>
        /// 功能描述：收费项目
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }

        /// <summary>
        /// 功能描述：所属服务大类编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVTypeNo1
        {
            get { return _SRVTypeNo1; }
            set { _SRVTypeNo1 = value; }
        }

        /// <summary>
        /// 功能描述：所属服务大类名称【非维护字段】
        /// 不能为空：否
        /// </summary>
        public string SRVTypeNo1Name
        {
            get { return _SRVTypeNo1Name; }
            set { _SRVTypeNo1Name = value; }
        }

        /// <summary>
        /// 功能描述：所属服务小类编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVTypeNo2
        {
            get { return _SRVTypeNo2; }
            set { _SRVTypeNo2 = value; }
        }

        /// <summary>
        /// 功能描述：所属服务小类名称【非维护字段】
        /// 不能为空：否
        /// </summary>
        public string SRVTypeNo2Name
        {
            get { return _SRVTypeNo2Name; }
            set { _SRVTypeNo2Name = value; }
        }

        /// <summary>
        /// 功能描述：所属服务商编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVSPNo
        {
            get { return _SRVSPNo; }
            set { _SRVSPNo = value; }
        }

        /// <summary>
        /// 功能描述：所属服务商名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVSPName
        {
            get { return _SRVSPName; }
            set { _SRVSPName = value; }
        }

        /// <summary>
        /// 功能描述：科目编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CANo
        {
            get { return _CANo; }
            set { _CANo = value; }
        }

        /// <summary>
        /// 功能描述：科目名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string CAName
        {
            get { return _CAName; }
            set { _CAName = value; }
        }

        /// <summary>
        /// 功能描述：收费方式 [ 1.按出租面积 2.按使用量 3.按天数 4.按次数 5.滞纳 ]
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string SRVCalType
        {
            get { return _SRVCalType; }
            set { _SRVCalType = value; }
        }

        /// <summary>
        /// 功能描述：取整方式
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string SRVRoundType
        {
            get { return _SRVRoundType; }
            set { _SRVRoundType = value; }
        }

        /// <summary>
        /// 功能描述：取整方式【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVRoundTypeName
        {
            get
            {
                string _SRVRoundTypeName = "";
                switch (_SRVRoundType)
                {
                    case "round":
                        _SRVRoundTypeName = "四舍五入";
                        break;
                    case "ceiling":
                        _SRVRoundTypeName = "向上取位";
                        break;
                    case "floor":
                        _SRVRoundTypeName = "向下取位";
                        break;
                }
                return _SRVRoundTypeName;
            }
        }

        /// <summary>
        /// 功能描述：精准位数
        /// </summary>
        public int SRVDecimalPoint
        {
            get { return _SRVDecimalPoint; }
            set { _SRVDecimalPoint = value; }
        }

        /// <summary>
        /// 功能描述：倍率
        /// </summary>
        public decimal SRVRate
        {
            get { return _SRVRate; }
            set { _SRVRate = value; }
        }

        /// <summary>
        /// 功能描述：税率
        /// </summary>
        public decimal SRVTaxRate
        {
            get { return _SRVTaxRate; }
            set { _SRVTaxRate = value; }
        }

        /// <summary>
        /// 功能描述：状态
        /// 不能为空：否
        /// </summary>
        public bool SRVStatus
        {
            get { return _SRVStatus; }
            set { _SRVStatus = value; }
        }

        /// <summary>
        /// 功能描述：备注
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string SRVRemark
        {
            get { return _SRVRemark; }
            set { _SRVRemark = value; }
        }

    }
}
