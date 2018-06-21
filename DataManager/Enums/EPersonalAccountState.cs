using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Статус лицевого счета
    /// </summary>
    public enum EPersonalAccountState
    {
        /// <summary>
        /// Изолированная
        /// </summary>
        [Display(Name = "Открыт")]
        Opened = 1,

        /// <summary>
        /// Коммунальная
        /// </summary>
        [Display(Name = "Закрыт")]
        Closed = 2,

        /// <summary>
        /// Не определено
        /// </summary>
        [Display(Name = "Тип не определен")]
        Undefined = 3
    }
}
