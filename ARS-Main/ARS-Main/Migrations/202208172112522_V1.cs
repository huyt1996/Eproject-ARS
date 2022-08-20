namespace ARS_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserRole", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserRoles");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserRoles", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserRole");
        }
    }
}
