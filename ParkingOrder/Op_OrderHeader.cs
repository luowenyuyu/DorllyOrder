namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Op_OrderHeader
    {
        [Key]
        [StringLength(36)]
        public string RowPointer { get; set; }

        [StringLength(30)]
        public string OrderNo { get; set; }

        [StringLength(30)]
        public string OrderType { get; set; }

        [StringLength(30)]
        public string CustNo { get; set; }

        public DateTime? OrderTime { get; set; }

        public int? DaysofMonth { get; set; }

        public DateTime? ARDate { get; set; }

        public decimal? ARAmount { get; set; }

        public decimal? ReduceAmount { get; set; }

        public decimal? PaidinAmount { get; set; }

        [StringLength(30)]
        public string OrderAuditor { get; set; }

        public DateTime? OrderAuditDate { get; set; }

        [StringLength(300)]
        public string OrderAuditReason { get; set; }

        [StringLength(30)]
        public string OrderRAuditor { get; set; }

        public DateTime? OrderRAuditDate { get; set; }

        [StringLength(300)]
        public string OrderRAuditReason { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [StringLength(10)]
        public string OrderStatus { get; set; }

        [StringLength(30)]
        public string OrderCreator { get; set; }

        public DateTime? OrderCreateDate { get; set; }

        [StringLength(30)]
        public string OrderLastReviser { get; set; }

        public DateTime? OrderLastRevisedDate { get; set; }

        public decimal? ODTaxAmount { get; set; }

        public DateTime? ReduceDate { get; set; }
    }
}
