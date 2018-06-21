using System;
using OhDataManager.Entities.Base;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Общая системная информация
    /// </summary>
    public class GeneralSystemInfo : BaseOhEntity
    {
        /// <summary>
        /// Текущий расчетный месяц
        /// </summary>
        public virtual DateTime? CurrentCalculationDate { get; set; }
    }
}