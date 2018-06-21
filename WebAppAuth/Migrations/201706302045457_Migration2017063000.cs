namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017063000 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("main.Claim", "DictClaimTypeId", "system.DictClaimType");
            DropIndex("main.Claim", new[] { "DictClaimTypeId" });
            AddColumn("main.Claim", "DictClaimKind_Id", c => c.Long());
            CreateIndex("main.Claim", "DictClaimKind_Id");
            AddForeignKey("main.Claim", "DictClaimKind_Id", "system.DictClaimKind", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("main.Claim", "DictClaimKind_Id", "system.DictClaimKind");
            DropIndex("main.Claim", new[] { "DictClaimKind_Id" });
            DropColumn("main.Claim", "DictClaimKind_Id");
            CreateIndex("main.Claim", "DictClaimTypeId");
            AddForeignKey("main.Claim", "DictClaimTypeId", "system.DictClaimType", "Id", cascadeDelete: true);
        }
    }
}
