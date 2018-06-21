namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017070100 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("main.Claim", "DictClaimKind_Id", "system.DictClaimKind");
            DropIndex("main.Claim", new[] { "DictClaimKind_Id" });
            RenameColumn(table: "main.Claim", name: "DictClaimKind_Id", newName: "DictClaimKindId");
            AlterColumn("main.Claim", "DictClaimKindId", c => c.Long(nullable: false));
            CreateIndex("main.Claim", "DictClaimKindId");
            AddForeignKey("main.Claim", "DictClaimKindId", "system.DictClaimKind", "Id", cascadeDelete: true);
            DropColumn("main.Claim", "DictClaimTypeId");
        }
        
        public override void Down()
        {
            AddColumn("main.Claim", "DictClaimTypeId", c => c.Long(nullable: false));
            DropForeignKey("main.Claim", "DictClaimKindId", "system.DictClaimKind");
            DropIndex("main.Claim", new[] { "DictClaimKindId" });
            AlterColumn("main.Claim", "DictClaimKindId", c => c.Long());
            RenameColumn(table: "main.Claim", name: "DictClaimKindId", newName: "DictClaimKind_Id");
            CreateIndex("main.Claim", "DictClaimKind_Id");
            AddForeignKey("main.Claim", "DictClaimKind_Id", "system.DictClaimKind", "Id");
        }
    }
}
