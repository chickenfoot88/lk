using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ”правл€юща€ организаци€
    /// </summary>
    [Table("ManagmentOrganization", Schema = "main")]
    public class ManagmentOrganization : BaseOhEntity, IManagmentOrganization
    {
        /// <summary>
        /// Ќаименование управл€ющей организации
        /// </summary>
        [Required]
        public string ManagmentOrganizationName { get; set; }
    }
}