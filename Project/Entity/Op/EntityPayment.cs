using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace project.Entity.Op
{
    /// <summary>
    /// 服务申请
    /// </summary>
    public class EntityPayment
    {
        private string _RowPointer;
        private string _PayNo;
        private string _TransactionId;
        private string _ParkNo;
        private string _PayType;
        private string _RefNo;
        private string _CRName;
        private string _Amount;
        private string _RentAmount;
        private string _Disposit;
        private string _RefundAmount;
        private string _ReservedTime;
        private string _ApplyCustName;
        private string _ApplyUserName;
        private string _ARState;        

        //缺省构造函数
        public EntityPayment() { }

        /// <summary>
        /// 功能描述：退款主键
        /// 长度：36
        /// 不能为空：否
        /// </summary>
        public string RowPointer
        {
            get { return _RowPointer; }
            set { _RowPointer = value; }
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
        /// 功能描述：园区编号
        /// 长度：36
        /// 不能为空：否
        public string ParkNo
        {
            get { return _ParkNo; }
            set { _ParkNo = value; }
        }

        /// <summary>
        /// 功能描述：支付类型（CR.会议室 RP.企业维修）
        /// 长度：36
        /// 不能为空：否
        public string PayType
        {
            get { return _PayType; }
            set { _PayType = value; }
        }

        /// <summary>
        /// 功能描述：会议室编号
        /// 长度：36
        /// 不能为空：否
        public string RefNo
        {
            get { return _RefNo; }
            set { _RefNo = value; }
        }

        /// <summary>
        /// 功能描述：会议室名称
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string CRName
        {
            get { return _CRName; }
            set { _CRName = value; }
        }

        /// <summary>
        /// 功能描述：支付金额
        /// </summary>
        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        /// <summary>
        /// 功能描述：租金
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RentAmount
        {
            get { return _RentAmount; }
            set { _RentAmount = value; }
        }

        /// <summary>
        /// 功能描述：押金
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string Disposit
        {
            get { return _Disposit; }
            set { _Disposit = value; }
        }

        /// <summary>
        /// 功能描述：申请退款金额
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string RefundAmount
        {
            get { return _RefundAmount; }
            set { _RefundAmount = value; }
        }

        /// <summary>
        /// 功能描述：租赁时间说明
        /// 长度：50
        /// 不能为空：否
        /// </summary>
        public string ReservedTime
        {
            get { return _ReservedTime; }
            set { _ReservedTime = value; }
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
        /// 功能描述：状态（0.已申请退款，1已退款，2客户申请退款）
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ARState
        {
            get { return _ARState; }
            set { _ARState = value; }
        }

        /// <summary>
        /// 功能描述：退租状态
        /// 长度：30
        /// 不能为空：否
        /// </summary>
        public string ARStateName
        {
            get
            {
                string _ARStateName = "";
                switch (_ARState)
                {
                    case "0":
                        _ARStateName = "客服申请退款";
                        break;
                    case "1":
                        _ARStateName = "已退款";
                        break;
                    case "2":
                        _ARStateName = "客户申请退款";
                        break;
                }
                return _ARStateName;
            }
        }
    }
}
