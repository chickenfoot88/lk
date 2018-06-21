using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// Начисления и расходы
    /// </summary>
    [Table("importaccrual", Schema = "import")]
    public class ImportAccrual : IBaseImportHelper
    {
        /// <summary>
        /// Идентификатор секции
        /// </summary>
        [Key, Column(Order = 0)]
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
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// Идентификатор услуги во внешней системе
        /// </summary>
        [Key, Column(Order = 6)]
        public long OuterSystemServiceId { get; set; }

        /// <summary>
        /// Идентификатор базовой услуги во внешней системе
        /// </summary>
        public long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// Группа услуг (жилищные, коммунальные и т.д.)
        /// </summary>
        [Key, Column(Order = 7)]
        public string ServiceGroupName { get; set; }

        /// <summary>
        /// Тариф
        /// </summary>
        [Key, Column(Order = 8)]
        public virtual decimal Tariff { get; set; }

        /// <summary>
        /// Расход по услуге
        /// </summary>
        [Key, Column(Order = 9)]
        public virtual decimal Consumption { get; set; }

        /// <summary>
        /// Расход ОДН по услуге
        /// </summary>
        public virtual decimal? ConsumptionChn { get; set; }

        /// <summary>
        /// Расход по ИПУ
        /// </summary>
        public virtual decimal? ConsumptionImd { get; set; }

        /// <summary>
        /// Расход по нормативу
        /// </summary>
        public virtual decimal? ConsumptionNorm { get; set; }

        /// <summary>
        /// Расход по дому
        /// </summary>
        public virtual decimal? ConsumptionHouse { get; set; }

        /// <summary>
        /// Расход по дому по квартирам
        /// </summary>
        public virtual decimal? ConsumptionHouseByApartments { get; set; }

        /// <summary>
        /// Расход по дому по квартирам с ИПУ
        /// </summary>
        public virtual decimal? ConsumptionHouseByApartmentsImd { get; set; }

        /// <summary>
        /// Расход по дому по квартирам рассчитанным по норме
        /// </summary>
        public virtual decimal? ConsumptionHouseByApartmentsNorm { get; set; }

        /// <summary>
        /// Расход по нежилым помещениям
        /// </summary>
        public virtual decimal? ConsumptionHouseByNonResidential { get; set; }

        /// <summary>
        /// Расход по лифтам (электроснабжение)
        /// </summary>
        public virtual decimal? ConsumptionLift { get; set; }

        /// <summary>
        /// Расход по дому ОДН
        /// </summary>
        public virtual decimal? ConsumptionHouseChn { get; set; }

        /// <summary>
        /// Расход по общедомовому прибору учета
        /// </summary>
        public virtual decimal? ConsumptionChmd { get; set; }

        /// <summary>
        /// Начислено по тарифу
        /// </summary>
        [Key, Column(Order = 10)]
        public virtual decimal AccruedTariff { get; set; }

        /// <summary>
        /// Начислено по тарифу с учетом недопоставки
        /// </summary>
        [Key, Column(Order = 11)]
        public virtual decimal AccruedTariffNondelivery { get; set; }

        /// <summary>
        /// Сумма недопоставки
        /// </summary>
        [Key, Column(Order = 12)]
        public virtual decimal SumNondelivery { get; set; }

        /// <summary>
        /// Расход по недопоставке
        /// </summary>
        [Key, Column(Order = 13)]
        public virtual decimal ConsumptionNondelivery { get; set; }
        
        /// <summary>
        /// Количество дней недопоставки
        /// </summary>
        public virtual decimal? NondeliveryDaysCount { get; set; }

        /// <summary>
        /// Сумма перерасчета начислений за прошлые месяца
        /// </summary>
        [Key, Column(Order = 14)]
        public virtual decimal Reval { get; set; }

        /// <summary>
        /// Сумма изменений в сальдо
        /// </summary>
        [Key, Column(Order = 15)]
        public virtual decimal SumBalanceDelta { get; set; }

        /// <summary>
        /// Сумма начисленная к оплате
        /// </summary>
        [Key, Column(Order = 16)]
        public virtual decimal AccruedForPayment { get; set; }

        /// <summary>
        /// Сумма оплаченная по ЕПД
        /// </summary>
        [Key, Column(Order = 17)]
        public virtual decimal SumPayd { get; set; }

        /// <summary>
        /// Сумма исходящего сальдо
        /// </summary>
        [Key, Column(Order = 18)]
        public virtual decimal SumOutsaldo { get; set; }

        /// <summary>
        /// Сумма входящего сальдо
        /// </summary>
        [Key, Column(Order = 19)]
        public virtual decimal SumInsaldo { get; set; }

        /// <summary>
        /// Начислено по тарифу ОДН
        /// </summary>
        public virtual decimal? SumTariffChn { get; set; }

        /// <summary>
        /// Сумма входящего сальдо ОДН
        /// </summary>
        public virtual decimal? SumInsaldoChn { get; set; }

        /// <summary>
        /// Сумма исходящего сальдо ОДН
        /// </summary>
        public virtual decimal? SumOutsaldoChn { get; set; }

        /// <summary>
        /// Перерасчет ОДН
        /// </summary>
        public virtual decimal? RevalChn { get; set; }

        /// <summary>
        /// Изменения в сальдо ОДН
        /// </summary>
        public virtual decimal? SumBalanceDeltaChn { get; set; }

        /// <summary>
        /// Начислено к оплате ОДН
        /// </summary>
        public virtual decimal? AccruedForPaymentChn { get; set; }

        /// <summary>
        /// Оплачено ОДН
        /// </summary>
        public virtual decimal? SumPaydChn { get; set; }

        /// <summary>
        /// Начислено в пределах социального норматива
        /// </summary>
        public virtual decimal? AccruedBySocNorm { get; set; }

        /// <summary>
        /// Коэффициент коррекции по ПУ
        /// </summary>
        public virtual decimal? CorrectionCoefficientImd { get; set; }

        /// <summary>
        /// коэффициент коррекции по нормативу
        /// </summary>
        public virtual decimal? CorrectionCoefficientNorm { get; set; }

        /// <summary>
        /// Наименование поставщика услуг
        /// </summary>
        [Key, Column(Order = 20)]
        public string SupplierName { get; set; }

        /// <summary>
        /// Количество дней оказания услуги
        /// </summary>
        [Key, Column(Order = 21)]
        public virtual decimal ServiceDeliveryDays { get; set; }

        /// <summary>
        /// Количество дней недопоставки услуги в прошлом
        /// </summary>
        public virtual decimal? NondeliveryDaysCountOnPast { get; set; }
    }
}