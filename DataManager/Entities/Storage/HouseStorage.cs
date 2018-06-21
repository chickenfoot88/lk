using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Entities.Storage.Intf;

namespace OhDataManager.Entities.Storage
{
    /// <summary>
    /// Дом
    /// </summary>
    [Table("House", Schema = "storage")]
    public class HouseStorage : Base.BaseOhEntity,  IHouse
    {
        /// <summary>
        /// Полный адреса до дома
        /// </summary>
        public virtual string HouseFullAddress { get; set; }

        /// <summary>
        /// Полный адрес до улицы
        /// </summary>
        public virtual string StreetFullAddress { get; set; }

        /// <summary>
        /// Идентификатор полного адреса до улицы
        /// </summary>
        [Required]
        public virtual AddressSpaceStorage AddressSpace { get; set; }

        /// <summary>
        /// Адресное пространство
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
        /// Текстовый идентификатор банка данных
        /// </summary>
        public virtual string DataBankPrefix { get; set; }
    }
}