namespace WebAppAuth.WebApi.Models.Requests
{
    /// <summary>
    /// Базовый запрос
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public long ClientId { get; set; }

        /// <summary>
        /// Контрольная подпись
        /// </summary>
        public string VerificationSignature { get; set; }
    }
}