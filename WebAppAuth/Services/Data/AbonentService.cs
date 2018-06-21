using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DependencyResolver;
using OhDataManager;
using OhDataManager.Entities.Main;
using OhDataManager.Enums;
using WebAppAuth.Context;
using WebAppAuth.Entities;
using WebAppAuth.Models;

namespace WebAppAuth.Services.Data
{
    /// <summary>
    /// Сервис Абонент
    /// </summary>
    public class AbonentService : IAbonentService
    {
        /// <summary>
        /// Получение лицевого счета
        /// </summary>
        /// <returns></returns>
        public async Task<IServiceResult<PersonalAccount>> GetPersonalAccount(long paymentCode,
            string apartmentOwnerFullName,
            string apartmentNumber)
        {
            using (var db = new ApplicationDbContext())
            {
                var personalAccount = await
                    db.PersonalAccount
                        .FirstOrDefaultAsync(x => x.PaymentCode == paymentCode);

                if (personalAccount == null)
                    return new ServiceResult<PersonalAccount> {Success = false, InfoMessage = "Лицевой счет не найден"};

                //обрезаем ФИО
                apartmentOwnerFullName = apartmentOwnerFullName.Length >= 5
                    ? apartmentOwnerFullName.Substring(0, 5).ToLower()
                    : apartmentOwnerFullName.ToLower();

                if (
                    personalAccount.ApartmentOwnerFullName.StartsWith(apartmentOwnerFullName,
                        StringComparison.OrdinalIgnoreCase)
                    && personalAccount.ApartmentNumber == apartmentNumber
                    )
                {
                    return new ServiceResult<PersonalAccount> {Success = true, Content = personalAccount};
                }
                return new ServiceResult<PersonalAccount> {Success = false, InfoMessage = "Лицевой счет не найден"};
            }
        }

        /// <summary>
        /// Получить начисления по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        public async Task<IServiceResult<IEnumerable<PersonalAccountAccrual>>> GetAccruals(long personalAccountId,
            DateTime calculationDate)
        {
            var data = await DependencyProvider
                .Resolve<DataManagersFactory>()
                .GetPersonalAccountDataManager()
                .GetPersonalAccountAccruals(personalAccountId, calculationDate);

            return new ServiceResult<IEnumerable<PersonalAccountAccrual>>
            {
                Success = true,
                Content = data
            };
        }

