namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091301 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("import.importaccrual", "Consumption", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("import.importaccrual", "Consumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
