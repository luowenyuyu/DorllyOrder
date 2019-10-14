namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Op_OrderDetail
    {
        [Key]
        [StringLength(36)]
        public string RowPointer { get; set; }

        [StringLength(36)]
        public string RefRP { get; set; }

        [StringLength(30)]
        public string ODSRVTypeNo1 { get; set; }

        [StringLength(30)]
        public string ODSRVTypeNo2 { get; set; }

        [StringLength(30)]
        public string ODSRVNo { get; set; }

        [StringLength(80)]
        public string ODSRVRemark { get; set; }

        [StringLength(30)]
        public string ODSRVCalType { get; set; }

        [StringLength(30)]
        public string ODContractSPNo { get; set; }

        [StringLength(30)]
        public string ODContractNo { get; set; }

        [StringLength(30)]
        public string ODContractNoManual { get; set; }

        [StringLength(30)]
        public string ResourceNo { get; set; }

        [StringLength(50)]
        public string ResourceName { get; set; }

        public DateTime? ODFeeStartDate { get; set; }

        public DateTime? ODFeeEndDate { get; set; }

        public int? BillingDays { get; set; }

        public decimal? ODQTY { get; set; }

        [StringLength(30)]
        public string ODUnit { get; set; }

        public decimal? ODUnitPrice { get; set; }

        public decimal? ODARAmount { get; set; }

        [StringLength(30)]
        public string ODCANo { get; set; }

        [StringLength(30)]
        public string ODCreator { get; set; }

        public DateTime? ODCreateDate { get; set; }

        [StringLength(30)]
        public string ODLastReviser { get; set; }

        public DateTime? ODLastRevisedDate { get; set; }

        [StringLength(36)]
        public string RefNo { get; set; }

        public decimal? ODTaxRate { get; set; }

        public decimal? ODTaxAmount { get; set; }

        public decimal? LastReadout { get; set; }

        public decimal? Readout { get; set; }

        public decimal? ODPaidAmount { get; set; }

        public bool? IsLateFee { get; set; }

        public decimal? ReduceAmount { get; set; }
    }
}
