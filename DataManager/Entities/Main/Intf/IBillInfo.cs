using System;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Информация, относящаяся к расчету
    /// </summary>
    public interface IBillInfo
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        DateTime CalculationDate { get; set; }
    }
}