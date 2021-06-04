namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingDataIntoRentTypesTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into [dbo].[RentTypes] (RentName) values ('One Month Rent')");
            Sql("insert into [dbo].[RentTypes] (RentName) values ('Six Months Rent')");

        }

        public override void Down()
        {
        }
    }
}
