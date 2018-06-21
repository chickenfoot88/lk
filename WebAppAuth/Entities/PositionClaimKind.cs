using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Список доступных типов заявок
    /// </summary>
    [Table(nameof(PositionClaimKind), Schema = "main")]
    public class PositionClaimKind : BaseEntity
    {
        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public virtual Position Position { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public virtual long PositionId { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Display(Name = "Вид заявки")]
        public virtual DictClaimKind DictClaimKind { get; set; }

        /// <summary>
        /// Вид заявки
        /// </summary>
        [Display(Name = "Вид заявки")]
        public virtual long DictClaimKindId { get; set; }
    }
}
