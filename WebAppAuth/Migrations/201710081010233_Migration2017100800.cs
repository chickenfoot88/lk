namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017100800 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.PositionClaimKind",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PositionId = c.Long(nullable: false),
                        DictClaimKindId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        ChangedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.DictClaimKind", t => t.DictClaimKindId, cascadeDelete: true)
                .ForeignKey("main.Position", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PositionId)
                .Index(t => t.DictClaimKindId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("main.PositionClaimKind", "PositionId", "main.Position");
            DropForeignKey("main.PositionClaimKind", "DictClaimKindId", "system.DictClaimKind");
            DropIndex("main.PositionClaimKind", new[] { "DictClaimKindId" });
            DropIndex("main.PositionClaimKind", new[] { "PositionId" });
            DropTable("main.PositionClaimKind");
        }
    }
}
