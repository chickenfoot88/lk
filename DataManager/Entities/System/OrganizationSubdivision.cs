
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Подразделение организации
    /// </summary>
    [Table(nameof(OrganizationSubdivision), Schema = "system")]
    public class OrganizationSubdivision : BaseOhEntity
    {
        /// <summary>
        /// Полное наименование подразделения
        /// </summary>
        [Required]
        [Display(Name = "Полное наименование подразделения")]
        public virtual string FullName { get; set; }

        /// <summary>
        /// Код подразделения во внешней системе
        /// </summary>
        [Display(Name = "Код подразделения во внешней системе")]
        public virtual long OrganizationSubdivisionExternalId { get; set; }
    }
}
