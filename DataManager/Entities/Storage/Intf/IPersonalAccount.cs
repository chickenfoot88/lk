using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Storage.Intf
{
    /// <summary>
    /// ������� ����
    /// </summary>
    public interface IPersonalAccount : IBaseEntity
    {
        /// <summary>
        /// ����� �������� �����
        /// </summary>
        long PersonalAccountNumber { get; set; }
        
        /// <summary>
        /// ��������� ���
        /// </summary>
        long PaymentCode { get; set; }

        /// <summary>
        /// ������ ������������ ������ (�� �������)
        /// </summary>
        string ApartmentFullAddress { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        HouseStorage House { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        long HouseId { get; set; }
    }
}