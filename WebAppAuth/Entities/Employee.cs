using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using WebAppAuth.Models;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    [Table(nameof(Employee), Schema = "main")]
    public class Employee : BaseOhEntity
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        [Display(Name = "Пользователь")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        [Index("IX_Employee_ApplicationUser", 2, IsUnique = true)]
        [Display(Name = "Пользователь")]
        public virtual int ApplicationUserId { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public virtual Position Position { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public virtual long? PositionId { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        public virtual string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public virtual string Patronymic { get; set; }

        /// <summary>
        /// ФИО для отображения в гридах
        /// </summary>
        [Display(Name = "ФИО для отображения")]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Display(Name = "Номер телефона")]
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Аватар
        /// </summary>
        public virtual byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}
