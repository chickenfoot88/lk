using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OhDataManager.Entities.Main;
using OhDataManager.Enums;
using WebAppAuth.Entities;
using WebAppAuth.Models;

namespace WebAppAuth.Services.Data
{
    /// <summary>
    /// Интерфейс сервиса Абонент
    /// </summary>
    internal interface IAbonentService
    {
        /// <summary>
        /// Получение лицевого счета
        /// </summary>
        /// <returns></returns>
        Task<IServiceResult<PersonalAccount>> GetPersonalAccount(long paymentCode, string apartmentOwnerFullName,
            string apartmentNumber);

        /// <summary>
        /// Получить начисления по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="calculationDate"></param>
        /// <returns></returns>
        Task<IServiceResult<IEnumerable<PersonalAccountAccrual>>> GetAccruals(long personalAccountId,
            DateTime calculationDate);

        /// <summary>
        /// Получить платежи по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        Task<IServiceResult<IEnumerable<PersonalAccountPayment>>> GetPayments(long personalAccountId,
            DateTime? periodBegin = null, DateTime? periodEnd = null);

        /// <summary>
        /// Получить показания приборов учета по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        Task<IServiceResult<IEnumerable<PersonalAccountMeterReading>>> GetMeterReadings(long personalAccountId,
            DateTime? periodBegin = null, DateTime? periodEnd = null);

        /// <summary>
        /// Получить последние показания приборов учета по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <returns></returns>
        Task<IServiceResult<IEnumerable<PersonalAccountMeterReading>>> GetLastMeterReadings(long personalAccountId);

        /// <summary>
        /// Сохранить показаний прибора учета
        /// </summary>
        /// <param name="meterReadingId"></param>
        /// <param name="enteredValue"></param>
        /// <param name="informationSource"></param>
        /// <returns></returns>
        Task<IServiceResult<object>> EnterMeterReading(long meterReadingId, decimal enteredValue,
            EInformationSource informationSource);

        /// <summary>
        /// Получить социальные выплаты по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        /// <returns></returns>
        Task<IServiceResult<IEnumerable<PersonalAccountSocialPayment>>> GetSocialPayments(long personalAccountId,
            DateTime? periodBegin = null, DateTime? periodEnd = null);

        /// <summary>
        /// Получить заявки по лицевому счету
        /// </summary>
        /// <returns></returns>
        Task<IServiceResult<IEnumerable<AbonentClaim>>> GetClaims(
            long? personalAccountId,
            long? organizationId = null,
            DateTime? periodBegin = null, 
            DateTime? periodEnd = null,
            long? claimKindId = null,
            long? employeeId = null,
            EClaimStatus? claimStatus=null,
            long? houseId = null);
    }
}
