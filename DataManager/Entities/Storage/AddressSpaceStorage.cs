using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// �������� ������������
    /// </summary>
    [Table("AddressSpace", Schema = "storage")]
    public class AddressSpaceStorage : Base.BaseOhEntity, IAddressSpace
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