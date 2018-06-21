using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Расчетный месяц
    /// </summary>
    [Table("CalculationMonth", Schema = "main")]
    public class CalculationMonth : BaseOhEntity, IBillInfo
    {
        /// <summary>
        /// Дата расчета (расчетный месяц)
        /// </summary>
        public virtual DateTime CalculationDate { get; set; }

        /// <summary>
        /// Признак текущего расчетного месяца
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}
