namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migraion2017102900 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.AbonentClaim", "Cost", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("main.AbonentClaim", "Cost");
        }
    }
}
