namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedingDataIntoStatusTable : DbMigration
    {
        public override void Up()
        {
            Sql("insert into [dbo].[Status] (StatusName) values ('Requested')");
            Sql("insert into [dbo].[Status] (StatusName) values ('Approved')");
            Sql("insert into [dbo].[Status] (StatusName) values ('Rejected')");
            Sql("insert into [dbo].[Status] (StatusName) values ('Rented')");
            Sql("insert into [dbo].[Status] (StatusName) values ('Closed')");


        }

        public override void Down()
        {
        }
    }
}
