using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Адресное пространство
    /// </summary>
    [Table("AddressSpace", Schema = "main")]
    public class AddressSpace : BaseOhEntity, IAddressSpace
    {
        /// <summary>
        /// Полный адрес до улицы
        /// </summary>
        [Required]
        public virtual string StreetFullAddress { get; set; }
        
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
    }
}