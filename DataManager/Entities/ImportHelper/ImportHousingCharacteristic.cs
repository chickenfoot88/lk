using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Enums;

namespace OhDataManager.Entities.ImportHelper
{
    /// <summary>
    /// Характеристики жилищного фонда
    /// </summary>
    [Table("importhousingcharacteristic", Schema = "import")]
    public class ImportHousingCharacteristic : IBaseImportHelper
    {
        /// <summary>
        /// Идентификатор секции
        /// </summary>
        [Key, Column(Order = 0)]
        public long SectionId { get; set; }

        /// <summary>
        /// Расчетный месяц
        /// </summary>
        [Key, Column(Order = 1)]
        public DateTime CalculationDate { get; set; }

        /// <summary>
        /// Код организации
        /// </summary>
        [Key, Column(Order = 2)]
        public long OrganizationCode { get; set; }

        /// <summary>
        /// Платежный код
        /// </summary>
        [Key, Column(Order = 3)]
        public long PaymentCode { get; set; }

        /// <summary>
        /// Номер лицевого счета
        /// </summary>
        public virtual long PersonalAccountNumber { get; set; }

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
        /// Комплекс
        /// </summary>
         public virtual string Complex { get; set; }

        /// <summary>
        /// Номер корпуса
        /// </summary>
        public virtual string HousingNumber { get; set; }

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
        /// Уникальный код УК в разрезе ЕРЦ
        /// </summary>
         public virtual long ManagmentOrganizationNameId { get; set; }

        /// <summary>
        /// Тип комфортности (0 - неизвестно, 1 - изолированная, 2 - коммунальная)
        /// </summary>
        public virtual EComfortableType? ComfortableType { get; set; }

        /// <summary>
        /// Тип приватизации
        /// </summary>
        public virtual EPrivatiziedType? PrivatiziedType { get; set; }

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

    }
}