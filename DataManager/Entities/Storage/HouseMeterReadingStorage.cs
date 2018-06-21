using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Показания домовых приборов учета
    /// </summary>
    [Table("HouseMeterReading", Schema = "storage")]
    public class HouseMeterReadingStorage : BaseOhEntity, IHouse, IMeterReading
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public virtual DateTime CalculationDate { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public virtual HouseStorage House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public virtual long HouseId { get; set; }

        /// <summary>
        /// Наименование услуги
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
        /// Порядковый номер услуги в ЕПД
        /// </summary>
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// Номер прибора учета
        /// </summary>
        public string MeterDeviceNumber { get; set; }

        /// <summary>
        /// Идентификатор прибора учета во внешней системе
        /// </summary>
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
        public decimal Consumption { get; set; }

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
        /// Дата ввода
        /// </summary>
        public DateTime? EnteredDate { get; set; }

        /// <summary>
        /// Введенное показание
        /// </summary>
        public decimal? EnteredValue { get; set; }

        /// <summary>
        /// Источник информации
        /// </summary>
        public EInformationSource? InformationSource { get; set; }

        /// <summary>
        /// Выгруженный файл
        /// </summary>
        public virtual ExportedFile ExportedFile { get; set; }

        /// <summary>
        /// Выгруженный файл
        /// </summary>
        public virtual long? ExportedFileId { get; set; }

        /// <summary>
        /// Текстовый идентификатор банка данных
        /// </summary>
        public string DataBankPrefix { get; set; }
    }
}