namespace _43LimeMobileApp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ButtonCommandCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommandLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CommandId = c.Int(nullable: false),
                        Timestamp = c.Long(nullable: false),
                        Location_Lattitude = c.String(),
                        Location_Longitude = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ButtonCommands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommandId = c.Int(nullable: false),
                        Command = c.String(maxLength: 56),
                        ParentId = c.Int(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(maxLength: 28),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 4),
                        Username = c.String(maxLength: 56),
                        RoleId = c.Int(nullable: false),
                        IsActive = c.Boolean(),
                        IsOnline = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.ButtonCommands");
            DropTable("dbo.CommandLogs");
            DropTable("dbo.ButtonCommandCategories");
        }
    }
}
