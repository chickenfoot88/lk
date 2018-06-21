namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091304 : DbMigration
    {
        public override void Up()
        {
            AddColumn("system.Organization", "OrganizationDivisionExternalId", c => c.Long(nullable: false));
            AlterColumn("main.HouseAccrual", "AccruedTariff", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedTariffNondelivery", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "Reval", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumBalanceDelta", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedForPayment", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumPayd", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumOutsaldo", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumInsaldo", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumTariffChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumInsaldoChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumOutsaldoChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "RevalChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumBalanceDeltaChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedForPaymentChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseMeterReading", "Consumption", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedTariff", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedTariffNondelivery", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "Reval", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumBalanceDelta", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedForPayment", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumPayd", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumOutsaldo", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumInsaldo", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumTariffChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumInsaldoChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumOutsaldoChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "RevalChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumBalanceDeltaChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedForPaymentChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "Tariff", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "NondeliveryDaysCount", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ServiceDeliveryDays", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountMeterReading", "Consumption", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("main.PersonalAccountMeterReading", "Consumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ServiceDeliveryDays", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "NondeliveryDaysCount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "Tariff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedForPaymentChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumBalanceDeltaChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "RevalChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumOutsaldoChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumInsaldoChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumTariffChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumInsaldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumOutsaldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumPayd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedForPayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumBalanceDelta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "Reval", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedTariffNondelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedTariff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseMeterReading", "Consumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedForPaymentChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumBalanceDeltaChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "RevalChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumOutsaldoChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumInsaldoChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumTariffChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumInsaldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumOutsaldo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumPayd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedForPayment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumBalanceDelta", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "Reval", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedTariffNondelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedTariff", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("system.Organization", "OrganizationDivisionExternalId");
        }
    }
}
