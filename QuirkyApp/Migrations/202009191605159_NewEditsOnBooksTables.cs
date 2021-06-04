namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewEditsOnBooksTables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Books", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Image", c => c.String(nullable: false));
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
