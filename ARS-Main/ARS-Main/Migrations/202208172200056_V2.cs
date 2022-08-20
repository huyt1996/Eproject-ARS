namespace ARS_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "PaymentId", "dbo.Payments");
            DropIndex("dbo.Tickets", new[] { "PaymentId" });
            DropColumn("dbo.Tickets", "PaymentId");
            DropTable("dbo.Payments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Method = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Tickets", "PaymentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tickets", "PaymentId");
            AddForeignKey("dbo.Tickets", "PaymentId", "dbo.Payments", "Id", cascadeDelete: true);
        }
    }
}
