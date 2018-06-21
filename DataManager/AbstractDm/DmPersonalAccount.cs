using System.Threading.Tasks;
using OhDataManager.Entities.Main;

namespace OhDataManager.AbstractDm
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Менеджер данных "Лицевой счет"
    /// </summary>
    public abstract class DmPersonalAccount : IDataManager
    {
        /// <summary>
        /// Получение информации о лицевом счете
        /// </summary>
        /// <param name="personalAccountId"></param>
        public abstract PersonalAccount GetPersonalAccountInfo(long personalAccountId);
        
        /// <summary>
        /// Получение начислений лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="calculationDate"></param>
        public abstract Task<IEnumerable<PersonalAccountAccrual>> GetPersonalAccountAccruals(long personalAccountId, DateTime calculationDate);

        /// <summary>
        /// Получение платежей лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        public abstract Task<IEnumerable<PersonalAccountPayment>> GetPersonalAccountPayments(long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null);

        /// <summary>
        /// Получение социальных платежей лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        public abstract Task<IEnumerable<PersonalAccountSocialPayment>> GetPersonalAccountSocialPayments(long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null);

        /// <summary>
        /// Получение показаний приборов учета лицевого счета
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <param name="periodBegin"></param>
        /// <param name="periodEnd"></param>
        public abstract Task<IEnumerable<PersonalAccountMeterReading>> GetPersonalAccountMeterReadings(long personalAccountId, DateTime? periodBegin = null, DateTime? periodEnd = null);
        
        /// <summary>
        /// Добавление предварительной оплаты
        /// </summary>
        public abstract bool SavePayResultPre(UniPay pay);

        /// <summary>
        /// Обновить статус предварительной оплаты
        /// </summary>
        public abstract bool UpdatePayResultPre(string orderId, string[] arr);
    }
}
