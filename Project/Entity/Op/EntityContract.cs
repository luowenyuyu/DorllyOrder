using System;
namespace project.Entity.Op
{
    /// <summary>合同资料</summary>
    /// <author>tianz</author>
    /// <date>2017-07-19</date>
    [System.Serializable]
    public class EntityContract
    {
        private string _RowPointer;
        private string _ContractNo;
        private string _ContractType;
        private string _ContractTypeName;
        private string _ContractSPNo;
        private string _ContractSPName;
        private string _ContractCustNo;
        private string _ContractCustName;
        private string _ContractNoManual;
        private string _ContractHandler;
        private DateTime _ContractSignedDate;
        private DateTime _ContractStartDate;
        private DateTime _ContractEndDate;
        private DateTime _EntryDate;
        private DateTime _FeeStartDate;
        private DateTime _ReduceStartDate1;
        private DateTime _ReduceEndDate1;
        private DateTime _ReduceStartDate2;
        private DateTime _ReduceEndDate2;
        private DateTime _ReduceStartDate3;
        private DateTime _ReduceEndDate3;
        private DateTime _ReduceStartDate4;
        private DateTime _ReduceEndDate4;
        private decimal _ContractLatefeeRate;
        private decimal _RMRentalDeposit;
        private decimal _RMUtilityDeposit;
        private DateTime _PropertyFeeStartDate;
        private DateTime _PropertyFeeReduceStartDate;
        private DateTime _PropertyFeeReduceEndDate;
        private decimal _WaterUnitPrice;
        private decimal _ElecticityUintPrice;
        private decimal _AirconUnitPrice;
        private decimal _PropertyUnitPrice;
        private decimal _SharedWaterFee;
        private decimal _SharedElectricyFee;
        private decimal _WPRentalDeposit;
        private decimal _WPUtilityDeposit;
        private int _WPQTY;
        private decimal _WPElectricyLimit;
        private decimal _WPOverElectricyPrice;
        private int _BBQTY;
        private decimal _BBAmount;
        private string _IncreaseType;
        private DateTime _IncreaseStartDate1;
        private decimal _IncreaseRate1;
        private DateTime _IncreaseStartDate2;
        private decimal _IncreaseRate2;
        private DateTime _IncreaseStartDate3;
        private decimal _IncreaseRate3;
        private DateTime _IncreaseStartDate4;
        private decimal _IncreaseRate4;
        private string _OffLeaseStatus;
        private string _Remark;
        private DateTime _OffLeaseApplyDate;
        private DateTime _OffLeaseScheduleDate;
        private DateTime _OffLeaseActulDate;
        private string _OffLeaseReason;
        private string _ContractCreator;
        private DateTime _ContractCreateDate;
        private string _ContractLastReviser;
        private DateTime _ContractLastReviseDate;
        private string _ContractAttachment;
        private string _ContractStatus;
        private string _ContractAuditor;
        private DateTime _ContractAuditDate;
        private string _ContractFinanceAuditor;
        private DateTime _ContractFinanceAuditDate;


        /// <summary>缺省构造函数</summary>
        public EntityContract() { }

        /// <summary>主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：合同编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractNo
        {
            get { return _ContractNo; }
            set { _ContractNo = value; }
        }

        /// <summary>
        /// 功能描述：合同类型
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractType
        {
            get { return _ContractType; }
            set { _ContractType = value; }
        }

        /// <summary>
        /// 功能描述：合同类型名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractTypeName
        {
            get { return _ContractTypeName; }
            set { _ContractTypeName = value; }
        }

        /// <summary>
        /// 功能描述：甲方（服务商）
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractSPNo
        {
            get { return _ContractSPNo; }
            set { _ContractSPNo = value; }
        }

        /// <summary>
        /// 功能描述：甲方名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractSPName
        {
            get { return _ContractSPName; }
            set { _ContractSPName = value; }
        }

        /// <summary>
        /// 功能描述：乙方（客户）
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractCustNo
        {
            get { return _ContractCustNo; }
            set { _ContractCustNo = value; }
        }

        /// <summary>
        /// 功能描述：乙方名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractCustName
        {
            get { return _ContractCustName; }
            set { _ContractCustName = value; }
        }

