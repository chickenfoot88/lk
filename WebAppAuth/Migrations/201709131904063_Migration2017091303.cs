namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017091303 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("main.HouseAccrual", "Consumption", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByApartments", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByApartmentsImd", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByApartmentsNorm", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByNonResidential", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionLift", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumNondelivery", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionNondelivery", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumPaydChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedBySocNorm", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "Consumption", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByApartments", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByApartmentsImd", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByApartmentsNorm", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByNonResidential", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionLift", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumNondelivery", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionNondelivery", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumPaydChn", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "AccruedBySocNorm", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("main.PersonalAccountAccrual", "AccruedBySocNorm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumPaydChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionNondelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "SumNondelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionLift", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByNonResidential", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByApartmentsNorm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByApartmentsImd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "ConsumptionHouseByApartments", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.PersonalAccountAccrual", "Consumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "AccruedBySocNorm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumPaydChn", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionNondelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "SumNondelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionLift", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByNonResidential", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByApartmentsNorm", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByApartmentsImd", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "ConsumptionHouseByApartments", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("main.HouseAccrual", "Consumption", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
