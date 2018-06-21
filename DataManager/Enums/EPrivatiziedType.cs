using System.ComponentModel.DataAnnotations;

namespace OhDataManager.Enums
{
    /// <summary>
    /// Тип приватизированности
    /// </summary>
    public enum EPrivatiziedType
    {
        /// <summary>
        /// Неопределено
        /// </summary>
        [Display(Name = "Тип неопределен")]
        Undefined = 0,

        /// <summary>
        /// Приватизирована
        /// </summary>
        [Display(Name = "Приватизирована")]
        Privatizied = 1,

        /// <summary>
        /// Не приватизирована
        /// </summary>
        [Display(Name = "Не приватизирована")]
        NonPrivatizied = 2
    }
}
