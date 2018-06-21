namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017110902 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("import.importaccrual");
            AddPrimaryKey("import.importaccrual", new[] { "SectionId", "CalculationDate", "OrganizationCode", "PaymentCode", "ServiceName", "MeasureName", "AccruedForPayment", "SupplierName" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("import.importaccrual");
            AddPrimaryKey("import.importaccrual", new[] { "SectionId", "CalculationDate", "OrganizationCode", "PaymentCode", "ServiceName", "MeasureName", "SupplierName" });
        }
    }
}
