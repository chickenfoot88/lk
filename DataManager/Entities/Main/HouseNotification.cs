using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Уведомление по дому
    /// </summary>
    [Table(nameof(HouseNotification), Schema = "main")]
    public class HouseNotification : BaseOhEntity
    {
        /// <summary>
        /// Уведомление
        /// </summary>
        [Display(Name = "Уведомление")]
        public virtual Notification Notification { get; set; }
        public virtual long NotificationId { get; set; }

        /// <summary>
        /// Дом
        /// </summary>
        [Display(Name = "Дом")]
        public virtual House House { get; set; }
        public virtual long HouseId { get; set; }
    }
}
