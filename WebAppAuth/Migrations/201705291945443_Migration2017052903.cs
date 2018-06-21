namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017052903 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "system.Claim",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonalAccountId = c.Long(nullable: false),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        HouseId = c.Long(nullable: false),
                        DictClaimTypeId = c.Long(nullable: false),
                        DictClaimKindId = c.Long(nullable: false),
                        Comment = c.String(),
                        ContactPhoneNumber = c.String(nullable: false),
                        ClaimNumber = c.String(nullable: false),
                        ClaimStatus = c.Int(nullable: false),
                        CompleteDate = c.DateTime(nullable: false),
                        EmployeeId = c.Long(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.DictClaimKind", t => t.DictClaimKindId, cascadeDelete: true)
                .ForeignKey("system.DictClaimType", t => t.DictClaimTypeId, cascadeDelete: true)
                .ForeignKey("main.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.PersonalAccount", t => t.PersonalAccountId, cascadeDelete: true)
                .Index(t => t.PersonalAccountId)
                .Index(t => t.HouseId)
                .Index(t => t.DictClaimTypeId)
                .Index(t => t.DictClaimKindId)
                .Index(t => t.EmployeeId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "system.DictClaimKind",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DictClaimTypeId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        Note = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.DictClaimType", t => t.DictClaimTypeId, cascadeDelete: true)
                .Index(t => t.DictClaimTypeId);
            
            CreateTable(
                "system.DictClaimType",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Note = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        IsCanEstimate = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "system.ClaimTransferHistory",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClaimId = c.Long(nullable: false),
                        PreviousStatus = c.Int(nullable: false),
                        NewStatus = c.Int(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Claim", t => t.ClaimId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ClaimId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "system.DictClaimStatusTransfer",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        NextStatus = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("system.ClaimTransferHistory", "OrganizationId", "system.Organization");
            DropForeignKey("system.ClaimTransferHistory", "ClaimId", "system.Claim");
            DropForeignKey("system.Claim", "PersonalAccountId", "main.PersonalAccount");
            DropForeignKey("system.Claim", "OrganizationId", "system.Organization");
            DropForeignKey("system.Claim", "HouseId", "main.House");
            DropForeignKey("system.Claim", "EmployeeId", "main.Employee");
            DropForeignKey("system.Claim", "DictClaimTypeId", "system.DictClaimType");
            DropForeignKey("system.Claim", "DictClaimKindId", "system.DictClaimKind");
            DropForeignKey("system.DictClaimKind", "DictClaimTypeId", "system.DictClaimType");
            DropIndex("system.ClaimTransferHistory", new[] { "OrganizationId" });
            DropIndex("system.ClaimTransferHistory", new[] { "ClaimId" });
            DropIndex("system.DictClaimKind", new[] { "DictClaimTypeId" });
            DropIndex("system.Claim", new[] { "OrganizationId" });
            DropIndex("system.Claim", new[] { "EmployeeId" });
            DropIndex("system.Claim", new[] { "DictClaimKindId" });
            DropIndex("system.Claim", new[] { "DictClaimTypeId" });
            DropIndex("system.Claim", new[] { "HouseId" });
            DropIndex("system.Claim", new[] { "PersonalAccountId" });
            DropTable("system.DictClaimStatusTransfer");
            DropTable("system.ClaimTransferHistory");
            DropTable("system.DictClaimType");
            DropTable("system.DictClaimKind");
            DropTable("system.Claim");
        }
    }
}
