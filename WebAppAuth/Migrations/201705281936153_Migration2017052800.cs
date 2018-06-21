namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052800 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "import.importsocialpayment",
                c => new
                    {
                        SectionId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        OrganizationCode = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        RecipientName = c.String(nullable: false, maxLength: 128),
                        ArticleCode = c.Long(nullable: false),
                        ArticleName = c.String(),
                        ArticleGroup = c.String(),
                        SumAccrued = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountToPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PayPlacement = c.String(),
                        SubsidiesEndDate = c.DateTime(nullable: false),
                        SumInsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumDelta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumRecalculation = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.CalculationDate, t.OrganizationCode, t.PaymentCode, t.RecipientName, t.ArticleCode, t.SumAccrued });
            
        }
        
        public override void Down()
        {
            DropTable("import.importsocialpayment");
        }
    }
}
