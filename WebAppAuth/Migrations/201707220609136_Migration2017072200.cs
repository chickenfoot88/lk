namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017072200 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.AbonentClaim", "ImportedFileId", c => c.Long());
            CreateIndex("main.AbonentClaim", "ImportedFileId");
            AddForeignKey("main.AbonentClaim", "ImportedFileId", "main.ImportedFile", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("main.AbonentClaim", "ImportedFileId", "main.ImportedFile");
            DropIndex("main.AbonentClaim", new[] { "ImportedFileId" });
            DropColumn("main.AbonentClaim", "ImportedFileId");
        }
    }
}
