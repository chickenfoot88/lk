namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091201 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("import.importaccrual", "SumTariffChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumInsaldoChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumOutsaldoChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "RevalChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumBalanceDeltaChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "AccruedForPaymentChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumPaydChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "AccruedBySocNorm", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("import.importaccrual", "AccruedBySocNorm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumPaydChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "AccruedForPaymentChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumBalanceDeltaChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "RevalChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumOutsaldoChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumInsaldoChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importaccrual", "SumTariffChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
