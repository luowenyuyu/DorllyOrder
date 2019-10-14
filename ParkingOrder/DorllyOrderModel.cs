namespace ParkingOrder
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DorllyOrderModel : DbContext
    {
        public DorllyOrderModel()
            : base("name=DorllyOrderModel")
        {
        }
        public virtual DbSet<Op_MonthCar> Op_MonthCar { get; set; }
        public virtual DbSet<Op_PassCar> Op_PassCar { get; set; }
        public virtual DbSet<Mstr_ChargeAccount> Mstr_ChargeAccount { get; set; }
        public virtual DbSet<Mstr_Customer> Mstr_Customer { get; set; }
        public virtual DbSet<Mstr_Service> Mstr_Service { get; set; }
        public virtual DbSet<Mstr_ServiceProvider> Mstr_ServiceProvider { get; set; }
        public virtual DbSet<Mstr_ServiceType> Mstr_ServiceType { get; set; }
        public virtual DbSet<Mstr_TaxRate> Mstr_TaxRate { get; set; }
        public virtual DbSet<Op_OrderDetail> Op_OrderDetail { get; set; }
        public virtual DbSet<Op_OrderHeader> Op_OrderHeader { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Op_MonthCar>()
               .Property(e => e.Amount)
               .HasPrecision(15, 2);

            modelBuilder.Entity<Op_PassCar>()
                .Property(e => e.NeedPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Op_PassCar>()
                .Property(e => e.ActualPay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Op_PassCar>()
                .Property(e => e.Prepay)
                .HasPrecision(15, 2);

            modelBuilder.Entity<Op_PassCar>()
                .Property(e => e.DiscountVal)
                .HasPrecision(15, 2);


            modelBuilder.Entity<Mstr_Service>()
                .Property(e => e.SRVRate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Mstr_Service>()
                .Property(e => e.SRVTaxRate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Mstr_TaxRate>()
                .Property(e => e.Rate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ODQTY)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ODUnitPrice)
                .HasPrecision(15, 6);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ODARAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ODTaxRate)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ODTaxAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.LastReadout)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.Readout)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ODPaidAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderDetail>()
                .Property(e => e.ReduceAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderHeader>()
                .Property(e => e.ARAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderHeader>()
                .Property(e => e.ReduceAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderHeader>()
                .Property(e => e.PaidinAmount)
                .HasPrecision(15, 4);

            modelBuilder.Entity<Op_OrderHeader>()
                .Property(e => e.ODTaxAmount)
                .HasPrecision(15, 4);
        }
    }
}
