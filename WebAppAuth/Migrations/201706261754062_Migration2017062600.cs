namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017062600 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("main.Claim", "DictClaimKind_Id", "system.DictClaimKind");
            DropIndex("main.Claim", new[] { "DictClaimKind_Id" });
            Sql("DROP INDEX IF EXISTS \"Claim_IX_DictClaimKind_Id\"");
            AddForeignKey("main.Claim", "DictClaimTypeId", "system.DictClaimType", "Id", cascadeDelete: true);
            //DropColumn("main.Claim", "DictClaimKind_Id");
        }
        
        public override void Down()
        {
            AddColumn("main.Claim", "DictClaimKind_Id", c => c.Long());
            DropForeignKey("main.Claim", "DictClaimTypeId", "system.DictClaimType");
            DropIndex("main.Claim", new[] { "DictClaimTypeId" });
            CreateIndex("main.Claim", "DictClaimKind_Id");
            AddForeignKey("main.Claim", "DictClaimKind_Id", "system.DictClaimKind", "Id");
        }
    }
}
