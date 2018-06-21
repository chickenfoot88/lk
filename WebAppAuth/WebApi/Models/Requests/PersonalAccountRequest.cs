namespace WebAppAuth.WebApi.Models.Requests
{
    /// <summary>
    /// Лицевой счет
    /// </summary>
    public class PersonalAccountRequest : BaseRequest
    {
        /// <summary>
        /// Платежный код
        /// </summary>
        public virtual long PaymentCode { get; set; }

        /// <summary>
        /// ФИО квартиросъемщика, нанимателя, собственника
        /// </summary>
        public virtual string ApartmentOwnerFullName { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public virtual string ApartmentNumber { get; set; }
    }
}