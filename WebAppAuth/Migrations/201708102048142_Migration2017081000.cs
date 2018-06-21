namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017081000 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.AbonentClaimComment", "InformationSource", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("main.AbonentClaimComment", "InformationSource");
        }
    }
}
