namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mstr_ChargeAccount
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string CANo { get; set; }

        [StringLength(50)]
        public string CAName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string CASPNo { get; set; }

        [StringLength(30)]
        public string APNo { get; set; }
    }
}
