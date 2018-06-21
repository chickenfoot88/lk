namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017053000 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("system.Claim", "DictClaimKindId", "system.DictClaimKind");
            DropForeignKey("system.Claim", "HouseId", "main.House");
            MoveTable(name: "system.Claim", newSchema: "main");
            MoveTable(name: "system.ClaimTransferHistory", newSchema: "main");
            DropIndex("main.Claim", new[] { "HouseId" });
            DropIndex("main.Claim", new[] { "DictClaimKindId" });
            AddColumn("main.Claim", "Description", c => c.String(nullable: false));
            AlterColumn("main.Claim", "ApartmentFullAddress", c => c.String(nullable: false));
            DropColumn("main.Claim", "PersonalAccountNumber");
            DropColumn("main.Claim", "PaymentCode");
            DropColumn("main.Claim", "HouseId");
            DropColumn("main.Claim", "DictClaimKindId");
            DropColumn("main.Claim", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("main.Claim", "Comment", c => c.String());
            AddColumn("main.Claim", "DictClaimKindId", c => c.Long(nullable: false));
            AddColumn("main.Claim", "HouseId", c => c.Long(nullable: false));
            AddColumn("main.Claim", "PaymentCode", c => c.Long(nullable: false));
            AddColumn("main.Claim", "PersonalAccountNumber", c => c.Long(nullable: false));
            AlterColumn("main.Claim", "ApartmentFullAddress", c => c.String());
            DropColumn("main.Claim", "Description");
            CreateIndex("main.Claim", "DictClaimKindId");
            CreateIndex("main.Claim", "HouseId");
            AddForeignKey("system.Claim", "HouseId", "main.House", "Id", cascadeDelete: true);
            AddForeignKey("system.Claim", "DictClaimKindId", "system.DictClaimKind", "Id", cascadeDelete: true);
            MoveTable(name: "main.ClaimTransferHistory", newSchema: "system");
            MoveTable(name: "main.Claim", newSchema: "system");
        }
    }
}
