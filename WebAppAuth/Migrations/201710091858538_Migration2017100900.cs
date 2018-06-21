namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017100900 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.AbonentClaim", "Rating", c => c.Int());
            AddColumn("main.AbonentClaim", "ClosedByEmployeeId", c => c.Long());
            CreateIndex("main.AbonentClaim", "ClosedByEmployeeId");
            AddForeignKey("main.AbonentClaim", "ClosedByEmployeeId", "main.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("main.AbonentClaim", "ClosedByEmployeeId", "main.Employee");
            DropIndex("main.AbonentClaim", new[] { "ClosedByEmployeeId" });
            DropColumn("main.AbonentClaim", "ClosedByEmployeeId");
            DropColumn("main.AbonentClaim", "Rating");
        }
    }
}
