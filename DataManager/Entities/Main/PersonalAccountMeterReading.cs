using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.System;
using OhDataManager.Enums;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// ��������� ������������� �������� �����
    /// </summary>
    [Table("PersonalAccountMeterReading", Schema = "main")]
    public class PersonalAccountMeterReading : BaseOhEntity, IPersonalAccount, IMeterReading
    {
        /// <summary>
        /// ������� ����
        /// </summary>
        public virtual PersonalAccount PersonalAccount { get; set; }

        /// <summary>
        /// ������� ����
        /// </summary>
        public virtual long PersonalAccountId { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual House House { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public long HouseId { get; set; }

        /// <summary>
        /// ���� ������� (��������� �����)
        /// </summary>
        public virtual DateTime CalculationDate { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public virtual MeterService MeterService { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public long MeterServiceId { get; set; }

        /// <summary>
        /// ����� �������� �����
        /// </summary>
        public long PersonalAccountNumber { get; set; }

        /// <summary>
        /// ��������� ���
        /// </summary>
        public long PaymentCode { get; set; }

        /// <summary>
        /// ������ ������������ ������ (�� �������)
        /// </summary>
        public string ApartmentFullAddress { get; set; }

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
        public Service BaseService { get; set; }

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
        public decimal? Consumption { get; set; }

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
    }
}