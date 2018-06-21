namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Migration2017052200 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.MeterService",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ServiceName = c.String(),
                    OuterSystemServiceId = c.Long(nullable: false),
                    OuterSystemBaseServiceId = c.Long(),
                    MeasureName = c.String(),
                    BaseServiceId = c.Long(),
                    ServiceGroupName = c.String(),
                    OrganizationId = c.Long(nullable: false),
                    CreationTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()")
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.Service", t => t.BaseServiceId)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.BaseServiceId)
                .Index(t => t.OrganizationId);

        }

        public override void Down()
        {
            DropForeignKey("main.MeterService", "OrganizationId", "system.Organization");
            DropForeignKey("main.MeterService", "BaseServiceId", "main.Service");
            DropIndex("main.MeterService", new[] {"OrganizationId"});
            DropIndex("main.MeterService", new[] {"BaseServiceId"});
            DropTable("main.MeterService");
        }
    }
}
