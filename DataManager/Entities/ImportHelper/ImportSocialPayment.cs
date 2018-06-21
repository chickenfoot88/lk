using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// Социальные выплаты
    /// </summary>
    [Table("importsocialpayment", Schema = "import")]
    public class ImportSocialPayment : IBaseImportHelper
    {
        /// <summary>
        /// Идентификатор секции
        /// </summary>
        [Column(Order = 0)]
        public long SectionId { get; set; }

        /// <summary>
        /// Расчетный месяц
        /// </summary>
        [Key, Column(Order = 1)]
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        [Key, Column(Order = 2)]
        public long OrganizationCode { get; set; }

        /// <summary>
        /// Платежный код
        /// </summary>
        [Key, Column(Order = 3)]
        public long PaymentCode { get; set; }

        /// <summary>
        /// Имя получателя средств
        /// </summary>
        [Key, Column(Order = 4)]
        public string RecipientName { get; set; }

        /// <summary>
        /// Код статьи финансирования
        /// </summary>
        [Key, Column(Order = 5)]
        public long? ArticleCode { get; set; }

        /// <summary>
        /// Статья финансирования
        /// </summary>
        [Column(Order = 6)]
        public string ArticleName { get; set; }

        /// <summary>
        /// Группа статей
        /// </summary>
        [Column(Order = 7)]
        public string ArticleGroupName { get; set; }

        /// <summary>
        /// Начисленная сумма
        /// </summary>
        [Key, Column(Order = 8)]
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
        /// Дата окончания предоставления субсидии
        /// </summary>
        public DateTime? SubsidiesEndDate { get; set; }

        /// <summary>
        /// Входящее сальдо(долг+/переплата-)
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