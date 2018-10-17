using System;
namespace project.Entity.Sys
{
    /// <summary>用户信息</summary>
    /// <author>tianz</author>
    /// <date>2017-05-19</date>
    [System.Serializable]
    public class EntityUserInfo
    {
        private string _entityOID;
        private string _userType;
        private string _userNo;
        private string _userName;
        private string _password;
        private string _tel;
        private string _email;
        private string _addr;
        private System.DateTime _regDate;
        private bool _valid;

        /// <summary>缺省构造函数</summary>
        public EntityUserInfo() {}
        
        /// <summary>内部映射主键</summary>
        public string InnerEntityOID
        {
            get { return _entityOID; }
            set { _entityOID = value; }
        }

        /// <summary>
        /// 功能描述：用户类型
        /// 长度：20
        /// 不能为空：否
        /// </summary>
        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }

        /// <summary>
        /// 功能描述：用户类别名称
        /// 长度：100
        /// 不能为空：否
        /// </summary>
        public string UserTypeName
        {
            get
            {
                if (_userType != "" && _userType != null)
                {
                    try
                    {
                        Business.Sys.BusinessUserType usertype = new Business.Sys.BusinessUserType();
                        usertype.load(_userType);
                        return usertype.Entity.UserTypeName;
                    }
                    catch { return ""; }
                }
                return "";
            }
        }

        /// <summary>
        /// 功能描述：用户编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string UserNo
        {
            get { return _userNo; }
            set { _userNo = value; }
        }

        /// <summary>
        /// 功能描述：用户名称
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        /// <summary>
        /// 功能描述：密码
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// 功能描述：电话
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string Tel
        {
            get { return _tel; }
            set { _tel = value; }
        }

        /// <summary>
        /// 功能描述：邮箱
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// 功能描述：地址
        /// 长度：200
        /// 不能为空：否
        /// </summary>
        public string Addr
        {
            get { return _addr; }
            set { _addr = value; }
        }

        /// <summary>
        /// 功能描述：注册日期
        /// 不能为空：否
        /// </summary>
        public System.DateTime RegDate
        {
            get { return _regDate; }
            set { _regDate = value; }
        }

        /// <summary>
        /// 功能描述：是否有效
        /// 不能为空：否
        /// </summary>
        public bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }

    }
}