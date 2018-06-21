using System;

namespace OhDataManager.Entities.Storage.Intf
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