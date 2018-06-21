using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Типы загрузки
    /// </summary>
    public enum EImportType
    {
        /// <summary>
        /// Не определено (значение по умолчанию)
        /// </summary>
        [Display(Name = "Тип не определен")]
        Undefined = 0,
        
        /// <summary>
        /// Загрузка файла с информацией о лицевых счетах
        /// </summary>
        [Display(Name = "Загрузка лицевых счетов")]
        Account = 1,

        /// <summary>
        /// Загрузка файла абонентов
        /// </summary>
        [Display(Name = "Загрузка файла абонентов")]
        AbonentFiles = 2
    }
}
