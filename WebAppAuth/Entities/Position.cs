using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace WebAppAuth.Entities
{
    /// <summary>
    /// Должность
    /// </summary>
    [Table(nameof(Position), Schema = "main")]
    public class Position : BaseEntity
    {
        /// <summary>
        /// Должность
        /// </summary>
        [Display(Name = "Должность")]
        public virtual string Name { get; set; }
    }
}
