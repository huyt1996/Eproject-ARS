namespace ARS_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ARSV5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TblUserAccount",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        Age = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        Cnic = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TblUserAccount");
        }
    }
}
