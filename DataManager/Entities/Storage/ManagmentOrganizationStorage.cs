using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// ����������� �����������
    /// </summary>
    [Table("ManagmentOrganization", Schema = "storage")]
    public class ManagmentOrganizationStorage : Base.BaseOhEntity, IManagmentOrganization
    {
        /// <summary>
        /// ������������ ����������� �����������
        /// </summary>
        [Required]
        public string ManagmentOrganizationName { get; set; }
    }
}