        /// <summary>
        /// 功能描述：手工合同编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractNoManual
        {
            get { return _ContractNoManual; }
            set { _ContractNoManual = value; }
        }

        /// <summary>
        /// 功能描述：经办人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractHandler
        {
            get { return _ContractHandler; }
            set { _ContractHandler = value; }
        }

        /// <summary>
        /// 功能描述：合同签订日期
        /// </summary>
        public DateTime ContractSignedDate
        {
            get { return _ContractSignedDate; }
            set { _ContractSignedDate = value; }
        }

        /// <summary>
        /// 功能描述：合同生效日期
        /// </summary>
        public DateTime ContractStartDate
        {
            get { return _ContractStartDate; }
            set { _ContractStartDate = value; }
        }

        /// <summary>
        /// 功能描述：合同到期日期
        /// </summary>
        public DateTime ContractEndDate
        {
            get { return _ContractEndDate; }
            set { _ContractEndDate = value; }
        }

        /// <summary>
        /// 功能描述：客户入场日期
        /// </summary>
        public DateTime EntryDate
        {
            get { return _EntryDate; }
            set { _EntryDate = value; }
        }

        /// <summary>
        /// 功能描述：租金起收日期
        /// </summary>
        public DateTime FeeStartDate
        {
            get { return _FeeStartDate; }
            set { _FeeStartDate = value; }
        }

