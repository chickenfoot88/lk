using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// ��������� ������� �������� �����
    /// </summary>
    [Table("HouseMeterReading", Schema = "storage")]
    public class HouseMeterReadingStorage : BaseOhEntity, IHouse, IMeterReading
    {
        /// <summary>
        /// ���� ������� (��������� �����)
        /// </summary>
        public virtual DateTime CalculationDate { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual HouseStorage House { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual long HouseId { get; set; }

        /// <summary>
        /// ������������ ������
        /// </summary>
        public virtual ServiceStorage Service { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public long ServiceId { get; set; }

        /// <summary>
        /// ������������ ������
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// ������������� ������ �� ������� �������
        /// </summary>
        public long OuterSystemServiceId { get; set; }

        /// <summary>
        /// ������������� ������� ������ �� ������� �������
        /// </summary>
        public long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// ������� ���������
        /// </summary>
        public string MeasureName { get; set; }

        /// <summary>
        /// ������ ����� (��������, ������������ � �.�.)
        /// </summary>
        public string ServiceGroupName { get; set; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public ServiceStorage BaseService { get; set; }

        /// <summary>
        /// ������� ������
        /// </summary>
        public long? BaseServiceId { get; set; }

        /// <summary>
        /// ���������� ����� ������ � ���
        /// </summary>
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// ����� ������� �����
        /// </summary>
        public string MeterDeviceNumber { get; set; }

        /// <summary>
        /// ������������� ������� ����� �� ������� �������
        /// </summary>
        public long OuterSystemMeterDeviceId { get; set; }

        /// <summary>
        /// ���� ����� ��������� ������� � �������
        /// </summary>
        public DateTime CalculationApplyingDate { get; set; }

        /// <summary>
        /// ��������� ������� �����
        /// </summary>
        public decimal Indication { get; set; }

        /// <summary>
        /// ���� ����� ����������� ���������
        /// </summary>
        public DateTime? CalculationApplyingDatePrevious { get; set; }

        /// <summary>
        /// ���������� ���������
        /// </summary>
        public decimal? IndicationPrevious { get; set; }

        /// <summary>
        /// ���������� ��������� (����������� �������������)
        /// </summary>
        public decimal? Multiplier { get; set; }

        /// <summary>
        /// ������ �� ������� �����
        /// </summary>
        public decimal Consumption { get; set; }

        /// <summary>
        /// ���������� ������ �� ������� �����
        /// </summary>
        public decimal? ConsumptionAdditional { get; set; }

        /// <summary>
        /// ������ �� ������� ����������
        /// </summary>
        public decimal? ConsumptionNonresidental { get; set; }

        /// <summary>
        /// ������ �� ���������������� �� �����
        /// </summary>
        public decimal? ConsumptionLift { get; set; }

        /// <summary>
        /// ����� ����������
        /// </summary>
        public string Placement { get; set; }

        /// <summary>
        /// ����������� ������� �����
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// �������� ���� ������� �����
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// ���� ������� ��
        /// </summary>
        public DateTime? VerificationDate { get; set; }

        /// <summary>
        /// ���� ��������� ������� ��
        /// </summary>
        public DateTime? VerificationDateNext { get; set; }

        /// <summary>
        /// ������ ����� �� �����
        /// </summary>
        public string StreetFullAddress { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// ���������� �����
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// ������ ������ �� ����
        /// </summary>
        public string HouseFullAddress { get; set; }

        /// <summary>
        /// �������� ������������
        /// </summary>
        public AddressSpaceStorage AddressSpace { get; set; }

        /// <summary>
        /// �������� ������������
        /// </summary>
        public long AddressSpaceId { get; set; }

        /// <summary>
        /// ����� ����
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// ����� �������
        /// </summary>
        public string HousingNumber { get; set; }

        /// <summary>
        /// ����� ������� ����
        /// </summary>
        public decimal? SquareHouse { get; set; }

        /// <summary>
        /// ������� ���� ������ ����������� ����
        /// </summary>
        public decimal? SquareHouseMop { get; set; }

        /// <summary>
        /// ������������ ������� ����
        /// </summary>
        public decimal? SquareHouseHeating { get; set; }

        /// <summary>
        /// ���� �����
        /// </summary>
        public DateTime? EnteredDate { get; set; }

        /// <summary>
        /// ��������� ���������
        /// </summary>
        public decimal? EnteredValue { get; set; }

        /// <summary>
        /// �������� ����������
        /// </summary>
        public EInformationSource? InformationSource { get; set; }

        /// <summary>
        /// ����������� ����
        /// </summary>
        public virtual ExportedFile ExportedFile { get; set; }

        /// <summary>
        /// ����������� ����
        /// </summary>
        public virtual long? ExportedFileId { get; set; }

        /// <summary>
        /// ��������� ������������� ����� ������
        /// </summary>
        public string DataBankPrefix { get; set; }
    }
}