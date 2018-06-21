using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Справочник "Тип заявки"
    /// </summary>
    [Table(nameof(DictClaimType), Schema = "system")]
    public class DictClaimType : BaseEntity
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// Описание (используется в подсказке для пользователя)
        /// </summary>
        public virtual string Note { get; set; }

        /// <summary>
        /// Признак видимости
        /// </summary>
        public virtual bool IsVisible { get; set; }

        /// <summary>
        /// Заявка может быть оценена автором
        /// </summary>
        public virtual bool IsCanEstimate { get; set; }
    }
}
