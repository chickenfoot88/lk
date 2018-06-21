namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052900 : DbMigration
    {
        public override void Up()
        {
            AddColumn("import.importsocialpayment", "ArticleGroupName", c => c.String());
            AddColumn("import.importsocialpayment", "SumPayoff", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("import.importsocialpayment", "PaymentPlacement", c => c.String());
            AddColumn("main.PersonalAccountSocialPayment", "RecipientName", c => c.String());
            AddColumn("main.PersonalAccountSocialPayment", "ArticleCode", c => c.Long());
            AddColumn("main.PersonalAccountSocialPayment", "ArticleName", c => c.String());
            AddColumn("main.PersonalAccountSocialPayment", "ArticleGroupName", c => c.String());
            AddColumn("main.PersonalAccountSocialPayment", "SubsidiesEndDate", c => c.DateTime());
            AddColumn("main.PersonalAccountSocialPayment", "SumInsaldo", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("main.PersonalAccountSocialPayment", "SumRecalculation", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("import.importsocialpayment", "ArticleGroup");
            DropColumn("import.importsocialpayment", "AmountToPaid");
            DropColumn("import.importsocialpayment", "PayPlacement");
            DropColumn("main.PersonalAccountSocialPayment", "PersonFullName");
            DropColumn("main.PersonalAccountSocialPayment", "ExpenceId");
            DropColumn("main.PersonalAccountSocialPayment", "ExpenceName");
            DropColumn("main.PersonalAccountSocialPayment", "ExpenceGroupName");
            DropColumn("main.PersonalAccountSocialPayment", "ExpenceEndDate");
            DropColumn("main.PersonalAccountSocialPayment", "BalanceIn");
            DropColumn("main.PersonalAccountSocialPayment", "SumRecalc");
        }
        
        public override void Down()
        {
            AddColumn("main.PersonalAccountSocialPayment", "SumRecalc", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("main.PersonalAccountSocialPayment", "BalanceIn", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("main.PersonalAccountSocialPayment", "ExpenceEndDate", c => c.DateTime());
            AddColumn("main.PersonalAccountSocialPayment", "ExpenceGroupName", c => c.String());
            AddColumn("main.PersonalAccountSocialPayment", "ExpenceName", c => c.String());
            AddColumn("main.PersonalAccountSocialPayment", "ExpenceId", c => c.Long());
            AddColumn("main.PersonalAccountSocialPayment", "PersonFullName", c => c.String());
            AddColumn("import.importsocialpayment", "PayPlacement", c => c.String());
            AddColumn("import.importsocialpayment", "AmountToPaid", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("import.importsocialpayment", "ArticleGroup", c => c.String());
            DropColumn("main.PersonalAccountSocialPayment", "SumRecalculation");
            DropColumn("main.PersonalAccountSocialPayment", "SumInsaldo");
            DropColumn("main.PersonalAccountSocialPayment", "SubsidiesEndDate");
            DropColumn("main.PersonalAccountSocialPayment", "ArticleGroupName");
            DropColumn("main.PersonalAccountSocialPayment", "ArticleName");
            DropColumn("main.PersonalAccountSocialPayment", "ArticleCode");
            DropColumn("main.PersonalAccountSocialPayment", "RecipientName");
            DropColumn("import.importsocialpayment", "PaymentPlacement");
            DropColumn("import.importsocialpayment", "SumPayoff");
            DropColumn("import.importsocialpayment", "ArticleGroupName");
        }
    }
}
