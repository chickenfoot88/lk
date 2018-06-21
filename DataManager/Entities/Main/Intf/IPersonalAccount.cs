using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
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
        House House { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        long HouseId { get; set; }

    }
}