namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Status");
        }
    }
}
