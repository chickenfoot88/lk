using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Enums;
using OhDataManager.Extensions;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ������� ����
    /// </summary>
    [Table("PersonalAccount", Schema = "main")]
    public class PersonalAccount : BaseOhEntity, IPersonalAccount, IHouse, IManagmentOrganization
    {
        /// <summary>
        /// ����� �������� �����
        /// </summary>
        public virtual long PersonalAccountNumber { get; set; }
        
        /// <summary>
        /// ��������� ���
        /// </summary>
        public virtual long PaymentCode { get; set; }

        /// <summary>
        /// ������ ������������ ������ (�� �������)
        /// </summary>
        public virtual string ApartmentFullAddress { get; set; }

        /// <summary>
        /// ������ ������ �� ����
        /// </summary>
        public virtual string HouseFullAddress { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        [Required]
        public virtual House House { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual long HouseId { get; set; }

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
        /// ������������ ������ �������� (��, ����)
        /// </summary>
        public virtual string ApartmentAddress { get; set; }

        /// <summary>
        /// ����� ��������
        /// </summary>
        public virtual string ApartmentNumber { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        public virtual string RoomNumber { get; set; }

        /// <summary>
        /// ����� ��������
        /// </summary>
        public virtual int? PorchNumber { get; set; }

        /// <summary>
        /// ��� ����������������, ����������, ������������
        /// </summary>
        public virtual string ApartmentOwnerFullName { get; set; }

        /// <summary>
        /// ������������ ����������� ��������
        /// </summary>
        public virtual string ManagmentOrganizationName { get; set; }

        /// <summary>
        /// ����������� �����������
        /// </summary>
        [Required]
        public virtual ManagmentOrganization ManagmentOrganization { get; set; }

        /// <summary>
        /// ����������� �����������
        /// </summary>
        public virtual long ManagmentOrganizationId { get; set; }

        /// <summary>
        /// ��� ������������ (0 - ����������, 1 - �������������, 2 - ������������)
        /// </summary>
        public virtual EComfortableType? ComfortableType { get; set; }
        
        /// <summary>
        /// ��������� ������������ ���� ������������
        /// </summary>
        [NotMapped]
        public virtual string ComfortableTypeText => ComfortableType.GetDisplayName();

        /// <summary>
        /// ��� ������������
        /// </summary>
        public virtual EPrivatiziedType? PrivatiziedType { get; set; }

        /// <summary>
        /// ��������� ������������ ���� ������������
        /// </summary>
        [NotMapped]
        public virtual string PrivatiziedTypeText => PrivatiziedType.GetDisplayName();

        /// <summary>
        /// ����
        /// </summary>
        public virtual int? FloorNumber { get; set; }

        /// <summary>
        /// ������� �� ���������� ������
        /// </summary>
        public virtual int? ApartmentsCountOnFloor { get; set; }

        /// <summary>
        /// ������� ��������
        /// </summary>
        public virtual decimal? SquareApartment { get; set; }

        /// <summary>
        /// ����� ������� ��������
        /// </summary>
        public virtual decimal? SquareApartmentLiving { get; set; }

        /// <summary>
        /// ������������ ������� ��������
        /// </summary>
        public virtual decimal? SquareApartmentHeating { get; set; }

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
        /// ���������� �������
        /// </summary>
        public virtual int? CountResidents { get; set; }

        /// <summary>
        /// ���������� �������� ��������
        /// </summary>
        public virtual int? CountDeparture { get; set; }

        /// <summary>
        /// ���������� �������� ���������
        /// </summary>
        public virtual int? CountArrive { get; set; }

        /// <summary>
        /// ���������� ������
        /// </summary>
        public virtual int? CountRooms { get; set; }

        /// <summary>
        /// ��������� ������������� ����� ������
        /// </summary>
        public virtual string DataBankPrefix { get; set; }

        /// <summary>
        /// ��������� ��
        /// </summary>
        public virtual EPersonalAccountState? PersonalAccountState { get; set; }

        /// <summary>
        /// ��������� ������������ ��������� ��
        /// </summary>
        [NotMapped]
        public virtual string PersonalAccountStateText => PersonalAccountState.GetDisplayName();
    }
}