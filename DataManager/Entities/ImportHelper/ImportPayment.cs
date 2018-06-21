using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// Платеж
    /// </summary>
    [Table("importpayment", Schema = "import")]
    public class ImportPayment : IBaseImportHelper
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
        /// Дата платежа
        /// </summary>
        [Key, Column(Order = 4)]
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Дата учета показания прибора в билинге
        /// </summary>
        [Key, Column(Order = 5)]
        public DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// За какой месяц оплачено
        /// </summary>
        [Key, Column(Order = 6)]
        public DateTime PaydCalculationMonth { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [Key, Column(Order = 7)]
        public decimal SumPayment { get; set; }

        /// <summary>
        /// Место проведения платежа
        /// </summary>
        public string PaymentPlacement { get; set; }

    }
}