using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ���
    /// </summary>
    [Table("House", Schema = "main")]
    public class House : BaseOhEntity, IHouse
    {
        /// <summary>
        /// ������ ����� �� ����
        /// </summary>
        public virtual string HouseFullAddress { get; set; }

        /// <summary>
        /// ������ ����� �� �����
        /// </summary>
        public virtual string StreetFullAddress { get; set; }

        /// <summary>
        /// ������������� ������� ������ �� �����
        /// </summary>
        [Required]
        public virtual AddressSpace AddressSpace { get; set; }

        /// <summary>
        /// ������������� �������� ������������
        /// </summary>
        public long AddressSpaceId { get; set; }

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
        
        /// <summary>
        /// ����� ����
        /// </summary>
        public virtual string HouseNumber { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        public virtual string HousingNumber { get; set; }
        
        /// <summary>
        /// ����� ������� ����
        /// </summary>
        public virtual decimal? SquareHouse { get; set; }

        /// <summary>
        /// ������� ���� ������ ����������� ����
        /// </summary>
        public virtual decimal? SquareHouseMop { get; set; }

        /// <summary>
        /// ������������ ������� ����
        /// </summary>
        public virtual decimal? SquareHouseHeating { get; set; }
        
        /// <summary>
        /// ��������� ������������� ����� ������
        /// </summary>
        public virtual string DataBankPrefix { get; set; }
    }
}