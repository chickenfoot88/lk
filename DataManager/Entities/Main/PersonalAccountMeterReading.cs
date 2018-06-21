using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Показания индивидульных приборов учета
    /// </summary>
    [Table("PersonalAccountMeterReading", Schema = "main")]
    public class PersonalAccountMeterReading : BaseOhEntity, IPersonalAccount, IMeterReading
    {
        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual PersonalAccount PersonalAccount { get; set; }

        /// <summary>
        /// Лицевой счет
        /// </summary>
        public virtual long PersonalAccountId { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public virtual House House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public long HouseId { get; set; }

        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public virtual DateTime CalculationDate { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public virtual MeterService MeterService { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        public long MeterServiceId { get; set; }

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
        public Service BaseService { get; set; }

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
    }
}