namespace OhDataManager.Entities.Storage.Intf
{
    public interface IHouse : IAddressSpace
    {
        /// <summary>
        /// Полный адреса до дома
        /// </summary>
        string HouseFullAddress { get; set; }

        /// <summary>
        /// Адресное пространство
        /// </summary>
        AddressSpaceStorage AddressSpace { get; set; }

        /// <summary>
        /// Адресное пространство
        /// </summary>
        long AddressSpaceId { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        string HouseNumber { get; set; }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        string HousingNumber { get; set; }

        /// <summary>
        /// Общая площадь дома
        /// </summary>
        decimal? SquareHouse { get; set; }

        /// <summary>
        /// Площадь мест общего пользования дома
        /// </summary>
        decimal? SquareHouseMop { get; set; }

        /// <summary>
        /// Отапливаемая площадь дома
        /// </summary>
        decimal? SquareHouseHeating { get; set; }

        /// <summary>
        /// Текстовый идентификатор банка данных
        /// </summary>
        string DataBankPrefix { get; set; }
    }
}