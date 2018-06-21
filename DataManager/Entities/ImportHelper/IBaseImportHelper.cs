using System;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// Интерфейс базовой сущности
    /// </summary>
    public interface IBaseImportHelper
    {
        /// <summary>
        /// Идентификатор секции
        /// </summary>
        long SectionId { get; set; }

        /// <summary>
        /// Расчетный месяц
        /// </summary>
        DateTime CalculationDate { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        long OrganizationCode { get; set; }

        /// <summary>
        /// Платежный код
        /// </summary>
        long PaymentCode { get; set; }
    }
}
