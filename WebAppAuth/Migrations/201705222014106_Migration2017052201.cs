namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052201 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("main.HouseMeterReading", "ServiceId", "main.Service");
            DropForeignKey("main.PersonalAccountMeterReading", "ServiceId", "main.Service");
            DropIndex("main.HouseMeterReading", new[] { "ServiceId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "ServiceId" });
            AddColumn("main.HouseMeterReading", "MeterServiceId", c => c.Long(nullable: false));
            AddColumn("main.PersonalAccountMeterReading", "MeterServiceId", c => c.Long(nullable: false));
            CreateIndex("main.HouseMeterReading", "MeterServiceId");
            CreateIndex("main.PersonalAccountMeterReading", "MeterServiceId");
            AddForeignKey("main.HouseMeterReading", "MeterServiceId", "main.MeterService", "Id", cascadeDelete: true);
            AddForeignKey("main.PersonalAccountMeterReading", "MeterServiceId", "main.MeterService", "Id", cascadeDelete: true);
            DropColumn("main.HouseMeterReading", "ServiceId");
            DropColumn("main.PersonalAccountMeterReading", "ServiceId");
        }
        
        public override void Down()
        {
            AddColumn("main.PersonalAccountMeterReading", "ServiceId", c => c.Long(nullable: false));
            AddColumn("main.HouseMeterReading", "ServiceId", c => c.Long(nullable: false));
            DropForeignKey("main.PersonalAccountMeterReading", "MeterServiceId", "main.MeterService");
            DropForeignKey("main.HouseMeterReading", "MeterServiceId", "main.MeterService");
            DropIndex("main.PersonalAccountMeterReading", new[] { "MeterServiceId" });
            DropIndex("main.HouseMeterReading", new[] { "MeterServiceId" });
            DropColumn("main.PersonalAccountMeterReading", "MeterServiceId");
            DropColumn("main.HouseMeterReading", "MeterServiceId");
            CreateIndex("main.PersonalAccountMeterReading", "ServiceId");
            CreateIndex("main.HouseMeterReading", "ServiceId");
            AddForeignKey("main.PersonalAccountMeterReading", "ServiceId", "main.Service", "Id", cascadeDelete: true);
            AddForeignKey("main.HouseMeterReading", "ServiceId", "main.Service", "Id", cascadeDelete: true);
        }
    }
}
