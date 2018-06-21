using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// ”правл€юща€ организаци€
    /// </summary>
    [Table("ManagmentOrganization", Schema = "storage")]
    public class ManagmentOrganizationStorage : Base.BaseOhEntity, IManagmentOrganization
    {
        /// <summary>
        /// Ќаименование управл€ющей организации
        /// </summary>
        [Required]
        public string ManagmentOrganizationName { get; set; }
    }
}