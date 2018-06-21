using System;

namespace WebAppAuth.WebApi.Models.Requests
{
    /// <summary>
    /// Общий запрос
    /// </summary>
    public class CommonRequest : BaseRequest
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Начало периода
        /// </summary>
        public DateTime PeriodBegin { get; set; }
        
        /// <summary>
        /// Окончание периода
        /// </summary>
        public DateTime PeriodEnd { get; set; }
    }
}