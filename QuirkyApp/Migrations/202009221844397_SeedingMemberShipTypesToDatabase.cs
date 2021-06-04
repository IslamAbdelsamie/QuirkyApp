namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingMemberShipTypesToDatabase : DbMigration
    {
        public override void Up()
        {
            Sql("insert into [dbo].[MemberShipTypes](Name,SignUpFee,ChargeRateOnOneMonth,ChargeRateOnSixMonth) values('Pay Per Rental',0,25,50)");
            Sql("insert into [dbo].[MemberShipTypes](Name,SignUpFee,ChargeRateOnOneMonth,ChargeRateOnSixMonth) values('Member',100,10,20)");
            Sql("insert into [dbo].[MemberShipTypes](Name,SignUpFee,ChargeRateOnOneMonth,ChargeRateOnSixMonth) values('Admin',0,0,0)");

        }

        public override void Down()
        {
        }
    }
}
