namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mstr_Service
    {
        [Key]
        [StringLength(30)]
        public string SRVNo { get; set; }

        [StringLength(50)]
        public string SRVName { get; set; }

        [StringLength(30)]
        public string SRVTypeNo1 { get; set; }

        [StringLength(30)]
        public string SRVTypeNo2 { get; set; }

        [StringLength(30)]
        public string SRVSPNo { get; set; }

        [StringLength(30)]
        public string CANo { get; set; }

        [StringLength(10)]
        public string SRVCalType { get; set; }

        [StringLength(10)]
        public string SRVRoundType { get; set; }

        public int? SRVDecimalPoint { get; set; }

        public decimal? SRVRate { get; set; }

        public decimal? SRVTaxRate { get; set; }

        public bool? SRVStatus { get; set; }

        [StringLength(300)]
        public string SRVRemark { get; set; }
    }
}
