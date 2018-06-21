using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Информация от соц.защиты
    /// </summary>
    [Table(nameof(PersonalAccountSocialPayment), Schema = "main")]
    public class PersonalAccountSocialPayment : BaseOhEntity, IPersonalAccount, IBillInfo
    {
        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public long PersonalAccountNumber { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual PersonalAccount PersonalAccount { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual long PersonalAccountId { get; set; }

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
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// ФИО получателя
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// Идентификатор льготы
        /// </summary>
        public long? ArticleCode { get; set; }

        /// <summary>
        /// Наименование льготы
        /// </summary>
        public string ArticleName { get; set; }

        /// <summary>
        /// Наименование группы льгот
        /// </summary>
        public string ArticleGroupName { get; set; }

        /// <summary>
        /// Начисленная сумма
        /// </summary>
        public decimal? SumAccrued { get; set; }

        /// <summary>
        /// Сумма к выплате
        /// </summary>
        public decimal? SumPayoff { get; set; }

        /// <summary>
        /// Место выплаты
        /// </summary>
        public string PaymentPlacement { get; set; }

        /// <summary>
        /// Дата окончания предоставления льготы
        /// </summary>
        public DateTime? SubsidiesEndDate { get; set; }
        
        /// <summary>
        /// Входящее сальдо
        /// </summary>
        public decimal? SumInsaldo { get; set; }

        /// <summary>
        /// Изменения
        /// </summary>
        public decimal? SumDelta { get; set; }

        /// <summary>
        /// Перерасчет
        /// </summary>
        public decimal? SumRecalculation { get; set; }
    }
}
