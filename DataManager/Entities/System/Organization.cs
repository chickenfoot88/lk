
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Организация (заказчик)
    /// </summary>
    [Table("Organization", Schema = "system")]
    public class Organization : BaseEntity
    {
        /// <summary>
        /// Полное наименование организации
        /// </summary>
        [Required]
        [Display(Name = "Полное наименование организации")]
        public virtual string FullName { get; set; }

        /// <summary>
        /// Код организации во внешней системе
        /// </summary>
        [Display(Name = "Код организации во внешней системе")]
        public virtual long OrganizationExternalId { get; set; }
    }
}
