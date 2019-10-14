namespace ParkingOrder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarObject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Op_MonthCar",
                c => new
                    {
                        CarNo = c.String(nullable: false, maxLength: 36),
                        PlateNo = c.String(maxLength: 15),
                        Amount = c.Decimal(nullable: false, precision: 15, scale: 2),
                        CreateTime = c.DateTime(),
                        EndDate = c.DateTime(),
                        ChargeType = c.Int(),
                    })
                .PrimaryKey(t => t.CarNo);
            
            CreateTable(
                "dbo.Op_PassCar",
                c => new
                    {
                        UniqueID = c.String(nullable: false, maxLength: 36),
                        PlateNo = c.String(maxLength: 15),
                        State = c.Int(),
                        EnterTime = c.DateTime(),
                        OutTime = c.DateTime(),
                        InPortName = c.String(maxLength: 36),
                        OutPortName = c.String(maxLength: 36),
                        CarType = c.String(maxLength: 36),
                        NeedPay = c.Decimal(nullable: false, precision: 15, scale: 2),
                        Prepay = c.Decimal(nullable: false, precision: 15, scale: 2),
                        ActualPay = c.Decimal(nullable: false, precision: 15, scale: 2),
                        DiscountVal = c.Decimal(nullable: false, precision: 15, scale: 2),
                        DiscountType = c.Int(),
                        ResidenceTime = c.Int(),
                        AutoPay = c.Int(),
                    })
                .PrimaryKey(t => t.UniqueID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Op_PassCar");
            DropTable("dbo.Op_MonthCar");
        }
    }
}
