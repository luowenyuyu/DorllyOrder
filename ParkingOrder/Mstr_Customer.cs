namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mstr_Customer
    {
        [Key]
        [StringLength(30)]
        public string CustNo { get; set; }

        [StringLength(50)]
        public string CustName { get; set; }

        [StringLength(30)]
        public string CustShortName { get; set; }

        [StringLength(30)]
        public string CustType { get; set; }

        [StringLength(30)]
        public string Representative { get; set; }

        [StringLength(50)]
        public string BusinessScope { get; set; }

        [StringLength(30)]
        public string CustLicenseNo { get; set; }

        [StringLength(30)]
        public string RepIDCard { get; set; }

        [StringLength(30)]
        public string CustContact { get; set; }

        [StringLength(30)]
        public string CustTel { get; set; }

        [StringLength(30)]
        public string CustContactMobile { get; set; }

        [StringLength(30)]
        public string CustEmail { get; set; }

        [StringLength(30)]
        public string CustBankTitle { get; set; }

        [StringLength(50)]
        public string CustBankAccount { get; set; }

        [StringLength(50)]
        public string CustBank { get; set; }

        [StringLength(10)]
        public string CustStatus { get; set; }

        public DateTime? CustCreateDate { get; set; }

        [StringLength(30)]
        public string CustCreator { get; set; }

        public bool? IsExternal { get; set; }
    }
}
