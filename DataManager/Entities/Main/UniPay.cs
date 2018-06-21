using System;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Платежи
    /// </summary>
    [Table(nameof(UniPay), Schema = "main")]
    public class UniPay : BaseEntity
    {
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Валюта платежа - RUB
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Идентификатор точки продажи в системе
        /// </summary>
        public string ShopIdp { get; set; }

        /// <summary>
        /// id операции во внешней системе
        /// </summary>
        public string OrderIdp { get; set; }

        /// <summary>
        /// сумма оплаты(строковый)
        /// </summary>
        public string SubtotalP { get; set; }

        /// <summary>
        /// сумма, которую заплатали с учетом комиссии
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        //// сумма, которую заплатали без учета комиссии
        /// </summary>
        public decimal? WithdrawAmount { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// ИНН
        /// </summary>
        public string Inn { get; set; }

        /// <summary>
        /// Адрес 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Подпись
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Идентификатор покупателя 
        /// </summary>
        public string CustomerIdp { get; set; }

        /// <summary>
        /// Комментарий к платежу
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Имя покупателя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия покупателя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// время жизни формы
        /// </summary>
        public int? LifeTime { get; set; }

        /// <summary>
        /// Адреса возврата после успешной оплаты покупателям
        /// </summary>
        public string UrlReturnOk { get; set; }

        /// <summary>
        /// Адреса возврата после неуспешной оплаты покупателям
        /// </summary>
        public string UrlReturnNo { get; set; }
    }
}