namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingRentTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentName = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RentTypes");
        }
    }
}
