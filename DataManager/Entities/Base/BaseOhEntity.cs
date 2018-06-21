using System.ComponentModel.DataAnnotations;
using OhDataManager.Entities.System;

namespace OhDataManager.Entities.Base
{
    /// <summary>
    /// Базовая сущность
    /// </summary>
    public class BaseOhEntity : BaseEntity
    {
        /// <summary>
        /// Организация
        /// </summary>
        [Display(Name = "Организация")]
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Организация
        /// </summary>
        [Required]
        [Display(Name = "Организация")]
        public virtual long OrganizationId { get; set; }
    }
}
