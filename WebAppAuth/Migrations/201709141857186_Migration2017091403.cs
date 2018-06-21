namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091403 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.HouseNotification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NotificationId = c.Long(nullable: false),
                        HouseId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ChangedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("main.Notification", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.HouseId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.Notification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ChangedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("main.HouseNotification", "OrganizationId", "system.Organization");
            DropForeignKey("main.HouseNotification", "NotificationId", "main.Notification");
            DropForeignKey("main.Notification", "OrganizationId", "system.Organization");
            DropForeignKey("main.HouseNotification", "HouseId", "main.House");
            DropIndex("main.Notification", new[] { "OrganizationId" });
            DropIndex("main.HouseNotification", new[] { "OrganizationId" });
            DropIndex("main.HouseNotification", new[] { "HouseId" });
            DropIndex("main.HouseNotification", new[] { "NotificationId" });
            DropTable("main.Notification");
            DropTable("main.HouseNotification");
        }
    }
}
