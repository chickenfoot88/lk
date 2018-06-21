namespace WebAppAuth.WebApi.Models.Requests
{
    /// <summary>
    /// Комментарий
    /// </summary>
    public class AbonentClaimCommentRequest : BaseRequest
    {
        /// <summary>
        /// Идентификатор заявки
        /// </summary>
        public long AbonentClaimId { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
    }
}