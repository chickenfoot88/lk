using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OhDataManager.Entities.Base;

namespace WebAppAuth.Models
{
    /// <summary>
    /// Дом
    /// </summary>
    public class HouseModel : BaseEntity
    {
        /// <summary>
        /// Полный адрес до дома
        /// </summary>
        [Display(Name = "Полный адрес до дома")]
        public virtual string HouseFullAddress { get; set; }
    }
}