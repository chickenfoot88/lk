namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017110901 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "system.OrganizationSubdivision",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        OrganizationSubdivisionExternalId = c.Long(nullable: false),
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
            DropForeignKey("system.OrganizationSubdivision", "OrganizationId", "system.Organization");
            DropIndex("system.OrganizationSubdivision", new[] { "OrganizationId" });
            DropTable("system.OrganizationSubdivision");
        }
    }
}
