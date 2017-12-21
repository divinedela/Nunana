namespace Nunana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTenantsToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        Address = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false),
                        EmergencyContactFirstName = c.String(nullable: false, maxLength: 50),
                        EmergencyContactLastName = c.String(nullable: false, maxLength: 50),
                        EmergencyContactPhoneNumber = c.String(nullable: false, maxLength: 15),
                        EmergencyContactAddress = c.String(nullable: false, maxLength: 200),
                        EmergencyContactEmail = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tenants");
        }
    }
}
