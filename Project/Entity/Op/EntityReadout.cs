using System;
namespace project.Entity.Op
{
    /// <summary>抄表记录</summary>
    /// <author>tianz</author>
    /// <date>2017-07-14</date>
    [System.Serializable]
    public class EntityReadout
    {
        private string _RowPointer;
        private string _MeterNo;
        private string _RMID;
        private string _RMNo;
        private string _ReadoutType;
        private string _MeterType;
        private decimal _LastReadout;
        private decimal _Readout;
        private decimal _JoinReadings;
        private decimal _Readings;
        private decimal _MeteRate;
        private bool _IsChange;
        private string _CMRP;
        private decimal _OldMeterReadings;
        private string _AuditStatus;
        private string _Auditor;
        private DateTime _AuditDate;
        private string _AuditReason;
        private string _ROOperator;
        private DateTime _RODate;
        private string _ROCreator;
        private DateTime _ROCreateDate;
        private bool _IsOrder;
        public string Img { get; set; }

        /// <summary>缺省构造函数</summary>
        public EntityReadout() { }

        /// <summary>主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：表记编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MeterNo
        {
            get { return _MeterNo; }
            set { _MeterNo = value; }
        }

        /// <summary>
        /// 功能描述：房间编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMID
        {
            get { return _RMID; }
            set { _RMID = value; }
        }

        /// <summary>
        /// 功能描述：房间号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RMNo
        {
            get { return _RMNo; }
            set { _RMNo = value; }
        }

        /// <summary>
        /// 功能描述：抄表类别（1.正常抄表 2.临时抄表）
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ReadoutType
        {
            get { return _ReadoutType; }
            set { _ReadoutType = value; }
        }

        /// <summary>
        /// 功能描述：抄表类别【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ReadoutTypeName
        {
            get
            {
                string _ReadoutTypeName = "";
                switch (_ReadoutType)
                {
                    case "1":
                        _ReadoutTypeName = "正常抄表";
                        break;
                    case "2":
                        _ReadoutTypeName = "临时抄表";
                        break;
                    case "0":
                        _ReadoutTypeName = "租前抄表";
                        break;
                }
                return _ReadoutTypeName;
            }
        }

        /// <summary>
        /// 功能描述：表记类型(wm水表 am电表)
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
        /// 功能描述：上期读数
        /// </summary>
        public decimal LastReadout
        {
            get { return _LastReadout; }
            set { _LastReadout = value; }
        }

        /// <summary>
        /// 功能描述：本期读数
        /// </summary>
        public decimal Readout
        {
            get { return _Readout; }
            set { _Readout = value; }
        }

        /// <summary>
        /// 功能描述：关联表记行度
        /// </summary>
        public decimal JoinReadings
        {
            get { return _JoinReadings; }
            set { _JoinReadings = value; }
        }

        /// <summary>
        /// 功能描述：行度
        /// </summary>
        public decimal Readings
        {
            get { return _Readings; }
            set { _Readings = value; }
        }

        /// <summary>
        /// 功能描述：表记倍率
        /// </summary>
        public decimal MeteRate
        {
            get { return _MeteRate; }
            set { _MeteRate = value; }
        }

        /// <summary>
        /// 功能描述：是否换表
        /// </summary>
        public bool IsChange
        {
            get { return _IsChange; }
            set { _IsChange = value; }
        }

        /// <summary>
        /// 功能描述：换表记录ID
        /// 长度：36
        /// 不能为空：否
        /// </summary>
        public string CMRP
        {
            get { return _CMRP; }
            set { _CMRP = value; }
        }

        /// <summary>
        /// 功能描述：换表记录ID
        /// </summary>
        public decimal OldMeterReadings
        {
            get { return _OldMeterReadings; }
            set { _OldMeterReadings = value; }
        }

        /// <summary>
        /// 功能描述：状态ID
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string AuditStatus
        {
            get { return _AuditStatus; }
            set { _AuditStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string AuditStatusName
        {
            get
            {
                string _AuditStatusName = "";
                switch (_AuditStatus)
                {
                    case "0":
                        _AuditStatusName = "待审核";
                        break;
                    case "1":
                        _AuditStatusName = "审核通过";
                        break;
                    case "-1":
                        _AuditStatusName = "审核不通过";
                        break;
                }
                return _AuditStatusName;
            }
        }

        /// <summary>
        /// 功能描述：审核人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string Auditor
        {
            get { return _Auditor; }
            set { _Auditor = value; }
        }

        /// <summary>
        /// 功能描述：审核日期
        /// </summary>
        public DateTime AuditDate
        {
            get { return _AuditDate; }
            set { _AuditDate = value; }
        }

        /// <summary>
        /// 功能描述：审核不通过原因
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string AuditReason
        {
            get { return _AuditReason; }
            set { _AuditReason = value; }
        }

        /// <summary>
        /// 功能描述：抄表人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ROOperator
        {
            get { return _ROOperator; }
            set { _ROOperator = value; }
        }

        /// <summary>
        /// 功能描述：抄表日期
        /// </summary>
        public DateTime RODate
        {
            get { return _RODate; }
            set { _RODate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ROCreator
        {
            get { return _ROCreator; }
            set { _ROCreator = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime ROCreateDate
        {
            get { return _ROCreateDate; }
            set { _ROCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：是否被引用
        /// </summary>
        public bool IsOrder
        {
            get { return _IsOrder; }
            set { _IsOrder = value; }
        }
    }
}
