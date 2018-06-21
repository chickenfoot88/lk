using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// ���������� �� ��
    /// </summary>
    [Table("PersonalAccountAccrual", Schema = "storage")]
    public class PersonalAccountAccrualStorage : BaseOhEntity, IPersonalAccount, IHouseAccrual
    {
        /// <summary>
        /// ���� ������� (��������� �����)
        /// </summary>
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// ������� ����
        /// </summary>
        public virtual PersonalAccountStorage PersonalAccount { get; set; }

        /// <summary>
        /// ������� ����
        /// </summary>
        public virtual long PersonalAccountId { get; set; }

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
        /// ���
        /// </summary>
        public HouseStorage House { get; set; }

        /// <summary>
        /// ���
        /// </summary>
        public virtual long HouseId { get; set; }

        /// <summary>
        /// ������
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
        /// ��������� �����
        /// </summary>
        public SupplierStorage Supplier { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        public long SupplierId { get; set; }

        /// <summary>
        /// ������������ ���������� �����
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// ������ �� ������
        /// </summary>
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
        public virtual decimal ConsumptionHouseByApartments { get; set; }

        /// <summary>
        /// ������ �� ���� �� ��������� � ���
        /// </summary>
        public virtual decimal ConsumptionHouseByApartmentsImd { get; set; }

        /// <summary>
        /// ������ �� ���� �� ��������� ������������ �� �����
        /// </summary>
        public virtual decimal ConsumptionHouseByApartmentsNorm { get; set; }

        /// <summary>
        /// ������ �� ������� ����������
        /// </summary>
        public virtual decimal ConsumptionHouseByNonResidential { get; set; }

        /// <summary>
        /// ������ �� ������ (����������������)
        /// </summary>
        public virtual decimal ConsumptionLift { get; set; }

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
        public virtual decimal AccruedTariff { get; set; }

        /// <summary>
        /// ��������� �� ������ � ������ ������������
        /// </summary>
        public virtual decimal AccruedTariffNondelivery { get; set; }

        /// <summary>
        /// ����� ������������
        /// </summary>
        public virtual decimal SumNondelivery { get; set; }

        /// <summary>
        /// ������ �� ������������
        /// </summary>
        public virtual decimal ConsumptionNondelivery { get; set; }

        /// <summary>
        /// ����� ����������� ���������� �� ������� ������
        /// </summary>
        public virtual decimal Reval { get; set; }

        /// <summary>
        /// ����� ��������� � ������
        /// </summary>
        public virtual decimal SumBalanceDelta { get; set; }

        /// <summary>
        /// ����� ����������� � ������
        /// </summary>
        public virtual decimal AccruedForPayment { get; set; }

        /// <summary>
        /// ����� ���������� �� ���
        /// </summary>
        public virtual decimal SumPayd { get; set; }

        /// <summary>
        /// ����� ���������� ������
        /// </summary>
        public virtual decimal SumOutsaldo { get; set; }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        public virtual decimal SumInsaldo { get; set; }

        /// <summary>
        /// ��������� �� ������ ���
        /// </summary>
        public virtual decimal SumTariffChn { get; set; }

        /// <summary>
        /// ����� ��������� ������ ���
        /// </summary>
        public virtual decimal SumInsaldoChn { get; set; }

        /// <summary>
        /// ����� ���������� ������ ���
        /// </summary>
        public virtual decimal SumOutsaldoChn { get; set; }

        /// <summary>
        /// ���������� ���
        /// </summary>
        public virtual decimal RevalChn { get; set; }

        /// <summary>
        /// ��������� � ������ ���
        /// </summary>
        public virtual decimal SumBalanceDeltaChn { get; set; }

        /// <summary>
        /// ��������� � ������ ���
        /// </summary>
        public virtual decimal AccruedForPaymentChn { get; set; }

        /// <summary>
        /// �������� ���
        /// </summary>
        public virtual decimal SumPaydChn { get; set; }

        /// <summary>
        /// ��������� � �������� ����������� ���������
        /// </summary>
        public virtual decimal AccruedBySocNorm { get; set; }

        /// <summary>
        /// ���������� ����� ������ � ���
        /// </summary>
        public virtual int? IndexNumber { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public virtual decimal Tariff { get; set; }

        /// <summary>
        /// ���������� ���� ������������
        /// </summary>
        public virtual decimal NondeliveryDaysCount { get; set; }

        /// <summary>
        /// ����������� ��������� �� ��
        /// </summary>
        public virtual decimal? CorrectionCoefficientImd { get; set; }

        /// <summary>
        /// ����������� ��������� �� ���������
        /// </summary>
        public virtual decimal? CorrectionCoefficientNorm { get; set; }

        /// <summary>
        /// ���������� ���� �������� ������
        /// </summary>
        public virtual decimal ServiceDeliveryDays { get; set; }

        /// <summary>
        /// ���������� ���� ������������ ������ � �������
        /// </summary>
        public virtual decimal? NondeliveryDaysCountOnPast { get; set; }
    }
}