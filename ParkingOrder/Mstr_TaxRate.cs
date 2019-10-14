namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mstr_TaxRate
    {
        [Key]
        [StringLength(36)]
        public string RP { get; set; }

        [StringLength(30)]
        public string SPNo { get; set; }

        [StringLength(30)]
        public string SRVNo { get; set; }

        public decimal? Rate { get; set; }

        [StringLength(30)]
        public string CreateUser { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(30)]
        public string UpdateUser { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
