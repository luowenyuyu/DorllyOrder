using System;
namespace project.Entity.Base
{
    /// <summary>广告位资料</summary>
    /// <author>tianz</author>
    /// <date>2017-07-12</date>
    [System.Serializable]
    public class EntityMeter
    {
        private string _MeterNo;
        private string _MeterName;
        private string _MeterType;
        private string _MeterLOCNo1;
        private string _MeterLOCNo1Name;
        private string _MeterLOCNo2;
        private string _MeterLOCNo2Name;
        private string _MeterLOCNo3;
        private string _MeterLOCNo3Name;
        private string _MeterLOCNo4;
        private string _MeterLOCNo4Name;
        private decimal _MeterRate;
        private int _MeterDigit;
        private string _MeterUsageType;
        private string _MeterNatureType;
        private decimal _MeterReadout;
        private DateTime _MeterReadoutDate;
        private string _MeterRMID;
        private string _MeterSize;
        private string _MeterRelatedMeterNo;
        private string _Addr;
        private string _MeterStatus;
        private DateTime _MeterCreateDate;
        private string _MeterCreator;
        
        /// <summary>缺省构造函数</summary>
        public EntityMeter() { }

        /// <summary>表记编号【主键】</summary>
        public string MeterNo
        {
            get { return _MeterNo; }
            set { _MeterNo = value; }
        }

        /// <summary>
        /// 功能描述：表记名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string MeterName
        {
            get { return _MeterName; }
            set { _MeterName = value; }
        }

        /// <summary>
        /// 功能描述：表记类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterType
        {
            get { return _MeterType; }
            set { _MeterType = value; }
        }

        /// <summary>
        /// 功能描述：表记类型名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string MeterTypeName
        {
            get
            {
                string _MeterTypeName = "";
                switch (_MeterType)
                {
                    case "wm":
                        _MeterTypeName = "水表";
                        break;
                    case "am":
                        _MeterTypeName = "电表";
                        break;
                }
                return _MeterTypeName;
            }
        }

        /// <summary>
        /// 功能描述：园区
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo1
        {
            get { return _MeterLOCNo1; }
            set { _MeterLOCNo1 = value; }
        }

        /// <summary>
        /// 功能描述：园区名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo1Name
        {
            get { return _MeterLOCNo1Name; }
            set { _MeterLOCNo1Name = value; }
        }

        /// <summary>
        /// 功能描述：建设期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo2
        {
            get { return _MeterLOCNo2; }
            set { _MeterLOCNo2 = value; }
        }

        /// <summary>
        /// 功能描述：建设期名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo2Name
        {
            get { return _MeterLOCNo2Name; }
            set { _MeterLOCNo2Name = value; }
        }

        /// <summary>
        /// 功能描述：楼栋
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo3
        {
            get { return _MeterLOCNo3; }
            set { _MeterLOCNo3 = value; }
        }

        /// <summary>
        /// 功能描述：楼栋名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo3Name
        {
            get { return _MeterLOCNo3Name; }
            set { _MeterLOCNo3Name = value; }
        }

        /// <summary>
        /// 功能描述：楼层
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo4
        {
            get { return _MeterLOCNo4; }
            set { _MeterLOCNo4 = value; }
        }

        /// <summary>
        /// 功能描述：楼层名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterLOCNo4Name
        {
            get { return _MeterLOCNo4Name; }
            set { _MeterLOCNo4Name = value; }
        }

        /// <summary>
        /// 功能描述：倍率
        /// </summary>
        public decimal MeterRate
        {
            get { return _MeterRate; }
            set { _MeterRate = value; }
        }

        /// <summary>
        /// 功能描述：位数
        /// </summary>
        public int MeterDigit
        {
            get { return _MeterDigit; }
            set { _MeterDigit = value; }
        }

        /// <summary>
        /// 功能描述：使用类别
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string MeterUsageType
        {
            get { return _MeterUsageType; }
            set { _MeterUsageType = value; }
        }

        /// <summary>
        /// 功能描述：使用类别名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterUsageTypeName
        {
            get
            {
                string _MeterUsageTypeName = "";
                switch (_MeterUsageType)
                {
                    case "0":
                        _MeterUsageTypeName = "公用";
                        break;
                    case "1":
                        _MeterUsageTypeName = "家用";
                        break;
                    case "2":
                        _MeterUsageTypeName = "商用";
                        break;
                    case "3":
                        _MeterUsageTypeName = "其他";
                        break;
                }
                return _MeterUsageTypeName;
            }
        }

        /// <summary>
        /// 功能描述：性质类别
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string MeterNatureType
        {
            get { return _MeterNatureType; }
            set { _MeterNatureType = value; }
        }

        /// <summary>
        /// 功能描述：性质类别名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterNatureTypeName
        {
            get
            {
                string _MeterNatureTypeName = "";
                switch (_MeterNatureType)
                {
                    case "1":
                        _MeterNatureTypeName = "居民用电";
                        break;
                    case "2":
                        _MeterNatureTypeName = "工业用电";
                        break;
                    case "3":
                        _MeterNatureTypeName = "商业用电";
                        break;
                    case "4":
                        _MeterNatureTypeName = "其它用电";
                        break;
                }
                return _MeterNatureTypeName;
            }
        }

        /// <summary>
        /// 功能描述：表记读数
        /// </summary>
        public decimal MeterReadout
        {
            get { return _MeterReadout; }
            set { _MeterReadout = value; }
        }

        /// <summary>
        /// 功能描述：表记读数日期
        /// </summary>
        public DateTime MeterReadoutDate
        {
            get { return _MeterReadoutDate; }
            set { _MeterReadoutDate = value; }
        }

        /// <summary>
        /// 功能描述：房间ID
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterRMID
        {
            get { return _MeterRMID; }
            set { _MeterRMID = value; }
        }

        /// <summary>
        /// 功能描述：大小类别
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string MeterSize
        {
            get { return _MeterSize; }
            set { _MeterSize = value; }
        }

        /// <summary>
        /// 功能描述：大小类别名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterSizeName
        {
            get
            {
                string _MeterSizeName = "";
                switch (_MeterSize)
                {
                    case "1":
                        _MeterSizeName = "大表";
                        break;
                    case "2":
                        _MeterSizeName = "小表";
                        break;
                }
                return _MeterSizeName;
            }
        }
        
        /// <summary>
        /// 功能描述：关联表记编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterRelatedMeterNo
        {
            get { return _MeterRelatedMeterNo; }
            set { _MeterRelatedMeterNo = value; }
        }
        
        /// <summary>
        /// 功能描述：位置
        /// 长度：200
        /// 不能为空：否
        /// </summary>
        public string Addr
        {
            get { return _Addr; }
            set { _Addr = value; }
        }
        
        /// <summary>
        /// 功能描述：状态
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string MeterStatus
        {
            get { return _MeterStatus; }
            set { _MeterStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态描述【非维护字段】
        /// </summary>
        public string MeterStatusName
        {
            get
            {
                string _MeterStatusName = "";
                switch (_MeterStatus)
                {
                    case "open":
                        _MeterStatusName = "正常";
                        break;
                    case "close":
                        _MeterStatusName = "停用";
                        break;
                }
                return _MeterStatusName;
            }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime MeterCreateDate
        {
            get { return _MeterCreateDate; }
            set { _MeterCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterCreator
        {
            get { return _MeterCreator; }
            set { _MeterCreator = value; }
        }
    }
}
