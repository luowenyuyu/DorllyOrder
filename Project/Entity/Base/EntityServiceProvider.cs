using System;
namespace project.Entity.Base
{
    /// <summary>服务商资料</summary>
    /// <author>tianz</author>
    /// <date>2017-06-22</date>
    [System.Serializable]
    public class EntityServiceProvider
    {
        private string _SPNo;
        private string _SPName;
        private string _SPShortName;
        private string _MService;
        private string _MServiceName;
        private string _SPLicenseNo;
        private string _SPContact;
        private string _SPContactMobile;
        private string _SPTel;
        private string _SPEMail;
        private string _SPAddr;
        private string _SPBank;
        private string _SPBankAccount;
        private string _SPBankTitle;
        private bool _SPStatus;
        private string _U8Account;
        private string _BankAccount;
        private string _CashAccount;
        private string _TaxAccount;
        
        /// <summary>缺省构造函数</summary>
        public EntityServiceProvider() { }
        
        /// <summary>服务商编号【主键】</summary>
        public string SPNo
        {
            get { return _SPNo; }
            set { _SPNo = value; }
        }

        /// <summary>
        /// 功能描述：服务商名称
        /// 长度：80
        /// 不能为空：否
        /// </summary>
        public string SPName
        {
            get { return _SPName; }
            set { _SPName = value; }
        }

        /// <summary>
        /// 功能描述：服务商简称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SPShortName
        {
            get { return _SPShortName; }
            set { _SPShortName = value; }
        }

        /// <summary>
        /// 功能描述：主营业务
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MService
        {
            get { return _MService; }
            set { _MService = value; }
        }

        /// <summary>
        /// 功能描述：主营业务名称【非维护字段】
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string MServiceName
        {
            get { return _MServiceName; }
            set { _MServiceName = value; }
        }

        /// <summary>
        /// 功能描述：营业执照
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPLicenseNo
        {
            get { return _SPLicenseNo; }
            set { _SPLicenseNo = value; }
        }

        /// <summary>
        /// 功能描述：联系人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPContact
        {
            get { return _SPContact; }
            set { _SPContact = value; }
        }

        /// <summary>
        /// 功能描述：手机
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPContactMobile
        {
            get { return _SPContactMobile; }
            set { _SPContactMobile = value; }
        }

        /// <summary>
        /// 功能描述：固定电话
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPTel
        {
            get { return _SPTel; }
            set { _SPTel = value; }
        }

        /// <summary>
        /// 功能描述：电子邮箱
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPEMail
        {
            get { return _SPEMail; }
            set { _SPEMail = value; }
        }
        
        /// <summary>
        /// 功能描述：地址
        /// 长度：300
        /// 不能为空：否
        /// </summary>
        public string SPAddr
        {
            get { return _SPAddr; }
            set { _SPAddr = value; }
        }

        /// <summary>
        /// 功能描述：开户行
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPBank
        {
            get { return _SPBank; }
            set { _SPBank = value; }
        }

        /// <summary>
        /// 功能描述：开户账户
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPBankAccount
        {
            get { return _SPBankAccount; }
            set { _SPBankAccount = value; }
        }

        /// <summary>
        /// 功能描述：收款人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SPBankTitle
        {
            get { return _SPBankTitle; }
            set { _SPBankTitle = value; }
        }   

        /// <summary>
        /// 功能描述：是否有效
        /// 不能为空：否
        /// </summary>
        public bool SPStatus
        {
            get { return _SPStatus; }
            set { _SPStatus = value; }
        }   

        /// <summary>
        /// 功能描述：U8账套编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string U8Account
        {
            get { return _U8Account; }
            set { _U8Account = value; }
        }     

        /// <summary>
        /// 功能描述：U8银行账套编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BankAccount
        {
            get { return _BankAccount; }
            set { _BankAccount = value; }
        }  

        /// <summary>
        /// 功能描述：U8现金科目编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CashAccount
        {
            get { return _CashAccount; }
            set { _CashAccount = value; }
        } 

        /// <summary>
        /// 功能描述：U8税额科目编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string TaxAccount
        {
            get { return _TaxAccount; }
            set { _TaxAccount = value; }
        } 
    }
}
