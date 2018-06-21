using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Платеж
    /// </summary>
    [Table(nameof(PersonalAccountPayment), Schema = "main")]
    public class PersonalAccountPayment : BaseOhEntity, IPersonalAccount, IBillInfo
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual PersonalAccount PersonalAccount { get; set; }

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
        public House House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public long HouseId { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Дата учета в билинге
        /// </summary>
        public DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// Оплачиваемый месяц
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
