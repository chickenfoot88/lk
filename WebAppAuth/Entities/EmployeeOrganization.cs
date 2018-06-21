using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Связка сотрудника с организациями 
    /// (организации берутся из родительского класса)
    /// </summary>
    [Table(nameof(EmployeeOrganization), Schema = "main")]
    public class EmployeeOrganization : BaseOhEntity
    {
        /// <summary>
        /// Сотрудник
        /// </summary>
        [Display(Name = "Сотрудник")]
        public virtual Employee Employee { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        [Display(Name = "Сотрудник")]
        public virtual int EmployeeId { get; set; }
    }
}
