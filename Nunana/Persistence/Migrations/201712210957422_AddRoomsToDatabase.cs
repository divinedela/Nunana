namespace Nunana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoomsToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(nullable: false, maxLength: 5),
                        Type = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        IsCurrentlyRented = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
        }
    }
}
