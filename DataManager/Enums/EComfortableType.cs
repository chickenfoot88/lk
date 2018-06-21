using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Тип комфортности
    /// </summary>
    public enum EComfortableType
    {
        /// <summary>
        /// Неопределено
        /// </summary>
        [Display(Name = "Тип неопределен")]
        Undefined = 0,

        /// <summary>
        /// Изолированная
        /// </summary>
        [Display(Name = "Изолированная")]
        Isolated = 1,

        /// <summary>
        /// Коммунальная
        /// </summary>
        [Display(Name = "Коммунальная")]
        NonIsolated = 2
    }
}
