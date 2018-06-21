using System;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Прибор учета
    /// </summary>
    interface IMeterReading: IBillInfo, IService
    {
        /// <summary>
        /// Услуга
        /// </summary>
        MeterService MeterService { get; set; }
        /// <summary>
        /// Услуга
        /// </summary>
        long MeterServiceId { get; set; }

        /// <summary>
        /// Номер прибора учета
        /// </summary>
        string MeterDeviceNumber { get; set; }

        /// <summary>
        /// Идентификатор прибора учета во внешней системе
        /// </summary>
        long OuterSystemMeterDeviceId { get; set; }

        /// <summary>
        /// Дата учета показания прибора в билинге
        /// </summary>
        DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// Показание прибора учета
        /// </summary>
        decimal Indication { get; set; }

        /// <summary>
        /// Дата учета предыдущего показания
        /// </summary>
        DateTime? CalculationApplyingDatePrevious { get; set; }

        /// <summary>
        /// Предыдущее показание
        /// </summary>
        decimal? IndicationPrevious { get; set; }

        /// <summary>
        /// Масштабный множитель (коэффициент трансформации)
        /// </summary>
        decimal? Multiplier { get; set; }

        /// <summary>
        /// Расход по прибору учета
        /// </summary>
        decimal? Consumption { get; set; }

        /// <summary>
        /// Добавочный расход по прибору учета
        /// </summary>
        decimal? ConsumptionAdditional { get; set; }

        /// <summary>
        /// Расход по нежилым помещениям
        /// </summary>
        decimal? ConsumptionNonresidental { get; set; }

        /// <summary>
        /// Расход по электроснабжению на лифты
        /// </summary>
        decimal? ConsumptionLift { get; set; }

        /// <summary>
        /// Место размещения
        /// </summary>
        string Placement { get; set; }

        /// <summary>
        /// Разрядность прибора учета
        /// </summary>
        int Capacity { get; set; }

        /// <summary>
        /// Название типа прибора учета
        /// </summary>
        string TypeName { get; set; }

        /// <summary>
        /// Дата поверки ПУ
        /// </summary>
        DateTime? VerificationDate { get; set; }

        /// <summary>
        /// Дата следующей поверки ПУ
        /// </summary>
        DateTime? VerificationDateNext { get; set; }
    }
}