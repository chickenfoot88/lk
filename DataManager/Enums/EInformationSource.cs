using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Источник информации
    /// </summary>
    public enum EInformationSource
    {
        /// <summary>
        /// Не определено
        /// </summary>
        [Display(Name = "Не определено")]
        Undefined = 0,

        /// <summary>
        /// Абонент
        /// </summary>
        [Display(Name = "Абонент")]
        Abonent = 1,

        /// <summary>
        /// Сотрудник
        /// </summary>
        [Display(Name = "Сотрудник")]
        Employee = 2,

        /// <summary>
        /// Мобильное приложение
        /// </summary>
        [Display(Name = "Мобильное приложение")]
        MobileApp = 3
    }
}
