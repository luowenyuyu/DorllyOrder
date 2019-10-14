namespace ParkingOrder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mstr_ServiceProvider
    {
        [Key]
        [StringLength(30)]
        public string SPNo { get; set; }

        [StringLength(80)]
        public string SPName { get; set; }

        [StringLength(50)]
        public string SPShortName { get; set; }

        [StringLength(30)]
        public string MService { get; set; }

        [StringLength(30)]
        public string SPLicenseNo { get; set; }

        [StringLength(30)]
        public string SPContact { get; set; }

        [StringLength(30)]
        public string SPContactMobile { get; set; }

        [StringLength(30)]
        public string SPTel { get; set; }

        [StringLength(30)]
        public string SPEMail { get; set; }

        [StringLength(300)]
        public string SPAddr { get; set; }

        [StringLength(30)]
        public string SPBank { get; set; }

        [StringLength(30)]
        public string SPBankAccount { get; set; }

        [StringLength(30)]
        public string SPBankTitle { get; set; }

        public bool? SPStatus { get; set; }

        [StringLength(30)]
        public string U8Account { get; set; }

        [StringLength(30)]
        public string BankAccount { get; set; }

        [StringLength(30)]
        public string CashAccount { get; set; }

        [StringLength(30)]
        public string TaxAccount { get; set; }
    }
}
