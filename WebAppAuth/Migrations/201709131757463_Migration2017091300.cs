namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091300 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("import.importaccrual");
            AlterColumn("import.importaccrual", "SupplierName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("import.importaccrual", new[] { "SectionId", "CalculationDate", "OrganizationCode", "PaymentCode", "ServiceName", "MeasureName", "SupplierName" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("import.importaccrual");
            AlterColumn("import.importaccrual", "SupplierName", c => c.String());
            AddPrimaryKey("import.importaccrual", new[] { "SectionId", "CalculationDate", "OrganizationCode", "PaymentCode", "ServiceName", "MeasureName" });
        }
    }
}
