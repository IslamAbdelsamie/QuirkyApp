namespace QuirkyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMemeberShipTypeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberShipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        SignUpFee = c.Byte(nullable: false),
                        ChargeRateOnOneMonth = c.Byte(nullable: false),
                        ChargeRateOnSixMonth = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MemberShipTypes");
        }
    }
}
