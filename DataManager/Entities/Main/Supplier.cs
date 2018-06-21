using System.ComponentModel.DataAnnotations.Schema;
using OhDataManager.Entities.Base;

namespace OhDataManager.Entities.Main
{
    /// <summary>
    /// Поставщик услуг
    /// </summary>
    [Table("Supplier", Schema = "main")]
    public class Supplier : Base.BaseOhEntity, ISupplier
    {
        /// <summary>
        /// Наименование поставщика
        /// </summary>
        public string SupplierName { get; set; }
    }
}