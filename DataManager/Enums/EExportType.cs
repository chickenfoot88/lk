using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Типы выгрузки
    /// </summary>
    public enum EExportType
    {
        /// <summary>
        /// Не определено (значение по умолчанию)
        /// </summary>
        [Display(Name = "Тип не определен")]
        Undefined = 0,

        /// <summary>
        /// Выгрузка показаний ИПУ
        /// </summary>
        [Display(Name = "Выгрузка показаний ПУ")]
        MeterReading = 1
    }
}
