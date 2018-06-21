namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091200 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("import.importaccrual", "ConsumptionHouseByApartments", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByApartmentsImd", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByApartmentsNorm", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByNonResidential", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionLift", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("import.importaccrual", "ConsumptionLift", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByNonResidential", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByApartmentsNorm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByApartmentsImd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "ConsumptionHouseByApartments", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
