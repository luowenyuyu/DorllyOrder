using System;
namespace project.Entity.Base
{
    /// <summary>客户资料</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityCustomer
    {
        private string _CustNo;
        private string _CustName;
        private string _CustShortName;
        private string _CustType;
        private string _Representative;
        private string _BusinessScope;
        private string _CustLicenseNo;
        private string _RepIDCard;
        private string _CustContact;
        private string _CustTel;
        private string _CustContactMobile;
        private string _CustEmail;
        private string _CustBankTitle;
        private string _CustBankAccount;
        private string _CustBank;
        private string _CustStatus;
        private DateTime _CustCreateDate;
        private string _CustCreator;
        private bool _IsExternal;

        /// <summary>缺省构造函数</summary>
        public EntityCustomer() { }

        /// <summary>客户编号【主键】</summary>
        public string CustNo
        {
            get { return _CustNo; }
            set { _CustNo = value; }
        }

        /// <summary>
        /// 功能描述：客户名称
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }

        /// <summary>
        /// 功能描述：客户简称
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string CustShortName
        {
            get { return _CustShortName; }
            set { _CustShortName = value; }
        }

        /// <summary>
        /// 功能描述：客户类别
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustType
        {
            get { return _CustType; }
            set { _CustType = value; }
        }

        /// <summary>
        /// 功能描述：单位法人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string Representative
        {
            get { return _Representative; }
            set { _Representative = value; }
        }

        /// <summary>
        /// 功能描述：企业经营范围
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BusinessScope
        {
            get { return _BusinessScope; }
            set { _BusinessScope = value; }
        }

        /// <summary>
        /// 功能描述：营业执照编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustLicenseNo
        {
            get { return _CustLicenseNo; }
            set { _CustLicenseNo = value; }
        }

        /// <summary>
        /// 功能描述：法人身份证号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RepIDCard
        {
            get { return _RepIDCard; }
            set { _RepIDCard = value; }
        }

        /// <summary>
        /// 功能描述：客户联系人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustContact
        {
            get { return _CustContact; }
            set { _CustContact = value; }
        }

        /// <summary>
        /// 功能描述：固定电话
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustTel
        {
            get { return _CustTel; }
            set { _CustTel = value; }
        }

        /// <summary>
        /// 功能描述：联系人手机号码
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustContactMobile
        {
            get { return _CustContactMobile; }
            set { _CustContactMobile = value; }
        }

        /// <summary>
        /// 功能描述：电子邮箱
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustEmail
        {
            get { return _CustEmail; }
            set { _CustEmail = value; }
        }

        /// <summary>
        /// 功能描述：收款人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustBankTitle
        {
            get { return _CustBankTitle; }
            set { _CustBankTitle = value; }
        }

        /// <summary>
        /// 功能描述：收款账号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustBankAccount
        {
            get { return _CustBankAccount; }
            set { _CustBankAccount = value; }
        }

        /// <summary>
        /// 功能描述：开户行
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustBank
        {
            get { return _CustBank; }
            set { _CustBank = value; }
        }

        /// <summary>
        /// 功能描述：客户状态
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string CustStatus
        {
            get { return _CustStatus; }
            set { _CustStatus = value; }
        }

        /// <summary>
        /// 功能描述：客户状态【非维护字段】
        /// </summary>
        public string CustStatusName
        {
            get
            {
                string _CustStatusName = "";
                switch (_CustStatus)
                {
                    case "1":
                        _CustStatusName = "在租";
                        break;
                    case "2":
                        _CustStatusName = "退租";
                        break;
                    case "3":
                        _CustStatusName = "未租";
                        break;
                }
                return _CustStatusName;
            }
        }

        /// <summary>
        /// 功能描述：创建日期
        /// </summary>
        public DateTime CustCreateDate
        {
            get { return _CustCreateDate; }
            set { _CustCreateDate = value; }
        }

        /// <summary>
        /// 功能描述：创建人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CustCreator
        {
            get { return _CustCreator; }
            set { _CustCreator = value; }
        }

        /// <summary>
        /// 功能描述：是否外部客户
        /// </summary>
        public bool IsExternal
        {
            get { return _IsExternal; }
            set { _IsExternal = value; }
        }
    }
}
