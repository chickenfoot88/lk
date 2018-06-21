using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Типы статусов выгрузки
    /// </summary>
    public enum EUnloadStatus
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
        [Display(Name = "Успешно выгружено")]
        Success = 2,
        /// <summary>
        /// Ошибка при загрузке
        /// </summary>
        [Display(Name = "Ошибка при выгрузке")]
        UnloadError = 3,
        /// <summary>
        /// Нет данных для выгрузки
        /// </summary>
        [Display(Name = "Нет данных для выгрузки")]
        NoData = 4
    }
}
