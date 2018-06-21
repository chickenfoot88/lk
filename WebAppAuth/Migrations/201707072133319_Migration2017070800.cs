namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017070800 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "main.Claim", newName: "AbonentClaim");
            DropForeignKey("main.ClaimTransferHistory", "ClaimId", "main.Claim");
            DropIndex("main.ClaimTransferHistory", new[] { "ClaimId" });
            AddColumn("main.AbonentClaim", "CreatorId", c => c.Long());
            AddColumn("main.ClaimTransferHistory", "AbonentClaim_Id", c => c.Long());
            CreateIndex("main.AbonentClaim", "CreatorId");
            CreateIndex("main.ClaimTransferHistory", "AbonentClaim_Id");
            AddForeignKey("main.AbonentClaim", "CreatorId", "main.Abonent", "Id");
            AddForeignKey("main.ClaimTransferHistory", "AbonentClaim_Id", "main.AbonentClaim", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("main.ClaimTransferHistory", "AbonentClaim_Id", "main.AbonentClaim");
            DropForeignKey("main.AbonentClaim", "CreatorId", "main.Abonent");
            DropIndex("main.ClaimTransferHistory", new[] { "AbonentClaim_Id" });
            DropIndex("main.AbonentClaim", new[] { "CreatorId" });
            DropColumn("main.ClaimTransferHistory", "AbonentClaim_Id");
            DropColumn("main.AbonentClaim", "CreatorId");
            CreateIndex("main.ClaimTransferHistory", "ClaimId");
            AddForeignKey("main.ClaimTransferHistory", "ClaimId", "main.Claim", "Id", cascadeDelete: true);
            RenameTable(name: "main.AbonentClaim", newName: "Claim");
        }
    }
}
