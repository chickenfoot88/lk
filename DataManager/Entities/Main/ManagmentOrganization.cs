using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ����������� �����������
    /// </summary>
    [Table("ManagmentOrganization", Schema = "main")]
    public class ManagmentOrganization : BaseOhEntity, IManagmentOrganization
    {
        /// <summary>
        /// ������������ ����������� �����������
        /// </summary>
        [Required]
        public string ManagmentOrganizationName { get; set; }
    }
}