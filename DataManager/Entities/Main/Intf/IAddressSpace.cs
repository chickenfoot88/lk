using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    public interface IAddressSpace : IBaseEntity
    {
        /// <summary>
        /// Полный адрес до улицы
        /// </summary>
        string StreetFullAddress { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        string Town { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        string Place { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        string Street { get; set; }
    }
}