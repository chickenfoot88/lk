using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Список доступных типов заявок
    /// </summary>
    [Table(nameof(ClaimKindTemplate), Schema = "main")]
    public class ClaimKindTemplate : BaseEntity
    {
        /// <summary>
        /// Вид заявки
        /// </summary>
        [Display(Name = "Вид заявки")]
        public virtual DictClaimKind ClaimKind { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Display(Name = "Вид заявки")]
        public virtual long ClaimKindId { get; set; }

        /// <summary>
        /// Шаблон
        /// </summary>
        [Display(Name = "Шаблон")]
        public virtual string Template { get; set; }
    }
}
