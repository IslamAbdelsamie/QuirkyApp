namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeRelationShipBetweenRentTypeTableAndBookRentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookRents", "RentTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookRents", "RentTypeId");
            AddForeignKey("dbo.BookRents", "RentTypeId", "dbo.RentTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookRents", "RentTypeId", "dbo.RentTypes");
            DropIndex("dbo.BookRents", new[] { "RentTypeId" });
            DropColumn("dbo.BookRents", "RentTypeId");
        }
    }
}
