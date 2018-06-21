using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Уведомление
    /// </summary>
    [Table(nameof(Notification), Schema = "main")]
    public class Notification : BaseOhEntity
    {
        /// <summary>
        /// Заголовок
        /// </summary>
        [Display(Name = "Заголовок")]
        public virtual string Title { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        [Display(Name = "Содержимое")]
        public virtual string Content { get; set; }
    }
}
