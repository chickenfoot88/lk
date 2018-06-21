namespace OhDataManager.Entities.Storage.Intf
{
    /// <summary>
    /// ����������
    /// </summary>
    public interface IHouseAccrual : IBillInfo, IService, ISupplier
    {
        /// <summary>
        /// ���
        /// </summary>
        HouseStorage House { get; set; }
        /// <summary>
        /// ���
        /// </summary>
        long HouseId { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        ServiceStorage Service { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        long  ServiceId { get; set; }

        /// <summary>
        /// ��������� �����
        /// </summary>
        SupplierStorage Supplier { get; set; }
        
        /// <summary>
        /// ��������� �����
        /// </summary>
        long SupplierId { get; set; }

        /// <summary>
        /// ������ �� ������
        /// </summary>
        decimal Consumption { get; set; }

        /// <summary>
        /// ������ ��� �� ������
        /// </summary>
        decimal? ConsumptionChn { get; set; }

        /// <summary>
        /// ������ �� ���
        /// </summary>
        decimal? ConsumptionImd { get; set; }

        /// <summary>
        /// ������ �� ���������
        /// </summary>
        decimal? ConsumptionNorm { get; set; }

        /// <summary>
        /// ������ �� ����
        /// </summary>
        decimal? ConsumptionHouse { get; set; }

        /// <summary>
        /// ������ �� ���� �� ���������
        /// </summary>
        decimal ConsumptionHouseByApartments { get; set; }

        /// <summary>
        /// ������ �� ���� �� ��������� � ���
        /// </summary>
        decimal ConsumptionHouseByApartmentsImd { get; set; }

        /// <summary>
        /// ������ �� ���� �� ��������� ������������ �� �����
        /// </summary>
        decimal ConsumptionHouseByApartmentsNorm { get; set; }

        /// <summary>
        /// ������ �� ������� ����������
        /// </summary>
        decimal ConsumptionHouseByNonResidential { get; set; }

        /// <summary>
        /// ������ �� ������ (����������������)
        /// </summary>
        decimal ConsumptionLift { get; set; }

        /// <summary>
        /// ������ �� ���� ���
        /// </summary>
        decimal? ConsumptionHouseChn { get; set; }

        /// <summary>
        /// ������ �� ������������ ������� �����
        /// </summary>
        decimal? ConsumptionChmd { get; set; }

        /// <summary>
        /// ��������� �� ������
        /// </summary>
        decimal AccruedTariff { get; set; }

        /// <summary>
        /// ��������� �� ������ � ������ ������������
        /// </summary>
        decimal AccruedTariffNondelivery { get; set; }

        /// <summary>
        /// ����� ������������
        /// </summary>
        decimal SumNondelivery { get; set; }

        /// <summary>
        /// ������ �� ������������
        /// </summary>
        decimal ConsumptionNondelivery { get; set; }
        
        /// <summary>
        /// ����� ����������� ���������� �� ������� ������
        /// </summary>
        decimal Reval { get; set; }

        /// <summary>
        /// ����� ��������� � ������
        /// </summary>
        decimal SumBalanceDelta { get; set; }

        /// <summary>
        /// ����� ����������� � ������
        /// </summary>
        decimal AccruedForPayment { get; set; }

        /// <summary>
        /// ����� ���������� �� ���
        /// </summary>
        decimal SumPayd { get; set; }

        /// <summary>
        /// ����� ���������� ������
        /// </summary>
        decimal SumOutsaldo { get; set; }

        /// <summary>
        /// ����� ��������� ������
        /// </summary>
        decimal SumInsaldo { get; set; }

        /// <summary>
        /// ��������� �� ������ ���
        /// </summary>
        decimal SumTariffChn { get; set; }

        /// <summary>
        /// ����� ��������� ������ ���
        /// </summary>
        decimal SumInsaldoChn { get; set; }

        /// <summary>
        /// ����� ���������� ������ ���
        /// </summary>
        decimal SumOutsaldoChn { get; set; }

        /// <summary>
        /// ���������� ���
        /// </summary>
        decimal RevalChn { get; set; }

        /// <summary>
        /// ��������� � ������ ���
        /// </summary>
        decimal SumBalanceDeltaChn { get; set; }

        /// <summary>
        /// ��������� � ������ ���
        /// </summary>
        decimal AccruedForPaymentChn { get; set; }

        /// <summary>
        /// �������� ���
        /// </summary>
        decimal SumPaydChn { get; set; }

        /// <summary>
        /// ��������� � �������� ����������� ���������
        /// </summary>
        decimal AccruedBySocNorm { get; set; }
    }
}