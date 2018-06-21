namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017093000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.EmployeeOrganization",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ChangedTime = c.DateTime(nullable: false),
                        Employee_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.Employee", t => t.Employee_Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId)
                .Index(t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("main.EmployeeOrganization", "OrganizationId", "system.Organization");
            DropForeignKey("main.EmployeeOrganization", "Employee_Id", "main.Employee");
            DropIndex("main.EmployeeOrganization", new[] { "Employee_Id" });
            DropIndex("main.EmployeeOrganization", new[] { "OrganizationId" });
            DropTable("main.EmployeeOrganization");
        }
    }
}
