using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// ���������� � �������
    /// </summary>
    [Table("importaccrual", Schema = "import")]
    public class ImportAccrual : IBaseImportHelper
    {
        /// <summary>
        /// ������������� ������
        /// </summary>
        [Key, Column(Order = 0)]
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
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// ������������� ������ �� ������� �������
        /// </summary>
        [Key, Column(Order = 6)]
        public long OuterSystemServiceId { get; set; }

        /// <summary>
        /// ������������� ������� ������ �� ������� �������
        /// </summary>
        public long? OuterSystemBaseServiceId { get; set; }

        /// <summary>
        /// ������ ����� (��������, ������������ � �.�.)
        /// </summary>
        [Key, Column(Order = 7)]
        public string ServiceGroupName { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        [Key, Column(Order = 8)]
        public virtual decimal Tariff { get; set; }

        /// <summary>
        /// ������ �� ������
        /// </summary>
        [Key, Column(Order = 9)]
        public virtual decimal Consumption { get; set; }

        /// <summary>
        /// ������ ��� �� ������
        /// </summary>
        public virtual decimal? ConsumptionChn { get; set; }

        /// <summary>
        /// ������ �� ���
        /// </summary>
        public virtual decimal? ConsumptionImd { get; set; }

        /// <summary>
        /// ������ �� ���������
        /// </summary>
        public virtual decimal? ConsumptionNorm { get; set; }

        /// <summary>
        /// ������ �� ����
        /// </summary>
        public virtual decimal? ConsumptionHouse { get; set; }

        /// <summary>
        /// ������ �� ���� �� ���������
        /// </summary>
        public virtual decimal? ConsumptionHouseByApartments { get; set; }

        /// <summary>
        /// ������ �� ���� �� ��������� � ���
        /// </summary>
        public virtual decimal? ConsumptionHouseByApartmentsImd { get; set; }

        /// <summary>
        /// ������ �� ���� �� ��������� ������������ �� �����
        /// </summary>
        public virtual decimal? ConsumptionHouseByApartmentsNorm { get; set; }

        /// <summary>
        /// ������ �� ������� ����������
        /// </summary>
        public virtual decimal? ConsumptionHouseByNonResidential { get; set; }

        /// <summary>
        /// ������ �� ������ (����������������)
        /// </summary>
        public virtual decimal? ConsumptionLift { get; set; }

        /// <summary>
        /// ������ �� ���� ���
        /// </summary>
        public virtual decimal? ConsumptionHouseChn { get; set; }

        /// <summary>
        /// ������ �� ������������ ������� �����
        /// </summary>
        public virtual decimal? ConsumptionChmd { get; set; }

        /// <summary>
        /// ��������� �� ������
        /// </summary>
        [Key, Column(Order = 10)]
        public virtual decimal AccruedTariff { get; set; }

        /// <summary>
        /// ��������� �� ������ � ������ ������������
        /// </summary>
        [Key, Column(Order = 11)]
        public virtual decimal AccruedTariffNondelivery { get; set; }

        /// <summary>
        /// ����� ������������
        /// </summary>
        [Key, Column(Order = 12)]
        public virtual decimal SumNondelivery { get; set; }

        /// <summary>
        /// ������ �� ������������
        /// </summary>
        [Key, Column(Order = 13)]
        public virtual decimal ConsumptionNondelivery { get; set; }
        
        /// <summary>
        /// ���������� ���� ������������
        /// </summary>
        public virtual decimal? NondeliveryDaysCount { get; set; }

        /// <summary>
        /// ����� ����������� ���������� �� ������� ������
        /// </summary>
        [Key, Column(Order = 14)]
        public virtual decimal Reval { get; set; }

        /// <summary>
        /// ����� ��������� � ������
        /// </summary>
        [Key, Column(Order = 15)]
        public virtual decimal SumBalanceDelta { get; set; }

        /// <summary>
        /// ����� ����������� � ������
        /// </summary>
        [Key, Column(Order = 16)]
        public virtual decimal AccruedForPayment { get; set; }

        /// <summary>
        /// ����� ���������� �� ���
        /// </summary>
        [Key, Column(Order = 17)]
        public virtual decimal SumPayd { get; set; }

        /// <summary>
        /// ����� ���������� ������
        /// </summary>
        [Key, Column(Order = 18)]
        public virtual decimal SumOutsaldo { get; set; }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        [Key, Column(Order = 19)]
        public virtual decimal SumInsaldo { get; set; }

        /// <summary>
        /// ��������� �� ������ ���
        /// </summary>
        public virtual decimal? SumTariffChn { get; set; }

        /// <summary>
        /// ����� ��������� ������ ���
        /// </summary>
        public virtual decimal? SumInsaldoChn { get; set; }

        /// <summary>
        /// ����� ���������� ������ ���
        /// </summary>
        public virtual decimal? SumOutsaldoChn { get; set; }

        /// <summary>
        /// ���������� ���
        /// </summary>
        public virtual decimal? RevalChn { get; set; }

        /// <summary>
        /// ��������� � ������ ���
        /// </summary>
        public virtual decimal? SumBalanceDeltaChn { get; set; }

        /// <summary>
        /// ��������� � ������ ���
        /// </summary>
        public virtual decimal? AccruedForPaymentChn { get; set; }

        /// <summary>
        /// �������� ���
        /// </summary>
        public virtual decimal? SumPaydChn { get; set; }

        /// <summary>
        /// ��������� � �������� ����������� ���������
        /// </summary>
        public virtual decimal? AccruedBySocNorm { get; set; }

        /// <summary>
        /// ����������� ��������� �� ��
        /// </summary>
        public virtual decimal? CorrectionCoefficientImd { get; set; }

        /// <summary>
        /// ����������� ��������� �� ���������
        /// </summary>
        public virtual decimal? CorrectionCoefficientNorm { get; set; }

        /// <summary>
        /// ������������ ���������� �����
        /// </summary>
        [Key, Column(Order = 20)]
        public string SupplierName { get; set; }

        /// <summary>
        /// ���������� ���� �������� ������
        /// </summary>
        [Key, Column(Order = 21)]
        public virtual decimal ServiceDeliveryDays { get; set; }

        /// <summary>
        /// ���������� ���� ������������ ������ � �������
        /// </summary>
        public virtual decimal? NondeliveryDaysCountOnPast { get; set; }
    }
}