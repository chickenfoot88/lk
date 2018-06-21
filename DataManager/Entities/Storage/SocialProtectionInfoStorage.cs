using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Информация от соц.защиты
    /// </summary>
    [Table("SocialProtectionInfo", Schema = "storage")]
    public class SocialProtectionInfoStorage : Base.BaseOhEntity, IPersonalAccount, IBillInfo
    {
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
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// ФИО получателя
        /// </summary>
        public string PersonFullName { get; set; }

        /// <summary>
        /// Идентификатор льготы
        /// </summary>
        public long ExpenceId { get; set; }

        /// <summary>
        /// Наименование льготы
        /// </summary>
        public string ExpenceName { get; set; }

        /// <summary>
        /// Наименование группы льгот
        /// </summary>
        public string ExpenceGroupName { get; set; }

        /// <summary>
        /// Начисленная сумма
        /// </summary>
        public decimal SumAccrued { get; set; }

        /// <summary>
        /// Сумма к выплате
        /// </summary>
        public decimal SumPayoff { get; set; }

        /// <summary>
        /// Дата окончания предоставления льготы
        /// </summary>
        public DateTime ExpenceEndDate { get; set; }

        /// <summary>
        /// Место проведения платежа
        /// </summary>
        public string PaymentPlacement { get; set; }

        /// <summary>
        /// Входящее сальдо
        /// </summary>
        public decimal BalanceIn { get; set; }

        /// <summary>
        /// Изменения
        /// </summary>
        public decimal SumDelta { get; set; }

        /// <summary>
        /// Перерасчет
        /// </summary>
        public decimal SumRecalc { get; set; }
    }
}
