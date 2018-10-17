using System;
namespace project.Entity.Op
{
    /// <summary>换表记录</summary>
    /// <author>tianz</author>
    /// <date>2017-07-14</date>
    [System.Serializable]
    public class EntityChangeMeter
    {
        private string _RowPointer;
        private string _RMID;
        private string _OldMeterNo;
        private string _OldMeterType;
        private decimal _OldMeterLastReadout;
        private decimal _OldMeterReadout;
        private decimal _OldMeterReadings;
        private string _NewMeterNo;
        private string _NewMeterName;
        private string _NewMeterSize;
        private decimal _NewMeterReadout;
        private decimal _NewMeterRate;
        private int _NewMeterDigit;
        private string _CMOperator;
        private DateTime _CMDate;
        private string _CMCreator;
        private DateTime _CMCreateDate;
        private string _AuditStatus;
        private string _Auditor;
        private DateTime _AuditDate;

        /// <summary>缺省构造函数</summary>
        public EntityChangeMeter() { }

        /// <summary>主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
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
        /// 功能描述：旧表编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OldMeterNo
        {
            get { return _OldMeterNo; }
            set { _OldMeterNo = value; }
        }

        /// <summary>
        /// 功能描述：旧表类型（wm水表，am电表）
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OldMeterType
        {
            get { return _OldMeterType; }
            set { _OldMeterType = value; }
        }

        /// <summary>
        /// 功能描述：表记类型名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string OldMeterTypeName
        {
            get
            {
                string _OldMeterTypeName = "";
                switch (_OldMeterType)
                {
                    case "wm":
                        _OldMeterTypeName = "水表";
                        break;
                    case "am":
                        _OldMeterTypeName = "电表";
                        break;
                }
                return _OldMeterTypeName;
            }
        }

        /// <summary>
        /// 功能描述：旧表上期读数
        /// </summary>
        public decimal OldMeterLastReadout
        {
            get { return _OldMeterLastReadout; }
            set { _OldMeterLastReadout = value; }
        }

        /// <summary>
        /// 功能描述：旧表换表止度
        /// </summary>
        public decimal OldMeterReadout
        {
            get { return _OldMeterReadout; }
            set { _OldMeterReadout = value; }
        }

        /// <summary>
        /// 功能描述：旧表行度
        /// </summary>
        public decimal OldMeterReadings
        {
            get { return _OldMeterReadings; }
            set { _OldMeterReadings = value; }
        }

        /// <summary>
        /// 功能描述：新表记编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string NewMeterNo
        {
            get { return _NewMeterNo; }
            set { _NewMeterNo = value; }
        }

        /// <summary>
        /// 功能描述：新表记名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string NewMeterName
        {
            get { return _NewMeterName; }
            set { _NewMeterName = value; }
        }

        /// <summary>
        /// 功能描述：新表记大小类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string NewMeterSize
        {
            get { return _NewMeterSize; }
            set { _NewMeterSize = value; }
        }

        /// <summary>
        /// 功能描述：新表起度
        /// </summary>
        public decimal NewMeterReadout
        {
            get { return _NewMeterReadout; }
            set { _NewMeterReadout = value; }
        }

        /// <summary>
        /// 功能描述：表记倍率
        /// </summary>
        public decimal NewMeterRate
        {
            get { return _NewMeterRate; }
            set { _NewMeterRate = value; }
        }

        /// <summary>
        /// 功能描述：表记倍率
        /// </summary>
        public int NewMeterDigit
        {
            get { return _NewMeterDigit; }
            set { _NewMeterDigit = value; }
        }

        /// <summary>
        /// 功能描述：操作人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CMOperator
        {
            get { return _CMOperator; }
            set { _CMOperator = value; }
        }

        /// <summary>
        /// 功能描述：操作日期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime CMDate
        {
            get { return _CMDate; }
            set { _CMDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CMCreator
        {
            get { return _CMCreator; }
            set { _CMCreator = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime CMCreateDate
        {
            get { return _CMCreateDate; }
            set { _CMCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：状态
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
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime AuditDate
        {
            get { return _AuditDate; }
            set { _AuditDate = value; }
        }
    }
}
