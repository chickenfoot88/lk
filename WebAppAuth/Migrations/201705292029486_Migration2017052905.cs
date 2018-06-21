namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052905 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("system.Claim", "CompleteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("system.Claim", "CompleteDate", c => c.DateTime(nullable: false));
        }
    }
}
