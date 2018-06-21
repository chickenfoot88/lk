namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052801 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("import.importsocialpayment", "AmountToPaid", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importsocialpayment", "SubsidiesEndDate", c => c.DateTime());
            AlterColumn("import.importsocialpayment", "SumInsaldo", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importsocialpayment", "SumDelta", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("import.importsocialpayment", "SumRecalculation", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("import.importsocialpayment", "SumRecalculation", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importsocialpayment", "SumDelta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importsocialpayment", "SumInsaldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("import.importsocialpayment", "SubsidiesEndDate", c => c.DateTime(nullable: false));
            AlterColumn("import.importsocialpayment", "AmountToPaid", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
