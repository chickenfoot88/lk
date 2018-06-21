using OhDataManager.Entities.ImportHelper;
using OhDataManager.Entities.Main;
using OhDataManager.Entities.System;
using WebAppAuth.Entities;

namespace WebAppAuth.Context
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ApplicationDbContext()
            : base("NpgsqlConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        /// <summary>
        /// Создание контекста
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public DbSet<ExchangeFile> ExchangeFiles { get; set; }
        public DbSet<ImportedFile> ImportedFiles { get; set; }
        public DbSet<ExportedFile> ExportedFiles { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationSubdivision> OrganizationSubdivision { get; set; }
        public DbSet<Abonent> Abonent { get; set; }
        public DbSet<PersonalAccount> PersonalAccount { get; set; }
        public DbSet<AddressSpace> AddressSpace { get; set; }
        public DbSet<House> House { get; set; }
        public DbSet<HouseAccrual> HouseAccrual { get; set; }
        public DbSet<PersonalAccountAccrual> PersonalAccountAccrual { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<MeterService> MeterService { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<PersonalAccountPayment> PersonalAccountPayment { get; set; }
        public DbSet<PersonalAccountPaymentRequisite> PersonalAccountPaymentRequisite { get; set; }
        public DbSet<PersonalAccountMeterReading> PersonalAccountMeterReading { get; set; }
        public DbSet<HouseMeterReading> HouseMeterReading { get; set; }
        public DbSet<PersonalAccountSocialPayment> PersonalAccountSocialPayment { get; set; }
        public DbSet<ImportAccrual> ImportAccrual { get; set; }
        public DbSet<ImportPayment> ImportPayment { get; set; }
        public DbSet<ImportHousingCharacteristic> ImportHousingCharacteristic { get; set; }
        public DbSet<ImportMeterReading> ImportMeterReading { get; set; }
        public DbSet<ImportPaymentRequisite> ImportPaymentRequisite { get; set; }
        public DbSet<ImportSocialPayment> ImportSocialPayment { get; set; }
        public DbSet<CalculationMonth> CalculationMonth { get; set; }
        public DbSet<UniPay> UniPay { get; set; }
        public DbSet<DictClaimType> DictClaimType { get; set; }
        public DbSet<DictClaimKind> DictClaimKind { get; set; }
        public DbSet<DictClaimStatusTransfer> DictClaimStatusTransfer { get; set; }
        public DbSet<AbonentClaim> AbonentClaim { get; set; }
        public DbSet<ClaimTransferHistory> ClaimTransferHistory { get; set; }
        public DbSet<AbonentClaimComment> AbonentClaimComment { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<HouseNotification> HouseNotification { get; set; }
        public DbSet<EmployeeOrganization> EmployeeOrganization { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<PositionClaimKind> PositionClaimKind { get; set; }
        public DbSet<ClaimKindTemplate> ClaimKindTemplate { get; set; }        
    }
}
