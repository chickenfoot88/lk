using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Типы статусов загрузки
    /// </summary>
    public enum ELoadStatus
    {
        /// <summary>
        /// Не определено (значение по умолчанию)
        /// </summary>
        [Display(Name = "Не определено")]
        Undefined = 0,
        /// <summary>
        /// В процессе выполнения
        /// </summary>
        [Display(Name = "В процессе выполнения")]
        InProcess = 1,
        /// <summary>
        /// Успешно загружено
        /// </summary>
        [Display(Name = "Успешно загружено")]
        Success = 2,
        /// <summary>
        /// Ошибка при загрузке
        /// </summary>
        [Display(Name = "Ошибка при загрузке")]
        LoadError = 3,
        /// <summary>
        /// Ошибка в данных
        /// </summary>
        [Display(Name = "Ошибка в данных")]
        DataError = 4
    }
}
