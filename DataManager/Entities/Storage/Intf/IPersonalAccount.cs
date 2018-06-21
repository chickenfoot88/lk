using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Storage.Intf
{
    /// <summary>
    /// Лицевой счет
    /// </summary>
    public interface IPersonalAccount : IBaseEntity
    {
        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        long PersonalAccountNumber { get; set; }
        
        /// <summary>
        /// Платежный код
        /// </summary>
        long PaymentCode { get; set; }

        /// <summary>
        /// Полное наименование адреса (до комнаты)
        /// </summary>
        string ApartmentFullAddress { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        HouseStorage House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        long HouseId { get; set; }
    }
}