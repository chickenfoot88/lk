using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// �������� � ��������� ���������
    /// </summary>
    [Table("importmeterreading", Schema = "import")]
    public class ImportMeterReading : IBaseImportHelper
    {
        /// <summary>
        /// ������������� ������
        /// </summary>
        [Column(Order = 0)]
        public long SectionId { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        [Key, Column(Order = 1)]
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// ��� �����������
        /// </summary>
        [Key, Column(Order = 2)]
        public long OrganizationCode { get; set; }

        /// <summary>
        /// ��������� ���
        /// </summary>
        [Key, Column(Order = 3)]
        public long PaymentCode { get; set; }

        /// <summary>
        /// ������������ ������
        /// </summary>
        [Key, Column(Order = 4)]
        public string ServiceName { get; set; }

        /// <summary>
        /// ������� ���������
        /// </summary>
        [Key, Column(Order = 5)]
        public string MeasureName { get; set; }

        /// <summary>
        /// ���������� ����� ������ � ���
        /// </summary>
        [Column(Order = 6)]
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// ��� ������� ����� (1- ������� ,2 - ���������, 3- ����������)
        /// </summary>
        [Key, Column(Order = 7)]
        public int MeterReadingType { get; set; }

        /// <summary>
        /// ����� ������� �����
        /// </summary>
        [Key, Column(Order = 8)]
        public string MeterDeviceNumber { get; set; }

        /// <summary>
        /// ������������� ������� ����� �� ������� �������
        /// </summary>
        [Key, Column(Order = 9)]
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
    }
}