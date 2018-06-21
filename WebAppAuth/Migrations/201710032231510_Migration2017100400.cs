namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017100400 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("system.DictClaimKind", "DictClaimTypeId", "system.DictClaimType");
            DropIndex("system.DictClaimKind", new[] { "DictClaimTypeId" });
            AlterColumn("system.DictClaimKind", "DictClaimTypeId", c => c.Long());
            CreateIndex("system.DictClaimKind", "DictClaimTypeId");
            AddForeignKey("system.DictClaimKind", "DictClaimTypeId", "system.DictClaimType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("system.DictClaimKind", "DictClaimTypeId", "system.DictClaimType");
            DropIndex("system.DictClaimKind", new[] { "DictClaimTypeId" });
            AlterColumn("system.DictClaimKind", "DictClaimTypeId", c => c.Long(nullable: false));
            CreateIndex("system.DictClaimKind", "DictClaimTypeId");
            AddForeignKey("system.DictClaimKind", "DictClaimTypeId", "system.DictClaimType", "Id", cascadeDelete: true);
        }
    }
}
