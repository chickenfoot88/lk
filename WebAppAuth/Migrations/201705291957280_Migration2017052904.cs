namespace WebAppAuth.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052904 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("system.Claim", "EmployeeId", "main.Employee");
            DropIndex("system.Claim", new[] { "EmployeeId" });
            AlterColumn("system.Claim", "EmployeeId", c => c.Long());
            CreateIndex("system.Claim", "EmployeeId");
            AddForeignKey("system.Claim", "EmployeeId", "main.Employee", "Id");
            DropColumn("system.Claim", "ClaimNumber");
        }
        
        public override void Down()
        {
            AddColumn("system.Claim", "ClaimNumber", c => c.String(nullable: false));
            DropForeignKey("system.Claim", "EmployeeId", "main.Employee");
            DropIndex("system.Claim", new[] { "EmployeeId" });
            AlterColumn("system.Claim", "EmployeeId", c => c.Long(nullable: false));
            CreateIndex("system.Claim", "EmployeeId");
            AddForeignKey("system.Claim", "EmployeeId", "main.Employee", "Id", cascadeDelete: true);
        }
    }
}
