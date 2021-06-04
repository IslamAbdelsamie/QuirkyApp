namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBooksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 20),
                        Author = c.String(nullable: false, maxLength: 20),
                        Description = c.String(nullable: false, maxLength: 50),
                        Image = c.String(nullable: false),
                        Availiability = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        Pages = c.Int(nullable: false),
                        GenresId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenresId, cascadeDelete: true)
                .Index(t => t.GenresId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "GenresId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenresId" });
            DropTable("dbo.Books");
        }
    }
}
