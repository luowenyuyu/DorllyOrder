using System;
namespace project.Entity.Base
{
    /// <summary>系统参数配置</summary>
    /// <author>tianz</author>
    /// <date>2017-07-27</date>
    [System.Serializable]
    public class EntitySetting
    {
        private string _SettingCode;
        private string _SettingName;
        private string _SettingType;
        private string _stringValue;
        private int _IntValue;
        private decimal _DecimalValue;
        private string _OrderNo;
        private string _SRVNo;
        private string _SRVName;

        /// <summary>缺省构造函数</summary>
        public EntitySetting() { }

        /// <summary>参数编号【主键】</summary>
        public string SettingCode
        {
            get { return _SettingCode; }
            set { _SettingCode = value; }
        }

        /// <summary>
        /// 功能描述：参数名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SettingName
        {
            get { return _SettingName; }
            set { _SettingName = value; }
        }

        /// <summary>
        /// 功能描述：参数类型
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string SettingType
        {
            get { return _SettingType; }
            set { _SettingType = value; }
        }

        /// <summary>
        /// 功能描述：字符串
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string StringValue
        {
            get { return _stringValue; }
            set { _stringValue = value; }
        }

        /// <summary>
        /// 功能描述：整型
        /// </summary>
        public int IntValue
        {
            get { return _IntValue; }
            set { _IntValue = value; }
        }

        /// <summary>
        /// 功能描述：浮点型
        /// </summary>
        public decimal DecimalValue
        {
            get { return _DecimalValue; }
            set { _DecimalValue = value; }
        }

        /// <summary>
        /// 功能描述：排序字段
        /// 长度：10
        /// 不能为空：否
        /// </summary>
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        /// <summary>
        /// 功能描述：服务项目
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string SRVNo
        {
            get { return _SRVNo; }
            set { _SRVNo = value; }
        }

        /// <summary>
        /// 功能描述：服务项目名称【非维护字段】
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string SRVName
        {
            get { return _SRVName; }
            set { _SRVName = value; }
        }
    }
}
