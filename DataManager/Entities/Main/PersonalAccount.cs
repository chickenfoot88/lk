using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Enums;
using OhDataManager.Extensions;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Лицевой счет
    /// </summary>
    [Table("PersonalAccount", Schema = "main")]
    public class PersonalAccount : BaseOhEntity, IPersonalAccount, IHouse, IManagmentOrganization
    {
        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public virtual long PersonalAccountNumber { get; set; }
        
        /// <summary>
        /// Платежный код
        /// </summary>
        public virtual long PaymentCode { get; set; }

        /// <summary>
        /// Полное наименование адреса (до комнаты)
        /// </summary>
        public virtual string ApartmentFullAddress { get; set; }

        /// <summary>
        /// Полный адреса до дома
        /// </summary>
        public virtual string HouseFullAddress { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [Required]
        public virtual House House { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        public virtual long HouseId { get; set; }

        /// <summary>
        /// Полный адрес до улицы
        /// </summary>
        public virtual string StreetFullAddress { get; set; }

        /// <summary>
        /// Идентификатор полного адреса до улицы
        /// </summary>
        [Required]
        public virtual AddressSpace AddressSpace { get; set; }

        /// <summary>
        /// Идентификатор Адресное пространство
        /// </summary>
        public long AddressSpaceId { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public virtual string Town { get; set; }

        /// <summary>
        /// Населенный пункт
        /// </summary>
        public virtual string Place { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        /// Номер дома
        /// </summary>
        public virtual string HouseNumber { get; set; }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        public virtual string HousingNumber { get; set; }

        /// <summary>
        /// Наименование адреса квартиры (кв, комн)
        /// </summary>
        public virtual string ApartmentAddress { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public virtual string ApartmentNumber { get; set; }

        /// <summary>
        /// Номер комнаты
        /// </summary>
        public virtual string RoomNumber { get; set; }

        /// <summary>
        /// Номер подъезда
        /// </summary>
        public virtual int? PorchNumber { get; set; }

        /// <summary>
        /// ФИО квартиросъемщика, нанимателя, собственника
        /// </summary>
        public virtual string ApartmentOwnerFullName { get; set; }

        /// <summary>
        /// Наименование управляющей компании
        /// </summary>
        public virtual string ManagmentOrganizationName { get; set; }

        /// <summary>
        /// Управляющая организация
        /// </summary>
        [Required]
        public virtual ManagmentOrganization ManagmentOrganization { get; set; }

        /// <summary>
        /// Управляющая организация
        /// </summary>
        public virtual long ManagmentOrganizationId { get; set; }

        /// <summary>
        /// Тип комфортности (0 - неизвестно, 1 - изолированная, 2 - коммунальная)
        /// </summary>
        public virtual EComfortableType? ComfortableType { get; set; }
        
        /// <summary>
        /// Текстовое наименование типа комфортности
        /// </summary>
        [NotMapped]
        public virtual string ComfortableTypeText => ComfortableType.GetDisplayName();

        /// <summary>
        /// Тип приватизации
        /// </summary>
        public virtual EPrivatiziedType? PrivatiziedType { get; set; }

        /// <summary>
        /// Текстовое наименование типа приватизации
        /// </summary>
        [NotMapped]
        public virtual string PrivatiziedTypeText => PrivatiziedType.GetDisplayName();

        /// <summary>
        /// Этаж
        /// </summary>
        public virtual int? FloorNumber { get; set; }

        /// <summary>
        /// Квартир на лестничной клетке
        /// </summary>
        public virtual int? ApartmentsCountOnFloor { get; set; }

        /// <summary>
        /// Площадь квартиры
        /// </summary>
        public virtual decimal? SquareApartment { get; set; }

        /// <summary>
        /// Жилая площадь квартиры
        /// </summary>
        public virtual decimal? SquareApartmentLiving { get; set; }

        /// <summary>
        /// Отапливаемая площадь квартиры
        /// </summary>
        public virtual decimal? SquareApartmentHeating { get; set; }

        /// <summary>
        /// Общая площадь дома
        /// </summary>
        public virtual decimal? SquareHouse { get; set; }

        /// <summary>
        /// Площадь мест общего пользования дома
        /// </summary>
        public virtual decimal? SquareHouseMop { get; set; }

        /// <summary>
        /// Отапливаемая площадь дома
        /// </summary>
        public virtual decimal? SquareHouseHeating { get; set; }

        /// <summary>
        /// Количество жильцов
        /// </summary>
        public virtual int? CountResidents { get; set; }

        /// <summary>
        /// Количество временно выбывших
        /// </summary>
        public virtual int? CountDeparture { get; set; }

        /// <summary>
        /// Количество временно прибывших
        /// </summary>
        public virtual int? CountArrive { get; set; }

        /// <summary>
        /// Количество комнат
        /// </summary>
        public virtual int? CountRooms { get; set; }

        /// <summary>
        /// Текстовый идентификатор банка данных
        /// </summary>
        public virtual string DataBankPrefix { get; set; }

        /// <summary>
        /// Состояние ЛС
        /// </summary>
        public virtual EPersonalAccountState? PersonalAccountState { get; set; }

        /// <summary>
        /// Текстовое наименование состояния ЛС
        /// </summary>
        [NotMapped]
        public virtual string PersonalAccountStateText => PersonalAccountState.GetDisplayName();
    }
}