        /// <summary>
        /// 功能描述：租金减免开始日期1
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime ReduceStartDate1
        {
            get { return _ReduceStartDate1; }
            set { _ReduceStartDate1 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免结束日期1
        /// </summary>
        public DateTime ReduceEndDate1
        {
            get { return _ReduceEndDate1; }
            set { _ReduceEndDate1 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免开始日期2
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime ReduceStartDate2
        {
            get { return _ReduceStartDate2; }
            set { _ReduceStartDate2 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免结束日期2
        /// </summary>
        public DateTime ReduceEndDate2
        {
            get { return _ReduceEndDate2; }
            set { _ReduceEndDate2 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免开始日期3
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime ReduceStartDate3
        {
            get { return _ReduceStartDate3; }
            set { _ReduceStartDate3 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免结束日期3
        /// </summary>
        public DateTime ReduceEndDate3
        {
            get { return _ReduceEndDate3; }
            set { _ReduceEndDate3 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免开始日期4
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime ReduceStartDate4
        {
            get { return _ReduceStartDate4; }
            set { _ReduceStartDate4 = value; }
        }

        /// <summary>
        /// 功能描述：租金减免结束日期4
        /// </summary>
        public DateTime ReduceEndDate4
        {
            get { return _ReduceEndDate4; }
            set { _ReduceEndDate4 = value; }
        }

        /// <summary>
        /// 功能描述：滞纳金占比
        /// </summary>
        public decimal ContractLatefeeRate
        {
            get { return _ContractLatefeeRate; }
            set { _ContractLatefeeRate = value; }
        }

        /// <summary>
        /// 功能描述：房屋租赁押金
        /// </summary>
        public decimal RMRentalDeposit
        {
            get { return _RMRentalDeposit; }
            set { _RMRentalDeposit = value; }
        }

        /// <summary>
        /// 功能描述：房屋水电押金
        /// </summary>
        public decimal RMUtilityDeposit
        {
            get { return _RMUtilityDeposit; }
            set { _RMUtilityDeposit = value; }
        }

        /// <summary>
        /// 功能描述：管理费起收日期
        /// </summary>
        public DateTime PropertyFeeStartDate
        {
            get { return _PropertyFeeStartDate; }
            set { _PropertyFeeStartDate = value; }
        }

        /// <summary>
        /// 功能描述：管理费减免开始日期
        /// </summary>
        public DateTime PropertyFeeReduceStartDate
        {
            get { return _PropertyFeeReduceStartDate; }
            set { _PropertyFeeReduceStartDate = value; }
        }

        /// <summary>
        /// 功能描述：管理费减免结束日期
        /// </summary>
        public DateTime PropertyFeeReduceEndDate
        {
            get { return _PropertyFeeReduceEndDate; }
            set { _PropertyFeeReduceEndDate = value; }
        }

        /// <summary>
        /// 功能描述：水费单价
        /// </summary>
        public decimal WaterUnitPrice
        {
            get { return _WaterUnitPrice; }
            set { _WaterUnitPrice = value; }
        }

        /// <summary>
        /// 功能描述：电费单价
        /// </summary>
        public decimal ElecticityUintPrice
        {
            get { return _ElecticityUintPrice; }
            set { _ElecticityUintPrice = value; }
        }

        /// <summary>
        /// 功能描述：空调费单价
        /// </summary>
        public decimal AirconUnitPrice
        {
            get { return _AirconUnitPrice; }
            set { _AirconUnitPrice = value; }
        }

        /// <summary>
        /// 功能描述：管理费单价
        /// </summary>
        public decimal PropertyUnitPrice
        {
            get { return _PropertyUnitPrice; }
            set { _PropertyUnitPrice = value; }
        }

        /// <summary>
        /// 功能描述：公摊水费
        /// </summary>
        public decimal SharedWaterFee
        {
            get { return _SharedWaterFee; }
            set { _SharedWaterFee = value; }
        }

        /// <summary>
        /// 功能描述：公摊电费
        /// </summary>
        public decimal SharedElectricyFee
        {
            get { return _SharedElectricyFee; }
            set { _SharedElectricyFee = value; }
        }

        /// <summary>
        /// 功能描述：工位租赁押金
        /// </summary>
        public decimal WPRentalDeposit
        {
            get { return _WPRentalDeposit; }
            set { _WPRentalDeposit = value; }
        }

        /// <summary>
        /// 功能描述：工位电费押金
        /// </summary>
        public decimal WPUtilityDeposit
        {
            get { return _WPUtilityDeposit; }
            set { _WPUtilityDeposit = value; }
        }

        /// <summary>
        /// 功能描述：工位数量
        /// </summary>
        public int WPQTY
        {
            get { return _WPQTY; }
            set { _WPQTY = value; }
        }

        /// <summary>
        /// 功能描述：工位用电额度
        /// </summary>
        public decimal WPElectricyLimit
        {
            get { return _WPElectricyLimit; }
            set { _WPElectricyLimit = value; }
        }

        /// <summary>
        /// 功能描述：超额用电单价
        /// </summary>
        public decimal WPOverElectricyPrice
        {
            get { return _WPOverElectricyPrice; }
            set { _WPOverElectricyPrice = value; }
        }

        /// <summary>
        /// 功能描述：广告位位数量
        /// </summary>
        public int BBQTY
        {
            get { return _BBQTY; }
            set { _BBQTY = value; }
        }

        /// <summary>
        /// 功能描述：广告位合同金额
        /// </summary>
        public decimal BBAmount
        {
            get { return _BBAmount; }
            set { _BBAmount = value; }
        }

        /// <summary>
        /// 功能描述：递增类型（1.按递增率递增（默认），2按固定金额递增）
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string IncreaseType
        {
            get { return _IncreaseType; }
            set { _IncreaseType = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间1
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate1
        {
            get { return _IncreaseStartDate1; }
            set { _IncreaseStartDate1 = value; }
        }

        /// <summary>
        /// 功能描述：递增率1
        /// </summary>
        public decimal IncreaseRate1
        {
            get { return _IncreaseRate1; }
            set { _IncreaseRate1 = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间2
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate2
        {
            get { return _IncreaseStartDate2; }
            set { _IncreaseStartDate2 = value; }
        }

        /// <summary>
        /// 功能描述：递增率2
        /// </summary>
        public decimal IncreaseRate2
        {
            get { return _IncreaseRate2; }
            set { _IncreaseRate2 = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间3
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate3
        {
            get { return _IncreaseStartDate3; }
            set { _IncreaseStartDate3 = value; }
        }

        /// <summary>
        /// 功能描述：递增率3
        /// </summary>
        public decimal IncreaseRate3
        {
            get { return _IncreaseRate3; }
            set { _IncreaseRate3 = value; }
        }

        /// <summary>
        /// 功能描述：递增开始时间4
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public DateTime IncreaseStartDate4
        {
            get { return _IncreaseStartDate4; }
            set { _IncreaseStartDate4 = value; }
        }

        /// <summary>
        /// 功能描述：递增率4
        /// </summary>
        public decimal IncreaseRate4
        {
            get { return _IncreaseRate4; }
            set { _IncreaseRate4 = value; }
        }

        /// <summary>
        /// 功能描述：备注
        /// 长度：500
        /// 不能为空：否
        /// </summary>
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        /// <summary>
        /// 功能描述：退租状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string OffLeaseStatus
        {
            get { return _OffLeaseStatus; }
            set { _OffLeaseStatus = value; }
        }

        /// <summary>
        /// 功能描述：退租状态描述【非维护字段】
        /// </summary>
        public string OffLeaseStatusName
        {
            get
            {
                string _OffLeaseStatusName = "";
                switch (_OffLeaseStatus)
                {
                    case "1":
                        _OffLeaseStatusName = "未退租";
                        break;
                    case "2":
                        _OffLeaseStatusName = "已申请";
                        break;
                    case "3":
                        _OffLeaseStatusName = "已办理";
                        break;
                    case "4":
                        _OffLeaseStatusName = "已结算";
                        break;
                }
                return _OffLeaseStatusName;
            }
        }

        /// <summary>
        /// 功能描述：申请退租日期
        /// </summary>
        public DateTime OffLeaseApplyDate
        {
            get { return _OffLeaseApplyDate; }
            set { _OffLeaseApplyDate = value; }
        }

        /// <summary>
        /// 功能描述：预约退租日期
        /// </summary>
        public DateTime OffLeaseScheduleDate
        {
            get { return _OffLeaseScheduleDate; }
            set { _OffLeaseScheduleDate = value; }
        }

        /// <summary>
        /// 功能描述：实际退租日期
        /// </summary>
        public DateTime OffLeaseActulDate
        {
            get { return _OffLeaseActulDate; }
            set { _OffLeaseActulDate = value; }
        }

        /// <summary>
        /// 功能描述：退租原因
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string OffLeaseReason
        {
            get { return _OffLeaseReason; }
            set { _OffLeaseReason = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractCreator
        {
            get { return _ContractCreator; }
            set { _ContractCreator = value; }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime ContractCreateDate
        {
            get { return _ContractCreateDate; }
            set { _ContractCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：最后修改人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractLastReviser
        {
            get { return _ContractLastReviser; }
            set { _ContractLastReviser = value; }
        }

        /// <summary>
        /// 功能描述：最后修改日期
        /// </summary>
        public DateTime ContractLastReviseDate
        {
            get { return _ContractLastReviseDate; }
            set { _ContractLastReviseDate = value; }
        }

        /// <summary>
        /// 功能描述：附件
        /// 长度：200
        /// 不能为空：否
        /// </summary>
        public string ContractAttachment
        {
            get { return _ContractAttachment; }
            set { _ContractAttachment = value; }
        }

        /// <summary>
        /// 功能描述：状态(1制单,2已审核,3作废)
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string ContractStatus
        {
            get { return _ContractStatus; }
            set { _ContractStatus = value; }
        }

        /// <summary>
        /// 功能描述：状态描述【非维护字段】
        /// </summary>
        public string ContractStatusName
        {
            get
            {
                string _ContractStatusName = "";
                switch (_ContractStatus)
                {
                    case "1":
                        _ContractStatusName = "制单";
                        break;
                    case "2":
                        _ContractStatusName = "已审核";
                        break;
                    case "3":
                        _ContractStatusName = "已退租";
                        break;
                    case "4":
                        _ContractStatusName = "已作废";
                        break;
                }
                return _ContractStatusName;
            }
        }

        /// <summary>
        /// 功能描述：审核人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractAuditor
        {
            get { return _ContractAuditor; }
            set { _ContractAuditor = value; }
        }

        /// <summary>
        /// 功能描述：审核日期
        /// </summary>
        public DateTime ContractAuditDate
        {
            get { return _ContractAuditDate; }
            set { _ContractAuditDate = value; }
        }

        /// <summary>
        /// 功能描述：财务审核人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ContractFinanceAuditor
        {
            get { return _ContractFinanceAuditor; }
            set { _ContractFinanceAuditor = value; }
        }

        /// <summary>
        /// 功能描述：财务审核日期
        /// </summary>
        public DateTime ContractFinanceAuditDate
        {
            get { return _ContractFinanceAuditDate; }
            set { _ContractFinanceAuditDate = value; }
        }
    }
}
