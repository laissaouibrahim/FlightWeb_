namespace FlightWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FlightDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightId = c.Int(nullable: false, identity: true),
                        FlightName = c.String(nullable: false, maxLength: 70),
                        Departure = c.String(nullable: false, maxLength: 70),
                        Destination = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.FlightId);
             
        }
        
        public override void Down()
        { 
            
            DropTable("dbo.Flights");
        }
    }
}
