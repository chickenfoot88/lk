using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Типы статусов заявок
    /// </summary>
    public enum EClaimStatus
    {
        /// <summary>
        /// Не определено (значение по умолчанию)
        /// </summary>
        [Display(Name = "Не определено")]
        Undefined = 0,

        /// <summary>
        /// Новая
        /// </summary>
        [Display(Name = "Новая")]
        Created = 1,

        /// <summary>
        /// В работе
        /// </summary>
        [Display(Name = "В работе")]
        InProcess = 2,

        /// <summary>
        /// Уточнение
        /// </summary>
        [Display(Name = "Уточнение")]
        Refinement = 3,

        /// <summary>
        /// Исполнена
        /// </summary>
        [Display(Name = "Исполнена")]
        Completed = 4,

        /// <summary>
        /// Возвращена
        /// </summary>
        [Display(Name = "Возвращена")]
        Returned = 5,

        /// <summary>
        /// Закрыта
        /// </summary>
        [Display(Name = "Закрыта")]
        Closed = 6
    }
}
