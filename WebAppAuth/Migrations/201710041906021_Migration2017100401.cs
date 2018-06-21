namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017100401 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.Position",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        ChangedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("main.Employee", "PositionId", c => c.Long());
            CreateIndex("main.Employee", "PositionId");
            AddForeignKey("main.Employee", "PositionId", "main.Position", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("main.Employee", "PositionId", "main.Position");
            DropIndex("main.Employee", new[] { "PositionId" });
            DropColumn("main.Employee", "PositionId");
            DropTable("main.Position");
        }
    }
}
