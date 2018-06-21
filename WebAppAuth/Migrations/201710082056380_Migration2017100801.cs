namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017100801 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.AbonentClaim", "Accepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("main.AbonentClaim", "Accepted");
        }
    }
}
