namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052803 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.PersonalAccountSocialPayment", "PersonalAccountId", c => c.Long(nullable: false));
            CreateIndex("main.PersonalAccountSocialPayment", "PersonalAccountId");
            AddForeignKey("main.PersonalAccountSocialPayment", "PersonalAccountId", "main.PersonalAccount", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("main.PersonalAccountSocialPayment", "PersonalAccountId", "main.PersonalAccount");
            DropIndex("main.PersonalAccountSocialPayment", new[] { "PersonalAccountId" });
            DropColumn("main.PersonalAccountSocialPayment", "PersonalAccountId");
        }
    }
}
