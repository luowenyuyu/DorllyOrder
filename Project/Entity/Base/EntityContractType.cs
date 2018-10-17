using System;
namespace project.Entity.Base
{
    /// <summary>合同类型</summary>
    /// <author>tianz</author>
    /// <date>2017-07-13</date>
    [System.Serializable]
    public class EntityContractType
    {
        private string _ContractTypeNo;
        private string _ContractTypeName;


        /// <summary>缺省构造函数</summary>
        public EntityContractType() { }

        /// <summary>合同类型编号【主键】</summary>
        public string ContractTypeNo
        {
            get { return _ContractTypeNo; }
            set { _ContractTypeNo = value; }
        }

        /// <summary>
        /// 功能描述：合同类型名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ContractTypeName
        {
            get { return _ContractTypeName; }
            set { _ContractTypeName = value; }
        }
    }
}
