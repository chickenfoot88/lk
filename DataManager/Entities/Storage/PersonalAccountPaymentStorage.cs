using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Платеж
    /// </summary>
    [Table("PersonalAccountPayment", Schema = "storage")]
    public class PersonalAccountPaymentStorage : BaseOhEntity, IPersonalAccount, IBillInfo
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual PersonalAccountStorage PersonalAccount { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual long PersonalAccountId { get; set; }


        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public long PersonalAccountNumber { get; set; }

        /// <summary>
        /// Платежный код
        /// </summary>
        public long PaymentCode { get; set; }

        /// <summary>
        /// Полное наименование адреса (до комнаты)
        /// </summary>
        public string ApartmentFullAddress { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public HouseStorage House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public long HouseId { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Дата учета платежа в билинге
        /// </summary>
        public DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// За какой месяц оплачено
        /// </summary>
        public DateTime PaydCalculationMonth { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal SumPayment { get; set; }

        /// <summary>
        /// Место проведения платежа
        /// </summary>
        public string PaymentPlacement { get; set; }
    }
}
