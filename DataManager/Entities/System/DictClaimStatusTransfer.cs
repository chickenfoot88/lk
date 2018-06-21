using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Enums;
using OhDataManager.Extensions;

namespace OhDataManager.Entities.System
{
    /// <summary>
    /// Справочник "Переходы статусов заявок"
    /// </summary>
    [Table(nameof(DictClaimStatusTransfer), Schema = "system")]
    public class DictClaimStatusTransfer : BaseEntity
    {
        /// <summary>
        /// Статус заявки
        /// </summary>
        public virtual EClaimStatus Status { get; set; }

        /// <summary>
        /// Статус заявки
        /// </summary>
        [NotMapped]
        public string StatusText => Status.GetDisplayName();

        /// <summary>
        /// Следующий статус заявки
        /// </summary>
        public virtual EClaimStatus NextStatus { get; set; }

        /// <summary>
        /// Следующий статус заявки
        /// </summary>
        [NotMapped]
        public string NextStatusText => NextStatus.GetDisplayName();
    }
}
