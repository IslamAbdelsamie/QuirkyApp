namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFieldsInBooksTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
