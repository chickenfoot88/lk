namespace OhDataManager.Entities
{
    /// <summary>
    /// Диаграмма
    /// </summary>
    public class ChartItem
    {
        /// <summary>
        /// Наименование
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Начислено
        /// </summary>
        public virtual decimal RsumTarif { get; set; }

        /// <summary>
        /// Оплата
        /// </summary>
        public virtual decimal SumMoney { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public virtual decimal Value { get; set; }

        /// <summary>
        /// Расход
        /// </summary>
        public virtual decimal Rashod { get; set; }

        /// <summary>
        /// Расход по общедомовому прибору учета
        /// </summary>
        public virtual decimal RashodDomOdpu { get; set; }

        /// <summary>
        /// Расход ОДН
        /// </summary>
        public virtual decimal RashodOdn { get; set; }

        /// <summary>
        /// Ед. измер.
        /// </summary>
        public virtual string Measure { get; set; }
    }
}