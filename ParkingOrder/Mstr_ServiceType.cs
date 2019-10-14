namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mstr_ServiceType
    {
        [Key]
        [StringLength(30)]
        public string SRVTypeNo { get; set; }

        [StringLength(50)]
        public string SRVTypeName { get; set; }

        [StringLength(30)]
        public string ParentTypeNo { get; set; }

        [StringLength(30)]
        public string SRVSPNo { get; set; }

        [StringLength(300)]
        public string Remark { get; set; }

        public bool? SRVStatus { get; set; }
    }
}
