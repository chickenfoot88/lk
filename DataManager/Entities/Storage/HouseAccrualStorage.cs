using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Начисление по дому
    /// </summary>
    [Table("HouseAccrual", Schema = "storage")]
    public class HouseAccrualStorage : Base.BaseOhEntity, IHouse, IHouseAccrual
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [Required]
        public HouseStorage House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public long HouseId { get; set; }

        /// <summary>
        /// Наименование услуги
        /// </summary>
        [Required]
        public virtual ServiceStorage Service { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// Наименование услуги
        /// </summary>
        [Required]
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
        /// Порядковый номер услуги
        /// </summary>
        public int? IndexNumber { get; set; }

        /// <summary>
        /// Поставщик услуг
        /// </summary>
        [Required]
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
        /// Полный адрес до улицы
        /// </summary>
        public string StreetFullAddress { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Полный адреса до дома
        /// </summary>
        public string HouseFullAddress { get; set; }

        /// <summary>
        /// Адресное пространство
        /// </summary>
        [Required]
        public AddressSpaceStorage AddressSpace { get; set; }

        /// <summary>
        /// Адресное пространство
        /// </summary>
        public long AddressSpaceId { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        public string HousingNumber { get; set; }

        /// <summary>
        /// Общая площадь дома
        /// </summary>
        public decimal? SquareHouse { get; set; }

        /// <summary>
        /// Площадь мест общего пользования дома
        /// </summary>
        public decimal? SquareHouseMop { get; set; }

        /// <summary>
        /// Отапливаемая площадь дома
        /// </summary>
        public decimal? SquareHouseHeating { get; set; }

        /// <summary>
        /// Текстовый идентификатор банка данных
        /// </summary>
        public string DataBankPrefix { get; set; }
    }
}