using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Справочник "Вид заявки"
    /// </summary>
    [Table(nameof(DictClaimKind), Schema = "system")]
    public class DictClaimKind : BaseEntity
    {
        /// <summary>
        /// Тип заявки
        /// </summary>
        public virtual DictClaimType DictClaimType { get; set; }

        /// <summary>
        /// Тип заявки
        /// </summary>
        public virtual long? DictClaimTypeId { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Required]
        [Display(Name = "Вид заявки")]
        public virtual string Name { get; set; }

        /// <summary>
        /// Описание (используется в подсказке для пользователя)
        /// </summary>
        [Display(Name = "Описание")]
        public virtual string Note { get; set; }

        /// <summary>
        /// Признак видимости
        /// </summary>
        [Display(Name = "Признак видимости")]
        public virtual bool IsVisible { get; set; }
    }
}
