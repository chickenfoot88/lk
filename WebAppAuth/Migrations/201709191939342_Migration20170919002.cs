namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration20170919002 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("main.Abonent", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Abonent", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.Organization", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.Organization", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccount", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccount", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AddressSpace", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AddressSpace", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.House", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.House", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ManagmentOrganization", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ManagmentOrganization", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaim", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaim", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimKind", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimKind", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimType", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimType", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Employee", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Employee", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ImportedFile", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ImportedFile", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExchangeFile", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExchangeFile", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaimComment", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaimComment", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.CalculationMonth", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.CalculationMonth", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ClaimTransferHistory", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ClaimTransferHistory", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimStatusTransfer", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimStatusTransfer", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExportedFile", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExportedFile", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseAccrual", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseAccrual", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Service", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Service", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Supplier", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Supplier", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseMeterReading", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseMeterReading", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.MeterService", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.MeterService", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseNotification", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseNotification", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Notification", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Notification", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountAccrual", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountAccrual", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountMeterReading", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountMeterReading", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPayment", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPayment", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPaymentRequisite", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPaymentRequisite", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountSocialPayment", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountSocialPayment", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.UniPay", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.UniPay", "ChangedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("main.UniPay", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.UniPay", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountSocialPayment", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountSocialPayment", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPaymentRequisite", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPaymentRequisite", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPayment", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountPayment", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountMeterReading", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountMeterReading", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountAccrual", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccountAccrual", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Notification", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Notification", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseNotification", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseNotification", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.MeterService", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.MeterService", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseMeterReading", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseMeterReading", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Supplier", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Supplier", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Service", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Service", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseAccrual", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.HouseAccrual", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExportedFile", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExportedFile", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimStatusTransfer", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimStatusTransfer", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ClaimTransferHistory", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ClaimTransferHistory", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.CalculationMonth", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.CalculationMonth", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaimComment", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaimComment", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExchangeFile", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ExchangeFile", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ImportedFile", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ImportedFile", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Employee", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Employee", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimType", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimType", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimKind", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.DictClaimKind", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaim", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AbonentClaim", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ManagmentOrganization", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.ManagmentOrganization", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.House", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.House", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AddressSpace", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.AddressSpace", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccount", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.PersonalAccount", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("system.Organization", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("system.Organization", "CreationTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Abonent", "ChangedTime", c => c.DateTime(nullable: false));
            AlterColumn("main.Abonent", "CreationTime", c => c.DateTime(nullable: false));
        }
    }
}
