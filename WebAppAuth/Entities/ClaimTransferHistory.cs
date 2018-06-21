using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;
using OhDataManager.Enums;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// История смены статусов заявки
    /// </summary>
    [Table(nameof(ClaimTransferHistory), Schema = "main")]
    public class ClaimTransferHistory : BaseOhEntity
    {
        /// <summary>
        /// Заявка
        /// </summary>
        public AbonentClaim AbonentClaim { get; set; }

        /// <summary>
        /// Заявка
        /// </summary>
        public long ClaimId { get; set; }

        /// <summary>
        /// Предыдущий статус заявки
        /// </summary>
        public EClaimStatus PreviousStatus { get; set; }

        /// <summary>
        /// Предыдущий статус заявки
        /// </summary>
        [NotMapped]
        public string PreviousStatusText => PreviousStatus.GetDisplayName();

        /// <summary>
        /// Новый статус заявки
        /// </summary>
        public EClaimStatus NewStatus { get; set; }

        /// <summary>
        /// Новый статус заявки
        /// </summary>
        [NotMapped]
        public string NewStatusText => NewStatus.GetDisplayName();
    }
}