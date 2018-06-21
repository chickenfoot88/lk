namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017070801 : DbMigration
    {
        public override void Up()
        {
            AddColumn("main.Employee", "Surname", c => c.String());
            AddColumn("main.Employee", "Name", c => c.String());
            AddColumn("main.Employee", "DisplayName", c => c.String());
            AddColumn("main.Employee", "Patronymic", c => c.String());
            AddColumn("main.Employee", "PhoneNumber", c => c.String());
            AddColumn("main.Employee", "ImageData", c => c.Binary());
            AddColumn("main.Employee", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("main.Employee", "ImageMimeType");
            DropColumn("main.Employee", "ImageData");
            DropColumn("main.Employee", "PhoneNumber");
            DropColumn("main.Employee", "Patronymic");
            DropColumn("main.Employee", "DisplayName");
            DropColumn("main.Employee", "Name");
            DropColumn("main.Employee", "Surname");
        }
    }
}
