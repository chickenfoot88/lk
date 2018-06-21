namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017051300 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "import.importpaymentrequisite",
                c => new
                    {
                        SectionId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        OrganizationCode = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        RequisiteType = c.Int(nullable: false),
                        RecipientName = c.String(),
                        RecipientBankName = c.String(),
                        RecipientCheckingAccount = c.String(),
                        RecipientCorrespondentAccount = c.String(),
                        RecipientInn = c.String(),
                        RecipientKpp = c.String(),
                        RecipientBankBik = c.String(),
                        RecipientAddress = c.String(),
                        RecipientPhone = c.String(),
                        RecipientEmail = c.String(),
                        RecipientNote = c.String(),
                        ProviderName = c.String(),
                        ProviderBankName = c.String(),
                        ProviderCheckingAccount = c.String(),
                        ProviderCorrespondentAccount = c.String(),
                        ProviderInn = c.String(),
                        ProviderKpp = c.String(),
                        ProviderBankBik = c.String(),
                        ProviderAddress = c.String(),
                        ProviderPhone = c.String(),
                        ProviderEmail = c.String(),
                        ProviderNote = c.String(),
                        ManagmentOrganizationCode = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => new { t.CalculationDate, t.OrganizationCode, t.PaymentCode, t.RequisiteType });
            
            CreateTable(
                "main.PersonalAccountPaymentRequisite",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        PersonalAccountId = c.Long(nullable: false),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        HouseId = c.Long(nullable: false),
                        RequisiteType = c.Int(nullable: false),
                        RecipientName = c.String(),
                        RecipientBankName = c.String(),
                        RecipientCheckingAccount = c.String(),
                        RecipientCorrespondentAccount = c.String(),
                        RecipientInn = c.String(),
                        RecipientKpp = c.String(),
                        RecipientBankBik = c.String(),
                        RecipientAddress = c.String(),
                        RecipientPhone = c.String(),
                        RecipientEmail = c.String(),
                        RecipientNote = c.String(),
                        ProviderName = c.String(),
                        ProviderBankName = c.String(),
                        ProviderCheckingAccount = c.String(),
                        ProviderCorrespondentAccount = c.String(),
                        ProviderInn = c.String(),
                        ProviderKpp = c.String(),
                        ProviderBankBik = c.String(),
                        ProviderAddress = c.String(),
                        ProviderPhone = c.String(),
                        ProviderEmail = c.String(),
                        ProviderNote = c.String(),
                        ManagmentOrganizationCode = c.String(),
                        Note = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.PersonalAccount", t => t.PersonalAccountId, cascadeDelete: true)
                .Index(t => t.PersonalAccountId)
                .Index(t => t.HouseId)
                .Index(t => t.OrganizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("main.PersonalAccountPaymentRequisite", "PersonalAccountId", "main.PersonalAccount");
            DropForeignKey("main.PersonalAccountPaymentRequisite", "OrganizationId", "system.Organization");
            DropForeignKey("main.PersonalAccountPaymentRequisite", "HouseId", "main.House");
            DropIndex("main.PersonalAccountPaymentRequisite", new[] { "OrganizationId" });
            DropIndex("main.PersonalAccountPaymentRequisite", new[] { "HouseId" });
            DropIndex("main.PersonalAccountPaymentRequisite", new[] { "PersonalAccountId" });
            DropTable("main.PersonalAccountPaymentRequisite");
            DropTable("import.importpaymentrequisite");
        }
    }
}
