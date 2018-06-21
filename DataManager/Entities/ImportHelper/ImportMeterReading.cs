using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// Счетчики и показания счетчиков
    /// </summary>
    [Table("importmeterreading", Schema = "import")]
    public class ImportMeterReading : IBaseImportHelper
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
        /// Наименование услуги
        /// </summary>
        [Key, Column(Order = 4)]
        public string ServiceName { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        [Key, Column(Order = 5)]
        public string MeasureName { get; set; }

        /// <summary>
        /// Порядковый номер услуги в ЕПД
        /// </summary>
        [Column(Order = 6)]
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// Тип прибора учета (1- домовой ,2 - групповой, 3- квартирный)
        /// </summary>
        [Key, Column(Order = 7)]
        public int MeterReadingType { get; set; }

        /// <summary>
        /// Номер прибора учета
        /// </summary>
        [Key, Column(Order = 8)]
        public string MeterDeviceNumber { get; set; }

        /// <summary>
        /// Идентификатор прибора учета во внешней системе
        /// </summary>
        [Key, Column(Order = 9)]
        public long OuterSystemMeterDeviceId { get; set; }

        /// <summary>
        /// Дата учета показания прибора в билинге
        /// </summary>
        public DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// Показание прибора учета
        /// </summary>
        public decimal Indication { get; set; }

        /// <summary>
        /// Дата учета предыдущего показания
        /// </summary>
        public DateTime? CalculationApplyingDatePrevious { get; set; }

        /// <summary>
        /// Предыдущее показание
        /// </summary>
        public decimal? IndicationPrevious { get; set; }

        /// <summary>
        /// Масштабный множитель (коэффициент трансформации)
        /// </summary>
        public decimal? Multiplier { get; set; }

        /// <summary>
        /// Расход по прибору учета
        /// </summary>
        public decimal? Consumption { get; set; }

        /// <summary>
        /// Добавочный расход по прибору учета
        /// </summary>
        public decimal? ConsumptionAdditional { get; set; }

        /// <summary>
        /// Расход по нежилым помещениям
        /// </summary>
        public decimal? ConsumptionNonresidental { get; set; }

        /// <summary>
        /// Расход по электроснабжению на лифты
        /// </summary>
        public decimal? ConsumptionLift { get; set; }

        /// <summary>
        /// Место размещения
        /// </summary>
        public string Placement { get; set; }

        /// <summary>
        /// Разрядность прибора учета
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Название типа прибора учета
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Дата поверки ПУ
        /// </summary>
        public DateTime? VerificationDate { get; set; }

        /// <summary>
        /// Дата следующей поверки ПУ
        /// </summary>
        public DateTime? VerificationDateNext { get; set; }
    }
}