namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017112600 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.ClaimKindTemplate",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClaimKindId = c.Long(nullable: false),
                        Template = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        ChangedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.DictClaimKind", t => t.ClaimKindId, cascadeDelete: true)
                .Index(t => t.ClaimKindId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("main.ClaimKindTemplate", "ClaimKindId", "system.DictClaimKind");
            DropIndex("main.ClaimKindTemplate", new[] { "ClaimKindId" });
            DropTable("main.ClaimKindTemplate");
        }
    }
}
