namespace WebAppAuth.WebApi.Models.Requests
{
    /// <summary>
    /// Закрытие заявки
    /// </summary>
    public class AbonentClaimCloseRequest : BaseRequest
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public long AbonentClaimId { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }
}