using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        [Display(Name = "Идентификатор")]
        public virtual long Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [Display(Name = "Время создания")]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Время изменения
        /// </summary>
        [Display(Name = "Время изменения")]
        public virtual DateTime ChangedTime { get; set; }
    }
}
