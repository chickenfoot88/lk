namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052802 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "main.SocialProtectionInfo", newName: "PersonalAccountSocialPayment");
            AlterColumn("main.PersonalAccountSocialPayment", "ExpenceId", c => c.Long());
            AlterColumn("main.PersonalAccountSocialPayment", "SumAccrued", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "SumPayoff", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "ExpenceEndDate", c => c.DateTime());
            AlterColumn("main.PersonalAccountSocialPayment", "BalanceIn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "SumDelta", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "SumRecalc", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("main.PersonalAccountSocialPayment", "SumRecalc", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "SumDelta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "BalanceIn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "ExpenceEndDate", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountSocialPayment", "SumPayoff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "SumAccrued", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountSocialPayment", "ExpenceId", c => c.Long(nullable: false));
            RenameTable(name: "main.PersonalAccountSocialPayment", newName: "SocialProtectionInfo");
        }
    }
}
