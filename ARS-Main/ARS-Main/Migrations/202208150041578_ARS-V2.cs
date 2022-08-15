namespace ARS_Main.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ARSV2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TblFlightBook", "Planeid", "dbo.AeroPlaneInfoes");
            DropIndex("dbo.TblFlightBook", new[] { "Planeid" });
            CreateTable(
                "dbo.TicketReserve_tbl",
                c => new
                    {
                        ResID = c.Int(nullable: false, identity: true),
                        ResFrom = c.String(nullable: false),
                        ResTo = c.String(nullable: false),
                        ResDepDate = c.String(nullable: false),
                        ResTime = c.String(nullable: false),
                        PlaneId = c.String(nullable: false),
                        PlaneSeat = c.Int(nullable: false),
                        ResTicketPrice = c.Single(nullable: false),
                        Plane_tbls_PlaneID = c.Int(),
                        FlightBooking_Bid = c.Int(),
                    })
                .PrimaryKey(t => t.ResID)
                .ForeignKey("dbo.AeroPlaneInfoes", t => t.Plane_tbls_PlaneID)
                .ForeignKey("dbo.TblFlightBook", t => t.FlightBooking_Bid)
                .Index(t => t.Plane_tbls_PlaneID)
                .Index(t => t.FlightBooking_Bid);
            
            AddColumn("dbo.TblFlightBook", "bCusName", c => c.String(nullable: false));
            AddColumn("dbo.TblFlightBook", "bCusAddress", c => c.String(nullable: false));
            AddColumn("dbo.TblFlightBook", "bCusEmail", c => c.String(nullable: false));
            AddColumn("dbo.TblFlightBook", "bCusSeats", c => c.String(nullable: false));
            AddColumn("dbo.TblFlightBook", "bCusPhoneNum", c => c.String(nullable: false));
            AddColumn("dbo.TblFlightBook", "bCusCnic", c => c.String(nullable: false));
            AddColumn("dbo.TblFlightBook", "ResID", c => c.Int(nullable: false));
            AlterColumn("dbo.AeroPlaneInfoes", "APlaneName", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.AeroPlaneInfoes", "SeatingCapacity", c => c.Int(nullable: false));
            DropColumn("dbo.TblFlightBook", "From");
            DropColumn("dbo.TblFlightBook", "To");
            DropColumn("dbo.TblFlightBook", "DDate");
            DropColumn("dbo.TblFlightBook", "DTime");
            DropColumn("dbo.TblFlightBook", "Planeid");
            DropColumn("dbo.TblFlightBook", "SeatType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TblFlightBook", "SeatType", c => c.String(maxLength: 25));
            AddColumn("dbo.TblFlightBook", "Planeid", c => c.Int(nullable: false));
            AddColumn("dbo.TblFlightBook", "DTime", c => c.String(maxLength: 15));
            AddColumn("dbo.TblFlightBook", "DDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.TblFlightBook", "To", c => c.String(nullable: false, maxLength: 40));
            AddColumn("dbo.TblFlightBook", "From", c => c.String(nullable: false, maxLength: 40));
            DropForeignKey("dbo.TicketReserve_tbl", "FlightBooking_Bid", "dbo.TblFlightBook");
            DropForeignKey("dbo.TicketReserve_tbl", "Plane_tbls_PlaneID", "dbo.AeroPlaneInfoes");
            DropIndex("dbo.TicketReserve_tbl", new[] { "FlightBooking_Bid" });
            DropIndex("dbo.TicketReserve_tbl", new[] { "Plane_tbls_PlaneID" });
            AlterColumn("dbo.AeroPlaneInfoes", "SeatingCapacity", c => c.String(nullable: false));
            AlterColumn("dbo.AeroPlaneInfoes", "APlaneName", c => c.String(nullable: false));
            DropColumn("dbo.TblFlightBook", "ResID");
            DropColumn("dbo.TblFlightBook", "bCusCnic");
            DropColumn("dbo.TblFlightBook", "bCusPhoneNum");
            DropColumn("dbo.TblFlightBook", "bCusSeats");
            DropColumn("dbo.TblFlightBook", "bCusEmail");
            DropColumn("dbo.TblFlightBook", "bCusAddress");
            DropColumn("dbo.TblFlightBook", "bCusName");
            DropTable("dbo.TicketReserve_tbl");
            CreateIndex("dbo.TblFlightBook", "Planeid");
            AddForeignKey("dbo.TblFlightBook", "Planeid", "dbo.AeroPlaneInfoes", "Planeid", cascadeDelete: true);
        }
    }
}
