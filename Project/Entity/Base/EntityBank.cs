using System;
namespace project.Entity.Base
{
    /// <summary>银行资料</summary>
    /// <author>tianz</author>
    /// <date>2017-10-18</date>
    [System.Serializable]
    public class EntityBank
    {
        private string _BankNo;
        private string _BankName;
        private string _BankAccount;
        private bool _Valid;
        
        /// <summary>缺省构造函数</summary>
        public EntityBank() { }

        /// <summary>银行编号【主键】</summary>
        public string BankNo
        {
            get { return _BankNo; }
            set { _BankNo = value; }
        }

        /// <summary>
        /// 功能描述：银行名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string BankName
        {
            get { return _BankName; }
            set { _BankName = value; }
        }

        /// <summary>
        /// 功能描述：银行科目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string BankAccount
        {
            get { return _BankAccount; }
            set { _BankAccount = value; }
        }

        /// <summary>
        /// 功能描述：是否可用
        /// </summary>
        public bool Valid
        {
            get { return _Valid; }
            set { _Valid = value; }
        }
    }
}
