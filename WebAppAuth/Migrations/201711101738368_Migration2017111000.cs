namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017111000 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("import.importaccrual");
            AlterColumn("import.importaccrual", "ServiceGroupName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("import.importaccrual", new[] { "SectionId", "CalculationDate", "OrganizationCode", "PaymentCode", "ServiceName", "MeasureName", "OuterSystemServiceId", "ServiceGroupName", "Tariff", "Consumption", "AccruedTariff", "AccruedTariffNondelivery", "SumNondelivery", "ConsumptionNondelivery", "Reval", "SumBalanceDelta", "AccruedForPayment", "SumPayd", "SumOutsaldo", "SumInsaldo", "SupplierName", "ServiceDeliveryDays" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("import.importaccrual");
            AlterColumn("import.importaccrual", "ServiceGroupName", c => c.String());
            AddPrimaryKey("import.importaccrual", new[] { "SectionId", "CalculationDate", "OrganizationCode", "PaymentCode", "ServiceName", "MeasureName", "AccruedForPayment", "SupplierName" });
        }
    }
}
