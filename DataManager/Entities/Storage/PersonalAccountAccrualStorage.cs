using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Начисление по ЛС
    /// </summary>
    [Table("PersonalAccountAccrual", Schema = "storage")]
    public class PersonalAccountAccrualStorage : BaseOhEntity, IPersonalAccount, IHouseAccrual
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
        public virtual long HouseId { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public virtual ServiceStorage Service { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// Наименование услуги
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Идентификатор услуги во внешней системе
        /// </summary>
        public long OuterSystemServiceId { get; set; }

        /// <summary>
        /// Идентификатор базовой услуги во внешней системе
        /// </summary>
        public long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string MeasureName { get; set; }

        /// <summary>
        /// Группа услуг (жилищные, коммунальные и т.д.)
        /// </summary>
        public string ServiceGroupName { get; set; }

        /// <summary>
        /// Базовая услуга
        /// </summary>
        public ServiceStorage BaseService { get; set; }

        /// <summary>
        /// Базовая услуга
        /// </summary>
        public long? BaseServiceId { get; set; }

        /// <summary>
        /// Поставщик услуг
        /// </summary>
        public SupplierStorage Supplier { get; set; }

        /// <summary>
        /// Поставщик услуг
        /// </summary>
        public long SupplierId { get; set; }

        /// <summary>
        /// Наименование поставщика услуг
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Расход по услуге
        /// </summary>
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
        public virtual decimal ConsumptionHouseByApartments { get; set; }

        /// <summary>
        /// Расход по дому по квартирам с ИПУ
        /// </summary>
        public virtual decimal ConsumptionHouseByApartmentsImd { get; set; }

        /// <summary>
        /// Расход по дому по квартирам рассчитанным по норме
        /// </summary>
        public virtual decimal ConsumptionHouseByApartmentsNorm { get; set; }

        /// <summary>
        /// Расход по нежилым помещениям
        /// </summary>
        public virtual decimal ConsumptionHouseByNonResidential { get; set; }

        /// <summary>
        /// Расход по лифтам (электроснабжение)
        /// </summary>
        public virtual decimal ConsumptionLift { get; set; }

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
        public virtual decimal AccruedTariff { get; set; }

        /// <summary>
        /// Начислено по тарифу с учетом недопоставки
        /// </summary>
        public virtual decimal AccruedTariffNondelivery { get; set; }

        /// <summary>
        /// Сумма недопоставки
        /// </summary>
        public virtual decimal SumNondelivery { get; set; }

        /// <summary>
        /// Расход по недопоставке
        /// </summary>
        public virtual decimal ConsumptionNondelivery { get; set; }

        /// <summary>
        /// Сумма перерасчета начислений за прошлые месяца
        /// </summary>
        public virtual decimal Reval { get; set; }

        /// <summary>
        /// Сумма изменений в сальдо
        /// </summary>
        public virtual decimal SumBalanceDelta { get; set; }

        /// <summary>
        /// Сумма начисленная к оплате
        /// </summary>
        public virtual decimal AccruedForPayment { get; set; }

        /// <summary>
        /// Сумма оплаченная по ЕПД
        /// </summary>
        public virtual decimal SumPayd { get; set; }

        /// <summary>
        /// Сумма исходящего сальдо
        /// </summary>
        public virtual decimal SumOutsaldo { get; set; }

        /// <summary>
        /// Сумма входящего сальдо
        /// </summary>
        public virtual decimal SumInsaldo { get; set; }

        /// <summary>
        /// Начислено по тарифу ОДН
        /// </summary>
        public virtual decimal SumTariffChn { get; set; }

        /// <summary>
        /// Сумма входящего сальдо ОДН
        /// </summary>
        public virtual decimal SumInsaldoChn { get; set; }

        /// <summary>
        /// Сумма исходящего сальдо ОДН
        /// </summary>
        public virtual decimal SumOutsaldoChn { get; set; }

        /// <summary>
        /// Перерасчет ОДН
        /// </summary>
        public virtual decimal RevalChn { get; set; }

        /// <summary>
        /// Изменения в сальдо ОДН
        /// </summary>
        public virtual decimal SumBalanceDeltaChn { get; set; }

        /// <summary>
        /// Начислено к оплате ОДН
        /// </summary>
        public virtual decimal AccruedForPaymentChn { get; set; }

        /// <summary>
        /// Оплачено ОДН
        /// </summary>
        public virtual decimal SumPaydChn { get; set; }

        /// <summary>
        /// Начислено в пределах социального норматива
        /// </summary>
        public virtual decimal AccruedBySocNorm { get; set; }

        /// <summary>
        /// Порядковый номер услуги в ЕПД
        /// </summary>
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// Тариф
        /// </summary>
        public virtual decimal Tariff { get; set; }

        /// <summary>
        /// Количество дней недопоставки
        /// </summary>
        public virtual decimal NondeliveryDaysCount { get; set; }

        /// <summary>
        /// Коэффициент коррекции по ПУ
        /// </summary>
        public virtual decimal? CorrectionCoefficientImd { get; set; }

        /// <summary>
        /// коэффициент коррекции по нормативу
        /// </summary>
        public virtual decimal? CorrectionCoefficientNorm { get; set; }

        /// <summary>
        /// Количество дней оказания услуги
        /// </summary>
        public virtual decimal ServiceDeliveryDays { get; set; }

        /// <summary>
        /// Количество дней недопоставки услуги в прошлом
        /// </summary>
        public virtual decimal? NondeliveryDaysCountOnPast { get; set; }
    }
}