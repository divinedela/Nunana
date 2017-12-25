namespace Nunana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRentalsToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        RoomId = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        IsCancelled = c.Boolean(nullable: false),
                        CancelledBy = c.String(),
                        DateCancelled = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.RoomId, t.TenantId })
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.RoomId)
                .Index(t => t.TenantId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rentals", "TenantId", "dbo.Tenants");
            DropIndex("dbo.Rentals", new[] { "TenantId" });
            DropIndex("dbo.Rentals", new[] { "RoomId" });
            DropTable("dbo.Rentals");
        }
    }
}
