namespace WebAppAuth.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration2017051000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "main.Abonent",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PaymentCode = c.Long(nullable: false),
                        PersonalAccountId = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        ApplicationUserId = c.Int(nullable: false),
                        Surname = c.String(),
                        Name = c.String(),
                        Patronymic = c.String(),
                        HousePhoneNumber = c.String(),
                        MobilePhoneNumber = c.String(),
                        SmsNotificationAllowed = c.Boolean(nullable: false),
                        EmailNotificationAllowed = c.Boolean(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.PersonalAccount", t => t.PersonalAccountId, cascadeDelete: true)
                .Index(t => t.PersonalAccountId)
                .Index(t => t.ApplicationUserId, unique: true, name: "IX_Abonent_ApplicationUser")
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "system.Organization",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FullName = c.String(nullable: false),
                        OrganizationExternalId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "main.PersonalAccount",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        HouseFullAddress = c.String(),
                        HouseId = c.Long(nullable: false),
                        StreetFullAddress = c.String(),
                        AddressSpaceId = c.Long(nullable: false),
                        Town = c.String(),
                        Place = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        HousingNumber = c.String(),
                        ApartmentAddress = c.String(),
                        ApartmentNumber = c.String(),
                        RoomNumber = c.String(),
                        PorchNumber = c.Int(),
                        ApartmentOwnerFullName = c.String(),
                        ManagmentOrganizationName = c.String(),
                        ManagmentOrganizationId = c.Long(nullable: false),
                        ComfortableType = c.Int(),
                        PrivatiziedType = c.Int(),
                        FloorNumber = c.Int(),
                        ApartmentsCountOnFloor = c.Int(),
                        SquareApartment = c.Decimal(precision: 18, scale: 2),
                        SquareApartmentLiving = c.Decimal(precision: 18, scale: 2),
                        SquareApartmentHeating = c.Decimal(precision: 18, scale: 2),
                        SquareHouse = c.Decimal(precision: 18, scale: 2),
                        SquareHouseMop = c.Decimal(precision: 18, scale: 2),
                        SquareHouseHeating = c.Decimal(precision: 18, scale: 2),
                        CountResidents = c.Int(),
                        CountDeparture = c.Int(),
                        CountArrive = c.Int(),
                        CountRooms = c.Int(),
                        DataBankPrefix = c.String(),
                        PersonalAccountState = c.Int(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.AddressSpace", t => t.AddressSpaceId, cascadeDelete: true)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("main.ManagmentOrganization", t => t.ManagmentOrganizationId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.AddressSpaceId)
                .Index(t => t.ManagmentOrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.AddressSpace",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StreetFullAddress = c.String(nullable: false),
                        Town = c.String(),
                        Place = c.String(),
                        Street = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.House",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        HouseFullAddress = c.String(),
                        StreetFullAddress = c.String(),
                        AddressSpaceId = c.Long(nullable: false),
                        Town = c.String(),
                        Place = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        HousingNumber = c.String(),
                        SquareHouse = c.Decimal(precision: 18, scale: 2),
                        SquareHouseMop = c.Decimal(precision: 18, scale: 2),
                        SquareHouseHeating = c.Decimal(precision: 18, scale: 2),
                        DataBankPrefix = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.AddressSpace", t => t.AddressSpaceId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.AddressSpaceId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.ManagmentOrganization",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ManagmentOrganizationName = c.String(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.CalculationMonth",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.Employee",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicationUserId = c.Int(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId, unique: true, name: "IX_Employee_ApplicationUser")
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.ExchangeFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserIdentityId = c.Int(nullable: false),
                        FileName = c.String(),
                        StoredName = c.String(),
                        Progress = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StartTime = c.DateTime(nullable: false),
                        FinishTime = c.DateTime(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.ExportedFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExchangeFileId = c.Long(nullable: false),
                        UnloadType = c.Int(nullable: false),
                        UnloadStatus = c.Int(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.ExchangeFile", t => t.ExchangeFileId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ExchangeFileId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.HouseAccrual",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        HouseId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        ServiceName = c.String(nullable: false),
                        OuterSystemServiceId = c.Long(nullable: false),
                        OuterSystemBaseServiceId = c.Long(),
                        BaseServiceId = c.Long(),
                        MeasureName = c.String(),
                        ServiceGroupName = c.String(),
                        IndexNumber = c.Int(),
                        SupplierId = c.Long(nullable: false),
                        SupplierName = c.String(),
                        Consumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionChn = c.Decimal(precision: 18, scale: 2),
                        ConsumptionImd = c.Decimal(precision: 18, scale: 2),
                        ConsumptionNorm = c.Decimal(precision: 18, scale: 2),
                        ConsumptionHouse = c.Decimal(precision: 18, scale: 2),
                        ConsumptionHouseByApartments = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByApartmentsImd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByApartmentsNorm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByNonResidential = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionLift = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseChn = c.Decimal(precision: 18, scale: 2),
                        ConsumptionChmd = c.Decimal(precision: 18, scale: 2),
                        AccruedTariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedTariffNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reval = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumBalanceDelta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedForPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPayd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumOutsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumInsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumTariffChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumInsaldoChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumOutsaldoChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RevalChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumBalanceDeltaChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedForPaymentChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPaydChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedBySocNorm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StreetFullAddress = c.String(),
                        Town = c.String(),
                        Place = c.String(),
                        Street = c.String(),
                        HouseFullAddress = c.String(),
                        AddressSpaceId = c.Long(nullable: false),
                        HouseNumber = c.String(),
                        HousingNumber = c.String(),
                        SquareHouse = c.Decimal(precision: 18, scale: 2),
                        SquareHouseMop = c.Decimal(precision: 18, scale: 2),
                        SquareHouseHeating = c.Decimal(precision: 18, scale: 2),
                        DataBankPrefix = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.AddressSpace", t => t.AddressSpaceId, cascadeDelete: true)
                .ForeignKey("main.Service", t => t.BaseServiceId)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.Service", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("main.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.ServiceId)
                .Index(t => t.BaseServiceId)
                .Index(t => t.SupplierId)
                .Index(t => t.AddressSpaceId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.Service",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ServiceName = c.String(),
                        OuterSystemServiceId = c.Long(nullable: false),
                        OuterSystemBaseServiceId = c.Long(),
                        MeasureName = c.String(),
                        BaseServiceId = c.Long(),
                        ServiceGroupName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.Service", t => t.BaseServiceId)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.BaseServiceId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.Supplier",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SupplierName = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.HouseMeterReading",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        HouseId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        ServiceName = c.String(),
                        OuterSystemServiceId = c.Long(nullable: false),
                        OuterSystemBaseServiceId = c.Long(),
                        MeasureName = c.String(),
                        ServiceGroupName = c.String(),
                        BaseServiceId = c.Long(),
                        IndexNumber = c.Int(),
                        MeterDeviceNumber = c.String(),
                        OuterSystemMeterDeviceId = c.Long(nullable: false),
                        CalculationApplyingDate = c.DateTime(nullable: false),
                        Indication = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalculationApplyingDatePrevious = c.DateTime(),
                        IndicationPrevious = c.Decimal(precision: 18, scale: 2),
                        Multiplier = c.Decimal(precision: 18, scale: 2),
                        Consumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionAdditional = c.Decimal(precision: 18, scale: 2),
                        ConsumptionNonresidental = c.Decimal(precision: 18, scale: 2),
                        ConsumptionLift = c.Decimal(precision: 18, scale: 2),
                        Placement = c.String(),
                        Capacity = c.Int(nullable: false),
                        TypeName = c.String(),
                        VerificationDate = c.DateTime(),
                        VerificationDateNext = c.DateTime(),
                        StreetFullAddress = c.String(),
                        Town = c.String(),
                        Place = c.String(),
                        Street = c.String(),
                        HouseFullAddress = c.String(),
                        AddressSpaceId = c.Long(nullable: false),
                        HouseNumber = c.String(),
                        HousingNumber = c.String(),
                        SquareHouse = c.Decimal(precision: 18, scale: 2),
                        SquareHouseMop = c.Decimal(precision: 18, scale: 2),
                        SquareHouseHeating = c.Decimal(precision: 18, scale: 2),
                        EnteredDate = c.DateTime(),
                        EnteredValue = c.Decimal(precision: 18, scale: 2),
                        InformationSource = c.Int(),
                        ExportedFileId = c.Long(),
                        DataBankPrefix = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.AddressSpace", t => t.AddressSpaceId, cascadeDelete: true)
                .ForeignKey("main.Service", t => t.BaseServiceId)
                .ForeignKey("main.ExportedFile", t => t.ExportedFileId)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.ServiceId)
                .Index(t => t.BaseServiceId)
                .Index(t => t.AddressSpaceId)
                .Index(t => t.ExportedFileId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "import.importaccrual",
                c => new
                    {
                        SectionId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        OrganizationCode = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ServiceName = c.String(nullable: false, maxLength: 128),
                        MeasureName = c.String(nullable: false, maxLength: 128),
                        IndexNumber = c.Int(),
                        OuterSystemServiceId = c.Long(nullable: false),
                        OuterSystemBaseServiceId = c.Long(),
                        ServiceGroupName = c.String(),
                        Tariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Consumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionChn = c.Decimal(precision: 18, scale: 2),
                        ConsumptionImd = c.Decimal(precision: 18, scale: 2),
                        ConsumptionNorm = c.Decimal(precision: 18, scale: 2),
                        ConsumptionHouse = c.Decimal(precision: 18, scale: 2),
                        ConsumptionHouseByApartments = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByApartmentsImd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByApartmentsNorm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByNonResidential = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionLift = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseChn = c.Decimal(precision: 18, scale: 2),
                        ConsumptionChmd = c.Decimal(precision: 18, scale: 2),
                        AccruedTariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedTariffNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NondeliveryDaysCount = c.Decimal(precision: 18, scale: 2),
                        Reval = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumBalanceDelta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedForPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPayd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumOutsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumInsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumTariffChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumInsaldoChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumOutsaldoChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RevalChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumBalanceDeltaChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedForPaymentChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPaydChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedBySocNorm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CorrectionCoefficientImd = c.Decimal(precision: 18, scale: 2),
                        CorrectionCoefficientNorm = c.Decimal(precision: 18, scale: 2),
                        SupplierName = c.String(),
                        ServiceDeliveryDays = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NondeliveryDaysCountOnPast = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.SectionId, t.CalculationDate, t.OrganizationCode, t.PaymentCode, t.ServiceName, t.MeasureName });
            
            CreateTable(
                "main.ImportedFile",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExchangeFileId = c.Long(nullable: false),
                        LoadType = c.Int(nullable: false),
                        LoadStatus = c.Int(nullable: false),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.ExchangeFile", t => t.ExchangeFileId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.ExchangeFileId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "import.importhousingcharacteristic",
                c => new
                    {
                        SectionId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        OrganizationCode = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        PersonalAccountNumber = c.Long(nullable: false),
                        Town = c.String(),
                        Place = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        Complex = c.String(),
                        HousingNumber = c.String(),
                        ApartmentNumber = c.String(),
                        RoomNumber = c.String(),
                        PorchNumber = c.Int(),
                        ApartmentOwnerFullName = c.String(),
                        ManagmentOrganizationName = c.String(),
                        ManagmentOrganizationNameId = c.Long(nullable: false),
                        ComfortableType = c.Int(),
                        PrivatiziedType = c.Int(),
                        FloorNumber = c.Int(),
                        ApartmentsCountOnFloor = c.Int(),
                        SquareApartment = c.Decimal(precision: 18, scale: 2),
                        SquareApartmentLiving = c.Decimal(precision: 18, scale: 2),
                        SquareApartmentHeating = c.Decimal(precision: 18, scale: 2),
                        SquareHouse = c.Decimal(precision: 18, scale: 2),
                        SquareHouseMop = c.Decimal(precision: 18, scale: 2),
                        SquareHouseHeating = c.Decimal(precision: 18, scale: 2),
                        CountResidents = c.Int(),
                        CountDeparture = c.Int(),
                        CountArrive = c.Int(),
                        CountRooms = c.Int(),
                        DataBankPrefix = c.String(),
                        PersonalAccountState = c.Int(),
                    })
                .PrimaryKey(t => new { t.SectionId, t.CalculationDate, t.OrganizationCode, t.PaymentCode });
            
            CreateTable(
                "import.importmeterreading",
                c => new
                    {
                        SectionId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        OrganizationCode = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ServiceName = c.String(nullable: false, maxLength: 128),
                        MeasureName = c.String(nullable: false, maxLength: 128),
                        IndexNumber = c.Int(),
                        MeterReadingType = c.Int(nullable: false),
                        MeterDeviceNumber = c.String(nullable: false, maxLength: 128),
                        OuterSystemMeterDeviceId = c.Long(nullable: false),
                        CalculationApplyingDate = c.DateTime(nullable: false),
                        Indication = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalculationApplyingDatePrevious = c.DateTime(),
                        IndicationPrevious = c.Decimal(precision: 18, scale: 2),
                        Multiplier = c.Decimal(precision: 18, scale: 2),
                        Consumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionAdditional = c.Decimal(precision: 18, scale: 2),
                        ConsumptionNonresidental = c.Decimal(precision: 18, scale: 2),
                        ConsumptionLift = c.Decimal(precision: 18, scale: 2),
                        Placement = c.String(),
                        Capacity = c.Int(nullable: false),
                        TypeName = c.String(),
                        VerificationDate = c.DateTime(),
                        VerificationDateNext = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.CalculationDate, t.OrganizationCode, t.PaymentCode, t.ServiceName, t.MeasureName, t.MeterReadingType, t.MeterDeviceNumber, t.OuterSystemMeterDeviceId });
            
            CreateTable(
                "import.importpayment",
                c => new
                    {
                        SectionId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        OrganizationCode = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        CalculationApplyingDate = c.DateTime(nullable: false),
                        PaydCalculationMonth = c.DateTime(nullable: false),
                        SumPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentPlacement = c.String(),
                    })
                .PrimaryKey(t => new { t.CalculationDate, t.OrganizationCode, t.PaymentCode, t.PaymentDate, t.CalculationApplyingDate, t.PaydCalculationMonth, t.SumPayment });
            
            CreateTable(
                "main.PersonalAccountAccrual",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        PersonalAccountId = c.Long(nullable: false),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        HouseId = c.Long(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        ServiceName = c.String(),
                        OuterSystemServiceId = c.Long(nullable: false),
                        OuterSystemBaseServiceId = c.Long(),
                        MeasureName = c.String(),
                        ServiceGroupName = c.String(),
                        BaseServiceId = c.Long(),
                        SupplierId = c.Long(nullable: false),
                        SupplierName = c.String(),
                        Consumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionChn = c.Decimal(precision: 18, scale: 2),
                        ConsumptionImd = c.Decimal(precision: 18, scale: 2),
                        ConsumptionNorm = c.Decimal(precision: 18, scale: 2),
                        ConsumptionHouse = c.Decimal(precision: 18, scale: 2),
                        ConsumptionHouseByApartments = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByApartmentsImd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByApartmentsNorm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseByNonResidential = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionLift = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionHouseChn = c.Decimal(precision: 18, scale: 2),
                        ConsumptionChmd = c.Decimal(precision: 18, scale: 2),
                        AccruedTariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedTariffNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionNondelivery = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reval = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumBalanceDelta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedForPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPayd = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumOutsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumInsaldo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumTariffChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumInsaldoChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumOutsaldoChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RevalChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumBalanceDeltaChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedForPaymentChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPaydChn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedBySocNorm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IndexNumber = c.Int(),
                        Tariff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NondeliveryDaysCount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CorrectionCoefficientImd = c.Decimal(precision: 18, scale: 2),
                        CorrectionCoefficientNorm = c.Decimal(precision: 18, scale: 2),
                        ServiceDeliveryDays = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NondeliveryDaysCountOnPast = c.Decimal(precision: 18, scale: 2),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.Service", t => t.BaseServiceId)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.PersonalAccount", t => t.PersonalAccountId, cascadeDelete: true)
                .ForeignKey("main.Service", t => t.ServiceId, cascadeDelete: true)
                .ForeignKey("main.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.PersonalAccountId)
                .Index(t => t.HouseId)
                .Index(t => t.ServiceId)
                .Index(t => t.BaseServiceId)
                .Index(t => t.SupplierId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.PersonalAccountMeterReading",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonalAccountId = c.Long(nullable: false),
                        HouseId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        ServiceId = c.Long(nullable: false),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        ServiceName = c.String(),
                        OuterSystemServiceId = c.Long(nullable: false),
                        OuterSystemBaseServiceId = c.Long(),
                        MeasureName = c.String(),
                        ServiceGroupName = c.String(),
                        BaseServiceId = c.Long(),
                        IndexNumber = c.Int(),
                        MeterDeviceNumber = c.String(),
                        OuterSystemMeterDeviceId = c.Long(nullable: false),
                        CalculationApplyingDate = c.DateTime(nullable: false),
                        Indication = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalculationApplyingDatePrevious = c.DateTime(),
                        IndicationPrevious = c.Decimal(precision: 18, scale: 2),
                        Multiplier = c.Decimal(precision: 18, scale: 2),
                        Consumption = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ConsumptionAdditional = c.Decimal(precision: 18, scale: 2),
                        ConsumptionNonresidental = c.Decimal(precision: 18, scale: 2),
                        ConsumptionLift = c.Decimal(precision: 18, scale: 2),
                        Placement = c.String(),
                        Capacity = c.Int(nullable: false),
                        TypeName = c.String(),
                        VerificationDate = c.DateTime(),
                        VerificationDateNext = c.DateTime(),
                        EnteredDate = c.DateTime(),
                        EnteredValue = c.Decimal(precision: 18, scale: 2),
                        InformationSource = c.Int(),
                        ExportedFileId = c.Long(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.Service", t => t.BaseServiceId)
                .ForeignKey("main.ExportedFile", t => t.ExportedFileId)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.PersonalAccount", t => t.PersonalAccountId, cascadeDelete: true)
                .ForeignKey("main.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.PersonalAccountId)
                .Index(t => t.HouseId)
                .Index(t => t.ServiceId)
                .Index(t => t.BaseServiceId)
                .Index(t => t.ExportedFileId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.PersonalAccountPayment",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CalculationDate = c.DateTime(nullable: false),
                        PersonalAccountId = c.Long(nullable: false),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        HouseId = c.Long(nullable: false),
                        PaymentDate = c.DateTime(nullable: false),
                        CalculationApplyingDate = c.DateTime(nullable: false),
                        PaydCalculationMonth = c.DateTime(nullable: false),
                        SumPayment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PaymentPlacement = c.String(),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .ForeignKey("main.PersonalAccount", t => t.PersonalAccountId, cascadeDelete: true)
                .Index(t => t.PersonalAccountId)
                .Index(t => t.HouseId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "main.SocialProtectionInfo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PersonalAccountNumber = c.Long(nullable: false),
                        PaymentCode = c.Long(nullable: false),
                        ApartmentFullAddress = c.String(),
                        HouseId = c.Long(nullable: false),
                        CalculationDate = c.DateTime(nullable: false),
                        PersonFullName = c.String(),
                        ExpenceId = c.Long(nullable: false),
                        ExpenceName = c.String(),
                        ExpenceGroupName = c.String(),
                        SumAccrued = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumPayoff = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpenceEndDate = c.DateTime(nullable: false),
                        PaymentPlacement = c.String(),
                        BalanceIn = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumDelta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SumRecalc = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrganizationId = c.Long(nullable: false),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("main.House", t => t.HouseId, cascadeDelete: true)
                .ForeignKey("system.Organization", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.HouseId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "main.UniPay",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(),
                        Currency = c.String(),
                        ShopIdp = c.String(),
                        OrderIdp = c.String(),
                        SubtotalP = c.String(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        WithdrawAmount = c.Decimal(precision: 18, scale: 2),
                        Email = c.String(),
                        Inn = c.String(),
                        Address = c.String(),
                        Signature = c.String(),
                        CustomerIdp = c.String(),
                        Comment = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        LifeTime = c.Int(),
                        UrlReturnOk = c.String(),
                        UrlReturnNo = c.String(),
                        CreationTime = c.DateTime(nullable: false, defaultValueSql:"NOW()"),
                        ChangedTime = c.DateTime(nullable: false, defaultValueSql: "NOW()"),
                    })
                .PrimaryKey(t => t.Id);
            

            Sql(
                          $@"
INSERT INTO dbo.""AspNetRoles"" (""Name"")  VALUES 
('{AppConstants.RoleAdministrator}'),
('{AppConstants.RoleAbonent}'),
('{AppConstants.RoleDirector}'),
('{AppConstants.RoleEmployee}');");
            Sql("CREATE SEQUENCE  seq_order_id;");
            Sql("ALTER TABLE import.importpayment DROP CONSTRAINT IF EXISTS \"PK_import.importpayment\";");

        }
        
        public override void Down()
        {
            DropForeignKey("main.SocialProtectionInfo", "OrganizationId", "system.Organization");
            DropForeignKey("main.SocialProtectionInfo", "HouseId", "main.House");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("main.PersonalAccountPayment", "PersonalAccountId", "main.PersonalAccount");
            DropForeignKey("main.PersonalAccountPayment", "OrganizationId", "system.Organization");
            DropForeignKey("main.PersonalAccountPayment", "HouseId", "main.House");
            DropForeignKey("main.PersonalAccountMeterReading", "ServiceId", "main.Service");
            DropForeignKey("main.PersonalAccountMeterReading", "PersonalAccountId", "main.PersonalAccount");
            DropForeignKey("main.PersonalAccountMeterReading", "OrganizationId", "system.Organization");
            DropForeignKey("main.PersonalAccountMeterReading", "HouseId", "main.House");
            DropForeignKey("main.PersonalAccountMeterReading", "ExportedFileId", "main.ExportedFile");
            DropForeignKey("main.PersonalAccountMeterReading", "BaseServiceId", "main.Service");
            DropForeignKey("main.PersonalAccountAccrual", "SupplierId", "main.Supplier");
            DropForeignKey("main.PersonalAccountAccrual", "ServiceId", "main.Service");
            DropForeignKey("main.PersonalAccountAccrual", "PersonalAccountId", "main.PersonalAccount");
            DropForeignKey("main.PersonalAccountAccrual", "OrganizationId", "system.Organization");
            DropForeignKey("main.PersonalAccountAccrual", "HouseId", "main.House");
            DropForeignKey("main.PersonalAccountAccrual", "BaseServiceId", "main.Service");
            DropForeignKey("main.ImportedFile", "OrganizationId", "system.Organization");
            DropForeignKey("main.ImportedFile", "ExchangeFileId", "main.ExchangeFile");
            DropForeignKey("main.HouseMeterReading", "ServiceId", "main.Service");
            DropForeignKey("main.HouseMeterReading", "OrganizationId", "system.Organization");
            DropForeignKey("main.HouseMeterReading", "HouseId", "main.House");
            DropForeignKey("main.HouseMeterReading", "ExportedFileId", "main.ExportedFile");
            DropForeignKey("main.HouseMeterReading", "BaseServiceId", "main.Service");
            DropForeignKey("main.HouseMeterReading", "AddressSpaceId", "main.AddressSpace");
            DropForeignKey("main.HouseAccrual", "SupplierId", "main.Supplier");
            DropForeignKey("main.Supplier", "OrganizationId", "system.Organization");
            DropForeignKey("main.HouseAccrual", "ServiceId", "main.Service");
            DropForeignKey("main.HouseAccrual", "OrganizationId", "system.Organization");
            DropForeignKey("main.HouseAccrual", "HouseId", "main.House");
            DropForeignKey("main.HouseAccrual", "BaseServiceId", "main.Service");
            DropForeignKey("main.Service", "OrganizationId", "system.Organization");
            DropForeignKey("main.Service", "BaseServiceId", "main.Service");
            DropForeignKey("main.HouseAccrual", "AddressSpaceId", "main.AddressSpace");
            DropForeignKey("main.ExportedFile", "OrganizationId", "system.Organization");
            DropForeignKey("main.ExportedFile", "ExchangeFileId", "main.ExchangeFile");
            DropForeignKey("main.ExchangeFile", "OrganizationId", "system.Organization");
            DropForeignKey("main.Employee", "OrganizationId", "system.Organization");
            DropForeignKey("main.Employee", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("main.CalculationMonth", "OrganizationId", "system.Organization");
            DropForeignKey("main.Abonent", "PersonalAccountId", "main.PersonalAccount");
            DropForeignKey("main.PersonalAccount", "OrganizationId", "system.Organization");
            DropForeignKey("main.PersonalAccount", "ManagmentOrganizationId", "main.ManagmentOrganization");
            DropForeignKey("main.ManagmentOrganization", "OrganizationId", "system.Organization");
            DropForeignKey("main.PersonalAccount", "HouseId", "main.House");
            DropForeignKey("main.House", "OrganizationId", "system.Organization");
            DropForeignKey("main.House", "AddressSpaceId", "main.AddressSpace");
            DropForeignKey("main.PersonalAccount", "AddressSpaceId", "main.AddressSpace");
            DropForeignKey("main.AddressSpace", "OrganizationId", "system.Organization");
            DropForeignKey("main.Abonent", "OrganizationId", "system.Organization");
            DropForeignKey("main.Abonent", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("main.SocialProtectionInfo", new[] { "OrganizationId" });
            DropIndex("main.SocialProtectionInfo", new[] { "HouseId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("main.PersonalAccountPayment", new[] { "OrganizationId" });
            DropIndex("main.PersonalAccountPayment", new[] { "HouseId" });
            DropIndex("main.PersonalAccountPayment", new[] { "PersonalAccountId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "OrganizationId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "ExportedFileId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "BaseServiceId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "ServiceId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "HouseId" });
            DropIndex("main.PersonalAccountMeterReading", new[] { "PersonalAccountId" });
            DropIndex("main.PersonalAccountAccrual", new[] { "OrganizationId" });
            DropIndex("main.PersonalAccountAccrual", new[] { "SupplierId" });
            DropIndex("main.PersonalAccountAccrual", new[] { "BaseServiceId" });
            DropIndex("main.PersonalAccountAccrual", new[] { "ServiceId" });
            DropIndex("main.PersonalAccountAccrual", new[] { "HouseId" });
            DropIndex("main.PersonalAccountAccrual", new[] { "PersonalAccountId" });
            DropIndex("main.ImportedFile", new[] { "OrganizationId" });
            DropIndex("main.ImportedFile", new[] { "ExchangeFileId" });
            DropIndex("main.HouseMeterReading", new[] { "OrganizationId" });
            DropIndex("main.HouseMeterReading", new[] { "ExportedFileId" });
            DropIndex("main.HouseMeterReading", new[] { "AddressSpaceId" });
            DropIndex("main.HouseMeterReading", new[] { "BaseServiceId" });
            DropIndex("main.HouseMeterReading", new[] { "ServiceId" });
            DropIndex("main.HouseMeterReading", new[] { "HouseId" });
            DropIndex("main.Supplier", new[] { "OrganizationId" });
            DropIndex("main.Service", new[] { "OrganizationId" });
            DropIndex("main.Service", new[] { "BaseServiceId" });
            DropIndex("main.HouseAccrual", new[] { "OrganizationId" });
            DropIndex("main.HouseAccrual", new[] { "AddressSpaceId" });
            DropIndex("main.HouseAccrual", new[] { "SupplierId" });
            DropIndex("main.HouseAccrual", new[] { "BaseServiceId" });
            DropIndex("main.HouseAccrual", new[] { "ServiceId" });
            DropIndex("main.HouseAccrual", new[] { "HouseId" });
            DropIndex("main.ExportedFile", new[] { "OrganizationId" });
            DropIndex("main.ExportedFile", new[] { "ExchangeFileId" });
            DropIndex("main.ExchangeFile", new[] { "OrganizationId" });
            DropIndex("main.Employee", new[] { "OrganizationId" });
            DropIndex("main.Employee", "IX_Employee_ApplicationUser");
            DropIndex("main.CalculationMonth", new[] { "OrganizationId" });
            DropIndex("main.ManagmentOrganization", new[] { "OrganizationId" });
            DropIndex("main.House", new[] { "OrganizationId" });
            DropIndex("main.House", new[] { "AddressSpaceId" });
            DropIndex("main.AddressSpace", new[] { "OrganizationId" });
            DropIndex("main.PersonalAccount", new[] { "OrganizationId" });
            DropIndex("main.PersonalAccount", new[] { "ManagmentOrganizationId" });
            DropIndex("main.PersonalAccount", new[] { "AddressSpaceId" });
            DropIndex("main.PersonalAccount", new[] { "HouseId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("main.Abonent", new[] { "OrganizationId" });
            DropIndex("main.Abonent", "IX_Abonent_ApplicationUser");
            DropIndex("main.Abonent", new[] { "PersonalAccountId" });
            DropTable("main.UniPay");
            DropTable("main.SocialProtectionInfo");
            DropTable("dbo.AspNetRoles");
            DropTable("main.PersonalAccountPayment");
            DropTable("main.PersonalAccountMeterReading");
            DropTable("main.PersonalAccountAccrual");
            DropTable("import.importpayment");
            DropTable("import.importmeterreading");
            DropTable("import.importhousingcharacteristic");
            DropTable("main.ImportedFile");
            DropTable("import.importaccrual");
            DropTable("main.HouseMeterReading");
            DropTable("main.Supplier");
            DropTable("main.Service");
            DropTable("main.HouseAccrual");
            DropTable("main.ExportedFile");
            DropTable("main.ExchangeFile");
            DropTable("main.Employee");
            DropTable("main.CalculationMonth");
            DropTable("main.ManagmentOrganization");
            DropTable("main.House");
            DropTable("main.AddressSpace");
            DropTable("main.PersonalAccount");
            DropTable("system.Organization");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("main.Abonent");
            
        }
    }
}
