using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project.Entity.Op
{
    /// <summary>
    /// 服务申请
    /// </summary>
    public class EntityApplyRefund
    {
        private string _RowPointer;
        private string _ParkNo;
        private string _ARNo;
        private string _SARP;
        private string _PayNo;
        private string _TransactionId;
        private string _CRNo;
        private string _DetailID;
        private string _RefundAmount;
        private string _ARState;
        private string _ApplyDate;
        private string _ApplyUser;
        private string _ApplyCustName;
        private string _ApplyUserName;
        private string _LeaseTime;

        //缺省构造函数
        public EntityApplyRefund() { }

        /// <summary>内部映射主键</summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
        }

        /// <summary>
        /// 功能描述：园区编号
        /// 长度：36
        /// 不能为空：否
        public string ParkNo
        {
            get { return _ParkNo; }
            set { _ParkNo = value; }
        }

        /// <summary>
        /// 功能描述：申请单号
        /// 长度：36
        /// 不能为空：否
        public string ARNo
        {
            get { return _ARNo; }
            set { _ARNo = value; }
        }

        /// <summary>
        /// 功能描述：服务申请外键
        /// 长度：36
        /// 不能为空：否
        public string SARP
        {
            get { return _SARP; }
            set { _SARP = value; }
        }

        /// <summary>
        /// 功能描述：支付单号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string PayNo
        {
            get { return _PayNo; }
            set { _PayNo = value; }
        }

        /// <summary>
        /// 功能描述：微信支付单号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string TransactionId
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        /// <summary>
        /// 功能描述：会议室编号
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRNo
        {
            get { return _CRNo; }
            set { _CRNo = value; }
        }

        /// <summary>
        /// 功能描述：租用明细外键
        /// 长度：36
        /// 不能为空：否
        /// </summary>
        public string DetailID
        {
            get { return _DetailID; }
            set { _DetailID = value; }
        }

        /// <summary>
        /// 功能描述：退租金额
        /// </summary>
        public string RefundAmount
        {
            get { return _RefundAmount; }
            set { _RefundAmount = value; }
        }

        /// <summary>
        /// 功能描述：退租状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ARState
        {
            get { return _ARState; }
            set { _ARState = value; }
        }

        /// <summary>
        /// 功能描述：申请日期
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ApplyDate
        {
            get { return _ApplyDate; }
            set { _ApplyDate = value; }
        }

        /// <summary>
        /// 功能描述：申请人
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ApplyUser
        {
            get { return _ApplyUser; }
            set { _ApplyUser = value; }
        }

        /// <summary>
        /// 功能描述：申请客户
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ApplyCustName
        {
            get { return _ApplyCustName; }
            set { _ApplyCustName = value; }
        }

        /// <summary>
        /// 功能描述：申请人姓名
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ApplyUserName
        {
            get { return _ApplyUserName; }
            set { _ApplyUserName = value; }
        }

        /// <summary>
        /// 功能描述：租赁时间
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string LeaseTime
        {
            get { return _LeaseTime; }
            set { _LeaseTime = value; }
        }
    }
}
