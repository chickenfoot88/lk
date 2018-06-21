namespace OhDataManager.Entities.Storage.Intf
{
    /// <summary>
    /// Начисление
    /// </summary>
    public interface IHouseAccrual : IBillInfo, IService, ISupplier
    {
        /// <summary>
        /// Дом
        /// </summary>
        HouseStorage House { get; set; }
        /// <summary>
        /// Дом
        /// </summary>
        long HouseId { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        ServiceStorage Service { get; set; }

        /// <summary>
        /// Услуга
        /// </summary>
        long  ServiceId { get; set; }

        /// <summary>
        /// Поставщик услуг
        /// </summary>
        SupplierStorage Supplier { get; set; }
        
        /// <summary>
        /// Поставщик услуг
        /// </summary>
        long SupplierId { get; set; }

        /// <summary>
        /// Расход по услуге
        /// </summary>
        decimal Consumption { get; set; }

        /// <summary>
        /// Расход ОДН по услуге
        /// </summary>
        decimal? ConsumptionChn { get; set; }

        /// <summary>
        /// Расход по ИПУ
        /// </summary>
        decimal? ConsumptionImd { get; set; }

        /// <summary>
        /// Расход по нормативу
        /// </summary>
        decimal? ConsumptionNorm { get; set; }

        /// <summary>
        /// Расход по дому
        /// </summary>
        decimal? ConsumptionHouse { get; set; }

        /// <summary>
        /// Расход по дому по квартирам
        /// </summary>
        decimal ConsumptionHouseByApartments { get; set; }

        /// <summary>
        /// Расход по дому по квартирам с ИПУ
        /// </summary>
        decimal ConsumptionHouseByApartmentsImd { get; set; }

        /// <summary>
        /// Расход по дому по квартирам рассчитанным по норме
        /// </summary>
        decimal ConsumptionHouseByApartmentsNorm { get; set; }

        /// <summary>
        /// Расход по нежилым помещениям
        /// </summary>
        decimal ConsumptionHouseByNonResidential { get; set; }

        /// <summary>
        /// Расход по лифтам (электроснабжение)
        /// </summary>
        decimal ConsumptionLift { get; set; }

        /// <summary>
        /// Расход по дому ОДН
        /// </summary>
        decimal? ConsumptionHouseChn { get; set; }

        /// <summary>
        /// Расход по общедомовому прибору учета
        /// </summary>
        decimal? ConsumptionChmd { get; set; }

        /// <summary>
        /// Начислено по тарифу
        /// </summary>
        decimal AccruedTariff { get; set; }

        /// <summary>
        /// Начислено по тарифу с учетом недопоставки
        /// </summary>
        decimal AccruedTariffNondelivery { get; set; }

        /// <summary>
        /// Сумма недопоставки
        /// </summary>
        decimal SumNondelivery { get; set; }

        /// <summary>
        /// Расход по недопоставке
        /// </summary>
        decimal ConsumptionNondelivery { get; set; }
        
        /// <summary>
        /// Сумма перерасчета начислений за прошлые месяца
        /// </summary>
        decimal Reval { get; set; }

        /// <summary>
        /// Сумма изменений в сальдо
        /// </summary>
        decimal SumBalanceDelta { get; set; }

        /// <summary>
        /// Сумма начисленная к оплате
        /// </summary>
        decimal AccruedForPayment { get; set; }

        /// <summary>
        /// Сумма оплаченная по ЕПД
        /// </summary>
        decimal SumPayd { get; set; }

        /// <summary>
        /// Сумма исходящего сальдо
        /// </summary>
        decimal SumOutsaldo { get; set; }

        /// <summary>
        /// Сумма входящего сальдо
        /// </summary>
        decimal SumInsaldo { get; set; }

        /// <summary>
        /// Начислено по тарифу ОДН
        /// </summary>
        decimal SumTariffChn { get; set; }

        /// <summary>
        /// Сумма входящего сальдо ОДН
        /// </summary>
        decimal SumInsaldoChn { get; set; }

        /// <summary>
        /// Сумма исходящего сальдо ОДН
        /// </summary>
        decimal SumOutsaldoChn { get; set; }

        /// <summary>
        /// Перерасчет ОДН
        /// </summary>
        decimal RevalChn { get; set; }

        /// <summary>
        /// Изменения в сальдо ОДН
        /// </summary>
        decimal SumBalanceDeltaChn { get; set; }

        /// <summary>
        /// Начислено к оплате ОДН
        /// </summary>
        decimal AccruedForPaymentChn { get; set; }

        /// <summary>
        /// Оплачено ОДН
        /// </summary>
        decimal SumPaydChn { get; set; }

        /// <summary>
        /// Начислено в пределах социального норматива
        /// </summary>
        decimal AccruedBySocNorm { get; set; }
    }
}