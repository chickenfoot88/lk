namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017072500 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.AbonentClaimComment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AbonentClaimId = c.Long(nullable: false),
                        EmployeeId = c.Long(),
                        Comment = c.String(nullable: false),
                        ImportedFileId = c.Long(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.AbonentClaim", t => t.AbonentClaimId, cascadeDelete: true)
                .ForeignKey("main.Employee", t => t.EmployeeId)
                .ForeignKey("main.ImportedFile", t => t.ImportedFileId)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.AbonentClaimId)
                .Index(t => t.EmployeeId)
                .Index(t => t.ImportedFileId)
                .Index(t => t.OrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("main.AbonentClaimComment", "OrganizationId", "system.Organization");
            DropForeignKey("main.AbonentClaimComment", "ImportedFileId", "main.ImportedFile");
            DropForeignKey("main.AbonentClaimComment", "EmployeeId", "main.Employee");
            DropForeignKey("main.AbonentClaimComment", "AbonentClaimId", "main.AbonentClaim");
            DropIndex("main.AbonentClaimComment", new[] { "OrganizationId" });
            DropIndex("main.AbonentClaimComment", new[] { "ImportedFileId" });
            DropIndex("main.AbonentClaimComment", new[] { "EmployeeId" });
            DropIndex("main.AbonentClaimComment", new[] { "AbonentClaimId" });
            DropTable("main.AbonentClaimComment");
        }
    }
}
