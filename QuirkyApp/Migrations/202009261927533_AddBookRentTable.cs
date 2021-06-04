namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookRentTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookRents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(),
                        ActualEndDate = c.DateTime(),
                        ScheduledEndDate = c.DateTime(),
                        RentPrice = c.Double(nullable: false),
                        AdditionalCharge = c.Double(),
                        BooksId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        StatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.Books", t => t.BooksId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.BooksId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.StatusId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookRents", "StatusId", "dbo.Status");
            DropForeignKey("dbo.BookRents", "BooksId", "dbo.Books");
            DropForeignKey("dbo.BookRents", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BookRents", new[] { "StatusId" });
            DropIndex("dbo.BookRents", new[] { "ApplicationUserId" });
            DropIndex("dbo.BookRents", new[] { "BooksId" });
            DropTable("dbo.BookRents");
        }
    }
}
