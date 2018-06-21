using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// �������� ������������
    /// </summary>
    [Table("AddressSpace", Schema = "main")]
    public class AddressSpace : BaseOhEntity, IAddressSpace
    {
        /// <summary>
        /// ������ ����� �� �����
        /// </summary>
        [Required]
        public virtual string StreetFullAddress { get; set; }
        
        /// <summary>
        /// �����
        /// </summary>
        public virtual string Town { get; set; }

        /// <summary>
        /// ���������� �����
        /// </summary>
        public virtual string Place { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public virtual string Street { get; set; }
    }
}