        /// <summary>
        /// Получить платежи по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        public async Task<IServiceResult<IEnumerable<PersonalAccountPayment>>> GetPayments(long personalAccountId,
            DateTime? periodBegin = null, DateTime? periodEnd = null)
        {
            var data = (await DependencyProvider
                .Resolve<DataManagersFactory>()
                .GetPersonalAccountDataManager()
                .GetPersonalAccountPayments(personalAccountId, periodBegin, periodEnd))
                .OrderByDescending(x => x.CalculationDate);

            return new ServiceResult<IEnumerable<PersonalAccountPayment>>
            {
                Success = true,
                Content = data
            };
        }

        /// <summary>
        /// Получить показания приборов учета по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        public async Task<IServiceResult<IEnumerable<PersonalAccountMeterReading>>> GetMeterReadings(
            long personalAccountId,
            DateTime? periodBegin = null, DateTime? periodEnd = null)
        {
            var data = await DependencyProvider
                .Resolve<DataManagersFactory>()
                .GetPersonalAccountDataManager()
                .GetPersonalAccountMeterReadings(personalAccountId, periodBegin, periodEnd);

            return new ServiceResult<IEnumerable<PersonalAccountMeterReading>>
            {
                Success = true,
                Content = data
            };
        }

        /// <summary>
        /// Получить последние показания приборов учета по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <returns></returns>
        public async Task<IServiceResult<IEnumerable<PersonalAccountMeterReading>>> GetLastMeterReadings(long personalAccountId)
        {
            using (var context = new ApplicationDbContext())
            {
                var personalAccount = await context.PersonalAccount.FindAsync(personalAccountId);

                if (personalAccount == null)
                    return new ServiceResult<IEnumerable<PersonalAccountMeterReading>>()
                    {
                        Success = false,
                        InfoMessage = "Лицевой счет не найден"
                    };

                var currentCalculationMonth =
                    context.CalculationMonth.FirstOrDefault(
                        x => x.IsCurrent && x.OrganizationId == personalAccount.OrganizationId);

                if (currentCalculationMonth == null)
                    return new ServiceResult<IEnumerable<PersonalAccountMeterReading>>()
                    {
                        Success = false,
                        InfoMessage = "Нет информации о расчетном месяце"
                    };

                return await GetMeterReadings(personalAccountId, currentCalculationMonth.CalculationDate,
                    currentCalculationMonth.CalculationDate);
            }
        }


        /// <summary>
        /// Сохранить показаний прибора учета
        /// </summary>
        /// <param name="meterReadingId"></param>
        /// <param name="enteredValue"></param>
        /// <param name="informationSource"></param>
        /// <returns></returns>
        public async Task<IServiceResult<object>> EnterMeterReading(long meterReadingId, decimal enteredValue, EInformationSource informationSource)
        {
            using (var context = new ApplicationDbContext())
            {
                var meterReading = context.PersonalAccountMeterReading.Find(meterReadingId);
                if (meterReading == null)
                    return new ServiceResult<object>
                    {
                        Success = false,
                        InfoMessage = "Прибор учета не найден"
                    };

                meterReading.EnteredValue = enteredValue;
                meterReading.ChangedTime = DateTime.Now;
                meterReading.EnteredDate = DateTime.Now;
                meterReading.InformationSource = informationSource;
                await context.SaveChangesAsync();
            }
            return new ServiceResult<object>
            {
                Success = true,
                InfoMessage = "Показание успешно сохранено"
            };
        }

        /// <summary>
        /// Получить социальные выплаты по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        public async Task<IServiceResult<IEnumerable<PersonalAccountSocialPayment>>> GetSocialPayments(
            long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null)
        {
            var data = (await DependencyProvider
                .Resolve<DataManagersFactory>()
                .GetPersonalAccountDataManager()
                .GetPersonalAccountSocialPayments(personalAccountId, periodBegin, periodEnd))
                .OrderByDescending(x => x.CalculationDate);

            return new ServiceResult<IEnumerable<PersonalAccountSocialPayment>>
            {
                Success = true,
                Content = data
            };
        }

        /// <summary>
        /// Получить заявки по лицевому счету
        /// </summary>
        /// <returns></returns>
        public async Task<IServiceResult<IEnumerable<AbonentClaim>>> GetClaims(
            long? personalAccountId, 
            long? organizationId = null, 
            DateTime? periodBegin = null, 
            DateTime? periodEnd = null,
            long? claimKindId=null,
            long? employeeId=null,
            EClaimStatus? claimStatus = null,
            long? houseId = null)
        {
            using (var context = new ApplicationDbContext())
            {
                var data = await context.AbonentClaim
                    .Include(x => x.DictClaimKind)
                    .Include(x => x.Employee)
                    .Include(x=>x.PersonalAccount)
                    .WhereIf(personalAccountId.HasValue && personalAccountId != 0,
                        x => x.PersonalAccountId == personalAccountId)
                    .WhereIf(organizationId.HasValue && organizationId != 0, x => x.OrganizationId == organizationId)
                    .WhereIf(periodBegin.HasValue, x => x.CreationTime >= periodBegin.Value)
                    .WhereIf(periodEnd.HasValue, x => x.CreationTime <= periodEnd.Value)
                    .WhereIf(claimKindId.HasValue && claimKindId != 0, x => x.DictClaimKindId == claimKindId)
                    .WhereIf(employeeId.HasValue && employeeId != 0, x => x.EmployeeId == employeeId)
                    .WhereIf(houseId.HasValue && houseId != 0, x => x.PersonalAccount.HouseId == houseId)
                    //.WhereIf(claimStatus.HasValue && claimStatus != EClaimStatus.Undefined, x => (int)x.ClaimStatus == (int)claimStatus.Value)
                    .ToListAsync();

                return new ServiceResult<IEnumerable<AbonentClaim>>
                {
                    Success = true,
                    Content = data.ToList()
                };
            }
        }
    }
}