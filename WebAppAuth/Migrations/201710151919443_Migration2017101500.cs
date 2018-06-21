namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017101500 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.AbonentClaim", "EmployeeCreatorId", c => c.Long());
            CreateIndex("main.AbonentClaim", "EmployeeCreatorId");
            AddForeignKey("main.AbonentClaim", "EmployeeCreatorId", "main.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("main.AbonentClaim", "EmployeeCreatorId", "main.Employee");
            DropIndex("main.AbonentClaim", new[] { "EmployeeCreatorId" });
            DropColumn("main.AbonentClaim", "EmployeeCreatorId");
        }
    }
}
